package com.faurecia.webapp.action;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.Charset;
import java.util.ArrayList;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Resource;
import com.faurecia.model.Supplier;
import com.faurecia.service.GenericManager;
import com.faurecia.service.NumberControlManager;
import com.faurecia.service.PlantScheduleGroupManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.SupplierManager;
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
	private GenericManager<Resource, Long> resourceManager;
	private PlantSupplierManager plantSupplierManager;
	private PlantScheduleGroupManager plantScheduleGroupManager;
	private SupplierManager supplierManager;
	private NumberControlManager numberControlManager;
	private List<PlantSupplier> plantSuppliers;
	private PlantSupplier plantSupplier;
	private int id;
	private boolean editProfile;
	private InputStream inputStream;
	private String fileName;

	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}

	public void setResourceManager(GenericManager<Resource, Long> resourceManager) {
		this.resourceManager = resourceManager;
	}

	public void setPlantScheduleGroupManager(PlantScheduleGroupManager plantScheduleGroupManager) {
		this.plantScheduleGroupManager = plantScheduleGroupManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public void setNumberControlManager(NumberControlManager numberControlManager) {
		this.numberControlManager = numberControlManager;
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
		if (plantSupplier != null && plantSupplier.getPlant() != null) {
			return plantScheduleGroupManager.getPlantScheduleGroupByPlantCode(plantSupplier.getPlant().getCode());
		} else {
			return new ArrayList<PlantScheduleGroup>();
		}
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
	
	public List<Supplier> getSuppliers() {
		if (plantSupplier != null && plantSupplier.getPlant() != null
				&& plantSupplier.getPlant().getCode() != null && !plantSupplier.getPlant().getCode().equals("-1")) {
			return this.supplierManager.getSuppliersByPlantAndUser(plantSupplier.getPlant().getCode().trim() + "|" + this.getRequest().getRemoteUser());
		} else {
			//return new ArrayList<Supplier>();
			return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
		}
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
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
		if (plantSupplier != null) {
			DetachedCriteria criteria = DetachedCriteria.forClass(PlantSupplier.class);
			criteria.createAlias("supplier", "s");
			criteria.createAlias("plant", "p");

			if (plantSupplier.getPlant() != null && plantSupplier.getPlant().getCode() != null
					&& plantSupplier.getPlant().getCode().trim().length() != 0) {
				if (plantSupplier.getPlant().getCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						criteria.add(Restrictions.in("plant", plants));
					} else {
						criteria.add(Restrictions.eq("p.code", "-1"));
					}

				} else {
					criteria.add(Restrictions.eq("p.code", plantSupplier.getPlant().getCode().trim()));
				}
			}

			if (plantSupplier.getSupplier() != null && plantSupplier.getSupplier().getCode() != null
					&& plantSupplier.getSupplier().getCode().trim().length() != 0) {
				if (plantSupplier.getSupplier().getCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null && suppliers.size() > 0) {
						criteria.add(Restrictions.in("supplier", suppliers));
					} else {
						criteria.add(Restrictions.eq("s.code", "-1"));
					}
				} else {
					criteria.add(Restrictions.eq("s.code", plantSupplier.getSupplier().getCode().trim()));
				}
			}

			if (plantSupplier.getSupplier() != null && plantSupplier.getSupplierName() != null
					&& plantSupplier.getSupplierName().trim().length() != 0) {
				criteria.add(Restrictions.eq("supplierName", plantSupplier.getSupplierName().trim()));
			}
			plantSuppliers = this.plantSupplierManager.findByCriteria(criteria);
		}

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
		if (this.id != 0) {
			plantSupplier = this.plantSupplierManager.get(id);
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

		PlantSupplier oldPlantSupplier = isNew ? new PlantSupplier() : this.plantSupplierManager.get(plantSupplier.getId());

		if (isNew) {
			Supplier supplier = null;
			
			Plant plant = plantManager.get(plantSupplier.getPlant().getCode());
			if (this.supplierManager.exists(plantSupplier.getSupplier().getCode())) {
				supplier = this.supplierManager.get(plantSupplier.getSupplier().getCode());
			} else {
				supplier = new Supplier();
				supplier.setCode(plantSupplier.getSupplier().getCode());
				supplier.setName(plantSupplier.getSupplierName());

				this.supplierManager.save(supplier);
				
				Resource resource = new Resource();
				resource.setCode(supplier.getCode());
				resource.setDescription(supplier.getName());
				resource.setType("supplier");
				this.resourceManager.save(resource);

				/*User supplierUser = new User();
				supplierUser.setUsername(supplier.getCode()); // 使用供应商编码作为用户名称
				supplierUser.setEnabled(true);
				supplierUser.setAccountExpired(false);
				supplierUser.setAccountLocked(false);
				supplierUser.setEmail(plant.getSupplierNotifyEmail());
				supplierUser.setPassword(RandomStringUtils.random(6, true, true));
				supplierUser.setConfirmPassword(supplierUser.getPassword());
				supplierUser.setFirstName(supplier.getName() != null ? supplier.getName() : supplier.getCode());
				// supplierUser.setLastName(supplier.getName() != null ?
				// supplier.getName() : supplier.getCode());
				supplierUser.setLastName("");
				supplierUser.setUserSupplier(supplier);
				// supplierUser.setUserPlant(plant);
				this.userManager.saveUser(supplierUser);*/
			}

			oldPlantSupplier.setDoNoPrefix(String.valueOf(this.numberControlManager.getNextNumber(Constants.DO_NO_PREFIX)));
			oldPlantSupplier.setPlant(plant);
			oldPlantSupplier.setSupplier(supplier);
		}

		oldPlantSupplier.setSupplierName(plantSupplier.getSupplierName());
		oldPlantSupplier.setSupplierAddress1(plantSupplier.getSupplierAddress1());
		oldPlantSupplier.setSupplierAddress2(plantSupplier.getSupplierAddress2());
		oldPlantSupplier.setSupplierContactPerson(plantSupplier.getSupplierContactPerson());
		oldPlantSupplier.setSupplierPhone(plantSupplier.getSupplierPhone());
		oldPlantSupplier.setSupplierFax(plantSupplier.getSupplierFax());
		oldPlantSupplier.setDoTemplateName(plantSupplier.getDoTemplateName());
		oldPlantSupplier.setBoxTemplateName(plantSupplier.getBoxTemplateName());
		/*if (!this.getRequest().isUserInRole(Constants.VENDOR_ROLE)) {
			oldPlantSupplier.setPlantScheduleGroup(plantSupplier.getPlantScheduleGroup());
			if (plantSupplier.getResponsibleUser() != null && plantSupplier.getResponsibleUser().getId() != null) {
				User user = this.userManager.getUser(plantSupplier.getResponsibleUser().getId().toString());
				oldPlantSupplier.setResponsibleUser(user);
			}
		}*/

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
		DetachedCriteria criteria = DetachedCriteria.forClass(PlantSupplier.class);
		
		List<Plant> plants = getPlants();
		criteria.add(Restrictions.in("plant", plants));
		
		List<Supplier> suppliers = getSuppliers();
		criteria.add(Restrictions.in("supplier", suppliers));

		plantSuppliers = this.plantSupplierManager.findByCriteria(criteria);

		if (plantSuppliers != null && plantSuppliers.size() > 0) {
			fileName = "supplier.csv";
			ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

			CSVWriter writer = new CSVWriter(outputStream, ',', Charset.forName("GBK"));
			for (int i = 0; i < plantSuppliers.size(); i++) {
				PlantSupplier plantSupplier = plantSuppliers.get(i);
				String[] entries = new String[7];

				entries[0] = plantSupplier.getSupplier().getCode();
				entries[1] = plantSupplier.getSupplierName();
				entries[2] = plantSupplier.getSupplierAddress1();
				entries[3] = plantSupplier.getSupplierAddress2();
				entries[4] = plantSupplier.getSupplierContactPerson();
				entries[5] = plantSupplier.getSupplierPhone();
				entries[6] = plantSupplier.getSupplierFax();

				writer.writeRecord(entries);
			}
			writer.close();
			inputStream = new ByteArrayInputStream(outputStream.toByteArray());
			
			return SUCCESS;
		}

		return INPUT;
	}
}