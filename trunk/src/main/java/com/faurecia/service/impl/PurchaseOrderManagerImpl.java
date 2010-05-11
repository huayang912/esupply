package com.faurecia.service.impl;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.IOException;
import java.io.InputStream;
import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Collections;
import java.util.Date;
import java.util.List;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

import org.apache.commons.io.FileUtils;
import org.springframework.orm.ObjectRetrievalFailureException;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;
import com.faurecia.model.order.ORDERS02;
import com.faurecia.model.order.ORDERS02E1EDK03;
import com.faurecia.model.order.ORDERS02E1EDKA1;
import com.faurecia.model.order.ORDERS02E1EDP01;
import com.faurecia.model.order.ORDERS02E1EDP19;
import com.faurecia.model.order.ORDERS02E1EDP20;
import com.faurecia.service.DataConvertException;
import com.faurecia.service.GenericManager;
import com.faurecia.service.ItemManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.util.FTPClientUtil;

public class PurchaseOrderManagerImpl extends
		GenericManagerImpl<PurchaseOrder, String> implements
		PurchaseOrderManager {

	private String BACKUP_DIRECTORY_FULL_PATH = "D:\\1\\ORDERS\\";
	private String SUCCESS_BACKUP_DIRECTORY = "success";
	private String ERROR_BACKUP_DIRECTORY = "error";
	private GenericManager<Plant, String> plantManager;
	private SupplierManager supplierManager;
	private PlantSupplierManager plantSupplierManager;
	private ItemManager itemManager;
	private SupplierItemManager supplierItemManager;
	private GenericManager<PurchaseOrder, String> purchaseOrderManager;
	private GenericManager<InboundLog, Integer> inboundLogManager;
	private Unmarshaller unmarshaller;

	public PurchaseOrderManagerImpl(GenericDao<PurchaseOrder, String> genericDao)
			throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.order");
		unmarshaller = jc.createUnmarshaller();
	}

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public void setPlantSupplierManager(
			PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	public void setSupplierItemManager(SupplierItemManager supplierItemManager) {
		this.supplierItemManager = supplierItemManager;
	}

	public void setPurchaseOrderManager(
			GenericManager<PurchaseOrder, String> purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void setInboundLogManager(
			GenericManager<InboundLog, Integer> inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}

	public void DownloadFiles(String ftpServer, int ftpPort, String ftpUser,
			String ftpPassword, String ftpPath, String user) {
		FTPClientUtil ftpClientUtil = new FTPClientUtil();
		List<String> fileNameList = null;
		Date nowDate = new Date();
		try {
			try {
				ftpClientUtil.connectServer(ftpServer, ftpPort, ftpUser,
						ftpPassword, ftpPath);
				fileNameList = ftpClientUtil.getFileList(ftpPath);
			} catch (Exception e) {
				// 访问FTP服务器失败
			}

			if (fileNameList != null && fileNameList.size() > 0) {
				Collections.sort(fileNameList);
				for (int i = 0; i < fileNameList.size(); i++) {

					String fileName = fileNameList.get(i);
					String filePrefix = fileName.substring(0, fileName
							.lastIndexOf('.'));
					String fileSuffix = fileName.substring(fileName
							.lastIndexOf('.') - 1);
					File tempFile = null;
					InputStream inputStream = null;
					try {
						tempFile = File.createTempFile(filePrefix, fileSuffix);
						ftpClientUtil.download(fileName, tempFile
								.getAbsolutePath());
						inputStream = new FileInputStream(tempFile);
					} catch (IOException e) {
						// 下载文件失败
					}

					//to-do 检索inboundLog
					InboundLog inboundLog = new InboundLog();
					inboundLog.setCreateDate(nowDate);
					inboundLog.setCreateUser(user);
					inboundLog.setDataType("ORDERS");
					inboundLog.setFileName(fileName);
					inboundLog.setLastModifyDate(nowDate);
					inboundLog.setLastModifyUser(user);

					PurchaseOrder po = SaveSingleFile(inputStream, inboundLog);

					String localBackupDirectory = null;
					File backupFile = null;
					try {
						// 备份文件
						if (inboundLog.getInboundResult() == "success") {
							localBackupDirectory = BACKUP_DIRECTORY_FULL_PATH
									+ SUCCESS_BACKUP_DIRECTORY;
						} else {
							localBackupDirectory = BACKUP_DIRECTORY_FULL_PATH
									+ ERROR_BACKUP_DIRECTORY;
						}

						FileUtils.forceMkdir(new File(localBackupDirectory));

						backupFile = new File(localBackupDirectory
								+ File.separator + fileName);

						FileUtils.copyFile(tempFile, backupFile);

						// 删除Ftp文件
						ftpClientUtil.deleteFile(fileName);

						try {
							FileUtils.forceDelete(tempFile);
						} catch (IOException ex) {
							//删除temp文件失败
						}
						
						inboundLog.setFullFilePath(backupFile.getAbsolutePath());
					} catch (Exception ex) {
						inboundLog.setInboundResult(ex.getMessage());
						// 本地文件备份失败
						if (po != null) {
							// 手工回滚
							this.genericDao.remove(po.getPoNo());
						}

						if (backupFile != null) {
							try {
								FileUtils.forceDelete(backupFile);
							} catch (IOException e) {
								// 删除备份文件失败
							}
						}
					} finally {
						this.inboundLogManager.save(inboundLog);
					}
				}
			}
		} finally {
			try {
				if (ftpClientUtil != null) {
					ftpClientUtil.closeServer();
				}
			} catch (IOException e) {
				// Ftp关闭失败
			}
		}
	}

	public void ReloadFile(InboundLog inboundLog, String userCode) {

		try {
			FileInputStream stream = new FileInputStream(inboundLog
					.getFullFilePath());
			SaveSingleFile(stream, inboundLog);
		} catch (FileNotFoundException fileNotFoundException) {
			inboundLog.setMemo(fileNotFoundException.getMessage());
		} finally {
			inboundLog.setLastModifyDate(new Date());
			inboundLog.setLastModifyUser(userCode);
			this.inboundLogManager.save(inboundLog);
		}
	}

	private PurchaseOrder SaveSingleFile(InputStream inputStream,
			InboundLog inboundLog) {

		try {
			ORDERS02 order = unmarshalOrder(inputStream);
			PurchaseOrder purchaseOrder = ORDERS02ToPO(order);

			if (inboundLog.getPlant() == null) {
				inboundLog.setPlant(purchaseOrder.getPlant());
			}

			if (inboundLog.getSupplier() == null) {
				inboundLog.setSupplier(purchaseOrder.getSupplier());
			}

			this.purchaseOrderManager.save(purchaseOrder);

			inboundLog.setInboundResult("success");

			return purchaseOrder;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal ORDERS.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert ORDERS to PO.",
					dataConvertException);
			inboundLog.setInboundResult("fail");

			PurchaseOrder purchaseOrder = (PurchaseOrder) dataConvertException
					.getObject();
			inboundLog.setPlant(purchaseOrder.getPlant());
			inboundLog.setSupplier(purchaseOrder.getSupplier());
			inboundLog.setMemo(dataConvertException.getMessage());
		} catch (Exception exception) {
			log.error("Error occur.", exception);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(exception.getMessage());
		}

		return null;
	}

	private ORDERS02 unmarshalOrder(InputStream stream) throws JAXBException {
		ORDERS02 o = (ORDERS02) unmarshaller.unmarshal(stream);
		return o;
	}

	private PurchaseOrder ORDERS02ToPO(final ORDERS02 order)
			throws DataConvertException {

		PurchaseOrder po = new PurchaseOrder();

		try {
			Plant plant = null;
			Supplier supplier = null;
			ORDERS02E1EDKA1 supplierE1EDKA1 = null;
			DateFormat dateFormat = new SimpleDateFormat("yyyyMMdd");

			po.setPoNo(order.getIDOC().getE1EDK01().getBELNR()); // po number

			List<ORDERS02E1EDK03> ORDERS02E1EDK03List = order.getIDOC()
					.getE1EDK03();
			if (ORDERS02E1EDK03List != null && ORDERS02E1EDK03List.size() > 0) {
				for (int i = 0; i < ORDERS02E1EDK03List.size(); i++) {
					ORDERS02E1EDK03 E1EDK03 = ORDERS02E1EDK03List.get(i);

					if ("012".equals(E1EDK03.getIDDAT())) {
						po.setCreateDate(dateFormat.parse(E1EDK03.getDATUM())); // document
						// date
					}
				}
			}

			List<ORDERS02E1EDKA1> ORDERS02E1EDKA1List = order.getIDOC()
					.getE1EDKA1();
			if (ORDERS02E1EDKA1List != null && ORDERS02E1EDKA1List.size() > 0) {
				for (int i = 0; i < ORDERS02E1EDKA1List.size(); i++) {
					ORDERS02E1EDKA1 E1EDKA1 = ORDERS02E1EDKA1List.get(i);
					if ("WE".equals(E1EDKA1.getPARVW())) {
						String plantCode = E1EDKA1.getLIFNR();

						plant = this.plantManager.get(plantCode); // plant
					} else if ("LF".equals(E1EDKA1.getPARVW())) {
						supplierE1EDKA1 = E1EDKA1;
						String supplierCode = E1EDKA1.getPARTN();

						try {
							supplier = this.supplierManager.get(supplierCode); // supplier
						} catch (ObjectRetrievalFailureException ex) {
							log
									.info("Supplier not found with the given supplier code: "
											+ supplierCode
											+ ", try to create a new one.");

							supplier = new Supplier();
							supplier.setCode(supplierCode);
							supplier.setName(E1EDKA1.getNAME1());

							supplier = this.supplierManager.save(supplier);
						}
					}
				}
			}

			po.setPlant(plant);
			po.setPlantName(plant.getName());
			po.setPlantAddress1(plant.getAddress1());
			po.setPlantAddress2(plant.getAddress2());
			po.setPlantContactPerson(plant.getContactPerson());
			po.setPlantPhone(plant.getPhone());
			po.setPlantFax(plant.getFax());

			PlantSupplier plantSupplier = this.plantSupplierManager
					.getPlantSupplier(plant, supplier);

			if (plantSupplier == null) {
				log.info("The relationship between Plant: " + plant.getCode()
						+ " and Supplier: " + supplier.getCode()
						+ " not found, try to create a new one.");

				plantSupplier = new PlantSupplier();
				plantSupplier.setSupplierName(supplierE1EDKA1.getNAME1());
				plantSupplier.setSupplierAddress1(supplierE1EDKA1.getSTRAS());
				plantSupplier.setSupplierAddress2(supplierE1EDKA1.getSTRS2());
				plantSupplier.setSupplierContactPerson(supplierE1EDKA1
						.getPERNR());
				plantSupplier.setSupplierPhone(supplierE1EDKA1.getTELF1());
				plantSupplier.setSupplierFax(supplierE1EDKA1.getTELFX());
				plantSupplier.setPlant(plant);
				plantSupplier.setSupplier(supplier);

				plantSupplier = this.plantSupplierManager.save(plantSupplier);
			}

			po.setSupplier(plantSupplier.getSupplier());
			po.setSupplierName(plantSupplier.getSupplierName());
			po.setSupplierAddress1(plantSupplier.getSupplierAddress1());
			po.setSupplierAddress2(plantSupplier.getSupplierAddress2());
			po.setSupplierContactPerson(plantSupplier
					.getSupplierContactPerson());
			po.setSupplierPhone(plantSupplier.getSupplierPhone());
			po.setSupplierFax(plantSupplier.getSupplierFax());

			// ----------------------------po detail---------------------
			List<ORDERS02E1EDP01> ORDERS02E1EDP01List = order.getIDOC()
					.getE1EDP01();

			if (ORDERS02E1EDP01List != null && ORDERS02E1EDP01List.size() > 0) {
				for (int i = 0; i < ORDERS02E1EDP01List.size(); i++) {

					ORDERS02E1EDP01 E1EDP01 = ORDERS02E1EDP01List.get(i);

					List<ORDERS02E1EDP19> ORDERS02E1EDP19List = E1EDP01
							.getE1EDP19();
					Item item = null;
					SupplierItem supplierItem = null;
					String supplierItemCode = null;

					if (ORDERS02E1EDP19List != null
							&& ORDERS02E1EDP19List.size() > 0) {
						for (int j = 0; j < ORDERS02E1EDP19List.size(); j++) {
							ORDERS02E1EDP19 E1EDP19 = ORDERS02E1EDP19List
									.get(j);

							if ("001".equals(E1EDP19.getQUALF())) {
								String itemCode = E1EDP19.getIDTNR();

								item = this.itemManager.getItemByPlantAndItem(
										plant, itemCode);
								if (item == null) {
									log
											.info("Item not found with the given item code: "
													+ itemCode
													+ ", try to create a new one.");

									item = new Item();
									item.setCode(itemCode);
									item.setDescription("");
									item.setPlant(plant);
									item.setUom(E1EDP01.getMENEE());

									item = this.itemManager.save(item);
								}
							} else if ("002".equals(E1EDP19.getQUALF())) {
								supplierItemCode = E1EDP19.getIDTNR();

								supplierItem = this.supplierItemManager
										.getSupplierItemByItemAndSupplier(item,
												supplier);

								if (supplierItem == null) {
									log
											.info("The relationship between Item: "
													+ item.getCode()
													+ " and Supplier: "
													+ supplier.getCode()
													+ " not found, try to create a new one.");

									supplierItem = new SupplierItem();
									supplierItem.setItem(item);
									supplierItem.setSupplier(supplier);
									supplierItem
											.setSupplierItemCode(supplierItemCode);

									supplierItem = this.supplierItemManager
											.save(supplierItem);
								}
							}
						}
					}

					List<ORDERS02E1EDP20> ORDERS02E1EDP20List = E1EDP01
							.getE1EDP20();

					if (ORDERS02E1EDP20List != null
							&& ORDERS02E1EDP20List.size() > 0) {

						for (int k = 0; k < ORDERS02E1EDP20List.size(); k++) {

							ORDERS02E1EDP20 E1EDP20 = ORDERS02E1EDP20List
									.get(k);

							PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail();

							purchaseOrderDetail.setSequence(E1EDP01.getPOSEX()); // 序号
							purchaseOrderDetail.setItem(item);
							purchaseOrderDetail.setPurchaseOrder(po);
							purchaseOrderDetail.setDeliveryDate(dateFormat
									.parse(E1EDP20.getEDATU()));
							purchaseOrderDetail.setQty(new BigDecimal(E1EDP20
									.getWMENG()));
							purchaseOrderDetail.setUom(E1EDP01.getMENEE());

							if (supplierItemCode != null) {
								purchaseOrderDetail
										.setSupplierItemCode(supplierItemCode);
							} else {
								supplierItem = this.supplierItemManager
										.getSupplierItemByItemAndSupplier(item,
												supplier);

								if (supplierItem != null) {
									purchaseOrderDetail
											.setSupplierItemCode(supplierItem
													.getSupplierItemCode());
								}
							}

							po.addPurchaseOrderDetail(purchaseOrderDetail);
						}
					}
				}
			}

		} catch (Exception ex) {
			log.error("Error occur when convert ORDERS02 To PO.", ex);
			throw new DataConvertException(ex, po);
		}

		return po;
	}
}
