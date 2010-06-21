package com.faurecia.webapp.action;

import java.io.FileInputStream;
import java.io.InputStream;
import java.net.MalformedURLException;
import java.util.List;

import javax.xml.bind.JAXBException;

import com.faurecia.Constants;
import com.faurecia.model.Notice;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.User;
import com.faurecia.service.GenericManager;
import com.faurecia.service.NoticeManager;
import com.faurecia.service.PlantSupplierManager;

public class NoticeReaderAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;

	private NoticeManager noticeManager;
	private PlantSupplierManager plantSupplierManager;
	private GenericManager<Plant, String> plantManager;
	private List<Notice> notices;
	private Notice notice;
	private int id;
	private InputStream inputStream;
	private String fileName;
	
	public int getId() {
		return id;
	}
	public void setId(int id) {
		this.id = id;
	}
	public List<Notice> getNotices() {
		return notices;
	}
	public void setNotices(List<Notice> notices) {
		this.notices = notices;
	}
	public Notice getNotice() {
		return notice;
	}
	public void setNotice(Notice notice) {
		this.notice = notice;
	}
	
	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}
	
	public void setNoticeManager(NoticeManager noticeManager) {
		this.noticeManager = noticeManager;
	}
	
	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}
	
	public void setPlantManager(GenericManager<Plant, String> plantManager) {
		this.plantManager = plantManager;
	}
	
	public String list() {
		if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
			return "mainMenu";
		}
		
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		Plant plant = this.plantManager.get((String)this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE));
		PlantSupplier plantSupplier = this.plantSupplierManager.getPlantSupplier(plant, user.getUserSupplier());
		notices = this.noticeManager.getNoticeByPlantSupplier(plantSupplier);
		return SUCCESS;
	}
	
	public String cancel() {		
		return CANCEL;
	}
	
	public String edit() throws JAXBException, MalformedURLException {
		notice = this.noticeManager.get(id);

		return SUCCESS;
	}
	
	public String download() throws Exception {
		notice = this.noticeManager.get(this.id);
		fileName = notice.getFileName();
		inputStream = new FileInputStream(notice.getFileFullPath());
		
		return SUCCESS;
	}
}
