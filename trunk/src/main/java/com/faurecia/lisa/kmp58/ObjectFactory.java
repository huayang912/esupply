
package com.faurecia.lisa.kmp58;

import javax.xml.bind.annotation.XmlRegistry;


/**
 * This object contains factory methods for each 
 * Java content interface and Java element interface 
 * generated in the com.faurecia.lisa.kmp58 package. 
 * <p>An ObjectFactory allows you to programatically 
 * construct new instances of the Java representation 
 * for XML content. The Java representation of XML 
 * content can consist of schema derived interfaces 
 * and classes representing the binding of schema 
 * type definitions, element declarations and model 
 * groups.  Factory methods for each of these are 
 * provided in this class.
 * 
 */
@XmlRegistry
public class ObjectFactory {


    /**
     * Create a new ObjectFactory that can be used to create new instances of schema derived classes for package: com.faurecia.lisa.kmp58
     * 
     */
    public ObjectFactory() {
    }

    /**
     * Create an instance of {@link ManifestFile.Delivery }
     * 
     */
    public ManifestFile.Delivery createManifestFileDelivery() {
        return new ManifestFile.Delivery();
    }

    /**
     * Create an instance of {@link ManifestFile.Delivery.Recheader }
     * 
     */
    public ManifestFile.Delivery.Recheader createManifestFileDeliveryRecheader() {
        return new ManifestFile.Delivery.Recheader();
    }

    /**
     * Create an instance of {@link ManifestFile.Delivery.Recpos }
     * 
     */
    public ManifestFile.Delivery.Recpos createManifestFileDeliveryRecpos() {
        return new ManifestFile.Delivery.Recpos();
    }

    /**
     * Create an instance of {@link ManifestFile }
     * 
     */
    public ManifestFile createManifestFile() {
        return new ManifestFile();
    }

    /**
     * Create an instance of {@link ManifestFile.FileEnd }
     * 
     */
    public ManifestFile.FileEnd createManifestFileFileEnd() {
        return new ManifestFile.FileEnd();
    }

    /**
     * Create an instance of {@link ManifestFile.FileHeader }
     * 
     */
    public ManifestFile.FileHeader createManifestFileFileHeader() {
        return new ManifestFile.FileHeader();
    }

}
