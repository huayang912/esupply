package com.faurecia.resolver;

import java.util.List;

import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.order.ORDERS02;
import com.faurecia.model.order.ORDERS02E1EDKA1;

public class PurchaseOrderReslover {

	public static PurchaseOrder ORDERS02ToPO(ORDERS02 order) {
		PurchaseOrder po = new PurchaseOrder();
		po.setPoNo(order.getIDOC().getE1EDK01().getBELNR());
		
		List<ORDERS02E1EDKA1> ORDERS02E1EDKA1List = order.getIDOC().getE1EDKA1();
		if (ORDERS02E1EDKA1List != null && ORDERS02E1EDKA1List.size() > 0) {
			for (int i = 0; i < ORDERS02E1EDKA1List.size(); i++) {
				ORDERS02E1EDKA1 E1EDKA1 = ORDERS02E1EDKA1List.get(i);
				if ("WE".equals(E1EDKA1.getPARVW())) {
					
				}
			}
		}
		
		return po;
	}
}
