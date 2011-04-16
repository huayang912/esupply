package com.faurecia.model;

import java.io.Serializable;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;

@Entity
@Table(name = "supplier")
@NamedQueries ({
    @NamedQuery(
        name = "findSuppliersByPlant",
        query = " select ps.supplier from PlantSupplier ps where ps.plant.code = :plantCode "
        )
})
public class Supplier extends BaseObject implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = -429716729199456024L;
	private String code;
	private String name;

	@Id
	@Column(length = 20)
	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	@Column(nullable = true, length = 50)
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		Supplier myClass = (Supplier) object;
		return new CompareToBuilder().append(this.code, myClass.code)
				.toComparison();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof Supplier)) {
			return false;
		}
		Supplier rhs = (Supplier) object;
		return new EqualsBuilder().append(this.code, rhs.code).isEquals();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(-1873980145, 11289259).append(this.code)
				.toHashCode();
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("code", this.code).toString();
	}
}
