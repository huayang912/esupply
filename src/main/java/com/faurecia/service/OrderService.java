package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.order.ORDERS02;

public interface OrderService {
	
	ORDERS02 unmarshalOrder(InputStream stream);
	
}
