package com.faurecia.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;
import org.springframework.security.GrantedAuthority;

import com.faurecia.Constants;
import com.faurecia.dao.GenericDao;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.UserManager;

public class PlantSupplierManagerImpl extends GenericManagerImpl<PlantSupplier, Integer> implements PlantSupplierManager {

	private UserManager userManager;

	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}

	public PlantSupplierManagerImpl(GenericDao<PlantSupplier, Integer> genericDao) {
		super(genericDao);
	}

	public List<Plant> getAuthorizedPlant(String userCode) {
		if (userCode.equalsIgnoreCase(Constants.SUPER_USER)) {
			DetachedCriteria criteria = DetachedCriteria.forClass(Plant.class);
			return this.findByCriteria(criteria);
		} else {
			User user = this.userManager.getUserByUsername(userCode);
			GrantedAuthority[] authorities = user.getPlantAuthorities();

			if (authorities != null && authorities.length > 0) {
				DetachedCriteria criteria = DetachedCriteria.forClass(Plant.class);
				String[] plantCodes = new String[authorities.length];
				for (int i = 0; i < authorities.length; i++) {
					plantCodes[i] = authorities[i].getAuthority();
				}
				criteria.add(Restrictions.in("code", plantCodes));

				return this.findByCriteria(criteria);
			}
		}

		return new ArrayList<Plant>();
	}

	public PlantSupplier getPlantSupplier(Plant plant, Supplier supplier) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plant", plant);
		queryParams.put("supplier", supplier);
		List<PlantSupplier> list = this.genericDao.findByNamedQuery("findPlantSupplierByPlantAndSupplier", queryParams);
		if (list != null && list.size() > 0) {
			return list.get(0);
		}

		return null;
	}

	public List<PlantSupplier> getPlantSupplierBySupplierCode(String supplierCode) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("supplierCode", supplierCode);
		return this.genericDao.findByNamedQuery("findPlantSupplierBySupplierCode", queryParams);
	}

	public List<PlantSupplier> getPlantSupplierByPlantCode(String plantCode) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plantCode", plantCode);
		return this.genericDao.findByNamedQuery("findPlantSupplierByPlantCode", queryParams);
	}

	public List<PlantSupplier> getPlantSupplierByPlantScheduleGroupId(Integer plantScheduleGroupId) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plantScheduleGroupId", plantScheduleGroupId);
		return this.genericDao.findByNamedQuery("findPlantSupplierByPlantScheduleGroupId", queryParams);
	}

	public List<PlantSupplier> getPlantSupplierByUserId(Long userId) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("userId", userId);
		return this.genericDao.findByNamedQuery("findPlantSupplierByUserId", queryParams);
	}
}
