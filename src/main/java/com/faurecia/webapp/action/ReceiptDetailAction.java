package com.faurecia.webapp.action;

import java.io.ByteArrayInputStream;
import java.io.ByteArrayOutputStream;
import java.io.IOException;
import java.io.InputStream;
import java.nio.charset.Charset;
import java.text.DateFormat;
import java.text.SimpleDateFormat;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.ProjectionList;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;
import org.hibernate.transform.Transformers;

import com.faurecia.Constants;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.Receipt;
import com.faurecia.model.ReceiptDetail;
import com.faurecia.model.User;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ReceiptManager;
import com.faurecia.util.CSVWriter;
import com.faurecia.webapp.util.PaginatedListUtil;

public class ReceiptDetailAction extends BaseAction {

	/**
	 * 
	 */
	private static final long serialVersionUID = -9143046808174675490L;
	private ReceiptManager receiptManager;
	private PlantSupplierManager plantSupplierManager;
	private PaginatedListUtil<Receipt> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private Receipt receipt;
	private InputStream inputStream;
	private String fileName;

	public void setReceiptManager(ReceiptManager receiptManager) {
		this.receiptManager = receiptManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public PaginatedListUtil<Receipt> getPaginatedList() {
		return paginatedList;
	}

	public void setPaginatedList(PaginatedListUtil<Receipt> paginatedList) {
		this.paginatedList = paginatedList;
	}

	public int getPageSize() {
		return pageSize;
	}

	public void setPageSize(int pageSize) {
		this.pageSize = pageSize;
	}

	public int getPage() {
		return page;
	}

	public void setPage(int page) {
		this.page = page;
	}

	public String getSort() {
		return sort;
	}

	public void setSort(String sort) {
		this.sort = sort;
	}

	public String getDir() {
		return dir;
	}

	public void setDir(String dir) {
		this.dir = dir;
	}

	public Receipt getReceipt() {
		return receipt;
	}

	public void setReceipt(Receipt receipt) {
		this.receipt = receipt;
	}

	public List<PlantSupplier> getSuppliers() {
		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		return this.plantSupplierManager.getPlantSupplierByUserId(user.getId());
	}

	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}

	public String enter() {
		if (receipt == null) {
			receipt = new Receipt();

			Calendar calendar = Calendar.getInstance();
			calendar.setTime(new Date());
			calendar.set(Calendar.HOUR_OF_DAY, 0);
			calendar.set(Calendar.MINUTE, 0);
			calendar.set(Calendar.SECOND, 0);
			calendar.set(Calendar.MILLISECOND, 0);
			Date dateNow = calendar.getTime();
			calendar.add(Calendar.MONTH, -3);
			Date lastWeek = calendar.getTime();

			DateFormat d = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS");
			System.out.println(d.format(lastWeek));
			System.out.println(d.format(dateNow));

			receipt.setPostingDateFrom(lastWeek);
			receipt.setPostingDateTo(dateNow);
			// inboundLog.setInboundResult("fail");

			sort = "postingDate";
			dir = SortOrderEnum.DESCENDING.toString();

			receipt.setDetailOrSummary("Detail");
		}

		return SUCCESS;
	}

