package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.PurchaseOrder;

public interface PurchaseOrderManager extends
		GenericManager<PurchaseOrder, String> {

	public PurchaseOrder SaveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
	
	public void ReloadFile(InboundLog inboundLog, String userCode);

}
