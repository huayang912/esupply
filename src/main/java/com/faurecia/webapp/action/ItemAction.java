package com.faurecia.webapp.action;

import java.util.List;

import com.faurecia.model.Item;
import com.faurecia.model.User;
import com.faurecia.service.ItemManager;

public class ItemAction extends BaseAction {

	private static final long serialVersionUID = 1L;
	private ItemManager itemManager;
	private List<Item> items;
	private Item item;
	private int id;

	public void setItemManager(ItemManager itemManager) {
		this.itemManager = itemManager;
	}

	public List<Item> getItems() {
		return items;
	}

	public String list() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		items = itemManager.getItemByPlant(user.getUserPlant());
		return SUCCESS;
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
		if (!"list".equals(from)) {
			return "mainMenu";
		}
		
		return CANCEL;
	}

	public String delete() {
		this.itemManager.remove(item.getId());
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

		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
}
