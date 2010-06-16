package com.faurecia.service.impl;

import java.io.File;
import java.io.FileInputStream;
import java.io.InputStream;
import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

import org.apache.commons.io.FileUtils;
import org.apache.commons.lang.RandomStringUtils;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.orm.ObjectRetrievalFailureException;

import com.faurecia.Constants;
import com.faurecia.dao.GenericDao;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrderApproval;
import com.faurecia.model.PurchaseOrderApprovalDetail;
import com.faurecia.model.Role;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;
import com.faurecia.model.User;
import com.faurecia.model.order.ORDERS02;
import com.faurecia.model.order.ORDERS02E1EDK03;
import com.faurecia.model.order.ORDERS02E1EDKA1;
import com.faurecia.model.order.ORDERS02E1EDP01;
import com.faurecia.model.order.ORDERS02E1EDP19;
import com.faurecia.model.order.ORDERS02E1EDP20;
import com.faurecia.service.DataConvertException;
import com.faurecia.service.GenericManager;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.ItemManager;
import com.faurecia.service.MailEngine;
import com.faurecia.service.NumberControlManager;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.PurchaseOrderApprovalManager;
import com.faurecia.service.RoleManager;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserManager;

public class PurchaseOrderApprovalManagerImpl extends GenericManagerImpl<PurchaseOrderApproval, String> implements PurchaseOrderApprovalManager {

	public PurchaseOrderApprovalManagerImpl(GenericDao<PurchaseOrderApproval, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.order");
		unmarshaller = jc.createUnmarshaller();
	}

	private Unmarshaller unmarshaller;
	private InboundLogManager inboundLogManager;
	private GenericManager<Plant, String> plantManager;
	private SupplierManager supplierManager;
	private PlantSupplierManager plantSupplierManager;
	private ItemManager itemManager;
	private SupplierItemManager supplierItemManager;
	private UserManager userManager;
	private RoleManager roleManager;
	private NumberControlManager numberControlManager;
	private PlantScheduleGroupManager plantScheduleGroupManager;

