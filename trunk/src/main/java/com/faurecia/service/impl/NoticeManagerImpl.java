package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Notice;
import com.faurecia.model.Plant;
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
}
