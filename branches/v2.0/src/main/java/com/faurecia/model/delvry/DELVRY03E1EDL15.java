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
 * IDOC: Batch Characteristic
 * 
 * <p>Java class for DELVRY03.E1EDL15 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DELVRY03.E1EDL15">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="ATINN" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ATNAM" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="30"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ATBEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="30"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ATWRT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="30"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ATWTB" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="30"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EWAHR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="24"/>
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
@XmlType(name = "DELVRY03.E1EDL15", propOrder = {
    "atinn",
    "atnam",
    "atbez",
    "atwrt",
    "atwtb",
    "ewahr"
})
public class DELVRY03E1EDL15 {

    @XmlElement(name = "ATINN")
    protected String atinn;
    @XmlElement(name = "ATNAM")
    protected String atnam;
    @XmlElement(name = "ATBEZ")
    protected String atbez;
    @XmlElement(name = "ATWRT")
    protected String atwrt;
    @XmlElement(name = "ATWTB")
    protected String atwtb;
    @XmlElement(name = "EWAHR")
    protected String ewahr;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the atinn property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getATINN() {
        return atinn;
    }

    /**
     * Sets the value of the atinn property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setATINN(String value) {
        this.atinn = value;
    }

    /**
     * Gets the value of the atnam property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getATNAM() {
        return atnam;
    }

    /**
     * Sets the value of the atnam property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setATNAM(String value) {
        this.atnam = value;
    }

    /**
     * Gets the value of the atbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getATBEZ() {
        return atbez;
    }

    /**
     * Sets the value of the atbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setATBEZ(String value) {
        this.atbez = value;
    }

    /**
     * Gets the value of the atwrt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getATWRT() {
        return atwrt;
    }

    /**
     * Sets the value of the atwrt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setATWRT(String value) {
        this.atwrt = value;
    }

    /**
     * Gets the value of the atwtb property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getATWTB() {
        return atwtb;
    }

    /**
     * Sets the value of the atwtb property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setATWTB(String value) {
        this.atwtb = value;
    }

    /**
     * Gets the value of the ewahr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEWAHR() {
        return ewahr;
    }

    /**
     * Sets the value of the ewahr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEWAHR(String value) {
        this.ewahr = value;
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