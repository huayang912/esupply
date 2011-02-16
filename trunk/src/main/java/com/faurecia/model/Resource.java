package com.faurecia.model;

import java.io.Serializable;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Transient;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.ToStringStyle;
import org.springframework.security.GrantedAuthority;

@Entity
@Table(name="resource")
public class Resource extends BaseObject implements Serializable, GrantedAuthority {

	private static final long serialVersionUID = -3986416840733225877L;
	private Long id;
	private String code;
	private String type;
	private String description;
	public static final String RESOURCE_TYPE_URL = "url";
	public static final String RESOURCE_TYPE_PLANT = "plant";
	public static final String RESOURCE_TYPE_SUPPLIER = "supplier";

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Long getId() {
		return id;
	}

	public void setId(Long id) {
		this.id = id;
	}

	@Column(name = "code", nullable = false, length = 50)
	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	@Column(name = "type", nullable = false, length = 20)
	public String getType() {
		return type;
	}

	public void setType(String type) {
		this.type = type;
	}

	@Column(name = "description", nullable = false, length = 50)
	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	@Override
	public boolean equals(Object o) {
		 if (this == o) {
	            return true;
	        }
	        if (!(o instanceof Resource)) {
	            return false;
	        }

	        final Resource resource = (Resource) o;

	        return !(code != null ? !code.equals(resource.code) : resource.code != null);

	}

	@Override
	public int hashCode() {
		 return (code != null ? code.hashCode() : 0);
	}

	@Override
	public String toString() {
		return new ToStringBuilder(this, ToStringStyle.SIMPLE_STYLE)
        .append(this.code)
        .toString();
	}

	/**
	 * @see org.springframework.security.GrantedAuthority#getAuthority()
	 * @return the name property (getAuthority required by Acegi's
	 *         GrantedAuthority interface)
	 */
	@Transient
	public String getAuthority() {
		return this.getCode();
	}

	public int compareTo(Object o) {
		return (equals(o) ? 0 : -1);
	}

}
