//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.02 at 10:27:02 ���� CST 
//


package com.faurecia.model.delvry;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * General Text Header
 * 
 * <p>Java class for DELVRY03.E1TXTH9 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DELVRY03.E1TXTH9">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="FUNCTION" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TDOBJECT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TDOBNAME" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="70"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TDID" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TDSPRAS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TDTEXTTYPE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LANGUA_ISO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="2"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="E1TXTP9" type="{}DELVRY03.E1TXTP9" maxOccurs="999999999" minOccurs="0"/>
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
@XmlType(name = "DELVRY03.E1TXTH9", propOrder = {
    "function",
    "tdobject",
    "tdobname",
    "tdid",
    "tdspras",
    "tdtexttype",
    "languaiso",
    "e1TXTP9"
})
public class DELVRY03E1TXTH9 {

    @XmlElement(name = "FUNCTION")
    protected String function;
    @XmlElement(name = "TDOBJECT")
    protected String tdobject;
    @XmlElement(name = "TDOBNAME")
    protected String tdobname;
    @XmlElement(name = "TDID")
    protected String tdid;
    @XmlElement(name = "TDSPRAS")
    protected String tdspras;
    @XmlElement(name = "TDTEXTTYPE")
    protected String tdtexttype;
    @XmlElement(name = "LANGUA_ISO")
    protected String languaiso;
    @XmlElement(name = "E1TXTP9")
    protected List<DELVRY03E1TXTP9> e1TXTP9;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the function property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getFUNCTION() {
        return function;
    }

    /**
     * Sets the value of the function property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setFUNCTION(String value) {
        this.function = value;
    }

    /**
     * Gets the value of the tdobject property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTDOBJECT() {
        return tdobject;
    }

    /**
     * Sets the value of the tdobject property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTDOBJECT(String value) {
        this.tdobject = value;
    }

    /**
     * Gets the value of the tdobname property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTDOBNAME() {
        return tdobname;
    }

    /**
     * Sets the value of the tdobname property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTDOBNAME(String value) {
        this.tdobname = value;
    }

    /**
     * Gets the value of the tdid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTDID() {
        return tdid;
    }

    /**
     * Sets the value of the tdid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTDID(String value) {
        this.tdid = value;
    }

    /**
     * Gets the value of the tdspras property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTDSPRAS() {
        return tdspras;
    }

    /**
     * Sets the value of the tdspras property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTDSPRAS(String value) {
        this.tdspras = value;
    }

    /**
     * Gets the value of the tdtexttype property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTDTEXTTYPE() {
        return tdtexttype;
    }

    /**
     * Sets the value of the tdtexttype property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTDTEXTTYPE(String value) {
        this.tdtexttype = value;
    }

    /**
     * Gets the value of the languaiso property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLANGUAISO() {
        return languaiso;
    }

    /**
     * Sets the value of the languaiso property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLANGUAISO(String value) {
        this.languaiso = value;
    }

    /**
     * Gets the value of the e1TXTP9 property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the e1TXTP9 property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getE1TXTP9().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link DELVRY03E1TXTP9 }
     * 
     * 
     */
    public List<DELVRY03E1TXTP9> getE1TXTP9() {
        if (e1TXTP9 == null) {
            e1TXTP9 = new ArrayList<DELVRY03E1TXTP9>();
        }
        return this.e1TXTP9;
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
