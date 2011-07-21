package com.faurecia.service.impl;

import java.io.File;
import java.io.FileInputStream;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.lang.reflect.InvocationTargetException;
import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.HashSet;
import java.util.List;
import java.util.Map;
import java.util.Set;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;
import javax.xml.bind.Unmarshaller;

import org.apache.commons.beanutils.BeanUtils;
import org.apache.commons.io.FileUtils;
import org.apache.commons.lang.RandomStringUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.MatchMode;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.orm.ObjectRetrievalFailureException;

import com.faurecia.Constants;
import com.faurecia.dao.GenericDao;
import com.faurecia.lisa.kmp58.ManifestFile;
import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.model.Resource;
import com.faurecia.model.Role;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.model.delvry.DELVRY03;
import com.faurecia.model.delvry.DELVRY03E1ADRM1;
import com.faurecia.model.delvry.DELVRY03E1EDL20;
import com.faurecia.model.delvry.DELVRY03E1EDL24;
import com.faurecia.model.delvry.DELVRY03E1EDL41;
import com.faurecia.model.delvry.DELVRY03E1EDT13;
import com.faurecia.model.delvry.DESADVDELVRY03;
import com.faurecia.model.delvry.EDIDC40DESADVDELVRY03;
import com.faurecia.service.DataConvertException;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.ItemManager;
import com.faurecia.service.MailEngine;
import com.faurecia.service.NumberControlManager;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.service.RoleManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserExistsException;
import com.faurecia.service.UserManager;

import freemarker.template.utility.StringUtil;

public class DeliveryOrderManagerImpl extends GenericManagerImpl<DeliveryOrder, String> implements DeliveryOrderManager {

	private NumberControlManager numberControlManager;
	private GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager;
	private PurchaseOrderManager purchaseOrderManager;
	private Marshaller marshaller;
	private Unmarshaller unmarshaller;
	private PlantSupplierManager plantSupplierManager;
	private GenericManager<Plant, String> plantManager;
	private ItemManager itemManager;
	private SupplierManager supplierManager;
	private RoleManager roleManager;
	private UserManager userManager;
	private PlantScheduleGroupManager plantScheduleGroupManager;
	private InboundLogManager inboundLogManager;
	private GenericManager<Resource, Long> resourceManager;
	
	private GenericManager<ScheduleItemDetail, Integer> scheduleItemDetailManager;
	private GenericManager<DeliveryOrderDetail, Integer> deliveryOrderDetailManager;

	protected MailEngine mailEngine;
	protected SimpleMailMessage mailMessage;
	protected String supplierCreatedTemplateName;

	public DeliveryOrderManagerImpl(GenericDao<DeliveryOrder, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.delvry");
		marshaller = jc.createMarshaller();

		JAXBContext jc2 = JAXBContext.newInstance("com.faurecia.lisa.kmp58");
		unmarshaller = jc2.createUnmarshaller();
	}

	public void setNumberControlManager(NumberControlManager numberControlManager) {
		this.numberControlManager = numberControlManager;
	}

	public void setPurchaseOrderDetailManager(GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager) {
		this.purchaseOrderDetailManager = purchaseOrderDetailManager;
	}

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setPurchaseOrderManager(PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	public void setScheduleItemDetailManager(GenericManager<ScheduleItemDetail, Integer> scheduleItemDetailManager) {
		this.scheduleItemDetailManager = scheduleItemDetailManager;
	}

	public void setDeliveryOrderDetailManager(GenericManager<DeliveryOrderDetail, Integer> deliveryOrderDetailManager) {
		this.deliveryOrderDetailManager = deliveryOrderDetailManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public void setRoleManager(RoleManager roleManager) {
		this.roleManager = roleManager;
	}

	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}

	public void setPlantScheduleGroupManager(PlantScheduleGroupManager plantScheduleGroupManager) {
		this.plantScheduleGroupManager = plantScheduleGroupManager;
	}

	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}

	public void setResourceManager(GenericManager<Resource, Long> resourceManager) {
		this.resourceManager = resourceManager;
	}

	public void setMailEngine(MailEngine mailEngine) {
		this.mailEngine = mailEngine;
	}

	public void setMailMessage(SimpleMailMessage mailMessage) {
		this.mailMessage = mailMessage;
	}

	public void setSupplierCreatedTemplateName(String supplierCreatedTemplateName) {
		this.supplierCreatedTemplateName = supplierCreatedTemplateName;
	}

