package com.faurecia.model;

import java.math.BigDecimal;
import java.util.Date;

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
@Table(name = "receipt_detail")
public class ReceiptDetail extends BaseObject {
	/**
	 * 
	 */
	private static final long serialVersionUID = -3880957536818984639L;
	private Integer id;
	private Receipt receipt;
	private Item item;
	private String itemDescription;
	private String supplierItemCode;
	private String uom;
	private BigDecimal qty;
	private String referenceOrderNo;
	private String referenceSequence;
	private String plusMinus;
	private String itemCode;
	private String plantCode;
	private String plantName;
	private String supplierCode;
	private String supplierName;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "receipt_no", nullable=true)
	public Receipt getReceipt() {
		return receipt;
	}

	public void setReceipt(Receipt receipt) {
		this.receipt = receipt;
	}

	@ManyToOne
	@JoinColumn(name = "item_code")
	public Item getItem() {
		return item;
	}

	public void setItem(Item item) {
		this.item = item;
	}

	@Column(name= "item_description", length = 50)
	public String getItemDescription() {
		if (itemDescription != null && itemDescription.trim().length() > 0) {
			return itemDescription;
		} else if (item != null) {
			return item.getDescription();
		}
		return itemDescription;
	}

	public void setItemDescription(String itemDescription) {
		this.itemDescription = itemDescription;
	}

	@Column(name= "supplier_item_code", length = 20)
	public String getSupplierItemCode() {
		return supplierItemCode;
	}

	public void setSupplierItemCode(String supplierItemCode) {
		this.supplierItemCode = supplierItemCode;
	}

	@Column(nullable = false, length = 5)
	public String getUom() {
		return uom;
	}

	public void setUom(String uom) {
		this.uom = uom;
	}
	
	@Column(nullable = false, precision = 18, scale = 4)
	public BigDecimal getQty() {
		return qty;
	}

	public void setQty(BigDecimal qty) {
		this.qty = qty;
	}
	
	@Column(name = "reference_order_no", nullable=false, length = 20)
	public String getReferenceOrderNo() {
		return referenceOrderNo;
	}

	public void setReferenceOrderNo(String referenceOrderNo) {
		this.referenceOrderNo = referenceOrderNo;
	}

	@Column(name = "reference_sequence", nullable=false, length = 10)
	public String getReferenceSequence() {
		return referenceSequence;
	}

	public void setReferenceSequence(String referenceSequence) {
		this.referenceSequence = referenceSequence;
	}

	@Column(name = "plus_minus", nullable=false, length = 10)
	public String getPlusMinus() {
		return plusMinus;
	}

	public void setPlusMinus(String plusMinus) {
		this.plusMinus = plusMinus;
	}	

	@Transient
	public String getItemCode() {
		return itemCode;
	}

	public void setItemCode(String itemCode) {
		this.itemCode = itemCode;
	}

	@Transient
	public String getPlantCode() {
		return plantCode;
	}

	public void setPlantCode(String plantCode) {
		this.plantCode = plantCode;
	}

	@Transient
	public String getPlantName() {
		return plantName;
	}

	public void setPlantName(String plantName) {
		this.plantName = plantName;
	}

	@Transient
	public String getSupplierCode() {
		return supplierCode;
	}

	public void setSupplierCode(String supplierCode) {
		this.supplierCode = supplierCode;
	}

	@Transient
	public String getSupplierName() {
		return supplierName;
	}

	public void setSupplierName(String supplierName) {
		this.supplierName = supplierName;
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
		return new HashCodeBuilder(180436277, 633449717).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof ReceiptDetail)) {
			return false;
		}
		ReceiptDetail rhs = (ReceiptDetail) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		ReceiptDetail myClass = (ReceiptDetail) object;
		return new CompareToBuilder().append(this.id, myClass.id).toComparison();
	}

}
