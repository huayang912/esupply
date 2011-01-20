//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.03 at 02:17:36 ���� CST 
//


package com.faurecia.model.order;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * IDoc: Document Item Reference Data
 * 
 * <p>Java class for ORDERS02.E1EDP02 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1EDP02">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="QUALF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BELNR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ZEILE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="DATUM" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UZEIT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BSARK" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="IHREZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="30"/>
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
@XmlType(name = "ORDERS02.E1EDP02", propOrder = {
    "qualf",
    "belnr",
    "zeile",
    "datum",
    "uzeit",
    "bsark",
    "ihrez"
})
public class ORDERS02E1EDP02 {

    @XmlElement(name = "QUALF")
    protected String qualf;
    @XmlElement(name = "BELNR")
    protected String belnr;
    @XmlElement(name = "ZEILE")
    protected String zeile;
    @XmlElement(name = "DATUM")
    protected String datum;
    @XmlElement(name = "UZEIT")
    protected String uzeit;
    @XmlElement(name = "BSARK")
    protected String bsark;
    @XmlElement(name = "IHREZ")
    protected String ihrez;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the qualf property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getQUALF() {
        return qualf;
    }

    /**
     * Sets the value of the qualf property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setQUALF(String value) {
        this.qualf = value;
    }

    /**
     * Gets the value of the belnr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBELNR() {
        return belnr;
    }

    /**
     * Sets the value of the belnr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBELNR(String value) {
        this.belnr = value;
    }

    /**
     * Gets the value of the zeile property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getZEILE() {
        return zeile;
    }

    /**
     * Sets the value of the zeile property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setZEILE(String value) {
        this.zeile = value;
    }

    /**
     * Gets the value of the datum property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getDATUM() {
        return datum;
    }

    /**
     * Sets the value of the datum property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setDATUM(String value) {
        this.datum = value;
    }

    /**
     * Gets the value of the uzeit property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUZEIT() {
        return uzeit;
    }

    /**
     * Sets the value of the uzeit property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUZEIT(String value) {
        this.uzeit = value;
    }

    /**
     * Gets the value of the bsark property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBSARK() {
        return bsark;
    }

    /**
     * Sets the value of the bsark property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBSARK(String value) {
        this.bsark = value;
    }

    /**
     * Gets the value of the ihrez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getIHREZ() {
        return ihrez;
    }

    /**
     * Sets the value of the ihrez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setIHREZ(String value) {
        this.ihrez = value;
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