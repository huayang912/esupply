package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Item;
import com.faurecia.model.Supplier;
import com.faurecia.model.SupplierItem;
import com.faurecia.service.SupplierItemManager;

public class SupplierItemManagerImpl extends
		GenericManagerImpl<SupplierItem, Integer> implements
		SupplierItemManager {

	public SupplierItemManagerImpl(GenericDao<SupplierItem, Integer> genericDao) {
		super(genericDao);
	}

	public List<SupplierItem> getSupplierItemByItem(Item item) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("item", item);
		return this.genericDao.findByNamedQuery("findSupplierItemByItem",
				queryParams);
	}

	public SupplierItem getSupplierItemByItemAndSupplier(Item item,
			Supplier supplier) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("supplier", supplier);
		queryParams.put("item", item);
		List<SupplierItem> list = this.genericDao.findByNamedQuery(
				"findSupplierItemByItemAndSupplier", queryParams);
		if (list != null && list.size() > 0) {
			return list.get(0);
		}

		return null;
	}
}
