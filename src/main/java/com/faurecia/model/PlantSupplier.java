package com.faurecia.model;

import javax.persistence.Entity;
import javax.persistence.Table;

@Entity
@Table(name="plant_supplier")
public class PlantSupplier extends Supplier {

	/**
	 * 
	 */
	private static final long serialVersionUID = -6616983854718294391L;

	private String plantDescription;
}
