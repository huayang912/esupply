package com.faurecia.model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

public class ScheduleHead {
	private List<String> scheduleTypeList;
	private List<Date> dateFromList;
	private List<Date> dateToList;
	public ScheduleHead() {
		this.scheduleTypeList = new ArrayList<String>();
		this.dateFromList = new ArrayList<Date>();
		this.dateToList = new ArrayList<Date>();
	}
	public List<String> getScheduleTypeList() {
		return scheduleTypeList;
	}
	public void setScheduleTypeList(List<String> scheduleTypeList) {
		this.scheduleTypeList = scheduleTypeList;
	}
	public void addScheduleType(String scheduleType) {
		if (scheduleTypeList == null) {
			scheduleTypeList = new ArrayList<String>();
		}
		scheduleTypeList.add(scheduleType);
	}
	public List<Date> getDateFromList() {
		return dateFromList;
	}
	public void setDateFromList(List<Date> dateFromList) {
		this.dateFromList = dateFromList;
	}
	public void addDateFrom(Date dateFrom) {
		if (dateFromList == null) {
			dateFromList = new ArrayList<Date>();
		}
		dateFromList.add(dateFrom);
	}
	public List<Date> getDateToList() {
		return dateToList;
	}
	public void setDateToList(List<Date> dateToList) {
		this.dateToList = dateToList;
	}
	public void addDateTo(Date dateTo) {
		if (dateToList == null) {
			dateToList = new ArrayList<Date>();
		}
		dateToList.add(dateTo);
	}
}
