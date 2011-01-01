package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;

public interface PurchaseOrderManager extends
		GenericManager<PurchaseOrder, String> {

	PurchaseOrder get(String poNo, boolean includeDetail);
	
	void tryClosePurchaseOrder(String poNo, PurchaseOrderDetail exceptPurchaseOrderDetail);
	
	PurchaseOrder saveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
	
	void reloadFile(InboundLog inboundLog, String userCode, String archiveFolder);

}
