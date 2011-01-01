//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.02 at 10:27:48 ���� CST 
//


package com.faurecia.model.delfor;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * IDoc: Header data ROr/JIT for suppliers plus user fields
 * 
 * <p>Java class for DELFOR02.E1EDK09 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DELFOR02.E1EDK09">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="VTRNR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BSTDK" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LABNK" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ZEICH" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="70"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BSTZD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="70"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ABRVW" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KSTAT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="2"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KTEXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="70"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ABNRA" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ABNRD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KTEXT_V" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="USR01" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="USR02" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="USR03" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="USR04" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="USR05" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CYEFZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CYDAT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MFLAUF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MFEIN" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="FFLAUF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="FFEIN" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="E1EDK10" type="{}DELFOR02.E1EDK10" minOccurs="0"/>
 *         &lt;element name="E1EDKA1" type="{}DELFOR02.E1EDKA1" maxOccurs="9999"/>
 *         &lt;element name="E1EDK11" type="{}DELFOR02.E1EDK11" maxOccurs="99999" minOccurs="0"/>
 *         &lt;element name="E1EDP10" type="{}DELFOR02.E1EDP10" maxOccurs="9999"/>
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
@XmlType(name = "DELFOR02.E1EDK09", propOrder = {
    "vtrnr",
    "bstdk",
    "labnk",
    "zeich",
    "bstzd",
    "abrvw",
    "kstat",
    "ktext",
    "abnra",
    "abnrd",
    "ktextv",
    "usr01",
    "usr02",
    "usr03",
    "usr04",
    "usr05",
    "cyefz",
    "cydat",
    "mflauf",
    "mfein",
    "fflauf",
    "ffein",
    "e1EDK10",
    "e1EDKA1",
    "e1EDK11",
    "e1EDP10"
})
public class DELFOR02E1EDK09 {

    @XmlElement(name = "VTRNR")
    protected String vtrnr;
    @XmlElement(name = "BSTDK")
    protected String bstdk;
    @XmlElement(name = "LABNK")
    protected String labnk;
    @XmlElement(name = "ZEICH")
    protected String zeich;
    @XmlElement(name = "BSTZD")
    protected String bstzd;
    @XmlElement(name = "ABRVW")
    protected String abrvw;
    @XmlElement(name = "KSTAT")
    protected String kstat;
    @XmlElement(name = "KTEXT")
    protected String ktext;
    @XmlElement(name = "ABNRA")
    protected String abnra;
    @XmlElement(name = "ABNRD")
    protected String abnrd;
    @XmlElement(name = "KTEXT_V")
    protected String ktextv;
    @XmlElement(name = "USR01")
    protected String usr01;
    @XmlElement(name = "USR02")
    protected String usr02;
    @XmlElement(name = "USR03")
    protected String usr03;
    @XmlElement(name = "USR04")
    protected String usr04;
    @XmlElement(name = "USR05")
    protected String usr05;
    @XmlElement(name = "CYEFZ")
    protected String cyefz;
    @XmlElement(name = "CYDAT")
    protected String cydat;
    @XmlElement(name = "MFLAUF")
    protected String mflauf;
    @XmlElement(name = "MFEIN")
    protected String mfein;
    @XmlElement(name = "FFLAUF")
    protected String fflauf;
    @XmlElement(name = "FFEIN")
    protected String ffein;
    @XmlElement(name = "E1EDK10")
    protected DELFOR02E1EDK10 e1EDK10;
    @XmlElement(name = "E1EDKA1", required = true)
    protected List<DELFOR02E1EDKA1> e1EDKA1;
    @XmlElement(name = "E1EDK11")
    protected List<DELFOR02E1EDK11> e1EDK11;
    @XmlElement(name = "E1EDP10", required = true)
    protected List<DELFOR02E1EDP10> e1EDP10;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the vtrnr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVTRNR() {
        return vtrnr;
    }

