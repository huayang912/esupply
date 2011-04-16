package com.faurecia.util;

import javax.servlet.http.HttpServletRequest;

import org.springframework.security.Authentication;
import org.springframework.security.ConfigAttribute;
import org.springframework.security.ConfigAttributeDefinition;
import org.springframework.security.GrantedAuthority;
import org.springframework.security.intercept.web.FilterInvocation;
import org.springframework.security.vote.AccessDecisionVoter;

import com.faurecia.Constants;

public class AuthorityVoter implements AccessDecisionVoter {

	public static final String MENU_PERMISSION_ADAPTER = "menuPermissionsAdapter";

	public boolean supports(ConfigAttribute attribute) {
		return true;
	}

	@SuppressWarnings("unchecked")
	public boolean supports(Class clazz) {
		return true;
	}

	public int vote(Authentication authentication, Object object, ConfigAttributeDefinition config) {

		if (authentication.getName().equalsIgnoreCase(Constants.SUPER_USER)) {
			return ACCESS_GRANTED;
		}

		FilterInvocation fi = (FilterInvocation) object;
		HttpServletRequest request = fi.getHttpRequest();
		MenuPermissionsAdapter menuPermissionsAdapter = (MenuPermissionsAdapter) request.getSession().getAttribute(MENU_PERMISSION_ADAPTER);
		if (menuPermissionsAdapter != null) {
			GrantedAuthority[] authorities = menuPermissionsAdapter.getAuthorities();
			if (authorities != null && authorities.length > 0) {
				String url = fi.getRequestUrl();

				for (GrantedAuthority authority : authorities) {
					if (url.toLowerCase().startsWith(authority.getAuthority().toLowerCase())) {
						return ACCESS_GRANTED;
					}
				}
			}
		}

		return ACCESS_DENIED;
	}

}
