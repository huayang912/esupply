package com.faurecia.webapp.action;

import java.util.Date;
import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.PlantSupplier;
import com.faurecia.model.ScheduleControl;
import com.faurecia.model.User;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ScheduleControlManager;

public class ScheduleControlAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -1420877857501600197L;
	
	private PlantSupplierManager plantSupplierManager;
	private ScheduleControlManager scheduleControlManager;
	private PlantSupplier schedulePlantSupplier; 
	private List<ScheduleControl> scheduleControlList;

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}
	
	public void setScheduleControlManager(ScheduleControlManager scheduleControlManager) {
		this.scheduleControlManager = scheduleControlManager;
	}

	public List<ScheduleControl> getScheduleControlList() {
		return scheduleControlList;
	}

	public void setScheduleControlList(List<ScheduleControl> scheduleControlList) {
		this.scheduleControlList = scheduleControlList;
	}

	public PlantSupplier getSchedulePlantSupplier() {
		return schedulePlantSupplier;
	}

	public void setSchedulePlantSupplier(PlantSupplier schedulePlantSupplier) {
		this.schedulePlantSupplier = schedulePlantSupplier;
	}

	public List<PlantSupplier> getSuppliers() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		
		return this.plantSupplierManager.getPlantSupplierByUserId(user.getId());
	}
	
	public String enter() {
		return SUCCESS;
	}
	
	public String cancel() {
		return "mainMenu";
	}
	
	@SuppressWarnings("unchecked")
	public String list() {
		
		prepare();
		return SUCCESS;
	}
	
	public String save() {
		if (scheduleControlList != null) {
			for (int i = 1; i < scheduleControlList.size(); i++) {
				ScheduleControl scheduleControl = scheduleControlList.get(i);
				Date expireDate = scheduleControl.getExpireDate();
				scheduleControl = this.scheduleControlManager.get(scheduleControl.getId());
				scheduleControl.setExpireDate(expireDate);
				this.scheduleControlManager.save(scheduleControl);
			}
		}

		prepare();
		saveMessage(getText("scheduleControl.updated"));
		return SUCCESS;
	}
	
	private void prepare() {
		DetachedCriteria selectCriteria = DetachedCriteria.forClass(ScheduleControl.class);
		
		selectCriteria.createAlias("plantSupplier", "ps");
		
		selectCriteria.add(Restrictions.eq("plantSupplier", schedulePlantSupplier));
		
		scheduleControlList = this.scheduleControlManager.findByCriteria(selectCriteria);
	}
}
