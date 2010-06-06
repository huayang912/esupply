package com.faurecia.model;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ScheduleBody {

	private String itemCode;
	private String supplierItemCode;
	private String itemDescription;
	private Date createDate;
	private Integer releaseNo;
	private List<BigDecimal> qtyList;
	private List<BigDecimal> deliverQtyList;
	public ScheduleBody() {
		this.qtyList = new ArrayList<BigDecimal>();
	}
	public String getItemCode() {
		return itemCode;
	}
	public void setItemCode(String itemCode) {
		this.itemCode = itemCode;
	}
	public String getSupplierItemCode() {
		return supplierItemCode;
	}
	public void setSupplierItemCode(String supplierItemCode) {
		this.supplierItemCode = supplierItemCode;
	}
	public String getItemDescription() {
		return itemDescription;
	}
	public void setItemDescription(String itemDescription) {
		this.itemDescription = itemDescription;
	}
	public Date getCreateDate() {
		return createDate;
	}
	public void setCreateDate(Date createDate) {
		this.createDate = createDate;
	}
	public Integer getReleaseNo() {
		return releaseNo;
	}
	public void setReleaseNo(Integer releaseNo) {
		this.releaseNo = releaseNo;
	}
	public List<BigDecimal> getQtyList() {
		return qtyList;
	}
	public void setQtyList(List<BigDecimal> qtyList) {
		this.qtyList = qtyList;
	}
	public void addQty(BigDecimal qty) {
		if (qtyList == null) {
			qtyList = new ArrayList<BigDecimal>();
		}
		qtyList.add(qty);
	}
	public List<BigDecimal> getDeliverQtyList() {
		return deliverQtyList;
	}
	public void setDeliverQtyList(List<BigDecimal> deliverQtyList) {
		this.deliverQtyList = deliverQtyList;
	}
	public void addDeliverQty(BigDecimal deliverQty) {
		if (deliverQtyList == null) {
			deliverQtyList = new ArrayList<BigDecimal>();
		}
		deliverQtyList.add(deliverQty);
	}
}
