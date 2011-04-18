package com.faurecia.webapp.action;

import java.util.ArrayList;
import java.util.List;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.Item;
import com.faurecia.model.LabelValue;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;
import com.faurecia.model.User;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.SupplierManager;

public class PlantScheduleGroupAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 7956682949766493549L;
	private PlantScheduleGroupManager plantScheduleGroupManager;
	private PlantSupplierManager plantSupplierManager;
	private List<PlantScheduleGroup> plantScheduleGroups;
	private PlantScheduleGroup plantScheduleGroup;
	private int id;
	private List<LabelValue> availableSuppliers;
	private SupplierManager supplierManager;
	
	public List<PlantScheduleGroup> getPlantScheduleGroups() {
		return plantScheduleGroups;
	}

	public void setPlantScheduleGroups(List<PlantScheduleGroup> plantScheduleGroups) {
		this.plantScheduleGroups = plantScheduleGroups;
	}

	public PlantScheduleGroup getPlantScheduleGroup() {
		return plantScheduleGroup;
	}

	public void setPlantScheduleGroup(PlantScheduleGroup plantScheduleGroup) {
		this.plantScheduleGroup = plantScheduleGroup;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public void setPlantScheduleGroupManager(PlantScheduleGroupManager plantScheduleGroupManager) {
		this.plantScheduleGroupManager = plantScheduleGroupManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}
	
	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public List<LabelValue> getAvailableSuppliers() {
		return availableSuppliers;
	}

	public void setAvailableSuppliers(List<LabelValue> availableSuppliers) {
		this.availableSuppliers = availableSuppliers;
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}
	
	public List<Supplier> getSuppliers() {
		if (plantScheduleGroup != null && plantScheduleGroup.getPlant() != null
				&& plantScheduleGroup.getPlant().getCode() != null && !plantScheduleGroup.getPlant().getCode().equals("-1")) {
			return this.supplierManager.getSuppliersByPlantAndUser(plantScheduleGroup.getPlant().getCode().trim() + "|" + this.getRequest().getRemoteUser());
		} else {
			return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
		}
	}

	public String list() {
		if (plantScheduleGroup != null) {
			
			DetachedCriteria criteria = DetachedCriteria.forClass(PlantScheduleGroup.class);
			criteria.createAlias("plant", "p");
			
			if (plantScheduleGroup.getPlant() != null && plantScheduleGroup.getPlant().getCode() != null
					&& plantScheduleGroup.getPlant().getCode().trim().length() != 0) {
				if (plantScheduleGroup.getPlant().getCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						criteria.add(Restrictions.in("plant", plants));
					} else {
						criteria.add(Restrictions.eq("p.code", "-1"));
					}

				} else {
					criteria.add(Restrictions.eq("p.code", plantScheduleGroup.getPlant().getCode().trim()));
				}
			}
			
			plantScheduleGroups = this.plantScheduleGroupManager.findByCriteria(criteria);
		}
		
		
		return SUCCESS;
	}

	public String delete() {
		try {
			this.plantScheduleGroupManager.remove(plantScheduleGroup.getId());
			saveMessage(getText("plantScheduleGroup.deleted"));
		} catch (Exception ex) {
			saveMessage(getText("plantScheduleGroup.deletefail"));
		}
		this.plantScheduleGroupManager.flushSession();
		return SUCCESS;
	}

	public String edit() throws Exception {

		if (this.id != 0) {
			plantScheduleGroup = this.plantScheduleGroupManager.get(id);
			
			prepare();
		} else {
			plantScheduleGroup = new PlantScheduleGroup();
		}

		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}

		boolean isNew = (plantScheduleGroup.getId() == null);

		plantScheduleGroup = this.plantScheduleGroupManager.save(plantScheduleGroup);

		if (!isNew) {
			this.plantScheduleGroupManager.cleanPlantScheduleGroupOfRelatedPlantSupplier(plantScheduleGroup);
		}

		String[] suppliers = getRequest().getParameterValues("suppliers");

		for (int i = 0; suppliers != null && i < suppliers.length; i++) {
			Integer plantSupplierId = Integer.parseInt(suppliers[i]);
			PlantSupplier plantSupplier = this.plantSupplierManager.get(plantSupplierId);
			plantSupplier.setPlantScheduleGroup(plantScheduleGroup);

			this.plantSupplierManager.save(plantSupplier);
		}

		if (plantScheduleGroup.getIsDefault()) {
			plantScheduleGroups = this.plantScheduleGroupManager.getPlantScheduleGroupByPlantCode(plantScheduleGroup.getPlant().getCode());

			for (int i = 0; i < plantScheduleGroups.size(); i++) {
				if (!plantScheduleGroups.get(i).getId().equals(plantScheduleGroup.getId()) && plantScheduleGroups.get(i).getIsDefault()) {
					plantScheduleGroups.get(i).setIsDefault(false);
					this.plantScheduleGroupManager.save(plantScheduleGroups.get(i));
				}
			}
		}

		String key = (isNew) ? "plantScheduleGroup.added" : "plantScheduleGroup.updated";
		saveMessage(getText(key));
		this.plantScheduleGroupManager.flushSession();
		if (!isNew) {
			prepare();
			return INPUT;
		} else {
			return SUCCESS;
		}
	}

	private void prepare() {

		List<PlantSupplier> plantSupplierList = new ArrayList<PlantSupplier>();
		if (plantScheduleGroup != null && plantScheduleGroup.getId() != null && plantScheduleGroup.getId() > 0) {
			plantSupplierList = this.plantSupplierManager.getPlantSupplierByPlantScheduleGroupId(plantScheduleGroup.getId());
			if (plantSupplierList != null && plantSupplierList.size() > 0) {
				plantScheduleGroup.setSupplierList(new ArrayList<LabelValue>());
				for (int i = 0; i < plantSupplierList.size(); i++) {
					plantScheduleGroup.getSupplierList().add(
							new LabelValue(plantSupplierList.get(i).getSupplierName(), plantSupplierList.get(i).getId().toString()));
				}
			}
		}

		
		DetachedCriteria criteria = DetachedCriteria.forClass(PlantSupplier.class);
		criteria.add(Restrictions.eq("plant", plantScheduleGroup.getPlant()));
		
		List<Supplier> suppliers = getSuppliers();
		if (suppliers != null && suppliers.size() > 0) {
			criteria.add(Restrictions.in("supplier", suppliers));
		}
		
		List<PlantSupplier> allPlantSupplierList = this.plantSupplierManager.findByCriteria(criteria);
		if (allPlantSupplierList != null && allPlantSupplierList.size() > 0) {
			this.availableSuppliers = new ArrayList<LabelValue>();
			for (int i = 0; i < allPlantSupplierList.size(); i++) {
				if (!plantSupplierList.contains(allPlantSupplierList.get(i))) {
					this.availableSuppliers.add(new LabelValue(allPlantSupplierList.get(i).getSupplierName(), allPlantSupplierList.get(i).getId()
							.toString()));
				}
			}
		}
	}
}
