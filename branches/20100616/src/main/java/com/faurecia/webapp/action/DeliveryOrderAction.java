package com.faurecia.webapp.action;

import java.io.InputStream;
import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import javax.servlet.http.HttpServletRequest;

import org.apache.commons.beanutils.BeanUtils;
import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.Constants;
import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.model.Schedule;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.User;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.util.DeliveryOrderExcelReportUtil;
import com.faurecia.util.XlsExport;
import com.faurecia.webapp.util.PaginatedListUtil;

import freemarker.template.utility.StringUtil;

public class DeliveryOrderAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = -5177104341924305525L;
	private DeliveryOrderManager deliveryOrderManager;
	private GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager;
	private PlantSupplierManager plantSupplierManager;
	private ScheduleManager scheduleManager;
	private GenericManager<ScheduleItemDetail, Integer> scheduleItemDetailManager;

	private PaginatedListUtil<DeliveryOrder> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private DeliveryOrder deliveryOrder;
	private String doNo;

	private String poNo;
	private List<PurchaseOrderDetail> purchaseOrderDetailList;

	private List<DeliveryOrderDetail> deliveryOrderDetailList;

	private Integer plantSupplierId;
	private Date dateFrom;
	private String scheduleType;
	private InputStream inputStream;
	private String fileName;

	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}

	public void setPurchaseOrderDetailManager(GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager) {
		this.purchaseOrderDetailManager = purchaseOrderDetailManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setScheduleManager(ScheduleManager scheduleManager) {
		this.scheduleManager = scheduleManager;
	}

	public void setScheduleItemDetailManager(GenericManager<ScheduleItemDetail, Integer> scheduleItemDetailManager) {
		this.scheduleItemDetailManager = scheduleItemDetailManager;
	}

	public PaginatedListUtil<DeliveryOrder> getPaginatedList() {
		return paginatedList;
	}

	public void setPaginatedList(PaginatedListUtil<DeliveryOrder> paginatedList) {
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

	public Map<String, String> getStatus() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("Create", "Create");
		status.put("Confirm", "Confirm");
		return status;
	}

	public DeliveryOrder getDeliveryOrder() {
		return deliveryOrder;
	}

	public void setDeliveryOrder(DeliveryOrder deliveryOrder) {
		this.deliveryOrder = deliveryOrder;
	}

	public String getDoNo() {
		return doNo;
	}

	public void setDoNo(String doNo) {
		this.doNo = doNo;
	}

	public String getPoNo() {
		return poNo;
	}

	public void setPoNo(String poNo) {
		this.poNo = poNo;
	}

	public Integer getPlantSupplierId() {
		return plantSupplierId;
	}

	public void setPlantSupplierId(Integer plantSupplierId) {
		this.plantSupplierId = plantSupplierId;
	}

	public Date getDateFrom() {
		return dateFrom;
	}

	public void setDateFrom(Date dateFrom) {
		this.dateFrom = dateFrom;
	}

	public String getScheduleType() {
		return scheduleType;
	}

	public void setScheduleType(String scheduleType) {
		this.scheduleType = scheduleType;
	}

	public List<PurchaseOrderDetail> getPurchaseOrderDetailList() {
		return purchaseOrderDetailList;
	}

	public void setPurchaseOrderDetailList(List<PurchaseOrderDetail> purchaseOrderDetailList) {
		this.purchaseOrderDetailList = purchaseOrderDetailList;
	}

	public List<DeliveryOrderDetail> getDeliveryOrderDetailList() {
		return deliveryOrderDetailList;
	}

	public void setDeliveryOrderDetailList(List<DeliveryOrderDetail> deliveryOrderDetailList) {
		this.deliveryOrderDetailList = deliveryOrderDetailList;
	}
	
	public String delete() {
		this.deliveryOrderManager.remove(deliveryOrder.getDoNo());
		saveMessage(getText("deliveryOrder.deleted"));
		return SUCCESS;
	}

	public String list() {
		if (deliveryOrder == null) {
			deliveryOrder = new DeliveryOrder();
		}

		pageSize = pageSize == 0 ? 25 : pageSize;
		page = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<DeliveryOrder>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(DeliveryOrder.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(DeliveryOrder.class).setProjection(Projections.count("doNo"));

		selectCriteria.createAlias("plantSupplier", "ps");
		selectCriteria.createAlias("ps.plant", "p");
		selectCriteria.createAlias("ps.supplier", "s");
		selectCountCriteria.createAlias("plantSupplier", "ps");
		selectCountCriteria.createAlias("ps.plant", "p");
		selectCountCriteria.createAlias("ps.supplier", "s");

		String userCode = this.getRequest().getRemoteUser();
		User user = this.userManager.getUserByUsername(userCode);

		HttpServletRequest request = this.getRequest();

		if (request.isUserInRole(Constants.PLANT_USER_ROLE) || request.isUserInRole(Constants.PLANT_ADMIN_ROLE)) {
			selectCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
			selectCountCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
		} else if (request.isUserInRole(Constants.VENDOR_ROLE)) {
			selectCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCountCriteria.add(Restrictions.eq("p.code", this.getSession().getAttribute(Constants.SUPPLIER_PLANT_CODE)));
			selectCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
			selectCountCriteria.add(Restrictions.eq("ps.supplier", user.getUserSupplier()));
		}

		if (deliveryOrder.getDoNo() != null && deliveryOrder.getDoNo().trim().length() > 0) {
			selectCriteria.add(Restrictions.like("poNo", deliveryOrder.getDoNo().trim()));
			selectCountCriteria.add(Restrictions.like("poNo", deliveryOrder.getDoNo().trim()));
		}

		if (deliveryOrder.getCreateDateFrom() != null) {
			selectCriteria.add(Restrictions.ge("createDate", deliveryOrder.getCreateDateFrom()));
			selectCountCriteria.add(Restrictions.ge("createDate", deliveryOrder.getCreateDateFrom()));
		}

		if (deliveryOrder.getCreateDateTo() != null) {
			selectCriteria.add(Restrictions.le("createDate", deliveryOrder.getCreateDateTo()));
			selectCountCriteria.add(Restrictions.le("createDate", deliveryOrder.getCreateDateTo()));
		}

		if (deliveryOrder.getCreateDateTo() != null) {
			selectCriteria.add(Restrictions.le("createDate", deliveryOrder.getCreateDateTo()));
			selectCountCriteria.add(Restrictions.le("createDate", deliveryOrder.getCreateDateTo()));
		}

		if (deliveryOrder.getStatus() != null && deliveryOrder.getStatus().trim().length() != 0) {
			selectCriteria.add(Restrictions.eq("status", deliveryOrder.getStatus()));
			selectCountCriteria.add(Restrictions.eq("status", deliveryOrder.getStatus()));
		}

		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if (SortOrderEnum.DESCENDING.equals(dir)) {
				selectCriteria.addOrder(Order.desc(sort));
				paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
			} else {
				selectCriteria.addOrder(Order.asc(sort));
				paginatedList.setSortDirection(SortOrderEnum.ASCENDING);
			}
		}

		paginatedList.setList(this.deliveryOrderManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		paginatedList.setFullListSize(Integer.parseInt(this.deliveryOrderManager.findByCriteria(selectCountCriteria).get(0).toString()));

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		if (this.doNo != null) {
			deliveryOrder = this.deliveryOrderManager.get(doNo, true);
		} else if (purchaseOrderDetailList != null) {

			// po ���� do
			boolean hasError = false;
			List<PurchaseOrderDetail> noneZeroPurchaseOrderDetailList = new ArrayList<PurchaseOrderDetail>();
			for (int i = 1; i < purchaseOrderDetailList.size(); i++) {
				PurchaseOrderDetail purchaseOrderDetail = this.purchaseOrderDetailManager.get(purchaseOrderDetailList.get(i).getId());
				BigDecimal currentShipQty = purchaseOrderDetailList.get(i).getCurrentShipQty();
				purchaseOrderDetail.setCurrentShipQty(currentShipQty);

				if (poNo == null) {
					poNo = purchaseOrderDetail.getPurchaseOrder().getPoNo();
				}

				if ((purchaseOrderDetail.getShipQty() != null && currentShipQty.add(purchaseOrderDetail.getShipQty()).compareTo(
						purchaseOrderDetail.getQty()) > 0)
						|| (purchaseOrderDetail.getShipQty() == null && currentShipQty.compareTo(purchaseOrderDetail.getQty()) > 0)) {
					saveMessage(getText("errors.purchaseOrder.shipQtyExcceed"));	
					hasError = true;
				}

				noneZeroPurchaseOrderDetailList.add(purchaseOrderDetail);
			}

			if (!hasError) {
				deliveryOrder = this.deliveryOrderManager.createDeliveryOrder(noneZeroPurchaseOrderDetailList);
				saveMessage(getText("deliveryOrder.added"));
			} else {				
				return "poInput";
			}
		} else if (plantSupplierId != null && plantSupplierId > 0) {

			// sa ���� do
			PlantSupplier plantSupplier = this.plantSupplierManager.get(plantSupplierId);
			Schedule schedule = this.scheduleManager
					.getLastestScheduleItem(plantSupplier.getPlant().getCode(), plantSupplier.getSupplier().getCode());
			PlantScheduleGroup plantScheduleGroup = plantSupplier.getPlantScheduleGroup();
			Boolean allowOverQty = plantScheduleGroup != null ? plantScheduleGroup.getAllowOverQtyDeliver() : false;

			int sequence = 1;
			for (int i = 0; i < schedule.getScheduleItemList().size(); i++) {
				ScheduleItem scheduleItem = schedule.getScheduleItemList().get(i);

				for (int j = 0; j < scheduleItem.getScheduleItemDetailList().size(); j++) {
					ScheduleItemDetail scheduleItemDetail = scheduleItem.getScheduleItemDetailList().get(j);

					if (scheduleItemDetail.getDateFrom().compareTo(dateFrom) == 0
							&& (scheduleItemDetail.getScheduleType().equals(scheduleType) || (scheduleType.startsWith("Backlog") && scheduleItemDetail
									.getScheduleType().startsWith("Backlog")))) {
						if (deliveryOrder == null) {
							deliveryOrder = new DeliveryOrder();
							BeanUtils.copyProperties(deliveryOrder, schedule);

							deliveryOrder.setCreateDate(new Date());
							deliveryOrder.setIsExport(false);
							deliveryOrder.setAllowOverQty(allowOverQty);
						}

						if ((scheduleItemDetail.getRemainQty() != null && scheduleItemDetail.getRemainQty().compareTo(BigDecimal.ZERO) > 0)
								|| (plantSupplier.getPlantScheduleGroup() != null && plantSupplier.getPlantScheduleGroup().getAllowOverQtyDeliver())) {
							DeliveryOrderDetail deliveryOrderDetail = new DeliveryOrderDetail();
							deliveryOrderDetail.setDeliveryOrder(deliveryOrder);

							deliveryOrderDetail.setSequence(StringUtil.leftPad(String.valueOf(sequence++ * 10), 4, '0'));
							deliveryOrderDetail.setItem(scheduleItem.getItem());
							deliveryOrderDetail.setItemDescription(scheduleItem.getItemDescription());
							deliveryOrderDetail.setSupplierItemCode(scheduleItem.getSupplierItemCode() != null ? scheduleItem.getSupplierItemCode()
									: "");
							deliveryOrderDetail.setUnitCount(scheduleItem.getItem().getUnitCount());
							deliveryOrderDetail.setUom(scheduleItem.getUom());
							deliveryOrderDetail.setScheduleItemDetail(scheduleItemDetail);
							deliveryOrderDetail.setQty(scheduleItemDetail.getRemainQty());
							// deliveryOrderDetail.setOrderQty(scheduleItemDetail.getReleaseQty());
							deliveryOrderDetail.setReferenceOrderNo(scheduleItem.getSchedule().getScheduleNo());
							deliveryOrderDetail.setReferenceSequence(scheduleItem.getSequence());
							deliveryOrder.addDeliveryOrderDetail(deliveryOrderDetail);
						}
					}
				}
			}
		} else if (deliveryOrder != null) {

			boolean hasError = false;
			if (deliveryOrderDetailList != null) {
				for (int i = 1; i < deliveryOrderDetailList.size(); i++) {
					DeliveryOrderDetail deliveryOrderDetail = deliveryOrderDetailList.get(i);
					deliveryOrderDetail
							.setScheduleItemDetail(this.scheduleItemDetailManager.get(deliveryOrderDetail.getScheduleItemDetail().getId()));
					BigDecimal currentQty = deliveryOrderDetail.getQty();
					BigDecimal deliverQty = deliveryOrderDetail.getDeliverQty();

					BigDecimal totalDeliverQty = currentQty;
					if (deliverQty != null) {
						totalDeliverQty = totalDeliverQty.add(deliverQty);
					}

					if (deliveryOrderDetail.getOrderQty().compareTo(totalDeliverQty) < 0 && !deliveryOrder.getAllowOverQty()) {
						saveMessage(getText("errors.deliveryOrder.shipQtyExcceed"));
						hasError = true;						
					}

					deliveryOrder.addDeliveryOrderDetail(deliveryOrderDetail);
				}
			}
			
			if (!hasError){
				deliveryOrder = this.deliveryOrderManager.createScheduleDeliveryOrder(deliveryOrder);
				saveMessage(getText("deliveryOrder.added"));
			}
		}

		return SUCCESS;
	}

	public String save() {
		if (!collectDevlieryOrder()) {
			this.deliveryOrderManager.save(deliveryOrder);
			saveMessage(getText("deliveryOrder.updated"));
		} 
		
		return SUCCESS;
	}

	public String confirm() {
		if (!collectDevlieryOrder()) {
			
			boolean allZero = true;
			for(int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);
				if (deliveryOrderDetail.getQty().compareTo(BigDecimal.ZERO) > 0) {
					allZero = false;
				}
			}
			
			if (allZero) {
				saveMessage(getText("errors.deliveryOrder.createDo.emptyDetail"));
			} else {
				this.deliveryOrderManager.confirm(deliveryOrder);
				saveMessage(getText("deliveryOrder.confirmed"));
			}
		} 
		
		return SUCCESS;
	}

	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}

	public String print() throws Exception {
		String localAbsolutPath = this.getSession().getServletContext().getRealPath("/");
		deliveryOrder = this.deliveryOrderManager.get(deliveryOrder.getDoNo(), true);
		XlsExport report = DeliveryOrderExcelReportUtil.generateReport(localAbsolutPath, deliveryOrder);

		fileName = "deliveryOrder_" + deliveryOrder.getDoNo() + ".xls";
		inputStream = report.exportToInputStream(fileName);
		return SUCCESS;
	}
	
	private boolean collectDevlieryOrder() {
		deliveryOrder = this.deliveryOrderManager.get(deliveryOrder.getDoNo(), true);

		boolean allowOverQtyDeliver = false;
		if (deliveryOrder.getPlantSupplier().getPlantScheduleGroup() != null) {
			allowOverQtyDeliver = deliveryOrder.getPlantSupplier().getPlantScheduleGroup().getAllowOverQtyDeliver();
		}

		boolean hasError = false;
		if (deliveryOrderDetailList != null) {

			for (int i = 1; i < deliveryOrderDetailList.size(); i++) {

				for (int j = 0; j < deliveryOrder.getDeliveryOrderDetailList().size(); j++) {
					DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(j);
					if (deliveryOrderDetail.getId().equals(deliveryOrderDetailList.get(i).getId())) {
						deliveryOrderDetail.setQty(deliveryOrderDetailList.get(i).getQty());

						if (!allowOverQtyDeliver) {
							BigDecimal deliverQty = deliveryOrderDetail.getDeliverQty();
							if (deliverQty == null) {
								deliverQty = BigDecimal.ZERO;
							}

							if (deliverQty.add(deliveryOrderDetail.getQty()).compareTo(deliveryOrderDetail.getOrderQty()) > 0) {								
								saveMessage(getText("errors.deliveryOrder.shipQtyExcceed"));
								hasError = true;
							}
						}
						break;
					}
				}
			}
		}
		
		return hasError;
	}
}