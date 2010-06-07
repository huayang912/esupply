package com.faurecia.webapp.action;

import java.util.ArrayList;
import java.util.List;

import com.faurecia.model.LabelValue;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.User;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;

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
	public List<LabelValue> getAvailableSuppliers() {
		return availableSuppliers;
	}
	public void setAvailableSuppliers(List<LabelValue> availableSuppliers) {
		this.availableSuppliers = availableSuppliers;
	}
	public String list() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);		
		plantScheduleGroups = this.plantScheduleGroupManager.getPlantScheduleGroupByPlantCode(user.getUserPlant().getCode());
		return SUCCESS;
	}
	
	public String delete() {
		this.plantScheduleGroupManager.remove(plantScheduleGroup.getId());
		saveMessage(getText("plantScheduleGroup.deleted"));

		return SUCCESS;
	}

	public String edit() throws Exception {
		
		if (this.id != 0) {
			plantScheduleGroup = this.plantScheduleGroupManager.get(id);
		}  else {
			plantScheduleGroup = new PlantScheduleGroup();
			
			String userCode = this.getRequest().getRemoteUser();
			User user = this.userManager.getUserByUsername(userCode);	
			plantScheduleGroup.setPlant(user.getUserPlant());
		}
		
		prepare();
		
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
			String userCode = this.getRequest().getRemoteUser();
			User user = this.userManager.getUserByUsername(userCode);		
			plantScheduleGroups = this.plantScheduleGroupManager.getPlantScheduleGroupByPlantCode(user.getUserPlant().getCode());
			
			for (int i = 0; i < plantScheduleGroups.size(); i++) {
				if (!plantScheduleGroups.get(i).getId().equals(plantScheduleGroup.getId())
						&& plantScheduleGroups.get(i).getIsDefault()) {
					plantScheduleGroups.get(i).setIsDefault(false);
					this.plantScheduleGroupManager.save(plantScheduleGroups.get(i));
				}
			}
		}

		String key = (isNew) ? "plantScheduleGroup.added" : "plantScheduleGroup.updated";
		saveMessage(getText(key));

		if (!isNew) {
			prepare();
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
	
	private void prepare() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		
		List<PlantSupplier> plantSupplierList = new ArrayList<PlantSupplier>();
		if (plantScheduleGroup != null 
				&& plantScheduleGroup.getId() != null && plantScheduleGroup.getId() > 0) {
			plantSupplierList = this.plantSupplierManager.getPlantSupplierByPlantScheduleGroupId(plantScheduleGroup.getId());
			if (plantSupplierList != null && plantSupplierList.size() > 0) {
				plantScheduleGroup.setSupplierList(new ArrayList<LabelValue>());
				for (int i = 0; i < plantSupplierList.size(); i++) {
					plantScheduleGroup.getSupplierList().add(new LabelValue(plantSupplierList.get(i).getSupplierName(), plantSupplierList.get(i).getId().toString()));
				}
			}
		}
		
		List<PlantSupplier> allPlantSupplierList = this.plantSupplierManager.getPlantSupplierByPlantCode(user.getUserPlant().getCode());
		if (allPlantSupplierList != null && allPlantSupplierList.size() > 0) {
			this.availableSuppliers = new ArrayList<LabelValue>();
			for (int i = 0; i < allPlantSupplierList.size(); i++) {
				if (!plantSupplierList.contains(allPlantSupplierList.get(i))) {
					this.availableSuppliers.add(new LabelValue(allPlantSupplierList.get(i).getSupplierName(), allPlantSupplierList.get(i).getId().toString()));
				}
			}
		}
	}
}

