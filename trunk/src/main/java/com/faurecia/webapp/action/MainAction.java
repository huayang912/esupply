package com.faurecia.webapp.action;

import java.util.List;

import com.faurecia.Constants;
import com.faurecia.model.PlantSupplier;
import com.faurecia.service.PlantSupplierManager;

public class MainAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = 1190064375724428165L;
	private PlantSupplierManager plantSupplierManager;
	private boolean selectPlant;
	private List<PlantSupplier> plantSupplierList;
	private String supplierPlant;
	
	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public boolean isSelectPlant() {
		return selectPlant;
	}

	public void setSelectPlant(boolean selectPlant) {
		this.selectPlant = selectPlant;
	}

	public List<PlantSupplier> getPlantSupplierList() {
		return plantSupplierList;
	}

	public void setPlantSupplierList(List<PlantSupplier> plantSupplierList) {
		this.plantSupplierList = plantSupplierList;
	}

	public String getSupplierPlant() {
		return supplierPlant;
	}

	public void setSupplierPlant(String supplierPlant) {
		this.supplierPlant = supplierPlant;
	}

	public String main() {
		
		if (this.getRequest().isUserInRole(Constants.VENDOR_ROLE)
				&& this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
			plantSupplierList = this.plantSupplierManager.getPlantSupplierBySupplierCode(this.getRequest().getRemoteUser());
			selectPlant = true;
		}
		
		return SUCCESS;
	}
	
	public String setSupplierPlant() {
		this.getSession().setAttribute(Constants.SUPPLIER_PLANT_CODE, supplierPlant);
		return SUCCESS;
	}
}
