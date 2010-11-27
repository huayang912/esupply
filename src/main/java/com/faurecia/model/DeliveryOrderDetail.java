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
import javax.persistence.Transient;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.CompareToBuilder;

@Entity
@Table(name = "do_detail")
public class DeliveryOrderDetail extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 3412376376621262495L;
	private Integer id;
	private DeliveryOrder deliveryOrder;
	private String sequence;
	private Item item;
	private String itemDescription;
	private String supplierItemCode;
	private String uom;
	private BigDecimal unitCount;   //ORDER_LOT
	private BigDecimal qty;
	private BigDecimal orderedQty;
	private BigDecimal receivedQty;
	private String referenceOrderNo;
	private String referenceSequence;
	private ScheduleItemDetail scheduleItemDetail;
	private PurchaseOrderDetail purchaseOrderDetail;
	private Integer label; 
	
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
	public DeliveryOrder getDeliveryOrder() {
		return deliveryOrder;
	}

	public void setDeliveryOrder(DeliveryOrder deliveryOrder) {
		this.deliveryOrder = deliveryOrder;
	}

	@Column(nullable = false, length = 10)
	public String getSequence() {
		return sequence;
	}

	public void setSequence(String sequence) {
		this.sequence = sequence;
	}

	@ManyToOne
	@JoinColumn(name = "item_code", nullable=false)
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

	@Column(nullable = true, precision = 18, scale = 4)
	public BigDecimal getQty() {
		return qty;
	}

	public void setQty(BigDecimal qty) {
		this.qty = qty;
	}
	
	@Column(name="order_Qty", precision = 18, scale = 4)
	public BigDecimal getOrderedQty() {
		return orderedQty;
	}

	public void setOrderedQty(BigDecimal orderedQty) {
		this.orderedQty = orderedQty;
	}
	
	@Column(name="receive_Qty", precision = 18, scale = 4)
	public BigDecimal getReceivedQty() {
		return receivedQty;
	}

	public void setReceivedQty(BigDecimal receivedQty) {
		this.receivedQty = receivedQty;
	}
	
	@Transient
	public BigDecimal getOrderQty() {
		if (scheduleItemDetail != null) {
			return scheduleItemDetail.getReleaseQty();
		} else if (purchaseOrderDetail != null) {
			return purchaseOrderDetail.getQty();
		}
		
		return BigDecimal.ZERO;
	}
	
	@Transient
	public BigDecimal getDeliverQty() {
		if (scheduleItemDetail != null) {
			return scheduleItemDetail.getDeliverQty();
		} else if (purchaseOrderDetail != null) {
			return purchaseOrderDetail.getShipQty();
		}
		
		return BigDecimal.ZERO;
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
	
	@ManyToOne
	@JoinColumn(name = "schedule_item_detail_id", nullable=true)
	public ScheduleItemDetail getScheduleItemDetail() {
		return scheduleItemDetail;
	}

	public void setScheduleItemDetail(ScheduleItemDetail scheduleItemDetail) {
		this.scheduleItemDetail = scheduleItemDetail;
	}

	@ManyToOne
	@JoinColumn(name = "po_detail_id", nullable=true)
	public PurchaseOrderDetail getPurchaseOrderDetail() {
		return purchaseOrderDetail;
	}

	public void setPurchaseOrderDetail(PurchaseOrderDetail purchaseOrderDetail) {
		this.purchaseOrderDetail = purchaseOrderDetail;
	}
	
	@Column(name = "label")
	public Integer getLabel() {
		return label;
	}

	public void setLabel(Integer label) {
		this.label = label;
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
		if (!(object instanceof DeliveryOrderDetail)) {
			return false;
		}
		DeliveryOrderDetail rhs = (DeliveryOrderDetail) object;
		return new EqualsBuilder().append(
				this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		DeliveryOrderDetail myClass = (DeliveryOrderDetail) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

}
