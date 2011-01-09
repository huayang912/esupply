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

	public GrantedAuthority[] getAuthorities() {
		return authorities;
	}

	public void setAuthorities(GrantedAuthority[] authorities) {
		this.authorities = authorities;
	}

	public boolean isAllowed(MenuComponent menu) {
		if (authorities != null) {
			for (GrantedAuthority authority : authorities) {
				if (menu.getPage().toLowerCase().startsWith((authority.getAuthority().toLowerCase()))) {
					return true;
				}
			}
		}

		return false;
	}

	public MenuPermissionsAdapter(GrantedAuthority[] authorities) {
		this.authorities = authorities;
	}	
}
