package com.faurecia.service;

import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.lang.reflect.InvocationTargetException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Plant;
import com.faurecia.model.PurchaseOrderDetail;

public interface DeliveryOrderManager extends GenericManager<DeliveryOrder, String> {

	DeliveryOrder createDeliveryOrder(List<PurchaseOrderDetail> purchaseOrderDetailList) throws IllegalAccessException, InvocationTargetException;
	
	DeliveryOrder createScheduleDeliveryOrder(DeliveryOrder deliveryOrder);	
	
	List<DeliveryOrder> getUnexportDeliveryOrderByPlant(Plant plant);

	DeliveryOrder get(String doNo, boolean includeDetail);
	
	DeliveryOrder confirm(DeliveryOrder deliveryOrder);
	
	File exportDeliveryOrder(DeliveryOrder deliveryOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException;
	
	List<DeliveryOrder> saveMultiFile(InputStream inputStream,
			InboundLog inboundLog);
	
	void reloadFile(InboundLog inboundLog, String userCode, String archiveFolder);
	
}
