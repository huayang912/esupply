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
 * IDoc: Document header general data
 * 
 * <p>Java class for ORDERS02.E1EDK01 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="ORDERS02.E1EDK01">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="ACTION" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KZABS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CURCY" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="HWAER" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="WKURS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="12"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ZTERM" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KUNDEUINR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EIGENUINR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BSART" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BELNR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="NTGEW" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BRGEW" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="GEWEI" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="FKART_RL" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ABLAD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="25"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="BSTZD" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VSART" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="2"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VSART_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="RECIPNT_NO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KZAZU" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="AUTLF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="AUGRU" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="AUGRU_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="40"/>
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
 *         &lt;element name="ABRVW_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="FKTYP" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LIFSK" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="2"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LIFSK_BEZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="20"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="EMPST" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="25"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="ABTNR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="DELCO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="WKURS_M" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="12"/>
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
@XmlType(name = "ORDERS02.E1EDK01", propOrder = {
    "action",
    "kzabs",
    "curcy",
    "hwaer",
    "wkurs",
    "zterm",
    "kundeuinr",
    "eigenuinr",
    "bsart",
    "belnr",
    "ntgew",
    "brgew",
    "gewei",
    "fkartrl",
    "ablad",
    "bstzd",
    "vsart",
    "vsartbez",
    "recipntno",
    "kzazu",
    "autlf",
    "augru",
    "augrubez",
    "abrvw",
    "abrvwbez",
    "fktyp",
    "lifsk",
    "lifskbez",
    "empst",
    "abtnr",
    "delco",
    "wkursm"
})
public class ORDERS02E1EDK01 {

    @XmlElement(name = "ACTION")
    protected String action;
    @XmlElement(name = "KZABS")
    protected String kzabs;
    @XmlElement(name = "CURCY")
    protected String curcy;
    @XmlElement(name = "HWAER")
    protected String hwaer;
    @XmlElement(name = "WKURS")
    protected String wkurs;
    @XmlElement(name = "ZTERM")
    protected String zterm;
    @XmlElement(name = "KUNDEUINR")
    protected String kundeuinr;
    @XmlElement(name = "EIGENUINR")
    protected String eigenuinr;
    @XmlElement(name = "BSART")
    protected String bsart;
    @XmlElement(name = "BELNR")
    protected String belnr;
    @XmlElement(name = "NTGEW")
    protected String ntgew;
    @XmlElement(name = "BRGEW")
    protected String brgew;
    @XmlElement(name = "GEWEI")
    protected String gewei;
    @XmlElement(name = "FKART_RL")
    protected String fkartrl;
    @XmlElement(name = "ABLAD")
    protected String ablad;
    @XmlElement(name = "BSTZD")
    protected String bstzd;
    @XmlElement(name = "VSART")
    protected String vsart;
    @XmlElement(name = "VSART_BEZ")
    protected String vsartbez;
    @XmlElement(name = "RECIPNT_NO")
    protected String recipntno;
    @XmlElement(name = "KZAZU")
    protected String kzazu;
    @XmlElement(name = "AUTLF")
    protected String autlf;
    @XmlElement(name = "AUGRU")
    protected String augru;
    @XmlElement(name = "AUGRU_BEZ")
    protected String augrubez;
    @XmlElement(name = "ABRVW")
    protected String abrvw;
    @XmlElement(name = "ABRVW_BEZ")
    protected String abrvwbez;
    @XmlElement(name = "FKTYP")
    protected String fktyp;
    @XmlElement(name = "LIFSK")
    protected String lifsk;
    @XmlElement(name = "LIFSK_BEZ")
    protected String lifskbez;
    @XmlElement(name = "EMPST")
    protected String empst;
    @XmlElement(name = "ABTNR")
    protected String abtnr;
    @XmlElement(name = "DELCO")
    protected String delco;
    @XmlElement(name = "WKURS_M")
    protected String wkursm;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the action property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getACTION() {
        return action;
    }

