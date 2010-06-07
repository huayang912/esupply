package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;

public interface PlantSupplierManager extends GenericManager<PlantSupplier, Integer> {
	PlantSupplier getPlantSupplier(Plant plant, Supplier supplier);
	
	List<PlantSupplier> getPlantSupplierBySupplierCode(String supplierCode);
	
	List<PlantSupplier> getPlantSupplierByPlantCode(String plantCode);
	
	List<PlantSupplier> getPlantSupplierByPlantScheduleGroupId(Integer plantScheduleGroupId);
}
