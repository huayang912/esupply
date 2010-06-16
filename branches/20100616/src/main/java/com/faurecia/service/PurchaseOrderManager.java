package com.faurecia.service;

import com.faurecia.model.PurchaseOrder;

public interface PurchaseOrderManager extends
		GenericManager<PurchaseOrder, String> {

	PurchaseOrder get(String poNo, boolean includeDetail);
	
	void tryClosePurchaseOrder(String poNo);

}
