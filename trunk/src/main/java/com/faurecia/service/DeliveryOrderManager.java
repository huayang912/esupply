package com.faurecia.service;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.Plant;
import com.faurecia.model.PurchaseOrderDetail;

public interface DeliveryOrderManager extends GenericManager<DeliveryOrder, String> {

	DeliveryOrder createDeliverOrder(List<PurchaseOrderDetail> purchaseOrderDetailList) throws IllegalAccessException, InvocationTargetException;
	
	List<DeliveryOrder> getUnexportDeliveryOrderByPlant(Plant plant);
	
	File exportDeliveryOrder(DeliveryOrder deliveryOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException; 
}
