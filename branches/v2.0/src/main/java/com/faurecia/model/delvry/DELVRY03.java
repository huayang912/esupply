//
// This file was generated by the JavaTM Architecture for XML Binding(JAXB) Reference Implementation, vJAXB 2.1.10 in JDK 6 
// See <a href="http://java.sun.com/xml/jaxb">http://java.sun.com/xml/jaxb</a> 
// Any modifications to this file will be lost upon recompilation of the source schema. 
// Generated on: 2010.05.02 at 10:27:02 ���� CST 
//


package com.faurecia.model.delvry;

import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlRootElement;
import javax.xml.bind.annotation.XmlType;


/**
 * <p>Java class for anonymous complex type.
 * 
 * <p>The following schema fragment specifies the expected content contained within this class.
 * 
 * <pre>
 * &lt;complexType>
 *   &lt;complexContent>
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *       &lt;sequence>
 *         &lt;element name="IDOC" type="{}DESADV.DELVRY03"/>
 *       &lt;/sequence>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "idoc"
})
@XmlRootElement(name = "DELVRY03")
public class DELVRY03 {

    @XmlElement(name = "IDOC", required = true)
    protected DESADVDELVRY03 idoc;

    /**
     * Gets the value of the idoc property.
     * 
     * @return
     *     possible object is
     *     {@link DESADVDELVRY03 }
     *     
     */
    public DESADVDELVRY03 getIDOC() {
        return idoc;
    }

    /**
     * Sets the value of the idoc property.
     * 
     * @param value
     *     allowed object is
     *     {@link DESADVDELVRY03 }
     *     
     */
    public void setIDOC(DESADVDELVRY03 value) {
        this.idoc = value;
    }

}
