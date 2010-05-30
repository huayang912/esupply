package com.faurecia.service.impl;

import java.io.FileInputStream;
import java.io.FileNotFoundException;
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

import org.apache.commons.lang.RandomStringUtils;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.orm.ObjectRetrievalFailureException;

import com.faurecia.Constants;
import com.faurecia.dao.GenericDao;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;
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
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.service.RoleManager;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserManager;

public class PurchaseOrderManagerImpl extends GenericManagerImpl<PurchaseOrder, String> implements PurchaseOrderManager {
	private GenericManager<Plant, String> plantManager;
	private SupplierManager supplierManager;
	private PlantSupplierManager plantSupplierManager;
	private ItemManager itemManager;
	private SupplierItemManager supplierItemManager;
	private InboundLogManager inboundLogManager;
	private UserManager userManager;
	private RoleManager roleManager;
	private NumberControlManager numberControlManager;
	private Unmarshaller unmarshaller;

	protected MailEngine mailEngine;
	protected SimpleMailMessage mailMessage;
	protected String errorLogTemplateName;
	protected String supplierCreatedTemplateName;

	public PurchaseOrderManagerImpl(GenericDao<PurchaseOrder, String> genericDao) throws JAXBException {
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

	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
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

	public PurchaseOrder get(String poNo, boolean includeDetail) {
		PurchaseOrder purchaseOrder = this.genericDao.get(poNo);

		if (includeDetail && purchaseOrder.getPurchaseOrderDetailList() != null && purchaseOrder.getPurchaseOrderDetailList().size() > 0) {
			List<PurchaseOrderDetail> purchaseOrderDetailList = purchaseOrder.getPurchaseOrderDetailList();
			purchaseOrder.setPurchaseOrderDetailList(null);
			
			//过滤掉需求数量是0的明细
			for(int i = 0; i < purchaseOrderDetailList.size(); i++) {
				if (purchaseOrderDetailList.get(i).getQty().compareTo(new BigDecimal(0)) != 0) {
					purchaseOrder.addPurchaseOrderDetail(purchaseOrderDetailList.get(i));
				}
			}
				
		}

		return purchaseOrder;
	}

	public void ReloadFile(InboundLog inboundLog, String userCode) {

		try {
			FileInputStream stream = new FileInputStream(inboundLog.getFullFilePath());
			SaveSingleFile(stream, inboundLog);
		} catch (FileNotFoundException fileNotFoundException) {
			inboundLog.setMemo(fileNotFoundException.getMessage());
		} finally {
			inboundLog.setLastModifyDate(new Date());
			inboundLog.setLastModifyUser(userCode);
			this.inboundLogManager.save(inboundLog);
		}
	}

	public PurchaseOrder SaveSingleFile(InputStream inputStream, InboundLog inboundLog) {

		try {
			ORDERS02 order = unmarshalOrder(inputStream);
			PurchaseOrder purchaseOrder = ORDERS02ToPO(order);

			if (inboundLog.getPlantSupplier() == null) {
				inboundLog.setPlantSupplier(purchaseOrder.getPlantSupplier());
			}

			// 如果采购单发送错误，同一个定单号，新定单直接覆盖旧定单
			if (this.exists(purchaseOrder.getPoNo())) {
				this.remove(purchaseOrder.getPoNo());
			}

			// 保存采购单
			this.save(purchaseOrder);		

			inboundLog.setInboundResult("success");

			return purchaseOrder;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal ORDERS.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert ORDERS to PO.", dataConvertException);
			inboundLog.setInboundResult("fail");

			PurchaseOrder purchaseOrder = (PurchaseOrder) dataConvertException.getObject();
			if (purchaseOrder != null && purchaseOrder.getPlantSupplier() != null)
			{
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

	private PurchaseOrder ORDERS02ToPO(final ORDERS02 order) throws DataConvertException {

		PurchaseOrder po = new PurchaseOrder();
		po.setStatus("Open");

		try {
			Plant plant = null;
			Supplier supplier = null;
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
							supplier = this.supplierManager.get(supplierCode); // supplier
						} catch (ObjectRetrievalFailureException ex) {
							log.info("Supplier not found with the given supplier code: " + supplierCode + ", try to create a new one.");

							supplier = new Supplier();
							supplier.setCode(supplierCode);
							supplier.setName(E1EDKA1.getNAME1() != null ? E1EDKA1.getNAME1() : supplierCode);

							supplier = this.supplierManager.save(supplier);
						}
					}
				}
			}

			/*
			 * po.setPlant(plant); po.setPlantName(plant.getName());
			 * po.setPlantAddress1(plant.getAddress1());
			 * po.setPlantAddress2(plant.getAddress2());
			 * po.setPlantContactPerson(plant.getContactPerson());
			 * po.setPlantPhone(plant.getPhone());
			 * po.setPlantFax(plant.getFax());
			 */

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

				plantSupplier = this.plantSupplierManager.save(plantSupplier);

				// 生成供应商帐号
				User supplierUser = new User();
				supplierUser.setUsername(String.valueOf(plantSupplier.getId() + 10000)); // 使用plantSupplier.id
				// +
				// 100000作为供应商用户的名称
				supplierUser.setEnabled(true);
				supplierUser.setAccountExpired(false);
				supplierUser.setAccountLocked(false);
				supplierUser.setEmail(plant.getSupplierNotifyEmail());
				supplierUser.setPassword(RandomStringUtils.random(6, true, true));
				supplierUser.setConfirmPassword(supplierUser.getPassword());
				supplierUser.setFirstName(supplier.getName() != null ? supplier.getName() : supplier.getCode());
				supplierUser.setLastName(supplier.getName() != null ? supplier.getName() : supplier.getCode());
				supplierUser.setUserSupplier(supplier);
				supplierUser.setUserPlant(plant);
				Set<Role> roles = new HashSet<Role>();
				roles.add(roleManager.getRole(Constants.VENDOR_ROLE));
				supplierUser.setRoles(roles);
				this.userManager.saveUser(supplierUser);

				try {
					// Email通知
					log.info("Send supplier created email to " + plant.getSupplierNotifyEmail());
					mailMessage.setTo(plant.getSupplierNotifyEmail());
					Map<String, Object> model = new HashMap<String, Object>();
					model.put("plantSupplier", plantSupplier);
					model.put("user", supplierUser);
					mailMessage.setSubject("Supplier " + plantSupplier.getSupplier().getCode() + " Created");
					mailEngine.sendMessage(mailMessage, supplierCreatedTemplateName, model);
					log.info("Send supplier created email successful.");
				} catch (MailException ex) {
					log.error("Error when send supplier create mail.", ex);
				}
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

								supplierItem = this.supplierItemManager.getSupplierItemByItemAndSupplier(item, supplier);

								if (supplierItem == null) {
									log.info("The relationship between Item: " + item.getCode() + " and Supplier: " + supplier.getCode()
											+ " not found, try to create a new one.");

									supplierItem = new SupplierItem();
									supplierItem.setItem(item);
									supplierItem.setSupplier(supplier);
									supplierItem.setSupplierItemCode(supplierItemCode);

									supplierItem = this.supplierItemManager.save(supplierItem);
								}
							}
						}
					}

					List<ORDERS02E1EDP20> ORDERS02E1EDP20List = E1EDP01.getE1EDP20();

					if (ORDERS02E1EDP20List != null && ORDERS02E1EDP20List.size() > 0) {

						for (int k = 0; k < ORDERS02E1EDP20List.size(); k++) {

							ORDERS02E1EDP20 E1EDP20 = ORDERS02E1EDP20List.get(k);

							PurchaseOrderDetail purchaseOrderDetail = new PurchaseOrderDetail();

							purchaseOrderDetail.setSequence(E1EDP01.getPOSEX()); // 序号
							purchaseOrderDetail.setItem(item);
							purchaseOrderDetail.setItemDescription(itemDescription);
							purchaseOrderDetail.setPurchaseOrder(po);
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
