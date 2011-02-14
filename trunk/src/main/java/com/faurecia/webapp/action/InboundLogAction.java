package com.faurecia.webapp.action;

import java.io.File;
import java.io.FileInputStream;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.Map;

import org.apache.commons.io.FileUtils;
import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.MatchMode;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.InboundLog;
import com.faurecia.service.InboundLogManager;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class InboundLogAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7108954954452920378L;

	private InboundLogManager inboundLogManager;
	private PurchaseOrderManager purchaseOrderManager;
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

	public void setPurchaseOrderManager(PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
	}

	public void setReceiptManager(ReceiptManager receiptManager) {
		this.receiptManager = receiptManager;
	}

	public Map<String, String> getInboundResult() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("success", "success");
		status.put("fail", "fail");
		return status;
	}

	public Map<String, String> getDataType() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("ORDERS", "ORDERS");
		status.put("DELINS", "DELINS");
		status.put("MBGMCR", "MBGMCR");
		status.put("SEQJIT", "SEQJIT");
		return status;
	}

	public String list() {
		if (inboundLog == null) {
			inboundLog = new InboundLog();

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

			inboundLog.setCreateDateFrom(lastWeek);
			inboundLog.setCreateDateTo(dateNow);
			// inboundLog.setInboundResult("fail");

			sort = "createDate";
			dir = SortOrderEnum.DESCENDING.toString();
		}

		pageSize = pageSize == 0 ? 25 : pageSize;
		page = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<InboundLog>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(InboundLog.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(InboundLog.class).setProjection(Projections.count("id"));

		selectCriteria.createAlias("plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");

		if (inboundLog.getPlantCode() != null && inboundLog.getPlantCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("p.code", inboundLog.getPlantCode().trim()));
			selectCountCriteria.add(Restrictions.eq("p.code", inboundLog.getPlantCode().trim()));
		}

		if (inboundLog.getSupplierCode() != null && inboundLog.getSupplierCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("s.code", inboundLog.getSupplierCode().trim()));
			selectCountCriteria.add(Restrictions.eq("s.code", inboundLog.getSupplierCode().trim()));
		}

		if (inboundLog.getCreateDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("createDate", inboundLog.getCreateDateFrom()));
			selectCountCriteria.add(Restrictions.ge("createDate", inboundLog.getCreateDateFrom()));
		}

		if (inboundLog.getCreateDateTo() != null) {
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(inboundLog.getCreateDateTo());
			calendar.add(Calendar.DATE, 1);
			selectCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
			selectCountCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
		}

		if (inboundLog.getDataType() != null && inboundLog.getDataType().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("dataType", inboundLog.getDataType()));
			selectCountCriteria.add(Restrictions.eq("dataType", inboundLog.getDataType()));
		}

		if (inboundLog.getInboundResult() != null && inboundLog.getInboundResult().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("inboundResult", inboundLog.getInboundResult()));
			selectCountCriteria.add(Restrictions.eq("inboundResult", inboundLog.getInboundResult()));
		}

		if (inboundLog.getFileName() != null && inboundLog.getFileName().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("inboundResult", inboundLog.getFileName(), MatchMode.ANYWHERE));
			selectCountCriteria.add(Restrictions.like("inboundResult", inboundLog.getFileName(), MatchMode.ANYWHERE));
		}

		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if ("desc".equals(dir)) {
				selectCriteria.addOrder(Order.desc(sort));
				paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
			} else {
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

			String archiveFolder = inboundLog.getPlantSupplier().getPlant().getArchiveFileDirectory() + File.separator
					+ inboundLog.getPlantSupplier().getPlant().getCode() + File.separator + inboundLog.getDataType();

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
		} else {
			saveMessage(getText("inboundLog.notimported"));
		}

		return SUCCESS;
	}
}
