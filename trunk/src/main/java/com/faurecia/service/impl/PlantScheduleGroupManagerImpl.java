package com.faurecia.service.impl;

import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;
import org.springframework.jdbc.core.JdbcTemplate;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.service.PlantScheduleGroupManager;

public class PlantScheduleGroupManagerImpl extends GenericManagerImpl<PlantScheduleGroup, Integer> implements PlantScheduleGroupManager {

	private JdbcTemplate jdbcTemplate;
	
	public PlantScheduleGroupManagerImpl(GenericDao<PlantScheduleGroup, Integer> genericDao) {
		super(genericDao);
	}

	public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
	}

	public PlantScheduleGroup getDefaultPlantScheduleGroupByPlantCode(String plantCode) {
		DetachedCriteria criteria = DetachedCriteria.forClass(PlantScheduleGroup.class);
		criteria.createAlias("plant", "p");
		
		criteria.add(Restrictions.eq("p.code", plantCode));
		criteria.add(Restrictions.eq("isDefault", true));
		
		List<PlantScheduleGroup> plantScheduleGroupList = this.findByCriteria(criteria);
		
		if (plantScheduleGroupList != null && plantScheduleGroupList.size() > 0) {
			return plantScheduleGroupList.get(0);
		} else {
			return null;
		}
	}

	public List<PlantScheduleGroup> getPlantScheduleGroupByPlantCode(String plantCode) {
		DetachedCriteria criteria = DetachedCriteria.forClass(PlantScheduleGroup.class);
		criteria.createAlias("plant", "p");
		criteria.add(Restrictions.eq("p.code", plantCode));
		
		return this.findByCriteria(criteria);
	}
	
	public void cleanPlantScheduleGroupOfRelatedPlantSupplier(PlantScheduleGroup plantScheduleGroup) {
		String sql = "update plant_supplier set plant_schedule_group_id = null where plant_schedule_group_id = " + plantScheduleGroup.getId();
		this.jdbcTemplate.execute(sql);
	}
}
