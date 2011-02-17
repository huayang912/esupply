package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Resource;
import com.faurecia.model.User;


public interface ResourceManager extends GenericManager<Resource, Integer> {
	List<Resource> getResourceByType(String type);
	void deleteUserResource(Long userId,Long resourceId);
	void deleteRoleResource(Long roleId,Long resourceId);
	void addUserResource(Long userId,Long resourceId);
	void addRoleResource(Long roleId,Long resourceId);
	Resource getResource(String resourceId);
}
