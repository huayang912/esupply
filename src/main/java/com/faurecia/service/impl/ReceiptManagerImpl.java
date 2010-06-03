package com.faurecia.service.impl;

import java.io.InputStream;

import javax.xml.bind.JAXBContext;
import javax.xml.bind.JAXBException;
import javax.xml.bind.Unmarshaller;

import com.faurecia.dao.GenericDao;
import com.faurecia.model.InboundLog;
import com.faurecia.model.Receipt;
import com.faurecia.model.mbgmcr.MBGMCR02;
import com.faurecia.service.DataConvertException;
import com.faurecia.service.ReceiptManager;

public class ReceiptManagerImpl extends GenericManagerImpl<Receipt, String> implements ReceiptManager {

	private Unmarshaller unmarshaller;
	
	public ReceiptManagerImpl(GenericDao<Receipt, String> genericDao) throws JAXBException {
		super(genericDao);
		JAXBContext jc = JAXBContext.newInstance("com.faurecia.model.mbgmcr");
		unmarshaller = jc.createUnmarshaller();
	}

	public Receipt saveSingleFile(InputStream inputStream, InboundLog inboundLog) {
		try {
			MBGMCR02 mbgmcr = unmarshalOrder(inputStream);
			Receipt receipt = MBGMCR02ToReceipt(mbgmcr);

			if (inboundLog.getPlantSupplier() == null) {
				inboundLog.setPlantSupplier(receipt.getPlantSupplier());
			}

			// 同一个定单号，新定单直接覆盖旧定单
			if (this.exists(receipt.getReceiptNo())) {
				this.remove(receipt.getReceiptNo());
			}

			// 保存采购单
			this.save(receipt);		

			inboundLog.setInboundResult("success");

			return receipt;

		} catch (JAXBException jaxbException) {
			log.error("Error occur when unmarshal ORDERS.", jaxbException);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(jaxbException.getMessage());
		} catch (DataConvertException dataConvertException) {
			log.error("Error occur when convert ORDERS to PO.", dataConvertException);
			inboundLog.setInboundResult("fail");

			Receipt receipt = (Receipt) dataConvertException.getObject();
			if (receipt != null && receipt.getPlantSupplier() != null)
			{
				inboundLog.setPlantSupplier(receipt.getPlantSupplier());
			}
			inboundLog.setMemo(dataConvertException.getMessage());
		} catch (Exception exception) {
			log.error("Error occur.", exception);
			inboundLog.setInboundResult("fail");
			inboundLog.setMemo(exception.getMessage());
		}

		return null;
	}

	private MBGMCR02 unmarshalOrder(InputStream stream) throws JAXBException {
		MBGMCR02 o = (MBGMCR02) unmarshaller.unmarshal(stream);
		return o;
	}

	private Receipt MBGMCR02ToReceipt(final MBGMCR02 mbgmcr) throws DataConvertException {
		return null;
	}
}