	public DeliveryOrder createDeliveryOrder(List<PurchaseOrderDetail> purchaseOrderDetailList) throws IllegalAccessException,
			InvocationTargetException {

		DeliveryOrder deliveryOrder = null;
		PurchaseOrder purchaseOrder = null;
		for (int i = 0; i < purchaseOrderDetailList.size(); i++) {
			PurchaseOrderDetail purchaseOrderDetail = purchaseOrderDetailList.get(i);

			if (deliveryOrder == null) {
				purchaseOrder = purchaseOrderDetail.getPurchaseOrder();
				deliveryOrder = new DeliveryOrder();
				BeanUtils.copyProperties(deliveryOrder, purchaseOrder);

				deliveryOrder.setDoNo(this.numberControlManager.generateNumber(purchaseOrder.getPlantSupplier().getDoNoPrefix(), 10));
				deliveryOrder.setExternalDoNo(deliveryOrder.getDoNo());
				deliveryOrder.setCreateDate(new Date());
				deliveryOrder.setIsExport(false);
				deliveryOrder.setIsPrint(false);
				deliveryOrder.setIsRead(false);
				deliveryOrder.setStatus("Create");
			}

			DeliveryOrderDetail deliveryOrderDetail = new DeliveryOrderDetail();
			BeanUtils.copyProperties(deliveryOrderDetail, purchaseOrderDetail);

			deliveryOrderDetail.setDeliveryOrder(deliveryOrder);
			deliveryOrderDetail.setUnitCount(purchaseOrderDetail.getItem().getUnitCount());
			deliveryOrderDetail.setQty(purchaseOrderDetail.getCurrentShipQty());
			// deliveryOrderDetail.setOrderQty(purchaseOrderDetail.getQty());
			deliveryOrderDetail.setReferenceOrderNo(purchaseOrderDetail.getPurchaseOrder().getPoNo());
			deliveryOrderDetail.setReferenceSequence(purchaseOrderDetail.getSequence());
			deliveryOrderDetail.setPurchaseOrderDetail(purchaseOrderDetail);
			deliveryOrder.addDeliveryOrderDetail(deliveryOrderDetail);

			// if (purchaseOrderDetail.getShipQty() == null) {
			// purchaseOrderDetail.setShipQty(BigDecimal.ZERO);
			// }
			//
			// purchaseOrderDetail.setShipQty(purchaseOrderDetail.getShipQty().add(purchaseOrderDetail.getCurrentShipQty()));

			this.purchaseOrderDetailManager.save(purchaseOrderDetail);
		}

		// this.purchaseOrderManager.tryClosePurchaseOrder(purchaseOrder.getPoNo());
		this.save(deliveryOrder);

		return deliveryOrder;
	}

	public DeliveryOrder createScheduleDeliveryOrder(DeliveryOrder deliveryOrder) {

		PlantSupplier plantSupplier = this.plantSupplierManager.get(deliveryOrder.getPlantSupplier().getId());
		deliveryOrder.setPlantSupplier(plantSupplier);
		deliveryOrder.setDoNo(this.numberControlManager.generateNumber(plantSupplier.getDoNoPrefix(), 10));
		deliveryOrder.setExternalDoNo(deliveryOrder.getDoNo());

		List<DeliveryOrderDetail> deliveryOrderDetailList = deliveryOrder.getDeliveryOrderDetailList();

		for (int i = 0; i < deliveryOrderDetailList.size(); i++) {

			DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);
			Item item = this.itemManager.get(deliveryOrderDetail.getItem().getId());
			deliveryOrderDetail.setDeliveryOrder(deliveryOrder);
			deliveryOrderDetail.setItem(item);
			deliveryOrderDetail.setPurchaseOrderDetail(null);
			// deliveryOrderDetail.setQty(deliveryOrderDetail.getCurrentQty());

			// ScheduleItemDetail scheduleItemDetail =
			// deliveryOrderDetail.getScheduleItemDetail();

			// BigDecimal deliverQty = scheduleItemDetail.getDeliverQty();
			// if (deliverQty == null) {
			// deliverQty = deliveryOrderDetail.getCurrentQty();
			// } else {
			// deliverQty = deliverQty.add(deliveryOrderDetail.getCurrentQty());
			// }
			// scheduleItemDetail.setDeliverQty(deliverQty);

			// this.scheduleItemDetailManager.save(scheduleItemDetail);
		}
		deliveryOrder.setStatus("Create");
		this.save(deliveryOrder);

