package com.faurecia.model;

import java.math.BigDecimal;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.CompareToBuilder;

@Entity
@Table(name = "do_detail")
public class DeliverOrderDetail extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3412376376621262495L;
	private Integer id;
	private DeliverOrder deliverOrder;
	private String sequence;
	private Item item;
	private String itemDescription;
	private String supplierItemCode;
	private String uom;
	private BigDecimal unitCount;
	private BigDecimal qty;
	private BigDecimal orderQty;
	private String referenceOrderNo;
	private String referenceSequence;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}
	
	@ManyToOne
	@JoinColumn(name = "do_no", nullable=true)
	public DeliverOrder getDeliverOrder() {
		return deliverOrder;
	}

	public void setDeliverOrder(DeliverOrder deliverOrder) {
		this.deliverOrder = deliverOrder;
	}

	@Column(nullable = false, length = 10)
	public String getSequence() {
		return sequence;
	}

	public void setSequence(String sequence) {
		this.sequence = sequence;
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
	
	@Column(name="unit_count", nullable = true, precision = 9, scale = 2)
	public BigDecimal getUnitCount() {
		return unitCount;
	}

	public void setUnitCount(BigDecimal unitCount) {
		this.unitCount = unitCount;
	}

	@Column(nullable = false, precision = 9, scale = 2)
	public BigDecimal getQty() {
		return qty;
	}

	public void setQty(BigDecimal qty) {
		this.qty = qty;
	}
	
	@Column(name="order_qty", nullable = false, precision = 9, scale = 2)
	public BigDecimal getOrderQty() {
		return orderQty;
	}

	public void setOrderQty(BigDecimal orderQty) {
		this.orderQty = orderQty;
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
		return new HashCodeBuilder(-471242143, 859352517).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof DeliverOrderDetail)) {
			return false;
		}
		DeliverOrderDetail rhs = (DeliverOrderDetail) object;
		return new EqualsBuilder().append(
				this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		DeliverOrderDetail myClass = (DeliverOrderDetail) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

}
