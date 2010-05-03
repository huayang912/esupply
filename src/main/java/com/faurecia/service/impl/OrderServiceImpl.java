package com.faurecia.service.impl;

import java.io.InputStream;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

import com.faurecia.model.order.ORDERS02;
import com.faurecia.service.OrderService;

public class OrderServiceImpl extends UniversalManagerImpl implements
		OrderService {

	private Unmarshaller unmarshaller;

	public OrderServiceImpl() throws JAXBException {
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.order");
		unmarshaller = jc.createUnmarshaller();
	}

	public ORDERS02 unmarshalOrder(InputStream stream) {
		try {
			ORDERS02 o = (ORDERS02)unmarshaller.unmarshal(stream);
			return o;
		} catch (JAXBException e) {
			// TODO Auto-generated catch block
			e.printStackTrace();
			
			return null;
		}
	}

}
