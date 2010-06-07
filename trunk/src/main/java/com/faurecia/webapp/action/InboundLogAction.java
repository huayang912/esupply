package com.faurecia.webapp.action;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.InboundLog;
import com.faurecia.model.User;
import com.faurecia.service.InboundLogManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class InboundLogAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7108954954452920378L;

	private InboundLogManager inboundLogManager;
	private PaginatedListUtil<InboundLog> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private InboundLog inboundLog;

	public void setInboundLogManager(InboundLogManager inboundLogManager) {
		this.inboundLogManager = inboundLogManager;
	}
	public PaginatedListUtil<InboundLog> getPaginatedList() {
		return paginatedList;
	}
	public void setPaginatedList(PaginatedListUtil<InboundLog> paginatedList) {
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
	public InboundLog getInboundLog() {
		return inboundLog;
	}
	public void setInboundLog(InboundLog inboundLog) {
		this.inboundLog = inboundLog;
	}
	
	public String list() {
		if (inboundLog == null) {
			inboundLog = new InboundLog();
		}
			
		pageSize = pageSize == 0 ? 25 : pageSize;
		page  = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<InboundLog>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(InboundLog.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(InboundLog.class)
			.setProjection(Projections.count("id"));
		
		selectCriteria.createAlias("plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		selectCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
		selectCountCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
	
		if (inboundLog.getCreateDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("createDate", inboundLog.getCreateDateFrom()));
			selectCountCriteria.add(Restrictions.ge("createDate", inboundLog.getCreateDateFrom()));
		}
		
		if (inboundLog.getCreateDateTo() != null) {
			selectCriteria.add(Restrictions.le("createDate", inboundLog.getCreateDateTo()));
			selectCountCriteria.add(Restrictions.le("createDate", inboundLog.getCreateDateTo()));
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

		paginatedList.setList(this.inboundLogManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.inboundLogManager.findByCriteria(selectCountCriteria).get(0).toString()));			

		return SUCCESS;
	}
}
