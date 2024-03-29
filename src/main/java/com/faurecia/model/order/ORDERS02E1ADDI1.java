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
 * IDoc: Additionals
 * 
 * <p>Java class for ORDERS02.E1ADDI1 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1ADDI1">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="ADDIMATNR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDINUMBER" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIVKME" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIFM" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIFM_TXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIKLART" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIKLART_TXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDICLASS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDICLASS_TXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIIDOC" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIMATNR_EXTERNAL" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIMATNR_VERSION" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ADDIMATNR_GUID" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="32"/>
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
@XmlType(name = "ORDERS02.E1ADDI1", propOrder = {
    "addimatnr",
    "addinumber",
    "addivkme",
    "addifm",
    "addifmtxt",
    "addiklart",
    "addiklarttxt",
    "addiclass",
    "addiclasstxt",
    "addiidoc",
    "addimatnrexternal",
    "addimatnrversion",
    "addimatnrguid"
})
public class ORDERS02E1ADDI1 {

    @XmlElement(name = "ADDIMATNR")
    protected String addimatnr;
    @XmlElement(name = "ADDINUMBER")
    protected String addinumber;
    @XmlElement(name = "ADDIVKME")
    protected String addivkme;
    @XmlElement(name = "ADDIFM")
    protected String addifm;
    @XmlElement(name = "ADDIFM_TXT")
    protected String addifmtxt;
    @XmlElement(name = "ADDIKLART")
    protected String addiklart;
    @XmlElement(name = "ADDIKLART_TXT")
    protected String addiklarttxt;
    @XmlElement(name = "ADDICLASS")
    protected String addiclass;
    @XmlElement(name = "ADDICLASS_TXT")
    protected String addiclasstxt;
    @XmlElement(name = "ADDIIDOC")
    protected String addiidoc;
    @XmlElement(name = "ADDIMATNR_EXTERNAL")
    protected String addimatnrexternal;
    @XmlElement(name = "ADDIMATNR_VERSION")
    protected String addimatnrversion;
    @XmlElement(name = "ADDIMATNR_GUID")
    protected String addimatnrguid;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the addimatnr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIMATNR() {
        return addimatnr;
    }

    /**
     * Sets the value of the addimatnr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIMATNR(String value) {
        this.addimatnr = value;
    }

    /**
     * Gets the value of the addinumber property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDINUMBER() {
        return addinumber;
    }

    /**
     * Sets the value of the addinumber property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDINUMBER(String value) {
        this.addinumber = value;
    }

    /**
     * Gets the value of the addivkme property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIVKME() {
        return addivkme;
    }

    /**
     * Sets the value of the addivkme property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIVKME(String value) {
        this.addivkme = value;
    }

    /**
     * Gets the value of the addifm property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIFM() {
        return addifm;
    }

    /**
     * Sets the value of the addifm property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIFM(String value) {
        this.addifm = value;
    }

    /**
     * Gets the value of the addifmtxt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIFMTXT() {
        return addifmtxt;
    }

    /**
     * Sets the value of the addifmtxt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIFMTXT(String value) {
        this.addifmtxt = value;
    }

    /**
     * Gets the value of the addiklart property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIKLART() {
        return addiklart;
    }

    /**
     * Sets the value of the addiklart property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIKLART(String value) {
        this.addiklart = value;
    }

    /**
     * Gets the value of the addiklarttxt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIKLARTTXT() {
        return addiklarttxt;
    }

    /**
     * Sets the value of the addiklarttxt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIKLARTTXT(String value) {
        this.addiklarttxt = value;
    }

    /**
     * Gets the value of the addiclass property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDICLASS() {
        return addiclass;
    }

    /**
     * Sets the value of the addiclass property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDICLASS(String value) {
        this.addiclass = value;
    }

    /**
     * Gets the value of the addiclasstxt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDICLASSTXT() {
        return addiclasstxt;
    }

    /**
     * Sets the value of the addiclasstxt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDICLASSTXT(String value) {
        this.addiclasstxt = value;
    }

    /**
     * Gets the value of the addiidoc property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIIDOC() {
        return addiidoc;
    }

    /**
     * Sets the value of the addiidoc property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIIDOC(String value) {
        this.addiidoc = value;
    }

    /**
     * Gets the value of the addimatnrexternal property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIMATNREXTERNAL() {
        return addimatnrexternal;
    }

    /**
     * Sets the value of the addimatnrexternal property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIMATNREXTERNAL(String value) {
        this.addimatnrexternal = value;
    }

    /**
     * Gets the value of the addimatnrversion property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIMATNRVERSION() {
        return addimatnrversion;
    }

    /**
     * Sets the value of the addimatnrversion property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIMATNRVERSION(String value) {
        this.addimatnrversion = value;
    }

    /**
     * Gets the value of the addimatnrguid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getADDIMATNRGUID() {
        return addimatnrguid;
    }

    /**
     * Sets the value of the addimatnrguid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setADDIMATNRGUID(String value) {
        this.addimatnrguid = value;
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
