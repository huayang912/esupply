package com.faurecia.webapp.action;

import javax.servlet.http.HttpServletRequest;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.Schedule;
import com.faurecia.model.User;
import com.faurecia.service.ScheduleManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class ScheduleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 5754208797077346146L;
	private ScheduleManager scheduleManager;

	private PaginatedListUtil<PurchaseOrder> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private Schedule schedule;
	private String scheduleNo;
	
	public ScheduleManager getScheduleManager() {
		return scheduleManager;
	}
	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
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
		if (schedule == null) {
			schedule = new Schedule();
		}
		
		pageSize = pageSize == 0 ? 25 : pageSize;
		page  = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<PurchaseOrder>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(Schedule.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(Schedule.class)
			.setProjection(Projections.count("scheduleNo"));
		
		selectCriteria.createAlias("plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCriteria.createAlias("ps.supplier", "s");
		selectCountCriteria.createAlias("plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("ps.supplier", "s");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		HttpServletRequest request = this.getRequest();

		if (request.isUserInRole(Constants.PLANT_USER_ROLE) || request.isUserInRole(Constants.PLANT_ADMIN_ROLE)) {
			selectCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
			selectCountCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
		} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
			selectCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
			selectCountCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
			selectCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
		}
		
		if (schedule.getCreateDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("createDate", schedule.getCreateDateFrom()));
			selectCountCriteria.add(Restrictions.ge("createDate", schedule.getCreateDateFrom()));
		}
		
		if (schedule.getCreateDateTo() != null) {
			selectCriteria.add(Restrictions.le("createDate", schedule.getCreateDateTo()));
			selectCountCriteria.add(Restrictions.le("createDate", schedule.getCreateDateTo()));
		}
			
		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if (SortOrderEnum.DESCENDING.equals(dir))
			{
				selectCriteria.addOrder(Order.desc(sort));
				paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
			}
			else
			{
				selectCriteria.addOrder(Order.asc(sort));
				paginatedList.setSortDirection(SortOrderEnum.ASCENDING);
			}
		}

		paginatedList.setList(this.scheduleManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		this.scheduleManager.findByCriteria(selectCountCriteria);
		paginatedList.setFullListSize(Integer.parseInt(this.scheduleManager.findByCriteria(selectCountCriteria).get(0).toString()));	
		
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
		boolean editProfile = (request.getRequestURI().indexOf("editScheduleProfile") > -1);
		
		if (this.scheduleNo != null) {
			schedule = this.scheduleManager.get(scheduleNo);
		} else if (editProfile) {
			User user = userManager.getUserByUsername(request.getRemoteUser());
		}
		
		return SUCCESS;
	}
}
