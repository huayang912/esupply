package com.faurecia.webapp.action;

import java.io.IOException;
import java.util.ArrayList;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;

import org.apache.struts2.ServletActionContext;
import org.springframework.mail.MailException;
import org.springframework.security.AccessDeniedException;
import org.springframework.security.Authentication;
import org.springframework.security.AuthenticationTrustResolver;
import org.springframework.security.AuthenticationTrustResolverImpl;
import org.springframework.security.context.SecurityContext;
import org.springframework.security.context.SecurityContextHolder;

import com.faurecia.Constants;
import com.faurecia.model.Plant;
import com.faurecia.model.Role;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.service.GenericManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.service.UserExistsException;
import com.faurecia.webapp.util.RequestUtil;
import com.opensymphony.xwork2.Preparable;

/**
 * Action for facilitating User Management feature.
 */
public class UserAction extends BaseAction implements Preparable {
	private static final long serialVersionUID = 6776558938712115191L;
	private List<User> users;
	private User user;
	private String id;
	private String roleType;
	private List<Plant> plants;
	private List<Supplier> suppliers;
	private GenericManager<Plant, String> plantManager;
	private SupplierManager supplierManager;

	/**
	 * Grab the entity from the database before populating with request
	 * parameters
	 */
	public void prepare() {
		if (getRequest().getMethod().equalsIgnoreCase("post")) {
			// prevent failures on new
			if (!"".equals(getRequest().getParameter("user.id"))) {
				user = userManager
						.getUser(getRequest().getParameter("user.id"));
			}
		}
	}

	/**
	 * Holder for users to display on list screen
	 * 
	 * @return list of users
	 */
	public List<User> getUsers() {
		return users;
	}

	public void setId(String id) {
		this.id = id;
	}

	public User getUser() {
		return user;
	}

	public void setUser(User user) {
		this.user = user;
	}

	public String getRoleType() {
		return roleType;
	}

	public void setRoleType(String roleType) {
		this.roleType = roleType;
	}

	public List<Plant> getPlants() {
		return plants;
	}

	public List<Supplier> getSuppliers() {
		return suppliers;
	}

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	/**
	 * Delete the user passed in.
	 * 
	 * @return success
	 */
	public String delete() {
		userManager.removeUser(user.getId().toString());
		List<String> args = new ArrayList<String>();
		args.add(user.getFullName());
		saveMessage(getText("user.deleted", args));

		if (Constants.ADMIN_ROLE.equals(roleType)
				|| Constants.PLANT_ADMIN_ROLE.equals(roleType)) {
			return "adminSuccess";
		} else if (Constants.PLANT_USER_ROLE.equals(roleType)
				|| Constants.VENDOR_ROLE.equals(roleType)) {
			return "userSuccess";
		} else {
			log.warn("Trying to back to user list with role not specified.");
			
			return "mainMenu";
		}
	}

	/**
	 * Grab the user from the database based on the "id" passed in.
	 * 
	 * @return success if user found
	 * @throws IOException
	 *             can happen when sending a "forbidden" from
	 *             response.sendError()
	 */
	public String edit() throws IOException {
		HttpServletRequest request = getRequest();
		boolean editProfile = (request.getRequestURI().indexOf("editProfile") > -1);

		// if URL is "editProfile" - make sure it's the current user
		if (editProfile) {
			// reject if id passed in or "list" parameter passed in
			// someone that is trying this probably knows the AppFuse code
			// but it's a legitimate bug, so I'll fix it. ;-)
			if ((request.getParameter("id") != null)
					|| (request.getParameter("from") != null)) {
				ServletActionContext.getResponse().sendError(
						HttpServletResponse.SC_FORBIDDEN);
				log.warn("User '" + request.getRemoteUser()
						+ "' is trying to edit user '"
						+ request.getParameter("id") + "'");

				return null;
			}
		}

		// if a user's id is passed in
		if (id != null) {
			// lookup the user using that id
			user = userManager.getUser(id);
		} else if (editProfile) {
			user = userManager.getUserByUsername(request.getRemoteUser());
		} else {
			user = new User();			
			// user.addRole(new Role(Constants.PLANT_ADMIN_ROLE));
		}

		if (user.getUsername() != null) {
			user.setConfirmPassword(user.getPassword());

			// if user logged in with remember me, display a warning that they
			// can't change passwords
			log.debug("checking for remember me login...");

			AuthenticationTrustResolver resolver = new AuthenticationTrustResolverImpl();
			SecurityContext ctx = SecurityContextHolder.getContext();

			if (ctx != null) {
				Authentication auth = ctx.getAuthentication();

				if (resolver.isRememberMe(auth)) {
					getSession().setAttribute("cookieLogin", "true");
					saveMessage(getText("userProfile.cookieLogin"));
				}
			}
		}
			
		this.prepareEdit();

		return SUCCESS;
	}

	/**
	 * Default: just returns "success"
	 * 
	 * @return "success"
	 */
	public String execute() {
		return SUCCESS;
	}

