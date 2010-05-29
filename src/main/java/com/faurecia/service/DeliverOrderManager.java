package com.faurecia.service;

import java.io.File;
import java.io.IOException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.model.DeliverOrder;
import com.faurecia.model.Plant;

public interface DeliverOrderManager extends GenericManager<DeliverOrder, String> {

	List<DeliverOrder> getUnexportDeliverOrderByPlant(Plant plant);
	
	File exportDeliverOrder(DeliverOrder deliverOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException; 
}
