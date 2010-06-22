package com.faurecia.model;

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
@Table(name = "notice_reader")
public class NoticeReader extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 8352538604149249061L;

	private Integer id;
	private Notice notice;
	private PlantSupplier plantSupplier;
	private Boolean isRead;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "notice_id")
	public Notice getNotice() {
		return notice;
	}

	public void setNotice(Notice notice) {
		this.notice = notice;
	}

	@ManyToOne
	@JoinColumn(name = "plant_supplier_id")
	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}

	@Column(name = "is_read", nullable = true)
	public Boolean isRead() {
		return isRead;
	}

	public void setRead(Boolean isRead) {
		this.isRead = isRead;
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
		return new HashCodeBuilder(957528315, -1505351505).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof NoticeReader)) {
			return false;
		}
		NoticeReader rhs = (NoticeReader) object;
		return new EqualsBuilder().append(
				this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		NoticeReader myClass = (NoticeReader) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

}
