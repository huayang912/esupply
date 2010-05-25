package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Plant;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.Supplier;

public interface ScheduleItemManager extends GenericManager<ScheduleItem, Integer> {
	List<ScheduleItem> getLastestScheduleItemByPlantAndSupplier(Plant plant, Supplier supplier);
}
