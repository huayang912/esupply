package com.faurecia.webapp.action;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.List;

import javax.servlet.http.HttpServletRequest;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;
import com.faurecia.model.User;
import com.faurecia.service.ItemManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.util.CSVReader;
import com.faurecia.webapp.util.PaginatedListUtil;

public class SupplierItemAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 773756108023411755L;
	private ItemManager itemManager;
	private SupplierItemManager supplierItemManager;
	private PlantSupplierManager plantSupplierManager;
	private PaginatedListUtil<SupplierItem> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private SupplierItem supplierItem;
	private Integer id;
	private File file;

	public void setItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

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

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public List<PlantSupplier> getSuppliers() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		return this.plantSupplierManager.getPlantSupplierByUserId(user.getId());
	}

	public void setFile(File file) {
		this.file = file;
	}

	public String list() {
		if (supplierItem == null) {
			supplierItem = new SupplierItem();

			if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
				List<PlantSupplier> supplierList = getSuppliers();
				if (supplierList != null && supplierList.size() > 0) {
					supplierItem.setSupplier(supplierList.get(0).getSupplier());
				}
			}
		}

		pageSize = pageSize == 0 ? 25 : pageSize;
		page = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<SupplierItem>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(SupplierItem.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(SupplierItem.class).setProjection(Projections.count("id"));

		selectCriteria.createAlias("item", "i");
		selectCriteria.createAlias("i.plant", "p");
		selectCountCriteria.createAlias("item", "i");
		selectCountCriteria.createAlias("i.plant", "p");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		if (this.getRequest().isUserInRole(Constants.PLANT_USER_ROLE)) {
			selectCriteria.add(Restrictions.eq("i.plant", user.getUserPlant()));
			selectCountCriteria.add(Restrictions.eq("i.plant", user.getUserPlant()));
		} else if (this.getRequest().isUserInRole(Constants.SUPPLIER_PLANT_CODE)) {
			selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCriteria.add(Restrictions.eq("supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("supplier", user.getUserSupplier()));
		}

		if (supplierItem.getItem() != null && supplierItem.getItem().getCode() != null && supplierItem.getItem().getCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("i.code", supplierItem.getItem().getCode().trim()));
			selectCountCriteria.add(Restrictions.like("i.code", supplierItem.getItem().getCode().trim()));
		}

		if (supplierItem.getSupplierItemCode() != null && supplierItem.getSupplierItemCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("supplierItemCode", supplierItem.getSupplierItemCode().trim()));
			selectCountCriteria.add(Restrictions.like("supplierItemCode", supplierItem.getSupplierItemCode().trim()));
		}

		if (supplierItem.getSupplier() != null) {
			selectCriteria.add(Restrictions.like("supplier", supplierItem.getSupplier()));
			selectCountCriteria.add(Restrictions.like("supplier", supplierItem.getSupplier()));
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

		paginatedList.setList(this.supplierItemManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.supplierItemManager.findByCriteria(selectCountCriteria).get(0).toString()));

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		if (this.id != null) {
			supplierItem = this.supplierItemManager.get(this.id);
		} else {
			supplierItem = new SupplierItem();
		}

		return SUCCESS;
	}

	public String delete() {
		this.supplierItemManager.remove(supplierItem.getId());
		saveMessage(getText("supplierItem.deleted"));
		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}

		boolean isNew = (supplierItem.getId() == null);

		if (isNew) {

			Item item = null;
			if (this.getRequest().isUserInRole(Constants.PLANT_USER_ROLE)) {
				String userCode = this.getRequest().getRemoteUser();
				User user = this.userManager.getUserByUsername(userCode);
				item = this.itemManager.getItemByPlantAndItem(user.getUserPlant(), supplierItem.getItem().getCode());
			} else {
				String plantCode = (String) this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE);
				item = this.itemManager.getItemByPlantAndItem(plantCode, supplierItem.getItem().getCode());
			}

			if (item == null) {
				saveMessage(getText("errors.supplierItem.itemCodeNotExist"));
				return INPUT;
			} else {
				supplierItem.setItem(item);
			}

			supplierItem = this.supplierItemManager.save(supplierItem);
			saveMessage(getText("supplierItem.added"));
		} else {
			SupplierItem newSupplierItem = this.supplierItemManager.get(supplierItem.getId());
			newSupplierItem.setSupplierItemCode(supplierItem.getSupplierItemCode());
			supplierItem = this.supplierItemManager.save(newSupplierItem);
			saveMessage(getText("supplierItem.updated"));
		}

		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}

	public String importFile() {
		return SUCCESS;
	}

	public String upload() {
		try {
			Supplier supplier = supplierItem.getSupplier();
			String userCode = this.getRequest().getRemoteUser();
			User user = this.userManager.getUserByUsername(userCode);

			if (file == null) {
				saveMessage(getText("error.supplierItem.uploadFileIsEmpty"));
				return INPUT;
			}

			FileReader reader = new FileReader(file);

			CSVReader csvReader = new CSVReader(reader);

			String[] content = csvReader.readNext();
			int i = 1;
			while (content != null) {
				String itemCode = content[0];
				String supplierItemCode = content[1];

				Item item = this.itemManager.getItemByPlantAndItem(user.getUserPlant(), itemCode);

				if (item == null) {
					String[] args = new String[] { String.valueOf(i), itemCode };
					saveMessage(getText("error.supplierItem.itemCodeNotExist", args));
					return INPUT;
				}

				SupplierItem si = this.supplierItemManager.getSupplierItemByItemAndSupplier(item, supplier);
				if (si == null) {
					si = new SupplierItem();
					si.setItem(item);
					si.setSupplier(supplier);
				}
				si.setSupplierItemCode(supplierItemCode);

				this.supplierItemManager.save(si);

				content = csvReader.readNext();
				i++;
			}
		} catch (FileNotFoundException e) {
			saveMessage(getText("error.supplierItem.uploadFileIsEmpty"));
			return INPUT;
		} catch (IOException e) {
			saveMessage(getText("error.supplierItem.uploadFileIsDamaged"));
			return INPUT;
		}

		return SUCCESS;
	}
}
