package com.faurecia.webapp.action;

import java.io.File;
import java.io.IOException;

import org.apache.commons.io.FileUtils;

public class AutoUploadAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private File file;
	private String fileContentType;
	private String fileFileName;
	private String serverFileFolder;
	private String localFileFolder;
	private String localbackupFolder;
	private String localFileName;
	private boolean uploadSuccess;
	
	public String getLocalFileName() {
		return localFileName;
	}

	public void setLocalFileName(String localFileName) {
		this.localFileName = localFileName;
	}

	public boolean isUploadSuccess() {
		return uploadSuccess;
	}

	public void setUploadSuccess(boolean uploadSuccess) {
		this.uploadSuccess = uploadSuccess;
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

	public String getServerFileFolder() {
		return serverFileFolder;
	}

	public void setServerFileFolder(String serverFileFolder) {
		this.serverFileFolder = serverFileFolder;
	}

	public String getLocalFileFolder() {
		return localFileFolder;
	}

	public void setLocalFileFolder(String localFileFolder) {
		this.localFileFolder = localFileFolder;
	}

	public String getLocalbackupFolder() {
		return localbackupFolder;
	}

	public void setLocalbackupFolder(String localbackupFolder) {
		this.localbackupFolder = localbackupFolder;
	}

	public String autoUpload() {
		if (fileFileName != null) {
			try {
				FileUtils.forceMkdir(new File(serverFileFolder));
				File destFile = new File(serverFileFolder + File.separator + fileFileName);
				FileUtils.copyFile(file, destFile);
				FileUtils.forceDelete(file);
				String[] args = new String[] {fileFileName};
				saveMessage(getText("autoUpload.success", args));
				uploadSuccess = true;
			} catch (Exception e) {
				String[] args = new String[] {fileFileName, e.getMessage()};
				saveMessage(getText("errors.autoUpload.fail", args));
				uploadSuccess = false;
				e.printStackTrace();
			}
		}
		return SUCCESS;
	}
}
