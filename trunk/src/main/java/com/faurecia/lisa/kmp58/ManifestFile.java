
package com.faurecia.lisa.kmp58;

import java.util.ArrayList;
import java.util.List;
import javax.xml.bind.annotation.XmlAccessType;
import javax.xml.bind.annotation.XmlAccessorType;
import javax.xml.bind.annotation.XmlElement;
import javax.xml.bind.annotation.XmlElements;
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
 *       &lt;choice maxOccurs="unbounded" minOccurs="0">
 *         &lt;element name="fileHeader">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element name="START" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                   &lt;element name="PCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                   &lt;element name="PDESC" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                   &lt;element name="FILID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *         &lt;element name="delivery">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element name="recheader" maxOccurs="unbounded" minOccurs="0">
 *                     &lt;complexType>
 *                       &lt;complexContent>
 *                         &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                           &lt;sequence>
 *                             &lt;element name="TYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANPART" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MURN" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PGCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="RECEPT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="CUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="CUCODNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="CULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUCODNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANTITLE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ARRIVAL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PICKUP" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUCONTACT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUFAX" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUPLANT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="POSTCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="CITY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUCONTACT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUFAX" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ROUTE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MROUTE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="LOGPNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ORDERG" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUADDR1" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUADDR2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUADDR3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUCTRY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUTEL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPADDR1" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPADDR2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPADDR3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPPCOD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPCITY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPCTRY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUPTEL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="INVNBR" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="TOTWEIGHT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="UNITWEIGHT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="TOTVOL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="UNITVOL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="TOTPAL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ARRIVAL1CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="DEPART1CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ARRIVAL2CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="DEPART2CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="DELORDGR" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="BLANK" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                           &lt;/sequence>
 *                         &lt;/restriction>
 *                       &lt;/complexContent>
 *                     &lt;/complexType>
 *                   &lt;/element>
 *                   &lt;element name="recpos" maxOccurs="unbounded" minOccurs="0">
 *                     &lt;complexType>
 *                       &lt;complexContent>
 *                         &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                           &lt;sequence>
 *                             &lt;element name="TYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANPART" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MURN" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="ERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PGCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FAUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="FDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="MANIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="RECEPT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="CUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="CULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PNUMBER" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SEBANGO" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PNUMIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="DESC" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="OLOT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PCS_PU" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="NB_PU" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="LABELID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="PACKTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                             &lt;element name="SCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                           &lt;/sequence>
 *                         &lt;/restriction>
 *                       &lt;/complexContent>
 *                     &lt;/complexType>
 *                   &lt;/element>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *         &lt;element name="fileEnd">
 *           &lt;complexType>
 *             &lt;complexContent>
 *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
 *                 &lt;sequence>
 *                   &lt;element name="c_end" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
 *                 &lt;/sequence>
 *               &lt;/restriction>
 *             &lt;/complexContent>
 *           &lt;/complexType>
 *         &lt;/element>
 *       &lt;/choice>
 *     &lt;/restriction>
 *   &lt;/complexContent>
 * &lt;/complexType>
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "", propOrder = {
    "fileHeaderOrDeliveryOrFileEnd"
})
@XmlRootElement(name = "ManifestFile")
public class ManifestFile {

    @XmlElements({
        @XmlElement(name = "delivery", type = ManifestFile.Delivery.class),
        @XmlElement(name = "fileEnd", type = ManifestFile.FileEnd.class),
        @XmlElement(name = "fileHeader", type = ManifestFile.FileHeader.class)
    })
    protected List<Object> fileHeaderOrDeliveryOrFileEnd;