	public Map<String, String> getDetailOrSummary() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("Detail", "Detail");
		status.put("Summary", "Summary");
		return status;
	}

	public String cancel() {
		return "mainMenu";
	}

	public String list() {
		pageSize = pageSize == 0 ? 25 : pageSize;
		page = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<Receipt>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(ReceiptDetail.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(ReceiptDetail.class);

		selectCriteria.createAlias("receipt", "r");
		selectCriteria.createAlias("r.plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCriteria.createAlias("ps.supplier", "s");
		selectCriteria.createAlias("item", "i");
		selectCountCriteria.createAlias("receipt", "r");
		selectCountCriteria.createAlias("r.plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("ps.supplier", "s");
		selectCountCriteria.createAlias("item", "i");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		HttpServletRequest request = this.getRequest();

		if (request.isUserInRole(Constants.PLANT_USER_ROLE) || request.isUserInRole(Constants.PLANT_ADMIN_ROLE)) {
			selectCriteria.add(Restrictions.eq("ps.responsibleUser", user));
			selectCountCriteria.add(Restrictions.eq("ps.responsibleUser", user));
		} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
			if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
				return "mainMenu";
			}

			selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
		}

		if (receipt.getReceiptNo() != null && receipt.getReceiptNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("r.receiptNo", receipt.getReceiptNo().trim()));
			selectCountCriteria.add(Restrictions.like("r.receiptNo", receipt.getReceiptNo().trim()));
		}

		if (receipt.getItemCode() != null && receipt.getItemCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("i.code", receipt.getItemCode().trim()));
			selectCountCriteria.add(Restrictions.like("i.code", receipt.getItemCode().trim()));
		}

		if (receipt.getPlusMinus() != null && receipt.getPlusMinus().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("plusMinus", receipt.getPlusMinus().trim()));
			selectCountCriteria.add(Restrictions.eq("getPlusMinus", receipt.getPlusMinus().trim()));
		}

		if (receipt.getSupplierItemCode() != null && receipt.getSupplierItemCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("supplierItemCode", receipt.getSupplierItemCode().trim()));
			selectCountCriteria.add(Restrictions.like("supplierItemCode", receipt.getSupplierItemCode().trim()));
		}

		if (receipt.getReferenceOrderNo() != null && receipt.getReferenceOrderNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("referenceOrderNo", receipt.getReferenceOrderNo().trim()));
			selectCountCriteria.add(Restrictions.like("referenceOrderNo", receipt.getReferenceOrderNo().trim()));
		}

		if (receipt.getPostingDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("r.postingDate", receipt.getPostingDateFrom()));
			selectCountCriteria.add(Restrictions.ge("r.postingDate", receipt.getPostingDateFrom()));
		}

		if (receipt.getPostingDateTo() != null) {
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(receipt.getPostingDateTo());
			calendar.add(Calendar.DATE, 1);
			selectCriteria.add(Restrictions.lt("r.postingDate", calendar.getTime()));
			selectCountCriteria.add(Restrictions.lt("r.postingDate", calendar.getTime()));
		}

		if (receipt.getPlantSupplier() != null) {
			selectCriteria.add(Restrictions.eq("r.plantSupplier", receipt.getPlantSupplier()));
			selectCountCriteria.add(Restrictions.eq("r.plantSupplier", receipt.getPlantSupplier()));
		}

		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if ("desc".equals(dir)) {
				selectCriteria.addOrder(Order.desc(sort));
				paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
			} else {
				selectCriteria.addOrder(Order.asc(sort));
				paginatedList.setSortDirection(SortOrderEnum.ASCENDING);
			}
		}

		if ("Detail".equals(receipt.getDetailOrSummary())) {
			selectCountCriteria.setProjection(Projections.count("id"));
		} else {
			ProjectionList projectionList = Projections.projectionList().add(Projections.sum("qty").as("qty")).add(
					Projections.groupProperty("p.code").as("plantCode")).add(Projections.groupProperty("p.name").as("plantName")).add(
					Projections.groupProperty("s.code").as("supplierCode")).add(Projections.groupProperty("ps.supplierName").as("supplierName")).add(
					Projections.groupProperty("i.code").as("itemCode")).add(Projections.groupProperty("itemDescription").as("itemDescription")).add(
					Projections.groupProperty("supplierItemCode").as("supplierItemCode")).add(Projections.groupProperty("uom").as("uom"));

			selectCriteria.setProjection(projectionList);
			selectCriteria.setResultTransformer(Transformers.aliasToBean(ReceiptDetail.class));

			selectCountCriteria.setProjection(projectionList);
			selectCountCriteria.setResultTransformer(Transformers.aliasToBean(ReceiptDetail.class));
		}

		paginatedList.setList(this.receiptManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		if ("Detail".equals(receipt.getDetailOrSummary())) {
			paginatedList.setFullListSize(Integer.parseInt(this.receiptManager.findByCriteria(selectCountCriteria).get(0).toString()));
		} else {
			List list = this.receiptManager.findByCriteria(selectCountCriteria);
			if (list != null && list.size() > 0) {
				paginatedList.setFullListSize(list.size());
			} else {
				paginatedList.setFullListSize(0);
			}
		}

		return SUCCESS;
	}

	public String export() throws IOException {

		pageSize = pageSize == 0 ? 25 : pageSize;
		page = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<Receipt>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(ReceiptDetail.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(ReceiptDetail.class);

		selectCriteria.createAlias("receipt", "r");
		selectCriteria.createAlias("r.plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCriteria.createAlias("ps.supplier", "s");
		selectCriteria.createAlias("item", "i");
		selectCountCriteria.createAlias("receipt", "r");
		selectCountCriteria.createAlias("r.plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("ps.supplier", "s");
		selectCountCriteria.createAlias("item", "i");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		HttpServletRequest request = this.getRequest();

		if (request.isUserInRole(Constants.PLANT_USER_ROLE) || request.isUserInRole(Constants.PLANT_ADMIN_ROLE)) {
			selectCriteria.add(Restrictions.eq("ps.responsibleUser", user));
			selectCountCriteria.add(Restrictions.eq("ps.responsibleUser", user));
		} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
			if (this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE) == null) {
				return "mainMenu";
			}

			selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
		}

		if (receipt.getReceiptNo() != null && receipt.getReceiptNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("r.receiptNo", receipt.getReceiptNo().trim()));
			selectCountCriteria.add(Restrictions.like("r.receiptNo", receipt.getReceiptNo().trim()));
		}

		if (receipt.getItemCode() != null && receipt.getItemCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("i.code", receipt.getItemCode().trim()));
			selectCountCriteria.add(Restrictions.like("i.code", receipt.getItemCode().trim()));
		}

		if (receipt.getPlusMinus() != null && receipt.getPlusMinus().trim().length() > 0) {
			selectCriteria.add(Restrictions.eq("plusMinus", receipt.getPlusMinus().trim()));
			selectCountCriteria.add(Restrictions.eq("getPlusMinus", receipt.getPlusMinus().trim()));
		}

		if (receipt.getSupplierItemCode() != null && receipt.getSupplierItemCode().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("supplierItemCode", receipt.getSupplierItemCode().trim()));
			selectCountCriteria.add(Restrictions.like("supplierItemCode", receipt.getSupplierItemCode().trim()));
		}

		if (receipt.getReferenceOrderNo() != null && receipt.getReferenceOrderNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("referenceOrderNo", receipt.getReferenceOrderNo().trim()));
			selectCountCriteria.add(Restrictions.like("referenceOrderNo", receipt.getReferenceOrderNo().trim()));
		}

		if (receipt.getPostingDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("r.postingDate", receipt.getPostingDateFrom()));
			selectCountCriteria.add(Restrictions.ge("r.postingDate", receipt.getPostingDateFrom()));
		}

		if (receipt.getPostingDateTo() != null) {
			Calendar calendar = Calendar.getInstance();
			calendar.setTime(receipt.getPostingDateTo());
			calendar.add(Calendar.DATE, 1);
			selectCriteria.add(Restrictions.lt("r.postingDate", calendar.getTime()));
			selectCountCriteria.add(Restrictions.lt("r.postingDate", calendar.getTime()));
		}

		if (receipt.getPlantSupplier() != null) {
			selectCriteria.add(Restrictions.eq("r.plantSupplier", receipt.getPlantSupplier()));
			selectCountCriteria.add(Restrictions.eq("r.plantSupplier", receipt.getPlantSupplier()));
		}

		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if ("desc".equals(dir)) {
				selectCriteria.addOrder(Order.desc(sort));
				paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
			} else {
				selectCriteria.addOrder(Order.asc(sort));
				paginatedList.setSortDirection(SortOrderEnum.ASCENDING);
			}
		}

		if ("Detail".equals(receipt.getDetailOrSummary())) {
			selectCountCriteria.setProjection(Projections.count("id"));
		} else {
			ProjectionList projectionList = Projections.projectionList().add(Projections.sum("qty").as("qty")).add(
					Projections.groupProperty("p.code").as("plantCode")).add(Projections.groupProperty("p.name").as("plantName")).add(
					Projections.groupProperty("s.code").as("supplierCode")).add(Projections.groupProperty("ps.supplierName").as("supplierName")).add(
					Projections.groupProperty("i.code").as("itemCode")).add(Projections.groupProperty("itemDescription").as("itemDescription")).add(
					Projections.groupProperty("supplierItemCode").as("supplierItemCode")).add(Projections.groupProperty("uom").as("uom"));

			selectCriteria.setProjection(projectionList);
			selectCriteria.setResultTransformer(Transformers.aliasToBean(ReceiptDetail.class));

			selectCountCriteria.setProjection(projectionList);
			selectCountCriteria.setResultTransformer(Transformers.aliasToBean(ReceiptDetail.class));
		}
		
		List<ReceiptDetail> receiptDetailList = this.receiptManager.findByCriteria(selectCriteria);
		
		if (receiptDetailList != null && receiptDetailList.size() > 0) {
			DateFormat dateFormat = new SimpleDateFormat("MM/dd/yyyy");
			fileName = "receipt.csv";
			ByteArrayOutputStream outputStream = new ByteArrayOutputStream();
	
			CSVWriter writer = new CSVWriter(outputStream, ',', Charset.forName("GBK"));
			for(int i = 0; i < receiptDetailList.size(); i++) 
			{
				ReceiptDetail receiptDetail = receiptDetailList.get(i);
				if ("Detail".equals(receipt.getDetailOrSummary())) {
					String[] entries = new String[7];
					
					entries[0] =  receiptDetail.getReceipt().getReceiptNo();
					entries[1] =  receiptDetail.getItem() != null ? receiptDetail.getItem().getCode() : receiptDetail.getItemCode();
					entries[2] =  receiptDetail.getItemDescription();
					entries[3] =  receiptDetail.getSupplierItemCode();
					entries[4] =  receiptDetail.getUom();
					entries[5] =  receiptDetail.getQty().toString();
					entries[6] =  dateFormat.format(receiptDetail.getReceipt().getPostingDate());
					
					writer.writeRecord(entries);
				} else {
					String[] entries = new String[5];
					
					entries[0] =  receiptDetail.getItem() != null ? receiptDetail.getItem().getCode() : receiptDetail.getItemCode();
					entries[1] =  receiptDetail.getItemDescription();
					entries[2] =  receiptDetail.getSupplierItemCode();
					entries[3] =  receiptDetail.getUom();
					entries[4] =  receiptDetail.getQty().toString();
					
					writer.writeRecord(entries);
				}
			}
			writer.close();
			inputStream = new ByteArrayInputStream(outputStream.toByteArray());
		}
		
		paginatedList.setList(this.receiptManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		if ("Detail".equals(receipt.getDetailOrSummary())) {
			paginatedList.setFullListSize(Integer.parseInt(this.receiptManager.findByCriteria(selectCountCriteria).get(0).toString()));
		} else {
			List list = this.receiptManager.findByCriteria(selectCountCriteria);
			if (list != null && list.size() > 0) {
				paginatedList.setFullListSize(list.size());
			} else {
				paginatedList.setFullListSize(0);
			}
		}

		return SUCCESS;
	}
}
