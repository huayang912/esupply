package com.faurecia.webapp.action;

import java.net.MalformedURLException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.model.Plant;
import com.faurecia.service.GenericManager;

public class PlantAction extends BaseAction {
	private GenericManager<Plant, String> plantManager;
	private List plants;
	private Plant plant;
	private String code;

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public List getPlants() {
		return plants;
	}

	public String list() {
		plants = plantManager.getAll();
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

	public String delete() {
		this.plantManager.remove(plant.getCode());
		saveMessage(getText("plant.deleted"));

		return SUCCESS;
	}

	public String edit() throws JAXBException, MalformedURLException {

		if (this.code != null) {
			plant = this.plantManager.get(code);
		} else {
			plant = new Plant();
		}

		return SUCCESS;
	}

	public String save() throws Exception {
		if (cancel != null) {
			return CANCEL;
		}

		if (delete != null) {
			return delete();
		}

		boolean isNew = (plant.getCode() == null);

		plant = this.plantManager.save(plant);

		String key = (isNew) ? "plant.added" : "plant.updated";
		saveMessage(getText(key));

		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
}
