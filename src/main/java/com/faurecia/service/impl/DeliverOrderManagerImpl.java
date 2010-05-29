package com.faurecia.service.impl;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.List;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;

import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.DeliverOrder;
import com.faurecia.model.DeliverOrderDetail;
import com.faurecia.model.Plant;
import com.faurecia.model.delvry.DELVRY03;
import com.faurecia.model.delvry.DELVRY03E1ADRM1;
import com.faurecia.model.delvry.DELVRY03E1EDL20;
import com.faurecia.model.delvry.DELVRY03E1EDL24;
import com.faurecia.model.delvry.DELVRY03E1EDL41;
import com.faurecia.model.delvry.DELVRY03E1EDT13;
import com.faurecia.model.delvry.DESADVDELVRY03;
import com.faurecia.model.delvry.EDIDC40DESADVDELVRY03;
import com.faurecia.service.DeliverOrderManager;

import freemarker.template.utility.StringUtil;

public class DeliverOrderManagerImpl extends GenericManagerImpl<DeliverOrder, String> implements DeliverOrderManager {

	private Marshaller marshaller;
	public DeliverOrderManagerImpl(GenericDao<DeliverOrder, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.delvry");
		marshaller = jc.createMarshaller();
	}
	
	public List<DeliverOrder> getUnexportDeliverOrderByPlant(Plant plant) {
		DetachedCriteria criteria = DetachedCriteria.forClass(DeliverOrder.class);
		
		criteria.add(Restrictions.eq("plantSupplier.plant", plant));
		criteria.add(Restrictions.eq("isExport", false));
		criteria.addOrder(Order.asc("createDate"));
		
		return this.findByCriteria(criteria);
	}

