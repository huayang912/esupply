package com.faurecia.job;

import java.io.File;
import java.io.IOException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.io.FileUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.OutboundLog;
import com.faurecia.model.Plant;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.MailEngine;
import com.faurecia.service.OutboundLogManager;
import com.faurecia.util.DateUtil;
import com.faurecia.util.FTPClientUtil;

public class DataOutboundJob {
	
	private final Log log = LogFactory.getLog(getClass());
	private GenericManager<Plant, String> plantManager;
	private DeliveryOrderManager deliveryOrderManager;
	private OutboundLogManager outboundLogManager;
	
	protected MailEngine mailEngine;
	protected SimpleMailMessage mailMessage;
	protected String errorOutboundTemplateName;
	
	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}
	
	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}

	public void setOutboundLogManager(OutboundLogManager outboundLogManager) {
		this.outboundLogManager = outboundLogManager;
	}
	
	public void setMailEngine(MailEngine mailEngine) {
		this.mailEngine = mailEngine;
	}

	public void setMailMessage(SimpleMailMessage mailMessage) {
		this.mailMessage = mailMessage;
	}

	public void setErrorOutboundTemplateName(String errorOutboundTemplateName) {
		this.errorOutboundTemplateName = errorOutboundTemplateName;
	}

	public void run() {
		log.info("Start run data outbound job.");
		
		Date nowDate = new Date();
		List<Plant> plantList = this.plantManager.getAll();
		if (plantList != null && plantList.size() > 0) {
			for (int i = 0; i < plantList.size(); i++) {
				Plant plant = plantList.get(i);

				if (plant.getNextOutboundDate() == null || nowDate.compareTo(plant.getNextOutboundDate()) > 0) {
					log.info("Start outbound data for plant: " + plant.getName() + ".");
					UploadFiles(plant, "service", new Date());

					// 设置下次运行时间
					Plant newPlant = this.plantManager.get(plant.getCode());
					if (newPlant.getNextOutboundDate() == null) {
						newPlant.setNextOutboundDate(nowDate);
					}
					newPlant.setNextOutboundDate(DateUtil.AddTime(newPlant.getNextOutboundDate(), newPlant.getOutboundIntervalType(), newPlant
									.getOutboundInterval()));
					log.info("Set next outbound date: " + DateUtil.getDateTime("MM/dd/yyyy HH:mm:ss.SSS", newPlant.getNextOutboundDate()) + ".");
					this.plantManager.save(newPlant);
					log.info("End outbound data for plant: " + newPlant.getName() + ".");
				}
			}
		} else {
			log.info("No plant found.");
		}
		log.info("End run data outbound job.");
	}
	
	private void UploadFiles(Plant plant, String user, Date nowDate) {
		FTPClientUtil ftpClientUtil = new FTPClientUtil();

		String errorDo = "";
		try {
			log.info("Connect to ftp server: " + plant.getFtpServer() + ".");
			ftpClientUtil.connectServer(plant.getFtpServer(), plant.getFtpPort(), plant.getFtpUser(), plant.getFtpPassword(), plant.getFtpPath());
		} catch (Exception e) {
			log.error("Error logon to ftp server.", e);
			return;
		}
		
		List<DeliveryOrder> deliveryOrderList = this.deliveryOrderManager.getUnexportDeliveryOrderByPlant(plant);
		if (deliveryOrderList != null && deliveryOrderList.size() > 0) {
			for(int i = 0; i < deliveryOrderList.size(); i++) {
				OutboundLog outboundLog = null;
				try {
					DeliveryOrder deliveryOrder = deliveryOrderList.get(i);
					
					// 查找是否已经记录过日志
					outboundLog = this.outboundLogManager.getOutboundLogByDoNo(deliveryOrder.getDoNo());
					if (outboundLog == null) {
						outboundLog = new OutboundLog();
						outboundLog.setCreateDate(nowDate);
						outboundLog.setCreateUser(user);
						outboundLog.setPlantSupplier(deliveryOrder.getPlantSupplier());
					}
					outboundLog.setDoNo(deliveryOrder.getDoNo());
					outboundLog.setLastModifyDate(nowDate);
					outboundLog.setLastModifyUser(user);
					
					String tempDirString = plant.getTempFileDirectory() + File.separator + plant.getCode() + File.separator + "DESADV";
					File filePath = new File(tempDirString);
					FileUtils.forceMkdir(filePath);
					
					DateFormat format = new SimpleDateFormat("yyyyMMdd_HHmmss_SSS");
					String fileName = "DESADV_" + plant.getCode() + format.format(new Date());
					File file = this.deliveryOrderManager.exportDeliveryOrder(deliveryOrder, filePath, fileName, "xml");
					
					String ftpDirectory = plant.getFtpPath() + FTPClientUtil.SEPERATE + "DESADV" + FTPClientUtil.SEPERATE + "OUT";
					ftpClientUtil.changeDirectory(ftpDirectory);
					ftpClientUtil.uploadFile(file.getAbsolutePath(), fileName + ".xml");
					
					String archiveDirString = plant.getArchiveFileDirectory() + File.separator + plant.getCode() + File.separator + "DESADV";
					File archivFile = new File(archiveDirString + File.separator + fileName + ".xml");
					FileUtils.copyFile(file, archivFile);
					
					deliveryOrder.setIsExport(true);
					this.deliveryOrderManager.save(deliveryOrder);
					
					outboundLog.setFileName(fileName + ".xml");
					outboundLog.setOutboundResult("success");
					
					file.deleteOnExit();
				} catch (Exception ex) {
					outboundLog.setOutboundResult("fail");
					outboundLog.setMemo(ex.getMessage());
				} finally {
					if (outboundLog != null) {
						this.outboundLogManager.save(outboundLog);
						
						if ("fail".equals(outboundLog.getOutboundResult())) {
							
							if (errorDo.trim().length() == 0) {
								errorDo = outboundLog.getDoNo();
							} else {
								errorDo += ", " + outboundLog.getDoNo();
							}
						}
					}
				}
			}
		}	else {
			log.info("No deliver order found to outbound.");
		}
		
		try {
			if (ftpClientUtil != null) {
				ftpClientUtil.closeServer();
			}
		} catch (IOException e) {
			log.error("Error close ftp server.", e);
		}
		
		if (errorDo.trim().length() > 0) {
			try {
				DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
				Date dateNow = new Date();
				// Email通知
				log.info("Send error outbound email to " + plant.getErrorLogEmail1() + " and " + plant.getErrorLogEmail2());
				mailMessage.setTo(new String[] {plant.getErrorLogEmail1(), plant.getErrorLogEmail2()});
				Map<String, Object> model = new HashMap<String, Object>();
				model.put("plantName", plant.getName());
				model.put("errorDo", errorDo);
				mailMessage.setSubject("Inbound data error " + df.format(dateNow));
				mailEngine.sendMessage(mailMessage, errorOutboundTemplateName, model);
				log.info("Send error outbound email successful.");
			} catch (MailException mailEx) {
				log.error("Error when send error outbound mail.", mailEx);
			}
		}
	}
}
