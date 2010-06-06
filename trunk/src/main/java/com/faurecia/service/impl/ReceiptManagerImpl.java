package com.faurecia.service.impl;

import java.io.InputStream;
import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
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
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Receipt;
import com.faurecia.model.ReceiptDetail;
import com.faurecia.model.Role;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.model.mbgmcr.MBGMCR02;
import com.faurecia.model.mbgmcr.MBGMCR02E1BP2017GMITEMCREATE;
import com.faurecia.service.DataConvertException;
import com.faurecia.service.GenericManager;
import com.faurecia.service.ItemManager;
import com.faurecia.service.MailEngine;
import com.faurecia.service.NumberControlManager;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.service.RoleManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserManager;

public class ReceiptManagerImpl extends GenericManagerImpl<Receipt, String> implements ReceiptManager {

	private GenericManager<Plant, String> plantManager;
	private SupplierManager supplierManager;
	private PlantSupplierManager plantSupplierManager;
	private ItemManager itemManager;
	private UserManager userManager;
	private RoleManager roleManager;
	private NumberControlManager numberControlManager;
	private PlantScheduleGroupManager plantScheduleGroupManager;
	private Unmarshaller unmarshaller;

	protected MailEngine mailEngine;
	protected SimpleMailMessage mailMessage;
	protected String errorLogTemplateName;
	protected String supplierCreatedTemplateName;

