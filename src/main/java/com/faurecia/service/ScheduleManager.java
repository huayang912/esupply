package com.faurecia.service;

import java.io.InputStream;

import com.faurecia.model.InboundLog;
import com.faurecia.model.Plant;
import com.faurecia.model.Schedule;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;

public interface ScheduleManager extends GenericManager<Schedule, String> {
	Schedule SaveSingleFile(InputStream inputStream,
			InboundLog inboundLog);
	
	Schedule GetLastestScheduleByUser(User user);
	
	int GetLastestScheduleVersion(Plant plant, Supplier supplier);
	
	Schedule get(String scheduleNo, boolean includeDetail);
}
