package com.faurecia.service.impl;

import java.io.InputStream;
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
import javax.xml.bind.Unmarshaller;

import org.apache.commons.lang.RandomStringUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.mail.MailException;
import org.springframework.mail.SimpleMailMessage;
import org.springframework.orm.ObjectRetrievalFailureException;

import com.faurecia.Constants;
import com.faurecia.dao.GenericDao;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Role;
import com.faurecia.model.Schedule;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;
import com.faurecia.model.User;
import com.faurecia.model.delfor.DELFOR02;
import com.faurecia.model.delfor.DELFOR02E1EDK09;
import com.faurecia.model.delfor.DELFOR02E1EDKA1;
import com.faurecia.model.delfor.DELFOR02E1EDP10;
import com.faurecia.model.delfor.DELFOR02E1EDP16;
import com.faurecia.service.DataConvertException;
import com.faurecia.service.GenericManager;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.ItemManager;
import com.faurecia.service.MailEngine;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.RoleManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserManager;

public class ScheduleManagerImpl extends GenericManagerImpl<Schedule, String> implements ScheduleManager {

	private GenericManager<ScheduleItem, Integer> scheduleItemManager;
	private GenericManager<Plant, String> plantManager;
	private SupplierManager supplierManager;
	private PlantSupplierManager plantSupplierManager;
	private ItemManager itemManager;
	private SupplierItemManager supplierItemManager;
	private InboundLogManager inboundLogManager;
	private UserManager userManager;
	private RoleManager roleManager;
	private Unmarshaller unmarshaller;
	private JdbcTemplate jdbcTemplate;

	protected MailEngine mailEngine;
	protected SimpleMailMessage mailMessage;
	protected String errorLogTemplateName;
	protected String supplierCreatedTemplateName;

	public ScheduleManagerImpl(GenericDao<Schedule, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.delfor");
		unmarshaller = jc.createUnmarshaller();
	}

