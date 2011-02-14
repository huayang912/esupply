package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Plant;
import com.faurecia.model.Supplier;

public interface SupplierManager extends GenericManager<Supplier, String> {
	List<Supplier> getSuppliersByPlant(Plant plant);
	List<Supplier> getAuthorizedSupplier(String userCode);
}
