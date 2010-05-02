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
 * Delivery interface
 * 
 * <p>Java class for DESADV.DELVRY03 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DESADV.DELVRY03">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="EDI_DC40" type="{}EDI_DC40.DESADV.DELVRY03"/>
 *         &lt;element name="E1EDL20" type="{}DELVRY03.E1EDL20" maxOccurs="9999"/>
 *       &lt;/sequence>
 *       &lt;attribute name="BEGIN" use="required" type="{http://www.w3.org/2001/XMLSchema}string" fixed="1" />
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "DESADV.DELVRY03", propOrder = {
    "edidc40",
    "e1EDL20"
})
public class DESADVDELVRY03 {

    @XmlElement(name = "EDI_DC40", required = true)
    protected EDIDC40DESADVDELVRY03 edidc40;
    @XmlElement(name = "E1EDL20", required = true)
    protected List<DELVRY03E1EDL20> e1EDL20;
    @XmlAttribute(name = "BEGIN", required = true)
    protected String begin;

    /**
     * Gets the value of the edidc40 property.
     * 
     * @return
     *     possible object is
     *     {@link EDIDC40DESADVDELVRY03 }
     *     
     */
    public EDIDC40DESADVDELVRY03 getEDIDC40() {
        return edidc40;
    }

    /**
     * Sets the value of the edidc40 property.
     * 
     * @param value
     *     allowed object is
     *     {@link EDIDC40DESADVDELVRY03 }
     *     
     */
    public void setEDIDC40(EDIDC40DESADVDELVRY03 value) {
        this.edidc40 = value;
    }

    /**
     * Gets the value of the e1EDL20 property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the e1EDL20 property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getE1EDL20().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link DELVRY03E1EDL20 }
     * 
     * 
     */
    public List<DELVRY03E1EDL20> getE1EDL20() {
        if (e1EDL20 == null) {
            e1EDL20 = new ArrayList<DELVRY03E1EDL20>();
        }
        return this.e1EDL20;
    }

    /**
     * Gets the value of the begin property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBEGIN() {
        if (begin == null) {
            return "1";
        } else {
            return begin;
        }
    }

    /**
     * Sets the value of the begin property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBEGIN(String value) {
        this.begin = value;
    }

}
