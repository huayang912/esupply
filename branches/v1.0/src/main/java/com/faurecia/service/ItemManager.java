package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Item;
import com.faurecia.model.Plant;


public interface ItemManager extends GenericManager<Item, Integer> {
	List<Item> getItems(Item item);
	
	List<Item> getItemByPlant(Plant plant);
	
	Item getItemByPlantAndItem(Plant plant, Item item);
	
	Item getItemByPlantAndItem(String plantCode, Item item);
	
	Item getItemByPlantAndItem(Plant plant, String itemCode);
	
	Item getItemByPlantAndItem(String plantCode, String itemCode);
}
