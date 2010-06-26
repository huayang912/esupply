package com.faurecia.service;

import com.faurecia.model.Item;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.ScheduleControl;

public interface ScheduleControlManager extends GenericManager<ScheduleControl, Integer> {
	
	ScheduleControl get(String scheduleNo, PlantSupplier plantSupplier, Item item);

}
