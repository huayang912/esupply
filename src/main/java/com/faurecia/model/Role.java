package com.faurecia.model;

import java.io.Serializable;
import java.util.HashSet;
import java.util.List;
import java.util.Set;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.JoinTable;
import javax.persistence.ManyToMany;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.persistence.Transient;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.ToStringStyle;

/**
 * This class is used to represent available roles in the database.
 * 
 * @author <a href="mailto:matt@raibledesigns.com">Matt Raible</a> Version by
 *         Dan Kibler dan@getrolling.com Extended to implement Acegi
 *         GrantedAuthority interface by David Carter david@carter.net
 */
@Entity
@Table(name = "role")
@NamedQueries( { @NamedQuery(name = "findRoleByName", query = "select r from Role r where r.name = :name ") })
public class Role extends BaseObject implements Serializable {
	private static final long serialVersionUID = 3690197650654049848L;
	private Long id;
	private String name;
	private String description;
	private Set<Resource> resources = new HashSet<Resource>();
	private List<LabelValue> resourceList;
	private List<LabelValue> userList;

	/**
	 * Default constructor - creates a new instance with no values set.
	 */
	public Role() {
	}

	/**
	 * Create a new instance and set the name.
	 * 
	 * @param name
	 *            name of the role.
	 */
	public Role(final String name) {
		this.name = name;
	}

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Long getId() {
		return id;
	}

	@ManyToMany(fetch = FetchType.LAZY)
	@JoinTable(name = "role_resource", joinColumns = { @JoinColumn(name = "role_id") }, inverseJoinColumns = @JoinColumn(name = "resource_id"))
	public Set<Resource> getResources() {
		return resources;
	}

	@Column(length = 20)
	public String getName() {
		return this.name;
	}

	@Column(length = 64)
	public String getDescription() {
		return this.description;
	}

	public void setId(Long id) {
		this.id = id;
	}

	public void setName(String name) {
		this.name = name;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	public void setResources(Set<Resource> resources) {
		this.resources = resources;
	}

	public void addResource(Resource resource) {
		getResources().add(resource);
	}

	@Transient
	public List<LabelValue> getResourceList() {
		return resourceList;
	}

	public void setResourceList(List<LabelValue> resourceList) {
		this.resourceList = resourceList;
	}
	
	@Transient
	public List<LabelValue> getUserList() {
		return userList;
	}

	public void setUserList(List<LabelValue> userList) {
		this.userList = userList;
	}

	/**
	 * {@inheritDoc}
	 */
	public boolean equals(Object o) {
		if (this == o) {
			return true;
		}
		if (!(o instanceof Role)) {
			return false;
		}

		final Role role = (Role) o;

		return !(name != null ? !name.equals(role.name) : role.name != null);

	}

	/**
	 * {@inheritDoc}
	 */
	public int hashCode() {
		return (name != null ? name.hashCode() : 0);
	}

	/**
	 * {@inheritDoc}
	 */
	public String toString() {
		return new ToStringBuilder(this, ToStringStyle.SIMPLE_STYLE).append(this.name).toString();
	}

	public int compareTo(Object o) {
		return (equals(o) ? 0 : -1);
	}
}
