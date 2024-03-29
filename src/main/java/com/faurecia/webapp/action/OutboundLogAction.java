package com.faurecia.webapp.action;

import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.MatchMode;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.OutboundLog;
import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.OutboundLogManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class OutboundLogAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -5301155045082867465L;

	private OutboundLogManager outboundLogManager;
	private DeliveryOrderManager deliveryOrderManager;
	private PaginatedListUtil<OutboundLog> paginatedList;
	private PlantSupplierManager plantSupplierManager;
	private SupplierManager supplierManager;
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

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
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

	public List<Supplier> getSuppliers() {
		if (outboundLog != null && outboundLog.getPlantCode() != null && !outboundLog.getPlantCode().equals("-1")) {
			return this.supplierManager.getSuppliersByPlantAndUser(outboundLog.getPlantCode().trim() + "|" + this.getRequest().getRemoteUser());
		} else {
			return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
		}
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public Map<String, String> getOutboundResult() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("success", "success");
		status.put("fail", "fail");
		return status;
	}

	public String list() {
		if (outboundLog != null) {

			pageSize = pageSize == 0 ? 25 : pageSize;
			page = page == 0 ? 1 : page;

			paginatedList = new PaginatedListUtil<OutboundLog>();
			paginatedList.setPageNumber(page);
			paginatedList.setObjectsPerPage(pageSize);

			DetachedCriteria selectCriteria = DetachedCriteria.forClass(OutboundLog.class);
			DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(OutboundLog.class).setProjection(Projections.count("id"));

			selectCriteria.createAlias("plantSupplier", "ps");
			selectCriteria.createAlias("ps.plant", "p");
			selectCriteria.createAlias("ps.supplier", "s");
			selectCountCriteria.createAlias("plantSupplier", "ps");
			selectCountCriteria.createAlias("ps.plant", "p");
			selectCountCriteria.createAlias("ps.supplier", "s");

			if (outboundLog.getPlantCode() != null && outboundLog.getPlantCode().trim().length() > 0) {

				if (outboundLog.getPlantCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						selectCriteria.add(Restrictions.or(Restrictions.in("ps.plant", plants), Restrictions.isNull("ps.plant")));
						selectCountCriteria.add(Restrictions.or(Restrictions.in("ps.plant", plants), Restrictions.isNull("ps.plant")));
					} else {
						selectCriteria.add(Restrictions.eq("p.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("p.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("p.code", outboundLog.getPlantCode().trim()));
					selectCountCriteria.add(Restrictions.eq("p.code", outboundLog.getPlantCode().trim()));
				}
			}

			if (outboundLog.getSupplierCode() != null && outboundLog.getSupplierCode().trim().length() > 0) {
				if (outboundLog.getSupplierCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null &&suppliers.size() > 0) {
						selectCriteria.add(Restrictions.or(Restrictions.in("ps.supplier", suppliers), Restrictions.isNull("ps.supplier")));
						selectCountCriteria.add(Restrictions.or(Restrictions.in("ps.supplier", suppliers), Restrictions.isNull("ps.supplier")));
					} else {
						selectCriteria.add(Restrictions.eq("s.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("s.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("s.code", outboundLog.getSupplierCode().trim()));
					selectCountCriteria.add(Restrictions.eq("s.code", outboundLog.getSupplierCode().trim()));
				}
			}

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
				if ("desc".equals(dir)) {
					selectCriteria.addOrder(Order.desc(sort));
					paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
				} else {
					selectCriteria.addOrder(Order.asc(sort));
					paginatedList.setSortDirection(SortOrderEnum.ASCENDING);
				}
			}

			paginatedList.setList(this.outboundLogManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
			paginatedList.setFullListSize(Integer.parseInt(this.outboundLogManager.findByCriteria(selectCountCriteria).get(0).toString()));
		}
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
