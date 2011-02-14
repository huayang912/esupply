package com.faurecia.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;
import org.springframework.security.GrantedAuthority;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserManager;

public class SupplierManagerImpl extends GenericManagerImpl<Supplier, String> implements SupplierManager {

	private UserManager userManager;
	
	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}
	
	public SupplierManagerImpl(GenericDao<Supplier, String> genericDao) {
		super(genericDao);
	}

	public List<Supplier> getSuppliersByPlant(Plant plant) {
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plant", plant);
    	return this.genericDao.findByNamedQuery("findSuppliersByPlant", queryParams);
	}
	
	public List<Supplier> getAuthorizedSupplier(String userCode) {
		User user = this.userManager.getUserByUsername(userCode);
		GrantedAuthority[] authorities = user.getSupplierAuthorities();
		
		if (authorities != null && authorities.length > 0) {
			DetachedCriteria criteria = DetachedCriteria.forClass(Supplier.class);
			String[] plantCodes = new String[authorities.length];
			for(int i = 0; i < authorities.length; i++) {
				plantCodes[i] = authorities[i].getAuthority();
			}
			criteria.add(Restrictions.in("code", plantCodes));
			
			return this.findByCriteria(criteria);
		}
		
		return new ArrayList<Supplier>();
	}
}
