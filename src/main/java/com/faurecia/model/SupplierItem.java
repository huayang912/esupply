package com.faurecia.model;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.Table;
import javax.persistence.Transient;
import javax.persistence.UniqueConstraint;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.CompareToBuilder;

@Entity
@Table(name = "supplier_item", uniqueConstraints = { @UniqueConstraint(columnNames = {
		"item_id", "supplier_code" }) })
@NamedQueries ({
	@NamedQuery(
	        name = "findSupplierItemByItem",
	        query = " select si from SupplierItem si where si.item = :item"
	        ),
	        
    @NamedQuery(
        name = "findSupplierItemByItemAndSupplier",
        query = " select si from SupplierItem si where si.item = :item and si.supplier = :supplier"
        )
})
public class SupplierItem extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 6706868456977861841L;

	private Integer id;
	private Item item;
	private Supplier supplier;
	private String supplierItemCode;
	
	private String pCode;
	private String sCode;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "item_id")
	public Item getItem() {
		return item;
	}

	public void setItem(Item item) {
		this.item = item;
	}

	@ManyToOne
	@JoinColumn(name = "supplier_code")
	public Supplier getSupplier() {
		return supplier;
	}

	public void setSupplier(Supplier supplier) {
		this.supplier = supplier;
	}

	@Column(name= "supplier_item_code", nullable = true, length = 20)
	public String getSupplierItemCode() {
		return supplierItemCode;
	}

	public void setSupplierItemCode(String supplierItemCode) {
		this.supplierItemCode = supplierItemCode;
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
		return new HashCodeBuilder(-525173783, 1044460813).append(this.id)
				.toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof SupplierItem)) {
			return false;
		}
		SupplierItem rhs = (SupplierItem) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		SupplierItem myClass = (SupplierItem) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

	@Transient
	public String getpCode() {
		return pCode;
	}

	public void setpCode(String pCode) {
		this.pCode = pCode;
	}

	@Transient
	public String getsCode() {
		return sCode;
	}

	public void setsCode(String sCode) {
		this.sCode = sCode;
	}
}
