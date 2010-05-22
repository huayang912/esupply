package com.faurecia.model;

import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.List;

public class ScheduleBody {

	private String itemCode;
	private String supplierItemCode;
	private String itemDescription;
	private List<BigDecimal> qtyList;
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
}
