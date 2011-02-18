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

public class RoleResourceAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7684324539522586901L;
	private List<Role> roles;
	private ResourceManager resourceManager;
	private RoleManager roleManager;
	private UserManager userManager;
	private String type;
	private Role role;
	private String name;

	private List<LabelValue> availableResources;

	public void setName(String name) {
		this.name = name;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
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

	public void setResourceManager(ResourceManager resourceManager) {
		this.resourceManager = resourceManager;
	}

	public void setRoleManager(RoleManager roleManager) {
		this.roleManager = roleManager;
	}
	
	public void setUserManager(UserManager userManager) {
		this.userManager = userManager;
	}

	public List<LabelValue> getAvailableResources() {
		return availableResources;
	}

	public void setAvailableResources(List<LabelValue> availableResources) {
		this.availableResources = availableResources;
	}

	public Map<String, String> getTypes() {
		Map<String, String> types = new HashMap<String, String>();
		types.put("url", "url");
		types.put("plant", "plant");
		types.put("supplier", "supplier");
		return types;
	}

	public String list() {
		if (role != null && type != null) {
			role = this.roleManager.getRole(role.getName(), true);
			prepareResourceList();
		} else {
			User user = this.userManager.getUserByUsername(getRequest().getRemoteUser(), true, true);
			roles = new ArrayList<Role>(user.getRoles());
		}
		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String save() throws Exception {
		if (role != null) {
			role = this.roleManager.getRole(role.getName(), true);
			String[] resources = getRequest().getParameterValues("resources");
			List<Resource> removeList = new ArrayList();
			// 删除权限
			Iterator<Resource> ite = role.getResources().iterator();
			while (ite.hasNext()) {
				Resource roleResource = (Resource) ite.next();
				if (roleResource.getType().equalsIgnoreCase(type)) {
					resourceManager.deleteRoleResource(role.getId(), roleResource.getId());
					removeList.add(roleResource);
				}
			}
			role.getResources().removeAll(removeList);

			// 添加新权限
			for (int i = 0; i < resources.length; i++) {
				resourceManager.addRoleResource(role.getId(), Long.parseLong(resources[i]));
				role.getResources().add(resourceManager.getResource(resources[i]));
			}
		}

		prepareResourceList();
		return SUCCESS;
	}

	// public void prepare() {
	// if (getRequest().getMethod().equalsIgnoreCase("post")) {
	// // prevent failures on new
	// if (getRequest().getParameter("role.name") != null &&
	// !"".equals(getRequest().getParameter("role.name"))) {
	// role = roleManager.getRole(getRequest().getParameter("role.name"));
	// }
	// }
	// }

	public String edit() throws IOException {
		if (name != null) {
			// lookup the role using that id
			role = roleManager.getRole(name);
			// prepareResourceList();
		}
		return SUCCESS;
	}

	private void prepareResourceList() {
		if (role.getName().equalsIgnoreCase("ROLE_ADMIN")) {
			List<Resource> allResourceList = this.resourceManager.getResourceByType(type);
			role.setResourceList(new ArrayList<LabelValue>());
			for (Resource res : allResourceList) {
				role.getResourceList().add(new LabelValue(res.getDescription(), res.getId().toString()));
			}
			this.availableResources = new ArrayList<LabelValue>();
		} else {
			if (role.getResources() != null && role.getResources().size() > 0) {
				role.setResourceList(new ArrayList<LabelValue>());
				Iterator<Resource> ite = role.getResources().iterator();
				while (ite.hasNext()) {
					Resource roleResource = (Resource) ite.next();
					if (roleResource.getType().equalsIgnoreCase(type)) {
						role.getResourceList().add(new LabelValue(roleResource.getDescription(), roleResource.getId().toString()));
					}
				}
			}

			List<Resource> allResourceList = this.resourceManager.getResourceByType(type);
			if (allResourceList != null && allResourceList.size() > 0) {
				this.availableResources = new ArrayList<LabelValue>();
				for (int i = 0; i < allResourceList.size(); i++) {
					boolean notInResourceList = true;
					if (role.getResources() != null && role.getResources().size() > 0) {
						if (role.getResources().contains(allResourceList.get(i))) {
							notInResourceList = false;
						}
					}
					if (notInResourceList) {
						this.availableResources
								.add(new LabelValue(allResourceList.get(i).getDescription(), allResourceList.get(i).getId().toString()));

					}
				}
			}
		}
	}
}
