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

					String fileName = fileNameList.get(i); // ��ȡ�����ļ���
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
						// �����ļ�����ʱĿ¼
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
						// �����ļ�
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

						// ɾ��Ftp�ļ�
						ftpClientUtil.deleteFile(fileName);

						try {
							FileUtils.forceDelete(tempFile);
						} catch (IOException ex) {
							// ɾ��temp�ļ�ʧ�ܣ���Ӱ�����̣����Ե�������
							log.warn("Fail to delete temp file", ex);
						}
					} catch (Exception ex) {
						log.error("Error when backup file.", ex);
						inboundLog.setInboundResult("fail");
						inboundLog.setMemo(ex.getMessage());

						// �����ļ�����ʧ��
						if (po != null) {
							// �ֹ��ع�
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
