package com.faurecia.service.impl;

import java.util.List;

import org.hibernate.Criteria;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Item;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.ScheduleControl;
import com.faurecia.service.ScheduleControlManager;

public class ScheduleControlManagerImpl extends GenericManagerImpl<ScheduleControl, Integer> implements ScheduleControlManager {

	public ScheduleControlManagerImpl(GenericDao<ScheduleControl, Integer> genericDao) {
		super(genericDao);
		// TODO Auto-generated constructor stub
	}

	public ScheduleControl get(String scheduleNo, PlantSupplier plantSupplier, Item item) {
		DetachedCriteria criteria = DetachedCriteria.forClass(ScheduleControl.class);
		
		criteria.add(Restrictions.eq("scheduleNo", scheduleNo));
		criteria.add(Restrictions.eq("plantSupplier", plantSupplier));
		criteria.add(Restrictions.eq("item", item));
		
		List<Criteria> criteriaList = this.genericDao.findByCriteria(criteria);
		
		if (criteriaList != null && criteriaList.size() > 0) {
			return (ScheduleControl)criteriaList.get(0);
		}
		else
		{
			return null;
		}
	}
}
