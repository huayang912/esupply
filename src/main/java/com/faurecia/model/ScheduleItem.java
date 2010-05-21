package com.faurecia.model;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.Table;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "schedule_item")
public class ScheduleItem extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 4094731904619615672L;

	private Integer id;
	private Schedule schedule;
	private String sequence;
	private Item item;
	private String itemDescription;
	private String supplierItemCode;
	private String uom;
	private BigDecimal receivedQty;
	private Integer releaseNo;
	private List<ScheduleItemDetail> scheduleItemDetailList;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "schedule_no", nullable=false)
	public Schedule getSchedule() {
		return schedule;
	}

	public void setSchedule(Schedule schedule) {
		this.schedule = schedule;
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

	@Column(name="received_qty", nullable = true, precision = 9, scale = 2)
	public BigDecimal getReceivedQty() {
		return receivedQty;
	}

	public void setReceivedQty(BigDecimal receivedQty) {
		this.receivedQty = receivedQty;
	}

	@Column(name="release_no", nullable = true)
	public Integer getReleaseNo() {
		return releaseNo;
	}

	public void setReleaseNo(Integer releaseNo) {
		this.releaseNo = releaseNo;
	}

	@OneToMany(cascade = CascadeType.ALL, mappedBy = "scheduleItem")
	public List<ScheduleItemDetail> getScheduleItemDetailList() {
		return scheduleItemDetailList;
	}

	public void setScheduleItemDetailList(List<ScheduleItemDetail> scheduleItemDetailList) {
		this.scheduleItemDetailList = scheduleItemDetailList;
	}
	
	public void addScheduleItemDetail(ScheduleItemDetail scheduleItemDetail) {
		if (scheduleItemDetailList == null) {
			scheduleItemDetailList = new ArrayList<ScheduleItemDetail>();
		}

		scheduleItemDetailList.add(scheduleItemDetail);
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
		return new HashCodeBuilder(1030150661, -1810211589).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof ScheduleItem)) {
			return false;
		}
		ScheduleItem rhs = (ScheduleItem) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		ScheduleItem myClass = (ScheduleItem) object;
		return new CompareToBuilder().append(this.id, myClass.id).toComparison();
	}

}
