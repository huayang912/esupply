package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Item;
import com.faurecia.model.Plant;
import com.faurecia.service.ItemManager;

public class ItemManagerImpl extends GenericManagerImpl<Item, Integer>
		implements ItemManager {

	public ItemManagerImpl(GenericDao<Item, Integer> genericDao) {
		super(genericDao);
	}

	public List<Item> getItems(Item item) {
		return this.genericDao.findByExample(item);
	}
	
	public List<Item> getItemByPlant(Plant plant) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plant", plant);
		return this.genericDao.findByNamedQuery("findItemByPlant", queryParams);
	}

	public Item getItemByPlantAndItem(Plant plant, Item item) {
		return getItemByPlantAndItem(plant.getCode(), item.getCode());
	}

	public Item getItemByPlantAndItem(String plantCode, Item item) {
		return getItemByPlantAndItem(plantCode, item.getCode());
	}

	public Item getItemByPlantAndItem(Plant plant, String itemCode) {
		return getItemByPlantAndItem(plant.getCode(), itemCode);
	}

	public Item getItemByPlantAndItem(String plantCode, String itemCode) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plantCode", plantCode);
		queryParams.put("itemCode", itemCode);
		List<Item> list = this.genericDao.findByNamedQuery(
				"findItemByPlantAndItem", queryParams);
		if (list != null && list.size() > 0) {
			return list.get(0);
		}

		return null;
	}
}
