package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.PurchaseOrder;

public interface PurchaseOrderManager extends
		GenericManager<PurchaseOrder, String> {

	PurchaseOrder get(String poNo, boolean includeDetail);
	
	void tryClosePurchaseOrder(String poNo);
	
	PurchaseOrder saveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
	
	void reloadFile(InboundLog inboundLog, String userCode);

}
