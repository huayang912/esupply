package com.faurecia.webapp.action;

import java.net.MalformedURLException;
import java.util.Calendar;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;
import javax.xml.bind.JAXBException;

import com.faurecia.model.Plant;
import com.faurecia.model.User;
import com.faurecia.service.GenericManager;

public class PlantAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -33601379593125595L;
	private GenericManager<Plant, String> plantManager;
	private List<Plant> plants;
	private Plant plant;
	private String code;
	private boolean editProfile;

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public List<Plant> getPlants() {
		return plants;
	}
	
	public Map<String, String> getMFTemplate() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("Do.png", "ELink Standard");
		status.put("FWAS.png", "FWAS");
		status.put("GSK.png", "GSK");
		return status;
	}
	
	public Map<String, String> getBoxTemplate() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("Box.png", "ELink Standard");
		status.put("Box_CN.png", "China(Support Chinese Character)");
		status.put("Box_WuXi.png", "WuXi");
		return status;
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
		HttpServletRequest request = getRequest();
		editProfile = (request.getRequestURI().indexOf("editPlantProfile") > -1);
		
		if (this.code != null) {
			plant = this.plantManager.get(code);
			plant.setConfirmFtpPassword(plant.getFtpPassword());
		} else if (editProfile) {
			User user = userManager.getUserByUsername(request.getRemoteUser());
			plant = user.getUserPlant();
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
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
}
