package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;

public interface SupplierManager extends UniversalManager {
	List<Supplier> getSuppliersByPlant(Plant plant);
}
