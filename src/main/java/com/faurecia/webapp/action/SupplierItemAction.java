package com.faurecia.webapp.action;

import javax.servlet.http.HttpServletRequest;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.SupplierItem;
import com.faurecia.model.User;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class SupplierItemAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 773756108023411755L;
	private SupplierItemManager supplierItemManager;

	private PaginatedListUtil<SupplierItem> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private SupplierItem supplierItem;
	private Integer id;	

	public void setSupplierItemManager(SupplierItemManager supplierItemManager) {
		this.supplierItemManager = supplierItemManager;
	}
	
	public PaginatedListUtil<SupplierItem> getPaginatedList() {
		return paginatedList;
	}

	public void setPaginatedList(PaginatedListUtil<SupplierItem> paginatedList) {
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

	public SupplierItem getSupplierItem() {
		return supplierItem;
	}

	public void setSupplierItem(SupplierItem supplierItem) {
		this.supplierItem = supplierItem;
	}

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	public String list() {
		if (supplierItem == null) {
			supplierItem = new SupplierItem();
		}
			
		pageSize = pageSize == 0 ? 25 : pageSize;
		page  = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<SupplierItem>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(SupplierItem.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(SupplierItem.class)
			.setProjection(Projections.count("id"));
		
		selectCriteria.createAlias("item", "i");
		selectCriteria.createAlias("i.plant", "p");
		selectCountCriteria.createAlias("item", "i");
		selectCountCriteria.createAlias("i.plant", "p");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		HttpServletRequest request = this.getRequest();

		selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
		selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
		selectCriteria.add(Restrictions.eq("supplier", user.getUserSupplier()));
		selectCountCriteria.add(Restrictions.eq("supplier", user.getUserSupplier()));
		
		if (supplierItem.getItem() != null && supplierItem.getItem().getCode() != null && supplierItem.getItem().getCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("i.code", supplierItem.getItem().getCode().trim()));
			selectCountCriteria.add(Restrictions.like("i.code", supplierItem.getItem().getCode().trim()));
		}
		
		if (supplierItem.getSupplierItemCode() != null && supplierItem.getSupplierItemCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("supplierItemCode", supplierItem.getSupplierItemCode().trim()));
			selectCountCriteria.add(Restrictions.like("supplierItemCode", supplierItem.getSupplierItemCode().trim()));
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

		paginatedList.setList(this.supplierItemManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.supplierItemManager.findByCriteria(selectCountCriteria).get(0).toString()));			

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		supplierItem = this.supplierItemManager.get(this.id);
		return SUCCESS;
	}
	
	public String save() throws Exception {
		SupplierItem newSupplierItem = this.supplierItemManager.get(supplierItem.getId());
		newSupplierItem.setSupplierItemCode(supplierItem.getSupplierItemCode());
		supplierItem = this.supplierItemManager.save(newSupplierItem);
		saveMessage(getText("supplierItem.updated"));
	
		return SUCCESS;
	}
}
