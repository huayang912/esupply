package com.faurecia.service.impl;

import java.util.List;

import com.faurecia.dao.RoleDao;
import com.faurecia.model.Role;
import com.faurecia.model.User;
import com.faurecia.service.RoleManager;

/**
 * Implementation of RoleManager interface.
 * 
 * @author <a href="mailto:dan@getrolling.com">Dan Kibler</a>
 */
public class RoleManagerImpl extends UniversalManagerImpl implements RoleManager {
    private RoleDao dao;

    public void setRoleDao(RoleDao dao) {
        this.dao = dao;
    }

    /**
     * {@inheritDoc}
     */
    public List<Role> getRoles(Role role) {
        return dao.getAll();
    }

    /**
     * {@inheritDoc}
     */
    public Role getRole(String rolename) {
        return dao.getRoleByName(rolename);
    }
    
    public Role getRole(String rolename,boolean includeResources) {
		Role role = dao.getRoleByName(rolename);
		 if (includeResources && role.getResources() != null && role.getResources().size() > 0)
		 {
		 }
		return role;
	}
    
    /**
     * {@inheritDoc}
     */
    public Role saveRole(Role role) {
        return dao.save(role);
    }

    /**
     * {@inheritDoc}
     */
    public void removeRole(String rolename) {
        dao.removeRole(rolename);
    }
}