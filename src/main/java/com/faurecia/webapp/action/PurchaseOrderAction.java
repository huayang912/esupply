package com.faurecia.webapp.action;

import com.faurecia.service.PurchaseOrderManager;
import com.faurecia.service.impl.PurchaseOrderManagerImpl;

public class PurchaseOrderAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = 3847028744873885262L;
	private PurchaseOrderManager purchaseOrderManager;

	public void setPurchaseOrderManager(PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}
	
	public String test() {
//		this.purchaseOrderManager.DownloadFiles("10.20.96.130", 21, "commonuser",
//				"test123", "/storage/team/dingxin/ORDERS", "su");
		
		return SUCCESS;
	}
}