	public File exportDeliverOrder(DeliverOrder deliverOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException {
		DELVRY03 DELVRY = new DELVRY03();
		
		DESADVDELVRY03 DESADVDELVRY = new DESADVDELVRY03();
		DESADVDELVRY.setBEGIN("1");
		
		EDIDC40DESADVDELVRY03 EDIDC40 = new EDIDC40DESADVDELVRY03();
		EDIDC40.setSEGMENT("1");
		EDIDC40.setTABNAM("EDI_DC40");
		EDIDC40.setDIRECT("1");
		EDIDC40.setIDOCTYP("DELVRY03");
		EDIDC40.setMESTYP("DESADV");
		EDIDC40.setSNDPOR("aaaaaaaaaa");
		EDIDC40.setSNDPRT("aa");
		EDIDC40.setSNDPRN("aaaaaaaaaa");
		EDIDC40.setRCVPOR("aaaaaaaaaa");
		EDIDC40.setRCVPRN("aaaaaaaaaa");
		
		DESADVDELVRY.setEDIDC40(EDIDC40);
		
		List<DELVRY03E1EDL20> E1EDL20List = DESADVDELVRY.getE1EDL20();
		AddE1EDL20List(E1EDL20List, deliverOrder);
		
		DELVRY.setIDOC(DESADVDELVRY);
		
		return marshalOrder(DELVRY, filePath, filePrefix, fileSuffix);
	}
	
	private void AddE1EDL20List(List<DELVRY03E1EDL20> E1EDL20List, DeliverOrder deliverOrder) {
		DELVRY03E1EDL20 E1EDL20 = new DELVRY03E1EDL20();
		E1EDL20.setSEGMENT("1");
		E1EDL20.setVBELN(deliverOrder.getDoNo());
		
		List<DELVRY03E1ADRM1> E1ADRM1List = E1EDL20.getE1ADRM1();
		addE1ADRM1List(E1ADRM1List, deliverOrder);
		
		List<DELVRY03E1EDT13> E1EDT13List = E1EDL20.getE1EDT13();
		addE1EDT13List(E1EDT13List, deliverOrder);
		
		List<DELVRY03E1EDL24> E1EDL24List = E1EDL20.getE1EDL24();
		addE1EDL24List(E1EDL24List, deliverOrder.getDeliverOrderDetailList());
		
		E1EDL20List.add(E1EDL20);
	}
	
	private void addE1ADRM1List(List<DELVRY03E1ADRM1> E1ADRM1List, DeliverOrder deliverOrder) {
		DELVRY03E1ADRM1 AG = new DELVRY03E1ADRM1();
		AG.setSEGMENT("1");
		AG.setPARTNERQ("AG");
		AG.setPARTNERID(deliverOrder.getPlantSupplier().getSupplier().getCode());		
		E1ADRM1List.add(AG);
		
		DELVRY03E1ADRM1 LF = new DELVRY03E1ADRM1();
		LF.setSEGMENT("1");
		LF.setPARTNERQ("LF");
		LF.setPARTNERID(deliverOrder.getPlantSupplier().getSupplier().getCode());		
		E1ADRM1List.add(LF);
		
		DELVRY03E1ADRM1 WE = new DELVRY03E1ADRM1();
		WE.setSEGMENT("1");
		WE.setPARTNERQ("WE");
		WE.setPARTNERID(deliverOrder.getPlantSupplier().getPlant().getCode());		
		E1ADRM1List.add(WE);
	}
	
	private void addE1EDT13List(List<DELVRY03E1EDT13> E1EDT13List, DeliverOrder deliverOrder) {
		DateFormat format = new SimpleDateFormat("yyyyMMdd");
		
		DELVRY03E1EDT13 E1EDT13 = new DELVRY03E1EDT13();
		E1EDT13.setSEGMENT("1");
		E1EDT13.setQUALF("007");
		E1EDT13.setNTANF(format.format(deliverOrder.getStartDate()));
		E1EDT13.setNTEND(format.format(deliverOrder.getEndDate()));
		
		E1EDT13List.add(E1EDT13);
	}
	
	private void addE1EDL24List(List<DELVRY03E1EDL24> E1EDL24List, List<DeliverOrderDetail> deliverOrderDetailList) {
		for(int i = 0; i < deliverOrderDetailList.size(); i++) {
			DeliverOrderDetail deliverOrderDetail = deliverOrderDetailList.get(i);
			E1EDL24List.add(prepareE1EDL24(i, deliverOrderDetail));
		}
	}
	
	private DELVRY03E1EDL24 prepareE1EDL24(int position, DeliverOrderDetail deliverOrderDetail) {
		DELVRY03E1EDL24 E1EDL24 = new DELVRY03E1EDL24();
		
		E1EDL24.setSEGMENT("1");
		E1EDL24.setPOSNR(StringUtil.leftPad(String.valueOf(position * 10), 4, '0'));
		E1EDL24.setKDMAT(deliverOrderDetail.getItem().getCode());
		E1EDL24.setLFIMG(String.valueOf(deliverOrderDetail.getQty()));
		E1EDL24.setVRKME(deliverOrderDetail.getUom());
		
		List<DELVRY03E1EDL41> E1EDL41List = E1EDL24.getE1EDL41();
		addE1EDL41List(E1EDL41List, deliverOrderDetail);
		
		return E1EDL24;
	}
	
	private void addE1EDL41List(List<DELVRY03E1EDL41> E1EDL41List, DeliverOrderDetail deliverOrderDetail) {
		DELVRY03E1EDL41 E1EDL41 = new DELVRY03E1EDL41();
		
		E1EDL41.setSEGMENT("1");
		E1EDL41.setQUALI("001");
		E1EDL41.setBSTNR(deliverOrderDetail.getReferenceOrderNo());
		E1EDL41.setPOSEX(deliverOrderDetail.getReferenceSequence());
		
		E1EDL41List.add(E1EDL41);
	}
	
	private File marshalOrder(DELVRY03 DELVRY, File filePath, String filePrefix, String fileSuffix) throws JAXBException, IOException {
		File tempFile = File.createTempFile(filePrefix, fileSuffix, filePath);
		FileOutputStream output = new FileOutputStream(tempFile);
		marshaller.marshal(DELVRY, output);
		output.flush();
		output.close();
		return tempFile;
	}
}
