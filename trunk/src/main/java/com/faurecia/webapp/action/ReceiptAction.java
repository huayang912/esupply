package com.faurecia.webapp.action;

import javax.servlet.http.HttpServletRequest;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.Receipt;
import com.faurecia.model.User;
import com.faurecia.service.ReceiptManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class ReceiptAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3665261977032180267L;
	private ReceiptManager receiptManager;

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
	
	public String list() {
		if (receipt == null) {
			receipt = new Receipt();
		}
			
		pageSize = pageSize == 0 ? 25 : pageSize;
		page  = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<Receipt>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(Receipt.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(Receipt.class)
			.setProjection(Projections.count("receiptNo"));
		
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
			selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
		}
		
		if (receipt.getReceiptNo() != null && receipt.getReceiptNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("receiptNo", receipt.getReceiptNo().trim()));
			selectCountCriteria.add(Restrictions.like("receiptNo", receipt.getReceiptNo().trim()));
		}
		
		if (receipt.getPostingDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("postingDate", receipt.getPostingDateFrom()));
			selectCountCriteria.add(Restrictions.ge("postingDate", receipt.getPostingDateFrom()));
		}
		
		if (receipt.getPostingDateTo() != null) {
			selectCriteria.add(Restrictions.le("postingDate", receipt.getPostingDateTo()));
			selectCountCriteria.add(Restrictions.le("postingDate", receipt.getPostingDateTo()));
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

		paginatedList.setList(this.receiptManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.receiptManager.findByCriteria(selectCountCriteria).get(0).toString()));			
		
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
