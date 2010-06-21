package com.faurecia.service.impl;

import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Notice;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.service.NoticeManager;

public class NoticeManagerImpl extends GenericManagerImpl<Notice, Integer> implements NoticeManager {

	public NoticeManagerImpl(GenericDao<Notice, Integer> genericDao) {
		super(genericDao);
		// TODO Auto-generated constructor stub
	}

	public List<Notice> getNoticeByPlant(Plant plant) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
    	queryParams.put("plant", plant);
		return this.genericDao.findByNamedQuery("findNoticeByPlant", queryParams);
	}
	
	public List<Notice> getNoticeByPlantSupplier(PlantSupplier plantSupplier) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
    	queryParams.put("plantSupplier", plantSupplier);
    	
    	Calendar calendar = Calendar.getInstance();
		calendar.setTime(new Date());
		calendar.set(Calendar.HOUR_OF_DAY, 0);
		calendar.set(Calendar.MINUTE, 0);
		calendar.set(Calendar.SECOND, 0);
		calendar.set(Calendar.MILLISECOND, 0);
		Date dateNow = calendar.getTime();
    	queryParams.put("dateNow", dateNow);
    	
		return this.genericDao.findByNamedQuery("findNoticeByPlantSupplier", queryParams);
	}
}
