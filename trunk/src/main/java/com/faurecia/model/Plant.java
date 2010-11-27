package com.faurecia.model;

import java.io.Serializable;
import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.Id;
import javax.persistence.Table;
import javax.persistence.Transient;
import javax.persistence.Version;

import org.apache.commons.lang.builder.ToStringBuilder;
import org.apache.commons.lang.builder.ToStringStyle;

@Entity
@Table(name = "plant")
public class Plant extends BaseObject implements Serializable {

	private static final long serialVersionUID = -1175522302415845678L;
	private String code;
	private String name;
	private String address1;
	private String address2;
	private String contactPerson;
	private String phone;
	private String fax;
	private String ftpServer;
	private Integer ftpPort;
	private String ftpUser;
	private String ftpPassword;
	private String confirmFtpPassword;
	private String ftpPath;
	private String tempFileDirectory;
	private String archiveFileDirectory;
	private String errorFileDirectory;
	private Integer inboundIntervalType;
	private Integer inboundInterval;
	private Date nextInboundDate;
	private Integer outboundIntervalType;
	private Integer outboundInterval;
	private Date nextOutboundDate;
	private String errorLogEmail1;
	private String errorLogEmail2;
	private String supplierNotifyEmail;
	private String doTemplateName;
	private int version;

	@Id
	@Column(length = 20)
	public String getCode() {
		return code;
	}

	public void setCode(String code) {
		this.code = code;
	}

	@Column(nullable = false, length = 50, unique = true)
	public String getName() {
		return name;
	}

	public void setName(String name) {
		this.name = name;
	}

	@Column(nullable = true, length = 255)
	public String getAddress1() {
		return address1;
	}

	public void setAddress1(String address1) {
		this.address1 = address1;
	}

	@Column(nullable = true, length = 255)
	public String getAddress2() {
		return address2;
	}

	public void setAddress2(String address2) {
		this.address2 = address2;
	}

	@Column(name = "contact_person", nullable = true, length = 50)
	public String getContactPerson() {
		return contactPerson;
	}

	public void setContactPerson(String contactPerson) {
		this.contactPerson = contactPerson;
	}

	@Column(nullable = true, length = 50)
	public String getPhone() {
		return phone;
	}

	public void setPhone(String phone) {
		this.phone = phone;
	}

	@Column(nullable = true, length = 50)
	public String getFax() {
		return fax;
	}

	public void setFax(String fax) {
		this.fax = fax;
	}

	@Column(name = "ftp_server", nullable = true, length = 50)
	public String getFtpServer() {
		return ftpServer;
	}

	public void setFtpServer(String ftpServer) {
		this.ftpServer = ftpServer;
	}

	@Column(name = "ftp_port", nullable = true)
	public Integer getFtpPort() {
		return ftpPort;
	}

	public void setFtpPort(Integer ftpPort) {
		this.ftpPort = ftpPort;
	}

	@Column(name = "ftp_user", nullable = true, length = 50)
	public String getFtpUser() {
		return ftpUser;
	}

	public void setFtpUser(String ftpUser) {
		this.ftpUser = ftpUser;
	}

	@Column(name = "ftp_password", nullable = true, length = 50)
	public String getFtpPassword() {
		return ftpPassword;
	}

	public void setFtpPassword(String ftpPassword) {
		this.ftpPassword = ftpPassword;
	}

	@Transient
	public String getConfirmFtpPassword() {
		return confirmFtpPassword;
	}

	public void setConfirmFtpPassword(String confirmFtpPassword) {
		this.confirmFtpPassword = confirmFtpPassword;
	}

	@Column(name = "ftp_path", nullable = true, length = 50)
	public String getFtpPath() {
		return ftpPath;
	}

	public void setFtpPath(String ftpPath) {
		this.ftpPath = ftpPath;
	}

	@Column(name = "temp_file_directory", nullable = true, length = 50)
	public String getTempFileDirectory() {
		return tempFileDirectory;
	}

	public void setTempFileDirectory(String tempFileDirectory) {
		this.tempFileDirectory = tempFileDirectory;
	}

