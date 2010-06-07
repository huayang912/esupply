package com.faurecia.webapp.action;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.OutboundLog;
import com.faurecia.model.User;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.OutboundLogManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class OutboundLogAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -5301155045082867465L;

	private OutboundLogManager outboundLogManager;
	private DeliveryOrderManager deliveryOrderManager;
	private PaginatedListUtil<OutboundLog> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private OutboundLog outboundLog;
	private String doNo;
	
	public void setOutboundLogManager(OutboundLogManager outboundLogManager) {
		this.outboundLogManager = outboundLogManager;
	}
	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}
	public PaginatedListUtil<OutboundLog> getPaginatedList() {
		return paginatedList;
	}
	public void setPaginatedList(PaginatedListUtil<OutboundLog> paginatedList) {
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
	public OutboundLog getOutboundLog() {
		return outboundLog;
	}
	public void setOutboundLog(OutboundLog outboundLog) {
		this.outboundLog = outboundLog;
	}
	public String getDoNo() {
		return doNo;
	}
	public void setDoNo(String doNo) {
		this.doNo = doNo;
	}
	public String list() {
		if (outboundLog == null) {
			outboundLog = new OutboundLog();
		}
			
		pageSize = pageSize == 0 ? 25 : pageSize;
		page  = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<OutboundLog>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(OutboundLog.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(OutboundLog.class)
			.setProjection(Projections.count("id"));
		
		selectCriteria.createAlias("plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		selectCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
		selectCountCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
	
		if (outboundLog.getCreateDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("createDate", outboundLog.getCreateDateFrom()));
			selectCountCriteria.add(Restrictions.ge("createDate", outboundLog.getCreateDateFrom()));
		}
		
		if (outboundLog.getCreateDateTo() != null) {
			selectCriteria.add(Restrictions.le("createDate", outboundLog.getCreateDateTo()));
			selectCountCriteria.add(Restrictions.le("createDate", outboundLog.getCreateDateTo()));
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

		paginatedList.setList(this.outboundLogManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.outboundLogManager.findByCriteria(selectCountCriteria).get(0).toString()));			

		return SUCCESS;
	}
	
	public String export() {		
		DeliveryOrder deliveryOrder = this.deliveryOrderManager.get(doNo);
		deliveryOrder.setIsExport(false);
		this.deliveryOrderManager.save(deliveryOrder);
		
		saveMessage(getText("outboundLog.exported"));
		
		return SUCCESS;
	}
}
