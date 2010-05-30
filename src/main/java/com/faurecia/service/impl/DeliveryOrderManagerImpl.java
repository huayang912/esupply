package com.faurecia.service.impl;

import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.lang.reflect.InvocationTargetException;
import java.math.BigDecimal;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Date;
import java.util.List;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Marshaller;

import org.apache.commons.beanutils.BeanUtils;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Restrictions;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;
import com.faurecia.model.Plant;
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.model.delvry.DELVRY03;
import com.faurecia.model.delvry.DELVRY03E1ADRM1;
import com.faurecia.model.delvry.DELVRY03E1EDL20;
import com.faurecia.model.delvry.DELVRY03E1EDL24;
import com.faurecia.model.delvry.DELVRY03E1EDL41;
import com.faurecia.model.delvry.DELVRY03E1EDT13;
import com.faurecia.model.delvry.DESADVDELVRY03;
import com.faurecia.model.delvry.EDIDC40DESADVDELVRY03;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.NumberControlManager;
import com.faurecia.service.PurchaseOrderManager;

import freemarker.template.utility.StringUtil;

public class DeliveryOrderManagerImpl extends GenericManagerImpl<DeliveryOrder, String> implements DeliveryOrderManager {

	private NumberControlManager numberControlManager;
	private GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager;
	private PurchaseOrderManager purchaseOrderManager;
	private Marshaller marshaller;

	public DeliveryOrderManagerImpl(GenericDao<DeliveryOrder, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.delvry");
		marshaller = jc.createMarshaller();
	}

	public void setNumberControlManager(NumberControlManager numberControlManager) {
		this.numberControlManager = numberControlManager;
	}

	public void setPurchaseOrderDetailManager(GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager) {
		this.purchaseOrderDetailManager = purchaseOrderDetailManager;
	}

	public void setPurchaseOrderManager(PurchaseOrderManager purchaseOrderManager) {
		this.purchaseOrderManager = purchaseOrderManager;
	}

	public DeliveryOrder createDeliveryOrder(List<PurchaseOrderDetail> purchaseOrderDetailList) throws IllegalAccessException,
			InvocationTargetException {

		DeliveryOrder deliveryOrder = null;
		PurchaseOrder purchaseOrder = null;
		for (int i = 0; i < purchaseOrderDetailList.size(); i++) {
			PurchaseOrderDetail purchaseOrderDetail = purchaseOrderDetailList.get(i);

			if (deliveryOrder == null) {
				purchaseOrder = purchaseOrderDetail.getPurchaseOrder();
				deliveryOrder = new DeliveryOrder();
				deliveryOrder.setDoNo(this.numberControlManager.generateNumber(purchaseOrder.getPlantSupplier().getDoNoPrefix(), 10));
				deliveryOrder.setCreateDate(new Date());
				deliveryOrder.setIsExport(false);

				BeanUtils.copyProperties(deliveryOrder, purchaseOrder);
			}

			DeliveryOrderDetail deliveryOrderDetail = new DeliveryOrderDetail();
			deliveryOrderDetail.setDeliveryOrder(deliveryOrder);

			BeanUtils.copyProperties(deliveryOrderDetail, purchaseOrderDetail);

			deliveryOrderDetail.setQty(purchaseOrderDetail.getCurrentShipQty());
			deliveryOrderDetail.setOrderQty(purchaseOrderDetail.getQty());
			deliveryOrderDetail.setReferenceOrderNo(purchaseOrderDetail.getPurchaseOrder().getPoNo());
			deliveryOrderDetail.setReferenceSequence(purchaseOrderDetail.getSequence());
			deliveryOrder.addDeliveryOrderDetail(deliveryOrderDetail);

			if (purchaseOrderDetail.getShipQty() == null) {
				purchaseOrderDetail.setShipQty(BigDecimal.ZERO);
			}

			purchaseOrderDetail.setShipQty(purchaseOrderDetail.getShipQty().add(purchaseOrderDetail.getCurrentShipQty()));

			this.purchaseOrderDetailManager.save(purchaseOrderDetail);
		}

		this.purchaseOrderManager.tryClosePurchaseOrder(purchaseOrder.getPoNo());
		this.save(deliveryOrder);

		return deliveryOrder;
	}

	public DeliveryOrder saveDeliveryOrder(DeliveryOrder deliveryOrder) throws IllegalAccessException, InvocationTargetException {

		deliveryOrder.setDoNo(this.numberControlManager.generateNumber(deliveryOrder.getPlantSupplier().getDoNoPrefix(), 10));
		deliveryOrder.setCreateDate(new Date());
		deliveryOrder.setIsExport(false);

		List<DeliveryOrderDetail> deliveryOrderDetailList = deliveryOrder.getDeliveryOrderDetailList();

		for (int i = 0; i < deliveryOrderDetailList.size(); i++) {

			DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);
			deliveryOrderDetail.setDeliveryOrder(deliveryOrder);

			deliveryOrderDetail.setQty(deliveryOrderDetail.getCurrentQty());
			deliveryOrder.addDeliveryOrderDetail(deliveryOrderDetail);

		}
		this.save(deliveryOrder);