    /**
     * Sets the value of the action property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setACTION(String value) {
        this.action = value;
    }

    /**
     * Gets the value of the kzabs property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKZABS() {
        return kzabs;
    }

    /**
     * Sets the value of the kzabs property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKZABS(String value) {
        this.kzabs = value;
    }

    /**
     * Gets the value of the curcy property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCURCY() {
        return curcy;
    }

    /**
     * Sets the value of the curcy property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCURCY(String value) {
        this.curcy = value;
    }

    /**
     * Gets the value of the hwaer property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getHWAER() {
        return hwaer;
    }

    /**
     * Sets the value of the hwaer property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setHWAER(String value) {
        this.hwaer = value;
    }

    /**
     * Gets the value of the wkurs property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getWKURS() {
        return wkurs;
    }

    /**
     * Sets the value of the wkurs property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setWKURS(String value) {
        this.wkurs = value;
    }

    /**
     * Gets the value of the zterm property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getZTERM() {
        return zterm;
    }

    /**
     * Sets the value of the zterm property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setZTERM(String value) {
        this.zterm = value;
    }

    /**
     * Gets the value of the kundeuinr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKUNDEUINR() {
        return kundeuinr;
    }

    /**
     * Sets the value of the kundeuinr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKUNDEUINR(String value) {
        this.kundeuinr = value;
    }

    /**
     * Gets the value of the eigenuinr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEIGENUINR() {
        return eigenuinr;
    }

    /**
     * Sets the value of the eigenuinr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEIGENUINR(String value) {
        this.eigenuinr = value;
    }

    /**
     * Gets the value of the bsart property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBSART() {
        return bsart;
    }

    /**
     * Sets the value of the bsart property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBSART(String value) {
        this.bsart = value;
    }

    /**
     * Gets the value of the belnr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBELNR() {
        return belnr;
    }

    /**
     * Sets the value of the belnr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBELNR(String value) {
        this.belnr = value;
    }

    /**
     * Gets the value of the ntgew property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getNTGEW() {
        return ntgew;
    }

    /**
     * Sets the value of the ntgew property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setNTGEW(String value) {
        this.ntgew = value;
    }

    /**
     * Gets the value of the brgew property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getBRGEW() {
        return brgew;
    }

    /**
     * Sets the value of the brgew property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setBRGEW(String value) {
        this.brgew = value;
    }

    /**
     * Gets the value of the gewei property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getGEWEI() {
        return gewei;
    }

    /**
     * Sets the value of the gewei property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setGEWEI(String value) {
        this.gewei = value;
    }

    /**
     * Gets the value of the fkartrl property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getFKARTRL() {
        return fkartrl;
    }

    /**
     * Sets the value of the fkartrl property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setFKARTRL(String value) {
        this.fkartrl = value;
    }

    /**
     * Gets the value of the ablad property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getABLAD() {
        return ablad;
    }

    /**
     * Sets the value of the ablad property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setABLAD(String value) {
        this.ablad = value;
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
     * Gets the value of the vsart property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVSART() {
        return vsart;
    }

    /**
     * Sets the value of the vsart property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVSART(String value) {
        this.vsart = value;
    }

    /**
     * Gets the value of the vsartbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVSARTBEZ() {
        return vsartbez;
    }

    /**
     * Sets the value of the vsartbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVSARTBEZ(String value) {
        this.vsartbez = value;
    }

    /**
     * Gets the value of the recipntno property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getRECIPNTNO() {
        return recipntno;
    }

    /**
     * Sets the value of the recipntno property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setRECIPNTNO(String value) {
        this.recipntno = value;
    }

    /**
     * Gets the value of the kzazu property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKZAZU() {
        return kzazu;
    }

    /**
     * Sets the value of the kzazu property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKZAZU(String value) {
        this.kzazu = value;
    }

    /**
     * Gets the value of the autlf property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAUTLF() {
        return autlf;
    }

    /**
     * Sets the value of the autlf property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAUTLF(String value) {
        this.autlf = value;
    }

    /**
     * Gets the value of the augru property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAUGRU() {
        return augru;
    }

    /**
     * Sets the value of the augru property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAUGRU(String value) {
        this.augru = value;
    }

    /**
     * Gets the value of the augrubez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAUGRUBEZ() {
        return augrubez;
    }

    /**
     * Sets the value of the augrubez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAUGRUBEZ(String value) {
        this.augrubez = value;
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
     * Gets the value of the abrvwbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getABRVWBEZ() {
        return abrvwbez;
    }

    /**
     * Sets the value of the abrvwbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setABRVWBEZ(String value) {
        this.abrvwbez = value;
    }

    /**
     * Gets the value of the fktyp property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getFKTYP() {
        return fktyp;
    }

    /**
     * Sets the value of the fktyp property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setFKTYP(String value) {
        this.fktyp = value;
    }

    /**
     * Gets the value of the lifsk property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLIFSK() {
        return lifsk;
    }

    /**
     * Sets the value of the lifsk property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLIFSK(String value) {
        this.lifsk = value;
    }

    /**
     * Gets the value of the lifskbez property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLIFSKBEZ() {
        return lifskbez;
    }

    /**
     * Sets the value of the lifskbez property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLIFSKBEZ(String value) {
        this.lifskbez = value;
    }

    /**
     * Gets the value of the empst property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getEMPST() {
        return empst;
    }

    /**
     * Sets the value of the empst property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setEMPST(String value) {
        this.empst = value;
    }

    /**
     * Gets the value of the abtnr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getABTNR() {
        return abtnr;
    }

    /**
     * Sets the value of the abtnr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setABTNR(String value) {
        this.abtnr = value;
    }

    /**
     * Gets the value of the delco property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getDELCO() {
        return delco;
    }

    /**
     * Sets the value of the delco property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setDELCO(String value) {
        this.delco = value;
    }

    /**
     * Gets the value of the wkursm property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getWKURSM() {
        return wkursm;
    }

    /**
     * Sets the value of the wkursm property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setWKURSM(String value) {
        this.wkursm = value;
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
