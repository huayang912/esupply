package com.faurecia.webapp.action;

import java.util.List;

import com.faurecia.model.Role;

public class RoleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	
	private Role role;
	private List<Role> roles;
	private String name;
	
	public Role getRole() {
		return role;
	}
	public void setRole(Role role) {
		this.role = role;
	}
	public List<Role> getRoles() {
		return roles;
	}
	public void setRoles(List<Role> roles) {
		this.roles = roles;
	}
	
	
	public String getName() {
		return name;
	}
	public void setName(String name) {
		this.name = name;
	}
	public String list() {
		roles = this.roleManager.getAll(Role.class);
		return SUCCESS;
	}
	
	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}

		return CANCEL;
	}
	
	public String delete() {
		this.roleManager.remove(Role.class, role.getId());
		saveMessage(getText("role.deleted"));
		return SUCCESS;
	}
	
	public String edit() throws Exception {
		if (this.name != null) {
			role = this.roleManager.getRole(this.name);
		} else {
			role = new Role();
		}

		return SUCCESS;
	}
	
	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}
		
		boolean isNew = (role.getId() == null);
		
		role = (Role)this.roleManager.save(role);

		String key = (isNew) ? "role.added" : "role.updated";
		saveMessage(getText(key));
		
		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
}
