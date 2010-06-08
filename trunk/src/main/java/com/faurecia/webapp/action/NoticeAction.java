package com.faurecia.webapp.action;

import java.io.File;
import java.net.MalformedURLException;
import java.util.List;

import javax.servlet.http.HttpServletRequest;
import javax.xml.bind.JAXBException;

import com.faurecia.model.Notice;
import com.faurecia.model.User;
import com.faurecia.service.NoticeManager;

public class NoticeAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -7684641539522685901L;
	private NoticeManager noticeManager;
	private List<Notice> notices;
	private Notice notice;
	private String code;
	private File file;
	private String fileContentType;
	private String fileFileName;
	private String title;
	private String content;

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

	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	public void setNoticeManager(NoticeManager noticeManager) {
		this.noticeManager = noticeManager;
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

	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
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

		return SUCCESS;
	}

	public String edit() throws JAXBException, MalformedURLException {
		HttpServletRequest request = getRequest();

		return SUCCESS;
	}

	public String save() throws Exception {
		if (delete != null) {
			return delete();
		}

		return SUCCESS;
	}
}
