package com.faurecia.webapp.action;

import java.util.Date;
import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.ScheduleControl;
import com.faurecia.model.Supplier;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ScheduleControlManager;
import com.faurecia.service.SupplierManager;

public class ScheduleControlAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -1420877857501600197L;

	private PlantSupplierManager plantSupplierManager;
	private ScheduleControlManager scheduleControlManager;
	private SupplierManager supplierManager;
	private PlantSupplier schedulePlantSupplier;
	private List<ScheduleControl> scheduleControlList;

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setScheduleControlManager(ScheduleControlManager scheduleControlManager) {
		this.scheduleControlManager = scheduleControlManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
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

	public List<Supplier> getSuppliers() {
		return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public String enter() {
		return SUCCESS;
	}

	public String cancel() {
		return "mainMenu";
	}

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
		this.scheduleControlManager.flushSession();
		prepare();
		saveMessage(getText("scheduleControl.updated"));
		return SUCCESS;
	}

	private void prepare() {
		if (schedulePlantSupplier != null) {
			DetachedCriteria criteria = DetachedCriteria.forClass(ScheduleControl.class);

			criteria.createAlias("plantSupplier", "ps");
			criteria.createAlias("ps.plant", "p");
			criteria.createAlias("ps.supplier", "s");

			if (schedulePlantSupplier.getPlant() != null && schedulePlantSupplier.getPlant().getCode() != null
					&& schedulePlantSupplier.getPlant().getCode().trim().length() != 0) {
				criteria.add(Restrictions.eq("p.code", schedulePlantSupplier.getPlant().getCode().trim()));
			} else {
				criteria.add(Restrictions.eq("p.code", "-1"));

			}

			if (schedulePlantSupplier.getSupplier() != null && schedulePlantSupplier.getSupplier().getCode() != null
					&& schedulePlantSupplier.getSupplier().getCode().trim().length() != 0) {
				criteria.add(Restrictions.eq("s.code", schedulePlantSupplier.getSupplier().getCode().trim()));
			} else {
				criteria.add(Restrictions.eq("s.code", "-1"));
			}

			scheduleControlList = this.scheduleControlManager.findByCriteria(criteria);
		}
	}
}
