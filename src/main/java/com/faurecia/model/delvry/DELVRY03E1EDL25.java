//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.02 at 10:27:02 ���� CST 
//


package com.faurecia.model.delvry;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * Delivery Item Descriptions
 * 
 * <p>Java class for DELVRY03.E1EDL25 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DELVRY03.E1EDL25">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="LGORT_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="16"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LADGR_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TRAGR_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VKBUR_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VKGRP_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VTWEG_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="SPART_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MFRGR_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *       &lt;/sequence>
 *       &lt;attribute name="SEGMENT" use="required" type="{http://www.w3.org/2001/XMLSchema}string" fixed="1" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "DELVRY03.E1EDL25", propOrder = {
    "lgortbez",
    "ladgrbez",
    "tragrbez",
    "vkburbez",
    "vkgrpbez",
    "vtwegbez",
    "spartbez",
    "mfrgrbez"
})
public class DELVRY03E1EDL25 {

    @XmlElement(name = "LGORT_BEZ")
    protected String lgortbez;
    @XmlElement(name = "LADGR_BEZ")
    protected String ladgrbez;
    @XmlElement(name = "TRAGR_BEZ")
    protected String tragrbez;
    @XmlElement(name = "VKBUR_BEZ")
    protected String vkburbez;
    @XmlElement(name = "VKGRP_BEZ")
    protected String vkgrpbez;
    @XmlElement(name = "VTWEG_BEZ")
    protected String vtwegbez;
    @XmlElement(name = "SPART_BEZ")
    protected String spartbez;
    @XmlElement(name = "MFRGR_BEZ")
    protected String mfrgrbez;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the lgortbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLGORTBEZ() {
        return lgortbez;
    }

    /**
     * Sets the value of the lgortbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLGORTBEZ(String value) {
        this.lgortbez = value;
    }

    /**
     * Gets the value of the ladgrbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLADGRBEZ() {
        return ladgrbez;
    }

    /**
     * Sets the value of the ladgrbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLADGRBEZ(String value) {
        this.ladgrbez = value;
    }

    /**
     * Gets the value of the tragrbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTRAGRBEZ() {
        return tragrbez;
    }

    /**
     * Sets the value of the tragrbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTRAGRBEZ(String value) {
        this.tragrbez = value;
    }

    /**
     * Gets the value of the vkburbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVKBURBEZ() {
        return vkburbez;
    }

    /**
     * Sets the value of the vkburbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVKBURBEZ(String value) {
        this.vkburbez = value;
    }

    /**
     * Gets the value of the vkgrpbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVKGRPBEZ() {
        return vkgrpbez;
    }

    /**
     * Sets the value of the vkgrpbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVKGRPBEZ(String value) {
        this.vkgrpbez = value;
    }

    /**
     * Gets the value of the vtwegbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVTWEGBEZ() {
        return vtwegbez;
    }

    /**
     * Sets the value of the vtwegbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVTWEGBEZ(String value) {
        this.vtwegbez = value;
    }

    /**
     * Gets the value of the spartbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSPARTBEZ() {
        return spartbez;
    }

    /**
     * Sets the value of the spartbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSPARTBEZ(String value) {
        this.spartbez = value;
    }

    /**
     * Gets the value of the mfrgrbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMFRGRBEZ() {
        return mfrgrbez;
    }

    /**
     * Sets the value of the mfrgrbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMFRGRBEZ(String value) {
        this.mfrgrbez = value;
    }

    /**
     * Gets the value of the segment property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSEGMENT() {
        if (segment == null) {
            return "1";
        } else {
            return segment;
        }
    }

    /**
     * Sets the value of the segment property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSEGMENT(String value) {
        this.segment = value;
    }

}
