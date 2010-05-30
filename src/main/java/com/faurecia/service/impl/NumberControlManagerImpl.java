package com.faurecia.service.impl;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.NumberControl;
import com.faurecia.service.NumberControlManager;

import freemarker.template.utility.StringUtil;

public class NumberControlManagerImpl extends GenericManagerImpl<NumberControl, String> implements NumberControlManager {

	public NumberControlManagerImpl(GenericDao<NumberControl, String> genericDao) {
		super(genericDao);
	}

	public String generateNumber(String codePrefix, int length) {
		NumberControl numberControl = null;
		if (!this.genericDao.exists(codePrefix)) {
			numberControl = new NumberControl();
			numberControl.setCode(codePrefix);
			numberControl.setNextValue(1);
		}
		else {
			numberControl = this.genericDao.get(codePrefix);
		}
		
		int value = numberControl.getNextValue();
		int profixLength = length - codePrefix.length();
		
		numberControl.setNextValue(numberControl.getNextValue() + 1);
		this.genericDao.save(numberControl);
		
		return codePrefix + StringUtil.leftPad(String.valueOf(value), profixLength, '0');
	}
	
	public int getNextNumber(String codePrefix) {
		NumberControl numberControl = null;
		if (!this.genericDao.exists(codePrefix)) {
			numberControl = new NumberControl();
			numberControl.setCode(codePrefix);
			numberControl.setNextValue(1);
		}
		else {
			numberControl = this.genericDao.get(codePrefix);
		}
		
		int value = numberControl.getNextValue();
		
		numberControl.setNextValue(numberControl.getNextValue() + 1);
		this.genericDao.save(numberControl);
		
		return value;
	}
}
