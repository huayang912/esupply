package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Item;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;

public interface SupplierItemManager extends
		GenericManager<SupplierItem, Integer> {
	SupplierItem getSupplierItemByItemAndSupplier(Item item, Supplier supplier);
	
	List<SupplierItem> getSupplierItemByItem(Item item);
}
