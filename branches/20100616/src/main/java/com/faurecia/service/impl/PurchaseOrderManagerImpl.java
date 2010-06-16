package com.faurecia.service.impl;

import java.math.BigDecimal;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.service.PurchaseOrderManager;

public class PurchaseOrderManagerImpl extends GenericManagerImpl<PurchaseOrder, String> implements PurchaseOrderManager {

	public PurchaseOrderManagerImpl(GenericDao<PurchaseOrder, String> genericDao) throws JAXBException {
		super(genericDao);
	}	
	
	public PurchaseOrder get(String poNo, boolean includeDetail) {
		PurchaseOrder purchaseOrder = this.genericDao.get(poNo);

		if (includeDetail && purchaseOrder.getPurchaseOrderDetailList() != null && purchaseOrder.getPurchaseOrderDetailList().size() > 0) {
			List<PurchaseOrderDetail> purchaseOrderDetailList = purchaseOrder.getPurchaseOrderDetailList();
			purchaseOrder.setPurchaseOrderDetailList(null);

			// 过滤掉需求数量是0的明细
			for (int i = 0; i < purchaseOrderDetailList.size(); i++) {
				if (purchaseOrderDetailList.get(i).getQty().compareTo(new BigDecimal(0)) != 0) {
					purchaseOrder.addPurchaseOrderDetail(purchaseOrderDetailList.get(i));
				}
			}

		}

		return purchaseOrder;
	}

	public void tryClosePurchaseOrder(String poNo) {
		PurchaseOrder purchaseOrder = this.get(poNo, true);

		if (purchaseOrder.getPurchaseOrderDetailList() != null && purchaseOrder.getPurchaseOrderDetailList().size() > 0) {
			boolean allClose = true;
			for (int i = 0; i < purchaseOrder.getPurchaseOrderDetailList().size(); i++) {
				PurchaseOrderDetail purchaseOrderDetail = purchaseOrder.getPurchaseOrderDetailList().get(i);
				if (purchaseOrderDetail.getShipQty() == null || purchaseOrderDetail.getQty().compareTo(purchaseOrderDetail.getShipQty()) > 0) {
					allClose = false;
					break;
				}
			}

			if (allClose) {
				purchaseOrder.setStatus("Close");
				this.genericDao.save(purchaseOrder);
			}
		}
	}	
}
