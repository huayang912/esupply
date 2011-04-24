package com.faurecia.webapp.action;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.Charset;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Role;
import com.faurecia.model.User;
import com.faurecia.service.GenericManager;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.util.CSVWriter;

public class SupplierAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = -6950278025858688589L;
	/**
	 * 
	 */
	private GenericManager<Plant, String> plantManager;
	private PlantSupplierManager plantSupplierManager;
	private PlantScheduleGroupManager plantScheduleGroupManager;
	private List<PlantSupplier> plantSuppliers;
	private PlantSupplier plantSupplier;
	private int id;
	private boolean editProfile;
	private InputStream inputStream;
	private String fileName;

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setPlantScheduleGroupManager(PlantScheduleGroupManager plantScheduleGroupManager) {
		this.plantScheduleGroupManager = plantScheduleGroupManager;
	}

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

	public List<PlantScheduleGroup> getPlantScheduleGroupList() {
		HttpServletRequest request = this.getRequest();
		User user = userManager.getUserByUsername(request.getRemoteUser());
		return plantScheduleGroupManager.getPlantScheduleGroupByPlantCode(user.getUserPlant().getCode());
	}
	
	public List<User> getResponsibleUserList() {
		HttpServletRequest request = this.getRequest();
		User user = userManager.getUserByUsername(request.getRemoteUser());
		Role role = this.roleManager.getRole(Constants.PLANT_USER_ROLE);
		return userManager.getPlantUsers(user.getUserPlant(), role);		
	}

	public Map<String, String> getMFTemplate() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "");
		status.put("Do.png", "EasyLink Standard");
		status.put("DoSebango.png", "EasyLink Standard (With SEBANGO)");
		status.put("DoSebango_CN.png", "EasyLink Standard China (With SEBANGO)");
		status.put("FWAS.png", "FWAS");
		status.put("GSK.png", "GSK");
		status.put("1037.PNG", "Wuxi");
		status.put("1057.PNG", "Shanghai");
		status.put("1545.png", "Guangzhou Automative Seating");
		status.put("1546.png", "Guangzhou Emission Control");
		status.put("1556.png", "Nanjing");
		return status;
	}
	
	public Map<String, String> getBoxTemplate() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "");
		status.put("Box.png", "EasyLink Standard");
		status.put("Box_CN.png", "EasyLink Standard (China)");
		status.put("Box_WuXi.png", "Wuxi");
		return status;
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

	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}
	
	public String list() {
		query();
		return SUCCESS;
	}
	
	private void query() {
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
		this.plantSupplierManager.flushSession();
		return SUCCESS;
	}

	public String edit() throws Exception {
		HttpServletRequest request = getRequest();
		boolean editProfile = (request.getRequestURI().indexOf("editSupplierProfile") > -1);

		if (this.id != 0) {
			plantSupplier = this.plantSupplierManager.get(id);
		} else if (editProfile) {
			if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
				return "mainMenu";
			}
			
			User user = userManager.getUserByUsername(request.getRemoteUser());
			Plant plant = this.plantManager.get((String) this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE));
			this.plantSupplier = this.plantSupplierManager.getPlantSupplier(plant, user.getUserSupplier());
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
		
		PlantSupplier oldPlantSupplier = this.plantSupplierManager.get(plantSupplier.getId());
		oldPlantSupplier.setSupplierName(plantSupplier.getSupplierName());
		oldPlantSupplier.setSupplierAddress1(plantSupplier.getSupplierAddress1());
		oldPlantSupplier.setSupplierAddress2(plantSupplier.getSupplierAddress2());
		oldPlantSupplier.setSupplierContactPerson(plantSupplier.getSupplierContactPerson());
		oldPlantSupplier.setSupplierPhone(plantSupplier.getSupplierPhone());
		oldPlantSupplier.setSupplierFax(plantSupplier.getSupplierFax());
		if (!this.getRequest().isUserInRole(Constants.VENDOR_ROLE)) {
			oldPlantSupplier.setPlantScheduleGroup(plantSupplier.getPlantScheduleGroup());
			if (plantSupplier.getResponsibleUser() != null && plantSupplier.getResponsibleUser().getId() != null) {
				User user = this.userManager.getUser(plantSupplier.getResponsibleUser().getId().toString());
				oldPlantSupplier.setResponsibleUser(user);
			}
			oldPlantSupplier.setDoTemplateName(plantSupplier.getDoTemplateName());
			oldPlantSupplier.setBoxTemplateName(plantSupplier.getBoxTemplateName());
		}

		plantSupplier = this.plantSupplierManager.save(oldPlantSupplier);

		String key = (isNew) ? "plantSupplier.added" : "plantSupplier.updated";
		saveMessage(getText(key));
		this.plantSupplierManager.flushSession();
		if (!isNew) {
			return INPUT;
		} else {
			return SUCCESS;
		}
	}
	
	public String export() throws IOException {
		query();
		
		if (plantSuppliers != null && plantSuppliers.size() > 0) {
			fileName = "supplier.csv";
			ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
	
			CSVWriter writer = new CSVWriter(outputStream, ',', Charset.forName("GBK"));
			for(int i = 0; i < plantSuppliers.size(); i++) 
			{
				PlantSupplier plantSupplier = plantSuppliers.get(i);
				String[] entries = new String[7];
				
				entries[0] =  plantSupplier.getSupplier().getCode();
				entries[1] =  plantSupplier.getSupplierName();
				entries[2] =  plantSupplier.getSupplierAddress1();
				entries[3] =  plantSupplier.getSupplierAddress2();
				entries[4] =  plantSupplier.getSupplierContactPerson();
				entries[5] =  plantSupplier.getSupplierPhone();
				entries[6] =  plantSupplier.getSupplierFax();
			
				writer.writeRecord(entries);
			}
			writer.close();
			inputStream = new ByteArrayInputStream(outputStream.toByteArray());
		}
		
		return SUCCESS;
	}
}
