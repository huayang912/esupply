//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.03 at 02:17:36 ���� CST 
//


package com.faurecia.model.order;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * IDoc: Document Header Text Identification
 * 
 * <p>Java class for ORDERS02.E1EDKT1 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1EDKT1">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="TDID" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TSSPRAS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TSSPRAS_ISO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="2"/>
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
 *         &lt;element name="E1EDKT2" type="{}ORDERS02.E1EDKT2" maxOccurs="999999999" minOccurs="0"/>
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
@XmlType(name = "ORDERS02.E1EDKT1", propOrder = {
    "tdid",
    "tsspras",
    "tssprasiso",
    "tdobject",
    "tdobname",
    "e1EDKT2"
})
public class ORDERS02E1EDKT1 {

    @XmlElement(name = "TDID")
    protected String tdid;
    @XmlElement(name = "TSSPRAS")
    protected String tsspras;
    @XmlElement(name = "TSSPRAS_ISO")
    protected String tssprasiso;
    @XmlElement(name = "TDOBJECT")
    protected String tdobject;
    @XmlElement(name = "TDOBNAME")
    protected String tdobname;
    @XmlElement(name = "E1EDKT2")
    protected List<ORDERS02E1EDKT2> e1EDKT2;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

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
     * Gets the value of the tsspras property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTSSPRAS() {
        return tsspras;
    }

    /**
     * Sets the value of the tsspras property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTSSPRAS(String value) {
        this.tsspras = value;
    }

    /**
     * Gets the value of the tssprasiso property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTSSPRASISO() {
        return tssprasiso;
    }

    /**
     * Sets the value of the tssprasiso property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTSSPRASISO(String value) {
        this.tssprasiso = value;
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
     * Gets the value of the e1EDKT2 property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the e1EDKT2 property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getE1EDKT2().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link ORDERS02E1EDKT2 }
     * 
     * 
     */
    public List<ORDERS02E1EDKT2> getE1EDKT2() {
        if (e1EDKT2 == null) {
            e1EDKT2 = new ArrayList<ORDERS02E1EDKT2>();
        }
        return this.e1EDKT2;
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
