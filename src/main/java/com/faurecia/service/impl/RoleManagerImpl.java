package com.faurecia.service.impl;

import java.util.List;

import org.springframework.jdbc.core.JdbcTemplate;

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
    private JdbcTemplate jdbcTemplate;

    public void setRoleDao(RoleDao dao) {
        this.dao = dao;
    }

    /**
     * {@inheritDoc}
     */
    public List<Role> getRoles(Role role) {
        return dao.getAll();
    }
    
    public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
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
    
	public void deleteRoleUser(Long roleId, Long userId) {
		this.jdbcTemplate.execute("delete from user_role where role_id = " + roleId + " and user_id = " + userId);
	}

	public void addRoleUser(Long roleId, Long userId) {
		this.jdbcTemplate
				.execute("insert into user_role (user_id,role_id) values ( " + userId + "," + roleId + ")");
	}
}