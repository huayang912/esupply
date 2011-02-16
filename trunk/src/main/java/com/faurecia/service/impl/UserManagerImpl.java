package com.faurecia.service.impl;

import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.jws.WebService;
import javax.persistence.EntityExistsException;

import org.springframework.beans.factory.annotation.Required;
import org.springframework.dao.DataIntegrityViolationException;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.jdbc.support.rowset.SqlRowSet;
import org.springframework.security.providers.encoding.PasswordEncoder;
import org.springframework.security.userdetails.UsernameNotFoundException;

import com.faurecia.dao.UserDao;
import com.faurecia.model.Plant;
import com.faurecia.model.Role;
import com.faurecia.model.User;
import com.faurecia.service.UserExistsException;
import com.faurecia.service.UserManager;
import com.faurecia.service.UserService;

/**
 * s Implementation of UserManager interface.
 * 
 * @author <a href="mailto:matt@raibledesigns.com">Matt Raible</a>
 */
@WebService(serviceName = "UserService", endpointInterface = "com.faurecia.service.UserService")
public class UserManagerImpl extends UniversalManagerImpl implements UserManager, UserService {
	private UserDao dao;
	private PasswordEncoder passwordEncoder;
	private JdbcTemplate jdbcTemplate;

	/**
	 * Set the Dao for communication with the data layer.
	 * 
	 * @param dao
	 *            the UserDao that communicates with the database
	 */
	@Required
	public void setUserDao(UserDao dao) {
		this.dao = dao;
	}

	/**
	 * Set the PasswordEncoder used to encrypt passwords.
	 * 
	 * @param passwordEncoder
	 *            the PasswordEncoder implementation
	 */
	@Required
	public void setPasswordEncoder(PasswordEncoder passwordEncoder) {
		this.passwordEncoder = passwordEncoder;
	}

	/**
	 * {@inheritDoc}
	 */
	public User getUser(String userId) {
		return dao.get(new Long(userId));
	}

	/**
	 * {@inheritDoc}
	 */
	public List<User> getUsers(User user) {
		return dao.getUsers();
	}

	public void setJdbcTemplate(JdbcTemplate jdbcTemplate) {
		this.jdbcTemplate = jdbcTemplate;
	}

	/**
	 * {@inheritDoc}
	 */
	public User saveUser(User user) throws UserExistsException {

		if (user.getVersion() == null) {
			// if new user, lowercase userId
			user.setUsername(user.getUsername().toLowerCase());
		}

		// Get and prepare password management-related artifacts
		boolean passwordChanged = false;
		if (passwordEncoder != null) {
			// Check whether we have to encrypt (or re-encrypt) the password
			if (user.getVersion() == null) {
				// New user, always encrypt
				passwordChanged = true;
			} else {
				// Existing user, check password in DB
				String currentPassword = dao.getUserPassword(user.getUsername());
				if (currentPassword == null) {
					passwordChanged = true;
				} else {
					if (!currentPassword.equals(user.getPassword())) {
						passwordChanged = true;
					}
				}
			}

			// If password was changed (or new user), encrypt it
			if (passwordChanged) {
				user.setPassword(passwordEncoder.encodePassword(user.getPassword(), null));
			}
		} else {
			log.warn("PasswordEncoder not set, skipping password encryption...");
		}

		try {
			return dao.saveUser(user);
		} catch (DataIntegrityViolationException e) {
			e.printStackTrace();
			log.warn(e.getMessage());
			throw new UserExistsException("User '" + user.getUsername() + "' already exists!");
		} catch (EntityExistsException e) { // needed for JPA
			e.printStackTrace();
			log.warn(e.getMessage());
			throw new UserExistsException("User '" + user.getUsername() + "' already exists!");
		}
	}

	/**
	 * {@inheritDoc}
	 */
	public void removeUser(String userId) {
		log.debug("removing user: " + userId);
		dao.remove(new Long(userId));
	}

	/**
	 * {@inheritDoc}
	 * 
	 * @param username
	 *            the login name of the human
	 * @return User the populated user object
	 * @throws UsernameNotFoundException
	 *             thrown when username not found
	 */
	public User getUserByUsername(String username) throws UsernameNotFoundException {
		return (User) dao.loadUserByUsername(username);
	}

	public List<User> getUsersByRole(Role role) {
		Map<String, Object> queryParam = new HashMap<String, Object>();
		queryParam.put("role", role);
		return dao.findByNamedQuery("findUsersByRole", queryParam);
	}

	public List<User> getPlantUsers(Plant plant, Role role) {
		Map<String, Object> queryParam = new HashMap<String, Object>();
		queryParam.put("plant", plant);
		queryParam.put("role", role);

		return dao.findByNamedQuery("findPlantUsers", queryParam);
	}

	public List<User> getSuppliers(Plant plant, Role role) {
		Map<String, Object> queryParam = new HashMap<String, Object>();
		queryParam.put("plant", plant);
		queryParam.put("role", role);

		return dao.findByNamedQuery("findSuppliers", queryParam);
	}

