package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.PurchaseOrderApproval;

public interface PurchaseOrderApprovalManager extends GenericManager<PurchaseOrderApproval, String> {
	
	PurchaseOrderApproval get(String poNo, boolean includeDetail);

	PurchaseOrderApproval saveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
	
	void reloadFile(InboundLog inboundLog, String userCode, String archiveFolder);
}
