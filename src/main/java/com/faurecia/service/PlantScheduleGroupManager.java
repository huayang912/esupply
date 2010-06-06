package com.faurecia.service;

import java.util.List;

import com.faurecia.model.PlantScheduleGroup;

public interface PlantScheduleGroupManager extends GenericManager<PlantScheduleGroup, Integer> {
	
	List<PlantScheduleGroup> getPlantScheduleGroupByPlantCode(String plantCode);
	
	PlantScheduleGroup getDefaultPlantScheduleGroupByPlantCode(String plantCode);
}
