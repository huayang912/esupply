package com.faurecia.job;

import java.io.File;
import java.io.FileInputStream;
import java.io.IOException;
import java.io.InputStream;
import java.util.Collections;
import java.util.Date;
import java.util.List;

import org.apache.commons.io.FileUtils;
import org.apache.commons.logging.Log;
import org.apache.commons.logging.LogFactory;

import com.faurecia.model.InboundLog;
import com.faurecia.model.Plant;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.service.GenericManager;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.util.FTPClientUtil;

public class DataInboundOutboundJob {

	private final Log log = LogFactory.getLog(getClass());
	private GenericManager<Plant, String> plantManager;
	private GenericManager<InboundLog, Integer> inboundLogManager;
	private PurchaseOrderManager purchaseOrderManager;
	private final String[] dataTypeArray = new String[] { "ORDERS" };

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setInboundLogManager(
			GenericManager<InboundLog, Integer> inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}

	public void setPurchaseOrderManager(
			PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void run() {
		List<Plant> plantList = this.plantManager.getAll();
		if (plantList != null && plantList.size() > 0) {
			for (int i = 0; i < plantList.size(); i++) {
				Plant plant = plantList.get(i);

				DownloadFiles(plant.getFtpServer(), plant.getFtpPort(), plant.getFtpUser(),
						plant.getFtpPassword(), plant.getFtpPath(), plant.getTempFileDirectory(),
						plant.getArchiveFileDirectory(), plant.getErrorFileDirectory(), "job");
			}
		}
	}

	public void DownloadFiles(String ftpServer, int ftpPort, String ftpUser,
			String ftpPassword, String ftpPath, String tempFileDirectory,
			String archiveFileDirectory, String errorFileDirectory, String user) {
		FTPClientUtil ftpClientUtil = new FTPClientUtil();

		Date nowDate = new Date();

		try {
			ftpClientUtil.connectServer(ftpServer, ftpPort, ftpUser,
					ftpPassword, ftpPath);
		} catch (Exception e) {
			log.error("Error logon to ftp server.", e);
		}

		for (int i = 0; i < dataTypeArray.length; i++) {
			String dataType = dataTypeArray[i];
			List<String> fileNameList = null;
			try {
				fileNameList = ftpClientUtil.getFileList(ftpPath
						+ File.separator + dataType);
			} catch (Exception e) {
				log.error("Error list file on ftp server.", e);
				continue;
			}

			if (fileNameList != null && fileNameList.size() > 0) {
				Collections.sort(fileNameList);
				for (int j = 0; j < fileNameList.size(); j++) {

					String fileName = fileNameList.get(i); // 获取下载文件名
					String filePrefix = fileName.substring(0, fileName
							.lastIndexOf('.'));
					String fileSuffix = fileName.substring(fileName
							.lastIndexOf('.') - 1);

					InboundLog inboundLog = new InboundLog();
					inboundLog.setDataType(dataType);
					inboundLog.setFileName(fileName);
					inboundLog.setCreateDate(nowDate);
					inboundLog.setCreateUser(user);
					inboundLog.setLastModifyDate(nowDate);
					inboundLog.setLastModifyUser(user);

					File tempFile = null;
					InputStream inputStream = null;
					try {
						// 下载文件至临时目录
						tempFile = File.createTempFile(filePrefix, fileSuffix,
								new File(tempFileDirectory + File.separator
										+ dataType));
						ftpClientUtil.download(fileName, tempFile
								.getAbsolutePath());

						inputStream = new FileInputStream(tempFile);
					} catch (IOException ex) {
						inboundLog.setInboundResult("fail");
						inboundLog.setMemo(ex.getMessage());
						
						this.inboundLogManager.save(inboundLog);
						
						continue;
					}

					PurchaseOrder po = null;
					if (dataType.equals("ORDERS")) {
						po = this.purchaseOrderManager.SaveSingleFile(
								inputStream, inboundLog);
					}

					String localBackupDirectory = null;
					File backupFile = null;

					try {
						// 备份文件
						if (inboundLog.getInboundResult() == "success") {
							localBackupDirectory = archiveFileDirectory
									+ File.separator + dataType;
						} else {
							localBackupDirectory = errorFileDirectory
									+ File.separator + dataType;
						}

						FileUtils.forceMkdir(new File(localBackupDirectory));

						backupFile = new File(localBackupDirectory
								+ File.separator + fileName);

						FileUtils.copyFile(tempFile, backupFile);

						inboundLog
								.setFullFilePath(backupFile.getAbsolutePath());

						// 删除Ftp文件
						ftpClientUtil.deleteFile(fileName);

						try {
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
						if (po != null) {
							// 手工回滚
							this.purchaseOrderManager.remove(po.getPoNo());
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
	}
}
