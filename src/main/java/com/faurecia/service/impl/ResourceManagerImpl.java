package com.faurecia.service.impl;

import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;
import org.springframework.jdbc.core.JdbcTemplate;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Resource;
import com.faurecia.service.ResourceManager;

public class ResourceManagerImpl extends GenericManagerImpl<Resource, Integer> implements ResourceManager {

	private JdbcTemplate jdbcTemplate;

	public ResourceManagerImpl(GenericDao<Resource, Integer> genericDao) {
		super(genericDao);
		// TODO Auto-generated constructor stub
	}

	public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
	}

	@SuppressWarnings("unchecked")
	public List<Resource> getResourceByType(String type) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("type", type);
		return this.genericDao.findByNamedQuery("findReourceByType", queryParams);
	}

	public void deleteUserResource(Long userId, Long resourceId) {
		this.jdbcTemplate.execute("delete from user_resource where user_id = " + userId + " and resource_id = " + resourceId);
	}

	public void deleteRoleResource(Long roleId, Long resourceId) {
		this.jdbcTemplate.execute("delete from role_resource where role_id = " + roleId + " and resource_id = " + resourceId);
	}

	public void addUserResource(Long userId, Long resourceId) {
		this.jdbcTemplate
				.execute("insert into user_resource (user_id,resource_id) values ( " + userId + "," + resourceId + ")");
	}

	public void addRoleResource(Long roleId, Long resourceId) {
		this.jdbcTemplate
				.execute("insert into  role_resource (role_id,resource_id) values ( " + roleId + "," + resourceId + ")");
	}
}
