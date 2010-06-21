package com.faurecia.util;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.math.BigDecimal;
import java.net.MalformedURLException;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Image;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.PdfContentByte;
import com.itextpdf.text.pdf.PdfWriter;

public class DeliveryOrderExportUtil {

	@SuppressWarnings("finally")
	public static InputStream export(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder) throws MalformedURLException,
			IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
				+ File.separator + backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		Image backGroupImage = Image.getInstance(backGroupImageUrl);
		backGroupImage.setAbsolutePosition(0, 0);
		backGroupImage.scaleAbsolute(600, 847);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			PdfContentByte cb = writer.getDirectContent();

			int rowPix = 580;
			BigDecimal totalBoxQty = BigDecimal.ZERO;
			for (int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);

				if (i % 22 == 0) {
					if (i > 0) {
						document.newPage();
					}
					rowPix = 580;
					exportHead(underPdfContentByte, cb, deliveryOrder, backGroupImage, dinBf, simBf);
				}
				
				BigDecimal unitCount = deliveryOrderDetail.getUnitCount();
				if (unitCount == null) {
					unitCount = new BigDecimal(1);
				}
				unitCount = unitCount.setScale(0, BigDecimal.ROUND_CEILING);
				
				BigDecimal damandQty = BigDecimal.ZERO;
				if (deliveryOrderDetail.getScheduleItemDetail() != null) {
					damandQty = deliveryOrderDetail.getScheduleItemDetail().getReleaseQty();
				} else {
					damandQty = deliveryOrderDetail.getPurchaseOrderDetail().getQty();
				}
				damandQty = damandQty.setScale(0, BigDecimal.ROUND_CEILING);
				
				BigDecimal qty = deliveryOrderDetail.getQty();
				qty = qty.setScale(0, BigDecimal.ROUND_CEILING);
				
				BigDecimal boxQty = qty.divide(unitCount, 0, BigDecimal.ROUND_CEILING);
				
				totalBoxQty = totalBoxQty.add(boxQty);

				cb.beginText();
				cb.setFontAndSize(dinBf, 14);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItem().getCode(), 89, rowPix, 0);
				cb.endText();

				cb.beginText();
				if (deliveryOrderDetail.getItemDescription().trim().length() <= 24) {
					cb.setFontAndSize(simBf, 14);
				} else {
					cb.setFontAndSize(simBf, 8);
				}
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 212, rowPix, 0);
				cb.endText();
				
				cb.beginText();
				cb.setFontAndSize(dinBf, 14);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, unitCount.toString(), 321, rowPix, 0);
				cb.endText();
			
				cb.beginText();
				cb.setFontAndSize(dinBf, 14);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, boxQty.toString(), 359, rowPix, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBf, 14);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, damandQty.toString(), 395, rowPix, 0);
				cb.endText();

				
				cb.beginText();
				cb.setFontAndSize(dinBf, 14);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, qty.toString(), 434, rowPix, 0);
				cb.endText();

				rowPix -= 18;
				if (i == 9 || i == 19) {
					rowPix -= 1;
				}
			}

			cb.beginText();
			cb.setFontAndSize(dinBf, 14);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, totalBoxQty.toString(), 359, 180, 0);
			cb.endText();
		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());
		}
	}

	private static void exportHead(PdfContentByte underPdfContentByte, PdfContentByte cb, DeliveryOrder deliveryOrder, Image backGroupImage,
			BaseFont dinBf, BaseFont simBf) throws DocumentException {
		underPdfContentByte.addImage(backGroupImage);

		cb.beginText();
		cb.setFontAndSize(dinBf, 28);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDoNo() != null ? deliveryOrder.getDoNo() : "", 465, 771, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(simBf, 12);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName() != null ? deliveryOrder.getSupplierName() : "", 265, 752, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(dinBf, 12);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode() != null ? deliveryOrder.getSupplierCode() : "", 529, 752, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(simBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER,deliveryOrder.getSupplierContactPerson() != null ? deliveryOrder.getSupplierContactPerson() : "", 140, 724, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(dinBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierPhone() != null ? deliveryOrder.getSupplierPhone() : "", 315, 724, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(simBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantContactPerson() != null ? deliveryOrder.getPlantContactPerson() : "", 457, 724, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(dinBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantPhone() != null ? deliveryOrder.getPlantPhone() : "", 550, 724, 0);
		cb.endText();
	}
}
