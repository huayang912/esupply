package com.faurecia.webapp.action;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.MatchMode;
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
	public Map<String, String> getOutboundResult()
	{
		Map<String, String> status = new HashMap<String, String>(); 
		status.put("", "All");
		status.put("success", "success");
		status.put("fail", "fail");
		return status;
	}
	public String list() {
		if (outboundLog == null) {
			outboundLog = new OutboundLog();
			
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(new Date());
			calendar.set(Calendar.HOUR_OF_DAY, 0);
			calendar.set(Calendar.MINUTE, 0);
			calendar.set(Calendar.SECOND, 0);
			calendar.set(Calendar.MILLISECOND, 0);
			Date dateNow = calendar.getTime();
			calendar.add(Calendar.DATE, -7);
			Date lastWeek = calendar.getTime();
			
			DateFormat d = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS");
			System.out.println(d.format(lastWeek));
			System.out.println(d.format(dateNow));
			
			outboundLog.setCreateDateFrom(lastWeek);
			outboundLog.setCreateDateTo(dateNow);
			//inboundLog.setInboundResult("fail");
			
			sort = "createDate";
			dir = SortOrderEnum.DESCENDING.toString();
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
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(outboundLog.getCreateDateTo());
			calendar.add(Calendar.DATE, 1);
			selectCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
			selectCountCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
		}
		
		if (outboundLog.getOutboundResult() != null && outboundLog.getOutboundResult().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("outboundResult", outboundLog.getOutboundResult()));
			selectCountCriteria.add(Restrictions.eq("outboundResult", outboundLog.getOutboundResult()));
		}
		
		if (outboundLog.getDoNo() != null && outboundLog.getDoNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("doNo", outboundLog.getDoNo(), MatchMode.ANYWHERE));
			selectCountCriteria.add(Restrictions.like("doNo", outboundLog.getDoNo(), MatchMode.ANYWHERE));
		}
		
		if (outboundLog.getFileName() != null && outboundLog.getFileName().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("fileName", outboundLog.getFileName(), MatchMode.ANYWHERE));
			selectCountCriteria.add(Restrictions.like("fileName", outboundLog.getFileName(), MatchMode.ANYWHERE));
		}
		
		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if ("desc".equals(dir))
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
