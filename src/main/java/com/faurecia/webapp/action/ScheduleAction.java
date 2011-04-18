package com.faurecia.webapp.action;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.httpclient.util.DateParseException;

import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Schedule;
import com.faurecia.model.ScheduleBody;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.Supplier;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.service.SupplierManager;

public class ScheduleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 5754208797077346146L;
	private ScheduleManager scheduleManager;
	private PlantSupplierManager plantSupplierManager;
	private SupplierManager supplierManager;
	private Schedule schedule;
	private ScheduleView scheduleView;
	private PlantSupplier plantSupplier;
	private Date effectiveDate;
	private Boolean isHistory;

	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}

	public ScheduleManager getScheduleManager() {
		return scheduleManager;
	}

	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public Schedule getSchedule() {
		return schedule;
	}

	public void setSchedule(Schedule schedule) {
		this.schedule = schedule;
	}

	public Date getEffectiveDate() {
		return effectiveDate;
	}

	public void setEffectiveDate(Date effectiveDate) {
		this.effectiveDate = effectiveDate;
	}

	public Boolean getIsHistory() {
		return isHistory;
	}

	public void setIsHistory(Boolean isHistory) {
		this.isHistory = isHistory;
	}

	public List<Supplier> getSuppliers() {
		if (plantSupplier != null && plantSupplier.getPlant() != null && plantSupplier.getPlant().getCode() != null) {
			return this.supplierManager.getSuppliersByPlantAndUser(plantSupplier.getPlant().getCode().trim() + "|"
					+ this.getRequest().getRemoteUser());
		} else {
			List<Plant> plants = getPlants();
			if (plants != null && plants.size() > 0) {
				return this.supplierManager.getSuppliersByPlantAndUser(plants.get(0).getCode().trim() + "|" + this.getRequest().getRemoteUser());
			}
			
			return new ArrayList<Supplier>();
		}
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public ScheduleView getScheduleView() throws DateParseException {
		PlantScheduleGroup plantScheduleGroup = schedule.getPlantSupplier().getPlantScheduleGroup();
		boolean allowOverDateDeliver = plantScheduleGroup != null ? plantScheduleGroup.getAllowOverDateDeliver() : false;
		boolean allowFirmDeliver = plantScheduleGroup != null ? plantScheduleGroup.getAllowFirmDeliver() : false;
		// boolean allowOverQtyDeliver = plantScheduleGroup != null ?
		// plantScheduleGroup.getAllowOverQtyDeliver() : false;
		boolean allowForecastDeliver = plantScheduleGroup != null ? plantScheduleGroup.getAllowForecastDeliver() : false;

		if (scheduleView == null) {
			scheduleView = new ScheduleView();

			if (schedule != null) {

				Calendar calendar = Calendar.getInstance();
				calendar.setTime(new Date());
				calendar.set(Calendar.HOUR_OF_DAY, 0);
				calendar.set(Calendar.MINUTE, 0);
				calendar.set(Calendar.SECOND, 0);
				calendar.set(Calendar.MILLISECOND, 0);
				Date dateNow = calendar.getTime();

				if (schedule.getScheduleItemList() != null && schedule.getScheduleItemList().size() > 0) {
					for (int i = 0; i < schedule.getScheduleItemList().size(); i++) {
						ScheduleItem scheduleItem = schedule.getScheduleItemList().get(i);
						if (scheduleItem.getScheduleItemDetailList() != null && scheduleItem.getScheduleItemDetailList().size() > 0) {
							for (int j = 0; j < scheduleItem.getScheduleItemDetailList().size(); j++) {

								ScheduleItemDetail scheduleItemDetail = scheduleItem.getScheduleItemDetailList().get(j);

								int insertPosion = -1;
								for (int k = 0; k < scheduleView.getScheduleHead().getHeadList().size(); k++) {
									Map<String, Object> head = scheduleView.getScheduleHead().getHeadList().get(k);
									String scheduleType = (String) head.get("scheduleType");
									Date dateFrom = (Date) head.get("dateFrom");
									// Date dateTo =
									// scheduleView.getScheduleHead().getDateToList().get(k);

									if (dateFrom.compareTo(scheduleItemDetail.getDateFrom()) > 0) {
										insertPosion = k;
										break;
									} else if (dateFrom.compareTo(scheduleItemDetail.getDateFrom()) == 0) {
										if (scheduleType.equals(scheduleItemDetail.getScheduleType())) {
											insertPosion = -2;
											break;
										}

										if ("Backlog + Immediate Requirement".equals(scheduleItemDetail.getScheduleType())
												&& "Firm".equals(scheduleType)) {
											insertPosion = k;
											break;
										} else if ("Backlog + Immediate Requirement".equals(scheduleItemDetail.getScheduleType())
												&& "Forecast".equals(scheduleType)) {
											insertPosion = k;
											break;
										} else if ("Firm".equals(scheduleItemDetail.getScheduleType()) && "Forecast".equals(scheduleType)) {
											insertPosion = k;
											break;
										}
									}
								}

								if (insertPosion > -2) {
									Map<String, Object> head = new HashMap<String, Object>();
									head.put("scheduleType", scheduleItemDetail.getScheduleType());
									head.put("dateFrom", scheduleItemDetail.getDateFrom());
									head.put("dateTo", scheduleItemDetail.getDateTo());

									// if
									// (getRequest().isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE))
									// {
									// head.put("createDo", false);
									// } else
									if (allowOverDateDeliver) {
										if (!allowFirmDeliver && scheduleItemDetail.getScheduleType().equals("Firm")) {
											head.put("createDo", false);
										} else if (!allowForecastDeliver && scheduleItemDetail.getScheduleType().equals("Forecast")) {
											head.put("createDo", false);
										} else {
											head.put("createDo", true);
										}
									} else {
										if (dateNow.compareTo(scheduleItemDetail.getDateTo()) > 0) {
											head.put("createDo", false);
										} else {
											if (!allowFirmDeliver && scheduleItemDetail.getScheduleType().equals("Firm")) {
												head.put("createDo", false);
											} else if (!allowForecastDeliver && scheduleItemDetail.getScheduleType().equals("Forecast")) {
												head.put("createDo", false);
											} else {
												head.put("createDo", true);
											}
										}
									}

									if (insertPosion != -1) {
										scheduleView.getScheduleHead().getHeadList().add(insertPosion, head);
									} else {
										scheduleView.getScheduleHead().getHeadList().add(head);
									}
								}
							}
						}
					}
				}

				if (schedule.getScheduleItemList() != null && schedule.getScheduleItemList().size() > 0) {
					for (int i = 0; i < schedule.getScheduleItemList().size(); i++) {

						ScheduleItem scheduleItem = schedule.getScheduleItemList().get(i);
						ScheduleBody scheduleBody = new ScheduleBody();
						scheduleBody.setScheduleItemId(scheduleItem.getId());
						scheduleBody.setCreateDate(scheduleItem.getCreateDate());
						scheduleBody.setReleaseNo(scheduleItem.getReleaseNo());
						scheduleBody.setItemCode(scheduleItem.getItem().getCode());
						scheduleBody.setItemDescription(scheduleItem.getItemDescription());
						scheduleBody.setSupplierItemCode(scheduleItem.getSupplierItemCode());
						scheduleView.addScheduleBody(scheduleBody);

						for (int j = 0; j < scheduleView.getScheduleHead().getHeadList().size(); j++) {
							Map<String, Object> head = scheduleView.getScheduleHead().getHeadList().get(j);
							String scheduleType = (String) head.get("scheduleType");
							Date dateFrom = (Date) head.get("dateFrom");

							boolean findMatch = false;
							if (scheduleItem.getScheduleItemDetailList() != null && scheduleItem.getScheduleItemDetailList().size() > 0) {
								for (int k = 0; k < scheduleItem.getScheduleItemDetailList().size(); k++) {
									ScheduleItemDetail scheduleItemDetail = scheduleItem.getScheduleItemDetailList().get(k);

									if (scheduleType.equals(scheduleItemDetail.getScheduleType())
											&& dateFrom.compareTo(scheduleItemDetail.getDateFrom()) == 0) {
										scheduleBody.addQty(scheduleItemDetail.getReleaseQty());
										scheduleBody.addDeliverQty(scheduleItemDetail.getDeliverQty());
										findMatch = true;
									}
								}
							}

							if (!findMatch) {
								scheduleBody.addQty(BigDecimal.ZERO);
								scheduleBody.addDeliverQty(BigDecimal.ZERO);
							}
						}

					}
				}
			}
		}

		return scheduleView;
	}

	public String list() throws Exception {
		if (plantSupplier != null && plantSupplier.getPlant() != null && plantSupplier.getPlant().getCode() != null
				&& plantSupplier.getPlant().getCode().trim().length() != 0 && plantSupplier.getSupplier() != null
				&& plantSupplier.getSupplier().getCode() != null && plantSupplier.getSupplier().getCode().trim().length() != 0) {

			if (effectiveDate != null) {
				schedule = this.scheduleManager.getLastestScheduleItem(plantSupplier.getPlant().getCode(), plantSupplier.getSupplier().getCode(),
						effectiveDate, true);
			} else {
				schedule = this.scheduleManager.getLastestScheduleItem(plantSupplier.getPlant().getCode(), plantSupplier.getSupplier().getCode(),
						new Date(), true);
			}

			return SUCCESS;
		}
		return INPUT;
	}

	public String listHistory() {
		isHistory = true;
		return SUCCESS;
	}

	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}

		return CANCEL;
	}

	public String listAudit() throws Exception {

		if (plantSupplier != null && plantSupplier.getPlant() != null && plantSupplier.getPlant().getCode() != null
				&& plantSupplier.getPlant().getCode().trim().length() != 0 && plantSupplier.getSupplier() != null
				&& plantSupplier.getSupplier().getCode() != null && plantSupplier.getSupplier().getCode().trim().length() != 0) {
			schedule = this.scheduleManager.getLastestScheduleItem(plantSupplier.getPlant().getCode(), plantSupplier.getSupplier().getCode(),
					new Date(), false);
		}

		return SUCCESS;
	}

	public String confirm() throws Exception {

		String[] scheduleItemIds = this.getRequest().getParameterValues("scheduleItem");

		this.scheduleManager.confirmScheduleItem(scheduleItemIds);
		this.scheduleManager.flushSession();

		schedule = this.scheduleManager.getLastestScheduleItem(plantSupplier.getPlant().getCode(), plantSupplier.getSupplier().getCode(), new Date(),
				false);

		return SUCCESS;
	}
}
