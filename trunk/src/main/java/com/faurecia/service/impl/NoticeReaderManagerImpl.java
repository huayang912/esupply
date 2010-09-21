package com.faurecia.service.impl;

import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;
import org.springframework.jdbc.core.JdbcTemplate;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.NoticeReader;
import com.faurecia.service.NoticeReaderManager;

public class NoticeReaderManagerImpl extends GenericManagerImpl<NoticeReader, Integer> implements NoticeReaderManager {

	private JdbcTemplate jdbcTemplate;
	
	public NoticeReaderManagerImpl(GenericDao<NoticeReader, Integer> genericDao) {
		super(genericDao);
		// TODO Auto-generated constructor stub
	}

	public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
	}
	
	public List<NoticeReader> getNoticeReaderByNoticeId(Integer noticeId) {
		DetachedCriteria detachedCriteria = DetachedCriteria.forClass(NoticeReader.class);
		detachedCriteria.createAlias("notice", "n");
		detachedCriteria.add(Restrictions.eq("n.id", noticeId));
		
		return this.findByCriteria(detachedCriteria);
	}
	
	public List<NoticeReader> getReadNoticeReaderByNoticeId(Integer noticeId) {
		DetachedCriteria detachedCriteria = DetachedCriteria.forClass(NoticeReader.class);
		detachedCriteria.createAlias("notice", "n");
		detachedCriteria.add(Restrictions.eq("n.id", noticeId));
		detachedCriteria.add(Restrictions.eq("isRead", true));
		
		return this.findByCriteria(detachedCriteria);
	}
	
	public NoticeReader getNoticeReaderByNoticeIdAndPlantSupplierId(Integer noticeId, Integer plantSupplierId) {
		DetachedCriteria detachedCriteria = DetachedCriteria.forClass(NoticeReader.class);
		detachedCriteria.createAlias("notice", "n");
		detachedCriteria.createAlias("plantSupplier", "p");
		detachedCriteria.add(Restrictions.eq("n.id", noticeId));
		detachedCriteria.add(Restrictions.eq("p.id", plantSupplierId));
		
		List<NoticeReader> list = this.findByCriteria(detachedCriteria);
		if (list != null && list.size() > 0) {
			return list.get(0);
		}
		else
		{
			return null;
		}
	}
	
	public void deleteNoticeReaderByNoticeId(Integer noticeId) {
		this.jdbcTemplate.execute("delete from notice_reader where notice_id = " + noticeId);
	}
}
