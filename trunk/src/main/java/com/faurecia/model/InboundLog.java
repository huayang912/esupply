package com.faurecia.model;

import java.util.Date;

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

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.CompareToBuilder;

@Entity
@Table(name = "inbound_log")
@NamedQueries ({
	@NamedQuery(
	        name = "findInboundLogByDataTypeAndFileName",
	        query = " select il from InboundLog il where il.dataType = :dataType and il.fileName = :fileName"
	        )	       
})
public class InboundLog extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = -18012686953934265L;

	private Integer id;
	private PlantSupplier plantSupplier;
	private String dataType;
	private String fileName;
	private String fullFilePath;
	private Date createDate;
	private String createUser;
	private Date lastModifyDate;
	private String lastModifyUser;
	private String inboundResult;
	private String memo;
	private Date createDateFrom;
	private Date createDateTo;
	private String plantCode;
	private String supplierCode;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "plant_supplier_id")
	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}

	@Column(name="data_type",nullable=false,length=50)
	public String getDataType() {
		return dataType;
	}

	public void setDataType(String dataType) {
		this.dataType = dataType;
	}

	@Column(name="file_name",nullable=false,length=255)
	public String getFileName() {
		return fileName;
	}

	public void setFileName(String fileName) {
		this.fileName = fileName;
	}

	@Column(name="full_file_path",nullable=true,length=255)
	public String getFullFilePath() {
		return fullFilePath;
	}

	public void setFullFilePath(String fullFilePath) {
		this.fullFilePath = fullFilePath;
	}

	@Column(name="create_date",nullable=false)
	public Date getCreateDate() {
		return createDate;
	}

	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}

	@Column(name="create_user",nullable=false,length=50)
	public String getCreateUser() {
		return createUser;
	}

	public void setCreateUser(String createUser) {
		this.createUser = createUser;
	}

	@Column(name="last_modify_date",nullable=false)
	public Date getLastModifyDate() {
		return lastModifyDate;
	}

	public void setLastModifyDate(Date lastModifyDate) {
		this.lastModifyDate = lastModifyDate;
	}

	@Column(name="last_modify_user",nullable=false,length=50)
	public String getLastModifyUser() {
		return lastModifyUser;
	}

	public void setLastModifyUser(String lastModifyUser) {
		this.lastModifyUser = lastModifyUser;
	}

	@Column(name="inbound_result",nullable=false,length=20)
	public String getInboundResult() {
		return inboundResult;
	}

	public void setInboundResult(String inboundResult) {
		this.inboundResult = inboundResult;
	}

	@Column(name="memo",nullable=true,length=2000)
	public String getMemo() {
		return memo;
	}

	public void setMemo(String memo) {
		this.memo = memo;
	}
	@Transient
	public Date getCreateDateFrom() {
		return createDateFrom;
	}

	public void setCreateDateFrom(Date createDateFrom) {
		this.createDateFrom = createDateFrom;
	}

	@Transient
	public Date getCreateDateTo() {
		return createDateTo;
	}

	public void setCreateDateTo(Date createDateTo) {
		this.createDateTo = createDateTo;
	}
	
	@Transient
	public String getReImport() {
		if ("success".equals(this.inboundResult))
		{
			return "";
		}
		else
		{
			return "Import";
		}
	}
	
	@Transient
	public String getPlantCode() {
		return plantCode;
	}

	public void setPlantCode(String plantCode) {
		this.plantCode = plantCode;
	}

	@Transient
	public String getSupplierCode() {
		return supplierCode;
	}

	public void setSupplierCode(String supplierCode) {
		this.supplierCode = supplierCode;
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(-1116344111, 627326659).append(this.id)
				.toHashCode();
	}	

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof InboundLog)) {
			return false;
		}
		InboundLog rhs = (InboundLog) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		InboundLog myClass = (InboundLog) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this)
				.append("id", this.id).toString();
	}
}
