package com.faurecia.job;

import java.io.File;
import java.io.IOException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import javax.xml.bind.JAXBException;

import org.apache.commons.io.FileUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.Plant;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.OutboundLogManager;
import com.faurecia.util.DateUtil;
import com.faurecia.util.FTPClientUtil;

public class DataOutboundJob {
	
	private final Log log = LogFactory.getLog(getClass());
	private GenericManager<Plant, String> plantManager;
	private DeliveryOrderManager deliveryOrderManager;
	private OutboundLogManager outboundLogManager;
	
	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}
	
	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}

	public void setOutboundLogManager(OutboundLogManager outboundLogManager) {
		this.outboundLogManager = outboundLogManager;
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
					UploadFiles(plant, "service");

					// 设置下次运行时间
					if (plant.getNextOutboundDate() == null) {
						plant.setNextOutboundDate(nowDate);
					}
					plant.setNextOutboundDate(DateUtil.AddTime(plant.getNextOutboundDate(), plant.getOutboundIntervalType(), plant
									.getOutboundInterval()));
					log.info("Set next outbound date: " + DateUtil.getDateTime("MM/dd/yyyy HH:mm:ss.SSS", plant.getNextOutboundDate()) + ".");
					this.plantManager.save(plant);
					log.info("End outbound data for plant: " + plant.getName() + ".");
				}
			}
		} else {
			log.info("No plant found.");
		}
		log.info("End run data outbound job.");
	}
	
	private void UploadFiles(Plant plant, String user) {
		FTPClientUtil ftpClientUtil = new FTPClientUtil();

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
				try {
					DeliveryOrder deliveryOrder = deliveryOrderList.get(i);
					
					String tempDirString = plant.getTempFileDirectory() + File.separator + "DESADV";
					File filePath = new File(tempDirString);
					FileUtils.forceMkdir(filePath);
					
					DateFormat format = new SimpleDateFormat("yyyyMMdd_HHmmss_SSS");
					String fileName = "DESADV_" + plant.getCode() + format.format(new Date());
					File file = this.deliveryOrderManager.exportDeliveryOrder(deliveryOrder, filePath, fileName, "xml");
					
					String ftpDirectory = plant.getFtpPath() + FTPClientUtil.SEPERATE + "DESADV" + FTPClientUtil.SEPERATE + "INP";
					ftpClientUtil.changeDirectory(ftpDirectory);
					ftpClientUtil.uploadFile(file.getAbsolutePath(), fileName + ".xml");
					
					deliveryOrder.setIsExport(true);
					this.deliveryOrderManager.save(deliveryOrder);
					
					file.deleteOnExit();
				} catch (JAXBException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
				} catch (IOException e) {
					// TODO Auto-generated catch block
					e.printStackTrace();
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
	}
}
