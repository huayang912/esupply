package com.faurecia.util;

import java.awt.image.BufferedImage;
import java.io.ByteArrayOutputStream;
import java.io.File;
import java.io.FileInputStream;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;
import java.io.IOException;
import java.util.Calendar;

import javax.imageio.ImageIO;
import javax.servlet.http.HttpServletResponse;

import org.apache.poi.hssf.usermodel.HSSFCell;
import org.apache.poi.hssf.usermodel.HSSFCellStyle;
import org.apache.poi.hssf.usermodel.HSSFClientAnchor;
import org.apache.poi.hssf.usermodel.HSSFDataFormat;
import org.apache.poi.hssf.usermodel.HSSFFont;
import org.apache.poi.hssf.usermodel.HSSFPatriarch;
import org.apache.poi.hssf.usermodel.HSSFPrintSetup;
import org.apache.poi.hssf.usermodel.HSSFRow;
import org.apache.poi.hssf.usermodel.HSSFSheet;
import org.apache.poi.hssf.usermodel.HSSFSimpleShape;
import org.apache.poi.hssf.usermodel.HSSFWorkbook;
import org.apache.poi.hssf.util.HSSFColor;
import org.apache.poi.hssf.util.Region;

public class XlsExport {
	// ����cell���������ĸ�λ�ֽڽض�
	private static short XLS_ENCODING = HSSFWorkbook.ENCODING_UTF_16;

	// �������ڸ�ʽ
	private static String DATE_FORMAT = " m/d/yy ";

	// ���Ƹ�������ʽ
	private static String NUMBER_FORMAT = " #,##0.00 ";

	private String xlsFileName;

	private HSSFWorkbook workbook;

	private HSSFSheet sheet;

	private HSSFRow row;

	private HSSFCellStyle style;

	private HSSFFont font;

	/** */
	/**
	 * ��ʼ��Excel
	 * @param fileName
	 * �����ļ���
	 */
	public XlsExport() {
		this.workbook = new HSSFWorkbook();
		this.sheet = workbook.createSheet();
	}

	public XlsExport(String fileName) {
		this.xlsFileName = fileName;

		try {
			FileInputStream filein = new FileInputStream(fileName);
			this.workbook = new HSSFWorkbook(filein);
			this.sheet = workbook.getSheetAt(0);
		} catch (FileNotFoundException e) {
			e.printStackTrace();
		} catch (IOException e) {
			e.printStackTrace();
		}
	}

	/** */
	/**
	 * ����Excel�ļ�
	 * 
	 * @throws XLSException
	 */
	public void exportXLS() throws Exception {
		try {
			FileOutputStream fOut = new FileOutputStream(xlsFileName);
			workbook.write(fOut);
			fOut.flush();
			fOut.close();
		} catch (FileNotFoundException e) {
			throw new FileNotFoundException(" ���ɵ���Excel�ļ�����! ");
		} catch (IOException e) {
			throw new IOException(" д��Excel�ļ�����! ");
		}

	}

	/**
	 * ����excel��ָ��Ŀ¼
	 * 
	 * @param FileName
	 * @throws Exception
	 */
	public void exportXLS(String FileName) throws Exception {
		try {
			FileOutputStream fOut = new FileOutputStream(FileName);
			workbook.write(fOut);
			fOut.flush();
			fOut.close();
		} catch (FileNotFoundException e) {
			throw new FileNotFoundException(" ���ɵ���Excel�ļ�����! ");
		} catch (IOException e) {
			throw new IOException(" д��Excel�ļ�����! ");
		}

	}

	/**
	 * ����excel��������ͻ����û�
	 * 
	 * @param response
	 */
	public void exportToResponse(HttpServletResponse response, String downloadFileName) throws IOException {
		
		response.reset();
		response.setContentType("application/octet-stream");
		
		response.setHeader("Content-Disposition", "attachment;filename=\"" + downloadFileName + "\"");
		// д��Excel������
		workbook.write(response.getOutputStream());
		response.getOutputStream().flush();
		// �ر�Excel����������
		response.getOutputStream().close();
		response.setStatus(HttpServletResponse.SC_OK);
		response.flushBuffer();

	}

	/** */
	/**
	 * ����һ��
	 * 
	 * @param index
	 * �к�
	 */
	public void createRow(int index) {
		this.row = this.sheet.createRow(index);
	}

