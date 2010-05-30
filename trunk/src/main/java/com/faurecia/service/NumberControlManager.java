package com.faurecia.service;

import com.faurecia.model.NumberControl;

public interface NumberControlManager extends GenericManager<NumberControl, String> {
	String generateNumber(String codePrefix, int length);
	
	int getNextNumber(String codePrefix);
}
