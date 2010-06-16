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
import javax.persistence.UniqueConstraint;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "po_approval_detail",
    uniqueConstraints={@UniqueConstraint(columnNames={"po_no", "sequence"})}
)
public class PurchaseOrderApprovalDetail extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private Integer id;
	private PurchaseOrderApproval purchaseOrderApproval;
	private String sequence;
	private Item item;
	private String itemDescription;
	private String supplierItemCode;
	private String uom;
	private BigDecimal qty;
	private Date deliveryDate;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}
	
	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "po_no", nullable=true)
	public PurchaseOrderApproval getPurchaseOrderApproval() {
		return purchaseOrderApproval;
	}

	public void setPurchaseOrderApproval(PurchaseOrderApproval purchaseOrderApproval) {
		this.purchaseOrderApproval = purchaseOrderApproval;
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

	@Column(nullable = false, precision = 9, scale = 2)
	public BigDecimal getQty() {
		return qty;
	}

	public void setQty(BigDecimal qty) {
		this.qty = qty;
	}

	@Column(name="delivery_date", nullable = false)
	public Date getDeliveryDate() {
		return deliveryDate;
	}

	public void setDeliveryDate(Date deliveryDate) {
		this.deliveryDate = deliveryDate;
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
		return new HashCodeBuilder(-704754133, 1431455743).append(this.id)
				.toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof PurchaseOrderApprovalDetail)) {
			return false;
		}
		PurchaseOrderApprovalDetail rhs = (PurchaseOrderApprovalDetail) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		PurchaseOrderApprovalDetail myClass = (PurchaseOrderApprovalDetail) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

	
}
