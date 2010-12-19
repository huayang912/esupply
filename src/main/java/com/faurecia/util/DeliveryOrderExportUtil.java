package com.faurecia.util;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.math.BigDecimal;
import java.net.MalformedURLException;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.util.List;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;
import com.itextpdf.text.BaseColor;
import com.itextpdf.text.Document;
import com.itextpdf.text.DocumentException;
import com.itextpdf.text.Image;
import com.itextpdf.text.PageSize;
import com.itextpdf.text.pdf.BaseFont;
import com.itextpdf.text.pdf.PdfContentByte;
import com.itextpdf.text.pdf.PdfTemplate;
import com.itextpdf.text.pdf.PdfWriter;

public class DeliveryOrderExportUtil {

	@SuppressWarnings("finally")
	public static InputStream export(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder) throws MalformedURLException,
			IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		Image backGroupImage = Image.getInstance(backGroupImageUrl);
		backGroupImage.setAbsolutePosition(0, 0);
		backGroupImage.scaleAbsolute(600, 847);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		NumberFormat numberFormat = new DecimalFormat("#.#");

		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			PdfContentByte cb = writer.getDirectContent();

			int rowPix = 535;
			BigDecimal totalBoxQty = BigDecimal.ZERO;
			for (int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);

				if (i % 22 == 0) {
					if (i > 0) {
						document.newPage();
					}
					rowPix = 535;
					exportHead(underPdfContentByte, cb, deliveryOrder, backGroupImage, dinBf, simBf);
				}

				BigDecimal unitCount = deliveryOrderDetail.getUnitCount();
				if (unitCount == null) {
					unitCount = new BigDecimal(1);
				}
				// unitCount = unitCount.setScale(0, BigDecimal.ROUND_CEILING);

				BigDecimal damandQty = BigDecimal.ZERO;
				if (deliveryOrderDetail.getScheduleItemDetail() != null) {
					damandQty = deliveryOrderDetail.getScheduleItemDetail().getReleaseQty();
				} else {
					damandQty = deliveryOrderDetail.getPurchaseOrderDetail().getQty();
				}
				// damandQty = damandQty.setScale(0, BigDecimal.ROUND_CEILING);

				BigDecimal qty = deliveryOrderDetail.getQty();
				// qty = qty.setScale(0, BigDecimal.ROUND_CEILING);

				BigDecimal boxQty = qty.divide(unitCount, 1, BigDecimal.ROUND_HALF_UP);

				totalBoxQty = totalBoxQty.add(boxQty);

