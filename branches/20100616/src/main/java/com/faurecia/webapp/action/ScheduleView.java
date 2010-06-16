package com.faurecia.webapp.action;

import java.util.ArrayList;
import java.util.List;

import com.faurecia.model.ScheduleBody;
import com.faurecia.model.ScheduleHead;

public class ScheduleView {
	private ScheduleHead scheduleHead;
	private List<ScheduleBody> scheduleBodyList;
	public ScheduleView() {
		this.scheduleHead = new ScheduleHead();
		this.scheduleBodyList = new ArrayList<ScheduleBody>();
	}
	public ScheduleHead getScheduleHead() {
		return scheduleHead;
	}
	public void setScheduleHead(ScheduleHead scheduleHead) {
		this.scheduleHead = scheduleHead;
	}
	public List<ScheduleBody> getScheduleBodyList() {
		return scheduleBodyList;
	}
	public void setScheduleBodyList(List<ScheduleBody> scheduleBodyList) {
		this.scheduleBodyList = scheduleBodyList;
	}
	public void addScheduleBody(ScheduleBody scheduleBody) {
		if (scheduleBodyList == null) {
			scheduleBodyList = new ArrayList<ScheduleBody>();
		}
		scheduleBodyList.add(scheduleBody);
	}
}
