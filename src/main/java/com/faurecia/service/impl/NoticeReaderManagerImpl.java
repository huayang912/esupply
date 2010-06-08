package com.faurecia.service.impl;

import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.NoticeReader;
import com.faurecia.service.NoticeReaderManager;

public class NoticeReaderManagerImpl extends GenericManagerImpl<NoticeReader, Integer> implements NoticeReaderManager {

	public NoticeReaderManagerImpl(GenericDao<NoticeReader, Integer> genericDao) {
		super(genericDao);
		// TODO Auto-generated constructor stub
	}

	public List<NoticeReader> getNoticeReaderByNoticeId(Integer noticeId) {
		DetachedCriteria detachedCriteria = DetachedCriteria.forClass(NoticeReader.class);
		detachedCriteria.createAlias("Notice", "n");
		detachedCriteria.add(Restrictions.eq("n.id", noticeId));
		
		return this.findByCriteria(detachedCriteria);
	}
}