    /**
     * Sets the value of the vtrnr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVTRNR(String value) {
        this.vtrnr = value;
    }

    /**
     * Gets the value of the bstdk property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBSTDK() {
        return bstdk;
    }

    /**
     * Sets the value of the bstdk property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBSTDK(String value) {
        this.bstdk = value;
    }

    /**
     * Gets the value of the labnk property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLABNK() {
        return labnk;
    }

    /**
     * Sets the value of the labnk property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLABNK(String value) {
        this.labnk = value;
    }

    /**
     * Gets the value of the zeich property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getZEICH() {
        return zeich;
    }

    /**
     * Sets the value of the zeich property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setZEICH(String value) {
        this.zeich = value;
    }

    /**
     * Gets the value of the bstzd property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBSTZD() {
        return bstzd;
    }

    /**
     * Sets the value of the bstzd property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBSTZD(String value) {
        this.bstzd = value;
    }

    /**
     * Gets the value of the abrvw property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getABRVW() {
        return abrvw;
    }

    /**
     * Sets the value of the abrvw property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setABRVW(String value) {
        this.abrvw = value;
    }

    /**
     * Gets the value of the kstat property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKSTAT() {
        return kstat;
    }

    /**
     * Sets the value of the kstat property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKSTAT(String value) {
        this.kstat = value;
    }

    /**
     * Gets the value of the ktext property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKTEXT() {
        return ktext;
    }

    /**
     * Sets the value of the ktext property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKTEXT(String value) {
        this.ktext = value;
    }

    /**
     * Gets the value of the abnra property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getABNRA() {
        return abnra;
    }

    /**
     * Sets the value of the abnra property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setABNRA(String value) {
        this.abnra = value;
    }

    /**
     * Gets the value of the abnrd property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getABNRD() {
        return abnrd;
    }

    /**
     * Sets the value of the abnrd property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setABNRD(String value) {
        this.abnrd = value;
    }

    /**
     * Gets the value of the ktextv property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKTEXTV() {
        return ktextv;
    }

    /**
     * Sets the value of the ktextv property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKTEXTV(String value) {
        this.ktextv = value;
    }

    /**
     * Gets the value of the usr01 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUSR01() {
        return usr01;
    }

    /**
     * Sets the value of the usr01 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUSR01(String value) {
        this.usr01 = value;
    }

    /**
     * Gets the value of the usr02 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUSR02() {
        return usr02;
    }

    /**
     * Sets the value of the usr02 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUSR02(String value) {
        this.usr02 = value;
    }

    /**
     * Gets the value of the usr03 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUSR03() {
        return usr03;
    }

    /**
     * Sets the value of the usr03 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUSR03(String value) {
        this.usr03 = value;
    }

    /**
     * Gets the value of the usr04 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUSR04() {
        return usr04;
    }

    /**
     * Sets the value of the usr04 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUSR04(String value) {
        this.usr04 = value;
    }

    /**
     * Gets the value of the usr05 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUSR05() {
        return usr05;
    }

    /**
     * Sets the value of the usr05 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUSR05(String value) {
        this.usr05 = value;
    }

    /**
     * Gets the value of the cyefz property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCYEFZ() {
        return cyefz;
    }

    /**
     * Sets the value of the cyefz property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCYEFZ(String value) {
        this.cyefz = value;
    }

    /**
     * Gets the value of the cydat property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCYDAT() {
        return cydat;
    }

    /**
     * Sets the value of the cydat property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCYDAT(String value) {
        this.cydat = value;
    }

    /**
     * Gets the value of the mflauf property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMFLAUF() {
        return mflauf;
    }

    /**
     * Sets the value of the mflauf property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMFLAUF(String value) {
        this.mflauf = value;
    }

    /**
     * Gets the value of the mfein property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMFEIN() {
        return mfein;
    }

    /**
     * Sets the value of the mfein property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMFEIN(String value) {
        this.mfein = value;
    }

    /**
     * Gets the value of the fflauf property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getFFLAUF() {
        return fflauf;
    }

    /**
     * Sets the value of the fflauf property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setFFLAUF(String value) {
        this.fflauf = value;
    }

    /**
     * Gets the value of the ffein property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getFFEIN() {
        return ffein;
    }

    /**
     * Sets the value of the ffein property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setFFEIN(String value) {
        this.ffein = value;
    }

    /**
     * Gets the value of the e1EDK10 property.
     * 
     * @return
     *     possible object is
     *     {@link DELFOR02E1EDK10 }
     *     
     */
    public DELFOR02E1EDK10 getE1EDK10() {
        return e1EDK10;
    }

    /**
     * Sets the value of the e1EDK10 property.
     * 
     * @param value
     *     allowed object is
     *     {@link DELFOR02E1EDK10 }
     *     
     */
    public void setE1EDK10(DELFOR02E1EDK10 value) {
        this.e1EDK10 = value;
    }

    /**
     * Gets the value of the e1EDKA1 property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the e1EDKA1 property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getE1EDKA1().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link DELFOR02E1EDKA1 }
     * 
     * 
     */
    public List<DELFOR02E1EDKA1> getE1EDKA1() {
        if (e1EDKA1 == null) {
            e1EDKA1 = new ArrayList<DELFOR02E1EDKA1>();
        }
        return this.e1EDKA1;
    }

    /**
     * Gets the value of the e1EDK11 property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the e1EDK11 property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getE1EDK11().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link DELFOR02E1EDK11 }
     * 
     * 
     */
    public List<DELFOR02E1EDK11> getE1EDK11() {
        if (e1EDK11 == null) {
            e1EDK11 = new ArrayList<DELFOR02E1EDK11>();
        }
        return this.e1EDK11;
    }

    /**
     * Gets the value of the e1EDP10 property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the e1EDP10 property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getE1EDP10().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link DELFOR02E1EDP10 }
     * 
     * 
     */
    public List<DELFOR02E1EDP10> getE1EDP10() {
        if (e1EDP10 == null) {
            e1EDP10 = new ArrayList<DELFOR02E1EDP10>();
        }
        return this.e1EDP10;
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
