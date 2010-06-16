package com.faurecia.service;

import java.util.List;

import com.faurecia.model.Notice;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;

public interface NoticeManager extends GenericManager<Notice, Integer> {
	List<Notice> getNoticeByPlant(Plant plant);
	
	List<Notice> getNoticeByPlantSupplier(PlantSupplier plantSupplier);
}
