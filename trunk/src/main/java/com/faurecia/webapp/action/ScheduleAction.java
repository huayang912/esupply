package com.faurecia.webapp.action;

import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.Schedule;
import com.faurecia.service.ScheduleManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class ScheduleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 5754208797077346146L;
	private ScheduleManager scheduleOrderManager;

	private PaginatedListUtil<PurchaseOrder> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private Schedule schedule;
	private String scheduleNo;
	
	public ScheduleManager getScheduleOrderManager() {
		return scheduleOrderManager;
	}
	public void setScheduleOrderManager(ScheduleManager scheduleOrderManager) {
		this.scheduleOrderManager = scheduleOrderManager;
	}
	public PaginatedListUtil<PurchaseOrder> getPaginatedList() {
		return paginatedList;
	}
	public void setPaginatedList(PaginatedListUtil<PurchaseOrder> paginatedList) {
		this.paginatedList = paginatedList;
	}
	public int getPageSize() {
		return pageSize;
	}
	public void setPageSize(int pageSize) {
		this.pageSize = pageSize;
	}
	public int getPage() {
		return page;
	}
	public void setPage(int page) {
		this.page = page;
	}
	public String getSort() {
		return sort;
	}
	public void setSort(String sort) {
		this.sort = sort;
	}
	public String getDir() {
		return dir;
	}
	public void setDir(String dir) {
		this.dir = dir;
	}
	public Schedule getSchedule() {
		return schedule;
	}
	public void setSchedule(Schedule schedule) {
		this.schedule = schedule;
	}
	public String getScheduleNo() {
		return scheduleNo;
	}
	public void setScheduleNo(String scheduleNo) {
		this.scheduleNo = scheduleNo;
	}	
	
	public String list() {
		return SUCCESS;
	}
	
	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		return SUCCESS;
	}
}