		return deliveryOrder;
	}

	public List<DeliveryOrder> getUnexportDeliveryOrderByPlant(Plant plant) {
		DetachedCriteria criteria = DetachedCriteria.forClass(DeliveryOrder.class);
		criteria.createAlias("plantSupplier", "ps");
		
		criteria.add(Restrictions.eq("ps.plant", plant));
		criteria.add(Restrictions.eq("isExport", false));
		criteria.addOrder(Order.asc("createDate"));

		List<DeliveryOrder> deliveryOrderList = this.findByCriteria(criteria);
	
		if (deliveryOrderList != null && deliveryOrderList.size() > 0) {
			for (int i = 0; i < deliveryOrderList.size(); i++) {
				DeliveryOrder deliveryOrder = deliveryOrderList.get(i);
				
				if (deliveryOrder.getDeliveryOrderDetailList() != null
						&& deliveryOrder.getDeliveryOrderDetailList().size() > 0) {
					
				}
			}
		}
		
		return deliveryOrderList;
	}

	public DeliveryOrder get(String doNo, boolean includeDetail) {
		DeliveryOrder deliveryOrder = this.genericDao.get(doNo);
		if (includeDetail && deliveryOrder.getDeliveryOrderDetailList() != null && deliveryOrder.getDeliveryOrderDetailList().size() > 0) {
		}
		return deliveryOrder;
	}

	public File exportDeliveryOrder(DeliveryOrder deliveryOrder, File filePath, String filePrefix, String fileSuffix) throws JAXBException,
			IOException {
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
		AddE1EDL20List(E1EDL20List, deliveryOrder);

		DELVRY.setIDOC(DESADVDELVRY);

		return marshalOrder(DELVRY, filePath, filePrefix, fileSuffix);
	}

	private void AddE1EDL20List(List<DELVRY03E1EDL20> E1EDL20List, DeliveryOrder deliveryOrder) {
		DELVRY03E1EDL20 E1EDL20 = new DELVRY03E1EDL20();
		E1EDL20.setSEGMENT("1");
		E1EDL20.setVBELN(deliveryOrder.getDoNo());

		List<DELVRY03E1ADRM1> E1ADRM1List = E1EDL20.getE1ADRM1();
		addE1ADRM1List(E1ADRM1List, deliveryOrder);

		List<DELVRY03E1EDT13> E1EDT13List = E1EDL20.getE1EDT13();
		addE1EDT13List(E1EDT13List, deliveryOrder);

		List<DELVRY03E1EDL24> E1EDL24List = E1EDL20.getE1EDL24();
		addE1EDL24List(E1EDL24List, deliveryOrder.getDeliveryOrderDetailList());

		E1EDL20List.add(E1EDL20);
	}

	private void addE1ADRM1List(List<DELVRY03E1ADRM1> E1ADRM1List, DeliveryOrder deliveryOrder) {
		DELVRY03E1ADRM1 AG = new DELVRY03E1ADRM1();
		AG.setSEGMENT("1");
		AG.setPARTNERQ("AG");
		AG.setPARTNERID(deliveryOrder.getPlantSupplier().getSupplier().getCode());
		E1ADRM1List.add(AG);

		DELVRY03E1ADRM1 LF = new DELVRY03E1ADRM1();
		LF.setSEGMENT("1");
		LF.setPARTNERQ("LF");
		LF.setPARTNERID(deliveryOrder.getPlantSupplier().getSupplier().getCode());
		E1ADRM1List.add(LF);

		DELVRY03E1ADRM1 WE = new DELVRY03E1ADRM1();
		WE.setSEGMENT("1");
		WE.setPARTNERQ("WE");
		WE.setPARTNERID(deliveryOrder.getPlantSupplier().getPlant().getCode());
		E1ADRM1List.add(WE);
	}

	private void addE1EDT13List(List<DELVRY03E1EDT13> E1EDT13List, DeliveryOrder deliveryOrder) {
		DateFormat format = new SimpleDateFormat("yyyyMMdd");

		DELVRY03E1EDT13 E1EDT13 = new DELVRY03E1EDT13();
		E1EDT13.setSEGMENT("1");
		E1EDT13.setQUALF("007");
		E1EDT13.setNTANF(format.format(new Date()));
		E1EDT13.setNTEND(format.format(new Date()));
		//E1EDT13.setNTANF(format.format(deliveryOrder.getStartDate()));
		//E1EDT13.setNTEND(format.format(deliveryOrder.getEndDate()));

		E1EDT13List.add(E1EDT13);
	}

	private void addE1EDL24List(List<DELVRY03E1EDL24> E1EDL24List, List<DeliveryOrderDetail> deliveryOrderDetailList) {
		for (int i = 0; i < deliveryOrderDetailList.size(); i++) {
			DeliveryOrderDetail deliveryOrderDetail = deliveryOrderDetailList.get(i);
			E1EDL24List.add(prepareE1EDL24(i, deliveryOrderDetail));
		}
	}

	private DELVRY03E1EDL24 prepareE1EDL24(int position, DeliveryOrderDetail deliveryOrderDetail) {
		DELVRY03E1EDL24 E1EDL24 = new DELVRY03E1EDL24();

		E1EDL24.setSEGMENT("1");
		E1EDL24.setPOSNR(StringUtil.leftPad(String.valueOf(position * 10), 4, '0'));
		E1EDL24.setKDMAT(deliveryOrderDetail.getItem().getCode());
		E1EDL24.setLFIMG(String.valueOf(deliveryOrderDetail.getQty()));
		E1EDL24.setVRKME(deliveryOrderDetail.getUom());

		List<DELVRY03E1EDL41> E1EDL41List = E1EDL24.getE1EDL41();
		addE1EDL41List(E1EDL41List, deliveryOrderDetail);

		return E1EDL24;
	}

	private void addE1EDL41List(List<DELVRY03E1EDL41> E1EDL41List, DeliveryOrderDetail deliveryOrderDetail) {
		DELVRY03E1EDL41 E1EDL41 = new DELVRY03E1EDL41();

		E1EDL41.setSEGMENT("1");
		E1EDL41.setQUALI("001");
		E1EDL41.setBSTNR(deliveryOrderDetail.getReferenceOrderNo());
		E1EDL41.setPOSEX(deliveryOrderDetail.getReferenceSequence());

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
