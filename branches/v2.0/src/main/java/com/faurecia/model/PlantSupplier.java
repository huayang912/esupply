package com.faurecia.model;

import java.io.Serializable;

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
import javax.persistence.UniqueConstraint;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "plant_supplier", uniqueConstraints = { @UniqueConstraint(columnNames = { "supplier_code", "plant_code" }) })
@NamedQueries( {
		@NamedQuery(name = "findPlantSupplierByPlantAndSupplier", query = " select ps from PlantSupplier ps where ps.plant = :plant and ps.supplier = :supplier"),
		@NamedQuery(name = "findPlantSupplierBySupplierCode", query = " select ps from PlantSupplier ps inner join ps.supplier s where s.code = :supplierCode"),
		@NamedQuery(name = "findPlantSupplierByPlantCode", query = " select ps from PlantSupplier ps inner join ps.plant p where p.code = :plantCode"),
		@NamedQuery(name = "findPlantSupplierByPlantScheduleGroupId", query = " select ps from PlantSupplier ps inner join ps.plantScheduleGroup s where s.id = :plantScheduleGroupId"),
		@NamedQuery(name = "findPlantSupplierByUserId", query = " select ps from PlantSupplier ps inner join ps.responsibleUser ru where ru.id = :userId")})
public class PlantSupplier extends BaseObject implements Serializable {

	/**
	 * 
	 */
	private static final long serialVersionUID = -6616983854718294391L;

	private Integer id;
	private Supplier supplier;
	private Plant plant;
	private String supplierName;
	private String supplierAddress1;
	private String supplierAddress2;
	private String supplierContactPerson;
	private String supplierPhone;
	private String supplierFax;
	private String doNoPrefix;
	private PlantScheduleGroup plantScheduleGroup;
	private User responsibleUser;
	private String doTemplateName;
	private String boxTemplateName;
	private Boolean needExportDo;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@ManyToOne
	@JoinColumn(name = "supplier_code", nullable = false)
	public Supplier getSupplier() {
		return supplier;
	}

	public void setSupplier(Supplier supplier) {
		this.supplier = supplier;
	}

	@ManyToOne
	@JoinColumn(name = "plant_code", nullable = false)
	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}

	@Column(name = "supplier_name", nullable = true, length = 50)
	public String getSupplierName() {
		if (supplierName != null && supplierName.trim().length() > 0) {
			return supplierName;
		} else if (supplier != null) {
			return supplier.getName();
		}
		return supplierName;
	}

	public void setSupplierName(String supplierName) {
		this.supplierName = supplierName;
	}

	@Column(name = "supplier_address1", nullable = true, length = 255)
	public String getSupplierAddress1() {
		return supplierAddress1;
	}

	public void setSupplierAddress1(String supplierAddress1) {
		this.supplierAddress1 = supplierAddress1;
	}

	@Column(name = "supplier_address2", nullable = true, length = 255)
	public String getSupplierAddress2() {
		return supplierAddress2;
	}

	public void setSupplierAddress2(String supplierAddress2) {
		this.supplierAddress2 = supplierAddress2;
	}

	@Column(name = "supplier_contact_person", nullable = true, length = 50)
	public String getSupplierContactPerson() {
		return supplierContactPerson;
	}

	public void setSupplierContactPerson(String supplierContactPerson) {
		this.supplierContactPerson = supplierContactPerson;
	}

	@Column(name = "supplier_phone", nullable = true, length = 50)
	public String getSupplierPhone() {
		return supplierPhone;
	}

	public void setSupplierPhone(String supplierPhone) {
		this.supplierPhone = supplierPhone;
	}

	@Column(name = "supplier_fax", nullable = true, length = 50)
	public String getSupplierFax() {
		return supplierFax;
	}

	public void setSupplierFax(String supplierFax) {
		this.supplierFax = supplierFax;
	}

	@Column(name = "do_no_prefix", length = 4, nullable = false)
	public String getDoNoPrefix() {
		return doNoPrefix;
	}

	public void setDoNoPrefix(String doNoPrefix) {
		this.doNoPrefix = doNoPrefix;
	}

	@ManyToOne
	@JoinColumn(name = "plant_schedule_group_id", nullable = true)
	public PlantScheduleGroup getPlantScheduleGroup() {
		return plantScheduleGroup;
	}

	public void setPlantScheduleGroup(PlantScheduleGroup plantScheduleGroup) {
		this.plantScheduleGroup = plantScheduleGroup;
	}

	@ManyToOne
	@JoinColumn(name = "responsible_user", nullable = true)
	public User getResponsibleUser() {
		return responsibleUser;
	}

	public void setResponsibleUser(User responsibleUser) {
		this.responsibleUser = responsibleUser;
	}

	@Column(name = "do_template_name", nullable = true, length = 255)
	public String getDoTemplateName() {
		return doTemplateName;
	}

	public void setDoTemplateName(String doTemplateName) {
		this.doTemplateName = doTemplateName;
	}

	@Column(name = "box_template_name", nullable = true, length = 255)
	public String getBoxTemplateName() {
		return boxTemplateName;
	}

	public void setBoxTemplateName(String boxTemplateName) {
		this.boxTemplateName = boxTemplateName;
	}
	
	@Column(name = "need_export_do", nullable = false)
	public Boolean getNeedExportDo() {
		return needExportDo;
	}

	public void setNeedExportDo(Boolean needExportDo) {
		this.needExportDo = needExportDo;
	}
	
	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		PlantSupplier myClass = (PlantSupplier) object;
		return new CompareToBuilder().append(this.id, myClass.id).toComparison();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof PlantSupplier)) {
			return false;
		}
		PlantSupplier rhs = (PlantSupplier) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(-1510998443, -783399761).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("id", this.id).toString();
	}
}
