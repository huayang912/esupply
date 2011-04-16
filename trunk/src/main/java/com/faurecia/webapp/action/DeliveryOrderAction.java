package com.faurecia.webapp.action;

import java.io.InputStream;
import java.math.BigDecimal;
import java.util.ArrayList;
import java.util.Calendar;
import java.util.Date;
import java.util.HashMap;
import java.util.List;
import java.util.Map;

import org.apache.commons.beanutils.BeanUtils;
import org.displaytag.properties.SortOrderEnum;
import org.hibernate.criterion.DetachedCriteria;
import org.hibernate.criterion.Order;
import org.hibernate.criterion.Projections;
import org.hibernate.criterion.Restrictions;

import com.faurecia.model.DeliveryOrder;
import com.faurecia.model.DeliveryOrderDetail;
import com.faurecia.model.Plant;
import com.faurecia.model.PlantScheduleGroup;
import com.faurecia.model.PlantSupplier;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.model.Schedule;
import com.faurecia.model.ScheduleItem;
import com.faurecia.model.ScheduleItemDetail;
import com.faurecia.model.Supplier;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.service.PlantSupplierManager;
import com.faurecia.service.ScheduleManager;
import com.faurecia.service.SupplierManager;
import com.faurecia.util.DeliveryOrderExportUtil;
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
	private SupplierManager supplierManager;

	private PaginatedListUtil<DeliveryOrder> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private DeliveryOrder deliveryOrder;
	private String doNo;
	private String fileIdentitfier;
	private Integer deliveryOrderDetailId;

	private String poNo;
	private List<PurchaseOrderDetail> purchaseOrderDetailList;

	private List<DeliveryOrderDetail> deliveryOrderDetailList;

	private Integer plantSupplierId;
	private Date dateFrom;
	private String scheduleType;
	private InputStream inputStream;
	private String fileName;
	private Boolean isLogisticPartner;

	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}

	public void setPurchaseOrderDetailManager(GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager) {
		this.purchaseOrderDetailManager = purchaseOrderDetailManager;
	}

	public void setPlantSupplierManager(PlantSupplierManager plantSupplierManager) {
		this.plantSupplierManager = plantSupplierManager;
	}

	public void setSupplierManager(SupplierManager supplierManager) {
		this.supplierManager = supplierManager;
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

	public Boolean getIsLogisticPartner() {
		return isLogisticPartner;
	}

	public void setSort(Boolean isLogisticPartner) {
		this.isLogisticPartner = isLogisticPartner;
	}

	public String getDir() {
		return dir;
	}

	public void setDir(String dir) {
		this.dir = dir;
	}

	public String getFileIdentitfier() {
		return fileIdentitfier;
	}

	public void setFileIdentitfier(String fileIdentitfier) {
		this.fileIdentitfier = fileIdentitfier;
	}

	public Map<String, String> getStatus() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("Create", "Create");
		status.put("Confirm", "Confirm");
		return status;
	}

	public Map<String, String> getIsExport() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("No", "false");
		status.put("Yes", "true");
		return status;
	}

	public Map<String, String> getIsRead() {
		Map<String, String> status = new HashMap<String, String>();
		status.put("", "All");
		status.put("No", "false");
		status.put("Yes", "true");
		return status;
	}

	public List<Supplier> getSuppliers() {
		return this.supplierManager.getAuthorizedSupplier(this.getRequest().getRemoteUser());
	}

	public List<Plant> getPlants() {
		return this.plantSupplierManager.getAuthorizedPlant(this.getRequest().getRemoteUser());
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

	public int getDeliveryOrderDetailId() {
		return deliveryOrderDetailId;
	}

	public void setDeliveryOrderDetailId(int deliveryOrderDetailId) {
		this.deliveryOrderDetailId = deliveryOrderDetailId;
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

	@SuppressWarnings("unchecked")
	public String list() {
		if (deliveryOrder != null) {
			/*
			 * Calendar calendar = Calendar.getInstance(); calendar.setTime(new
			 * Date()); calendar.set(Calendar.HOUR_OF_DAY, 0);
			 * calendar.set(Calendar.MINUTE, 0); calendar.set(Calendar.SECOND,
			 * 0); calendar.set(Calendar.MILLISECOND, 0); Date dateNow =
			 * calendar.getTime(); calendar.add(Calendar.MONTH, -1); Date
			 * lastWeek = calendar.getTime();
			 */

			// DateFormat d = new SimpleDateFormat("yyyy-MM-dd HH:mm:ss SSS");

			// deliveryOrder.setCreateDateFrom(lastWeek);
			// deliveryOrder.setCreateDateTo(dateNow);
			// inboundLog.setInboundResult("fail");

			/*
			 * List<PlantSupplier> supplierList = getSuppliers(); if
			 * (supplierList != null && supplierList.size() > 0) {
			 * deliveryOrder.setPlantSupplier(supplierList.get(0)); }
			 */

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

			if (deliveryOrder.getpCode() != null && deliveryOrder.getpCode().trim().length() > 0) {

				if (deliveryOrder.getpCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						selectCriteria.add(Restrictions.in("ps.plant", plants));
						selectCountCriteria.add(Restrictions.in("ps.plant", plants));
					} else {
						selectCriteria.add(Restrictions.eq("p.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("p.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("p.code", deliveryOrder.getpCode().trim()));
					selectCountCriteria.add(Restrictions.eq("p.code", deliveryOrder.getpCode().trim()));
				}
			}

			if (deliveryOrder.getsCode() != null && deliveryOrder.getsCode().trim().length() > 0) {
				if (deliveryOrder.getsCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null && suppliers.size() > 0) {
						selectCriteria.add(Restrictions.in("ps.supplier", suppliers));
						selectCountCriteria.add(Restrictions.in("ps.supplier", suppliers));
					} else {
						selectCriteria.add(Restrictions.eq("s.code", "-1"));
						selectCountCriteria.add(Restrictions.eq("s.code", "-1"));
					}
				} else {
					selectCriteria.add(Restrictions.eq("s.code", deliveryOrder.getsCode().trim()));
					selectCountCriteria.add(Restrictions.eq("s.code", deliveryOrder.getsCode().trim()));
				}
			}

			if (deliveryOrder.getExternalDoNo() != null && deliveryOrder.getExternalDoNo().trim().length() > 0) {
				selectCriteria.add(Restrictions.like("externalDoNo", deliveryOrder.getExternalDoNo().trim()));
				selectCountCriteria.add(Restrictions.like("externalDoNo", deliveryOrder.getExternalDoNo().trim()));
			}

			if (deliveryOrder.getCreateDateFrom() != null) {
				selectCriteria.add(Restrictions.ge("createDate", deliveryOrder.getCreateDateFrom()));
				selectCountCriteria.add(Restrictions.ge("createDate", deliveryOrder.getCreateDateFrom()));
			}

			if (deliveryOrder.getCreateDateTo() != null) {
				Calendar calendar = Calendar.getInstance();
				calendar.setTime(deliveryOrder.getCreateDateTo());
				calendar.add(Calendar.DATE, 1);
				selectCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
				selectCountCriteria.add(Restrictions.lt("createDate", calendar.getTime()));
			}

			if (deliveryOrder.getStatus() != null && deliveryOrder.getStatus().trim().length() != 0) {
				selectCriteria.add(Restrictions.eq("status", deliveryOrder.getStatus()));
				selectCountCriteria.add(Restrictions.eq("status", deliveryOrder.getStatus()));
			}

			if (deliveryOrder.getExportFlag() != null && deliveryOrder.getExportFlag().trim().length() > 0) {
				if ("true".equalsIgnoreCase(deliveryOrder.getExportFlag())) {
					selectCriteria.add(Restrictions.eq("isExport", true));
					selectCountCriteria.add(Restrictions.eq("isExport", true));
				} else {
					selectCriteria.add(Restrictions.eq("isExport", false));
					selectCountCriteria.add(Restrictions.eq("isExport", false));
				}
			}

			if (deliveryOrder.getPrintFlag() != null && deliveryOrder.getPrintFlag().trim().length() > 0) {
				if ("true".equalsIgnoreCase(deliveryOrder.getPrintFlag())) {
					selectCriteria.add(Restrictions.eq("isPrint", true));
					selectCountCriteria.add(Restrictions.eq("isPrint", true));
				} else {
					selectCriteria.add(Restrictions.eq("isPrint", false));
					selectCountCriteria.add(Restrictions.eq("isPrint", false));
				}
			}

			if (deliveryOrder.getReadFlag() != null && deliveryOrder.getReadFlag().trim().length() > 0) {
				if ("true".equalsIgnoreCase(deliveryOrder.getReadFlag())) {
					selectCriteria.add(Restrictions.eq("isRead", true));
					selectCountCriteria.add(Restrictions.eq("isRead", true));
				} else {
					selectCriteria.add(Restrictions.eq("isRead", false));
					selectCountCriteria.add(Restrictions.eq("isRead", false));
				}
			}

			if (deliveryOrder.getPlantSupplier() != null) {
				selectCriteria.add(Restrictions.eq("plantSupplier", deliveryOrder.getPlantSupplier()));
				selectCountCriteria.add(Restrictions.eq("plantSupplier", deliveryOrder.getPlantSupplier()));
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

			paginatedList.setList(this.deliveryOrderManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
			paginatedList.setFullListSize(Integer.parseInt(this.deliveryOrderManager.findByCriteria(selectCountCriteria).get(0).toString()));
		}
		return SUCCESS;
	}

	public String list2() {

		if (deliveryOrder != null) {

			String hql = "select min(do.createDate), p.code, p.name, s.code, s.name, do.fileIdentitfier, min(do.firstReadDate), max(do.isRead), do.plantContactPerson "
					+ "from DeliveryOrder as do join do.plantSupplier as ps join ps.plant as p join ps.supplier as s where 1=1 ";
			List args = new ArrayList();

			if (deliveryOrder.getpCode() != null && deliveryOrder.getpCode().trim().length() > 0) {

				if (deliveryOrder.getpCode().equals("-1")) {
					List<Plant> plants = getPlants();
					if (plants != null && plants.size() > 0) {
						hql += "and ps.plant in ? ";
						args.add(plants);
					} else {
						hql += "and p.code = -1 ";
					}
				} else {
					hql += "and p.code = ? ";
					args.add(deliveryOrder.getpCode().trim());
				}
			}

			if (deliveryOrder.getsCode() != null && deliveryOrder.getsCode().trim().length() > 0) {
				if (deliveryOrder.getsCode().equals("-1")) {
					List<Supplier> suppliers = getSuppliers();
					if (suppliers != null && suppliers.size() > 0) {
						hql += "and ps.supplier in ? ";
						args.add(suppliers);
					} else {
						hql += "and s.code = -1 ";
					}
				} else {
					hql += "and s.code = ? ";
					args.add(deliveryOrder.getsCode().trim());
				}
			}

			hql += "and do.fileIdentitfier is not null and do.fileIdentitfier <> '' ";

			if (deliveryOrder.getPlantSupplier() != null) {
				hql += "and ps.id = ? ";
				args.add(deliveryOrder.getPlantSupplier().getId());
			}

			if (deliveryOrder.getFileIdentitfier() != null) {
				hql += "and do.fileIdentitfier like ? ";
				args.add("%" + deliveryOrder.getFileIdentitfier() + "%");
			}

			hql += "group by p.code, p.name, s.code, s.name, do.fileIdentitfier, do.plantContactPerson having 1=1 ";

			if (deliveryOrder.getCreateDateFrom() != null) {
				hql += "and min(do.createDate) >= ? ";
				args.add(deliveryOrder.getCreateDateFrom());
			}

			if (deliveryOrder.getCreateDateTo() != null) {
				Calendar calendar = Calendar.getInstance();
				calendar.setTime(deliveryOrder.getCreateDateTo());
				calendar.add(Calendar.DATE, 1);
				hql += "and min(do.createDate) < ? ";
				args.add(calendar.getTime());
			}

			if (deliveryOrder.getReadFlag() != null && deliveryOrder.getReadFlag().trim().length() > 0) {
				if ("true".equalsIgnoreCase(deliveryOrder.getReadFlag())) {
					hql += "and max(do.isRead) = '1' ";
				} else {
					hql += "and max(do.isRead) = '0' ";
				}
			}

			if (sort != null && sort.trim().length() > 0) {
				paginatedList.setSortCriterion(sort);
				hql += "order by " + sort + " " + dir;
			}

			List result = null;
			if (args.size() > 0) {
				Object[] objs = new Object[args.size()];

				for (int i = 0; i < args.size(); i++) {
					objs[i] = args.get(i);
				}
				result = this.deliveryOrderManager.findByHql(hql, objs);
			} else {
				result = this.deliveryOrderManager.findByHql(hql);
			}

			if (result != null && result.size() > 0) {
				List<DeliveryOrder> list2 = new ArrayList<DeliveryOrder>();

				for (int i = (page - 1) * pageSize; i < result.size() && i < page * pageSize; i++) {
					Object[] obj = (Object[]) (result.get(i));

					DeliveryOrder deliveryOrder = new DeliveryOrder();
					deliveryOrder.setCreateDate((Date) obj[0]);
					deliveryOrder.setPlantCode((String) obj[1]);
					deliveryOrder.setFileIdentitfier((String) obj[5]);
					deliveryOrder.setSupplierName((String) obj[4]);
					deliveryOrder.setPlantContactPerson((String) obj[8]);
					deliveryOrder.setFirstReadDate((Date) obj[6]);

					list2.add(deliveryOrder);
				}

				paginatedList.setList(list2);
				paginatedList.setFullListSize(list2.size());
			} else {
				paginatedList.setList(result);
				paginatedList.setFullListSize(result.size());
			}
		}
		
		return SUCCESS;
	}

	public String list3() {
		if (this.fileIdentitfier != null) {
			DetachedCriteria criteria = DetachedCriteria.forClass(DeliveryOrder.class);

			criteria.add(Restrictions.eq("fileIdentitfier", this.fileIdentitfier));

			deliveryOrderDetailList = this.deliveryOrderManager.findByCriteria(criteria);
		}

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		if (this.doNo != null) {
			deliveryOrder = this.deliveryOrderManager.get(doNo, true);
			deliveryOrder.setFirstReadDate(new Date());
			deliveryOrder.setIsRead(true);
			deliveryOrder = this.deliveryOrderManager.save(deliveryOrder);
		} else if (purchaseOrderDetailList != null) {
			// po 创建 do
			boolean hasError = false;
			List<PurchaseOrderDetail> noneZeroPurchaseOrderDetailList = new ArrayList<PurchaseOrderDetail>();
			for (int i = 1; i < purchaseOrderDetailList.size(); i++) {
				PurchaseOrderDetail purchaseOrderDetail = this.purchaseOrderDetailManager.get(purchaseOrderDetailList.get(i).getId());
				BigDecimal currentShipQty = purchaseOrderDetailList.get(i).getCurrentShipQty();
				purchaseOrderDetail.setCurrentShipQty(currentShipQty);

				if (poNo == null) {
					poNo = purchaseOrderDetail.getPurchaseOrder().getPoNo();
				}

				if ((purchaseOrderDetail.getShipQty() != null && currentShipQty != null && currentShipQty.add(purchaseOrderDetail.getShipQty())
						.compareTo(purchaseOrderDetail.getQty()) > 0)
						|| (purchaseOrderDetail.getShipQty() == null && currentShipQty != null && currentShipQty.compareTo(purchaseOrderDetail
								.getQty()) > 0)) {
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

			// sa 创建 do
			PlantSupplier plantSupplier = this.plantSupplierManager.get(plantSupplierId);
			Schedule schedule = this.scheduleManager.getLastestScheduleItem(plantSupplier.getPlant().getCode(),
					plantSupplier.getSupplier().getCode(), true);
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
							deliveryOrder.setIsPrint(false);
							deliveryOrder.setIsRead(false);
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
							deliveryOrderDetail.setQty(BigDecimal.ZERO);
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
					if (totalDeliverQty == null) {
						totalDeliverQty = BigDecimal.ZERO;
					}
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
			if (!hasError) {
				deliveryOrder = this.deliveryOrderManager.createScheduleDeliveryOrder(deliveryOrder);
				saveMessage(getText("deliveryOrder.added"));
			}
		}

		this.deliveryOrderManager.flushSession();

		return SUCCESS;
	}

	public String save() {
		if (!collectDevlieryOrder()) {
			this.deliveryOrderManager.save(deliveryOrder);
			this.deliveryOrderManager.flushSession();
			saveMessage(getText("deliveryOrder.updated"));
		}

		return SUCCESS;
	}

	public String confirm() {
		if (!collectDevlieryOrder()) {

			boolean allZero = true;
			for (int i = 0; i < deliveryOrder.getDeliveryOrderDetailList().size(); i++) {
				DeliveryOrderDetail deliveryOrderDetail = deliveryOrder.getDeliveryOrderDetailList().get(i);
				if (deliveryOrderDetail.getQty() != null && deliveryOrderDetail.getQty().compareTo(BigDecimal.ZERO) > 0) {
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
		this.deliveryOrderManager.flushSession();
		return SUCCESS;
	}

	public InputStream getInputStream() {
		return inputStream;
	}

	public String getFileName() {
		return fileName;
	}

	public String print(Boolean isLogistic) throws Exception {
		String localAbsolutPath = this.getSession().getServletContext().getRealPath("/");
		deliveryOrder = this.deliveryOrderManager.get(deliveryOrder.getDoNo(), true);
		deliveryOrder.setIsLogisticPartner(isLogistic);

		String doTemplateName = deliveryOrder.getPlantSupplier().getDoTemplateName();
		if (doTemplateName == null || doTemplateName.trim().length() == 0) {
			doTemplateName = deliveryOrder.getPlantSupplier().getPlant().getDoTemplateName();
		}

		if (doTemplateName.equalsIgnoreCase("Do.png")) {
			inputStream = DeliveryOrderExportUtil.exportDo(localAbsolutPath, deliveryOrder.getPlantSupplier().getPlant().getDoTemplateName(),
					deliveryOrder, false);
		} else if (doTemplateName.equalsIgnoreCase("Do_CN.png")) {
			inputStream = DeliveryOrderExportUtil.exportDo(localAbsolutPath, "Do.png", deliveryOrder, true);
		} else if (doTemplateName.equalsIgnoreCase("DoSebango.png")) {
			inputStream = DeliveryOrderExportUtil.exportDoSebango(localAbsolutPath, doTemplateName, deliveryOrder, false);
		} else if (doTemplateName.equalsIgnoreCase("DoSebango_CN.png")) {
			inputStream = DeliveryOrderExportUtil.exportDoSebango(localAbsolutPath, doTemplateName, deliveryOrder, true);
		} else {
			inputStream = DeliveryOrderExportUtil.export(localAbsolutPath, deliveryOrder.getPlantSupplier().getPlant().getDoTemplateName(),
					deliveryOrder);
		}
		fileName = "deliveryOrder_" + deliveryOrder.getDoNo() + ".pdf";
		if (deliveryOrder.getIsPrint() == null || !deliveryOrder.getIsPrint()) {
			deliveryOrder.setIsPrint(true);
			this.deliveryOrderManager.save(deliveryOrder);
		}
		return SUCCESS;
	}

	public String printLogistic() throws Exception {
		return print(true);
	}

	public String printSupplier() throws Exception {
		return print(false);
	}

	public String printPalletLabel() throws Exception {
		String localAbsolutPath = this.getSession().getServletContext().getRealPath("/");
		deliveryOrder = this.deliveryOrderManager.get(deliveryOrder.getDoNo(), true);
		inputStream = DeliveryOrderExportUtil.printPalletLabel(localAbsolutPath, "Pallet.png", deliveryOrder);
		fileName = "PalletLabel_" + deliveryOrder.getDoNo() + ".pdf";

		return SUCCESS;
	}

	public String printBoxLabel() throws Exception {
		String localAbsolutPath = this.getSession().getServletContext().getRealPath("/");
		deliveryOrder = this.deliveryOrderManager.get(deliveryOrder.getDoNo(), true);
		List<DeliveryOrderDetail> selectedDeliveryOrderDetailList = new ArrayList<DeliveryOrderDetail>();
		if (deliveryOrderDetailList == null) {
			return ERROR;
		} else {
			for (int i = 0; i < deliveryOrderDetailList.size(); i++) {
				if (deliveryOrderDetailList.get(i) != null) {
					selectedDeliveryOrderDetailList.add(deliveryOrder.getDeliveryOrderDetailList().get(i - 1));
				}
			}
		}

		String boxTemplateName = deliveryOrder.getPlantSupplier().getBoxTemplateName();
		if (boxTemplateName == null || boxTemplateName.trim().length() == 0) {
			boxTemplateName = deliveryOrder.getPlantSupplier().getPlant().getBoxTemplateName();
		}

		if (boxTemplateName.equalsIgnoreCase("Box.png")) {
			inputStream = DeliveryOrderExportUtil.printBoxLabel(localAbsolutPath, deliveryOrder.getPlantSupplier().getPlant().getBoxTemplateName(),
					deliveryOrder, selectedDeliveryOrderDetailList);
		} else if (boxTemplateName.equalsIgnoreCase("Box_CN.png")) {
			inputStream = DeliveryOrderExportUtil.printBoxLabel2(localAbsolutPath, deliveryOrder.getPlantSupplier().getPlant().getBoxTemplateName(),
					deliveryOrder, selectedDeliveryOrderDetailList);
		} else {
			inputStream = DeliveryOrderExportUtil.printBoxLabel1(localAbsolutPath, deliveryOrder.getPlantSupplier().getPlant().getBoxTemplateName(),
					deliveryOrder, selectedDeliveryOrderDetailList);
		}
		fileName = "BoxLabel_" + deliveryOrder.getDoNo() + ".pdf";
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
