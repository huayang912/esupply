package com.faurecia.webapp.action;

import java.util.Calendar;
import java.util.List;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.Plant;
import com.faurecia.model.Receipt;
import com.faurecia.model.Supplier;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class ReceiptAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3665261977032180267L;
	private ReceiptManager receiptManager;
	private PlantSupplierManager plantSupplierManager;
	private SupplierManager supplierManager;
	private PaginatedListUtil<Receipt> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private Receipt receipt;
	private String receiptNo;

	public void setReceiptManager(ReceiptManager receiptManager) {
		this.receiptManager = receiptManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public PaginatedListUtil<Receipt> getPaginatedList() {
		return paginatedList;
	}

	public void setPaginatedList(PaginatedListUtil<Receipt> paginatedList) {
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

	public Receipt getReceipt() {
		return receipt;
	}

	public void setReceipt(Receipt receipt) {
		this.receipt = receipt;
	}

	public String getReceiptNo() {
		return receiptNo;
	}

	public void setReceiptNo(String receiptNo) {
		this.receiptNo = receiptNo;
	}

	public List<Supplier> getSuppliers() {
		return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public String list() {
		if (receipt != null) {

			pageSize = pageSize == 0 ? 25 : pageSize;
			page = page == 0 ? 1 : page;

			paginatedList = new PaginatedListUtil<Receipt>();
			paginatedList.setPageNumber(page);
			paginatedList.setObjectsPerPage(pageSize);

			DetachedCriteria selectCriteria = DetachedCriteria.forClass(Receipt.class);
			DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(Receipt.class).setProjection(Projections.count("receiptNo"));

			selectCriteria.createAlias("plantSupplier", "ps");
			selectCriteria.createAlias("ps.plant", "p");
			selectCriteria.createAlias("ps.supplier", "s");
			selectCountCriteria.createAlias("plantSupplier", "ps");
			selectCountCriteria.createAlias("ps.plant", "p");
			selectCountCriteria.createAlias("ps.supplier", "s");

			if (receipt.getpCode() != null && receipt.getpCode().trim().length() > 0) {

				if (receipt.getpCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						selectCriteria.add(Restrictions.in("ps.plant", plants));
						selectCountCriteria.add(Restrictions.in("ps.plant", plants));
					} else {
						selectCriteria.add(Restrictions.eq("p.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("p.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("p.code", receipt.getpCode().trim()));
					selectCountCriteria.add(Restrictions.eq("p.code", receipt.getpCode().trim()));
				}
			}

			if (receipt.getsCode() != null && receipt.getsCode().trim().length() > 0) {
				if (receipt.getsCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null &&suppliers.size() > 0) {
						selectCriteria.add(Restrictions.in("ps.supplier", suppliers));
						selectCountCriteria.add(Restrictions.in("ps.supplier", suppliers));
					} else {
						selectCriteria.add(Restrictions.eq("s.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("s.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("s.code", receipt.getsCode().trim()));
					selectCountCriteria.add(Restrictions.eq("s.code", receipt.getsCode().trim()));
				}
			}

			if (receipt.getReceiptNo() != null && receipt.getReceiptNo().trim().length() > 0) {
				selectCriteria.add(Restrictions.like("receiptNo", receipt.getReceiptNo().trim()));
				selectCountCriteria.add(Restrictions.like("receiptNo", receipt.getReceiptNo().trim()));
			}

			if (receipt.getReferenceReceiptNo() != null && receipt.getReferenceReceiptNo().trim().length() > 0) {
				selectCriteria.add(Restrictions.like("referenceReceiptNo", receipt.getReferenceReceiptNo().trim()));
				selectCountCriteria.add(Restrictions.like("referenceReceiptNo", receipt.getReferenceReceiptNo().trim()));
			}

			if (receipt.getReferenceReceiptNoLong() != null && receipt.getReferenceReceiptNoLong().trim().length() > 0) {
				selectCriteria.add(Restrictions.like("referenceReceiptNoLong", receipt.getReferenceReceiptNoLong().trim()));
				selectCountCriteria.add(Restrictions.like("referenceReceiptNoLong", receipt.getReferenceReceiptNoLong().trim()));
			}

			if (receipt.getPostingDateFrom() != null) {
				selectCriteria.add(Restrictions.ge("postingDate", receipt.getPostingDateFrom()));
				selectCountCriteria.add(Restrictions.ge("postingDate", receipt.getPostingDateFrom()));
			}

			if (receipt.getPostingDateTo() != null) {
				Calendar calendar = Calendar.getInstance();
				calendar.setTime(receipt.getPostingDateTo());
				calendar.add(Calendar.DATE, 1);
				selectCriteria.add(Restrictions.lt("postingDate", calendar.getTime()));
				selectCountCriteria.add(Restrictions.lt("postingDate", calendar.getTime()));
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

			paginatedList.setList(this.receiptManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
			paginatedList.setFullListSize(Integer.parseInt(this.receiptManager.findByCriteria(selectCountCriteria).get(0).toString()));
		}

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		receipt = this.receiptManager.get(this.receiptNo, true);
		return SUCCESS;
	}

}
