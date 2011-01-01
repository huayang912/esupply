package com.faurecia.service.impl;

import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.OutboundLog;
import com.faurecia.service.OutboundLogManager;

public class OutboundLogManagerImpl extends GenericManagerImpl<OutboundLog, Integer> implements OutboundLogManager {

	public OutboundLogManagerImpl(GenericDao<OutboundLog, Integer> genericDao) {
		super(genericDao);
		// TODO Auto-generated constructor stub
	}

	public OutboundLog getOutboundLogByDoNo(String doNo) {
		DetachedCriteria criteria = DetachedCriteria.forClass(OutboundLog.class);
		
		criteria.add(Restrictions.eq("doNo", doNo));
		
		List<OutboundLog> outboundLogList = this.findByCriteria(criteria);
		
		if (outboundLogList != null && outboundLogList.size() > 0) {
			return outboundLogList.get(0);
		} else {
			return null;
		}
	}
}
