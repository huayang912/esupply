package com.faurecia.service;

import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;

public interface PlantSupplierManager extends GenericManager<PlantSupplier, Integer> {
	PlantSupplier getPlantSupplier(Plant plant, Supplier supplier);
}
