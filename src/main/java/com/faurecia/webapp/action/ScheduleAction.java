package com.faurecia.webapp.action;

import java.math.BigDecimal;
import java.util.Date;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import com.faurecia.Constants;
import com.faurecia.model.Schedule;
import com.faurecia.model.ScheduleBody;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.service.ScheduleManager;
import com.faurecia.service.SupplierManager;

public class ScheduleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 5754208797077346146L;
	private ScheduleManager scheduleManager;
	private Schedule schedule;
	private ScheduleView scheduleView;
	private SupplierManager supplierManager;
	private boolean isPlantUser;

	public ScheduleManager getScheduleManager() {
		return scheduleManager;
	}

	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
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
	
	public boolean getIsPlantUser() {
		return isPlantUser;
	}

	public List<Supplier> getSuppliers() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		return this.supplierManager.getSuppliersByPlant(user.getUserPlant());
	}

	public ScheduleView getScheduleView() {
		if (scheduleView == null) {
			scheduleView = new ScheduleView();

			if (schedule != null) {
				if (schedule.getScheduleItemList() != null && schedule.getScheduleItemList().size() > 0) {
					for (int i = 0; i < schedule.getScheduleItemList().size(); i++) {
						ScheduleItem scheduleItem = schedule.getScheduleItemList().get(i);
						if (scheduleItem.getScheduleItemDetailList() != null && scheduleItem.getScheduleItemDetailList().size() > 0) {
							for (int j = 0; j < scheduleItem.getScheduleItemDetailList().size(); j++) {

								ScheduleItemDetail scheduleItemDetail = scheduleItem.getScheduleItemDetailList().get(j);

								int insertPosion = -1;
								for (int k = 0; k < scheduleView.getScheduleHead().getDateFromList().size(); k++) {
									String scheduleType = scheduleView.getScheduleHead().getScheduleTypeList().get(k);
									Date dateFrom = scheduleView.getScheduleHead().getDateFromList().get(k);
									// Date dateTo =
									// scheduleView.getScheduleHead().getDateToList().get(k);

									if (dateFrom.compareTo(scheduleItemDetail.getDateFrom()) > 0) {
										insertPosion = k;
										break;
									} else if (dateFrom.compareTo(scheduleItemDetail.getDateFrom()) == 0) {
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

								if (insertPosion != -1) {
									scheduleView.getScheduleHead().getScheduleTypeList().add(insertPosion, scheduleItemDetail.getScheduleType());
									scheduleView.getScheduleHead().getDateFromList().add(insertPosion, scheduleItemDetail.getDateFrom());
									scheduleView.getScheduleHead().getDateToList().add(insertPosion, scheduleItemDetail.getDateTo());
								} else {
									scheduleView.getScheduleHead().getScheduleTypeList().add(scheduleItemDetail.getScheduleType());
									scheduleView.getScheduleHead().getDateFromList().add(scheduleItemDetail.getDateFrom());
									scheduleView.getScheduleHead().getDateToList().add(scheduleItemDetail.getDateTo());
								}
							}
						}
					}
				}

				if (schedule.getScheduleItemList() != null && schedule.getScheduleItemList().size() > 0) {
					for (int i = 0; i < schedule.getScheduleItemList().size(); i++) {

						ScheduleItem scheduleItem = schedule.getScheduleItemList().get(i);
						ScheduleBody scheduleBody = new ScheduleBody();
						scheduleBody.setCreateDate(scheduleItem.getCreateDate());
						scheduleBody.setReleaseNo(scheduleItem.getReleaseNo());
						scheduleBody.setItemCode(scheduleItem.getItem().getCode());
						scheduleBody.setItemDescription(scheduleItem.getItemDescription());
						scheduleBody.setSupplierItemCode(scheduleItem.getSupplierItemCode());
						scheduleView.addScheduleBody(scheduleBody);

						for (int j = 0; j < scheduleView.getScheduleHead().getDateFromList().size(); j++) {
							String scheduleType = scheduleView.getScheduleHead().getScheduleTypeList().get(j);
							Date dateFrom = scheduleView.getScheduleHead().getDateFromList().get(j);

							boolean findMatch = false;
							if (scheduleItem.getScheduleItemDetailList() != null && scheduleItem.getScheduleItemDetailList().size() > 0) {
								for (int k = 0; k < scheduleItem.getScheduleItemDetailList().size(); k++) {
									ScheduleItemDetail scheduleItemDetail = scheduleItem.getScheduleItemDetailList().get(k);

									if (scheduleType.equals(scheduleItemDetail.getScheduleType())
											&& dateFrom.compareTo(scheduleItemDetail.getDateFrom()) == 0) {
										scheduleBody.addQty(scheduleItemDetail.getReleaseQty());
										findMatch = true;
									}
								}
							}

							if (!findMatch) {
								scheduleBody.addQty(BigDecimal.ZERO);
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
				schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), schedule.getSupplierCode(), new Date());
			} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
				schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), user.getUserSupplier().getCode(), new Date());
			}
		} else {
			if (request.isUserInRole(Constants.PLANT_USER_ROLE)) {
				schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), schedule.getSupplierCode(), schedule.getCreateDate());
			} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
				schedule = this.scheduleManager.getLastestScheduleItem(user.getUserPlant().getCode(), user.getUserSupplier().getCode(), schedule.getCreateDate());
			}
		}

		return SUCCESS;
	}
}