	public ReceiptManagerImpl(GenericDao<Receipt, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.mbgmcr");
		unmarshaller = jc.createUnmarshaller();
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

	public Receipt get(String receiptNo, boolean includeDetail) {
		Receipt receipt = this.genericDao.get(receiptNo);

		if (includeDetail && receipt.getReceiptDetailList() != null && receipt.getReceiptDetailList().size() > 0) {

		}

		return receipt;
	}

	public Receipt saveSingleFile(InputStream inputStream, InboundLog inboundLog) {
		try {
			MBGMCR02 mbgmcr = unmarshalOrder(inputStream);
			Receipt receipt = MBGMCR02ToReceipt(mbgmcr);

			if (inboundLog.getPlantSupplier() == null) {
				inboundLog.setPlantSupplier(receipt.getPlantSupplier());
			}

			// 同一个定单号，新定单直接覆盖旧定单
			if (this.exists(receipt.getReceiptNo())) {
				this.remove(receipt.getReceiptNo());
			}

			// 保存采购单
			this.save(receipt);

			inboundLog.setInboundResult("success");

			return receipt;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal MBGMCR.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert MBGMCR to PO.", dataConvertException);
			inboundLog.setInboundResult("fail");

			Receipt receipt = (Receipt) dataConvertException.getObject();
			if (receipt != null && receipt.getPlantSupplier() != null) {
				inboundLog.setPlantSupplier(receipt.getPlantSupplier());
			}
			inboundLog.setMemo(dataConvertException.getMessage());
		} catch (Exception exception) {
			log.error("Error occur.", exception);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(exception.getMessage());
		}

		return null;
	}

	private MBGMCR02 unmarshalOrder(InputStream stream) throws JAXBException {
		MBGMCR02 o = (MBGMCR02) unmarshaller.unmarshal(stream);
		return o;
	}

	private Receipt MBGMCR02ToReceipt(final MBGMCR02 mbgmcr) throws DataConvertException {
		Receipt receipt = new Receipt();

		try {
			Plant plant = null;
			Supplier supplier = null;
			DateFormat dateFormat = new SimpleDateFormat("yyyyMMdd");

			receipt.setReceiptNo(mbgmcr.getIDOC().getE1BP2017GMHEAD01().getREFDOCNOLONG()); // ReceiptNo
			receipt.setPostingDate(dateFormat.parse(mbgmcr.getIDOC().getE1BP2017GMHEAD01().getPSTNGDATE()));

			List<MBGMCR02E1BP2017GMITEMCREATE> E1BP2017GMITEMCREATEList = mbgmcr.getIDOC().getE1BP2017GMITEMCREATE();

			for (int i = 0; i < E1BP2017GMITEMCREATEList.size(); i++) {
				MBGMCR02E1BP2017GMITEMCREATE E1BP2017GMITEMCREATE = E1BP2017GMITEMCREATEList.get(i);
				ReceiptDetail receiptDetail = new ReceiptDetail();
				receiptDetail.setReceipt(receipt);

				if (i == 0) {
					String plantCode = E1BP2017GMITEMCREATE.getPLANT();
					plant = this.plantManager.get(plantCode); // plant

					String supplierCode = E1BP2017GMITEMCREATE.getVENDOR();

					try {
						supplier = this.supplierManager.get(supplierCode); // supplier
					} catch (ObjectRetrievalFailureException ex) {
						log.info("Supplier not found with the given supplier code: " + supplierCode + ", try to create a new one.");

						supplier = new Supplier();
						supplier.setCode(supplierCode);
						supplier.setName(supplierCode);

						supplier = this.supplierManager.save(supplier);
						
						log.info("Creating supplier user account.");
						// 生成供应商帐号
						User supplierUser = new User();
						supplierUser.setUsername(supplierCode); // 使用供应商编码作为用户名称
						supplierUser.setEnabled(true);
						supplierUser.setAccountExpired(false);
						supplierUser.setAccountLocked(false);
						supplierUser.setEmail(plant.getSupplierNotifyEmail());
						supplierUser.setPassword(RandomStringUtils.random(6, true, true));
						supplierUser.setConfirmPassword(supplierUser.getPassword());
						supplierUser.setFirstName(supplier.getName() != null ? supplier.getName() : supplier.getCode());
						//supplierUser.setLastName(supplier.getName() != null ? supplier.getName() : supplier.getCode());
						supplierUser.setLastName("");
						supplierUser.setUserSupplier(supplier);
						//supplierUser.setUserPlant(plant);
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
						plantSupplier.setSupplierName(supplierCode);
						plantSupplier.setPlant(plant);
						plantSupplier.setSupplier(supplier);
						plantSupplier.setDoNoPrefix(String.valueOf(this.numberControlManager.getNextNumber(Constants.DO_NO_PREFIX)));
						
						PlantScheduleGroup defaultPlantScheduleGroup = this.plantScheduleGroupManager.getDefaultPlantScheduleGroupByPlantCode(plant.getCode());
						if (defaultPlantScheduleGroup != null) {
							plantSupplier.setPlantScheduleGroup(defaultPlantScheduleGroup);
						}

						plantSupplier = this.plantSupplierManager.save(plantSupplier);					
					}

					receipt.setPlantSupplier(plantSupplier);
				}

				Item item = null;

				String itemCode = E1BP2017GMITEMCREATE.getMATERIAL();
				try {
					// 零件号如果是全数字，则要把前置0去掉
					itemCode = Long.toString((Long.parseLong(itemCode)));
				} catch (NumberFormatException ex) {
				}

				item = this.itemManager.getItemByPlantAndItem(plant, itemCode);

				if (item == null) {
					log.info("Item not found with the given item code: " + itemCode + ", try to create a new one.");

					item = new Item();
					item.setCode(itemCode);
					item.setDescription(itemCode);
					item.setPlant(plant);
					item.setUom(E1BP2017GMITEMCREATE.getENTRYUOM());

					item = this.itemManager.save(item);
				}

				receiptDetail.setItem(item);
				receiptDetail.setUom(E1BP2017GMITEMCREATE.getENTRYUOM());
				receiptDetail.setQty(new BigDecimal(E1BP2017GMITEMCREATE.getENTRYQNT().trim()));
				receiptDetail.setReferenceOrderNo(E1BP2017GMITEMCREATE.getPONUMBER());
				receiptDetail.setReferenceSequence(E1BP2017GMITEMCREATE.getPOITEM());
				receiptDetail.setPlusMinus(E1BP2017GMITEMCREATE.getMVTIND());

				receipt.addReceiptDetail(receiptDetail);
			}
		} catch (Exception ex) {
			log.error("Error occur when convert MBGMCR02 To PO.", ex);
			throw new DataConvertException(ex, receipt);
		}

		return receipt;
	}
}