	public void setRowHeight(int index, short height) {
		this.row = this.sheet.getRow(index);
		if (this.row == null)
			this.row = this.sheet.createRow(index);
		this.row.setHeight((short) height);
	}

	public void setRowCellStyle(int RowIndex, int CellIndex, short cellStyle, String fontName, short fontSize) {
		this.setRowCellStyle(RowIndex, CellIndex, HSSFFont.BOLDWEIGHT_BOLD, cellStyle, fontName, fontSize);
	}

	public void setRowCellStyle(int RowIndex, int CellIndex, short bold, short cellStyle, String fontName, short fontSize) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		cell.setCellType(HSSFCell.CELL_TYPE_STRING);
		font.setFontHeightInPoints((short) fontSize);
		font.setBoldweight(bold);
		font.setFontName(fontName);
		style.setFont(font);
		style.setAlignment(cellStyle);
		cell.setCellStyle(style);
	}

	public void setRowCell(int RowIndex, int CellIndex, String value) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		cell.setCellType(HSSFCell.CELL_TYPE_STRING);
		font = workbook.createFont();
		font.setFontHeightInPoints((short) 8);
		font.setFontName("����");
		style = workbook.createCellStyle();
		style.setFont(font);
		cell.setCellStyle(style);
		cell.setEncoding(XLS_ENCODING);
		cell.setCellValue(value);
	}

	public void setRowCell(int RowIndex, int CellIndex, double value) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		cell.setCellType(HSSFCell.CELL_TYPE_NUMERIC);
		font.setFontHeightInPoints((short) 8);
		font.setFontName("����");
		style.setFont(font);
		cell.setCellStyle(style);
		cell.setEncoding(XLS_ENCODING);
		cell.setCellValue(value);
	}

	public void setRowCellRightStyle(int RowIndex, int CellIndex, String value) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		cell.setCellType(HSSFCell.CELL_TYPE_STRING);
		font.setFontHeightInPoints((short) 8);
		font.setFontName("����");
		style.setFont(font);
		style.setAlignment(HSSFCellStyle.ALIGN_RIGHT);
		cell.setCellStyle(style);
		cell.setEncoding(XLS_ENCODING);
		cell.setCellValue(value);
	}

	public void getRowCell(int RowIndex, int CellIndex) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		style = sheet.getRow(RowIndex).getCell((short) CellIndex).getCellStyle();
		String value = sheet.getRow(RowIndex).getCell((short) CellIndex).getStringCellValue();

		cell.setCellType(HSSFCell.CELL_TYPE_STRING);
		cell.setCellValue(value);
		cell.setCellStyle(style);
	}

	public HSSFCell getCell(int RowIndex, int CellIndex) {
		return this.sheet.getRow(RowIndex).getCell((short) CellIndex);
	}

	public HSSFCellStyle getCellStyle(int RowIndex, int CellIndex) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		style = sheet.getRow(RowIndex).getCell((short) CellIndex).getCellStyle();

		return style;
	}

	public void setBorderBottom(int RowIndex, int CellIndex, int type) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		style.setBorderBottom((short) type);// �±߿�
		style.setBottomBorderColor(HSSFColor.BLACK.index);
		cell.setCellStyle(style);
	}

	public void setBorderLeft(int RowIndex, int CellIndex, int type) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		style.setBorderLeft((short) type);// ��߿�
		style.setBottomBorderColor(HSSFColor.BLACK.index);
		cell.setCellStyle(style);
	}

	public void setBorderRight(int RowIndex, int CellIndex, int type) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		style.setBorderRight((short) type);// �ұ߿�
		style.setBottomBorderColor(HSSFColor.BLACK.index);
		cell.setCellStyle(style);
	}

	public void setBorderTop(int RowIndex, int CellIndex, int type) {
		this.row = this.sheet.getRow(RowIndex);
		if (this.row == null)
			this.row = this.sheet.createRow(RowIndex);
		HSSFCell cell = this.row.getCell((short) CellIndex);
		if (cell == null)
			cell = this.row.createCell((short) CellIndex);
		style.setBorderTop((short) type);// �ϱ߿�
		style.setBottomBorderColor(HSSFColor.BLACK.index);
		cell.setCellStyle(style);
	}

	public void copyCellStyle(HSSFCell cellFrom, HSSFCell cellTo) {
		cellTo.setCellStyle(cellFrom.getCellStyle());
	}

	public void copyCellRichString(HSSFCell cellFrom, HSSFCell cellTo) {
		cellTo.setCellValue(cellFrom.getRichStringCellValue());
	}

	/**
	 * column ���� width ���
	 */
	public void setColumnWidth(short column, short width) {
		sheet.setColumnWidth(column, width);
	}

	// HSSFPrintSetup ps = sheet.getPrintSetup();
	// sheet.setAutobreaks(true);
	// ps.setFitHeight((short)1);
	// ps.setFitWidth((short)1);

	/**
	 * ���õ�Ԫ��
	 * 
	 * @param index
	 *            �к�
	 * @param value
	 *            ��Ԫ�����ֵ
	 */
	public void setCell(int index, String value) {
		HSSFCell cell = this.row.createCell((short) index);
		cell.setCellType(HSSFCell.CELL_TYPE_STRING);
		font = workbook.createFont();
		font.setFontHeightInPoints((short) 8);
		font.setFontName("����");
		style = workbook.createCellStyle();
		style.setFont(font);
		cell.setCellStyle(style);
		cell.setEncoding(XLS_ENCODING);
		cell.setCellValue(value);
	}

	/** */
	/**
	 * ���õ�Ԫ��
	 * 
	 * @param index
	 *            �к�
	 * @param value
	 *            ��Ԫ�����ֵ
	 */
	public void setCell(int index, Calendar value) {
		HSSFCell cell = this.row.createCell((short) index);
		cell.setEncoding(XLS_ENCODING);
		cell.setCellValue(value.getTime());
		HSSFCellStyle cellStyle = workbook.createCellStyle(); // �����µ�cell��ʽ
		cellStyle.setDataFormat(HSSFDataFormat.getBuiltinFormat(DATE_FORMAT)); // ����cell��ʽΪ���Ƶ����ڸ�ʽ
		cell.setCellStyle(cellStyle); // ���ø�cell���ڵ���ʾ��ʽ
	}

	public void setStyle(int RowIndex, int CellIndex) {
		style = sheet.getRow(RowIndex).getCell((short) CellIndex).getCellStyle();
	}

	/** */
	/**
	 * ���õ�Ԫ��
	 * 
	 * @param index
	 *            �к�
	 * @param value
	 *            ��Ԫ�����ֵ
	 */
	public void setCell(int index, int value) {
		HSSFCell cell = this.row.createCell((short) index);
		cell.setCellType(HSSFCell.CELL_TYPE_NUMERIC);
		font = workbook.createFont();
		font.setFontHeightInPoints((short) 8);
		font.setFontName("����");
		style = workbook.createCellStyle();
		style.setFont(font);
		cell.setCellStyle(style);
		cell.setEncoding(XLS_ENCODING);
		cell.setCellValue(value);
	}

	/** */
	/**
	 * ���õ�Ԫ��
	 * 
	 * @param index
	 *            �к�
	 * @param value
	 *            ��Ԫ�����ֵ
	 */
	public void setCell(int index, double value) {
		HSSFCell cell = this.row.createCell((short) index);
		cell.setCellType(HSSFCell.CELL_TYPE_NUMERIC);
		cell.setCellValue(value);
		HSSFCellStyle cellStyle = workbook.createCellStyle(); // �����µ�cell��ʽ
		HSSFDataFormat format = workbook.createDataFormat();
		cellStyle.setDataFormat(format.getFormat(NUMBER_FORMAT)); // ����cell��ʽΪ���Ƶĸ�������ʽ
		cell.setCellStyle(cellStyle); // ���ø�cell����������ʾ��ʽ
	}

	
	public void setMergedRegion(int row1, int column1, int row2, int colunm2) {
		this.sheet.addMergedRegion(new Region(row1, (short) (column1), row2, (short) (colunm2)));
	}

	
	public void printSetup() {

		// ������ӡ���ö���
		HSSFPrintSetup hps = this.sheet.getPrintSetup();
		hps.setHeaderMargin(0.5);
		hps.setFooterMargin(0.5);
		sheet.setMargin(HSSFSheet.BottomMargin, (double) 0.5);
		sheet.setMargin(HSSFSheet.LeftMargin, (double) 0.1);
		sheet.setMargin(HSSFSheet.RightMargin, (double) 0.1);
		sheet.setMargin(HSSFSheet.TopMargin, (double) 0.5);

		// ����A4ֽ
		hps.setPaperSize((short) 9);
		
		sheet.setVerticallyCenter(true);
	}
}