	@Column(name = "archive_file_directory", nullable = true, length = 50)
	public String getArchiveFileDirectory() {
		return archiveFileDirectory;
	}

	public void setArchiveFileDirectory(String archiveFileDirectory) {
		this.archiveFileDirectory = archiveFileDirectory;
	}

	@Column(name = "error_file_directory", nullable = true, length = 50)
	public String getErrorFileDirectory() {
		return errorFileDirectory;
	}

	public void setErrorFileDirectory(String errorFileDirectory) {
		this.errorFileDirectory = errorFileDirectory;
	}

	@Column(name = "inbound_interval_type", nullable = true)
	public Integer getInboundIntervalType() {
		return inboundIntervalType;
	}

	public void setInboundIntervalType(Integer inboundIntervalType) {
		this.inboundIntervalType = inboundIntervalType;
	}

	@Column(name = "inbound_interval", nullable = true)
	public Integer getInboundInterval() {
		return inboundInterval;
	}

	public void setInboundInterval(Integer inboundInterval) {
		this.inboundInterval = inboundInterval;
	}

	@Column(name = "outbound_interval_type", nullable = true)
	public Integer getOutboundIntervalType() {
		return outboundIntervalType;
	}

	public void setOutboundIntervalType(Integer outboundIntervalType) {
		this.outboundIntervalType = outboundIntervalType;
	}

	@Column(name = "outbound_interval", nullable = true)
	public Integer getOutboundInterval() {
		return outboundInterval;
	}

	public void setOutboundInterval(Integer outboundInterval) {
		this.outboundInterval = outboundInterval;
	}

	@Column(name = "next_inbound_date", nullable = true)
	public Date getNextInboundDate() {
		return nextInboundDate;
	}

	public void setNextInboundDate(Date nextInboundDate) {
		this.nextInboundDate = nextInboundDate;
	}

	@Column(name = "next_outbound_date", nullable = true)
	public Date getNextOutboundDate() {
		return nextOutboundDate;
	}

	public void setNextOutboundDate(Date nextOutboundDate) {
		this.nextOutboundDate = nextOutboundDate;
	}

	@Column(name="error_log_email1", nullable = true, length = 255)
	public String getErrorLogEmail1() {
		return errorLogEmail1;
	}

	public void setErrorLogEmail1(String errorLogEmail1) {
		this.errorLogEmail1 = errorLogEmail1;
	}
	
	@Column(name="error_log_email2", nullable = true, length = 255)
	public String getErrorLogEmail2() {
		return errorLogEmail2;
	}

	public void setErrorLogEmail2(String errorLogEmail2) {
		this.errorLogEmail2 = errorLogEmail2;
	}

	@Column(name="supplier_notify_email", nullable = true, length = 255)
	public String getSupplierNotifyEmail() {
		return supplierNotifyEmail;
	}

	public void setSupplierNotifyEmail(String supplierNotifyEmail) {
		this.supplierNotifyEmail = supplierNotifyEmail;
	}

	@Column(name="do_template_name", nullable = true, length = 255)
	public String getDoTemplateName() {
		return doTemplateName;
	}

	public void setDoTemplateName(String doTemplateName) {
		this.doTemplateName = doTemplateName;
	}

	@Version
	@Column(name="version")
	public void setVersion(int version) {
		this.version = version;
	}

	public int getVersion() {
		return version;
	}

	/**
	 * {@inheritDoc}
	 */
	public boolean equals(Object o) {
		if (this == o) {
			return true;
		}
		if (!(o instanceof Role)) {
			return false;
		}

		final Plant plant = (Plant) o;

		return !(code != null ? !code.equals(plant.code) : plant.code != null);

	}

	/**
	 * {@inheritDoc}
	 */
	public int hashCode() {
		return (code != null ? code.hashCode() : 0);
	}

	/**
	 * {@inheritDoc}
	 */
	public String toString() {
		return new ToStringBuilder(this, ToStringStyle.SIMPLE_STYLE).append(this.code).toString();
	}

	public int compareTo(Object o) {
		return (equals(o) ? 0 : -1);
	}

}
