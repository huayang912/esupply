package com.faurecia.webapp.action;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileReader;
import java.io.IOException;
import java.util.List;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;
import com.faurecia.model.User;
import com.faurecia.service.ItemManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.SupplierItemManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.util.CSVReader;
import com.faurecia.webapp.util.PaginatedListUtil;

public class SupplierItemAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 773756108023411755L;
	private ItemManager itemManager;
	private SupplierManager supplierManager;
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

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
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

	public List<Supplier> getSuppliers() {
		if (supplierItem != null && supplierItem.getpCode() != null && !supplierItem.getpCode().equals("-1")) {
			return this.supplierManager.getSuppliersByPlantAndUser(supplierItem.getpCode().trim() + "|" + this.getRequest().getRemoteUser());
		} else {
			return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
		}
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public void setFile(File file) {
		this.file = file;
	}

	public String list() {
		if (supplierItem != null) {
			pageSize = pageSize == 0 ? 25 : pageSize;
			page = page == 0 ? 1 : page;

			paginatedList = new PaginatedListUtil<SupplierItem>();
			paginatedList.setPageNumber(page);
			paginatedList.setObjectsPerPage(pageSize);
			
			DetachedCriteria selectCriteria = DetachedCriteria.forClass(SupplierItem.class);
			DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(SupplierItem.class).setProjection(Projections.count("id"));
			
			selectCriteria.createAlias("supplier", "s");
			selectCriteria.createAlias("item", "i");
			selectCriteria.createAlias("i.plant", "p");
			selectCountCriteria.createAlias("supplier", "s");
			selectCountCriteria.createAlias("item", "i");
			selectCountCriteria.createAlias("i.plant", "p");
			
			if (supplierItem.getpCode() != null && supplierItem.getpCode().trim().length() > 0) {

				if (supplierItem.getpCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						selectCriteria.add(Restrictions.in("i.plant", plants));
						selectCountCriteria.add(Restrictions.in("i.plant", plants));
					} else {
						selectCriteria.add(Restrictions.eq("p.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("p.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("p.code", supplierItem.getpCode().trim()));
					selectCountCriteria.add(Restrictions.eq("p.code", supplierItem.getpCode().trim()));
				}
			}

			if (supplierItem.getsCode() != null && supplierItem.getsCode().trim().length() > 0) {
				if (supplierItem.getsCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null && suppliers.size() > 0) {
						selectCriteria.add(Restrictions.in("supplier", suppliers));
						selectCountCriteria.add(Restrictions.in("supplier", suppliers));
					} else {
						selectCriteria.add(Restrictions.eq("s.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("s.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("s.code", supplierItem.getsCode().trim()));
					selectCountCriteria.add(Restrictions.eq("s.code", supplierItem.getsCode().trim()));
				}
			}

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
		}
		
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

			Item item = this.itemManager.getItemByPlantAndItem(supplierItem.getpCode(), supplierItem.getItem().getCode());

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