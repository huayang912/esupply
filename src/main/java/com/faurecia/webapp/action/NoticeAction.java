package com.faurecia.webapp.action;

import java.io.File;
import java.net.MalformedURLException;
import java.util.ArrayList;
import java.util.List;

import javax.xml.bind.JAXBException;

import org.apache.commons.beanutils.BeanUtils;
import org.apache.commons.io.FileUtils;

import com.faurecia.model.LabelValue;
import com.faurecia.model.Notice;
import com.faurecia.model.NoticeReader;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.User;
import com.faurecia.service.NoticeManager;
import com.faurecia.service.NoticeReaderManager;
import com.faurecia.service.PlantSupplierManager;

public class NoticeAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7684641539522685901L;
	private NoticeManager noticeManager;
	private PlantSupplierManager plantSupplierManager;
	private NoticeReaderManager noticeReaderManager;
	private List<Notice> notices;
	private Notice notice;
	private Integer id;
	private File file;
	private String fileContentType;
	private String fileFileName;
	private String uploadFileDirectory;
	
	private List<LabelValue> availableSuppliers;

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

	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
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

	public File getFile() {
		return file;
	}

	public void setFile(File file) {
		this.file = file;
	}

	public String getFileContentType() {
		return fileContentType;
	}

	public void setFileContentType(String fileContentType) {
		this.fileContentType = fileContentType;
	}

	public String getFileFileName() {
		return fileFileName;
	}

	public void setFileFileName(String fileFileName) {
		this.fileFileName = fileFileName;
	}

	public List<LabelValue> getAvailableSuppliers() {
		return availableSuppliers;
	}

	public void setAvailableSuppliers(List<LabelValue> availableSuppliers) {
		this.availableSuppliers = availableSuppliers;
	}

	public String getUploadFileDirectory() {
		return uploadFileDirectory;
	}

	public void setUploadFileDirectory(String uploadFileDirectory) {
		this.uploadFileDirectory = uploadFileDirectory;
	}

	public String list() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		notices = this.noticeManager.getNoticeByPlant(user.getUserPlant());
		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String delete() {
		this.noticeManager.remove(notice.getId());
		saveMessage(getText("notice.deleted"));
		this.noticeManager.flushSession();
		return SUCCESS;
	}

	public String edit() throws JAXBException, MalformedURLException {
		if (this.id != null && this.id > 0) {
			notice = this.noticeManager.get(this.id);
		} else {
			notice = new Notice();
		}
		prepare();
		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}
		
		boolean isNew = (notice.getId() == null);
		
		if (!isNew) {
			Notice oldNotice = this.noticeManager.get(notice.getId());
			notice.setFileFullPath(oldNotice.getFileFullPath());
			notice.setFileName(oldNotice.getFileName());
			BeanUtils.copyProperties(oldNotice, notice);	
			notice = oldNotice;
		}
		
		if (fileFileName != null) {
			FileUtils.forceMkdir(new File(uploadFileDirectory));
			
			File destFile = new File(uploadFileDirectory + File.separator + file.getName());
			FileUtils.copyFile(file, destFile);
			
			FileUtils.forceDelete(file);
			
			notice.setFileFullPath(destFile.getAbsolutePath());
			notice.setFileName(fileFileName);
		}
		
		String[] suppliers = getRequest().getParameterValues("suppliers");
		
		if (!isNew) {
			this.noticeReaderManager.deleteNoticeReaderByNoticeId(notice.getId());
		}
		
		for (int i = 0; suppliers != null && i < suppliers.length; i++) {
			Integer plantSupplierId = Integer.parseInt(suppliers[i]);
			PlantSupplier plantSupplier = this.plantSupplierManager.get(plantSupplierId);
			
			NoticeReader noticeReader = new NoticeReader();
			noticeReader.setNotice(notice);
			noticeReader.setPlantSupplier(plantSupplier);
			
			notice.addNoticeReader(noticeReader);
		}
		
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		notice.setPlant(user.getUserPlant());
		
		this.noticeManager.save(notice);
		this.noticeManager.flushSession();
		String key = (isNew) ? "notice.added" : "notice.updated";
		saveMessage(getText(key));

		return SUCCESS;
	}
	
	private void prepare() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);
		
		if (notice != null 
				&& notice.getId() != null && notice.getId() > 0) {
			List<NoticeReader> noticeReaderList = this.noticeReaderManager.getNoticeReaderByNoticeId(notice.getId());
			if (noticeReaderList != null && noticeReaderList.size() > 0) {
				notice.setSupplierList(new ArrayList<LabelValue>());
				for (int i = 0; i < noticeReaderList.size(); i++) {
					notice.getSupplierList().add(new LabelValue(noticeReaderList.get(i).getPlantSupplier().getSupplierName(), noticeReaderList.get(i).getPlantSupplier().getId().toString()));
				}
			}
			
			List<NoticeReader> readNoticeReaderList = this.noticeReaderManager.getReadNoticeReaderByNoticeId(notice.getId());
			if (readNoticeReaderList != null && readNoticeReaderList.size() > 0) {
				notice.setReadList(new ArrayList<LabelValue>());
				for (int i = 0; i < readNoticeReaderList.size(); i++) {
					notice.getReadList().add(new LabelValue(readNoticeReaderList.get(i).getPlantSupplier().getSupplierName(), readNoticeReaderList.get(i).getPlantSupplier().getId().toString()));
				}
			}
		}
		
		List<PlantSupplier> allPlantSupplierList = this.plantSupplierManager.getPlantSupplierByPlantCode(user.getUserPlant().getCode());
		if (allPlantSupplierList != null && allPlantSupplierList.size() > 0) {
			this.availableSuppliers = new ArrayList<LabelValue>();
			for (int i = 0; i < allPlantSupplierList.size(); i++) {
				boolean notInSupplierList = true;
				if (notice.getSupplierList() != null && notice.getSupplierList().size() > 0) {
					for (int j = 0; j < notice.getSupplierList().size(); j++) {
						if (allPlantSupplierList.get(i).getId().toString().equals(notice.getSupplierList().get(j).getValue())) {
							notInSupplierList = false;
						}					
					}
				}
				
				if (notInSupplierList) {
					this.availableSuppliers.add(new LabelValue(allPlantSupplierList.get(i).getSupplierName(), allPlantSupplierList.get(i).getId().toString()));
				}
			}
		}
	}
}
