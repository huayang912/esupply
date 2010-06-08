package com.faurecia.model;

import java.util.Date;

import javax.persistence.Column;
import javax.persistence.Entity;
import javax.persistence.GeneratedValue;
import javax.persistence.GenerationType;
import javax.persistence.Id;
import javax.persistence.JoinColumn;
import javax.persistence.ManyToOne;
import javax.persistence.Table;

import org.apache.commons.lang.builder.CompareToBuilder;
import org.apache.commons.lang.builder.EqualsBuilder;
import org.apache.commons.lang.builder.HashCodeBuilder;
import org.apache.commons.lang.builder.ToStringBuilder;

@Entity
@Table(name = "notice")
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

	@Column(name="file_name", nullable = true, length = 255)
	public String getFileName() {
		return fileName;
	}

	public void setFileName(String fileName) {
		this.fileName = fileName;
	}

	@Column(name="file_full_path", nullable = true, length = 255)
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
	@JoinColumn(name = "plant_code")
	public Plant getPlant() {
		return plant;
	}

	public void setPlant(Plant plant) {
		this.plant = plant;
	}

	@Column(name="display_date_from", nullable = true)
	public Date getDisplayDateFrom() {
		return displayDateFrom;
	}

	public void setDisplayDateFrom(Date displayDateFrom) {
		this.displayDateFrom = displayDateFrom;
	}

	@Column(name="display_date_to", nullable = true)
	public Date getDisplayDateTo() {
		return displayDateTo;
	}

	public void setDisplayDateTo(Date displayDateTo) {
		this.displayDateTo = displayDateTo;
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
		return new EqualsBuilder().append(
				this.id, rhs.id).isEquals();
	}

	/**
	 * @see java.lang.Comparable#compareTo(Object)
	 */
	public int compareTo(Object object) {
		Notice myClass = (Notice) object;
		return new CompareToBuilder().append(this.id, myClass.id)
				.toComparison();
	}
	

}