		return this.get(deliveryOrder.getDoNo(), true);
	}

	public DeliveryOrder confirm(DeliveryOrder deliveryOrder) {
		deliveryOrder.setStatus("Confirm");

		List<DeliveryOrderDetail> deliveryOrderDetailList = new ArrayList<DeliveryOrderDetail>();
		for (int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
			DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);
			if (deliveryOrderDetail.getQty() != null && deliveryOrderDetail.getQty().compareTo(BigDecimal.ZERO) > 0) {
				deliveryOrderDetailList.add(deliveryOrderDetail);

				if (deliveryOrderDetail.getPurchaseOrderDetail() != null) {
					PurchaseOrderDetail purchaseOrderDetail = deliveryOrderDetail.getPurchaseOrderDetail();
					if (purchaseOrderDetail.getShipQty() == null) {
						purchaseOrderDetail.setShipQty(deliveryOrderDetail.getQty());
					} else {
						BigDecimal deliverQty = purchaseOrderDetail.getShipQty().add(deliveryOrderDetail.getQty());
						purchaseOrderDetail.setShipQty(deliverQty);
					}

					purchaseOrderDetail = this.purchaseOrderDetailManager.save(purchaseOrderDetail);

					if (purchaseOrderDetail.getQty().compareTo(purchaseOrderDetail.getShipQty()) == 0) {
						this.purchaseOrderManager.tryClosePurchaseOrder(purchaseOrderDetail.getPurchaseOrder().getPoNo(), purchaseOrderDetail);
					}

				} else if (deliveryOrderDetail.getScheduleItemDetail() != null) {
					ScheduleItemDetail scheduleItemDetail = deliveryOrderDetail.getScheduleItemDetail();
					if (scheduleItemDetail.getDeliverQty() == null) {
						scheduleItemDetail.setDeliverQty(deliveryOrderDetail.getQty());
					} else {
						BigDecimal deliverQty = scheduleItemDetail.getDeliverQty().add(deliveryOrderDetail.getQty());
						scheduleItemDetail.setDeliverQty(deliverQty);
					}

					this.scheduleItemDetailManager.save(scheduleItemDetail);
				}
			} else {
				this.deliveryOrderDetailManager.remove(deliveryOrderDetail.getId());
			}
		}

		deliveryOrder.setDeliveryOrderDetailList(deliveryOrderDetailList);
		this.genericDao.save(deliveryOrder);

		return deliveryOrder;
	}

	@SuppressWarnings("unchecked")
	public List<DeliveryOrder> getUnexportDeliveryOrderByPlant(Plant plant) {
		DetachedCriteria criteria = DetachedCriteria.forClass(DeliveryOrder.class);
		criteria.createAlias("plantSupplier", "ps");

		criteria.add(Restrictions.eq("ps.plant", plant));
		criteria.add(Restrictions.eq("isPrint", true));
		criteria.add(Restrictions.eq("isExport", false));
		criteria.add(Restrictions.eq("status", "Confirm"));
		criteria.addOrder(Order.asc("createDate"));

		List<DeliveryOrder> deliveryOrderList = this.findByCriteria(criteria);

		if (deliveryOrderList != null && deliveryOrderList.size() > 0) {
			for (int i = 0; i < deliveryOrderList.size(); i++) {
				DeliveryOrder deliveryOrder = deliveryOrderList.get(i);

				if (deliveryOrder.getDeliveryOrderDetailList() != null && deliveryOrder.getDeliveryOrderDetailList().size() > 0) {

				}
			}
		}

		return deliveryOrderList;
	}

	public DeliveryOrder get(String doNo, boolean includeDetail) {
		DeliveryOrder deliveryOrder = this.genericDao.get(doNo);
		if (includeDetail && deliveryOrder.getDeliveryOrderDetailList() != null && deliveryOrder.getDeliveryOrderDetailList().size() > 0) {
		}
		return deliveryOrder;
	}

	public File exportDeliveryOrder(DeliveryOrder deliveryOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException,
			IOException {
		DELVRY03 DELVRY = new DELVRY03();

		DESADVDELVRY03 DESADVDELVRY = new DESADVDELVRY03();
		DESADVDELVRY.setBEGIN("1");

		EDIDC40DESADVDELVRY03 EDIDC40 = new EDIDC40DESADVDELVRY03();
		EDIDC40.setSEGMENT("1");
		EDIDC40.setTABNAM("EDI_DC40");
		EDIDC40.setDIRECT("1");
		EDIDC40.setIDOCTYP("DELVRY03");
		EDIDC40.setMESTYP("DESADV");
		EDIDC40.setSNDPOR("aaaaaaaaaa");
		EDIDC40.setSNDPRT("aa");
		EDIDC40.setSNDPRN("aaaaaaaaaa");
		EDIDC40.setRCVPOR("aaaaaaaaaa");
		EDIDC40.setRCVPRN("aaaaaaaaaa");

		DESADVDELVRY.setEDIDC40(EDIDC40);

		List<DELVRY03E1EDL20> E1EDL20List = DESADVDELVRY.getE1EDL20();
		AddE1EDL20List(E1EDL20List, deliveryOrder);

		DELVRY.setIDOC(DESADVDELVRY);

		return marshalOrder(DELVRY, filePath, filePrefix, fileSuffix);
	}

	public List<DeliveryOrder> saveMultiFile(InputStream inputStream, InboundLog inboundLog) {
		try {
			ManifestFile order = unmarshalOrder(inputStream);
			List<DeliveryOrder> doList = ManifestFileToDo(order);

			if (inboundLog.getPlantSupplier() == null) {
				inboundLog.setPlantSupplier(doList.get(0).getPlantSupplier());
			}

			inboundLog.setInboundResult("success");

			return doList;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal ORDERS.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert ORDERS to PO.", dataConvertException);
			inboundLog.setInboundResult("fail");

			DeliveryOrder deliveryOrder = (DeliveryOrder) dataConvertException.getObject();
			if (deliveryOrder != null && deliveryOrder.getPlantSupplier() != null) {
				inboundLog.setPlantSupplier(deliveryOrder.getPlantSupplier());
			}
			inboundLog.setMemo(dataConvertException.getMessage());
		} catch (Exception exception) {
			log.error("Error occur.", exception);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(exception.getMessage());
		}

		return null;
	}

	public void reloadFile(InboundLog inboundLog, String userCode, String archiveFolder) {
		try {
			File file = new File(inboundLog.getFullFilePath());
			FileInputStream stream = new FileInputStream(file);

			this.saveMultiFile(stream, inboundLog);

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

	@SuppressWarnings("unchecked")
	private List<DeliveryOrder> ManifestFileToDo(final ManifestFile order) throws DataConvertException, UserExistsException {
		List<DeliveryOrder> deliveryOrderList = new ArrayList<DeliveryOrder>();

		if (order != null && order.getFileHeaderOrDeliveryOrFileEnd() != null && order.getFileHeaderOrDeliveryOrFileEnd().size() > 0) {

			ManifestFile.FileHeader fileHeader = (ManifestFile.FileHeader) order.getFileHeaderOrDeliveryOrFileEnd().get(0);
			String plantCode = fileHeader.getPCODE();
			if (plantCode.trim().length() > 3) {
				plantCode = plantCode.substring(plantCode.length() - 3);
			}
			String fileId = fileHeader.getFILID();
			DetachedCriteria criteria0 = DetachedCriteria.forClass(Plant.class);
			criteria0.add(Restrictions.like("code", plantCode, MatchMode.END));
			List<Plant> plantList = plantManager.findByCriteria(criteria0);
			Plant plant = null;
			if (plantList != null && plantList.size() > 0) {
				plant = plantList.get(0);
			} else {
				log.error("Error find plant with code " + fileHeader.getPCODE());
				return null;
			}

			for (Object obj : order.getFileHeaderOrDeliveryOrFileEnd()) {
				if (obj instanceof ManifestFile.Delivery) {
					ManifestFile.Delivery delivery = (ManifestFile.Delivery) obj;
					DeliveryOrder deliveryOrder = parseDelivery(delivery, plant, fileId);

					DetachedCriteria criteria = DetachedCriteria.forClass(DeliveryOrder.class);
					criteria.add(Restrictions.eq("murn", deliveryOrder.getMurn()));
					//criteria.add(Restrictions.eq("fileIdentitfier", deliveryOrder.getFileIdentitfier()));

					List<DeliveryOrder> doList = this.findByCriteria(criteria);
					if (doList != null && doList.size() > 0) {
						String doNo = doList.get(0).getDoNo();
						this.remove(doNo);

						deliveryOrder.setDoNo(doNo);
					}

					this.save(deliveryOrder);
					deliveryOrderList.add(deliveryOrder);

					this.flushSession();
				}
			}
		}

		return deliveryOrderList;
	}

	private DeliveryOrder parseDelivery(ManifestFile.Delivery delivery, Plant plant, String fileId) throws DataConvertException {
		DateFormat dtFormat = new SimpleDateFormat("yyyyMMddHHmmss");

		ManifestFile.Delivery.Recheader header = delivery.getRecheader().get(0);
		DeliveryOrder deliveryOrder = new DeliveryOrder();
		deliveryOrder.setFileIdentitfier(fileId);

		try {
			String supplierCode = header.getSUCODE();
			Supplier supplier = null;

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
				supplier.setName(header.getSUNAME());

				supplier = this.supplierManager.save(supplier);

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
				plantSupplier.setSupplierName(header.getSUNAME());
				plantSupplier.setSupplierAddress1(header.getSUPADDR1());
				plantSupplier.setSupplierAddress2(header.getSUPADDR2());
				plantSupplier.setSupplierContactPerson(header.getSUCONTACT());
				plantSupplier.setSupplierPhone(header.getSUPTEL());
				plantSupplier.setSupplierFax(header.getSUFAX());
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

			deliveryOrder.setPlantSupplier(plantSupplier);
			deliveryOrder.setDoNo(this.numberControlManager.generateNumber(plantSupplier.getDoNoPrefix(), 10));
			deliveryOrder.setExternalDoNo(header.getMANCODE());
			deliveryOrder.setPlantName(header.getFAUPLANT());
			deliveryOrder.setPlantAddress1(header.getFAUADDR1());
			deliveryOrder.setPlantAddress2(header.getFAUADDR2());
			deliveryOrder.setPlantAddress3(header.getFAUADDR3());
			deliveryOrder.setPlantContactPerson(header.getFAUCONTACT());
			deliveryOrder.setPlantPhone(header.getFAUTEL());
			deliveryOrder.setPlantFax(header.getFAUFAX());
			deliveryOrder.setPlantPostCode(header.getPOSTCODE());
			deliveryOrder.setPlantCity(header.getCITY());
			deliveryOrder.setPlantCountry(header.getFAUCTRY());
			deliveryOrder.setSupplierName(header.getSUNAME());
			deliveryOrder.setSupplierAddress1(header.getSUPADDR1());
			deliveryOrder.setSupplierAddress2(header.getSUPADDR2());
			deliveryOrder.setSupplierAddress3(header.getSUPADDR3());
			deliveryOrder.setSupplierContactPerson(header.getSUCONTACT());
			deliveryOrder.setSupplierPhone(header.getSUPTEL());
			deliveryOrder.setSupplierFax(header.getSUFAX());
			deliveryOrder.setSupplierPostCode(header.getSUPPCOD());
			deliveryOrder.setSupplierCity(header.getSUPCITY());
			deliveryOrder.setSupplierCountry(header.getSUPCTRY());
			deliveryOrder.setCreateDate(new Date());
			try {
				deliveryOrder.setStartDate(dtFormat.parse(header.getPICKUP()));
			} catch (Exception ex) {
				log.warn("Error when convert PICKUP into datetime.", ex);
				deliveryOrder.setStartDate(null);
			}
			try {
				deliveryOrder.setEndDate(dtFormat.parse(header.getRECEPT()));
			} catch (Exception ex) {
				log.warn("Error when convert RECEPT into datetime.", ex);
				deliveryOrder.setEndDate(null);
			}
			deliveryOrder.setIsExport(false);
			deliveryOrder.setIsPrint(false);
			deliveryOrder.setIsRead(false);
			deliveryOrder.setFirstReadDate(null);
			deliveryOrder.setStatus("Confirm");
			deliveryOrder.setMurn(header.getMURN());
			deliveryOrder.setOrderGroup(header.getORDERG());
			deliveryOrder.setDeliveryOrderGroup(header.getDELORDGR());
			deliveryOrder.setDock(header.getFDCODE());
			deliveryOrder.setRoute(header.getROUTE());
			deliveryOrder.setMainRoute(header.getMROUTE());
			try {
				deliveryOrder.setTotalWeight(new BigDecimal(header.getTOTWEIGHT()));
			} catch (Exception ex) {
				log.warn("Error when convert TOTWEIGHT into decimal.", ex);
			}
			deliveryOrder.setUnitWeight(header.getUNITWEIGHT());
			try {
				deliveryOrder.setTotalVolume(new BigDecimal(header.getTOTVOL()));
			} catch (Exception ex) {
				log.warn("Error when convert TOTVOL into decimal.", ex);
			}
			deliveryOrder.setUnitVolume(header.getUNITVOL());
			try {
				deliveryOrder.setTotalNbPallets(new BigDecimal(header.getTOTPAL()));
			} catch (Exception ex) {
				log.warn("Error when convert TOTPAL into decimal.", ex);
			}
			deliveryOrder.setTitle(header.getMANTITLE());

			int i = 0;
			for (ManifestFile.Delivery.Recpos recpos : delivery.getRecpos()) {
				i++;

				String itemCode = recpos.getPNUMBER();
				try {
					// 零件号如果是全数字，则要把前置0去掉
					itemCode = Long.toString((Long.parseLong(itemCode)));
				} catch (NumberFormatException ex) {
				}

				Item item = this.itemManager.getItemByPlantAndItem(plant, itemCode);
				String itemDescription = recpos.getDESC();

				if (item == null) {
					log.info("Item not found with the given item code: " + itemCode + ", try to create a new one.");

					item = new Item();
					item.setCode(itemCode);
					item.setDescription(itemDescription);
					item.setPlant(plant);
					item.setUom("");

					item = this.itemManager.save(item);
				}

				DeliveryOrderDetail detail = new DeliveryOrderDetail();
				detail.setDeliveryOrder(deliveryOrder);
				detail.setItem(item);
				detail.setItemDescription(itemDescription);
				try {
					detail.setLabel(Integer.parseInt(recpos.getLABELID()));
				} catch (Exception ex) {
					log.warn("Error when convert LABELID into int.", ex);
				}
				try {
					detail.setOrderLot(new BigDecimal(recpos.getOLOT()));
				} catch (Exception ex) {
					log.warn("Error when convert OLOT into decimal.", ex);
				}
				detail.setSequence(String.valueOf(i));
				detail.setReferenceSequence(StringUtil.leftPad(String.valueOf(i), 4, '0'));
				try {
					detail.setUnitCount(new BigDecimal(recpos.getPCSPU()));
				} catch (Exception ex) {
					log.warn("Error when convert PCSPU into decimal.", ex);
				}
				try {
					detail.setIndice(Integer.parseInt(recpos.getPNUMIND()));
				} catch (Exception ex) {
					log.warn("Error when convert PNUMIND into int.", ex);
				}
				try {
					detail.setBoxCount(new BigDecimal(recpos.getNBPU()));
				} catch (Exception ex) {
					log.warn("Error when convert NBPU into decimal.", ex);
				}
				if (detail.getUnitCount() != null && detail.getBoxCount() != null) {
					detail.setOrderedQty(detail.getUnitCount().multiply(detail.getBoxCount()));
					detail.setQty(detail.getOrderedQty());
				}
				detail.setUom(item.getUom());
				detail.setPackageType(recpos.getPACKTYPE());
				detail.setSebango(recpos.getSEBANGO());
				detail.setStorageCode(recpos.getSCODE());
				deliveryOrder.addDeliveryOrderDetail(detail);
			}

			return deliveryOrder;
		} catch (Exception e) {
			throw new DataConvertException(e, deliveryOrder);
		}
	}

	private ManifestFile unmarshalOrder(InputStream stream) throws JAXBException {
		ManifestFile o = (ManifestFile) unmarshaller.unmarshal(stream);
		return o;
	}

	private void AddE1EDL20List(List<DELVRY03E1EDL20> E1EDL20List, DeliveryOrder deliveryOrder) {
		DELVRY03E1EDL20 E1EDL20 = new DELVRY03E1EDL20();
		E1EDL20.setSEGMENT("1");
		E1EDL20.setVBELN(deliveryOrder.getExternalDoNo());

		List<DELVRY03E1ADRM1> E1ADRM1List = E1EDL20.getE1ADRM1();
		addE1ADRM1List(E1ADRM1List, deliveryOrder);

		List<DELVRY03E1EDT13> E1EDT13List = E1EDL20.getE1EDT13();
		addE1EDT13List(E1EDT13List, deliveryOrder);

		List<DELVRY03E1EDL24> E1EDL24List = E1EDL20.getE1EDL24();
		addE1EDL24List(E1EDL24List, deliveryOrder.getDeliveryOrderDetailList());

		E1EDL20List.add(E1EDL20);
	}

	private void addE1ADRM1List(List<DELVRY03E1ADRM1> E1ADRM1List, DeliveryOrder deliveryOrder) {
		DELVRY03E1ADRM1 AG = new DELVRY03E1ADRM1();
		AG.setSEGMENT("1");
		AG.setPARTNERQ("AG");
		AG.setPARTNERID(deliveryOrder.getPlantSupplier().getSupplier().getCode());
		E1ADRM1List.add(AG);

		DELVRY03E1ADRM1 LF = new DELVRY03E1ADRM1();
		LF.setSEGMENT("1");
		LF.setPARTNERQ("LF");
		LF.setPARTNERID(deliveryOrder.getPlantSupplier().getSupplier().getCode());
		E1ADRM1List.add(LF);

		DELVRY03E1ADRM1 WE = new DELVRY03E1ADRM1();
		WE.setSEGMENT("1");
		WE.setPARTNERQ("WE");
		WE.setPARTNERID(deliveryOrder.getPlantSupplier().getPlant().getCode());
		E1ADRM1List.add(WE);
	}

	private void addE1EDT13List(List<DELVRY03E1EDT13> E1EDT13List, DeliveryOrder deliveryOrder) {
		DateFormat format = new SimpleDateFormat("yyyyMMdd");

		DELVRY03E1EDT13 E1EDT13 = new DELVRY03E1EDT13();
		E1EDT13.setSEGMENT("1");
		E1EDT13.setQUALF("007");
		E1EDT13.setNTANF(format.format(new Date()));
		E1EDT13.setNTEND(format.format(new Date()));
		// E1EDT13.setNTANF(format.format(deliveryOrder.getStartDate()));
		// E1EDT13.setNTEND(format.format(deliveryOrder.getEndDate()));

		E1EDT13List.add(E1EDT13);
	}

	private void addE1EDL24List(List<DELVRY03E1EDL24> E1EDL24List, List<DeliveryOrderDetail> deliveryOrderDetailList) {
		for (int i = 0; i < deliveryOrderDetailList.size(); i++) {
			DeliveryOrderDetail deliveryOrderDetail = deliveryOrderDetailList.get(i);
			E1EDL24List.add(prepareE1EDL24(i + 1, deliveryOrderDetail));
		}
	}

	private DELVRY03E1EDL24 prepareE1EDL24(int position, DeliveryOrderDetail deliveryOrderDetail) {
		DELVRY03E1EDL24 E1EDL24 = new DELVRY03E1EDL24();

		E1EDL24.setSEGMENT("1");
		E1EDL24.setPOSNR(StringUtil.leftPad(String.valueOf(position * 10), 4, '0'));
		E1EDL24.setKDMAT(deliveryOrderDetail.getItem().getCode());
		E1EDL24.setLFIMG(String.valueOf(deliveryOrderDetail.getQty()));
		E1EDL24.setVRKME(deliveryOrderDetail.getUom());

		List<DELVRY03E1EDL41> E1EDL41List = E1EDL24.getE1EDL41();
		addE1EDL41List(E1EDL41List, deliveryOrderDetail);

		return E1EDL24;
	}

	private void addE1EDL41List(List<DELVRY03E1EDL41> E1EDL41List, DeliveryOrderDetail deliveryOrderDetail) {
		DELVRY03E1EDL41 E1EDL41 = new DELVRY03E1EDL41();

		E1EDL41.setSEGMENT("1");
		E1EDL41.setQUALI("001");
		E1EDL41.setBSTNR(deliveryOrderDetail.getReferenceOrderNo());
		E1EDL41.setPOSEX(deliveryOrderDetail.getReferenceSequence());

		E1EDL41List.add(E1EDL41);
	}

	private File marshalOrder(DELVRY03 DELVRY, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException {
		File tempFile = File.createTempFile(filePrefix, fileSuffix, filePath);
		FileOutputStream output = new FileOutputStream(tempFile);
		marshaller.marshal(DELVRY, output);
		output.flush();
		output.close();
		return tempFile;
	}
}
