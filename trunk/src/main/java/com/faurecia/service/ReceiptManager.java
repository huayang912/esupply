package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.Receipt;

public interface ReceiptManager extends GenericManager<Receipt, String> {
	
	Receipt get(String poNo, boolean includeDetail);
	
	Receipt saveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
}
