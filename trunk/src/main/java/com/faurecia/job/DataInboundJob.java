package com.faurecia.job;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Collections;
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
import com.faurecia.model.InboundLog;
import com.faurecia.model.Plant;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.Receipt;
import com.faurecia.model.Schedule;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.MailEngine;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.util.DateUtil;
import com.faurecia.util.FTPClientUtil;

public class DataInboundJob {

	private final Log log = LogFactory.getLog(getClass());
	private GenericManager<Plant, String> plantManager;
	private InboundLogManager inboundLogManager;
	private PurchaseOrderManager purchaseOrderManager;
	private ScheduleManager scheduleManager;
	private ReceiptManager receiptManager;
	private DeliveryOrderManager deliveryOrderManager;
	
	protected MailEngine mailEngine;
	protected SimpleMailMessage mailMessage;
	protected String errorInboundTemplateName;
	private final String[] dataTypeArray = new String[] { "ORDERS", "DELINS", "MBGMCR", "DESADV" };

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}

	public void setPurchaseOrderManager(PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
	}

	public void setReceiptManager(ReceiptManager receiptManager) {
		this.receiptManager = receiptManager;
	}
	
	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}
	
	public void setMailEngine(MailEngine mailEngine) {
		this.mailEngine = mailEngine;
	}

	public void setMailMessage(SimpleMailMessage mailMessage) {
		this.mailMessage = mailMessage;
	}

	public void setErrorInboundTemplateName(String errorInboundTemplateName) {
		this.errorInboundTemplateName = errorInboundTemplateName;
	}

	public void run() {
		log.info("Start run data inbound job.");
		Date nowDate = new Date();
		List<Plant> plantList = this.plantManager.getAll();
		if (plantList != null && plantList.size() > 0) {
			for (int i = 0; i < plantList.size(); i++) {
				Plant plant = plantList.get(i);

				if (plant.getNextInboundDate() == null || nowDate.compareTo(plant.getNextInboundDate()) > 0) {
					log.info("Start inbound data for plant: " + plant.getName() + ".");
					DownloadFiles(plant.getFtpServer(), plant.getFtpPort(), plant.getFtpUser(), plant.getFtpPassword(), plant.getFtpPath(), plant
							.getTempFileDirectory(), plant.getArchiveFileDirectory(), plant.getErrorFileDirectory(), nowDate, "service", plant);

					// 设置下次运行时间
					Plant newPlant = this.plantManager.get(plant.getCode());
					if (newPlant.getNextInboundDate() == null) {
						newPlant.setNextInboundDate(nowDate);
					}
					newPlant.setNextInboundDate(DateUtil.AddTime(newPlant.getNextInboundDate(), newPlant.getInboundIntervalType(), newPlant
									.getInboundInterval()));
					log.info("Set next inbound date: " + DateUtil.getDateTime("MM/dd/yyyy HH:mm:ss.SSS", newPlant.getNextInboundDate()) + ".");
					this.plantManager.save(newPlant);
					log.info("End inbound data for plant: " + newPlant.getName() + ".");
				}
			}
		} else {
			log.info("No plant found.");
		}
		log.info("End run data inbound job.");
	}

	private void DownloadFiles(String ftpServer, int ftpPort, String ftpUser, String ftpPassword, String ftpPath, String tempFileDirectory,
			String archiveFileDirectory, String errorFileDirectory, Date nowDate, String user, Plant plant) {
		FTPClientUtil ftpClientUtil = new FTPClientUtil();

		String errorFileName = "";
		try {
			log.info("Connect to ftp server: " + ftpServer + ".");
			ftpClientUtil.connectServer(ftpServer, ftpPort, ftpUser, ftpPassword, ftpPath);
		} catch (Exception e) {
			log.error("Error logon to ftp server.", e);
			return;
		}

		for (int i = 0; i < dataTypeArray.length; i++) {
			String dataType = dataTypeArray[i];
			List<String> fileNameList = null;
			try {
				String ftpDirectory = ftpPath + FTPClientUtil.SEPERATE + dataType + FTPClientUtil.SEPERATE + "INP";
				ftpClientUtil.changeDirectory(ftpDirectory);
				log.info("Fetching files in directory: " + ftpDirectory + ".");
				fileNameList = ftpClientUtil.getFileList(".");
			} catch (Exception e) {
				log.error("Error fetching file on ftp server.", e);
				continue;
			}

			if (fileNameList != null && fileNameList.size() > 0) {
				Collections.sort(fileNameList);
				for (int j = 0; j < fileNameList.size(); j++) {

					String fileName = fileNameList.get(j); // 获取下载文件名
					String filePrefix = fileName.substring(0, fileName.lastIndexOf('.'));
					String fileSuffix = fileName.substring(fileName.lastIndexOf('.') - 1);
					if (!fileName.toLowerCase().endsWith(".xml")) {
						continue;
					}

					// 查找是否已经记录过日志
					InboundLog inboundLog = this.inboundLogManager.getInboundLogByDataTypeAndFileName(dataType, fileName);
					if (inboundLog == null) {
						inboundLog = new InboundLog();
						inboundLog.setDataType(dataType);
						inboundLog.setFileName(fileName);
						inboundLog.setCreateDate(nowDate);
						inboundLog.setCreateUser(user);
					}
					inboundLog.setLastModifyDate(nowDate);
					inboundLog.setLastModifyUser(user);

					File tempFile = null;
					InputStream inputStream = null;
					try {
						// 下载文件至临时目录
						String tempDirString = tempFileDirectory + File.separator + plant.getCode() + File.separator + dataType;
						File tempDir = new File(tempDirString);
						FileUtils.forceMkdir(tempDir);

						log.info("Download file: " + fileName + " to temperary folder: " + tempDirString + ".");
						tempFile = File.createTempFile(filePrefix, fileSuffix, tempDir);
						ftpClientUtil.download(fileName, tempFile.getAbsolutePath());

						inputStream = new FileInputStream(tempFile);
					} catch (IOException ex) {
						log.error("Error download file: " + fileName + ".", ex);
						inboundLog.setInboundResult("fail");
						inboundLog.setMemo(ex.getMessage());

						this.inboundLogManager.save(inboundLog);

						continue;
					}

					// 记录至数据库
					log.info("Processing file: " + fileName);
					PurchaseOrder po = null;
					Schedule schedule = null;
					Receipt receipt = null;
					List<DeliveryOrder> doList = null;
					try {
						if (dataType.equals("ORDERS")) {
							po = this.purchaseOrderManager.saveSingleFile(inputStream, inboundLog);
						} else if (dataType.equals("DELINS")) {
							schedule = this.scheduleManager.saveSingleFile(inputStream, inboundLog);
						} else if (dataType.equals("MBGMCR")) {
							receipt = this.receiptManager.saveSingleFile(inputStream, inboundLog);
						} else if (dataType.equals("DESADV")) {
							doList = this.deliveryOrderManager.saveMultiFile(inputStream, inboundLog);
						}
					} catch (Exception ex) {
						log.error("Error when save file to database.", ex);
						inboundLog.setInboundResult("fail");
						inboundLog.setMemo(ex.getMessage());
					}

					String localBackupDirectory = null;
					File backupFile = null;
					try {
						// 备份文件
						if (inboundLog.getInboundResult() == "success") {
							localBackupDirectory = archiveFileDirectory + File.separator + plant.getCode() + File.separator + dataType;
							log.info("Processing file: " + fileName + " success, back up file to archive directory: " + localBackupDirectory);
						} else {
							localBackupDirectory = errorFileDirectory + File.separator + plant.getCode() + File.separator + dataType;
							log.info("Processing file: " + fileName + " fail, back up file to error directory: " + localBackupDirectory);
						}

						FileUtils.forceMkdir(new File(localBackupDirectory));

						backupFile = new File(localBackupDirectory + File.separator + fileName);

						FileUtils.copyFile(tempFile, backupFile);
						log.info("Backup file: " + fileName + " success.");

						inboundLog.setFullFilePath(backupFile.getAbsolutePath());

						// 删除Ftp文件
						ftpClientUtil.deleteFile(fileName);
						log.info("Delete file: " + fileName + " on ftp success.");

						try {
							if (inputStream != null) {
								inputStream.close();
							}
							FileUtils.forceDelete(tempFile);
						} catch (IOException ex) {
							// 删除temp文件失败，不影响流程，所以单独捕获
							log.warn("Fail to delete temp file", ex);
						}
					} catch (Exception ex) {
						log.error("Error when backup file.", ex);
						inboundLog.setInboundResult("fail");
						inboundLog.setMemo(ex.getMessage());

						// 本地文件备份失败
						if (po != null && po.getPoNo() != null) {
							// 手工回滚
							this.purchaseOrderManager.remove(po.getPoNo());
						}

						if (schedule != null && schedule.getScheduleNo() != null) {
							// 手工回滚
							this.scheduleManager.remove(schedule.getScheduleNo());
						}
						
						if (receipt != null && receipt.getReceiptNo() != null) {
							// 手工回滚
							this.receiptManager.remove(receipt.getReceiptNo());
						}
						
						if (doList != null && doList.size() > 0) {
							// 手工回滚
							for(DeliveryOrder deliveryOrder : doList) {								
								this.deliveryOrderManager.remove(deliveryOrder.getDoNo());
							}
						}

						if (backupFile != null) {
							try {
								FileUtils.forceDelete(backupFile);
							} catch (IOException e) {
								log.error("Error delete backup file.", e);
							}
						}
					} finally {
						this.inboundLogManager.save(inboundLog);
						
						if ("fail".equals(inboundLog.getInboundResult())) {
							if (errorFileName.trim().length() == 0) {
								errorFileName = inboundLog.getFileName();
							} else {
								errorFileName += ", " + inboundLog.getFileName();
							}
						}
					}
				}
			}
		}

		try {
			if (ftpClientUtil != null) {
				ftpClientUtil.closeServer();
			}
		} catch (IOException e) {
			log.error("Error close ftp server.", e);
		}
		
		if (errorFileName.trim().length() > 0) {
			try {
				DateFormat df = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss");
				Date dateNow = new Date();
				// Email通知
				log.info("Send error inbound email to " + plant.getErrorLogEmail1() + " and " + plant.getErrorLogEmail2());
				mailMessage.setTo(new String[] {plant.getErrorLogEmail1(), plant.getErrorLogEmail2()});
				Map<String, Object> model = new HashMap<String, Object>();
				model.put("plantName", plant.getName());
				model.put("errorFileName", errorFileName);
				mailMessage.setSubject("Inbound data error " + df.format(dateNow));
				mailEngine.sendMessage(mailMessage, errorInboundTemplateName, model);
				log.info("Send error inbound email successful.");
			} catch (MailException mailEx) {
				log.error("Error when send error inbound mail.", mailEx);
			}
		}
	}
}
