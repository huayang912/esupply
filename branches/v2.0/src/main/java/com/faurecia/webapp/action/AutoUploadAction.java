package com.faurecia.webapp.action;

import java.io.File;
import java.io.FileOutputStream;

import org.apache.commons.io.FileUtils;

public class AutoUploadAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = 1L;
	private String fileContent;
	private String fileName;
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
	
	public void setFileContent(String fileContent) {
		this.fileContent = fileContent;
	}

	public void setFileName(String fileName) {
		this.fileName = fileName;
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
		if (fileName != null && fileName.trim().length() > 0) {
			try {
				FileUtils.forceMkdir(new File(serverFileFolder));
				File destFile = new File(serverFileFolder + File.separator + fileName);
				FileOutputStream fileOutputStream = new FileOutputStream(destFile);
				fileOutputStream.write(fileContent.getBytes());
				fileOutputStream.flush();
				fileOutputStream.close();
				String[] args = new String[] {fileName};
				saveMessage(getText("autoUpload.success", args));
				uploadSuccess = true;
			} catch (Exception e) {
				String[] args = new String[] {fileName, e.getMessage()};
				saveMessage(getText("errors.autoUpload.fail", args));
				uploadSuccess = false;
				e.printStackTrace();
			}
		}
		return SUCCESS;
	}
}
