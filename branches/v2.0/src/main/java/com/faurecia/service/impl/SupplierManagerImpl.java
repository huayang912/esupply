package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;
import com.faurecia.service.SupplierManager;

public class SupplierManagerImpl extends GenericManagerImpl<Supplier, String> implements SupplierManager {


	public SupplierManagerImpl(GenericDao<Supplier, String> genericDao) {
		super(genericDao);
	}

	public List<Supplier> getSuppliersByPlant(Plant plant) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plant", plant);
    	return this.genericDao.findByNamedQuery("findSuppliersByPlant", queryParams);
	}
}
