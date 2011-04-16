package com.faurecia.webapp.action;

import java.math.BigDecimal;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.apache.commons.httpclient.util.DateParseException;

import com.faurecia.Constants;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Schedule;
import com.faurecia.model.ScheduleBody;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ScheduleManager;

public class ScheduleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 5754208797077346146L;
	private ScheduleManager scheduleManager;
	private PlantSupplierManager plantSupplierManager;
	private Schedule schedule;
	private ScheduleView scheduleView;
	private boolean isPlantUser;
	private Supplier supplier;

	public Supplier getSupplier() {
		return supplier;
	}

	public void setSupplier(Supplier supplier) {
		this.supplier = supplier;
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

	public Schedule getSchedule() {
		return schedule;
	}

	public void setSchedule(Schedule schedule) {
		this.schedule = schedule;
	}

	public boolean getIsPlantUser() {
		return isPlantUser;
	}

	public List<PlantSupplier> getSuppliers() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		return this.plantSupplierManager.getPlantSupplierByUserId(user.getId());
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

									if (getRequest().isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)) {
										head.put("createDo", false);
									} else if (allowOverDateDeliver) {
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

	public String list() {
		HttpServletRequest request = getRequest();
		if (request.isUserInRole(Constants.PLANT_USER_ROLE)) {
			this.isPlantUser = true;
		}

		schedule = new Schedule();
		schedule.setCreateDate(new Date());

		return SUCCESS;
	}

	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}

		return CANCEL;
	}

	public String edit() throws Exception {
		HttpServletRequest request = getRequest();
		User user = this.userManager.getUserByUsername(request.getRemoteUser());
		boolean editProfile = (request.getRequestURI().indexOf("editScheduleProfile") > -1);

		if (editProfile) {
			if (request.isUserInRole(Constants.PLANT_USER_ROLE)) {
				schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), schedule.getSupplierCode(), new Date(), true);
			} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
				if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
					return "mainMenu";
				}

				schedule = this.scheduleManager.getLastestScheduleItem((String) this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE), user
						.getUserSupplier().getCode(), new Date(), true);
			}
		} else {
			if (request.isUserInRole(Constants.PLANT_USER_ROLE)) {
				schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), schedule.getSupplierCode(), schedule
						.getCreateDate(), true);
			} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
				if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
					return "mainMenu";
				}

				schedule = this.scheduleManager.getLastestScheduleItem((String) this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE), user
						.getUserSupplier().getCode(), schedule.getCreateDate(), true);
			}
		}

		return SUCCESS;
	}

	public String listAudit() {
		return SUCCESS;
	}

	public String audit() throws Exception {
		HttpServletRequest request = getRequest();
		User user = this.userManager.getUserByUsername(request.getRemoteUser());
		if (supplier != null) {
			schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), supplier.getCode(), new Date(), false);
		}

		return SUCCESS;
	}

	public String confirm() throws Exception {

		String[] scheduleItemIds = this.getRequest().getParameterValues("scheduleItem");

		this.scheduleManager.confirmScheduleItem(scheduleItemIds);

		HttpServletRequest request = getRequest();
		User user = this.userManager.getUserByUsername(request.getRemoteUser());
		schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), supplier.getCode(), new Date(), false);
		this.scheduleManager.flushSession();
		return SUCCESS;
	}
}
