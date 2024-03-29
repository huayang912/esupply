package com.faurecia.service;

import java.util.List;

import org.springframework.security.userdetails.UsernameNotFoundException;

import com.faurecia.dao.UserDao;
import com.faurecia.model.Plant;
import com.faurecia.model.Role;
import com.faurecia.model.User;

/**
 * Business Service Interface to handle communication between web and
 * persistence layer.
 * 
 * @author <a href="mailto:matt@raibledesigns.com">Matt Raible</a> Modified by
 *         <a href="mailto:dan@getrolling.com">Dan Kibler </a>
 */
public interface UserManager extends UniversalManager {

	/**
	 * Convenience method for testing - allows you to mock the DAO and set it on
	 * an interface.
	 * 
	 * @param userDao
	 *            the UserDao implementation to use
	 */
	void setUserDao(UserDao userDao);

	/**
	 * Retrieves a user by userId. An exception is thrown if user not found
	 * 
	 * @param userId
	 *            the identifier for the user
	 * @return User
	 */
	User getUser(String userId);
	
	User getUser(String userId,boolean includeResources,boolean includeRoles);

	/**
	 * Finds a user by their username.
	 * 
	 * @param username
	 *            the user's username used to login
	 * @return User a populated user object
	 * @throws org.springframework.security.userdetails.UsernameNotFoundException
	 *             exception thrown when user not found
	 */
	User getUserByUsername(String username) throws UsernameNotFoundException;

	/**
	 * Retrieves a list of users, filtering with parameters on a user object
	 * 
	 * @param user
	 *            parameters to filter on
	 * @return List
	 */
	List<User> getUsers(User user);

	/**
	 * Saves a user's information.
	 * 
	 * @param user
	 *            the user's information
	 * @throws UserExistsException
	 *             thrown when user already exists
	 * @return user the updated user object
	 */
	User saveUser(User user) throws UserExistsException;

	/**
	 * Removes a user from the database by their userId
	 * 
	 * @param userId
	 *            the user's id
	 */
	void removeUser(String userId);

	List<User> getUsersByRole(Role role);

	List<User> getPlantUsers(Plant plant, Role role);

	List<User> getSuppliers(Plant plant, Role role);
	
	void keepSession();
	
	List<User> getAuthorizedUser(String userName, String userName2, String firstName, String lastName, String email, String orderBy, String orderSort);
	
	User getUserByUsername(String username,boolean includeResources,boolean includeRoles);
	
	User getUserByUsername(String username,boolean includeResources,boolean includeRoles,boolean includeRoleResources);
}
