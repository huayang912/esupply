package com.faurecia.model;

import java.util.ArrayList;
import java.util.Date;
import java.util.List;

import javax.persistence.CascadeType;
import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.NamedQueries;
import javax.persistence.NamedQuery;
import javax.persistence.OneToMany;
import javax.persistence.Table;
import javax.persistence.Transient;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

/**
 * @author ¶¡öÎ
 * 
 */
@Entity
@Table(name = "notice")
@NamedQueries( {
		@NamedQuery(name = "findNoticeByPlant", query = " select n from Notice n where n.plant = :plant order by n.displayDateFrom desc"),
		@NamedQuery(name = "findNoticeByPlantSupplier", query = " select nr.notice from NoticeReader nr inner join nr.notice as n where nr.plantSupplier = :plantSupplier and n.displayDateFrom <= :dateNow and n.displayDateTo >= :dateNow order by n.displayDateFrom desc") })
public class Notice extends BaseObject {

	/**
	 * 
	 */
	private static final long serialVersionUID = -3296230973878530174L;
	private Integer id;
	private String title;
	private String fileName;
	private String fileFullPath;
	private String content;
	private Plant plant;
	private Date displayDateFrom;
	private Date displayDateTo;
	private List<LabelValue> supplierList;
	private List<NoticeReader> noticeReaderList;

	@Id
	@GeneratedValue(strategy = GenerationType.AUTO)
	public Integer getId() {
		return id;
	}

	public void setId(Integer id) {
		this.id = id;
	}

	@Column(nullable = false, length = 255)
	public String getTitle() {
		return title;
	}

	public void setTitle(String title) {
		this.title = title;
	}

	@Column(name = "file_name", nullable = true, length = 255)
	public String getFileName() {
		return fileName;
	}

	public void setFileName(String fileName) {
		this.fileName = fileName;
	}

	@Column(name = "file_full_path", nullable = true, length = 255)
	public String getFileFullPath() {
		return fileFullPath;
	}

	public void setFileFullPath(String fileFullPath) {
		this.fileFullPath = fileFullPath;
	}

	@Column(nullable = false, length = 2000)
	public String getContent() {
		return content;
	}

	public void setContent(String content) {
		this.content = content;
	}

	@ManyToOne
	@JoinColumn(name = "plant_code", nullable = false)
	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}

	@Column(name = "display_date_from", nullable = true)
	public Date getDisplayDateFrom() {
		return displayDateFrom;
	}

	public void setDisplayDateFrom(Date displayDateFrom) {
		this.displayDateFrom = displayDateFrom;
	}

	@Column(name = "display_date_to", nullable = true)
	public Date getDisplayDateTo() {
		return displayDateTo;
	}

	public void setDisplayDateTo(Date displayDateTo) {
		this.displayDateTo = displayDateTo;
	}

	@Transient
	public List<LabelValue> getSupplierList() {
		return supplierList;
	}

	public void setSupplierList(List<LabelValue> supplierList) {
		this.supplierList = supplierList;
	}

	@OneToMany(cascade = CascadeType.ALL, mappedBy = "notice")
	public List<NoticeReader> getNoticeReaderList() {
		return noticeReaderList;
	}

	public void setNoticeReaderList(List<NoticeReader> noticeReaderList) {
		this.noticeReaderList = noticeReaderList;
	}

	public void addNoticeReader(NoticeReader noticeReader) {
		if (noticeReaderList == null) {
			noticeReaderList = new ArrayList<NoticeReader>();
		}

		noticeReaderList.add(noticeReader);
	}

	/**
	 * @see java.lang.Object#toString()
	 */
	public String toString() {
		return new ToStringBuilder(this).append("id", this.id).toString();
	}

	/**
	 * @see java.lang.Object#hashCode()
	 */
	public int hashCode() {
		return new HashCodeBuilder(-1184691281, 1355574935).append(this.id).toHashCode();
	}

	/**
	 * @see java.lang.Object#equals(Object)
	 */
	public boolean equals(Object object) {
		if (!(object instanceof Notice)) {
			return false;
		}
		Notice rhs = (Notice) object;
		return new EqualsBuilder().append(this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		Notice myClass = (Notice) object;
		return new CompareToBuilder().append(this.id, myClass.id).toComparison();
	}

}