	public void keepSession() {
	}

	public List<User> getAuthorizedUser(String userName, String userName2, String firstName, String lastName, String email, String orderBy, String orderSort) {
		String sql = "select u.id ,u.account_expired ,u.account_locked ,u.address ,u.city ,u.country ,u.postal_code ,u.province ,u.credentials_expired ,u.email ,u.account_enabled ,u.first_name ,u.last_name ,u.password ,u.password_hint ,u.phone_number ,u.username ,u.version ,u.website from " 
				+ "(select distinct u.id ,u.account_expired ,u.account_locked ,u.address ,u.city ,u.country ,u.postal_code ,u.province ,u.credentials_expired ,u.email ,u.account_enabled ,u.first_name ,u.last_name ,u.password ,u.password_hint ,u.phone_number ,u.username ,u.version ,u.website from app_user as u " 
				+ "inner join user_resource as ur on u.id = ur.user_id " 
				+ "where ur.resource_id in "
				+ "(select distinct ur.resource_id from app_user as u " 
				+ "inner join user_resource as ur on u.id = ur.user_id "
				+ "inner join resource as r on ur.resource_id = r.id and r.type = 'plant' " 
				+ "where u.username = ? " 
				+ "union all "
				+ "select distinct rr.resource_id from app_user as u " 
				+ "inner join user_role as ur on u.id = ur.user_id "
				+ "inner join role_resource as rr on ur.role_id = rr.role_id "
				+ "inner join resource as r on rr.resource_id = r.id and r.type = 'plant' " 
				+ "where u.username = ?) " 
				+ "union all "
				+ "select distinct u.id ,u.account_expired ,u.account_locked ,u.address ,u.city ,u.country ,u.postal_code ,u.province ,u.credentials_expired ,u.email ,u.account_enabled ,u.first_name ,u.last_name ,u.password ,u.password_hint ,u.phone_number ,u.username ,u.version ,u.website from app_user as u " 
				+ "inner join user_role as ur on u.id = ur.user_id "
				+ "inner join role_resource as rr on ur.role_id = rr.role_id " 
				+ "where rr.resource_id in "
				+ "(select distinct ur.resource_id from app_user as u " 
				+ "inner join user_resource as ur on u.id = ur.user_id "
				+ "inner join resource as r on ur.resource_id = r.id and r.type = 'plant' " 
				+ "where u.username = ? " 
				+ "union all "
				+ "select distinct rr.resource_id from app_user as u " 
				+ "inner join user_role as ur on u.id = ur.user_id "
				+ "inner join role_resource as rr on ur.role_id = rr.role_id "
				+ "inner join resource as r on rr.resource_id = r.id and r.type = 'plant' " 
				+ "where u.username = ?)) as u where 1= 1 ";
		
		List<Object > params = new ArrayList<Object>();
		params.add(userName);
		params.add(userName);
		params.add(userName);
		params.add(userName);
		
		if (userName2 != null && userName2.trim().length() > 0) {
			sql += "and u.username like ? ";
			params.add("%" + userName2 + "%");
		}
		
		if (firstName != null && firstName.trim().length() > 0) {
			sql += "and u.first_name like ? ";
			params.add("%" + firstName + "%");
		}
		
		if (lastName != null && lastName.trim().length() > 0) {
			sql += "and u.last_name like ? ";
			params.add("%" + lastName + "%");
		}
		
		if (email != null && email.trim().length() > 0) {
			sql += "and u.email like ? ";
			params.add("%" + email + "%");
		}
		
		if (orderBy != null && orderBy.trim().length() > 0) {
			sql += "order by u." + orderBy;
			if (orderSort != null && orderSort.trim().length() > 0) {
				sql += " " + orderSort;
			}
		}
		
		SqlRowSet resultSet = this.jdbcTemplate.queryForRowSet(sql, params.toArray());
		
		List<User> users = new ArrayList<User>();
		while (resultSet.next()) {
			User user = new User();
			user.setId(resultSet.getLong("id"));
			user.setAccountExpired(resultSet.getBoolean("account_expired"));
			user.setAccountLocked(resultSet.getBoolean("account_locked"));
			//user.setAddress(resultSet.getString(4));
			//user.setCity(5);
			//user.setCountry(6);
			//user.setPostal_code(7);
			//user.setProvince(8);
			user.setCredentialsExpired(resultSet.getBoolean("credentials_expired"));
			user.setEmail(resultSet.getString("email"));
			user.setEnabled(resultSet.getBoolean("account_enabled"));
			user.setFirstName(resultSet.getString("first_name"));
			user.setLastName(resultSet.getString("last_name"));
			user.setPassword(resultSet.getString("password"));
			user.setPasswordHint(resultSet.getString("password_hint"));
			user.setPhoneNumber(resultSet.getString("phone_number"));
			user.setUsername(resultSet.getString("username"));
			user.setVersion(resultSet.getInt("version"));
			user.setWebsite(resultSet.getString("website"));
			
			users.add(user);
		}
		
		return users;
	}
}
