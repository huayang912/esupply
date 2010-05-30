package com.faurecia.model;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.CompareToBuilder;


@Entity
@Table(name = "number_control")
public class NumberControl extends BaseObject {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = -113795835755016133L;
	private String code;
	private Integer nextValue;

	@Id
	@Column(length = 10)
	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	@Column(name="next_value", nullable = false)
	public Integer getNextValue() {
		return nextValue;
	}

	public void setNextValue(Integer nextValue) {
		this.nextValue = nextValue;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("code", this.code).toString();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(-324028339, -1394272213).append(this.code).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof NumberControl)) {
			return false;
		}
		NumberControl rhs = (NumberControl) object;
		return new EqualsBuilder().append(
				this.code, rhs.code).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		NumberControl myClass = (NumberControl) object;
		return new CompareToBuilder().append(this.code, myClass.code)
				.toComparison();
	} 
	
	
}
