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
 * IDoc schedule lines
 * 
 * <p>Java class for ORDERS02.E1EDP20 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1EDP20">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="WMENG" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="AMENG" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EDATU" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EZEIT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EDATU_OLD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EZEIT_OLD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ACTION" minOccurs="0">
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
@XmlType(name = "ORDERS02.E1EDP20", propOrder = {
    "wmeng",
    "ameng",
    "edatu",
    "ezeit",
    "edatuold",
    "ezeitold",
    "action"
})
public class ORDERS02E1EDP20 {

    @XmlElement(name = "WMENG")
    protected String wmeng;
    @XmlElement(name = "AMENG")
    protected String ameng;
    @XmlElement(name = "EDATU")
    protected String edatu;
    @XmlElement(name = "EZEIT")
    protected String ezeit;
    @XmlElement(name = "EDATU_OLD")
    protected String edatuold;
    @XmlElement(name = "EZEIT_OLD")
    protected String ezeitold;
    @XmlElement(name = "ACTION")
    protected String action;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the wmeng property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getWMENG() {
        return wmeng;
    }

    /**
     * Sets the value of the wmeng property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setWMENG(String value) {
        this.wmeng = value;
    }

    /**
     * Gets the value of the ameng property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAMENG() {
        return ameng;
    }

    /**
     * Sets the value of the ameng property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAMENG(String value) {
        this.ameng = value;
    }

    /**
     * Gets the value of the edatu property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEDATU() {
        return edatu;
    }

    /**
     * Sets the value of the edatu property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEDATU(String value) {
        this.edatu = value;
    }

    /**
     * Gets the value of the ezeit property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEZEIT() {
        return ezeit;
    }

    /**
     * Sets the value of the ezeit property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEZEIT(String value) {
        this.ezeit = value;
    }

    /**
     * Gets the value of the edatuold property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEDATUOLD() {
        return edatuold;
    }

    /**
     * Sets the value of the edatuold property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEDATUOLD(String value) {
        this.edatuold = value;
    }

    /**
     * Gets the value of the ezeitold property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEZEITOLD() {
        return ezeitold;
    }

    /**
     * Sets the value of the ezeitold property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEZEITOLD(String value) {
        this.ezeitold = value;
    }

    /**
     * Gets the value of the action property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getACTION() {
        return action;
    }

    /**
     * Sets the value of the action property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setACTION(String value) {
        this.action = value;
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
