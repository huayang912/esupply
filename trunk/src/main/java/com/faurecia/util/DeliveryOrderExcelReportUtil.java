package com.faurecia.util;

import java.math.BigDecimal;
import java.util.List;

import org.apache.poi.hssf.usermodel.HSSFCellStyle;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;

public class DeliveryOrderExcelReportUtil {

	public static final String EXCEL_TEMPLATE_URL = "template/Report/DeliveryOrderTemplate.xls";

	public static XlsExport generateReport(String localAbsolutPath, DeliveryOrder deliveryOrder) {
		XlsExport xls = loadReportTemlate(localAbsolutPath);

		ReportDataSet(xls, deliveryOrder);

		return xls;
	}

	private static XlsExport loadReportTemlate(String localAbsolutPath) {
		String templateUrl = localAbsolutPath + EXCEL_TEMPLATE_URL;
		XlsExport xls = new XlsExport(templateUrl);

		return xls;
	}

	private static void ReportDataSet(XlsExport xls, DeliveryOrder order) {

		xls.setRowCell(1, 17, order.getDoNo());
		xls.setRowCellStyle(1, 17, HSSFCellStyle.ALIGN_CENTER, "Arial", (short) 30);
		xls.setBorderTop(1, 17, HSSFCellStyle.BORDER_THICK);
		xls.setBorderLeft(1, 17, HSSFCellStyle.BORDER_THICK);
		 
		xls.setRowCell(5, 7, order.getSupplierName());
		xls.setRowCellStyle(5, 7, HSSFCellStyle.ALIGN_CENTER, "宋体", (short) 20);
		xls.setBorderTop(5, 7, HSSFCellStyle.BORDER_THIN);
		xls.setBorderBottom(5, 7, HSSFCellStyle.BORDER_THIN);

		xls.setRowCell(5, 28, order.getSupplierCode());
		xls.setRowCellStyle(5, 28, HSSFCellStyle.ALIGN_CENTER, "Arial", (short) 16);
		xls.setBorderTop(5, 28, HSSFCellStyle.BORDER_THIN);
		xls.setBorderBottom(5, 28, HSSFCellStyle.BORDER_THIN);

		if(order.getSupplierContactPerson() != null){
			xls.setRowCell(8, 5, order.getSupplierContactPerson());
			xls.setRowCellStyle(8, 5, HSSFCellStyle.ALIGN_CENTER, "宋体", (short)14);
			xls.setBorderTop(8,5, HSSFCellStyle.BORDER_THIN);
			xls.setBorderBottom(8,5, HSSFCellStyle.BORDER_THIN);
		}
		
		
		if(order.getSupplierPhone() != null){
			xls.setRowCell(8, 14, order.getSupplierPhone());
			xls.setRowCellStyle(8, 14, HSSFCellStyle.ALIGN_CENTER, "宋体", (short)14);
			xls.setBorderTop(8, 14, HSSFCellStyle.BORDER_THIN);
			xls.setBorderBottom(8, 14, HSSFCellStyle.BORDER_THIN);
		}
		
		
		if(order.getPlantContactPerson() != null)
		{
			xls.setRowCell(8, 25, order.getPlantContactPerson());
			xls.setRowCellStyle(8, 25, HSSFCellStyle.ALIGN_CENTER, "宋体", (short)14);
			xls.setBorderTop(8, 25, HSSFCellStyle.BORDER_THIN);
			xls.setBorderBottom(8, 25, HSSFCellStyle.BORDER_THIN);
		}
	
		
		if(order.getPlantPhone() != null)
		{
			xls.setRowCell(8, 35, order.getPlantPhone());
			xls.setRowCellStyle(8, 35, HSSFCellStyle.ALIGN_CENTER, "Arial", (short)14);
			xls.setBorderTop(8, 35, HSSFCellStyle.BORDER_THIN);
			xls.setBorderBottom(8, 35, HSSFCellStyle.BORDER_THIN);
		}
		
		
		// 到达日期，时间，

		if (order.getDeliveryOrderDetailList() != null) {
			for (int i = 0; i < order.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail orderDetail = order.getDeliveryOrderDetailList().get(i);
				xls.setRowCell(20 + i, 3, orderDetail.getItem().getCode());
				xls.setRowCellStyle(20 + i, 3, HSSFCellStyle.ALIGN_CENTER, "Arial", (short)14);

				xls.setRowCell(20 + i, 7, orderDetail.getItemDescription());
				//xls.setRowCellStyle(20 + i, 7, HSSFCellStyle.ALIGN_CENTER, "宋体", (short)14);
				
				
				if (orderDetail.getUnitCount() == null) {
					orderDetail.setUnitCount(new BigDecimal(1));
				}
				xls.setRowCell(20 + i, 16, orderDetail.getUnitCount().doubleValue());
			//	xls.setRowCellStyle(20 + i, 16, HSSFCellStyle.ALIGN_GENERAL, "Arial", (short) 14);

				xls.setRowCell(20 + i, 18, orderDetail.getOrderQty().divide(orderDetail.getUnitCount()).doubleValue());
			//	xls.setRowCellStyle(20 + i, 18, HSSFCellStyle.ALIGN_GENERAL, "Arial", (short) 14);

				xls.setRowCell(20 + i, 21, orderDetail.getOrderQty().doubleValue());
			//	xls.setRowCellStyle(20 + i, 21, HSSFCellStyle.ALIGN_GENERAL, "Arial", (short) 12);

				xls.setRowCell(20 + i, 24, orderDetail.getQty().doubleValue());
			//	xls.setRowCellStyle(20 + i, 24, HSSFCellStyle.ALIGN_GENERAL, "Arial", (short) 12);

			}
		}

	}
}
