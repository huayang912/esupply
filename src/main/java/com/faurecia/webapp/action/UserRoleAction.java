package com.faurecia.webapp.action;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import com.faurecia.model.LabelValue;
import com.faurecia.model.Resource;
import com.faurecia.model.Role;
import com.faurecia.model.User;
import com.faurecia.service.ResourceManager;
import com.faurecia.service.RoleManager;
import com.faurecia.service.UserManager;

public class UserRoleAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7684323679522586901L;
	private List<Role> roles;
	private List<User> roleUsers;
	private UserManager userManager;
	private RoleManager roleManager;
	private Role role;
	private String name;

	private List<LabelValue> availableUsers;

	public void setName(String name) {
		this.name = name;
	}

	public Role getRole() {
		return role;
	}

	public void setRole(Role role) {
		this.role = role;
	}

	public List<Role> getRoles() {
		return roles;
	}

	public List<User> getRoleUsers() {
		return roleUsers;
	}

	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}

	public void setRoleManager(RoleManager roleManager) {
		this.roleManager = roleManager;
	}

	public List<LabelValue> getAvailableUsers() {
		return availableUsers;
	}

	public void setAvailableUsers(List<LabelValue> availableUsers) {
		this.availableUsers = availableUsers;
	}

	public String list() {
		if (role != null) {
			role = this.roleManager.getRole(role.getName(), true);
			prepareResourceList();
		} else {
			if (getRequest().getRemoteUser().equalsIgnoreCase("admin")) {
				roles = this.roleManager.getAll(Role.class);
			} else {
				User user = this.userManager.getUserByUsername(getRequest().getRemoteUser(), true, true);
				roles = new ArrayList<Role>(user.getRoles());
			}
		}
		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String save() throws Exception {
		if (role != null) {
			role = this.roleManager.getRole(role.getName(), true);
			String[] users = getRequest().getParameterValues("users");

			// 删除用户
			List<User> roleUsersList = this.userManager.getUsersByRole(role);
			for (User roleUser : roleUsersList) {
				this.roleManager.deleteRoleUser(role.getId(), roleUser.getId());
			}

			// 添加用户
			roleUsers = new ArrayList<User>();
			for (int i = 0; i < users.length; i++) {
				this.roleManager.addRoleUser(role.getId(), new Long(users[i]));

			}
		}

		prepareResourceList();
		return SUCCESS;
	}

	public String edit() throws IOException {
		if (name != null) {
			// lookup the role using that id
			role = roleManager.getRole(name);
			prepareResourceList();
		}
		return SUCCESS;
	}

	private void prepareResourceList() {

		List<User> roleUsersList = this.userManager.getUsersByRole(role);
		if (roleUsersList != null && roleUsersList.size() > 0) {
			role.setUserList(new ArrayList<LabelValue>());
			for (User roleUser : roleUsersList) {
				role.getUserList().add(new LabelValue(roleUser.getUsername(), roleUser.getId().toString()));
			}
		}

		List<User> allUserList = this.userManager.getAuthorizedUser(getRequest().getRemoteUser(), null, null, null, null, null, null);

		if (allUserList != null && allUserList.size() > 0) {
			this.availableUsers = new ArrayList<LabelValue>();
			for (int i = 0; i < allUserList.size(); i++) {
				boolean notInResourceList = true;
				if (roleUsersList != null && roleUsersList.size() > 0) {
					if (roleUsersList.contains(allUserList.get(i))) {
						notInResourceList = false;
					}
				}
				if (notInResourceList) {
					this.availableUsers.add(new LabelValue(allUserList.get(i).getUsername(), allUserList.get(i).getId().toString()));

				}
			}
		}

	}
}
