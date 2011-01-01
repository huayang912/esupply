package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.InboundLog;
import com.faurecia.service.InboundLogManager;

public class InboundLogManagerImpl extends GenericManagerImpl<InboundLog, Integer> implements InboundLogManager {

	public InboundLogManagerImpl(GenericDao<InboundLog, Integer> genericDao) {
		super(genericDao);
	}

	public InboundLog getInboundLogByDataTypeAndFileName(String dataType, String fileName) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("dataType", dataType);
		queryParams.put("fileName", fileName);
		List<InboundLog> list = this.genericDao.findByNamedQuery(
				"findInboundLogByDataTypeAndFileName", queryParams);
		if (list != null && list.size() > 0) {
			return list.get(0);
		}

		return null;
	}
}
