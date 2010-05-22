package com.faurecia.webapp.action;

import java.util.List;

import javax.servlet.http.HttpServletRequest;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.PlantSupplier;
import com.faurecia.model.User;
import com.faurecia.service.PlantSupplierManager;

public class SupplierAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = -6950278025858688589L;
	/**
	 * 
	 */
	private PlantSupplierManager plantSupplierManager;
	private List<PlantSupplier> plantSuppliers;
	private PlantSupplier plantSupplier;
	private int id;
	private boolean editProfile;

	public List<PlantSupplier> getPlantSuppliers() {
		return plantSuppliers;
	}

	public void setPlantSuppliers(List<PlantSupplier> plantSuppliers) {
		this.plantSuppliers = plantSuppliers;
	}

	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}

	public boolean isEditProfile() {
		return editProfile;
	}

	public void setEditProfile(boolean editProfile) {
		this.editProfile = editProfile;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public String list() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		DetachedCriteria criteria = DetachedCriteria.forClass(PlantSupplier.class);
		criteria.add(Restrictions.eq("plant", user.getUserPlant()));

		if (plantSupplier != null) {

			if (plantSupplier.getSupplier() != null && plantSupplier.getSupplier().getCode() != null
					&& plantSupplier.getSupplier().getCode().trim().length() != 0) {
				criteria.createAlias("supplier", "s");
				criteria.add(Restrictions.eq("s.code", plantSupplier.getSupplier().getCode().trim()));
			}

			if (plantSupplier.getSupplier() != null && plantSupplier.getSupplierName() != null
					&& plantSupplier.getSupplierName().trim().length() != 0) {
				criteria.add(Restrictions.eq("supplierName", plantSupplier.getSupplierName().trim()));
			}
		}

		plantSuppliers = this.plantSupplierManager.findByCriteria(criteria);
		return SUCCESS;
	}

	public String cancel() {
		if (!"list".equals(from)) {
			return "mainMenu";
		}
		
		return CANCEL;
	}

	public String delete() {
		this.plantSupplierManager.remove(plantSupplier.getId());
		saveMessage(getText("plantSupplier.deleted"));

		return SUCCESS;
	}

	public String edit() throws Exception {
		HttpServletRequest request = getRequest();
		boolean editProfile = (request.getRequestURI().indexOf("editSupplierProfile") > -1);
		
		if (this.id != 0) {
			plantSupplier = this.plantSupplierManager.get(id);
		} else if (editProfile) {
			User user = userManager.getUserByUsername(request.getRemoteUser());
			this.plantSupplier = this.plantSupplierManager.getPlantSupplier(user.getUserPlant(), user.getUserSupplier());
		} else {
			plantSupplier = new PlantSupplier();
		}

		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}

		boolean isNew = (plantSupplier.getId() == null);

		plantSupplier = this.plantSupplierManager.save(plantSupplier);

		String key = (isNew) ? "plantSupplier.added" : "plantSupplier.updated";
		saveMessage(getText(key));

		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
}