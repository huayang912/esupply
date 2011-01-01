package com.faurecia.service;

import com.faurecia.model.InboundLog;

public interface InboundLogManager extends GenericManager<InboundLog, Integer> {
	public InboundLog getInboundLogByDataTypeAndFileName(String dataType, String fileName);
}