	private MailEngine mailEngine;
	private SimpleMailMessage mailMessage;
	private String errorLogTemplateName;
	private String supplierCreatedTemplateName;
	
	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}
	
	public void setPlantScheduleGroupManager(PlantScheduleGroupManager plantScheduleGroupManager) {
		this.plantScheduleGroupManager = plantScheduleGroupManager;
	}

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	public void setSupplierItemManager(SupplierItemManager supplierItemManager) {
		this.supplierItemManager = supplierItemManager;
	}

	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}

	public void setRoleManager(RoleManager roleManager) {
		this.roleManager = roleManager;
	}	

	public void setNumberControlManager(NumberControlManager numberControlManager) {
		this.numberControlManager = numberControlManager;
	}

	public void setMailEngine(MailEngine mailEngine) {
		this.mailEngine = mailEngine;
	}

	public void setMailMessage(SimpleMailMessage mailMessage) {
		this.mailMessage = mailMessage;
	}

	public void setErrorLogTemplateName(String errorLogTemplateName) {
		this.errorLogTemplateName = errorLogTemplateName;
	}

	public void setSupplierCreatedTemplateName(String supplierCreatedTemplateName) {
		this.supplierCreatedTemplateName = supplierCreatedTemplateName;
	}
	
	public PurchaseOrderApproval get(String poNo, boolean includeDetail) {
		PurchaseOrderApproval purchaseOrder = this.genericDao.get(poNo);

		if (includeDetail && purchaseOrder.getPurchaseOrderApprovalDetailList() != null && purchaseOrder.getPurchaseOrderApprovalDetailList().size() > 0) {
			List<PurchaseOrderApprovalDetail> purchaseOrderDetailList = purchaseOrder.getPurchaseOrderApprovalDetailList();
			purchaseOrder.setPurchaseOrderApprovalDetailList(null);

			// 过滤掉需求数量是0的明细
			for (int i = 0; i < purchaseOrderDetailList.size(); i++) {
				if (purchaseOrderDetailList.get(i).getQty().compareTo(new BigDecimal(0)) != 0) {
					purchaseOrder.addPurchaseOrderApprovalDetail(purchaseOrderDetailList.get(i));
				}
			}

		}

		return purchaseOrder;
	}
	
	public void reloadFile(InboundLog inboundLog, String userCode, String archiveFolder) {
		try {
			File file = new File(inboundLog.getFullFilePath());
			FileInputStream stream = new FileInputStream(file);

			saveSingleFile(stream, inboundLog);

			FileUtils.forceMkdir(new File(archiveFolder));
			File backupFile = new File(archiveFolder + File.separator + file.getName());

			FileUtils.copyFile(file, backupFile);
			inboundLog.setFullFilePath(backupFile.getAbsolutePath());
			inboundLog.setInboundResult("success");

			FileUtils.forceDelete(file);
		} catch (Exception ex) {
			inboundLog.setMemo(ex.getMessage());
		} finally {
			inboundLog.setLastModifyDate(new Date());
			inboundLog.setLastModifyUser(userCode);
			this.inboundLogManager.save(inboundLog);
		}
	}

	public PurchaseOrderApproval saveSingleFile(InputStream inputStream, InboundLog inboundLog) {

		try {
			ORDERS02 order = unmarshalOrder(inputStream);
			PurchaseOrderApproval purchaseOrder = ORDERS02ToPO(order);

			if (inboundLog.getPlantSupplier() == null) {
				inboundLog.setPlantSupplier(purchaseOrder.getPlantSupplier());
			}

			// 如果采购单发送错误，同一个定单号，新定单直接覆盖旧定单
			if (this.exists(purchaseOrder.getPoNo())) {
				PurchaseOrderApproval oldPurchaseOrder = this.get(purchaseOrder.getPoNo(), true);

				for (int i = 0; i < purchaseOrder.getPurchaseOrderApprovalDetailList().size(); i++) {
					PurchaseOrderApprovalDetail purchaseOrderDetail = purchaseOrder.getPurchaseOrderApprovalDetailList().get(i);
					boolean findMatch = false;
					
					for (int k = 0; k < oldPurchaseOrder.getPurchaseOrderApprovalDetailList().size(); k++) {
						PurchaseOrderApprovalDetail oldPurchaseOrderDetail = oldPurchaseOrder.getPurchaseOrderApprovalDetailList().get(k);
						if (purchaseOrderDetail.getSequence().equals(oldPurchaseOrderDetail.getSequence())) {
							oldPurchaseOrderDetail.setQty(purchaseOrderDetail.getQty());
							oldPurchaseOrderDetail.setDeliveryDate(purchaseOrderDetail.getDeliveryDate());
							findMatch = true;
							break;
						}
					}
					
					if (!findMatch) {
						oldPurchaseOrder.addPurchaseOrderApprovalDetail(purchaseOrderDetail);
					}
				}

				this.save(oldPurchaseOrder);
			} else {
				// 保存采购单
				this.save(purchaseOrder);
			}

			inboundLog.setInboundResult("success");

			return purchaseOrder;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal ORDERS.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert ORDERS to PO.", dataConvertException);
			inboundLog.setInboundResult("fail");

			PurchaseOrderApproval purchaseOrder = (PurchaseOrderApproval) dataConvertException.getObject();
			if (purchaseOrder != null && purchaseOrder.getPlantSupplier() != null) {
				inboundLog.setPlantSupplier(purchaseOrder.getPlantSupplier());
			}
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

	private PurchaseOrderApproval ORDERS02ToPO(final ORDERS02 order) throws DataConvertException {

		PurchaseOrderApproval po = new PurchaseOrderApproval();

		try {
			Plant plant = null;
			Supplier supplier = null;
			boolean isCreateSupplier = false;
			ORDERS02E1EDKA1 supplierE1EDKA1 = null;
			DateFormat dateFormat = new SimpleDateFormat("yyyyMMdd");

			po.setPoNo(order.getIDOC().getE1EDK01().getBELNR()); // po number

			List<ORDERS02E1EDK03> ORDERS02E1EDK03List = order.getIDOC().getE1EDK03();
			if (ORDERS02E1EDK03List != null && ORDERS02E1EDK03List.size() > 0) {
				for (int i = 0; i < ORDERS02E1EDK03List.size(); i++) {
					ORDERS02E1EDK03 E1EDK03 = ORDERS02E1EDK03List.get(i);

					if ("012".equals(E1EDK03.getIDDAT())) {
						po.setCreateDate(dateFormat.parse(E1EDK03.getDATUM())); // document
						// date
					}
				}
			}

			List<ORDERS02E1EDKA1> ORDERS02E1EDKA1List = order.getIDOC().getE1EDKA1();
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
							// 供应商号如果是全数字，则要把前置0去掉
							supplierCode = Long.toString((Long.parseLong(supplierCode)));
						} catch (NumberFormatException ex) {
						}

						try {
							supplier = this.supplierManager.get(supplierCode); // supplier
						} catch (ObjectRetrievalFailureException ex) {
							log.info("Supplier not found with the given supplier code: " + supplierCode + ", try to create a new one.");

							supplier = new Supplier();
							supplier.setCode(supplierCode);
							supplier.setName(E1EDKA1.getNAME1() != null ? E1EDKA1.getNAME1() : supplierCode);

							supplier = this.supplierManager.save(supplier);

							isCreateSupplier = true;
						}
					}
				}
			}

			if (isCreateSupplier) {
				log.info("Creating supplier user account.");
				// 生成供应商帐号
				User supplierUser = new User();
				supplierUser.setUsername(supplier.getCode()); // 使用供应商编码作为用户名称
				supplierUser.setEnabled(true);
				supplierUser.setAccountExpired(false);
				supplierUser.setAccountLocked(false);
				supplierUser.setEmail(plant.getSupplierNotifyEmail());
				supplierUser.setPassword(RandomStringUtils.random(6, true, true));
				supplierUser.setConfirmPassword(supplierUser.getPassword());
				supplierUser.setFirstName(supplier.getName() != null ? supplier.getName() : supplier.getCode());
				// supplierUser.setLastName(supplier.getName() != null ?
				// supplier.getName() : supplier.getCode());
				supplierUser.setLastName("");
				supplierUser.setUserSupplier(supplier);
				// supplierUser.setUserPlant(plant);
				Set<Role> roles = new HashSet<Role>();
				roles.add(roleManager.getRole(Constants.VENDOR_ROLE));
				supplierUser.setRoles(roles);
				this.userManager.saveUser(supplierUser);

				try {
					// Email通知
					log.info("Send supplier created email to " + plant.getSupplierNotifyEmail());
					mailMessage.setTo(plant.getSupplierNotifyEmail());
					Map<String, Object> model = new HashMap<String, Object>();
					model.put("supplier", supplier);
					model.put("user", supplierUser);
					mailMessage.setSubject("Supplier " + supplier.getCode() + " Created");
					mailEngine.sendMessage(mailMessage, supplierCreatedTemplateName, model);
					log.info("Send supplier created email successful.");
				} catch (MailException mailEx) {
					log.error("Error when send supplier create mail.", mailEx);
				}
			}

			PlantSupplier plantSupplier = this.plantSupplierManager.getPlantSupplier(plant, supplier);

			if (plantSupplier == null) {
				log.info("The relationship between Plant: " + plant.getCode() + " and Supplier: " + supplier.getCode()
						+ " not found, try to create a new one.");

				plantSupplier = new PlantSupplier();
				plantSupplier.setSupplierName(supplierE1EDKA1.getNAME1());
				plantSupplier.setSupplierAddress1(supplierE1EDKA1.getSTRAS());
				plantSupplier.setSupplierAddress2(supplierE1EDKA1.getSTRS2());
				plantSupplier.setSupplierContactPerson(supplierE1EDKA1.getPERNR());
				plantSupplier.setSupplierPhone(supplierE1EDKA1.getTELF1());
				plantSupplier.setSupplierFax(supplierE1EDKA1.getTELFX());
				plantSupplier.setPlant(plant);
				plantSupplier.setSupplier(supplier);
				plantSupplier.setDoNoPrefix(String.valueOf(this.numberControlManager.getNextNumber(Constants.DO_NO_PREFIX)));

				PlantScheduleGroup defaultPlantScheduleGroup = this.plantScheduleGroupManager
						.getDefaultPlantScheduleGroupByPlantCode(plant.getCode());
				if (defaultPlantScheduleGroup != null) {
					plantSupplier.setPlantScheduleGroup(defaultPlantScheduleGroup);
				}

				plantSupplier = this.plantSupplierManager.save(plantSupplier);
			}

			po.setPlantSupplier(plantSupplier);
			/*
			 * po.setSupplierName(plantSupplier.getSupplierName());
			 * po.setSupplierAddress1(plantSupplier.getSupplierAddress1());
			 * po.setSupplierAddress2(plantSupplier.getSupplierAddress2());
			 * po.setSupplierContactPerson(plantSupplier
			 * .getSupplierContactPerson());
			 * po.setSupplierPhone(plantSupplier.getSupplierPhone());
			 * po.setSupplierFax(plantSupplier.getSupplierFax());
			 */

			// ----------------------------po detail---------------------
			List<ORDERS02E1EDP01> ORDERS02E1EDP01List = order.getIDOC().getE1EDP01();

			if (ORDERS02E1EDP01List != null && ORDERS02E1EDP01List.size() > 0) {
				for (int i = 0; i < ORDERS02E1EDP01List.size(); i++) {

					ORDERS02E1EDP01 E1EDP01 = ORDERS02E1EDP01List.get(i);

					List<ORDERS02E1EDP19> ORDERS02E1EDP19List = E1EDP01.getE1EDP19();
					Item item = null;
					String itemDescription = null;
					SupplierItem supplierItem = null;
					String supplierItemCode = null;

					if (ORDERS02E1EDP19List != null && ORDERS02E1EDP19List.size() > 0) {
						for (int j = 0; j < ORDERS02E1EDP19List.size(); j++) {
							ORDERS02E1EDP19 E1EDP19 = ORDERS02E1EDP19List.get(j);

							if ("001".equals(E1EDP19.getQUALF())) {
								String itemCode = E1EDP19.getIDTNR();
								try {
									// 零件号如果是全数字，则要把前置0去掉
									itemCode = Long.toString((Long.parseLong(itemCode)));
								} catch (NumberFormatException ex) {
								}

								item = this.itemManager.getItemByPlantAndItem(plant, itemCode);
								itemDescription = E1EDP19.getKTEXT();

								if (item == null) {
									log.info("Item not found with the given item code: " + itemCode + ", try to create a new one.");

									item = new Item();
									item.setCode(itemCode);
									item.setDescription(E1EDP19.getKTEXT());
									item.setPlant(plant);
									item.setUom(E1EDP01.getMENEE());

									item = this.itemManager.save(item);
								}
							} else if ("002".equals(E1EDP19.getQUALF())) {
								supplierItemCode = E1EDP19.getIDTNR();
							}
						}
					}

					supplierItem = this.supplierItemManager.getSupplierItemByItemAndSupplier(item, supplier);
					if (supplierItem == null) {
						log.info("The relationship between Item: " + item.getCode() + " and Supplier: " + supplier.getCode()
								+ " not found, try to create a new one.");

						supplierItem = new SupplierItem();
						supplierItem.setItem(item);
						supplierItem.setSupplier(supplier);
						supplierItem.setSupplierItemCode(supplierItemCode);

						supplierItem = this.supplierItemManager.save(supplierItem);
					} else if (supplierItemCode != null
							&& (supplierItem.getSupplierItemCode() == null || supplierItem.getSupplierItemCode().trim().length() == 0)) {

						supplierItem.setSupplierItemCode(supplierItemCode);
						supplierItem = this.supplierItemManager.save(supplierItem);
					}

					List<ORDERS02E1EDP20> ORDERS02E1EDP20List = E1EDP01.getE1EDP20();

					if (ORDERS02E1EDP20List != null && ORDERS02E1EDP20List.size() > 0) {

						for (int k = 0; k < ORDERS02E1EDP20List.size(); k++) {

							ORDERS02E1EDP20 E1EDP20 = ORDERS02E1EDP20List.get(k);

							PurchaseOrderApprovalDetail purchaseOrderDetail = new PurchaseOrderApprovalDetail();

							purchaseOrderDetail.setSequence(E1EDP01.getPOSEX()); // 序号
							purchaseOrderDetail.setItem(item);
							purchaseOrderDetail.setItemDescription(itemDescription);
							purchaseOrderDetail.setPurchaseOrderApproval(po);
							purchaseOrderDetail.setDeliveryDate(dateFormat.parse(E1EDP20.getEDATU()));
							purchaseOrderDetail.setQty(new BigDecimal(E1EDP20.getWMENG()));
							purchaseOrderDetail.setUom(E1EDP01.getMENEE());

							if (supplierItemCode != null) {
								purchaseOrderDetail.setSupplierItemCode(supplierItemCode);
							} else {
								supplierItem = this.supplierItemManager.getSupplierItemByItemAndSupplier(item, supplier);

								if (supplierItem != null) {
									purchaseOrderDetail.setSupplierItemCode(supplierItem.getSupplierItemCode());
								}
							}

							po.addPurchaseOrderApprovalDetail(purchaseOrderDetail);
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
