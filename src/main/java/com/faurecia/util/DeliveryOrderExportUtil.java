package com.faurecia.util;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.IOException;
import java.io.InputStream;
import java.math.BigDecimal;
import java.net.MalformedURLException;
import java.text.DecimalFormat;
import java.text.NumberFormat;
import java.text.SimpleDateFormat;
import java.util.List;
import java.util.Locale;

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

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
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
				} else if (deliveryOrderDetail.getPurchaseOrderDetail() != null) {
					damandQty = deliveryOrderDetail.getPurchaseOrderDetail().getQty();
				} else {
					damandQty = BigDecimal.ZERO;
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

	@SuppressWarnings("finally")
	public static InputStream exportDo(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder, Boolean isCN)
			throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		Image backGroupImage = Image.getInstance(backGroupImageUrl);
		backGroupImage.setAbsolutePosition(0, -2);
		backGroupImage.scaleAbsolute(595, 845);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBfb = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		NumberFormat numberFormat = new DecimalFormat("#.#");

		BaseFont nameFont = isCN ? simBf : dinBf;
		BaseFont nameBoldFont = isCN ? simBfb : dinBfb;

		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			PdfContentByte cb = writer.getDirectContent();

			int rowPix = 520;
			BigDecimal totalBoxQty = BigDecimal.ZERO;
			int totalPage = (int) Math.ceil(((double) deliveryOrder.getDeliveryOrderDetailList().size()) / 15);
			int currentPage = 0;
			for (int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);

				if (i % 15 == 0) {

					if (i > 0) {
						document.newPage();
					}

					currentPage++;
					// Release Date and Time
					SimpleDateFormat printDateToStr = new SimpleDateFormat("dd-MMM-yy HH:mm", Locale.ENGLISH);
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, printDateToStr.format(new java.util.Date()), 100, 35, 0);
					cb.endText();

					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, currentPage + "/" + totalPage, 490, 35, 0);
					cb.endText();

					if (totalPage > 1) {
						if (currentPage < totalPage) {
							if (currentPage == 1) {
								backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
										+ File.separator + "DoFirst.png";
							} else {
								backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
										+ File.separator + "DoFirst1.png";
							}
						} else {
							backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
									+ File.separator + "Do1.png";
						}

					}

					backGroupImage = Image.getInstance(backGroupImageUrl);
					backGroupImage.setAbsolutePosition(0, -2);
					backGroupImage.scaleAbsolute(595, 845);
					rowPix = 524;
					exportDoHead(underPdfContentByte, cb, deliveryOrder, backGroupImage, isCN);
				}

				BigDecimal unitCount = deliveryOrderDetail.getUnitCount();
				if (unitCount == null) {
					unitCount = new BigDecimal(1);
				}

				BigDecimal qty = deliveryOrderDetail.getQty();
				BigDecimal boxQty = qty.divide(unitCount, 1, BigDecimal.ROUND_HALF_UP);
				totalBoxQty = totalBoxQty.add(boxQty);

				// PART NUMBER
				if (deliveryOrderDetail.getItem() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 7);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItem().getCode(), 88, rowPix, 0);
					cb.endText();
				}

				// PART NUMBER INDICE
				if (deliveryOrderDetail.getIndice() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getIndice().toString(), 134, rowPix, 0);
					cb.endText();
				}

				// DESCRIPTION
				if (deliveryOrderDetail.getItemDescription() != null) {
					cb.beginText();
					cb.setFontAndSize(nameFont, 7);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 220, rowPix, 0);
					cb.endText();
				}

				// ORDER_LOT
				if (deliveryOrderDetail.getOrderLot() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrderDetail.getOrderLot()), 320, rowPix, 0);
					cb.endText();
				}

				// PCS/PU
				if (unitCount != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(unitCount), 350, rowPix, 0);
					cb.endText();
				}

				if (boxQty != null) {
					cb.beginText();
					cb.setFontAndSize(dinBfb, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(boxQty), 380, rowPix, 0);
					cb.endText();
				}

				if (qty != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(qty), 410, rowPix, 0);
					cb.endText();
				}

				rowPix -= 23;
				if (i % 5 == 0) {
					rowPix -= 5;
				}

			}

			cb.beginText();
			cb.setFontAndSize(dinBfb, 12);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(totalBoxQty), 380, 162, 0);
			cb.endText();

		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());

		}
	}

	public static InputStream exportDoSebango(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder, Boolean isCN)
			throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		Image backGroupImage = Image.getInstance(backGroupImageUrl);
		backGroupImage.setAbsolutePosition(0, 0);
		backGroupImage.scaleAbsolute(600, 847);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBfb = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		BaseFont nameFont = isCN ? simBf : dinBf;

		NumberFormat numberFormat = new DecimalFormat("#.#");

		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			PdfContentByte cb = writer.getDirectContent();

			int rowPix = 522;
			BigDecimal totalBoxQty = BigDecimal.ZERO;
			int totalPage = (int) Math.ceil(((double) deliveryOrder.getDeliveryOrderDetailList().size()) / 15);
			int currentPage = 0;
			for (int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);

				if (i % 15 == 0) {

					if (i > 0) {
						document.newPage();
					}

					currentPage++;
					// Release Date and Time
					SimpleDateFormat printDateToStr = new SimpleDateFormat("dd-MMM-yy HH:mm", Locale.ENGLISH);
					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, printDateToStr.format(new java.util.Date()), 80, 40, 0);
					cb.endText();

					cb.beginText();
					cb.setFontAndSize(dinBf, 10);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, currentPage + "/" + totalPage, 480, 40, 0);
					cb.endText();

					if (totalPage > 1) {
						String template = "";
						if (currentPage < totalPage) {
							if (currentPage == 1) {
								template = isCN ? "DoSebangoFirst_CN.png" : "DoSebangoFirst.png";
								backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
										+ File.separator + template;
							} else {
								template = isCN ? "DoSebangoFirst1_CN.png" : "DoSebangoFirst1.png";
								backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
										+ File.separator + template;
							}
						} else {
							template = isCN ? "DoSebango1_CN.png" : "DoSebango1.png";
							backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template"
									+ File.separator + template;
						}

					}

					backGroupImage = Image.getInstance(backGroupImageUrl);
					backGroupImage.setAbsolutePosition(0, 0);
					backGroupImage.scaleAbsolute(600, 847);
					rowPix = 520;
					exportDoSebangoHead(underPdfContentByte, cb, deliveryOrder, backGroupImage, isCN);
				}

				BigDecimal unitCount = deliveryOrderDetail.getUnitCount();
				if (unitCount == null) {
					unitCount = new BigDecimal(1);
				}

				BigDecimal qty = deliveryOrderDetail.getQty();
				BigDecimal boxQty = qty.divide(unitCount, 1, BigDecimal.ROUND_HALF_UP);
				totalBoxQty = totalBoxQty.add(boxQty);

				// PART NUMBER
				if (deliveryOrderDetail.getItem() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItem().getCode(), 68, rowPix, 0);
					cb.endText();
				}

				// PART NUMBER INDICE
				if (deliveryOrderDetail.getIndice() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getIndice().toString(), 116, rowPix, 0);
					cb.endText();
				}

				// PART SEBANGO
				if (deliveryOrderDetail.getSebango() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBfb, 12);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getSebango(), 145, rowPix, 0);
					cb.endText();
				}

				// DESCRIPTION
				if (deliveryOrderDetail.getItemDescription() != null) {
					cb.beginText();
					cb.setFontAndSize(nameFont, 6);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 230, rowPix, 0);
					cb.endText();
				}

				// ORDER_LOT
				if (deliveryOrderDetail.getOrderLot() != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrderDetail.getOrderLot()), 305, rowPix, 0);
					cb.endText();
				}

				// PCS/PU
				if (unitCount != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(unitCount), 335, rowPix, 0);
					cb.endText();
				}

				if (boxQty != null) {
					cb.beginText();
					cb.setFontAndSize(dinBfb, 12);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(boxQty), 365, rowPix, 0);
					cb.endText();
				}

				if (qty != null) {
					cb.beginText();
					cb.setFontAndSize(dinBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(qty), 397, rowPix, 0);
					cb.endText();
				}

				rowPix -= 23;
				if (i % 5 == 0) {
					rowPix -= 2;
				}

			}

			cb.beginText();
			cb.setFontAndSize(dinBf, 12);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(totalBoxQty), 370, 165, 0);
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

	private static void exportDoHead(PdfContentByte underPdfContentByte, PdfContentByte cb, DeliveryOrder deliveryOrder, Image backGroupImage,
			Boolean isCN) throws DocumentException, IOException {
		underPdfContentByte.addImage(backGroupImage);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBfb = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\free3of9.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		NumberFormat numberFormat = new DecimalFormat("#.#");

		BaseFont nameFont = isCN ? simBf : dinBf;
		BaseFont nameBoldFont = isCN ? simBfb : dinBfb;

		// Title
		String title = deliveryOrder.getTitle();
		if (deliveryOrder.getIsLogisticPartner()) {
			title = "LOGISTIC PARTNER MANIFEST";
		}
		if (title != null) {
			cb.beginText();
			cb.setFontAndSize(nameBoldFont, 18);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, title, 373, 805, 0);
			cb.endText();
		}
		// Faurecia plant name and address
		if (deliveryOrder.getPlantName() != null) {
			cb.beginText();
			cb.setFontAndSize(nameBoldFont, 9);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantName(), 38, 785, 0);
			cb.endText();
		}

		if (deliveryOrder.getPlantAddress1() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantAddress1(), 38, 774, 0);
			cb.endText();
		}
		if (deliveryOrder.getPlantAddress2() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantAddress2(), 38, 767, 0);
			cb.endText();
		}
		if (deliveryOrder.getPlantAddress3() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantAddress3(), 38, 758, 0);
			cb.endText();
		}

		String pinfoStr = "";
		if (deliveryOrder.getPlantPostCode() != null) {
			pinfoStr += deliveryOrder.getPlantPostCode();
		}
		if (deliveryOrder.getPlantCity() != null) {
			pinfoStr += " " + deliveryOrder.getPlantCity();
		}
		if (deliveryOrder.getPlantCountry() != null) {
			pinfoStr += "-" + deliveryOrder.getPlantCountry();
		}

		if (pinfoStr != "") {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, pinfoStr, 38, 750, 0);
			cb.endText();
		}

		if (deliveryOrder.getPlantPhone() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, "TEL:" + deliveryOrder.getPlantPhone(), 38, 742, 0);
			cb.endText();
		}

		// MURN
		if (deliveryOrder.getMurn() != null) {
			cb.beginText();
			cb.setFontAndSize(barCodeBf, 36);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*" + deliveryOrder.getMurn() + "*", 292, 767, 0);
			cb.endText();

			cb.beginText();
			cb.setFontAndSize(dinBf, 14);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getMurn(), 293, 747, 0);
			cb.endText();
		}

		// supplier name and address

		if (deliveryOrder.getSupplierName() != null) {
			cb.beginText();
			cb.setFontAndSize(nameBoldFont, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName(), 478, 785, 0);
			cb.endText();
		}

		if (deliveryOrder.getSupplierAddress1() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierAddress1(), 478, 776, 0);
			cb.endText();
		}
		if (deliveryOrder.getSupplierAddress2() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierAddress2(), 478, 767, 0);
			cb.endText();
		}
		if (deliveryOrder.getSupplierAddress3() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierAddress3(), 478, 758, 0);
			cb.endText();
		}

		String infoStr = "";
		if (deliveryOrder.getSupplierPostCode() != null) {
			infoStr += deliveryOrder.getSupplierPostCode();
		}
		if (deliveryOrder.getSupplierCity() != null) {
			infoStr += " " + deliveryOrder.getSupplierCity();
		}
		if (deliveryOrder.getSupplierCountry() != null) {
			infoStr += "-" + deliveryOrder.getSupplierCountry();
		}

		if (infoStr != "") {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, infoStr, 478, 750, 0);
			cb.endText();
		}

		if (deliveryOrder.getSupplierPhone() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "TEL:" + deliveryOrder.getSupplierPhone(), 477, 742, 0);
			cb.endText();
		}

		// Manifest Number
		if (deliveryOrder.getExternalDoNo() != null) {
			String manfest = deliveryOrder.getExternalDoNo();
			if (manfest.length() >= 13) {
				String[] strArr = manfest.split("-");
				String str1 = strArr[0].substring(0, strArr[0].length() - 4);
				String str2 = strArr[0].substring(strArr[0].length() - 4);

				cb.beginText();
				cb.setFontAndSize(dinBfb, 16);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str1 + " ", 80, 696, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBfb, 25);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str2 + " ", 140, 696, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBfb, 16);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "-" + strArr[1], 182, 696, 0);
				cb.endText();
			} else {
				cb.beginText();
				cb.setFontAndSize(dinBf, 16);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo(), 120, 696, 0);
				cb.endText();
			}
		}

		// Supplier Code
		if (deliveryOrder.getSupplierCode() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 290, 701, 0);
			cb.endText();
		}

		// Invoice Number
		if (false) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "AAAAAAAAA", 403, 701, 0);
			cb.endText();
		}

		// Total weight + Unit
		String totalWeight = "";
		if (deliveryOrder.getTotalWeight() != null) {
			totalWeight = numberFormat.format(deliveryOrder.getTotalWeight());
			if (deliveryOrder.getUnitWeight() != null) {
				totalWeight += deliveryOrder.getUnitWeight();
			}
			if (deliveryOrder.getTotalWeight().intValue() == 0 || deliveryOrder.getTotalWeight().intValue() > 50000) {
				totalWeight = "#####";
			}
		} else {
			totalWeight = "#####";
		}
		cb.beginText();
		cb.setFontAndSize(dinBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, totalWeight, 74, 652, 0);
		cb.endText();

		// Volume + Unit
		String totalVolume = "";
		if (deliveryOrder.getTotalVolume() != null) {
			totalVolume = numberFormat.format(deliveryOrder.getTotalVolume());
			if (deliveryOrder.getUnitVolume() != null) {
				totalVolume += deliveryOrder.getUnitVolume();
			}
			if (deliveryOrder.getTotalVolume().intValue() == 0 || deliveryOrder.getTotalVolume().intValue() > 150) {
				totalVolume = "###";
			}
		} else {
			totalVolume = "###";
		}
		cb.beginText();
		cb.setFontAndSize(dinBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, totalVolume, 145, 652, 0);
		cb.endText();

		// Nb Pallets or Cont
		if (deliveryOrder.getTotalNbPallets() != null)
			cb.beginText();
		cb.setFontAndSize(dinBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrder.getTotalNbPallets()), 232, 652, 0);
		cb.endText();

		// Order group
		if (deliveryOrder.getOrderGroup() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 330, 652, 0);
			cb.endText();
		}

		// Sub-route
		if (deliveryOrder.getRoute() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getRoute(), 423, 652, 0);
			cb.endText();
		}

		// Main route
		if (deliveryOrder.getMainRoute() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBfb, 14);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getMainRoute(), 508, 652, 0);
			cb.endText();
		}

		// Supplier collection date
		if (deliveryOrder.getStartDate() != null) {
			SimpleDateFormat startDateToStr = new SimpleDateFormat("dd-MMM-yy", Locale.ENGLISH);

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, startDateToStr.format(deliveryOrder.getStartDate()), 65, 595, 0);
			cb.endText();

			// Collection time

			SimpleDateFormat startTimeToStr = new SimpleDateFormat("HH:mm");

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, startTimeToStr.format(deliveryOrder.getStartDate()), 115, 595, 0);
			cb.endText();
		}

		// Faurecia delivery date
		if (deliveryOrder.getEndDate() != null) {
			SimpleDateFormat endDateToStr = new SimpleDateFormat("dd-MMM-yy", Locale.ENGLISH);

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, endDateToStr.format(deliveryOrder.getEndDate()), 170, 595, 0);
			cb.endText();

			// Delivery time
			SimpleDateFormat endTimeToStr = new SimpleDateFormat("HH:mm");

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, endTimeToStr.format(deliveryOrder.getEndDate()), 225, 595, 0);
			cb.endText();
		}

		// Delivery dock
		if (deliveryOrder.getDock() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 262, 595, 0);
			cb.endText();
		}

		// Arrival at 1st cross dock

		cb.beginText();
		cb.setFontAndSize(dinBf, 8);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 320, 595, 0);
		cb.endText();

		// Departure from 1st cross dock
		cb.beginText();
		cb.setFontAndSize(dinBf, 8);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 385, 595, 0);
		cb.endText();

		// Arrival at 2nd cross dock
		cb.beginText();
		cb.setFontAndSize(dinBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 457, 595, 0);
		cb.endText();

		// Departure from 2nd cross dock
		cb.beginText();
		cb.setFontAndSize(dinBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 525, 595, 0);
		cb.endText();

	}

	private static void exportDoSebangoHead(PdfContentByte underPdfContentByte, PdfContentByte cb, DeliveryOrder deliveryOrder, Image backGroupImage,
			Boolean isCN) throws DocumentException, IOException {
		underPdfContentByte.addImage(backGroupImage);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBfb = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\free3of9.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		NumberFormat numberFormat = new DecimalFormat("#.#");

		BaseFont nameFont = isCN ? simBf : dinBf;
		BaseFont nameBoldFont = isCN ? simBfb : dinBfb;

		// Title
		String title = deliveryOrder.getTitle();
		if (deliveryOrder.getIsLogisticPartner()) {
			title = isCN ? new String("承运商要货令") : "LOGISTIC PARTNER MANIFEST";
		}
		if (title != null) {
			cb.beginText();
			cb.setFontAndSize(nameBoldFont, 18);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, title, 353, 810, 0);
			cb.endText();
		}
		// Faurecia plant name and address
		if (deliveryOrder.getPlantName() != null) {
			cb.beginText();
			cb.setFontAndSize(nameBoldFont, 9);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantName(), 18, 790, 0);
			cb.endText();
		}

		if (deliveryOrder.getPlantAddress1() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantAddress1(), 18, 782, 0);
			cb.endText();
		}
		if (deliveryOrder.getPlantAddress2() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantAddress2(), 18, 775, 0);
			cb.endText();
		}
		if (deliveryOrder.getPlantAddress3() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, deliveryOrder.getPlantAddress3(), 18, 766, 0);
			cb.endText();
		}

		String pinfoStr = "";
		if (deliveryOrder.getPlantPostCode() != null) {
			pinfoStr += deliveryOrder.getPlantPostCode();
		}
		if (deliveryOrder.getPlantCity() != null) {
			pinfoStr += " " + deliveryOrder.getPlantCity();
		}
		if (deliveryOrder.getPlantCountry() != null) {
			pinfoStr += "-" + deliveryOrder.getPlantCountry();
		}

		if (pinfoStr != "") {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, pinfoStr, 18, 758, 0);
			cb.endText();
		}

		if (deliveryOrder.getPlantPhone() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_LEFT, "TEL:" + deliveryOrder.getPlantPhone(), 18, 750, 0);
			cb.endText();
		}

		// MURN
		if (deliveryOrder.getMurn() != null) {
			cb.beginText();
			cb.setFontAndSize(barCodeBf, 36);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*" + deliveryOrder.getMurn() + "*", 272, 768, 0);
			cb.endText();

			cb.beginText();
			cb.setFontAndSize(dinBf, 14);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getMurn(), 273, 752, 0);
			cb.endText();
		}

		// supplier name and address

		if (deliveryOrder.getSupplierName() != null) {
			cb.beginText();
			cb.setFontAndSize(nameBoldFont, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName(), 467, 793, 0);
			cb.endText();
		}

		if (deliveryOrder.getSupplierAddress1() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierAddress1(), 467, 784, 0);
			cb.endText();
		}
		if (deliveryOrder.getSupplierAddress2() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierAddress2(), 467, 776, 0);
			cb.endText();
		}
		if (deliveryOrder.getSupplierAddress3() != null) {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierAddress3(), 467, 767, 0);
			cb.endText();
		}

		String infoStr = "";
		if (deliveryOrder.getSupplierPostCode() != null) {
			infoStr += deliveryOrder.getSupplierPostCode();
		}
		if (deliveryOrder.getSupplierCity() != null) {
			infoStr += " " + deliveryOrder.getSupplierCity();
		}
		if (deliveryOrder.getSupplierCountry() != null) {
			infoStr += "-" + deliveryOrder.getSupplierCountry();
		}

		if (infoStr != "") {
			cb.beginText();
			cb.setFontAndSize(nameFont, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, infoStr, 467, 759, 0);
			cb.endText();
		}

		if (deliveryOrder.getSupplierPhone() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 6);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "TEL:" + deliveryOrder.getSupplierPhone(), 467, 751, 0);
			cb.endText();
		}

		// Manifest Number
		if (deliveryOrder.getExternalDoNo() != null) {
			String manfest = deliveryOrder.getExternalDoNo();
			if (manfest.length() >= 12) {

				String str1 = manfest.substring(0, 4);
				String str2 = manfest.substring(4, 8);
				String str3 = manfest.substring(8);

				cb.beginText();
				cb.setFontAndSize(dinBfb, 16);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str1 + " ", 63, 701, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBfb, 25);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str2 + " ", 112, 701, 0);
				cb.endText();

				cb.beginText();
				cb.setFontAndSize(dinBfb, 16);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str3, 162, 701, 0);
				cb.endText();
			} else {
				cb.beginText();
				cb.setFontAndSize(dinBf, 16);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo(), 120, 701, 0);
				cb.endText();
			}
		}

		// Supplier Code
		if (deliveryOrder.getSupplierCode() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 275, 706, 0);
			cb.endText();
		}

		// Invoice Number
		if (false) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "AAAAAAAAA", 385, 706, 0);
			cb.endText();
		}

		// Total weight + Unit
		String totalWeight = "";
		if (deliveryOrder.getTotalWeight() != null) {
			totalWeight = numberFormat.format(deliveryOrder.getTotalWeight());
			if (deliveryOrder.getUnitWeight() != null) {
				totalWeight += deliveryOrder.getUnitWeight();
			}
			if (deliveryOrder.getTotalWeight().intValue() == 0 || deliveryOrder.getTotalWeight().intValue() > 50000) {
				totalWeight = "#####";
			}
		} else {
			totalWeight = "#####";
		}
		cb.beginText();
		cb.setFontAndSize(dinBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, totalWeight, 54, 657, 0);
		cb.endText();

		// Volume + Unit
		String totalVolume = "";
		if (deliveryOrder.getTotalVolume() != null) {
			totalVolume = numberFormat.format(deliveryOrder.getTotalVolume());
			if (deliveryOrder.getUnitVolume() != null) {
				totalVolume += deliveryOrder.getUnitVolume();
			}
			if (deliveryOrder.getTotalVolume().intValue() == 0 || deliveryOrder.getTotalVolume().intValue() > 150) {
				totalVolume = "###";
			}
		} else {
			totalVolume = "###";
		}
		cb.beginText();
		cb.setFontAndSize(dinBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, totalVolume, 125, 657, 0);
		cb.endText();

		// Nb Pallets or Cont
		if (deliveryOrder.getTotalNbPallets() != null)
			cb.beginText();
		cb.setFontAndSize(dinBf, 10);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(deliveryOrder.getTotalNbPallets()), 212, 657, 0);
		cb.endText();

		// Order group
		if (deliveryOrder.getOrderGroup() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 315, 657, 0);
			cb.endText();
		}

		// Sub-route
		if (deliveryOrder.getRoute() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 10);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getRoute(), 420, 657, 0);
			cb.endText();
		}

		// Main route
		if (deliveryOrder.getMainRoute() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBfb, 14);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getMainRoute(), 510, 657, 0);
			cb.endText();
		}

		// Supplier collection date
		if (deliveryOrder.getStartDate() != null) {
			SimpleDateFormat startDateToStr = new SimpleDateFormat("dd-MMM-yy", Locale.ENGLISH);

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, startDateToStr.format(deliveryOrder.getStartDate()), 45, 593, 0);
			cb.endText();

			// Collection time

			SimpleDateFormat startTimeToStr = new SimpleDateFormat("HH:mm");

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, startTimeToStr.format(deliveryOrder.getStartDate()), 100, 593, 0);
			cb.endText();
		}

		// Faurecia delivery date
		if (deliveryOrder.getEndDate() != null) {
			SimpleDateFormat endDateToStr = new SimpleDateFormat("dd-MMM-yy", Locale.ENGLISH);

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, endDateToStr.format(deliveryOrder.getEndDate()), 155, 593, 0);
			cb.endText();

			// Delivery time
			SimpleDateFormat endTimeToStr = new SimpleDateFormat("HH:mm");

			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, endTimeToStr.format(deliveryOrder.getEndDate()), 210, 593, 0);
			cb.endText();
		}

		// Delivery dock
		if (deliveryOrder.getDock() != null) {
			cb.beginText();
			cb.setFontAndSize(dinBf, 8);
			cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 245, 593, 0);
			cb.endText();
		}

		// Arrival at 1st cross dock

		cb.beginText();
		cb.setFontAndSize(dinBf, 8);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 305, 593, 0);
		cb.endText();

		// Departure from 1st cross dock
		cb.beginText();
		cb.setFontAndSize(dinBf, 8);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 370, 593, 0);
		cb.endText();

		// Arrival at 2nd cross dock
		cb.beginText();
		cb.setFontAndSize(dinBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 437, 593, 0);
		cb.endText();

		// Departure from 2nd cross dock
		cb.beginText();
		cb.setFontAndSize(dinBf, 11);
		cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "", 505, 593, 0);
		cb.endText();

	}

	@SuppressWarnings("deprecation")
	public static InputStream printPalletLabel(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder)
			throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		Image backGroupImage = Image.getInstance(backGroupImageUrl);
		backGroupImage.setAbsolutePosition(0, 0);
		backGroupImage.scaleAbsolute(600, 847);

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\free3of9.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		NumberFormat numberFormat = new DecimalFormat("#.#");

		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			PdfContentByte cb = writer.getDirectContent();

			backGroupImage.setAbsolutePosition(20, 0);
			backGroupImage.scaleAbsolute(600, 850);
			underPdfContentByte.addImage(backGroupImage);

			// customer code
			if (deliveryOrder.getPlantName() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBfb, 30);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantName(), 300, 680, 0);
				cb.endText();
			}

			// supplier code
			if (deliveryOrder.getSupplierCode() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBf, 26);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 300, 625, 0);
				cb.endText();
			}

			// supplier name
			if (deliveryOrder.getSupplierName() != null) {
				cb.beginText();
				cb.setFontAndSize(simBf, 20);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierName(), 300, 588, 0);
				cb.endText();
			}

			// Route Number
			if (deliveryOrder.getMainRoute() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBfb, 50);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getMainRoute(), 230, 500, 0);
				cb.endText();
			}

			// Order Group
			if (deliveryOrder.getOrderGroup() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBfb, 50);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 460, 500, 0);
				cb.endText();
			}

			// Manifest Number
			if (deliveryOrder.getExternalDoNo() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBfb, 30);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo(), 300, 430, 0);
				cb.endText();
			}

			// Unload Time
			if (deliveryOrder.getEndDate() != null) {
				SimpleDateFormat dateToStr = new SimpleDateFormat("HH:mm");

				cb.beginText();
				cb.setFontAndSize(dinBfb, 50);
				cb.setColorFill(BaseColor.WHITE);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, dateToStr.format(deliveryOrder.getEndDate()), 300, 350, 0);
				cb.endText();
			}

			// Dock
			if (deliveryOrder.getDock() != null) {
				cb.beginText();
				cb.setFontAndSize(dinBfb, 100);
				cb.setColorFill(BaseColor.BLACK);
				cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 300, 200, 0);
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
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\free3of9.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		NumberFormat numberFormat = new DecimalFormat("#.#");
		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			Image backGroupImage = Image.getInstance(backGroupImageUrl);
			PdfContentByte cb = writer.getDirectContent();

			int labelHeight = 0;
			int labelId = 0;
			int totalBoxCount = 0;
			for (int i = 0; i < selectedDeliveryOrderDetailList.size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = selectedDeliveryOrderDetailList.get(i);
				labelId = deliveryOrderDetail.getLabel();
				for (int j = 0; j < deliveryOrderDetail.getBoxCount().intValue(); j++) {

					if (totalBoxCount % 4 == 0) {
						if (totalBoxCount > 0) {
							document.newPage();

							labelHeight = 0;
						}
					}

					backGroupImage.setAbsolutePosition(-100, 300 - labelHeight);
					backGroupImage.scalePercent(57, 52);
					underPdfContentByte.addImage(backGroupImage);

					BigDecimal qty = deliveryOrderDetail.getUnitCount();
					if (j > 0 && j == deliveryOrderDetail.getBoxCount().intValue() - 1) {
						qty = deliveryOrderDetail.getQty().subtract(deliveryOrderDetail.getUnitCount().multiply(new BigDecimal(j)));
					}

					// Supplier Name
					if (deliveryOrder.getSupplierName() != null) {

						String supplierName = deliveryOrder.getSupplierName();
						String[] supplierNameArr = supplierName.split(" ");
						String supplier1 = "";
						String supplier2 = "";
						if (supplierName.length() > 10) {
							for (int k = 0; k < supplierNameArr.length; k++) {
								if (supplier1.length() < 10) {
									supplier1 = supplier1 + "  " + supplierNameArr[k];
								} else {
									supplier2 = supplier2 + "  " + supplierNameArr[k];
								}
							}
						} else {
							supplier1 = supplierName;
						}

						cb.beginText();
						cb.setFontAndSize(dinBf, 7);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, supplier1, 110, 790 - labelHeight, 0);
						cb.endText();

						if (supplier2 != "") {
							cb.beginText();
							cb.setFontAndSize(dinBf, 7);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, supplier2, 110, 780 - labelHeight, 0);
							cb.endText();
						}
					}

					// Supplier Code
					if (deliveryOrder.getSupplierCode() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 8);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 110, 765 - labelHeight, 0);
						cb.endText();
					}
					// Order Group
					if (deliveryOrder.getOrderGroup() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 20);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 217, 762 - labelHeight, 0);
						cb.endText();
					}
					// Plantname + Address
					if (deliveryOrder.getPlantName() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 12);
						cb.setColorFill(BaseColor.WHITE);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantName(), 329, 768 - labelHeight, 0);
						cb.endText();
					}
					if (deliveryOrder.getPlantPostCode() != null && deliveryOrder.getPlantCity() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 8);
						cb.setColorFill(BaseColor.WHITE);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantPostCode() + "  " + deliveryOrder.getPlantCity(), 320,
								757 - labelHeight, 0);
						cb.endText();
					}

					// Manifest number
					if (deliveryOrder.getExternalDoNo() != null) {
						String manfest = deliveryOrder.getExternalDoNo();
						if (manfest.length() == 13) {
							String[] strArr = manfest.split("-");
							String str1 = strArr[0].substring(0, strArr[0].length() - 4);
							String str2 = strArr[0].substring(strArr[0].length() - 4);

							cb.beginText();
							cb.setFontAndSize(dinBfb, 14);
							cb.setColorFill(BaseColor.BLACK);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str1.substring(0, 4) + "  " + str1.substring(4), 450, 780 - labelHeight,
									0);
							cb.endText();

							cb.beginText();
							cb.setFontAndSize(dinBfb, 20);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str2, 505, 780 - labelHeight, 0);
							cb.endText();

							cb.beginText();
							cb.setFontAndSize(dinBfb, 14);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "-" + strArr[1], 545, 780 - labelHeight, 0);
							cb.endText();
						} else {
							cb.beginText();
							cb.setFontAndSize(dinBfb, 20);
							cb.setColorFill(BaseColor.BLACK);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo(), 450, 780 - labelHeight, 0);
							cb.endText();
						}
					}

					// Package type
					if (deliveryOrderDetail.getPackageType() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 10);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getPackageType(), 100, 730 - labelHeight, 0);
						cb.endText();
					}

					// Label ID (“*0000000001*”)+ barcode #1
					if (labelId != 0) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 10);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*00" + Integer.toString(labelId) + "*", 220, 789 - labelHeight, 0);
						cb.endText();

						cb.beginText();
						cb.setFontAndSize(barCodeBf, 24);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*00" + Integer.toString(labelId) + "*", 350, 784 - labelHeight, 0);
						cb.endText();
					}

					// Part Number
					if (deliveryOrderDetail.getItem().getCode() != null) {
						String partNubmer = deliveryOrderDetail.getItem().getCode();
						if (deliveryOrderDetail.getIndice() != null) {
							partNubmer += "-" + deliveryOrderDetail.getIndice().toString();
						}
						cb.beginText();
						cb.setFontAndSize(dinBf, 8);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, partNubmer, 513, 755 - labelHeight, 0);
						cb.endText();
					}

					// part description
					if (deliveryOrderDetail.getItemDescription() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 6);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 510, 725 - labelHeight, 0);
						cb.endText();
					}

					// SEBANGO code + barcode #3
					if (deliveryOrderDetail.getSebango() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBfb, 76);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getSebango(), 320, 688 - labelHeight, 0);
						cb.endText();
					}
					if (deliveryOrderDetail.getItem().getCode() != null) {
						cb.beginText();
						cb.setFontAndSize(barCodeBf, 22);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*P" + deliveryOrderDetail.getItem().getCode() + "*", 320, 664 - labelHeight,
								0);
						cb.endText();
					}

					if (qty != BigDecimal.ZERO) {
						// Quantity (per box) + barcode #3
						cb.beginText();
						cb.setFontAndSize(dinBfb, 16);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(qty), 495, 672 - labelHeight, 0);
						cb.endText();

						cb.beginText();
						cb.setFontAndSize(barCodeBf, 22);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*" + numberFormat.format(qty) + "*", 540, 672 - labelHeight, 0);
						cb.endText();
					}

					// Storage Code
					if (deliveryOrderDetail.getStorageCode() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBfb, 26);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getStorageCode(), 320, 635 - labelHeight, 0);
						cb.endText();
					}

					// Dock
					if (deliveryOrder.getDock() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBfb, 18);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 495, 639 - labelHeight, 0);
						cb.endText();
					}
					labelHeight += 200;
					labelId++;
					totalBoxCount++;
				}
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

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\free3of9.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		NumberFormat numberFormat = new DecimalFormat("#.#");
		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			Image backGroupImage = Image.getInstance(backGroupImageUrl);
			PdfContentByte cb = writer.getDirectContent();

			int labelHeight = 0;
			int totalBoxCount = 0;
			for (int i = 0; i < selectedDeliveryOrderDetailList.size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = selectedDeliveryOrderDetailList.get(i);

				for (int j = 0; j < deliveryOrderDetail.getBoxCount().intValue(); j++) {
					if (totalBoxCount % 4 == 0) {

						if (totalBoxCount > 0) {
							document.newPage();

							labelHeight = 0;
						}
					}

					backGroupImage.setAbsolutePosition(-38, 220 - labelHeight);
					backGroupImage.scalePercent(52, 57);
					underPdfContentByte.addImage(backGroupImage);

					String itemCode = deliveryOrderDetail.getItem().getCode();
					BigDecimal qty = deliveryOrderDetail.getUnitCount();
					if (j > 0 && j == deliveryOrderDetail.getBoxCount().intValue() - 1) {
						qty = deliveryOrderDetail.getQty().subtract(deliveryOrderDetail.getUnitCount().multiply(new BigDecimal(j)));
					}
					// barcode
					if (deliveryOrder.getSupplierCode() != null && deliveryOrderDetail.getItem() != null && qty != BigDecimal.ZERO) {
						cb.beginText();
						cb.setFontAndSize(barCodeBf, 16);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*10" + deliveryOrder.getSupplierCode()
								+ deliveryOrderDetail.getItem().getCode() + numberFormat.format(qty) + "*", 280, 754 - labelHeight, 0);
						cb.endText();
					}

					// Supplier
					if (deliveryOrder.getSupplierCode() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 16);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 95, 745 - labelHeight, 0);
						cb.endText();
					}

					// inbound number
					if (deliveryOrder.getExternalDoNo() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 20);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo(), 485, 742 - labelHeight, 0);
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
					if (deliveryOrder.getSupplierCode() != null && deliveryOrderDetail.getItem() != null && qty != BigDecimal.ZERO) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 10);
						cb.setColorFill(BaseColor.BLACK);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*10" + deliveryOrder.getSupplierCode()
								+ deliveryOrderDetail.getItem().getCode() + numberFormat.format(qty) + "*", 280, 722 - labelHeight, 0);
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
					String supplierName = deliveryOrder.getSupplierName();
					String[] supplierNameArr = supplierName.split(" ");
					String supplier1 = "";
					String supplier2 = "";
					if (supplierName.length() > 10) {
						for (int k = 0; k < supplierNameArr.length; k++) {
							if (supplier1.length() < 10) {
								supplier1 = supplier1 + supplierNameArr[k];
							} else {
								supplier2 = supplier2 + supplierNameArr[k];
							}
						}
					} else {
						supplier1 = supplierName;
					}
					cb.beginText();
					cb.setFontAndSize(simBf, 8);
					cb.showTextAligned(PdfContentByte.ALIGN_CENTER, supplier1, 95, 675 - labelHeight, 0);
					cb.endText();

					if (supplier2 != "") {
						cb.beginText();
						cb.setFontAndSize(simBf, 8);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, supplier2, 95, 660 - labelHeight, 0);
						cb.endText();
					}
					// if (deliveryOrder.getSupplierCode() != null) {
					// cb.beginText();
					// cb.setFontAndSize(barCodeBf, 8);
					// cb.showTextAligned(PdfContentByte.ALIGN_CENTER,
					// deliveryOrder.getSupplierCode(), 95, 675 - labelHeight,
					// 0);
					// cb.endText();
					// }
					//
					// if (deliveryOrder.getSupplierName() != null) {
					// cb.beginText();
					// cb.setFontAndSize(dinBf, 10);
					// cb.showTextAligned(PdfContentByte.ALIGN_CENTER,
					// deliveryOrder.getSupplierName(), 95, 660 - labelHeight,
					// 0);
					// cb.endText();
					// }

					// last 4 of part Number
					if (itemCode != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 90);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, itemCode.substring(itemCode.length() - 4), 305, 645 - labelHeight, 0);
						cb.endText();
					}

					// qty
					if (qty != BigDecimal.ZERO) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 26);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(qty), 512, 658 - labelHeight, 0);
						cb.endText();
					}
					labelHeight += 200;
					totalBoxCount++;
				}
			}
		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());
		}
	}

	public static InputStream printBoxLabel2(String localAbsolutPath, String backGroupImageName, DeliveryOrder deliveryOrder,
			List<DeliveryOrderDetail> selectedDeliveryOrderDetailList) throws MalformedURLException, IOException, DocumentException {

		String backGroupImageUrl = localAbsolutPath + "WEB-INF" + File.separator + "classes" + File.separator + "template" + File.separator
				+ backGroupImageName;
		Document document = new Document(PageSize.A4);
		ByteArrayOutputStream outputStream = new ByteArrayOutputStream();

		BaseFont dinBf = BaseFont.createFont("c:\\windows\\fonts\\arial.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont simBf = BaseFont.createFont("c:\\windows\\fonts\\simsun.ttc,1,Bold", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont barCodeBf = BaseFont.createFont("c:\\windows\\fonts\\free3of9.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);
		BaseFont dinBfb = BaseFont.createFont("c:\\windows\\fonts\\arialBD.ttf", BaseFont.IDENTITY_H, BaseFont.NOT_EMBEDDED);

		NumberFormat numberFormat = new DecimalFormat("#.#");
		try {
			PdfWriter writer = PdfWriter.getInstance(document, outputStream);
			document.open();

			PdfContentByte underPdfContentByte = writer.getDirectContentUnder();
			Image backGroupImage = Image.getInstance(backGroupImageUrl);
			PdfContentByte cb = writer.getDirectContent();

			int labelHeight = 0;
			int labelId = 0;
			int totalBoxCount = 0;
			for (int i = 0; i < selectedDeliveryOrderDetailList.size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = selectedDeliveryOrderDetailList.get(i);
				labelId = deliveryOrderDetail.getLabel();
				for (int j = 0; j < deliveryOrderDetail.getBoxCount().intValue(); j++) {

					if (totalBoxCount % 4 == 0) {
						if (totalBoxCount > 0) {
							document.newPage();

							labelHeight = 0;
						}
					}

					backGroupImage.setAbsolutePosition(-100, 300 - labelHeight);
					backGroupImage.scalePercent(57, 52);
					underPdfContentByte.addImage(backGroupImage);

					BigDecimal qty = deliveryOrderDetail.getUnitCount();
					if (j > 0 && j == deliveryOrderDetail.getBoxCount().intValue() - 1) {
						qty = deliveryOrderDetail.getQty().subtract(deliveryOrderDetail.getUnitCount().multiply(new BigDecimal(j)));
					}

					// Supplier Name
					if (deliveryOrder.getSupplierName() != null) {

						String supplierName = deliveryOrder.getSupplierName();
						String[] supplierNameArr = supplierName.split(" ");
						String supplier1 = "";
						String supplier2 = "";
						if (supplierName.length() > 10) {
							for (int k = 0; k < supplierNameArr.length; k++) {
								if (supplier1.length() < 10) {
									supplier1 = supplier1 + "  " + supplierNameArr[k];
								} else {
									supplier2 = supplier2 + "  " + supplierNameArr[k];
								}
							}
						} else {
							supplier1 = supplierName;
						}

						cb.beginText();
						cb.setFontAndSize(simBf, 7);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, supplier1, 110, 790 - labelHeight, 0);
						cb.endText();

						if (supplier2 != "") {
							cb.beginText();
							cb.setFontAndSize(simBf, 7);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, supplier2, 110, 780 - labelHeight, 0);
							cb.endText();
						}
					}

					// Supplier Code
					if (deliveryOrder.getSupplierCode() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 8);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getSupplierCode(), 110, 765 - labelHeight, 0);
						cb.endText();
					}
					// Order Group
					if (deliveryOrder.getOrderGroup() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 20);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getOrderGroup(), 217, 762 - labelHeight, 0);
						cb.endText();
					}
					// Plantname + Address
					if (deliveryOrder.getPlantName() != null) {
						cb.beginText();
						cb.setFontAndSize(simBf, 12);
						cb.setColorFill(BaseColor.WHITE);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantName(), 329, 768 - labelHeight, 0);
						cb.endText();
					}
					if (deliveryOrder.getPlantPostCode() != null && deliveryOrder.getPlantCity() != null) {
						cb.beginText();
						cb.setFontAndSize(simBf, 8);
						cb.setColorFill(BaseColor.WHITE);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getPlantPostCode() + "  " + deliveryOrder.getPlantCity(), 320,
								757 - labelHeight, 0);
						cb.endText();
					}

					// Manifest number
					if (deliveryOrder.getExternalDoNo() != null) {
						String manfest = deliveryOrder.getExternalDoNo();
						if (manfest.length() == 13) {
							String[] strArr = manfest.split("-");
							String str1 = strArr[0].substring(0, strArr[0].length() - 4);
							String str2 = strArr[0].substring(strArr[0].length() - 4);

							cb.beginText();
							cb.setFontAndSize(dinBfb, 14);
							cb.setColorFill(BaseColor.BLACK);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str1.substring(0, 4) + "  " + str1.substring(4), 450, 780 - labelHeight,
									0);
							cb.endText();

							cb.beginText();
							cb.setFontAndSize(dinBfb, 20);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, str2, 505, 780 - labelHeight, 0);
							cb.endText();

							cb.beginText();
							cb.setFontAndSize(dinBfb, 14);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "-" + strArr[1], 545, 780 - labelHeight, 0);
							cb.endText();
						} else {
							cb.beginText();
							cb.setFontAndSize(dinBf, 20);
							cb.setColorFill(BaseColor.BLACK);
							cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getExternalDoNo(), 450, 780 - labelHeight, 0);
							cb.endText();
						}
					}

					// Package type
					if (deliveryOrderDetail.getPackageType() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 10);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getPackageType(), 100, 730 - labelHeight, 0);
						cb.endText();
					}

					// Label ID (“*0000000001*”)+ barcode #1
					if (labelId != 0) {
						cb.beginText();
						cb.setFontAndSize(dinBf, 10);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*00" + Integer.toString(labelId) + "*", 220, 789 - labelHeight, 0);
						cb.endText();

						cb.beginText();
						cb.setFontAndSize(barCodeBf, 24);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*00" + Integer.toString(labelId) + "*", 350, 784 - labelHeight, 0);
						cb.endText();
					}

					// Part Number
					if (deliveryOrderDetail.getItem().getCode() != null) {
						String partNubmer = deliveryOrderDetail.getItem().getCode();
						if (deliveryOrderDetail.getIndice() != null) {
							partNubmer += "-" + deliveryOrderDetail.getIndice().toString();
						}
						cb.beginText();
						cb.setFontAndSize(dinBf, 10);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, partNubmer, 513, 755 - labelHeight, 0);
						cb.endText();
					}

					// part description
					if (deliveryOrderDetail.getItemDescription() != null) {
						cb.beginText();
						cb.setFontAndSize(simBf, 6);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getItemDescription(), 510, 725 - labelHeight, 0);
						cb.endText();
					}

					// SEBANGO code + barcode #3
					if (deliveryOrderDetail.getSebango() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBfb, 70);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getSebango(), 320, 690 - labelHeight, 0);
						cb.endText();
					}
					if (deliveryOrderDetail.getItem().getCode() != null) {
						cb.beginText();
						cb.setFontAndSize(barCodeBf, 22);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*P" + deliveryOrderDetail.getItem().getCode() + "*", 320, 670 - labelHeight,
								0);
						cb.endText();
					}

					if (qty != BigDecimal.ZERO) {
						// Quantity (per box) + barcode #3
						cb.beginText();
						cb.setFontAndSize(dinBfb, 16);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, numberFormat.format(qty), 495, 672 - labelHeight, 0);
						cb.endText();

						cb.beginText();
						cb.setFontAndSize(barCodeBf, 22);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, "*" + numberFormat.format(qty) + "*", 540, 672 - labelHeight, 0);
						cb.endText();
					}

					// Storage Code
					if (deliveryOrderDetail.getStorageCode() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBfb, 26);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrderDetail.getStorageCode(), 320, 635 - labelHeight, 0);
						cb.endText();
					}

					// Dock
					if (deliveryOrder.getDock() != null) {
						cb.beginText();
						cb.setFontAndSize(dinBfb, 18);
						cb.showTextAligned(PdfContentByte.ALIGN_CENTER, deliveryOrder.getDock(), 495, 639 - labelHeight, 0);
						cb.endText();
					}
					labelHeight += 200;
					labelId++;
					totalBoxCount++;
				}
			}
		} finally {
			document.close();
			return new ByteArrayInputStream(outputStream.toByteArray());
		}
	}

}
