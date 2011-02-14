package com.faurecia.webapp.action;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.Charset;
import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.service.ItemManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.util.CSVWriter;

public class ItemAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 6064722808180238396L;
	private ItemManager itemManager;
	private PlantSupplierManager plantSupplierManager;
	private List<Item> items;
	private Item item;
	private int id;
	private InputStream inputStream;
	private String fileName;

	public void setItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}
	
	public List<Item> getItems() {
		return items;
	}
	
	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}
	
	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public String list() {
		query();
		return SUCCESS;
	}
	
	private void query() {
		if (item != null) {
			DetachedCriteria criteria = DetachedCriteria.forClass(Item.class);
			criteria.createAlias("plant", "p");
			
			if (item.getPlant() != null && item.getPlant().getCode() != null
					&& item.getPlant().getCode().trim().length() != 0) {
				if (item.getPlant().getCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						criteria.add(Restrictions.in("plant", plants));
					} else {
						criteria.add(Restrictions.eq("p.code", "-1"));
					}

				} else {
					criteria.add(Restrictions.eq("p.code", item.getPlant().getCode().trim()));
				}
			}
			
			if (item.getCode() != null && item.getCode().trim().length() != 0)
			{
				criteria.add(Restrictions.eq("code", item.getCode()));
			}
			
			if (item.getDescription() != null && item.getDescription().trim().length() != 0)
			{
				criteria.add(Restrictions.eq("description", item.getDescription()));
			}
			
			items = itemManager.findByCriteria(criteria);
		}
	}

	public void setId(int id) {
		this.id = id;
	}

	public Item getItem() {
		return item;
	}

	public void setItem(Item item) {
		this.item = item;
	}
	
	public String cancel() {
		return CANCEL;
	}

	public String delete() {
		this.itemManager.remove(item.getId());
		this.itemManager.flushSession();
		saveMessage(getText("item.deleted"));

		return SUCCESS;
	}

	public String edit() throws Exception {
		
		if (this.id != 0) {
			item = this.itemManager.get(id);
		}  else {
			item = new Item();
		}
		
		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}

		boolean isNew = (item.getId() == null);

		item = this.itemManager.save(item);

		String key = (isNew) ? "item.added" : "item.updated";
		saveMessage(getText(key));
		this.itemManager.flushSession();
		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
	
	public String export() throws IOException {
		DetachedCriteria criteria = DetachedCriteria.forClass(Item.class);
		criteria.createAlias("plant", "p");
		
		List<Plant> plants = getPlants();
		criteria.add(Restrictions.in("plant", plants));
		
		items = itemManager.findByCriteria(criteria);
		
		if (items != null && items.size() > 0) {
			fileName = "item.csv";
			ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
	
			CSVWriter writer = new CSVWriter(outputStream, ',', Charset.forName("GBK"));
			for(int i = 0; i < items.size(); i++) 
			{
				Item item = items.get(i);
				String[] entries = new String[6];
				
				entries[0] =  item.getCode();
				entries[1] =  item.getPlant().getCode();
				entries[2] =  item.getPlant().getName();
				entries[3] =  item.getDescription();
				entries[4] =  item.getUnitCount() != null ? item.getUnitCount().toPlainString() : "";
				entries[5] =  item.getUom();
			
				writer.writeRecord(entries);
			}
			writer.close();
			inputStream = new ByteArrayInputStream(outputStream.toByteArray());
			
			return SUCCESS;
		}
		
		return INPUT;
	}
}
