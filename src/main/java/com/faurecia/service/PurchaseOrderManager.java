package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.PurchaseOrder;

public interface PurchaseOrderManager extends
		GenericManager<PurchaseOrder, String> {

	PurchaseOrder get(String poNo, boolean includeDetail);
	
	PurchaseOrder SaveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
	
	void ReloadFile(InboundLog inboundLog, String userCode);

}
