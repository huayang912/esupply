//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.02 at 10:27:02 ���� CST 
//


package com.faurecia.model.delvry;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlAttribute;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlType;


/**
 * Delivery Item Additional Data
 * 
 * <p>Java class for DELVRY03.E1EDL26 complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType name="DELVRY03.E1EDL26">
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="PSTYV" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MATKL" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="9"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="PRODH" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="18"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UMVKZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UMVKN" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="6"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KZTLF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UEBTK" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UEBTO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="5"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="UNTTO" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="5"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CHSPL" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="XCHBW" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="POSAR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="SOBKZ" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="PCKPF" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MAGRV" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="4"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="SHKZG" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KOQUI" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="AKTNR" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="10"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KZUMW" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="1"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KVGR1" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KVGR2" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KVGR3" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KVGR4" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="KVGR5" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MVGR1" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MVGR2" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MVGR3" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MVGR4" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="MVGR5" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="LIFEXPOS2" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="35"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CQU_SA" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CQU_SA_UNIT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CQU_ITQS" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="17"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="CQU_ITQS_UNIT" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="3"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="VBUMG_BME" minOccurs="0">
 *           &lt;simpleType>
 *             &lt;restriction base="{http://www.w3.org/2001/XMLSchema}string">
 *               &lt;maxLength value="15"/>
 *             &lt;/restriction>
 *           &lt;/simpleType>
 *         &lt;/element>
 *         &lt;element name="E1EDL27" type="{}DELVRY03.E1EDL27" minOccurs="0"/>
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
@XmlType(name = "DELVRY03.E1EDL26", propOrder = {
    "pstyv",
    "matkl",
    "prodh",
    "umvkz",
    "umvkn",
    "kztlf",
    "uebtk",
    "uebto",
    "untto",
    "chspl",
    "xchbw",
    "posar",
    "sobkz",
    "pckpf",
    "magrv",
    "shkzg",
    "koqui",
    "aktnr",
    "kzumw",
    "kvgr1",
    "kvgr2",
    "kvgr3",
    "kvgr4",
    "kvgr5",
    "mvgr1",
    "mvgr2",
    "mvgr3",
    "mvgr4",
    "mvgr5",
    "lifexpos2",
    "cqusa",
    "cqusaunit",
    "cquitqs",
    "cquitqsunit",
    "vbumgbme",
    "e1EDL27"
})
public class DELVRY03E1EDL26 {

    @XmlElement(name = "PSTYV")
    protected String pstyv;
    @XmlElement(name = "MATKL")
    protected String matkl;
    @XmlElement(name = "PRODH")
    protected String prodh;
    @XmlElement(name = "UMVKZ")
    protected String umvkz;
    @XmlElement(name = "UMVKN")
    protected String umvkn;
    @XmlElement(name = "KZTLF")
    protected String kztlf;
    @XmlElement(name = "UEBTK")
    protected String uebtk;
    @XmlElement(name = "UEBTO")
    protected String uebto;
    @XmlElement(name = "UNTTO")
    protected String untto;
    @XmlElement(name = "CHSPL")
    protected String chspl;
    @XmlElement(name = "XCHBW")
    protected String xchbw;
    @XmlElement(name = "POSAR")
    protected String posar;
    @XmlElement(name = "SOBKZ")
    protected String sobkz;
    @XmlElement(name = "PCKPF")
    protected String pckpf;
    @XmlElement(name = "MAGRV")
    protected String magrv;
    @XmlElement(name = "SHKZG")
    protected String shkzg;
    @XmlElement(name = "KOQUI")
    protected String koqui;
    @XmlElement(name = "AKTNR")
    protected String aktnr;
    @XmlElement(name = "KZUMW")
    protected String kzumw;
    @XmlElement(name = "KVGR1")
    protected String kvgr1;
    @XmlElement(name = "KVGR2")
    protected String kvgr2;
    @XmlElement(name = "KVGR3")
    protected String kvgr3;
    @XmlElement(name = "KVGR4")
    protected String kvgr4;
    @XmlElement(name = "KVGR5")
    protected String kvgr5;
    @XmlElement(name = "MVGR1")
    protected String mvgr1;
    @XmlElement(name = "MVGR2")
    protected String mvgr2;
    @XmlElement(name = "MVGR3")
    protected String mvgr3;
    @XmlElement(name = "MVGR4")
    protected String mvgr4;
    @XmlElement(name = "MVGR5")
    protected String mvgr5;
    @XmlElement(name = "LIFEXPOS2")
    protected String lifexpos2;
    @XmlElement(name = "CQU_SA")
    protected String cqusa;
    @XmlElement(name = "CQU_SA_UNIT")
    protected String cqusaunit;
    @XmlElement(name = "CQU_ITQS")
    protected String cquitqs;
    @XmlElement(name = "CQU_ITQS_UNIT")
    protected String cquitqsunit;
    @XmlElement(name = "VBUMG_BME")
    protected String vbumgbme;
    @XmlElement(name = "E1EDL27")
    protected DELVRY03E1EDL27 e1EDL27;
    @XmlAttribute(name = "SEGMENT", required = true)
    protected String segment;

    /**
     * Gets the value of the pstyv property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPSTYV() {
        return pstyv;
    }

    /**
     * Sets the value of the pstyv property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPSTYV(String value) {
        this.pstyv = value;
    }

    /**
     * Gets the value of the matkl property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMATKL() {
        return matkl;
    }

    /**
     * Sets the value of the matkl property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMATKL(String value) {
        this.matkl = value;
    }

    /**
     * Gets the value of the prodh property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPRODH() {
        return prodh;
    }

    /**
     * Sets the value of the prodh property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPRODH(String value) {
        this.prodh = value;
    }

    /**
     * Gets the value of the umvkz property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUMVKZ() {
        return umvkz;
    }

    /**
     * Sets the value of the umvkz property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUMVKZ(String value) {
        this.umvkz = value;
    }

    /**
     * Gets the value of the umvkn property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUMVKN() {
        return umvkn;
    }

    /**
     * Sets the value of the umvkn property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUMVKN(String value) {
        this.umvkn = value;
    }

    /**
     * Gets the value of the kztlf property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKZTLF() {
        return kztlf;
    }

    /**
     * Sets the value of the kztlf property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKZTLF(String value) {
        this.kztlf = value;
    }

    /**
     * Gets the value of the uebtk property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUEBTK() {
        return uebtk;
    }

    /**
     * Sets the value of the uebtk property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUEBTK(String value) {
        this.uebtk = value;
    }

    /**
     * Gets the value of the uebto property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUEBTO() {
        return uebto;
    }

    /**
     * Sets the value of the uebto property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUEBTO(String value) {
        this.uebto = value;
    }

    /**
     * Gets the value of the untto property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getUNTTO() {
        return untto;
    }

    /**
     * Sets the value of the untto property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setUNTTO(String value) {
        this.untto = value;
    }

    /**
     * Gets the value of the chspl property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCHSPL() {
        return chspl;
    }

    /**
     * Sets the value of the chspl property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCHSPL(String value) {
        this.chspl = value;
    }

    /**
     * Gets the value of the xchbw property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getXCHBW() {
        return xchbw;
    }

    /**
     * Sets the value of the xchbw property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setXCHBW(String value) {
        this.xchbw = value;
    }

    /**
     * Gets the value of the posar property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPOSAR() {
        return posar;
    }

    /**
     * Sets the value of the posar property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPOSAR(String value) {
        this.posar = value;
    }

    /**
     * Gets the value of the sobkz property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSOBKZ() {
        return sobkz;
    }

    /**
     * Sets the value of the sobkz property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSOBKZ(String value) {
        this.sobkz = value;
    }

    /**
     * Gets the value of the pckpf property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getPCKPF() {
        return pckpf;
    }

    /**
     * Sets the value of the pckpf property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setPCKPF(String value) {
        this.pckpf = value;
    }

    /**
     * Gets the value of the magrv property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMAGRV() {
        return magrv;
    }

    /**
     * Sets the value of the magrv property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMAGRV(String value) {
        this.magrv = value;
    }

    /**
     * Gets the value of the shkzg property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getSHKZG() {
        return shkzg;
    }

    /**
     * Sets the value of the shkzg property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setSHKZG(String value) {
        this.shkzg = value;
    }

    /**
     * Gets the value of the koqui property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKOQUI() {
        return koqui;
    }

    /**
     * Sets the value of the koqui property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKOQUI(String value) {
        this.koqui = value;
    }

    /**
     * Gets the value of the aktnr property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getAKTNR() {
        return aktnr;
    }

    /**
     * Sets the value of the aktnr property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setAKTNR(String value) {
        this.aktnr = value;
    }

    /**
     * Gets the value of the kzumw property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKZUMW() {
        return kzumw;
    }

    /**
     * Sets the value of the kzumw property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKZUMW(String value) {
        this.kzumw = value;
    }

    /**
     * Gets the value of the kvgr1 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKVGR1() {
        return kvgr1;
    }

    /**
     * Sets the value of the kvgr1 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKVGR1(String value) {
        this.kvgr1 = value;
    }

    /**
     * Gets the value of the kvgr2 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKVGR2() {
        return kvgr2;
    }

    /**
     * Sets the value of the kvgr2 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKVGR2(String value) {
        this.kvgr2 = value;
    }

    /**
     * Gets the value of the kvgr3 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKVGR3() {
        return kvgr3;
    }

    /**
     * Sets the value of the kvgr3 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKVGR3(String value) {
        this.kvgr3 = value;
    }

    /**
     * Gets the value of the kvgr4 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKVGR4() {
        return kvgr4;
    }

    /**
     * Sets the value of the kvgr4 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKVGR4(String value) {
        this.kvgr4 = value;
    }

    /**
     * Gets the value of the kvgr5 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getKVGR5() {
        return kvgr5;
    }

    /**
     * Sets the value of the kvgr5 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setKVGR5(String value) {
        this.kvgr5 = value;
    }

    /**
     * Gets the value of the mvgr1 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMVGR1() {
        return mvgr1;
    }

    /**
     * Sets the value of the mvgr1 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMVGR1(String value) {
        this.mvgr1 = value;
    }

    /**
     * Gets the value of the mvgr2 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMVGR2() {
        return mvgr2;
    }

    /**
     * Sets the value of the mvgr2 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMVGR2(String value) {
        this.mvgr2 = value;
    }

    /**
     * Gets the value of the mvgr3 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMVGR3() {
        return mvgr3;
    }

    /**
     * Sets the value of the mvgr3 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMVGR3(String value) {
        this.mvgr3 = value;
    }

    /**
     * Gets the value of the mvgr4 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMVGR4() {
        return mvgr4;
    }

    /**
     * Sets the value of the mvgr4 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMVGR4(String value) {
        this.mvgr4 = value;
    }

    /**
     * Gets the value of the mvgr5 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getMVGR5() {
        return mvgr5;
    }

    /**
     * Sets the value of the mvgr5 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setMVGR5(String value) {
        this.mvgr5 = value;
    }

    /**
     * Gets the value of the lifexpos2 property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getLIFEXPOS2() {
        return lifexpos2;
    }

    /**
     * Sets the value of the lifexpos2 property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setLIFEXPOS2(String value) {
        this.lifexpos2 = value;
    }

    /**
     * Gets the value of the cqusa property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCQUSA() {
        return cqusa;
    }

    /**
     * Sets the value of the cqusa property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCQUSA(String value) {
        this.cqusa = value;
    }

    /**
     * Gets the value of the cqusaunit property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCQUSAUNIT() {
        return cqusaunit;
    }

    /**
     * Sets the value of the cqusaunit property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCQUSAUNIT(String value) {
        this.cqusaunit = value;
    }

    /**
     * Gets the value of the cquitqs property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCQUITQS() {
        return cquitqs;
    }

    /**
     * Sets the value of the cquitqs property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCQUITQS(String value) {
        this.cquitqs = value;
    }

    /**
     * Gets the value of the cquitqsunit property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getCQUITQSUNIT() {
        return cquitqsunit;
    }

    /**
     * Sets the value of the cquitqsunit property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setCQUITQSUNIT(String value) {
        this.cquitqsunit = value;
    }

    /**
     * Gets the value of the vbumgbme property.
     * 
     * @return
     *     possible object is
     *     {@link String }
     *     
     */
    public String getVBUMGBME() {
        return vbumgbme;
    }

    /**
     * Sets the value of the vbumgbme property.
     * 
     * @param value
     *     allowed object is
     *     {@link String }
     *     
     */
    public void setVBUMGBME(String value) {
        this.vbumgbme = value;
    }

    /**
     * Gets the value of the e1EDL27 property.
     * 
     * @return
     *     possible object is
     *     {@link DELVRY03E1EDL27 }
     *     
     */
    public DELVRY03E1EDL27 getE1EDL27() {
        return e1EDL27;
    }

    /**
     * Sets the value of the e1EDL27 property.
     * 
     * @param value
     *     allowed object is
     *     {@link DELVRY03E1EDL27 }
     *     
     */
    public void setE1EDL27(DELVRY03E1EDL27 value) {
        this.e1EDL27 = value;
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
