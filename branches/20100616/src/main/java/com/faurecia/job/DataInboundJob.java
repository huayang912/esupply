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
import com.faurecia.model.PurchaseOrderApproval;
import com.faurecia.model.Receipt;
import com.faurecia.model.Schedule;
import com.faurecia.service.GenericManager;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.PurchaseOrderApprovalManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.util.DateUtil;
import com.faurecia.util.FTPClientUtil;

public class DataInboundJob {

	private final Log log = LogFactory.getLog(getClass());
	private GenericManager<Plant, String> plantManager;
	private InboundLogManager inboundLogManager;
	private PurchaseOrderApprovalManager purchaseOrderManager;
	private ScheduleManager scheduleManager;
	private ReceiptManager receiptManager;
	private final String[] dataTypeArray = new String[] { "ORDERS", "DELINS", "MBGMCR" };

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}

	public void setPurchaseOrderApprovalManager(PurchaseOrderApprovalManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
	}

	public void setReceiptManager(ReceiptManager receiptManager) {
		this.receiptManager = receiptManager;
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
							.getTempFileDirectory(), plant.getArchiveFileDirectory(), plant.getErrorFileDirectory(), nowDate, "service", plant.getCode());

					// �����´�����ʱ��
					if (plant.getNextInboundDate() == null) {
						plant.setNextInboundDate(nowDate);
					}
					plant
							.setNextInboundDate(DateUtil.AddTime(plant.getNextInboundDate(), plant.getInboundIntervalType(), plant
									.getInboundInterval()));
					log.info("Set next inbound date: " + DateUtil.getDateTime("MM/dd/yyyy HH:mm:ss.SSS", plant.getNextInboundDate()) + ".");
					this.plantManager.save(plant);
					log.info("End inbound data for plant: " + plant.getName() + ".");
				}
			}
		} else {
			log.info("No plant found.");
		}
		log.info("End run data inbound job.");
	}

	private void DownloadFiles(String ftpServer, int ftpPort, String ftpUser, String ftpPassword, String ftpPath, String tempFileDirectory,
			String archiveFileDirectory, String errorFileDirectory, Date nowDate, String user, String plantCode) {
		FTPClientUtil ftpClientUtil = new FTPClientUtil();

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

					String fileName = fileNameList.get(j); // ��ȡ�����ļ���
					String filePrefix = fileName.substring(0, fileName.lastIndexOf('.'));
					String fileSuffix = fileName.substring(fileName.lastIndexOf('.') - 1);

					// �����Ƿ��Ѿ���¼����־
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
						// �����ļ�����ʱĿ¼
						String tempDirString = tempFileDirectory + File.separator + plantCode + File.separator + dataType;
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

					// ��¼�����ݿ�
					log.info("Processing file: " + fileName);
					PurchaseOrderApproval po = null;
					Schedule schedule = null;
					Receipt receipt = null;
					try {
						if (dataType.equals("ORDERS")) {
							po = this.purchaseOrderManager.saveSingleFile(inputStream, inboundLog);
						} else if (dataType.equals("DELINS")) {
							schedule = this.scheduleManager.saveSingleFile(inputStream, inboundLog);
						} else if (dataType.equals("MBGMCR")) {
							receipt = this.receiptManager.saveSingleFile(inputStream, inboundLog);
						}
					} catch (Exception ex) {
						log.error("Error when save file to database.", ex);
						inboundLog.setInboundResult("fail");
						inboundLog.setMemo(ex.getMessage());
					}

					String localBackupDirectory = null;
					File backupFile = null;
					try {
						// �����ļ�
						if (inboundLog.getInboundResult() == "success") {
							localBackupDirectory = archiveFileDirectory + File.separator + plantCode + File.separator + dataType;
							log.info("Processing file: " + fileName + " success, back up file to archive directory: " + localBackupDirectory);
						} else {
							localBackupDirectory = errorFileDirectory + File.separator + plantCode + File.separator + dataType;
							log.info("Processing file: " + fileName + " fail, back up file to error directory: " + localBackupDirectory);
						}

						FileUtils.forceMkdir(new File(localBackupDirectory));

						backupFile = new File(localBackupDirectory + File.separator + fileName);

						FileUtils.copyFile(tempFile, backupFile);
						log.info("Backup file: " + fileName + " success.");

						inboundLog.setFullFilePath(backupFile.getAbsolutePath());

						// ɾ��Ftp�ļ�
						ftpClientUtil.deleteFile(fileName);
						log.info("Delete file: " + fileName + " on ftp success.");

						try {
							if (inputStream != null) {
								inputStream.close();
							}
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
						if (po != null && po.getPoNo() != null) {
							// �ֹ��ع�
							this.purchaseOrderManager.remove(po.getPoNo());
						}

						if (schedule != null && schedule.getScheduleNo() != null) {
							// �ֹ��ع�
							this.scheduleManager.remove(schedule.getScheduleNo());
						}
						
						if (receipt != null && receipt.getReceiptNo() != null) {
							// �ֹ��ع�
							this.receiptManager.remove(receipt.getReceiptNo());
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