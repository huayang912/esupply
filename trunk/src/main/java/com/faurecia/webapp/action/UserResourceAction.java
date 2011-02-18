package com.faurecia.webapp.action;

import java.io.IOException;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.Iterator;
import java.util.List;
import java.util.Map;

import com.faurecia.model.LabelValue;
import com.faurecia.model.Resource;
import com.faurecia.model.User;
import com.faurecia.service.ResourceManager;
import com.faurecia.service.UserManager;
import com.faurecia.webapp.util.PaginatedListUtil;
import com.opensymphony.xwork2.Preparable;

public class UserResourceAction extends BaseAction implements Preparable {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7684641539522685901L;
	private List<User> users;
	private ResourceManager resourceManager;
	private UserManager userManager;
	private String type;
	private User user;
	private PaginatedListUtil<User> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private String id;

	private List<LabelValue> availableResources;

	public void setId(String id) {
		this.id = id;
	}

	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	public PaginatedListUtil<User> getPaginatedList() {
		return paginatedList;
	}

	public void setPaginatedList(PaginatedListUtil<User> paginatedList) {
		this.paginatedList = paginatedList;
	}

	public int getPageSize() {
		return pageSize;
	}

	public void setPageSize(int pageSize) {
		this.pageSize = pageSize;
	}

	public int getPage() {
		return page;
	}

	public void setPage(int page) {
		this.page = page;
	}

	public String getSort() {
		return sort;
	}

	public void setSort(String sort) {
		this.sort = sort;
	}

	public String getDir() {
		return dir;
	}

	public void setDir(String dir) {
		this.dir = dir;
	}

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		this.user = user;
	}

	public List<User> getUsers() {
		return users;
	}

	public void setResourceManager(ResourceManager resourceManager) {
		this.resourceManager = resourceManager;
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
		if (user != null) {
			if (type == null) {
				pageSize = pageSize == 0 ? 25 : pageSize;
				page = page == 0 ? 1 : page;

				paginatedList = new PaginatedListUtil<User>();
				paginatedList.setPageNumber(page);
				paginatedList.setObjectsPerPage(pageSize);

				users = this.userManager.getAuthorizedUser(getRequest().getRemoteUser(), user.getUsername(), user.getFirstName(), user.getLastName(),
						user.getEmail(), sort, dir);

				if ((page - 1) * pageSize > users.size()) {
					paginatedList.setList(new ArrayList<User>());
				} else if (page * pageSize > users.size()) {
					paginatedList.setList(users.subList((page - 1) * pageSize, users.size()));
				} else {
					paginatedList.setList(users.subList((page - 1) * pageSize, page * pageSize));
				}
				paginatedList.setFullListSize(users.size());
			} else {
				user = this.userManager.getUserByUsername(user.getUsername());
				prepareResourceList();
			}
		}
		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String save() throws Exception {
		if (user != null && !user.getUsername().equalsIgnoreCase("admin")) {
			user = this.userManager.getUserByUsername(user.getUsername());
			String[] resources = getRequest().getParameterValues("resources");
			List<Resource> removeList = new ArrayList();
			// 删除权限
			Iterator<Resource> ite = user.getResources().iterator();
			while (ite.hasNext()) {
				Resource userResource = (Resource) ite.next();
				if (userResource.getType().equalsIgnoreCase(type)) {
					resourceManager.deleteUserResource(user.getId(), userResource.getId());
					removeList.add(userResource);
				}
			}
			user.getResources().removeAll(removeList);

			// 添加新权限
			for (int i = 0; i < resources.length; i++) {
				resourceManager.addUserResource(user.getId(), Long.parseLong(resources[i]));
				user.getResources().add(resourceManager.getResource(resources[i]));
			}
		}
		
		prepareResourceList();
		
		return SUCCESS;
	}

	public void prepare() {
		if (getRequest().getMethod().equalsIgnoreCase("post")) {
			// prevent failures on new
			if (getRequest().getParameter("user.id") != null && !"".equals(getRequest().getParameter("user.id"))) {
				user = userManager.getUser(getRequest().getParameter("user.id"));
			}
		}
	}

	public String edit() throws IOException {
		if (id != null) {
			// lookup the user using that id
			user = userManager.getUser(id);
			// prepareResourceList();
		}
		return SUCCESS;
	}

	private void prepareResourceList() {
		if (user.getUsername().equalsIgnoreCase("admin")) {
			List<Resource> allResourceList = this.resourceManager.getResourceByType(type);
			user.setResourceList(new ArrayList<LabelValue>());
			for (Resource res : allResourceList) {
				user.getResourceList().add(new LabelValue(res.getDescription(), res.getId().toString()));
			}
			this.availableResources = new ArrayList<LabelValue>();
		} else {
			if (user.getResources() != null && user.getResources().size() > 0) {
				user.setResourceList(new ArrayList<LabelValue>());
				Iterator<Resource> ite = user.getResources().iterator();
				while (ite.hasNext()) {
					Resource userResource = (Resource) ite.next();
					if (userResource.getType().equalsIgnoreCase(type)) {
						user.getResourceList().add(new LabelValue(userResource.getDescription(), userResource.getId().toString()));
					}
				}
			}

			User user2 = this.userManager.getUserByUsername(getRequest().getRemoteUser(), true, true,true);
			
			List<Resource> allResourceList = null;
			if (type.equals(Resource.RESOURCE_TYPE_URL)) {
				allResourceList = user2.getUrlResource();
			} else if (type.equals(Resource.RESOURCE_TYPE_PLANT)) {
				allResourceList = user2.getPlantResource();
			} else if (type.equals(Resource.RESOURCE_TYPE_SUPPLIER)) {
				allResourceList = user2.getSupplierResource();
			}
			if (allResourceList != null && allResourceList.size() > 0) {
				this.availableResources = new ArrayList<LabelValue>();
				for (int i = 0; i < allResourceList.size(); i++) {
					boolean notInResourceList = true;
					if (user.getResources() != null && user.getResources().size() > 0) {
						if (user.getResources().contains(allResourceList.get(i))) {
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
