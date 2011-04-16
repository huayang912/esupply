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
		queryParams.put("plantCode", plant.getCode());
		return this.genericDao.findByNamedQuery("findSuppliersByPlant", queryParams);
	}
	
	public List<Supplier> getSuppliersByPlantAndUser(String plantCodeAnduserCode) {
		String[] s = plantCodeAnduserCode.split("\\|");
		String plantCode = s[0];
		String userCode = s[1];
		Map<String, Object> queryParams = new HashMap<String, Object>();
		queryParams.put("plantCode", plantCode);
		List<Supplier> suppliers = this.genericDao.findByNamedQuery("findSuppliersByPlant", queryParams);
		
		if (Constants.SUPER_USER.equalsIgnoreCase(userCode)) {
			return suppliers;
		}
		
		User user = this.userManager.getUserByUsername(userCode);
		GrantedAuthority[] authorities = user.getSupplierAuthorities();
		
		if (authorities != null && authorities.length > 0
				&& suppliers != null && suppliers.size() > 0) {
			
			List<Supplier> returnSuppliers = new ArrayList<Supplier>();
			for(Supplier supplier : suppliers) {
				for (GrantedAuthority authority : authorities) {
					if (supplier.getCode().equals(authority.getAuthority())) {
						returnSuppliers.add(supplier);
						break;
					}
				}
			}
			
			return returnSuppliers;
		}
		
		return null;
	}

	public List<Supplier> getAuthorizedSupplier(String userCode) {
		if (userCode.equalsIgnoreCase(Constants.SUPER_USER)) {
			return this.getAll();
		} else {
			User user = this.userManager.getUserByUsername(userCode);
			GrantedAuthority[] authorities = user.getSupplierAuthorities();

			if (authorities != null && authorities.length > 0) {
				DetachedCriteria criteria = DetachedCriteria.forClass(Supplier.class);
				String[] supplierCodes = new String[authorities.length];
				for (int i = 0; i < authorities.length; i++) {
					supplierCodes[i] = authorities[i].getAuthority();
				}
				criteria.add(Restrictions.in("code", supplierCodes));

				return this.findByCriteria(criteria);
			}
		}

		return new ArrayList<Supplier>();
	}
}
