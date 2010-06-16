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
 * Handling Unit Header Descriptions
 * 
 * <p>Java class for ORDERS02.E1EDL38 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1EDL38">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="VEGR1_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VEGR2_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VEGR3_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VEGR4_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VEGR5_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VHART_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MAGRV_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VEBEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
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
@XmlType(name = "ORDERS02.E1EDL38", propOrder = {
    "vegr1BEZ",
    "vegr2BEZ",
    "vegr3BEZ",
    "vegr4BEZ",
    "vegr5BEZ",
    "vhartbez",
    "magrvbez",
    "vebez"
})
public class ORDERS02E1EDL38 {

    @XmlElement(name = "VEGR1_BEZ")
    protected String vegr1BEZ;
    @XmlElement(name = "VEGR2_BEZ")
    protected String vegr2BEZ;
    @XmlElement(name = "VEGR3_BEZ")
    protected String vegr3BEZ;
    @XmlElement(name = "VEGR4_BEZ")
    protected String vegr4BEZ;
    @XmlElement(name = "VEGR5_BEZ")
    protected String vegr5BEZ;
    @XmlElement(name = "VHART_BEZ")
    protected String vhartbez;
    @XmlElement(name = "MAGRV_BEZ")
    protected String magrvbez;
    @XmlElement(name = "VEBEZ")
    protected String vebez;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the vegr1BEZ property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVEGR1BEZ() {
        return vegr1BEZ;
    }

    /**
     * Sets the value of the vegr1BEZ property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVEGR1BEZ(String value) {
        this.vegr1BEZ = value;
    }

    /**
     * Gets the value of the vegr2BEZ property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVEGR2BEZ() {
        return vegr2BEZ;
    }

    /**
     * Sets the value of the vegr2BEZ property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVEGR2BEZ(String value) {
        this.vegr2BEZ = value;
    }

    /**
     * Gets the value of the vegr3BEZ property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVEGR3BEZ() {
        return vegr3BEZ;
    }

    /**
     * Sets the value of the vegr3BEZ property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVEGR3BEZ(String value) {
        this.vegr3BEZ = value;
    }

    /**
     * Gets the value of the vegr4BEZ property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVEGR4BEZ() {
        return vegr4BEZ;
    }

    /**
     * Sets the value of the vegr4BEZ property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVEGR4BEZ(String value) {
        this.vegr4BEZ = value;
    }

    /**
     * Gets the value of the vegr5BEZ property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVEGR5BEZ() {
        return vegr5BEZ;
    }

    /**
     * Sets the value of the vegr5BEZ property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVEGR5BEZ(String value) {
        this.vegr5BEZ = value;
    }

    /**
     * Gets the value of the vhartbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVHARTBEZ() {
        return vhartbez;
    }

    /**
     * Sets the value of the vhartbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVHARTBEZ(String value) {
        this.vhartbez = value;
    }

    /**
     * Gets the value of the magrvbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMAGRVBEZ() {
        return magrvbez;
    }

    /**
     * Sets the value of the magrvbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMAGRVBEZ(String value) {
        this.magrvbez = value;
    }

    /**
     * Gets the value of the vebez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVEBEZ() {
        return vebez;
    }

    /**
     * Sets the value of the vebez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVEBEZ(String value) {
        this.vebez = value;
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
