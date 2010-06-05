package com.faurecia.util;

import org.apache.poi.hssf.usermodel.HSSFCellStyle;

import com.faurecia.model.DeliveryOrder;

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
		if (order.getSupplierName() != null) {
			xls.setRowCell(5, 7, order.getSupplierName());
		}
	}
}
