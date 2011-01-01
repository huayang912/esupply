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
 * SS item: Taxes
 * 
 * <p>Java class for ORDERS02.E1EDC04 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1EDC04">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
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
 *         &lt;element name="MWSBT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="TXJCD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
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
@XmlType(name = "ORDERS02.E1EDC04", propOrder = {
    "mwskz",
    "msatz",
    "mwsbt",
    "txjcd"
})
public class ORDERS02E1EDC04 {

    @XmlElement(name = "MWSKZ")
    protected String mwskz;
    @XmlElement(name = "MSATZ")
    protected String msatz;
    @XmlElement(name = "MWSBT")
    protected String mwsbt;
    @XmlElement(name = "TXJCD")
    protected String txjcd;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

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
     * Gets the value of the mwsbt property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMWSBT() {
        return mwsbt;
    }

    /**
     * Sets the value of the mwsbt property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMWSBT(String value) {
        this.mwsbt = value;
    }

    /**
     * Gets the value of the txjcd property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getTXJCD() {
        return txjcd;
    }

    /**
     * Sets the value of the txjcd property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setTXJCD(String value) {
        this.txjcd = value;
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
