package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;
import com.faurecia.service.SupplierManager;

public class SupplierManagerImpl extends UniversalManagerImpl implements SupplierManager {

	GenericDao<Supplier, String> dao;
	
	public GenericDao<Supplier, String> getSupplierDao() {
		return dao;
	}

	public void setSupplierDao(GenericDao<Supplier, String> dao) {
		this.dao = dao;
	}

	public List<Supplier> getSuppliersByPlant(Plant plant) {
		Map<String, Object> queryParam = new HashMap<String, Object>();
    	queryParam.put("plant", plant);
    	return this.dao.findByNamedQuery("findSuppliersByPlant", queryParam);
	}	
}
