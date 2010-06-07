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
@Table(name = "schedule_item_detail")
public class ScheduleItemDetail extends BaseObject {
	
	/**
	 * 
	 */
	private static final long serialVersionUID = 2086678568571563361L;
	private Integer id;
	private ScheduleItem scheduleItem;
	private String scheduleType;
	private String dateType;
	private Date dateFrom;
	private Date dateTo;
	private BigDecimal releaseQty;
	private BigDecimal deliverQty;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "schedule_item_id", nullable=true)
	public ScheduleItem getScheduleItem() {
		return scheduleItem;
	}

	public void setScheduleItem(ScheduleItem scheduleItem) {
		this.scheduleItem = scheduleItem;
	}

	@Column(name="schedule_type", nullable = false, length = 50)
	public String getScheduleType() {
		return scheduleType;
	}

	public void setScheduleType(String scheduleType) {
		this.scheduleType = scheduleType;
	}

	@Column(name="date_type", nullable = false, length = 10)
	public String getDateType() {
		return dateType;
	}

	public void setDateType(String dateType) {
		this.dateType = dateType;
	}

	@Column(name = "date_from", nullable = false)
	public Date getDateFrom() {
		return dateFrom;
	}

	public void setDateFrom(Date dateFrom) {
		this.dateFrom = dateFrom;
	}

	@Column(name = "date_to", nullable = false)
	public Date getDateTo() {
		return dateTo;
	}

	public void setDateTo(Date dateTo) {
		this.dateTo = dateTo;
	}

	@Column(name="release_qty", nullable = false, precision = 9, scale = 2)
	public BigDecimal getReleaseQty() {
		return releaseQty;
	}

	public void setReleaseQty(BigDecimal releaseQty) {
		this.releaseQty = releaseQty;
	}

	@Column(name="deliver_qty", nullable = true, precision = 9, scale = 2)
	public BigDecimal getDeliverQty() {
		return deliverQty;
	}

	public void setDeliverQty(BigDecimal deliverQty) {
		this.deliverQty = deliverQty;
	}
	
	@Transient
	public BigDecimal getRemainQty() {
		if (deliverQty != null) {
			return releaseQty.subtract(deliverQty).compareTo(BigDecimal.ZERO) > 0 ?
				releaseQty.subtract(deliverQty) : BigDecimal.ZERO;
		}
		else {
			return releaseQty;
		}
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
		if (!(object instanceof ScheduleItemDetail)) {
			return false;
		}
		ScheduleItemDetail rhs = (ScheduleItemDetail) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		ScheduleItemDetail myClass = (ScheduleItemDetail) object;
		return new CompareToBuilder().append(this.id, myClass.id).toComparison();
	}
}
