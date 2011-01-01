package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

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

	public List<ScheduleItem> getUnconfirmScheduleItemByPlantAndSupplier(Plant plant, Supplier supplier) {
		DetachedCriteria criteria = DetachedCriteria.forClass(ScheduleItem.class);
		criteria.createAlias("schedule", "s");
		criteria.createAlias("s.plantSupplier", "ps");
		
		criteria.add(Restrictions.eq("ps.plant", plant));
		criteria.add(Restrictions.eq("ps.supplier", supplier));
		criteria.add(Restrictions.eq("isConfirm", false));
		
		List<ScheduleItem> scheduleItemList = this.genericDao.findByCriteria(criteria);
		if (scheduleItemList != null && scheduleItemList.size() > 0) {
			for (int i = 0; i < scheduleItemList.size(); i++) {
				if (scheduleItemList.get(i).getScheduleItemDetailList() != null 
						&& scheduleItemList.get(i).getScheduleItemDetailList().size() > 0) {
					
				}
			}
		}
		
		return scheduleItemList;
	}
}
