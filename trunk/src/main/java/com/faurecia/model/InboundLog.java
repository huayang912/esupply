package com.faurecia.model;

import java.util.Date;

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
@Table(name = "inbound_log")
public class InboundLog extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = -18012686953934265L;

	private Integer id;
	private Plant plant;
	private Supplier supplier;
	private String dataType;
	private String fileName;
	private String fullFilePath;
	private Date createDate;
	private String createUser;
	private Date lastModifyDate;
	private String lastModifyUser;
	private String inboundResult;
	private String memo;
	
	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "plant_code", nullable=true)
	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}

	@ManyToOne
	@JoinColumn(name = "supplier_code", nullable=true)
	public Supplier getSupplier() {
		return supplier;
	}

	public void setSupplier(Supplier supplier) {
		this.supplier = supplier;
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

	@Column(name="full_file_path",nullable=false,length=255)
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

	@Column(name="memo",nullable=true,length=255)
	public String getMemo() {
		return memo;
	}

	public void setMemo(String memo) {
		this.memo = memo;
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
