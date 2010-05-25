package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Plant;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.Supplier;
import com.faurecia.service.ScheduleItemManager;

public class ScheduleItemManagerImpl extends GenericManagerImpl<ScheduleItem, Integer> implements ScheduleItemManager {

	public ScheduleItemManagerImpl(GenericDao<ScheduleItem, Integer> genericDao) {
		super(genericDao);
	}

	public List<ScheduleItem> getLastestScheduleItemByPlantAndSupplier(Plant plant, Supplier supplier) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
    	queryParams.put("plant", plant);
    	queryParams.put("supplier", supplier);
		return this.genericDao.findByNamedQuery("findLastestScheduleItemByPlantAndSupplier", queryParams);
	}

}
