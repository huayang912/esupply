package com.faurecia.model;

import java.io.Serializable;
import java.math.BigDecimal;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.FetchType;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.persistence.UniqueConstraint;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "item",
    uniqueConstraints={@UniqueConstraint(columnNames={"plant_code", "code"})}
)
@NamedQueries ({
	@NamedQuery(
	        name = "findItemByPlant",
	        query = " select i from Item i where i.plant = :plant"
	        ),
	        
    @NamedQuery(
        name = "findItemByPlantAndItem",
        query = " select i from Item i inner join i.plant p where p.code = :plantCode and i.code = :itemCode"
        )
})
public class Item extends BaseObject implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = -1071131332587672817L;
	private Integer id;
	private Plant plant;
	private String code;
	private String description;
	private String uom;
	private BigDecimal unitCount;
	private List<SupplierItem> supplierItems;

	@Id
	@GeneratedValue(strategy = GenerationType.IDENTITY)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "plant_code", nullable=false)
	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}

	@Column(nullable = false, length = 20)
	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	@Column(nullable = true, length = 50)
	public String getDescription() {
		return description;
	}

	public void setDescription(String description) {
		this.description = description;
	}

	@Column(nullable = true, length = 5)
	public String getUom() {
		return uom;
	}

	public void setUom(String uom) {
		this.uom = uom;
	}

	@Column(name="unit_count", nullable = true, precision = 9, scale = 2)
	public BigDecimal getUnitCount() {
		return unitCount;
	}

	public void setUnitCount(BigDecimal unitCount) {
		this.unitCount = unitCount;
	}

	@OneToMany(cascade = CascadeType.ALL, fetch = FetchType.LAZY, mappedBy = "item")
	public List<SupplierItem> getSupplierItems() {
		return supplierItems;
	}

	public void setSupplierItems(List<SupplierItem> supplierItems) {
		this.supplierItems = supplierItems;
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		Item myClass = (Item) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof Item)) {
			return false;
		}
		Item rhs = (Item) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(1702501669, -1551922991).append(this.id)
				.toHashCode();
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("id", this.id).toString();
	}

}
