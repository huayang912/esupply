package com.faurecia.model;

import java.math.BigDecimal;
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
@Table(name = "do")
public class DeliveryOrder extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7456872599350319130L;

	private String doNo; // MANIFEST CODE
	private String externalDoNo;
	private PlantSupplier plantSupplier;
	private String plantName;
	private String plantAddress1;
	private String plantAddress2;
	private String plantAddress3;
	private String plantContactPerson;
	private String plantPhone;
	private String plantFax;
	private String supplierName;
	private String supplierAddress1;
	private String supplierAddress2;
	private String supplierAddress3;
	private String supplierContactPerson;
	private String supplierPhone;
	private String supplierFax;
	private Date createDate;
	private Date createDateFrom;
	private Date createDateTo;
	private Date startDate; // DATETIME_PICKUP
	private Date endDate; // DATETIME_RECEPTION
	private Boolean isExport;
	private List<DeliveryOrderDetail> deliveryOrderDetailList;
	private Boolean allowOverQty;
	private String status;
	private String exportFlag;
	private String readFlag;
	private String printFlag;
	private Boolean isPrint;
	private Boolean isRead;
	private Date firstReadDate;

	private String murn; // code + bar code
	private String OrderGroup; // MANIFEST ORDER GROUP
	private String deliveryOrderGroup; // DELIVERY ORDER GROUPk
	private String dock;
	private String route;
	private String mainRoute;
	private BigDecimal totalWeight;
	private String unitWeight;
	private BigDecimal totalVolume;
	private String unitVolume;
	private BigDecimal totalNbPallets;
	private String title;
	private Boolean isLogisticPartner;
	private String fileIdentitfier;

	private String plantCode;
	private String supplierCode;

	private String supplierPostCode;
	private String supplierCity;
	private String supplierCountry;
	
	private String plantPostCode;
	private String plantCity;
	private String plantCountry;

	private String pCode;
	private String sCode;
	
	@Id
	@Column(name = "do_no", length = 10)
	public String getDoNo() {
		return doNo;
	}

	public void setDoNo(String doNo) {
		this.doNo = doNo;
	}

	@Column(name = "ext_do_no", nullable = false, length = 20, unique = true)
	public String getExternalDoNo() {
		return externalDoNo;
	}

	public void setExternalDoNo(String externalDoNo) {
		this.externalDoNo = externalDoNo;
	}

	@ManyToOne
	@JoinColumn(name = "plant_supplier_id", nullable = false)
	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}

	@Transient
	public String getPlantCode() {
		if (this.plantSupplier != null) {
			return this.plantSupplier.getPlant().getCode();
		}
		return plantCode;
	}

	@Transient
	public Boolean getIsLogisticPartner() {
		return isLogisticPartner;
	}

	public void setIsLogisticPartner(Boolean isLogisticPartner) {
		this.isLogisticPartner = isLogisticPartner;
	}

	@Column(name = "plant_name", nullable = false, length = 50)
	public String getPlantName() {
		if (plantName != null && plantName.trim().length() > 0) {
			return plantName;
		} else if (plantSupplier != null) {
			return plantSupplier.getPlant().getName();
		}
		return plantName;
	}

	public void setPlantName(String plantName) {
		this.plantName = plantName;
	}

	@Column(name = "plant_address1", nullable = true, length = 255)
	public String getPlantAddress1() {
		if (plantAddress1 != null && plantAddress1.trim().length() > 0) {
			return plantAddress1;
		} else if (plantSupplier != null) {
			return plantSupplier.getPlant().getAddress1();
		}
		return plantAddress1;
	}

	public void setPlantAddress1(String plantAddress1) {
		this.plantAddress1 = plantAddress1;
	}

	@Column(name = "plant_address2", nullable = true, length = 255)
	public String getPlantAddress2() {
		if (plantAddress2 != null && plantAddress2.trim().length() > 0) {
			return plantAddress2;
		} else if (plantSupplier != null) {
			return plantSupplier.getPlant().getAddress2();
		}
		return plantAddress2;
	}

	public void setPlantAddress2(String plantAddress2) {
		this.plantAddress2 = plantAddress2;
	}

	@Column(name = "plant_address3", nullable = true, length = 255)
	public String getPlantAddress3() {
		return plantAddress3;
	}

	public void setPlantAddress3(String plantAddress3) {
		this.plantAddress3 = plantAddress3;
	}

	@Column(name = "plant_contact_person", nullable = true, length = 50)
	public String getPlantContactPerson() {
		if (plantContactPerson != null && plantContactPerson.trim().length() > 0) {
			return plantContactPerson;
		} else if (plantSupplier != null) {
			return plantSupplier.getPlant().getContactPerson();
		}
		return plantContactPerson;
	}

	public void setPlantContactPerson(String plantContactPerson) {
		this.plantContactPerson = plantContactPerson;
	}

	@Column(name = "plant_phone", nullable = true, length = 50)
	public String getPlantPhone() {
		if (plantPhone != null && plantPhone.trim().length() > 0) {
			return plantPhone;
		} else if (plantSupplier != null) {
			return plantSupplier.getPlant().getPhone();
		}
		return plantPhone;
	}

	public void setPlantPhone(String plantPhone) {
		this.plantPhone = plantPhone;
	}

	@Column(name = "plant_fax", nullable = true, length = 50)
	public String getPlantFax() {
		if (plantFax != null && plantFax.trim().length() > 0) {
			return plantFax;
		} else if (plantSupplier != null) {
			return plantSupplier.getPlant().getFax();
		}
		return plantFax;
	}

	public void setPlantFax(String plantFax) {
		this.plantFax = plantFax;
	}

	@Transient
	public String getSupplierCode() {
		if (this.plantSupplier != null) {
			return this.plantSupplier.getSupplier().getCode();
		}
		return supplierCode;
	}

	@Column(name = "supplier_name", nullable = true, length = 50)
	public String getSupplierName() {
		if (supplierName != null && supplierName.trim().length() > 0) {
			return supplierName;
		} else if (plantSupplier != null) {
			return plantSupplier.getSupplierName();
		}
		return supplierName;
	}

	public void setSupplierName(String supplierName) {
		this.supplierName = supplierName;
	}

	@Column(name = "supplier_address1", nullable = true, length = 255)
	public String getSupplierAddress1() {
		if (supplierAddress1 != null && supplierAddress1.trim().length() > 0) {
			return supplierAddress1;
		} else if (plantSupplier != null) {
			return plantSupplier.getSupplierAddress1();
		}
		return supplierAddress1;
	}

	public void setSupplierAddress1(String supplierAddress1) {
		this.supplierAddress1 = supplierAddress1;
	}

	@Column(name = "supplier_address2", nullable = true, length = 255)
	public String getSupplierAddress2() {
		if (supplierAddress2 != null && supplierAddress2.trim().length() > 0) {
			return supplierAddress2;
		} else if (plantSupplier != null) {
			return plantSupplier.getSupplierAddress2();
		}
		return supplierAddress2;
	}

	public void setSupplierAddress2(String supplierAddress2) {
		this.supplierAddress2 = supplierAddress2;
	}

	@Column(name = "supplier_address3", nullable = true, length = 255)
	public String getSupplierAddress3() {
		return supplierAddress3;
	}

	public void setSupplierAddress3(String supplierAddress3) {
		this.supplierAddress3 = supplierAddress3;
	}

	@Column(name = "supplier_contact_person", nullable = true, length = 50)
	public String getSupplierContactPerson() {
		if (supplierContactPerson != null && supplierContactPerson.trim().length() > 0) {
			return supplierContactPerson;
		} else if (plantSupplier != null) {
			return plantSupplier.getSupplierContactPerson();
		}
		return supplierContactPerson;
	}

	public void setSupplierContactPerson(String supplierContactPerson) {
		this.supplierContactPerson = supplierContactPerson;
	}

	@Column(name = "supplier_phone", nullable = true, length = 50)
	public String getSupplierPhone() {
		if (supplierPhone != null && supplierPhone.trim().length() > 0) {
			return supplierPhone;
		} else if (plantSupplier != null) {
			return plantSupplier.getSupplierPhone();
		}
		return supplierPhone;
	}

	public void setSupplierPhone(String supplierPhone) {
		this.supplierPhone = supplierPhone;
	}

	@Column(name = "supplier_fax", nullable = true, length = 50)
	public String getSupplierFax() {
		if (supplierFax != null && supplierFax.trim().length() > 0) {
			return supplierFax;
		} else if (plantSupplier != null) {
			return plantSupplier.getSupplierFax();
		}
		return supplierFax;
	}

	public void setSupplierFax(String supplierFax) {
		this.supplierFax = supplierFax;
	}

	@Column(name = "create_date", nullable = false)
	public Date getCreateDate() {
		return createDate;
	}

	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}

	@Column(name = "start_date", nullable = true)
	public Date getStartDate() {
		return startDate;
	}

	public void setStartDate(Date startDate) {
		this.startDate = startDate;
	}

	@Column(name = "end_date", nullable = true)
	public Date getEndDate() {
		return endDate;
	}

	public void setEndDate(Date endDate) {
		this.endDate = endDate;
	}

	@Column(name = "is_export", nullable = true)
	public Boolean getIsExport() {
		return isExport;
	}

	public void setIsExport(Boolean isExport) {
		this.isExport = isExport;
	}

	@Column(name = "is_print", nullable = true)
	public Boolean getIsPrint() {
		return isPrint;
	}

	public void setIsPrint(Boolean isPrint) {
		this.isPrint = isPrint;
	}

	@Column(name = "is_read", nullable = true)
	public Boolean getIsRead() {
		return isRead;
	}

	public void setIsRead(Boolean isRead) {
		this.isRead = isRead;
	}

	@Column(name = "first_read_date", nullable = true)
	public Date getFirstReadDate() {
		return firstReadDate;
	}

	public void setFirstReadDate(Date firstReadDate) {
		this.firstReadDate = firstReadDate;
	}

	@OneToMany(cascade = CascadeType.ALL, mappedBy = "deliveryOrder")
	public List<DeliveryOrderDetail> getDeliveryOrderDetailList() {
		return deliveryOrderDetailList;
	}

	public void setDeliveryOrderDetailList(List<DeliveryOrderDetail> deliveryOrderDetailList) {
		this.deliveryOrderDetailList = deliveryOrderDetailList;
	}

	public void addDeliveryOrderDetail(DeliveryOrderDetail deliveryOrderDetail) {
		if (deliveryOrderDetailList == null) {
			deliveryOrderDetailList = new ArrayList<DeliveryOrderDetail>();
		}

		deliveryOrderDetailList.add(deliveryOrderDetail);
	}

	@Column(name = "status", nullable = false, length = 10)
	public String getStatus() {
		return status;
	}

	public void setStatus(String status) {
		this.status = status;
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
	public Boolean getAllowOverQty() {
		return allowOverQty;
	}

	public void setAllowOverQty(Boolean allowOverQty) {
		this.allowOverQty = allowOverQty;
	}

	@Transient
	public String getExportFlag() {
		return exportFlag;
	}

	public void setExportFlag(String exportFlag) {
		this.exportFlag = exportFlag;
	}

	@Transient
	public String getPrintFlag() {
		return printFlag;
	}

	public void setPrintFlag(String printFlag) {
		this.printFlag = printFlag;
	}

	@Transient
	public String getReadFlag() {
		return readFlag;
	}

	public void setReadFlag(String readFlag) {
		this.readFlag = readFlag;
	}

	@Column(name = "murn", length = 20)
	public String getMurn() {
		return murn;
	}

	public void setMurn(String murn) {
		this.murn = murn;
	}

	@Column(name = "order_group", length = 20)
	public String getOrderGroup() {
		return OrderGroup;
	}

	public void setOrderGroup(String orderGroup) {
		OrderGroup = orderGroup;
	}

	@Column(name = "delivery_order_group", length = 20)
	public String getDeliveryOrderGroup() {
		return deliveryOrderGroup;
	}

	public void setDeliveryOrderGroup(String deliveryOrderGroup) {
		this.deliveryOrderGroup = deliveryOrderGroup;
	}

	@Column(name = "dock", length = 20)
	public String getDock() {
		return dock;
	}

	public void setDock(String dock) {
		this.dock = dock;
	}

	@Column(name = "route", length = 20)
	public String getRoute() {
		return route;
	}

	public void setRoute(String route) {
		this.route = route;
	}

	@Column(name = "main_route", length = 20)
	public String getMainRoute() {
		return mainRoute;
	}

	public void setMainRoute(String mainRoute) {
		this.mainRoute = mainRoute;
	}

	@Column(name = "total_weight", precision = 18, scale = 4)
	public BigDecimal getTotalWeight() {
		return totalWeight;
	}

	public void setTotalWeight(BigDecimal totalWeight) {
		this.totalWeight = totalWeight;
	}

	@Column(name = "unit_weight", length = 10)
	public String getUnitWeight() {
		return unitWeight;
	}

	public void setUnitWeight(String unitWeight) {
		this.unitWeight = unitWeight;
	}

	@Column(name = "total_volume", precision = 18, scale = 4)
	public BigDecimal getTotalVolume() {
		return totalVolume;
	}

	public void setTotalVolume(BigDecimal totalVolume) {
		this.totalVolume = totalVolume;
	}

	@Column(name = "unit_volume", length = 10)
	public String getUnitVolume() {
		return unitVolume;
	}

	public void setUnitVolume(String unitVolume) {
		this.unitVolume = unitVolume;
	}

	@Column(name = "total_nb_pallets", precision = 18, scale = 4)
	public BigDecimal getTotalNbPallets() {
		return totalNbPallets;
	}

	public void setTotalNbPallets(BigDecimal totalNbPallets) {
		this.totalNbPallets = totalNbPallets;
	}

	@Column(name = "title", length = 50)
	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	@Column(name = "file_id", length = 50)
	public String getFileIdentitfier() {
		return fileIdentitfier;
	}

	public void setFileIdentitfier(String fileIdentitfier) {
		this.fileIdentitfier = fileIdentitfier;
	}

	public void setPlantCode(String plantCode) {
		this.plantCode = plantCode;
	}

	public void setSupplierCode(String supplierCode) {
		this.supplierCode = supplierCode;
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("doNo", this.doNo).toString();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(1818782001, -1029300457).append(this.doNo).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof DeliveryOrder)) {
			return false;
		}
		DeliveryOrder rhs = (DeliveryOrder) object;
		return new EqualsBuilder().append(this.doNo, rhs.doNo).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		DeliveryOrder myClass = (DeliveryOrder) object;
		return new CompareToBuilder().append(this.doNo, myClass.doNo).toComparison();
	}

	@Column(name = "supplier_post_code", length = 50)
	public String getSupplierPostCode() {
		return supplierPostCode;
	}

	public void setSupplierPostCode(String supplierPostCode) {
		this.supplierPostCode = supplierPostCode;
	}

	@Column(name = "supplier_city", length = 50)
	public String getSupplierCity() {
		return supplierCity;
	}

	public void setSupplierCity(String supplierCity) {
		this.supplierCity = supplierCity;
	}

	@Column(name = "supplier_country", length = 50)
	public String getSupplierCountry() {
		return supplierCountry;
	}

	public void setSupplierCountry(String supplierCountry) {
		this.supplierCountry = supplierCountry;
	}

	@Column(name = "plant_post_code", length = 50)
	public String getPlantPostCode() {
		return plantPostCode;
	}

	public void setPlantPostCode(String plantPostCode) {
		this.plantPostCode = plantPostCode;
	}

	@Column(name = "plant_city", length = 50)
	public String getPlantCity() {
		return plantCity;
	}

	public void setPlantCity(String plantCity) {
		this.plantCity = plantCity;
	}

	@Column(name = "plant_country", length = 50)
	public String getPlantCountry() {
		return plantCountry;
	}

	public void setPlantCountry(String plantCountry) {
		this.plantCountry = plantCountry;
	}

	@Transient
	public String getpCode() {
		return pCode;
	}

	public void setpCode(String pCode) {
		this.pCode = pCode;
	}

	@Transient
	public String getsCode() {
		return sCode;
	}

	public void setsCode(String sCode) {
		this.sCode = sCode;
	}
}
