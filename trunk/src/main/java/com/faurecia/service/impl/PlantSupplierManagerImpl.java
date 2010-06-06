package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;
import com.faurecia.service.PlantSupplierManager;

public class PlantSupplierManagerImpl extends GenericManagerImpl<PlantSupplier, Integer>
		implements PlantSupplierManager {

	public PlantSupplierManagerImpl(
			GenericDao<PlantSupplier, Integer> genericDao) {
		super(genericDao);
	}

	public PlantSupplier getPlantSupplier(Plant plant, Supplier supplier) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
    	queryParams.put("plant", plant);
    	queryParams.put("supplier", supplier);
		List<PlantSupplier> list = this.genericDao.findByNamedQuery("findPlantSupplierByPlantAndSupplier", queryParams);
		if (list != null && list.size() > 0) {
			return list.get(0);
		}
		
		return null;
	}
	
	public List<PlantSupplier> getPlantSupplierBySupplierCode(String supplierCode) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
    	queryParams.put("supplierCode", supplierCode);
		return this.genericDao.findByNamedQuery("findPlantSupplierBySupplierCode", queryParams);
	}
}
