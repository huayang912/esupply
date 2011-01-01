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
 * CU: part-of data
 * 
 * <p>Java class for ORDERS02.E1CUPRT complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1CUPRT">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="PARENT_ID" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="INST_ID" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="8"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="PART_OF_NO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="OBJ_TYPE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CLASS_TYPE" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="OBJ_KEY" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="50"/>
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
 *         &lt;element name="SALES_RELEVANT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="PART_OF_GUID" minOccurs="0">
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
@XmlType(name = "ORDERS02.E1CUPRT", propOrder = {
    "parentid",
    "instid",
    "partofno",
    "objtype",
    "classtype",
    "objkey",
    "author",
    "salesrelevant",
    "partofguid"
})
public class ORDERS02E1CUPRT {

    @XmlElement(name = "PARENT_ID")
    protected String parentid;
    @XmlElement(name = "INST_ID")
    protected String instid;
    @XmlElement(name = "PART_OF_NO")
    protected String partofno;
    @XmlElement(name = "OBJ_TYPE")
    protected String objtype;
    @XmlElement(name = "CLASS_TYPE")
    protected String classtype;
    @XmlElement(name = "OBJ_KEY")
    protected String objkey;
    @XmlElement(name = "AUTHOR")
    protected String author;
    @XmlElement(name = "SALES_RELEVANT")
    protected String salesrelevant;
    @XmlElement(name = "PART_OF_GUID")
    protected String partofguid;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the parentid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPARENTID() {
        return parentid;
    }

    /**
     * Sets the value of the parentid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPARENTID(String value) {
        this.parentid = value;
    }

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
     * Gets the value of the partofno property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPARTOFNO() {
        return partofno;
    }

    /**
     * Sets the value of the partofno property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPARTOFNO(String value) {
        this.partofno = value;
    }

    /**
     * Gets the value of the objtype property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getOBJTYPE() {
        return objtype;
    }

    /**
     * Sets the value of the objtype property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setOBJTYPE(String value) {
        this.objtype = value;
    }

    /**
     * Gets the value of the classtype property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCLASSTYPE() {
        return classtype;
    }

    /**
     * Sets the value of the classtype property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCLASSTYPE(String value) {
        this.classtype = value;
    }

    /**
     * Gets the value of the objkey property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getOBJKEY() {
        return objkey;
    }

    /**
     * Sets the value of the objkey property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setOBJKEY(String value) {
        this.objkey = value;
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
     * Gets the value of the salesrelevant property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSALESRELEVANT() {
        return salesrelevant;
    }

    /**
     * Sets the value of the salesrelevant property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSALESRELEVANT(String value) {
        this.salesrelevant = value;
    }

    /**
     * Gets the value of the partofguid property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPARTOFGUID() {
        return partofguid;
    }

    /**
     * Sets the value of the partofguid property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPARTOFGUID(String value) {
        this.partofguid = value;
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