package com.faurecia.webapp.action;

import java.net.MalformedURLException;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.xml.bind.JAXBException;

import com.faurecia.model.Plant;
import com.faurecia.model.Resource;
import com.faurecia.service.GenericManager;
import com.faurecia.service.PlantSupplierManager;

public class PlantAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -33601379593125595L;
	private GenericManager<Plant, String> plantManager;
	private GenericManager<Resource, Long> resourceManager;
	private PlantSupplierManager plantSupplierManager;
	private List<Plant> plants;
	private Plant plant;
	private String code;
	private boolean editProfile;

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setResourceManager(GenericManager<Resource, Long> resourceManager) {
		this.resourceManager = resourceManager;
	}

	public List<Plant> getPlants() {
		return plants;
	}

	public Map<Integer, String> getDateType() {
		Map<Integer, String> dateType = new HashMap<Integer, String>();
		dateType.put(Calendar.YEAR, "Year");
		dateType.put(Calendar.MONTH, "Month");
		dateType.put(Calendar.DATE, "Day");
		dateType.put(Calendar.HOUR, "Hour");
		dateType.put(Calendar.MINUTE, "Minute");
		
		return dateType;
	}

	public String list() {		
		plants = plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
		return SUCCESS;
	}

	public void setCode(String code) {
		this.code = code;
	}

	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}
	
	public boolean isEditProfile() {
		return editProfile;
	}

	public void setEditProfile(boolean editProfile) {
		this.editProfile = editProfile;
	}

	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}
		
		return CANCEL;
	}

	public String delete() {
		this.plantManager.remove(plant.getCode());
		saveMessage(getText("plant.deleted"));
		this.plantManager.flushSession();
		return SUCCESS;
	}

	public String edit() throws JAXBException, MalformedURLException {
		if (this.code != null) {
			plant = this.plantManager.get(code);
			plant.setConfirmFtpPassword(plant.getFtpPassword());
		} else {
			plant = new Plant();
		}

		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}

		boolean isNew = (plant.getCode() == null);

		this.plantManager.save(plant);
		plant.setConfirmFtpPassword(plant.getFtpPassword());
		
		String key = (isNew) ? "plant.added" : "plant.updated";
		saveMessage(getText(key));
		this.plantManager.flushSession();
		if (!isNew) {
			Resource resource = new Resource();
			resource.setCode(plant.getCode());
			resource.setDescription(plant.getName());
			resource.setType("plant");
			this.resourceManager.save(resource);
			
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
}