    /**
     * Gets the value of the fileHeaderOrDeliveryOrFileEnd property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the JAXB object.
     * This is why there is not a <CODE>set</CODE> method for the fileHeaderOrDeliveryOrFileEnd property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getFileHeaderOrDeliveryOrFileEnd().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link ManifestFile.Delivery }
     * {@link ManifestFile.FileEnd }
     * {@link ManifestFile.FileHeader }
     * 
     * 
     */
    public List<Object> getFileHeaderOrDeliveryOrFileEnd() {
        if (fileHeaderOrDeliveryOrFileEnd == null) {
            fileHeaderOrDeliveryOrFileEnd = new ArrayList<Object>();
        }
        return this.fileHeaderOrDeliveryOrFileEnd;
    }


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
     *         &lt;element name="recheader" maxOccurs="unbounded" minOccurs="0">
     *           &lt;complexType>
     *             &lt;complexContent>
     *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *                 &lt;sequence>
     *                   &lt;element name="TYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANPART" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MURN" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PGCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="RECEPT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="CUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="CUCODNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="CULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUCODNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANTITLE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ARRIVAL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PICKUP" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUCONTACT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUFAX" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUPLANT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="POSTCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="CITY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUCONTACT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUFAX" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ROUTE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MROUTE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="LOGPNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ORDERG" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUADDR1" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUADDR2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUADDR3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUCTRY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUTEL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPADDR1" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPADDR2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPADDR3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPPCOD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPCITY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPCTRY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUPTEL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="INVNBR" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="TOTWEIGHT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="UNITWEIGHT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="TOTVOL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="UNITVOL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="TOTPAL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ARRIVAL1CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="DEPART1CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ARRIVAL2CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="DEPART2CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="DELORDGR" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="BLANK" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                 &lt;/sequence>
     *               &lt;/restriction>
     *             &lt;/complexContent>
     *           &lt;/complexType>
     *         &lt;/element>
     *         &lt;element name="recpos" maxOccurs="unbounded" minOccurs="0">
     *           &lt;complexType>
     *             &lt;complexContent>
     *               &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType">
     *                 &lt;sequence>
     *                   &lt;element name="TYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANPART" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MURN" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="ERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PGCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FAUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="FDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="MANIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="RECEPT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="CUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="CULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PNUMBER" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SEBANGO" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PNUMIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="DESC" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="OLOT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PCS_PU" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="NB_PU" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="LABELID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="PACKTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                   &lt;element name="SCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *                 &lt;/sequence>
     *               &lt;/restriction>
     *             &lt;/complexContent>
     *           &lt;/complexType>
     *         &lt;/element>
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
        "recheader",
        "recpos"
    })
    public static class Delivery {

        protected List<ManifestFile.Delivery.Recheader> recheader;
        protected List<ManifestFile.Delivery.Recpos> recpos;

        /**
         * Gets the value of the recheader property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the recheader property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getRecheader().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link ManifestFile.Delivery.Recheader }
         * 
         * 
         */
        public List<ManifestFile.Delivery.Recheader> getRecheader() {
            if (recheader == null) {
                recheader = new ArrayList<ManifestFile.Delivery.Recheader>();
            }
            return this.recheader;
        }

        /**
         * Gets the value of the recpos property.
         * 
         * <p>
         * This accessor method returns a reference to the live list,
         * not a snapshot. Therefore any modification you make to the
         * returned list will be present inside the JAXB object.
         * This is why there is not a <CODE>set</CODE> method for the recpos property.
         * 
         * <p>
         * For example, to add a new item, do as follows:
         * <pre>
         *    getRecpos().add(newItem);
         * </pre>
         * 
         * 
         * <p>
         * Objects of the following type(s) are allowed in the list
         * {@link ManifestFile.Delivery.Recpos }
         * 
         * 
         */
        public List<ManifestFile.Delivery.Recpos> getRecpos() {
            if (recpos == null) {
                recpos = new ArrayList<ManifestFile.Delivery.Recpos>();
            }
            return this.recpos;
        }


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
         *         &lt;element name="TYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANPART" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MURN" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PGCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="RECEPT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="CUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="CUCODNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="CULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUCODNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANTITLE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ARRIVAL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PICKUP" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUCONTACT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUFAX" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUPLANT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="POSTCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="CITY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUCONTACT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUFAX" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ROUTE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MROUTE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="LOGPNAME" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ORDERG" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUADDR1" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUADDR2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUADDR3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUCTRY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUTEL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPADDR1" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPADDR2" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPADDR3" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPPCOD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPCITY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPCTRY" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUPTEL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="INVNBR" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="TOTWEIGHT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="UNITWEIGHT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="TOTVOL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="UNITVOL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="TOTPAL" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ARRIVAL1CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="DEPART1CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ARRIVAL2CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="DEPART2CD" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="DELORDGR" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="BLANK" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
            "type",
            "manpart",
            "murn",
            "erpcode",
            "pgcode",
            "faucode",
            "mancode",
            "fdcode",
            "serpcode",
            "sdcode",
            "mantype",
            "manind",
            "recept",
            "cucode",
            "cucodname",
            "culogid",
            "sucode",
            "sucodname",
            "sulogid",
            "mantitle",
            "arrival",
            "pickup",
            "suname",
            "sucontact",
            "sufax",
            "fauplant",
            "postcode",
            "city",
            "faucontact",
            "faufax",
            "route",
            "mroute",
            "logpname",
            "orderg",
            "fauaddr1",
            "fauaddr2",
            "fauaddr3",
            "fauctry",
            "fautel",
            "supaddr1",
            "supaddr2",
            "supaddr3",
            "suppcod",
            "supcity",
            "supctry",
            "suptel",
            "invnbr",
            "totweight",
            "unitweight",
            "totvol",
            "unitvol",
            "totpal",
            "arrival1CD",
            "depart1CD",
            "arrival2CD",
            "depart2CD",
            "delordgr",
            "blank"
        })
        public static class Recheader {

            @XmlElement(name = "TYPE")
            protected String type;
            @XmlElement(name = "MANPART")
            protected String manpart;
            @XmlElement(name = "MURN")
            protected String murn;
            @XmlElement(name = "ERPCODE")
            protected String erpcode;
            @XmlElement(name = "PGCODE")
            protected String pgcode;
            @XmlElement(name = "FAUCODE")
            protected String faucode;
            @XmlElement(name = "MANCODE")
            protected String mancode;
            @XmlElement(name = "FDCODE")
            protected String fdcode;
            @XmlElement(name = "SERPCODE")
            protected String serpcode;
            @XmlElement(name = "SDCODE")
            protected String sdcode;
            @XmlElement(name = "MANTYPE")
            protected String mantype;
            @XmlElement(name = "MANIND")
            protected String manind;
            @XmlElement(name = "RECEPT")
            protected String recept;
            @XmlElement(name = "CUCODE")
            protected String cucode;
            @XmlElement(name = "CUCODNAME")
            protected String cucodname;
            @XmlElement(name = "CULOGID")
            protected String culogid;
            @XmlElement(name = "SUCODE")
            protected String sucode;
            @XmlElement(name = "SUCODNAME")
            protected String sucodname;
            @XmlElement(name = "SULOGID")
            protected String sulogid;
            @XmlElement(name = "MANTITLE")
            protected String mantitle;
            @XmlElement(name = "ARRIVAL")
            protected String arrival;
            @XmlElement(name = "PICKUP")
            protected String pickup;
            @XmlElement(name = "SUNAME")
            protected String suname;
            @XmlElement(name = "SUCONTACT")
            protected String sucontact;
            @XmlElement(name = "SUFAX")
            protected String sufax;
            @XmlElement(name = "FAUPLANT")
            protected String fauplant;
            @XmlElement(name = "POSTCODE")
            protected String postcode;
            @XmlElement(name = "CITY")
            protected String city;
            @XmlElement(name = "FAUCONTACT")
            protected String faucontact;
            @XmlElement(name = "FAUFAX")
            protected String faufax;
            @XmlElement(name = "ROUTE")
            protected String route;
            @XmlElement(name = "MROUTE")
            protected String mroute;
            @XmlElement(name = "LOGPNAME")
            protected String logpname;
            @XmlElement(name = "ORDERG")
            protected String orderg;
            @XmlElement(name = "FAUADDR1")
            protected String fauaddr1;
            @XmlElement(name = "FAUADDR2")
            protected String fauaddr2;
            @XmlElement(name = "FAUADDR3")
            protected String fauaddr3;
            @XmlElement(name = "FAUCTRY")
            protected String fauctry;
            @XmlElement(name = "FAUTEL")
            protected String fautel;
            @XmlElement(name = "SUPADDR1")
            protected String supaddr1;
            @XmlElement(name = "SUPADDR2")
            protected String supaddr2;
            @XmlElement(name = "SUPADDR3")
            protected String supaddr3;
            @XmlElement(name = "SUPPCOD")
            protected String suppcod;
            @XmlElement(name = "SUPCITY")
            protected String supcity;
            @XmlElement(name = "SUPCTRY")
            protected String supctry;
            @XmlElement(name = "SUPTEL")
            protected String suptel;
            @XmlElement(name = "INVNBR")
            protected String invnbr;
            @XmlElement(name = "TOTWEIGHT")
            protected String totweight;
            @XmlElement(name = "UNITWEIGHT")
            protected String unitweight;
            @XmlElement(name = "TOTVOL")
            protected String totvol;
            @XmlElement(name = "UNITVOL")
            protected String unitvol;
            @XmlElement(name = "TOTPAL")
            protected String totpal;
            @XmlElement(name = "ARRIVAL1CD")
            protected String arrival1CD;
            @XmlElement(name = "DEPART1CD")
            protected String depart1CD;
            @XmlElement(name = "ARRIVAL2CD")
            protected String arrival2CD;
            @XmlElement(name = "DEPART2CD")
            protected String depart2CD;
            @XmlElement(name = "DELORDGR")
            protected String delordgr;
            @XmlElement(name = "BLANK")
            protected String blank;

            /**
             * Gets the value of the type property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getTYPE() {
                return type;
            }

            /**
             * Sets the value of the type property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setTYPE(String value) {
                this.type = value;
            }

            /**
             * Gets the value of the manpart property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANPART() {
                return manpart;
            }

            /**
             * Sets the value of the manpart property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANPART(String value) {
                this.manpart = value;
            }

            /**
             * Gets the value of the murn property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMURN() {
                return murn;
            }

            /**
             * Sets the value of the murn property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMURN(String value) {
                this.murn = value;
            }

            /**
             * Gets the value of the erpcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getERPCODE() {
                return erpcode;
            }

            /**
             * Sets the value of the erpcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setERPCODE(String value) {
                this.erpcode = value;
            }

            /**
             * Gets the value of the pgcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPGCODE() {
                return pgcode;
            }

            /**
             * Sets the value of the pgcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPGCODE(String value) {
                this.pgcode = value;
            }

            /**
             * Gets the value of the faucode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUCODE() {
                return faucode;
            }

            /**
             * Sets the value of the faucode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUCODE(String value) {
                this.faucode = value;
            }

            /**
             * Gets the value of the mancode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANCODE() {
                return mancode;
            }

            /**
             * Sets the value of the mancode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANCODE(String value) {
                this.mancode = value;
            }

            /**
             * Gets the value of the fdcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFDCODE() {
                return fdcode;
            }

            /**
             * Sets the value of the fdcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFDCODE(String value) {
                this.fdcode = value;
            }

            /**
             * Gets the value of the serpcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSERPCODE() {
                return serpcode;
            }

            /**
             * Sets the value of the serpcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSERPCODE(String value) {
                this.serpcode = value;
            }

            /**
             * Gets the value of the sdcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSDCODE() {
                return sdcode;
            }

            /**
             * Sets the value of the sdcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSDCODE(String value) {
                this.sdcode = value;
            }

            /**
             * Gets the value of the mantype property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANTYPE() {
                return mantype;
            }

            /**
             * Sets the value of the mantype property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANTYPE(String value) {
                this.mantype = value;
            }

            /**
             * Gets the value of the manind property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANIND() {
                return manind;
            }

            /**
             * Sets the value of the manind property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANIND(String value) {
                this.manind = value;
            }

            /**
             * Gets the value of the recept property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getRECEPT() {
                return recept;
            }

            /**
             * Sets the value of the recept property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setRECEPT(String value) {
                this.recept = value;
            }

            /**
             * Gets the value of the cucode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getCUCODE() {
                return cucode;
            }

            /**
             * Sets the value of the cucode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setCUCODE(String value) {
                this.cucode = value;
            }

            /**
             * Gets the value of the cucodname property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getCUCODNAME() {
                return cucodname;
            }

            /**
             * Sets the value of the cucodname property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setCUCODNAME(String value) {
                this.cucodname = value;
            }

            /**
             * Gets the value of the culogid property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getCULOGID() {
                return culogid;
            }

            /**
             * Sets the value of the culogid property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setCULOGID(String value) {
                this.culogid = value;
            }

            /**
             * Gets the value of the sucode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUCODE() {
                return sucode;
            }

            /**
             * Sets the value of the sucode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUCODE(String value) {
                this.sucode = value;
            }

            /**
             * Gets the value of the sucodname property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUCODNAME() {
                return sucodname;
            }

            /**
             * Sets the value of the sucodname property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUCODNAME(String value) {
                this.sucodname = value;
            }

            /**
             * Gets the value of the sulogid property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSULOGID() {
                return sulogid;
            }

            /**
             * Sets the value of the sulogid property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSULOGID(String value) {
                this.sulogid = value;
            }

            /**
             * Gets the value of the mantitle property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANTITLE() {
                return mantitle;
            }

            /**
             * Sets the value of the mantitle property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANTITLE(String value) {
                this.mantitle = value;
            }

            /**
             * Gets the value of the arrival property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getARRIVAL() {
                return arrival;
            }

            /**
             * Sets the value of the arrival property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setARRIVAL(String value) {
                this.arrival = value;
            }

            /**
             * Gets the value of the pickup property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPICKUP() {
                return pickup;
            }

            /**
             * Sets the value of the pickup property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPICKUP(String value) {
                this.pickup = value;
            }

            /**
             * Gets the value of the suname property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUNAME() {
                return suname;
            }

            /**
             * Sets the value of the suname property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUNAME(String value) {
                this.suname = value;
            }

            /**
             * Gets the value of the sucontact property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUCONTACT() {
                return sucontact;
            }

            /**
             * Sets the value of the sucontact property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUCONTACT(String value) {
                this.sucontact = value;
            }

            /**
             * Gets the value of the sufax property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUFAX() {
                return sufax;
            }

            /**
             * Sets the value of the sufax property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUFAX(String value) {
                this.sufax = value;
            }

            /**
             * Gets the value of the fauplant property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUPLANT() {
                return fauplant;
            }

            /**
             * Sets the value of the fauplant property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUPLANT(String value) {
                this.fauplant = value;
            }

            /**
             * Gets the value of the postcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPOSTCODE() {
                return postcode;
            }

            /**
             * Sets the value of the postcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPOSTCODE(String value) {
                this.postcode = value;
            }

            /**
             * Gets the value of the city property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getCITY() {
                return city;
            }

            /**
             * Sets the value of the city property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setCITY(String value) {
                this.city = value;
            }

            /**
             * Gets the value of the faucontact property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUCONTACT() {
                return faucontact;
            }

            /**
             * Sets the value of the faucontact property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUCONTACT(String value) {
                this.faucontact = value;
            }

            /**
             * Gets the value of the faufax property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUFAX() {
                return faufax;
            }

            /**
             * Sets the value of the faufax property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUFAX(String value) {
                this.faufax = value;
            }

            /**
             * Gets the value of the route property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getROUTE() {
                return route;
            }

            /**
             * Sets the value of the route property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setROUTE(String value) {
                this.route = value;
            }

            /**
             * Gets the value of the mroute property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMROUTE() {
                return mroute;
            }

            /**
             * Sets the value of the mroute property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMROUTE(String value) {
                this.mroute = value;
            }

            /**
             * Gets the value of the logpname property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getLOGPNAME() {
                return logpname;
            }

            /**
             * Sets the value of the logpname property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setLOGPNAME(String value) {
                this.logpname = value;
            }

            /**
             * Gets the value of the orderg property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getORDERG() {
                return orderg;
            }

            /**
             * Sets the value of the orderg property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setORDERG(String value) {
                this.orderg = value;
            }

            /**
             * Gets the value of the fauaddr1 property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUADDR1() {
                return fauaddr1;
            }

            /**
             * Sets the value of the fauaddr1 property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUADDR1(String value) {
                this.fauaddr1 = value;
            }

            /**
             * Gets the value of the fauaddr2 property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUADDR2() {
                return fauaddr2;
            }

            /**
             * Sets the value of the fauaddr2 property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUADDR2(String value) {
                this.fauaddr2 = value;
            }

            /**
             * Gets the value of the fauaddr3 property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUADDR3() {
                return fauaddr3;
            }

            /**
             * Sets the value of the fauaddr3 property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUADDR3(String value) {
                this.fauaddr3 = value;
            }

            /**
             * Gets the value of the fauctry property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUCTRY() {
                return fauctry;
            }

            /**
             * Sets the value of the fauctry property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUCTRY(String value) {
                this.fauctry = value;
            }

            /**
             * Gets the value of the fautel property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUTEL() {
                return fautel;
            }

            /**
             * Sets the value of the fautel property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUTEL(String value) {
                this.fautel = value;
            }

            /**
             * Gets the value of the supaddr1 property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPADDR1() {
                return supaddr1;
            }

            /**
             * Sets the value of the supaddr1 property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPADDR1(String value) {
                this.supaddr1 = value;
            }

            /**
             * Gets the value of the supaddr2 property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPADDR2() {
                return supaddr2;
            }

            /**
             * Sets the value of the supaddr2 property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPADDR2(String value) {
                this.supaddr2 = value;
            }

            /**
             * Gets the value of the supaddr3 property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPADDR3() {
                return supaddr3;
            }

            /**
             * Sets the value of the supaddr3 property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPADDR3(String value) {
                this.supaddr3 = value;
            }

            /**
             * Gets the value of the suppcod property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPPCOD() {
                return suppcod;
            }

            /**
             * Sets the value of the suppcod property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPPCOD(String value) {
                this.suppcod = value;
            }

            /**
             * Gets the value of the supcity property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPCITY() {
                return supcity;
            }

            /**
             * Sets the value of the supcity property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPCITY(String value) {
                this.supcity = value;
            }

            /**
             * Gets the value of the supctry property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPCTRY() {
                return supctry;
            }

            /**
             * Sets the value of the supctry property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPCTRY(String value) {
                this.supctry = value;
            }

            /**
             * Gets the value of the suptel property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUPTEL() {
                return suptel;
            }

            /**
             * Sets the value of the suptel property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUPTEL(String value) {
                this.suptel = value;
            }

            /**
             * Gets the value of the invnbr property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getINVNBR() {
                return invnbr;
            }

            /**
             * Sets the value of the invnbr property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setINVNBR(String value) {
                this.invnbr = value;
            }

            /**
             * Gets the value of the totweight property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getTOTWEIGHT() {
                return totweight;
            }

            /**
             * Sets the value of the totweight property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setTOTWEIGHT(String value) {
                this.totweight = value;
            }

            /**
             * Gets the value of the unitweight property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getUNITWEIGHT() {
                return unitweight;
            }

            /**
             * Sets the value of the unitweight property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setUNITWEIGHT(String value) {
                this.unitweight = value;
            }

            /**
             * Gets the value of the totvol property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getTOTVOL() {
                return totvol;
            }

            /**
             * Sets the value of the totvol property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setTOTVOL(String value) {
                this.totvol = value;
            }

            /**
             * Gets the value of the unitvol property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getUNITVOL() {
                return unitvol;
            }

            /**
             * Sets the value of the unitvol property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setUNITVOL(String value) {
                this.unitvol = value;
            }

            /**
             * Gets the value of the totpal property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getTOTPAL() {
                return totpal;
            }

            /**
             * Sets the value of the totpal property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setTOTPAL(String value) {
                this.totpal = value;
            }

            /**
             * Gets the value of the arrival1CD property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getARRIVAL1CD() {
                return arrival1CD;
            }

            /**
             * Sets the value of the arrival1CD property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setARRIVAL1CD(String value) {
                this.arrival1CD = value;
            }

            /**
             * Gets the value of the depart1CD property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getDEPART1CD() {
                return depart1CD;
            }

            /**
             * Sets the value of the depart1CD property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setDEPART1CD(String value) {
                this.depart1CD = value;
            }

            /**
             * Gets the value of the arrival2CD property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getARRIVAL2CD() {
                return arrival2CD;
            }

            /**
             * Sets the value of the arrival2CD property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setARRIVAL2CD(String value) {
                this.arrival2CD = value;
            }

            /**
             * Gets the value of the depart2CD property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getDEPART2CD() {
                return depart2CD;
            }

            /**
             * Sets the value of the depart2CD property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setDEPART2CD(String value) {
                this.depart2CD = value;
            }

            /**
             * Gets the value of the delordgr property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getDELORDGR() {
                return delordgr;
            }

            /**
             * Sets the value of the delordgr property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setDELORDGR(String value) {
                this.delordgr = value;
            }

            /**
             * Gets the value of the blank property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getBLANK() {
                return blank;
            }

            /**
             * Sets the value of the blank property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setBLANK(String value) {
                this.blank = value;
            }

        }


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
         *         &lt;element name="TYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANPART" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MURN" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="ERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PGCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FAUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="FDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SERPCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SDCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="MANIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="RECEPT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="CUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="CULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SUCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SULOGID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PNUMBER" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SEBANGO" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PNUMIND" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="DESC" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="OLOT" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PCS_PU" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="NB_PU" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="LABELID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="PACKTYPE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
         *         &lt;element name="SCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
            "type",
            "manpart",
            "murn",
            "erpcode",
            "pgcode",
            "faucode",
            "mancode",
            "fdcode",
            "serpcode",
            "sdcode",
            "mantype",
            "manind",
            "recept",
            "cucode",
            "culogid",
            "sucode",
            "sulogid",
            "pnumber",
            "sebango",
            "pnumind",
            "desc",
            "olot",
            "pcspu",
            "nbpu",
            "labelid",
            "packtype",
            "scode"
        })
        public static class Recpos {

            @XmlElement(name = "TYPE")
            protected String type;
            @XmlElement(name = "MANPART")
            protected String manpart;
            @XmlElement(name = "MURN")
            protected String murn;
            @XmlElement(name = "ERPCODE")
            protected String erpcode;
            @XmlElement(name = "PGCODE")
            protected String pgcode;
            @XmlElement(name = "FAUCODE")
            protected String faucode;
            @XmlElement(name = "MANCODE")
            protected String mancode;
            @XmlElement(name = "FDCODE")
            protected String fdcode;
            @XmlElement(name = "SERPCODE")
            protected String serpcode;
            @XmlElement(name = "SDCODE")
            protected String sdcode;
            @XmlElement(name = "MANTYPE")
            protected String mantype;
            @XmlElement(name = "MANIND")
            protected String manind;
            @XmlElement(name = "RECEPT")
            protected String recept;
            @XmlElement(name = "CUCODE")
            protected String cucode;
            @XmlElement(name = "CULOGID")
            protected String culogid;
            @XmlElement(name = "SUCODE")
            protected String sucode;
            @XmlElement(name = "SULOGID")
            protected String sulogid;
            @XmlElement(name = "PNUMBER")
            protected String pnumber;
            @XmlElement(name = "SEBANGO")
            protected String sebango;
            @XmlElement(name = "PNUMIND")
            protected String pnumind;
            @XmlElement(name = "DESC")
            protected String desc;
            @XmlElement(name = "OLOT")
            protected String olot;
            @XmlElement(name = "PCS_PU")
            protected String pcspu;
            @XmlElement(name = "NB_PU")
            protected String nbpu;
            @XmlElement(name = "LABELID")
            protected String labelid;
            @XmlElement(name = "PACKTYPE")
            protected String packtype;
            @XmlElement(name = "SCODE")
            protected String scode;

            /**
             * Gets the value of the type property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getTYPE() {
                return type;
            }

            /**
             * Sets the value of the type property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setTYPE(String value) {
                this.type = value;
            }

            /**
             * Gets the value of the manpart property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANPART() {
                return manpart;
            }

            /**
             * Sets the value of the manpart property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANPART(String value) {
                this.manpart = value;
            }

            /**
             * Gets the value of the murn property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMURN() {
                return murn;
            }

            /**
             * Sets the value of the murn property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMURN(String value) {
                this.murn = value;
            }

            /**
             * Gets the value of the erpcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getERPCODE() {
                return erpcode;
            }

            /**
             * Sets the value of the erpcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setERPCODE(String value) {
                this.erpcode = value;
            }

            /**
             * Gets the value of the pgcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPGCODE() {
                return pgcode;
            }

            /**
             * Sets the value of the pgcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPGCODE(String value) {
                this.pgcode = value;
            }

            /**
             * Gets the value of the faucode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFAUCODE() {
                return faucode;
            }

            /**
             * Sets the value of the faucode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFAUCODE(String value) {
                this.faucode = value;
            }

            /**
             * Gets the value of the mancode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANCODE() {
                return mancode;
            }

            /**
             * Sets the value of the mancode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANCODE(String value) {
                this.mancode = value;
            }

            /**
             * Gets the value of the fdcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getFDCODE() {
                return fdcode;
            }

            /**
             * Sets the value of the fdcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setFDCODE(String value) {
                this.fdcode = value;
            }

            /**
             * Gets the value of the serpcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSERPCODE() {
                return serpcode;
            }

            /**
             * Sets the value of the serpcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSERPCODE(String value) {
                this.serpcode = value;
            }

            /**
             * Gets the value of the sdcode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSDCODE() {
                return sdcode;
            }

            /**
             * Sets the value of the sdcode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSDCODE(String value) {
                this.sdcode = value;
            }

            /**
             * Gets the value of the mantype property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANTYPE() {
                return mantype;
            }

            /**
             * Sets the value of the mantype property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANTYPE(String value) {
                this.mantype = value;
            }

            /**
             * Gets the value of the manind property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getMANIND() {
                return manind;
            }

            /**
             * Sets the value of the manind property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setMANIND(String value) {
                this.manind = value;
            }

            /**
             * Gets the value of the recept property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getRECEPT() {
                return recept;
            }

            /**
             * Sets the value of the recept property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setRECEPT(String value) {
                this.recept = value;
            }

            /**
             * Gets the value of the cucode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getCUCODE() {
                return cucode;
            }

            /**
             * Sets the value of the cucode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setCUCODE(String value) {
                this.cucode = value;
            }

            /**
             * Gets the value of the culogid property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getCULOGID() {
                return culogid;
            }

            /**
             * Sets the value of the culogid property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setCULOGID(String value) {
                this.culogid = value;
            }

            /**
             * Gets the value of the sucode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSUCODE() {
                return sucode;
            }

            /**
             * Sets the value of the sucode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSUCODE(String value) {
                this.sucode = value;
            }

            /**
             * Gets the value of the sulogid property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSULOGID() {
                return sulogid;
            }

            /**
             * Sets the value of the sulogid property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSULOGID(String value) {
                this.sulogid = value;
            }

            /**
             * Gets the value of the pnumber property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPNUMBER() {
                return pnumber;
            }

            /**
             * Sets the value of the pnumber property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPNUMBER(String value) {
                this.pnumber = value;
            }

            /**
             * Gets the value of the sebango property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSEBANGO() {
                return sebango;
            }

            /**
             * Sets the value of the sebango property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSEBANGO(String value) {
                this.sebango = value;
            }

            /**
             * Gets the value of the pnumind property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPNUMIND() {
                return pnumind;
            }

            /**
             * Sets the value of the pnumind property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPNUMIND(String value) {
                this.pnumind = value;
            }

            /**
             * Gets the value of the desc property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getDESC() {
                return desc;
            }

            /**
             * Sets the value of the desc property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setDESC(String value) {
                this.desc = value;
            }

            /**
             * Gets the value of the olot property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getOLOT() {
                return olot;
            }

            /**
             * Sets the value of the olot property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setOLOT(String value) {
                this.olot = value;
            }

            /**
             * Gets the value of the pcspu property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPCSPU() {
                return pcspu;
            }

            /**
             * Sets the value of the pcspu property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPCSPU(String value) {
                this.pcspu = value;
            }

            /**
             * Gets the value of the nbpu property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getNBPU() {
                return nbpu;
            }

            /**
             * Sets the value of the nbpu property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setNBPU(String value) {
                this.nbpu = value;
            }

            /**
             * Gets the value of the labelid property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getLABELID() {
                return labelid;
            }

            /**
             * Sets the value of the labelid property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setLABELID(String value) {
                this.labelid = value;
            }

            /**
             * Gets the value of the packtype property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getPACKTYPE() {
                return packtype;
            }

            /**
             * Sets the value of the packtype property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setPACKTYPE(String value) {
                this.packtype = value;
            }

            /**
             * Gets the value of the scode property.
             * 
             * @return
             *     possible object is
             *     {@link String }
             *     
             */
            public String getSCODE() {
                return scode;
            }

            /**
             * Sets the value of the scode property.
             * 
             * @param value
             *     allowed object is
             *     {@link String }
             *     
             */
            public void setSCODE(String value) {
                this.scode = value;
            }

        }

    }


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
     *         &lt;element name="c_end" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
        "cEnd"
    })
    public static class FileEnd {

        @XmlElement(name = "c_end")
        protected String cEnd;

        /**
         * Gets the value of the cEnd property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getCEnd() {
            return cEnd;
        }

        /**
         * Sets the value of the cEnd property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setCEnd(String value) {
            this.cEnd = value;
        }

    }


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
     *         &lt;element name="START" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         &lt;element name="PCODE" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         &lt;element name="PDESC" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
     *         &lt;element name="FILID" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/>
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
        "start",
        "pcode",
        "pdesc",
        "filid"
    })
    public static class FileHeader {

        @XmlElement(name = "START")
        protected String start;
        @XmlElement(name = "PCODE")
        protected String pcode;
        @XmlElement(name = "PDESC")
        protected String pdesc;
        @XmlElement(name = "FILID")
        protected String filid;

        /**
         * Gets the value of the start property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getSTART() {
            return start;
        }

        /**
         * Sets the value of the start property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setSTART(String value) {
            this.start = value;
        }

        /**
         * Gets the value of the pcode property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getPCODE() {
            return pcode;
        }

        /**
         * Sets the value of the pcode property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setPCODE(String value) {
            this.pcode = value;
        }

        /**
         * Gets the value of the pdesc property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getPDESC() {
            return pdesc;
        }

        /**
         * Sets the value of the pdesc property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setPDESC(String value) {
            this.pdesc = value;
        }

        /**
         * Gets the value of the filid property.
         * 
         * @return
         *     possible object is
         *     {@link String }
         *     
         */
        public String getFILID() {
            return filid;
        }

        /**
         * Sets the value of the filid property.
         * 
         * @param value
         *     allowed object is
         *     {@link String }
         *     
         */
        public void setFILID(String value) {
            this.filid = value;
        }

    }

}
