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
 * Delivery Item POD
 * 
 * <p>Java class for DELVRY03.E1EDL53 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DELVRY03.E1EDL53">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="GRUND" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="PODMG" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LFIMG_DIFF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VRKME" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LGMNG_DIFF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MEINS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
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
@XmlType(name = "DELVRY03.E1EDL53", propOrder = {
    "grund",
    "podmg",
    "lfimgdiff",
    "vrkme",
    "lgmngdiff",
    "meins"
})
public class DELVRY03E1EDL53 {

    @XmlElement(name = "GRUND")
    protected String grund;
    @XmlElement(name = "PODMG")
    protected String podmg;
    @XmlElement(name = "LFIMG_DIFF")
    protected String lfimgdiff;
    @XmlElement(name = "VRKME")
    protected String vrkme;
    @XmlElement(name = "LGMNG_DIFF")
    protected String lgmngdiff;
    @XmlElement(name = "MEINS")
    protected String meins;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the grund property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getGRUND() {
        return grund;
    }

    /**
     * Sets the value of the grund property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setGRUND(String value) {
        this.grund = value;
    }

    /**
     * Gets the value of the podmg property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPODMG() {
        return podmg;
    }

    /**
     * Sets the value of the podmg property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPODMG(String value) {
        this.podmg = value;
    }

    /**
     * Gets the value of the lfimgdiff property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLFIMGDIFF() {
        return lfimgdiff;
    }

    /**
     * Sets the value of the lfimgdiff property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLFIMGDIFF(String value) {
        this.lfimgdiff = value;
    }

    /**
     * Gets the value of the vrkme property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVRKME() {
        return vrkme;
    }

    /**
     * Sets the value of the vrkme property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVRKME(String value) {
        this.vrkme = value;
    }

    /**
     * Gets the value of the lgmngdiff property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLGMNGDIFF() {
        return lgmngdiff;
    }

    /**
     * Sets the value of the lgmngdiff property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLGMNGDIFF(String value) {
        this.lgmngdiff = value;
    }

    /**
     * Gets the value of the meins property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMEINS() {
        return meins;
    }

    /**
     * Sets the value of the meins property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMEINS(String value) {
        this.meins = value;
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