				cb.beginText();
				cb.setFontAndSize(dinBf, 8);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItem().getCode(), 82, rowPix, 0);
				cb.endText();

				// cb.beginText();
				// if (deliveryOrderDetail.getItemDescription().trim().length()
				// <= 24) {
				// cb.setFontAndSize(simBf, 14);
				// } else {
				// cb.setFontAndSize(simBf, 8);
				// }
				// cb.showTextAligned(PdfContentByte.ALIGN_CENTER,
				// deliveryOrderDetail.getItemDescription(), 212, rowPix, 0);
				// cb.endText();
				PdfTemplate tp2 = cb.createTemplate(100, 50);
				tp2.beginText();
				tp2.setTextRenderingMode(PdfContentByte.TEXT_RENDER_MODE_FILL_CLIP);
				tp2.setFontAndSize(simBf, 8);
				// tp2.moveText(6, -6);
				tp2.showText(deliveryOrderDetail.getItemDescription());
				tp2.endText();
				cb.addTemplate(tp2, 122, rowPix);

				cb.beginText();
				cb.setFontAndSize(dinBf, 12);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(damandQty), 241, rowPix, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBf, 12);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(unitCount), 283, rowPix, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBf, 12);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(boxQty), 323, rowPix, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBf, 12);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(qty), 363, rowPix, 0);
				cb.endText();

				rowPix -= 17;
				if (i % 5 == 0) {
					rowPix -= 1;
				}
			}

			cb.beginText();
			cb.setFontAndSize(dinBf, 12);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(totalBoxQty), 323, 158, 0);
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
		cb.setFontAndSize(dinBf, 30);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo() != null ? deliveryOrder.getExternalDoNo() : "", 442, 762, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(simBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName() != null ? deliveryOrder.getSupplierName() : "", 206, 732, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(dinBf, 20);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode() != null ? deliveryOrder.getSupplierCode() : "", 489, 728, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(simBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierContactPerson() != null ? deliveryOrder.getSupplierContactPerson()
				: "", 120, 700, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(dinBf, 9);
		cb
				.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierPhone() != null ? deliveryOrder.getSupplierPhone() : "", 250,
						700, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(simBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantContactPerson() != null ? deliveryOrder.getPlantContactPerson() : "",
				393, 700, 0);
		cb.endText();

		cb.beginText();
		cb.setFontAndSize(dinBf, 9);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantPhone() != null ? deliveryOrder.getPlantPhone() : "", 536, 700, 0);
		cb.endText();
	}

	public static InputStream printPalletLabel(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder)
			throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		Image backGroupImage = Image.getInstance(backGroupImageUrl);
		backGroupImage.setAbsolutePosition(0, 0);
		backGroupImage.scaleAbsolute(600, 847);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\Code128.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		NumberFormat numberFormat = new DecimalFormat("#.#");

		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			PdfContentByte cb = writer.getDirectContent();

			backGroupImage.setAbsolutePosition(20, -50);
			backGroupImage.scaleAbsolute(600, 850);
			underPdfContentByte.addImage(backGroupImage);

			// supplier code
			if (deliveryOrder.getSupplierCode() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 26);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 300, 575, 0);
				cb.endText();
			}

			// supplier name
			if (deliveryOrder.getSupplierName() != null) {
				cb.beginText();
				cb.setFontAndSize(simBf, 20);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName(), 300, 535, 0);
				cb.endText();
			}

			// Route Number
			if (deliveryOrder.getRoute() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 50);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getRoute(), 230, 450, 0);
				cb.endText();
			}

			// Order Group
			if (deliveryOrder.getOrderGroup() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 50);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 460, 450, 0);
				cb.endText();
			}

			// Manifest Number
			if (deliveryOrder.getDoNo() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 30);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDoNo(), 300, 380, 0);
				cb.endText();
			}

			// Unload Time
			if (true) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 50);
				cb.setColorFill(BaseColor.WHITE);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "99:99", 300, 300, 0);
				cb.endText();
			}

			// Dock
			if (deliveryOrder.getDock() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 100);
				cb.setColorFill(BaseColor.BLACK);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 300, 150, 0);
				cb.endText();
			}
		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());
		}
	}

	public static InputStream printBoxLabel(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder,
			List<DeliveryOrderDetail> selectedDeliveryOrderDetailList) throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.LETTER);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\Code128.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		NumberFormat numberFormat = new DecimalFormat("#.#");
		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			Image backGroupImage = Image.getInstance(backGroupImageUrl);
			PdfContentByte cb = writer.getDirectContent();

			int labelHeight = 0;
			for (int i = 0; i < selectedDeliveryOrderDetailList.size(); i++) {

				if (i % 4 == 0) {
					if (i > 0) {
						document.newPage();

						labelHeight = 0;
					}
				}

				DeliveryOrderDetail deliveryOrderDetail = selectedDeliveryOrderDetailList.get(i);

				backGroupImage.setAbsolutePosition(-50, 220 - labelHeight);
				backGroupImage.scalePercent(52, 57);
				underPdfContentByte.addImage(backGroupImage);

				// Supplier Name
				if (deliveryOrder.getSupplierName() != null) {
					cb.beginText();
					cb.setFontAndSize(simBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName(), 120, 755 - labelHeight, 0);
					cb.endText();
				}

				// Supplier Code
				if (deliveryOrder.getSupplierCode() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 14);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 120, 735 - labelHeight, 0);
					cb.endText();
				}
				// Order Group
				if (deliveryOrder.getOrderGroup() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 205, 735 - labelHeight, 0);
					cb.endText();
				}
				// Plantname + Address
				if (deliveryOrder.getPlantName() != null) {
					cb.beginText();
					cb.setFontAndSize(simBf, 10);
					cb.setColorFill(BaseColor.WHITE);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantName(), 310, 745 - labelHeight, 0);
					cb.endText();
				}
				if (deliveryOrder.getPlantAddress1() != null) {
					cb.beginText();
					cb.setFontAndSize(simBf, 8);
					cb.setColorFill(BaseColor.WHITE);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantAddress1(), 310, 735 - labelHeight, 0);
					cb.endText();
				}

				// Manifest number
				if (deliveryOrder.getDoNo() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 20);
					cb.setColorFill(BaseColor.BLACK);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDoNo(), 490, 745 - labelHeight, 0);
					cb.endText();
				}

				// Package type
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 14);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "AAAA", 120, 708 - labelHeight, 0);
					cb.endText();
				}

				// Label ID (¡°*0000000001*¡±)+ barcode #1
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 12);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*0000000000*", 240, 759 - labelHeight, 0);
					cb.endText();
				
					cb.beginText();
					cb.setFontAndSize(barCodeBf, 18);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "00000000000000000000", 350, 755 - labelHeight, 0);
					cb.endText();
				}

				// Part Number
				if (deliveryOrderDetail.getItem().getCode() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItem().getCode(), 525, 715 - labelHeight, 0);
					cb.endText();
				}

				// part description
				if (deliveryOrderDetail.getItemDescription() != null) {
					cb.beginText();
					cb.setFontAndSize(simBf, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 523, 680 - labelHeight, 0);
					cb.endText();
				}

				// SEBANGO code + barcode #3
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 70);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "AAAA", 330, 670 - labelHeight, 0);
					cb.endText();
				}
				if (true) {
					cb.beginText();
					cb.setFontAndSize(barCodeBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "99999999999999999999", 330, 645 - labelHeight, 0);
					cb.endText();
				}

				// Supplier data + barcode #2
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 14);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "9999999999", 120, 675 - labelHeight, 0);
					cb.endText();
				}
				if (true) {
					cb.beginText();
					cb.setFontAndSize(barCodeBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "9999999999999999999", 120, 652 - labelHeight, 0);
					cb.endText();
				}

				if (deliveryOrderDetail.getQty() != null) {
					// Quantity (per box) + barcode #3
					cb.beginText();
					cb.setFontAndSize(dinBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrderDetail.getQty()), 510, 636 - labelHeight, 0);
					cb.endText();

					cb.beginText();
					cb.setFontAndSize(barCodeBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrderDetail.getQty()), 555, 636 - labelHeight, 0);
					cb.endText();
				}

				// Storage Code
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 30);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "K17O-2", 330, 605 - labelHeight, 0);
					cb.endText();
				}

				// Dock
				if (deliveryOrder.getDock() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 520, 605 - labelHeight, 0);
					cb.endText();
				}
				labelHeight += 190;
			}

		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());
		}
	}

	public static InputStream printBoxLabel1(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder,
			List<DeliveryOrderDetail> selectedDeliveryOrderDetailList) throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.LETTER);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\Code128.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		NumberFormat numberFormat = new DecimalFormat("#.#");
		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			Image backGroupImage = Image.getInstance(backGroupImageUrl);
			PdfContentByte cb = writer.getDirectContent();

			int labelHeight = 0;
			for (int i = 0; i < selectedDeliveryOrderDetailList.size(); i++) {

				if (i % 4 == 0) {
					if (i > 0) {
						document.newPage();

						labelHeight = 0;
					}
				}

				backGroupImage.setAbsolutePosition(-38, 220 - labelHeight);
				backGroupImage.scalePercent(52, 57);
				underPdfContentByte.addImage(backGroupImage);

				DeliveryOrderDetail deliveryOrderDetail = selectedDeliveryOrderDetailList.get(i);

				String itemCode = deliveryOrderDetail.getItem().getCode();

				// barcode
				if (true) {
					cb.beginText();
					cb.setFontAndSize(barCodeBf, 16);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "999999999999999999999999999999", 280, 754 - labelHeight, 0);
					cb.endText();
				}

				// Supplier
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 18);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "999999999", 95, 745 - labelHeight, 0);
					cb.endText();
				}

				// inbound number
				if (deliveryOrder.getDoNo() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 20);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDoNo(), 485, 742 - labelHeight, 0);
					cb.endText();
				}

				// plantcode
				if (deliveryOrder.getPlantCode() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.setColorFill(BaseColor.WHITE);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantCode(), 275, 740 - labelHeight, 0);
					cb.endText();
				}

				// barcode text
				if (true) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.setColorFill(BaseColor.BLACK);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*999999999999999999999999999*", 280, 722 - labelHeight, 0);
					cb.endText();
				}

				// part number
				if (itemCode != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, itemCode, 510, 713 - labelHeight, 0);
					cb.endText();
				}

				// part description
				if (deliveryOrderDetail.getItemDescription() != null) {
					cb.beginText();
					cb.setFontAndSize(simBf, 9);
					cb.setColorFill(BaseColor.BLACK);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 510, 690 - labelHeight, 0);
					cb.endText();
				}

				// supplier data
				if (deliveryOrder.getSupplierCode() != null) {
					cb.beginText();
					cb.setFontAndSize(barCodeBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 95, 675 - labelHeight, 0);
					cb.endText();
				}

				if (deliveryOrder.getSupplierName() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName(), 95, 660 - labelHeight, 0);
					cb.endText();
				}

				// last 4 of part Number
				if (itemCode != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 90);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, itemCode.substring(itemCode.length() - 4), 305, 645 - labelHeight, 0);
					cb.endText();
				}

				// qty
				if (deliveryOrderDetail.getQty() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 26);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrderDetail.getQty()), 512, 658 - labelHeight, 0);
					cb.endText();
				}
				labelHeight += 200;
			}

		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());
		}
	}

}
