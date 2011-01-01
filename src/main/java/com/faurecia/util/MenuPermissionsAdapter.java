package com.faurecia.util;

import java.io.Serializable;

import net.sf.navigator.menu.MenuComponent;
import net.sf.navigator.menu.PermissionsAdapter;

import org.springframework.security.GrantedAuthority;

public class MenuPermissionsAdapter implements PermissionsAdapter, Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private GrantedAuthority[] authorities;

	public boolean isAllowed(MenuComponent menu) {
		if (authorities != null) {
			for (GrantedAuthority authority : authorities) {
//				if (menu.getName().equalsIgnoreCase(authority.getAuthority())) {
//					return true;
//				}
				if (menu.getRoles() == null || menu.getRoles().indexOf(authority.getAuthority()) > -1) {
					return true;
				}
			}
		}

		return true;
	}

	public MenuPermissionsAdapter(GrantedAuthority[] authorities) {
		this.authorities = authorities;
	}
}
