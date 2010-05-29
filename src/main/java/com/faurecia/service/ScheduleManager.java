package com.faurecia.service;

import java.io.InputStream;
import java.util.Date;

import com.faurecia.model.InboundLog;
import com.faurecia.model.Schedule;

public interface ScheduleManager extends GenericManager<Schedule, String> {
	Schedule SaveSingleFile(InputStream inputStream,
			InboundLog inboundLog);	
	
	Schedule getLastestScheduleItem(String plantCode, String supplierCode);
	
	Schedule getLastestScheduleItem(String plantCode, String supplierCode, Date tillDate);
}
