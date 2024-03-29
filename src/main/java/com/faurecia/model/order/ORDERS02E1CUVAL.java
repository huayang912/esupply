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
 * CU: Characteristic valuation
 * 
 * <p>Java class for ORDERS02.E1CUVAL complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1CUVAL">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="INST_ID" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CHARC" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CHARC_TXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="70"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VALUE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VALUE_TXT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="70"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="AUTHOR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VALUE_TO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VALCODE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
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
@XmlType(name = "ORDERS02.E1CUVAL", propOrder = {
    "instid",
    "charc",
    "charctxt",
    "value",
    "valuetxt",
    "author",
    "valueto",
    "valcode"
})
public class ORDERS02E1CUVAL {

    @XmlElement(name = "INST_ID")
    protected String instid;
    @XmlElement(name = "CHARC")
    protected String charc;
    @XmlElement(name = "CHARC_TXT")
    protected String charctxt;
    @XmlElement(name = "VALUE")
    protected String value;
    @XmlElement(name = "VALUE_TXT")
    protected String valuetxt;
    @XmlElement(name = "AUTHOR")
    protected String author;
    @XmlElement(name = "VALUE_TO")
    protected String valueto;
    @XmlElement(name = "VALCODE")
    protected String valcode;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the instid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getINSTID() {
        return instid;
    }

    /**
     * Sets the value of the instid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setINSTID(String value) {
        this.instid = value;
    }

    /**
     * Gets the value of the charc property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCHARC() {
        return charc;
    }

    /**
     * Sets the value of the charc property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCHARC(String value) {
        this.charc = value;
    }

    /**
     * Gets the value of the charctxt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCHARCTXT() {
        return charctxt;
    }

    /**
     * Sets the value of the charctxt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCHARCTXT(String value) {
        this.charctxt = value;
    }

    /**
     * Gets the value of the value property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVALUE() {
        return value;
    }

    /**
     * Sets the value of the value property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVALUE(String value) {
        this.value = value;
    }

    /**
     * Gets the value of the valuetxt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVALUETXT() {
        return valuetxt;
    }

    /**
     * Sets the value of the valuetxt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVALUETXT(String value) {
        this.valuetxt = value;
    }

    /**
     * Gets the value of the author property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAUTHOR() {
        return author;
    }

    /**
     * Sets the value of the author property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAUTHOR(String value) {
        this.author = value;
    }

    /**
     * Gets the value of the valueto property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVALUETO() {
        return valueto;
    }

    /**
     * Sets the value of the valueto property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVALUETO(String value) {
        this.valueto = value;
    }

    /**
     * Gets the value of the valcode property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVALCODE() {
        return valcode;
    }

    /**
     * Sets the value of the valcode property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVALCODE(String value) {
        this.valcode = value;
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
