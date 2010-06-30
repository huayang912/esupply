package com.faurecia.webapp.action;

import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.User;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class PurchaseOrderAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = 3847028744873885262L;
	private PurchaseOrderManager purchaseOrderManager;
	private PlantSupplierManager plantSupplierManager;
	private PaginatedListUtil<PurchaseOrder> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private PurchaseOrder purchaseOrder;
	private String poNo;

	public void setPurchaseOrderManager(PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
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

	public Map<String, String> getStatus() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("Open", "Open");
		status.put("Close", "Close");
		return status;
	}

	public PurchaseOrder getPurchaseOrder() {
		return purchaseOrder;
	}

	public void setPurchaseOrder(PurchaseOrder purchaseOrder) {
		this.purchaseOrder = purchaseOrder;
	}

	public String getPoNo() {
		return poNo;
	}

	public void setPoNo(String poNo) {
		this.poNo = poNo;
	}

	public List<PlantSupplier> getSuppliers() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		return this.plantSupplierManager.getPlantSupplierByUserId(user.getId());
	}

	public String list() {
		if (purchaseOrder == null) {
			purchaseOrder = new PurchaseOrder();

			Calendar calendar = Calendar.getInstance();
			calendar.setTime(new Date());
			calendar.set(Calendar.HOUR_OF_DAY, 0);
			calendar.set(Calendar.MINUTE, 0);
			calendar.set(Calendar.SECOND, 0);
			calendar.set(Calendar.MILLISECOND, 0);
			Date dateNow = calendar.getTime();
			calendar.add(Calendar.MONTH, -1);
			Date lastWeek = calendar.getTime();

			DateFormat d = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS");

			purchaseOrder.setCreateDateFrom(lastWeek);
			purchaseOrder.setCreateDateTo(dateNow);

			List<PlantSupplier> supplierList = getSuppliers();
			if (supplierList != null && supplierList.size() > 0) {
				purchaseOrder.setPlantSupplier(supplierList.get(0));
			}

			sort = "poNo";
			dir = SortOrderEnum.DESCENDING.toString();
		}

		pageSize = pageSize == 0 ? 25 : pageSize;
		page = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<PurchaseOrder>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(PurchaseOrder.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(PurchaseOrder.class).setProjection(Projections.count("poNo"));

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
			selectCriteria.add(Restrictions.eq("ps.responsibleUser", user));
			selectCountCriteria.add(Restrictions.eq("ps.responsibleUser", user));
		} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
			if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
				return "mainMenu";
			}

			selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
		}

		if (purchaseOrder.getPoNo() != null && purchaseOrder.getPoNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("poNo", purchaseOrder.getPoNo().trim()));
			selectCountCriteria.add(Restrictions.like("poNo", purchaseOrder.getPoNo().trim()));
		}

		if (purchaseOrder.getStatus() != null && purchaseOrder.getStatus().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("status", purchaseOrder.getStatus()));
			selectCountCriteria.add(Restrictions.eq("status", purchaseOrder.getStatus()));
		}

		if (purchaseOrder.getCreateDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("createDate", purchaseOrder.getCreateDateFrom()));
			selectCountCriteria.add(Restrictions.ge("createDate", purchaseOrder.getCreateDateFrom()));
		}

		if (purchaseOrder.getCreateDateTo() != null) {
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(purchaseOrder.getCreateDateTo());
			calendar.add(Calendar.DATE, 1);
			selectCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
			selectCountCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
		}

		if (purchaseOrder.getPlantSupplier() != null) {
			selectCriteria.add(Restrictions.eq("plantSupplier", purchaseOrder.getPlantSupplier()));
			selectCountCriteria.add(Restrictions.eq("plantSupplier", purchaseOrder.getPlantSupplier()));
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

		paginatedList.setList(this.purchaseOrderManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.purchaseOrderManager.findByCriteria(selectCountCriteria).get(0).toString()));

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		purchaseOrder = this.purchaseOrderManager.get(this.poNo, true);
		return SUCCESS;
	}
}
