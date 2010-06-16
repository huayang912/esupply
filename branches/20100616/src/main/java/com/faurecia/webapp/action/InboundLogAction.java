package com.faurecia.webapp.action;

import java.io.File;
import java.io.FileInputStream;
import java.util.Date;

import org.apache.commons.io.FileUtils;
import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.InboundLog;
import com.faurecia.model.User;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.PurchaseOrderApprovalManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class InboundLogAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7108954954452920378L;

	private InboundLogManager inboundLogManager;
	private PurchaseOrderApprovalManager purchaseOrderManager;
	private ScheduleManager scheduleManager;
	private ReceiptManager receiptManager;
	private PaginatedListUtil<InboundLog> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private InboundLog inboundLog;
	private int id;

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
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public void setPurchaseOrderApprovalManager(PurchaseOrderApprovalManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}
	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
	}
	public void setReceiptManager(ReceiptManager receiptManager) {
		this.receiptManager = receiptManager;
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
	
	public String reimport() {
		inboundLog = this.inboundLogManager.get(id);
		
		if (inboundLog.getInboundResult().equals("fail") && inboundLog.getFullFilePath() != null && inboundLog.getFullFilePath().trim().length() > 0) {
			
			String archiveFolder = inboundLog.getPlantSupplier().getPlant().getArchiveFileDirectory() 
				+ File.separator + inboundLog.getPlantSupplier().getPlant().getCode() + File.separator + inboundLog.getDataType();
			
			try {
				File file = new File(inboundLog.getFullFilePath());
				FileInputStream stream = new FileInputStream(file);
				
				if (inboundLog.getDataType().equals("ORDERS")) {
					this.purchaseOrderManager.saveSingleFile(stream, inboundLog);
				} else if (inboundLog.getDataType().equals("DELINS")) {
					this.scheduleManager.saveSingleFile(stream, inboundLog);
				} else if (inboundLog.getDataType().equals("MBGMCR")) {
					this.receiptManager.saveSingleFile(stream, inboundLog);
				}
				
				FileUtils.forceMkdir(new File(archiveFolder));
				File backupFile = new File(archiveFolder + File.separator + file.getName());
				
				FileUtils.copyFile(file, backupFile);
				inboundLog.setFullFilePath(backupFile.getAbsolutePath());
				inboundLog.setInboundResult("success");
				
				FileUtils.forceDelete(file);
				inboundLog.setMemo("");
			} catch (Exception ex) {
				inboundLog.setMemo(ex.getMessage());
			} finally {
				inboundLog.setLastModifyDate(new Date());
				inboundLog.setLastModifyUser(this.getRequest().getRemoteUser());
				this.inboundLogManager.save(inboundLog);
			}
			
			saveMessage(getText("inboundLog.imported"));
		}
		else
		{
			saveMessage(getText("inboundLog.notimported"));
		}
		
		return SUCCESS;
	}
}
