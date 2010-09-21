package com.faurecia.model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.persistence.Transient;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "receipt")
public class Receipt extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = 8525330490366535673L;
	private String receiptNo;
	private PlantSupplier plantSupplier;
	private Date postingDate;
	private Date postingDateFrom;
	private Date postingDateTo;
	private List<ReceiptDetail> receiptDetailList;
	private String itemCode;
	private String plusMinus;
	private String detailOrSummary;
	private String supplierItemCode;
	private String referenceOrderNo;
	private String referenceReceiptNoLong;
	private String referenceReceiptNo;

	@Id
	@Column(name = "receipt_no", length = 20)
	public String getReceiptNo() {
		return receiptNo;
	}

	public void setReceiptNo(String receiptNo) {
		this.receiptNo = receiptNo;
	}
	
	@ManyToOne
	@JoinColumn(name = "plant_supplier_id")
	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}
	
	@Column(name = "posting_date", nullable = true)
	public Date getPostingDate() {
		return postingDate;
	}

	public void setPostingDate(Date postingDate) {
		this.postingDate = postingDate;
	}

	@OneToMany(cascade = CascadeType.ALL, mappedBy = "receipt")
	public List<ReceiptDetail> getReceiptDetailList() {
		return receiptDetailList;
	}

	public void setReceiptDetailList(List<ReceiptDetail> receiptDetailList) {
		this.receiptDetailList = receiptDetailList;
	}

	public void addReceiptDetail(ReceiptDetail receiptDetail) {
		if (receiptDetailList == null) {
			receiptDetailList = new ArrayList<ReceiptDetail>();
		}

		receiptDetailList.add(receiptDetail);
	}
	
	@Column(name = "ref_receipt_no_long", length = 50)
	public String getReferenceReceiptNoLong() {
		return referenceReceiptNoLong;
	}

	public void setReferenceReceiptNoLong(String referenceReceiptNoLong) {
		this.referenceReceiptNoLong = referenceReceiptNoLong;
	}
	
	@Column(name = "ref_receipt_no", length = 20)
	public String getReferenceReceiptNo() {
		return referenceReceiptNo;
	}

	public void setReferenceReceiptNo(String referenceReceiptNo) {
		this.referenceReceiptNo = referenceReceiptNo;
	}
	
	@Transient
	public Date getPostingDateFrom() {
		return postingDateFrom;
	}

	public void setPostingDateFrom(Date postingDateFrom) {
		this.postingDateFrom = postingDateFrom;
	}

	@Transient
	public Date getPostingDateTo() {
		return postingDateTo;
	}

	public void setPostingDateTo(Date postingDateTo) {
		this.postingDateTo = postingDateTo;
	}

	@Transient
	public String getPlantCode() {
		return this.plantSupplier.getPlant().getCode();
	}
	
	@Transient
	public String getPlantName() {
		return this.plantSupplier.getPlant().getName();
	}
	
	@Transient
	public String getSupplierCode() {
		return this.plantSupplier.getSupplier().getCode();
	}
	
	@Transient
	public String getSupplierName() {
		return this.plantSupplier.getSupplierName();
	}
	
	@Transient
	public String getItemCode() {
		return itemCode;
	}

	public void setItemCode(String itemCode) {
		this.itemCode = itemCode;
	}

	@Transient
	public String getPlusMinus() {
		return plusMinus;
	}

	public void setPlusMinus(String plusMinus) {
		this.plusMinus = plusMinus;
	}

	@Transient
	public String getDetailOrSummary() {
		return detailOrSummary;
	}

	public void setDetailOrSummary(String detailOrSummary) {
		this.detailOrSummary = detailOrSummary;
	}

	@Transient
	public String getSupplierItemCode() {
		return supplierItemCode;
	}

	public void setSupplierItemCode(String supplierItemCode) {
		this.supplierItemCode = supplierItemCode;
	}

	@Transient
	public String getReferenceOrderNo() {
		return referenceOrderNo;
	}

	public void setReferenceOrderNo(String referenceOrderNo) {
		this.referenceOrderNo = referenceOrderNo;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("receiptNo", this.receiptNo)
				.toString();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(1784450709, 482339105).append(this.receiptNo).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof Receipt)) {
			return false;
		}
		Receipt rhs = (Receipt) object;
		return new EqualsBuilder().append(
				this.receiptNo, rhs.receiptNo).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		Receipt myClass = (Receipt) object;
		return new CompareToBuilder().append(this.receiptNo, myClass.receiptNo)
				.toComparison();
	}

}