	public void setScheduleItemManager(GenericManager<ScheduleItem, Integer> scheduleItemManager) {
		this.scheduleItemManager = scheduleItemManager;
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

	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}

	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}

	public void setRoleManager(RoleManager roleManager) {
		this.roleManager = roleManager;
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

	public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
	}

	public Schedule SaveSingleFile(InputStream inputStream, InboundLog inboundLog) {
		try {
			DELFOR02 delfor = unmarshalOrder(inputStream);
			Schedule schedule = DELFOR02ToSchedule(delfor);

			if (inboundLog.getPlantSupplier() == null) {
				inboundLog.setPlantSupplier(schedule.getPlantSupplier());
			}				

			// 零件号+版本号如果遇到重复的情况，覆盖原有的记录(先删除，在新增)
			if (schedule.getScheduleItemList() != null && schedule.getScheduleItemList().size() > 0) {
				for (int i = 0; i < schedule.getScheduleItemList().size(); i++) {
					ScheduleItem scheduleItem = schedule.getScheduleItemList().get(i);

					DetachedCriteria criteria = DetachedCriteria.forClass(ScheduleItem.class);

					criteria.add(Restrictions.eq("schedule", scheduleItem.getSchedule()));
					criteria.add(Restrictions.eq("item", scheduleItem.getItem()));
					criteria.add(Restrictions.eq("releaseNo", scheduleItem.getReleaseNo()));

					List<ScheduleItem> scheduleItemList = this.findByCriteria(criteria);

					if (scheduleItemList != null && scheduleItemList.size() > 0) {
						this.scheduleItemManager.remove(scheduleItemList.get(0).getId());
					}
				}
			}
			// 保存采购单
			this.save(schedule);

			inboundLog.setInboundResult("success");

			return schedule;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal DELFOR02.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert DELFOR02 to Schedule.", dataConvertException);
			inboundLog.setInboundResult("fail");

			Schedule schedule = (Schedule) dataConvertException.getObject();
			if (schedule != null && schedule.getPlantSupplier() != null) {
				inboundLog.setPlantSupplier(schedule.getPlantSupplier());
			}
			inboundLog.setMemo(dataConvertException.getMessage());
		} catch (Exception exception) {
			log.error("Error occur.", exception);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(exception.getMessage());
		}

		return null;
	}
	
	public Schedule getLastestScheduleItem(String plantCode, String supplierCode) {
		return getLastestScheduleItem(plantCode, supplierCode, new Date());
	}

	public Schedule getLastestScheduleItem(String plantCode, String supplierCode, Date tillDate) {
		String sql = "select schedule_item.id from schedule_item inner join "
				+ "(select item_code, max(release_no) as release_no from schedule_item "
				+ "inner join schedule on schedule_item.schedule_no = schedule.schedule_no "
				+ "inner join plant_supplier on schedule.plant_supplier_id = plant_supplier.id "
				+ "where plant_supplier.plant_code = ? and plant_supplier.supplier_code = ? and schedule_item.create_date <= ? " 
				+ "group by item_code) as a on schedule_item.item_code = a.item_code and schedule_item.release_no = a.release_no";
		
		
		SqlRowSet resultSet = this.jdbcTemplate.queryForRowSet(sql, new Object[]{plantCode, supplierCode, tillDate});
		List<Integer> scheduleItemIdIist = new ArrayList<Integer>();
		
		while (resultSet.next()) {
			scheduleItemIdIist.add(resultSet.getInt(1));
		}
		
		DetachedCriteria criteria = DetachedCriteria.forClass(ScheduleItem.class);
		criteria.add(Restrictions.in("id", scheduleItemIdIist));
		criteria.addOrder(Order.asc("sequence"));
		
		List<ScheduleItem> scheduleItemList = this.findByCriteria(criteria);
		
		if (scheduleItemList != null && scheduleItemList.size() > 0) {
			Schedule schedule = null;
			for (int i = 0; i < scheduleItemList.size(); i++) {
				ScheduleItem scheduleItem = scheduleItemList.get(i);
				if (scheduleItem.getScheduleItemDetailList() != null && scheduleItem.getScheduleItemDetailList().size() > 0) {
					
				}
				
				//查找最新的Schedule
				if (schedule == null || schedule.getCreateDate().compareTo(scheduleItem.getSchedule().getCreateDate()) < 0) {
					schedule = scheduleItemList.get(i).getSchedule();
				}
			}
			
			schedule.setScheduleItemList(scheduleItemList);
			
			return schedule;
		}
		else {
			return null;
		}
	}

	private DELFOR02 unmarshalOrder(InputStream stream) throws JAXBException {
		DELFOR02 d = (DELFOR02) unmarshaller.unmarshal(stream);
		return d;
	}

	private Schedule DELFOR02ToSchedule(final DELFOR02 delfor) throws DataConvertException {
		Schedule schedule = new Schedule();
		// po.setStatus("Open");

		try {
			Plant plant = null;
			Supplier supplier = null;
			DELFOR02E1EDKA1 supplierE1EDKA1 = null;
			DateFormat dateFormat = new SimpleDateFormat("yyyyMMdd");
			DELFOR02E1EDK09 E1EDK09 = delfor.getIDOC().getE1EDK09();

			schedule.setScheduleNo(E1EDK09.getVTRNR()); // po number
			schedule.setCreateDate(dateFormat.parse(delfor.getIDOC().getEDIDC40().getCREDAT()));

			List<DELFOR02E1EDKA1> DELFOR02E1EDKA1List = E1EDK09.getE1EDKA1();
			if (DELFOR02E1EDKA1List != null && DELFOR02E1EDKA1List.size() > 0) {
				for (int i = 0; i < DELFOR02E1EDKA1List.size(); i++) {
					DELFOR02E1EDKA1 E1EDKA1 = DELFOR02E1EDKA1List.get(i);
					if ("WE".equals(E1EDKA1.getPARVW())) {
						String plantCode = E1EDKA1.getPARTN();

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

			schedule.setPlantSupplier(plantSupplier);

			// ----------------------------schedule item---------------------
			List<DELFOR02E1EDP10> E1EDP10List = E1EDK09.getE1EDP10();
			if (E1EDP10List != null && E1EDP10List.size() > 0) {
				for (int i = 0; i < E1EDP10List.size(); i++) {
					DELFOR02E1EDP10 E1EDP10 = E1EDP10List.get(i);
					Item item = null;
					String itemDescription = null;
					SupplierItem supplierItem = null;
					String supplierItemCode = null;

					ScheduleItem scheduleItem = new ScheduleItem();

					String itemCode = E1EDP10.getIDNKD();
					try {
						// 零件号如果是全数字，则要把前置0去掉
						itemCode = Long.toString((Long.parseLong(itemCode)));
					} catch (NumberFormatException ex) {
					}

					item = this.itemManager.getItemByPlantAndItem(plant, itemCode);
					itemDescription = E1EDP10.getARKTX();

					if (item == null) {
						log.info("Item not found with the given item code: " + itemCode + ", try to create a new one.");

						item = new Item();
						item.setCode(itemCode);
						item.setDescription(itemDescription);
						item.setPlant(plant);
						item.setUom(E1EDP10.getVRKME());

						item = this.itemManager.save(item);
					}

					supplierItemCode = E1EDP10.getIDNLF();

					if (supplierItemCode != null && supplierItemCode.trim().length() > 0) {
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

					scheduleItem.setSchedule(schedule);
					scheduleItem.setItem(item);
					scheduleItem.setItemDescription(itemDescription);
					scheduleItem.setSupplierItemCode(supplierItemCode);
					scheduleItem.setUom(E1EDP10.getVRKME());
					scheduleItem.setReleaseNo(Integer.parseInt(E1EDP10.getLABNK()));
					scheduleItem.setReceivedQty(new BigDecimal(E1EDP10.getAKUEM()));
					scheduleItem.setSequence(E1EDP10.getPOSEX());
					scheduleItem.setCreateDate(schedule.getCreateDate());

					schedule.addScheduleItem(scheduleItem);

					// ----------------------------schedule item
					// detail---------------------

					List<DELFOR02E1EDP16> E1EDP16List = E1EDP10.getE1EDP16();

					if (E1EDP16List != null && E1EDP16List.size() > 0) {
						for (int j = 0; j < E1EDP16List.size(); j++) {
							DELFOR02E1EDP16 E1EDP16 = E1EDP16List.get(j);

							ScheduleItemDetail scheduleItemDetail = new ScheduleItemDetail();

							String scheduleType = E1EDP16.getETTYP();
							String dateType = E1EDP16.getPRGRS();

							if ("R".equals(scheduleType)) {
								scheduleItemDetail.setScheduleType("Backlog + Immediate Requirement");
							} else if ("1".equals(scheduleType)) {
								scheduleItemDetail.setScheduleType("Firm");
							} else// if ("4".equals(scheduleType))
							{
								scheduleItemDetail.setScheduleType("Forecast");
							}

							if ("M".equals(dateType)) {
								scheduleItemDetail.setDateType("Month");
							} else if ("W".equals(dateType)) {
								scheduleItemDetail.setDateType("Week");
							} else {
								scheduleItemDetail.setDateType("Day");
							}

							scheduleItemDetail.setDateFrom(dateFormat.parse(E1EDP16.getEDATUV()));
							scheduleItemDetail.setDateTo(dateFormat.parse(E1EDP16.getEDATUB()));
							scheduleItemDetail.setReleaseQty(new BigDecimal(E1EDP16.getWMENG()));
							scheduleItemDetail.setScheduleItem(scheduleItem);

							scheduleItem.addScheduleItemDetail(scheduleItemDetail);
						}
					}
				}
			}
		} catch (Exception ex) {
			log.error("Error occur when convert ORDERS02 To PO.", ex);
			throw new DataConvertException(ex, schedule);
		}

		return schedule;
	}
}