package com.faurecia.model;

import java.util.ArrayList;
import java.util.List;
import java.util.Map;

public class ScheduleHead {
	private List<Map<String, Object>> headList;
	public ScheduleHead() {
		this.headList = new ArrayList<Map<String, Object>>();
	}
	public List<Map<String, Object>> getHeadList() {
		return headList;
	}
	public void setHeadList(List<Map<String, Object>> headList) {
		this.headList = headList;
	}
	public void addHead(Map<String, Object> head) {
		if (headList == null) {
			headList = new ArrayList<Map<String, Object>>();
		}
		headList.add(head);
	}
}
