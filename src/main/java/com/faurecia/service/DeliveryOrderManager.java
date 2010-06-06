package com.faurecia.service;

import java.io.File;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.Plant;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;

public interface DeliveryOrderManager extends GenericManager<DeliveryOrder, String> {

	DeliveryOrder createDeliveryOrder(List<PurchaseOrderDetail> purchaseOrderDetailList) throws IllegalAccessException, InvocationTargetException;
	
	DeliveryOrder createScheduleDeliveryOrder(DeliveryOrder deliveryOrder);	
	
	List<DeliveryOrder> getUnexportDeliveryOrderByPlant(Plant plant);

	DeliveryOrder get(String doNo, boolean includeDetail);
	
	File exportDeliveryOrder(DeliveryOrder deliveryOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException; 
}