	/**
	 * Sends users to "mainMenu" when !from.equals("list"). Sends everyone else
	 * to "cancel"
	 * 
	 * @return "mainMenu" or "cancel"
	 */
	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}
		
		if (Constants.ADMIN_ROLE.equals(roleType)
				|| Constants.PLANT_ADMIN_ROLE.equals(roleType)) {
			return "adminCancel";
		} else if (Constants.PLANT_USER_ROLE.equals(roleType)
				|| Constants.VENDOR_ROLE.equals(roleType)) {
			return "userCancel";
		} else {
			log.warn("Trying to back to user list with role not specified.");
			
			return "mainMenu";
		}
	}

	/**
	 * Save user
	 * 
	 * @return success if everything worked, otherwise input
	 * @throws IOException
	 *             when setting "access denied" fails on response
	 */
	public String save() throws Exception {

		Integer originalVersion = user.getVersion();

		boolean isNew = ("".equals(getRequest().getParameter("user.version")));

		//为新增用户添加角色
		if (isNew) {
			user.addRole(roleManager.getRole(roleType));
			
			//工厂管理员维护工厂用户和供应商时，默认填写新增用户的工厂属性
			if (Constants.PLANT_USER_ROLE.equals(roleType) ||
					Constants.VENDOR_ROLE.equals(roleType)) {
				String userCode = this.getRequest().getRemoteUser();
				User plantAdmin = this.userManager.getUserByUsername(userCode);
				user.setUserPlant(plantAdmin.getUserPlant());
			}
		}

		try {
			userManager.saveUser(user);
		} catch (AccessDeniedException ade) {
			// thrown by UserSecurityAdvice configured in aop:advisor
			// userManagerSecurity
			log.warn(ade.getMessage());
			getResponse().sendError(HttpServletResponse.SC_FORBIDDEN);
			return null;
		} catch (UserExistsException e) {
			List<String> args = new ArrayList<String>();
			args.add(user.getUsername());
			args.add(user.getEmail());
			addActionError(getText("errors.existing.user", args));

			// reset the version # to what was passed in
			user.setVersion(originalVersion);
			// redisplay the unencrypted passwords
			user.setPassword(user.getConfirmPassword());
			
			this.prepareEdit();
			return INPUT;
		}

		if (!"list".equals(from)) {
			// add success messages
			saveMessage(getText("user.saved"));
			return "mainMenu";
		} else {
			// add success messages
			List<String> args = new ArrayList<String>();
			args.add(user.getFullName());
			if (isNew) {
				saveMessage(getText("user.added", args));
				// Send an account information e-mail
				mailMessage.setSubject(getText("signup.email.subject"));
				try {
					sendUserMessage(user,
							getText("newuser.email.message", args), RequestUtil
									.getAppURL(getRequest()));
				} catch (MailException me) {
					addActionError(me.getCause().getLocalizedMessage());
				}
				
				if (Constants.ADMIN_ROLE.equals(roleType)
						|| Constants.PLANT_ADMIN_ROLE.equals(roleType)) {
					return "adminSuccess";
				} else if (Constants.PLANT_USER_ROLE.equals(roleType)
						|| Constants.VENDOR_ROLE.equals(roleType)) {
					return "userSuccess";
				} else {
					log.warn("Trying to back to user list with role not specified.");
					
					return "mainMenu";
				}
			} else {
				saveMessage(getText("user.updated.byAdmin", args));
				this.prepareEdit();
				return INPUT;
			}
		}
	}

	/**
	 * Fetch all users from database and put into local "users" variable for
	 * retrieval in the UI.
	 * 
	 * @return "success" if no exceptions thrown
	 * @throws IOException
	 */
	public String list() throws IOException {

		if (Constants.ADMIN_ROLE.equals(roleType)) {
			Role role = this.roleManager.getRole(Constants.ADMIN_ROLE);
			users = userManager.getUsersByRole(role);
		} else if (Constants.PLANT_ADMIN_ROLE.equals(roleType)) {
			Role role = this.roleManager.getRole(Constants.PLANT_ADMIN_ROLE);
			users = userManager.getUsersByRole(role);
		} else if (Constants.PLANT_USER_ROLE.equals(roleType)) {
			String userCode = this.getRequest().getRemoteUser();
			User user = this.userManager.getUserByUsername(userCode);
			Role role = this.roleManager.getRole(Constants.PLANT_USER_ROLE);
			users = userManager.getPlantUsers(user.getUserPlant(), role);
		} else if (Constants.VENDOR_ROLE.equals(roleType)) {
			String userCode = this.getRequest().getRemoteUser();
			User user = this.userManager.getUserByUsername(userCode);
			Role role = this.roleManager.getRole(Constants.VENDOR_ROLE);
			users = userManager.getSuppliers(user.getUserPlant(), role);
		} else {
			
			log.warn("Trying to list user with role not specified.");
			
			ServletActionContext.getResponse().sendError(
					HttpServletResponse.SC_NOT_FOUND);
			
		}

		return SUCCESS;
	}
	
	private void prepareEdit(){
		if (Constants.PLANT_ADMIN_ROLE.equals(roleType)) {
			this.plants = this.plantManager.getAll();
		} else if (Constants.VENDOR_ROLE.equals(roleType)) {	
			String userCode = this.getRequest().getRemoteUser();
			User user = this.userManager.getUserByUsername(userCode);
			this.suppliers = this.supplierManager.getSuppliersByPlant(user.getUserPlant());
		}
	}
}
