package com.faurecia.model;

import java.util.List;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;
import javax.persistence.Transient;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "plant_schedule_group")
public class PlantScheduleGroup extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = -1717182251842693997L;

	private Integer id;
	private String name;
	private Plant plant;
	private Boolean allowOverDateDeliver;
	private Boolean allowOverQtyDeliver;
	private Boolean allowForecastDeliver;
	private Boolean isDefault;
	private List<LabelValue> supplierList;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@Column(name="name", nullable = false, length = 50)
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	@ManyToOne
	@JoinColumn(name = "plant_code", nullable=false)
	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}

	@Column(name="allow_over_date_deliver", nullable = false)
	public Boolean getAllowOverDateDeliver() {
		return allowOverDateDeliver;
	}

	public void setAllowOverDateDeliver(Boolean allowOverDateDeliver) {
		this.allowOverDateDeliver = allowOverDateDeliver;
	}
	
	@Column(name="allow_over_qty_deliver", nullable = false)
	public Boolean getAllowOverQtyDeliver() {
		return allowOverQtyDeliver;
	}

	public void setAllowOverQtyDeliver(Boolean allowOverQtyDeliver) {
		this.allowOverQtyDeliver = allowOverQtyDeliver;
	}

	@Column(name="allow_forecast_deliver", nullable = false)
	public Boolean getAllowForecastDeliver() {
		return allowForecastDeliver;
	}

	public void setAllowForecastDeliver(Boolean allowForecastDeliver) {
		this.allowForecastDeliver = allowForecastDeliver;
	}
	
	@Column(name="is_default", nullable = false)
	public Boolean getIsDefault() {
		return isDefault;
	}

	public void setIsDefault(Boolean isDefault) {
		this.isDefault = isDefault;
	}

	@Transient
	public List<LabelValue> getSupplierList() {
		return supplierList;
	}

	public void setSupplierList(List<LabelValue> supplierList) {
		this.supplierList = supplierList;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("id", this.id).toString();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(-1746334721, -424958193).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof PlantScheduleGroup)) {
			return false;
		}
		PlantScheduleGroup rhs = (PlantScheduleGroup) object;
		return new EqualsBuilder().append(
				this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		PlantScheduleGroup myClass = (PlantScheduleGroup) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

}
