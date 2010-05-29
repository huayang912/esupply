package com.faurecia.service;

import java.io.File;
import java.io.IOException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.Plant;

public interface DeliveryOrderManager extends GenericManager<DeliveryOrder, String> {

	List<DeliveryOrder> getUnexportDeliveryOrderByPlant(Plant plant);
	
	File exportDeliveryOrder(DeliveryOrder deliveryOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException; 
}
