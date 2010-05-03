package com.faurecia.webapp.action;

import com.faurecia.service.OrderService;

public class OrderAction extends BaseAction {
	private OrderService orderManager;

	public void setOrderManager(OrderService orderManager) {
		this.orderManager = orderManager;
	}
}
