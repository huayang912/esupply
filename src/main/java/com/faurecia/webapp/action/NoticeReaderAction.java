package com.faurecia.webapp.action;

import java.io.FileInputStream;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.util.List;

import javax.xml.bind.JAXBException;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.NoticeReader;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Supplier;
import com.faurecia.service.NoticeManager;
import com.faurecia.service.NoticeReaderManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.SupplierManager;

public class NoticeReaderAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private NoticeManager noticeManager;
	private PlantSupplierManager plantSupplierManager;
	private SupplierManager supplierManager;
	private List<NoticeReader> noticeReaders;
	private NoticeReader noticeReader;
	private int id;
	private InputStream inputStream;
	private String fileName;
	private NoticeReaderManager noticeReaderManager;
	private PlantSupplier plantSupplier;

	public int getId() {
		return id;
	}

	public void setId(int id) {
		this.id = id;
	}
	
	public List<NoticeReader> getNoticeReaders() {
		return noticeReaders;
	}

	public void setNoticeReaders(List<NoticeReader> noticeReaders) {
		this.noticeReaders = noticeReaders;
	}

	public NoticeReader getNoticeReader() {
		return noticeReader;
	}

	public void setNoticeReader(NoticeReader noticeReader) {
		this.noticeReader = noticeReader;
	}

	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}

	public PlantSupplier getPlantSupplier() {
		return plantSupplier;
	}

	public void setPlantSupplier(PlantSupplier plantSupplier) {
		this.plantSupplier = plantSupplier;
	}

	public void setNoticeManager(NoticeManager noticeManager) {
		this.noticeManager = noticeManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setNoticeReaderManager(NoticeReaderManager noticeReaderManager) {
		this.noticeReaderManager = noticeReaderManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
	}

	public List<Supplier> getSuppliers() {
		if (plantSupplier != null && plantSupplier.getPlant() != null
				&& plantSupplier.getPlant().getCode() != null && !plantSupplier.getPlant().getCode().equals("-1")) {
			return this.supplierManager.getSuppliersByPlantAndUser(plantSupplier.getPlant().getCode().trim() + "|" + this.getRequest().getRemoteUser());
		} else {
			return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
		}
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
	}

	public String list() {
		if (plantSupplier != null) {
			DetachedCriteria criteria = DetachedCriteria.forClass(NoticeReader.class);
			
			criteria.createAlias("plantSupplier", "ps");
			criteria.createAlias("ps.plant", "p");
			criteria.createAlias("ps.supplier", "s");
			
			if (plantSupplier.getPlant() != null && plantSupplier.getPlant().getCode().trim().length() > 0) {

				if (plantSupplier.getPlant().getCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						criteria.add(Restrictions.in("ps.plant", plants));
					} else {
						criteria.add(Restrictions.eq("p.code", "-1"));
					}
				} else {
					criteria.add(Restrictions.eq("p.code", plantSupplier.getPlant().getCode()));
				}
			}			

			if (plantSupplier.getSupplier() != null && plantSupplier.getSupplier().getCode().trim().length() > 0) {
				if (plantSupplier.getSupplier().getCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null && suppliers.size() > 0) {
						criteria.add(Restrictions.in("ps.supplier", suppliers));
					} else {
						criteria.add(Restrictions.eq("s.code", "-1"));
					}
				} else {
					criteria.add(Restrictions.eq("s.code", plantSupplier.getSupplier().getCode()));
				}
			}

			noticeReaders = this.noticeManager.findByCriteria(criteria);
		}
		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws JAXBException, MalformedURLException {
		noticeReader = this.noticeReaderManager.get(id);
		if (noticeReader != null) {
			noticeReader.setIsRead(true);
			this.noticeReaderManager.save(noticeReader);
		}

		return SUCCESS;
	}

	public String download() throws Exception {
		noticeReader = this.noticeReaderManager.get(this.id);
		fileName = noticeReader.getNotice().getFileName();
		inputStream = new FileInputStream(noticeReader.getNotice().getFileFullPath());

		return SUCCESS;
	}
}
