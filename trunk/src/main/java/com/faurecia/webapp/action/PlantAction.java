package com.faurecia.webapp.action;

import java.net.MalformedURLException;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.xml.bind.JAXBException;

import com.faurecia.model.Plant;
import com.faurecia.model.User;
import com.faurecia.service.GenericManager;

public class PlantAction extends BaseAction {

	private static final long serialVersionUID = 1L;
	private GenericManager<Plant, String> plantManager;
	private List<Plant> plants;
	private Plant plant;
	private String code;

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public List<Plant> getPlants() {
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
	
	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}
		
		return CANCEL;
	}

	public String delete() {
		this.plantManager.remove(plant.getCode());
		saveMessage(getText("plant.deleted"));

		return SUCCESS;
	}

	public String edit() throws JAXBException, MalformedURLException {
		HttpServletRequest request = getRequest();
		boolean editProfile = (request.getRequestURI().indexOf("editPlantProfile") > -1);
		
		
		if (this.code != null) {
			plant = this.plantManager.get(code);
		} else if (editProfile) {
			User user = userManager.getUserByUsername(request.getRemoteUser());
			plant = user.getUserPlant();
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
