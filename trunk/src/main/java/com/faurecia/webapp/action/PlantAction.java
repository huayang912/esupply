package com.faurecia.webapp.action;

import java.util.List;

import com.faurecia.model.Plant;
import com.faurecia.service.GenericManager;

public class PlantAction extends BaseAction {
	private GenericManager<Plant, String> plantManager;
    private List plants;

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
}
