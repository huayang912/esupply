package com.faurecia.service;

import com.faurecia.model.InboundLog;
import com.faurecia.model.PurchaseOrder;

public interface PurchaseOrderManager extends
		GenericManager<PurchaseOrder, String> {

	public void DownloadFiles(String ftpServer, int ftpPort, String ftpUser,
			String ftpPassword, String ftpPath, String user);

	public void ReloadFile(InboundLog inboundLog, String userCode);

}
