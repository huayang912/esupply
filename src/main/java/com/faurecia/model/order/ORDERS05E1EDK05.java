//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.02 at 10:25:07 ���� CST 
//


package com.faurecia.model.order;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * IDoc: Document header conditions
 * 
 * <p>Java class for ORDERS05.E1EDK05 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS05.E1EDK05">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="ALCKZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KSCHL" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KOTXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="80"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BETRG" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KPERC" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KRATE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UPRBS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="9"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MEAUN" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KOBTR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MWSKZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="7"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MSATZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KOEIN" minOccurs="0">
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
@XmlType(name = "ORDERS05.E1EDK05", propOrder = {
    "alckz",
    "kschl",
    "kotxt",
    "betrg",
    "kperc",
    "krate",
    "uprbs",
    "meaun",
    "kobtr",
    "mwskz",
    "msatz",
    "koein"
})
public class ORDERS05E1EDK05 {

    @XmlElement(name = "ALCKZ")
    protected String alckz;
    @XmlElement(name = "KSCHL")
    protected String kschl;
    @XmlElement(name = "KOTXT")
    protected String kotxt;
    @XmlElement(name = "BETRG")
    protected String betrg;
    @XmlElement(name = "KPERC")
    protected String kperc;
    @XmlElement(name = "KRATE")
    protected String krate;
    @XmlElement(name = "UPRBS")
    protected String uprbs;
    @XmlElement(name = "MEAUN")
    protected String meaun;
    @XmlElement(name = "KOBTR")
    protected String kobtr;
    @XmlElement(name = "MWSKZ")
    protected String mwskz;
    @XmlElement(name = "MSATZ")
    protected String msatz;
    @XmlElement(name = "KOEIN")
    protected String koein;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the alckz property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getALCKZ() {
        return alckz;
    }

    /**
     * Sets the value of the alckz property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setALCKZ(String value) {
        this.alckz = value;
    }

    /**
     * Gets the value of the kschl property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKSCHL() {
        return kschl;
    }

    /**
     * Sets the value of the kschl property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKSCHL(String value) {
        this.kschl = value;
    }

    /**
     * Gets the value of the kotxt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKOTXT() {
        return kotxt;
    }

    /**
     * Sets the value of the kotxt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKOTXT(String value) {
        this.kotxt = value;
    }

    /**
     * Gets the value of the betrg property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBETRG() {
        return betrg;
    }

    /**
     * Sets the value of the betrg property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBETRG(String value) {
        this.betrg = value;
    }

    /**
     * Gets the value of the kperc property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKPERC() {
        return kperc;
    }

    /**
     * Sets the value of the kperc property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKPERC(String value) {
        this.kperc = value;
    }

    /**
     * Gets the value of the krate property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKRATE() {
        return krate;
    }

    /**
     * Sets the value of the krate property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKRATE(String value) {
        this.krate = value;
    }

    /**
     * Gets the value of the uprbs property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUPRBS() {
        return uprbs;
    }

    /**
     * Sets the value of the uprbs property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUPRBS(String value) {
        this.uprbs = value;
    }

    /**
     * Gets the value of the meaun property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMEAUN() {
        return meaun;
    }

    /**
     * Sets the value of the meaun property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMEAUN(String value) {
        this.meaun = value;
    }

    /**
     * Gets the value of the kobtr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKOBTR() {
        return kobtr;
    }

    /**
     * Sets the value of the kobtr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKOBTR(String value) {
        this.kobtr = value;
    }

    /**
     * Gets the value of the mwskz property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMWSKZ() {
        return mwskz;
    }

    /**
     * Sets the value of the mwskz property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMWSKZ(String value) {
        this.mwskz = value;
    }

    /**
     * Gets the value of the msatz property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMSATZ() {
        return msatz;
    }

    /**
     * Sets the value of the msatz property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMSATZ(String value) {
        this.msatz = value;
    }

    /**
     * Gets the value of the koein property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKOEIN() {
        return koein;
    }

    /**
     * Sets the value of the koein property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKOEIN(String value) {
        this.koein = value;
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
