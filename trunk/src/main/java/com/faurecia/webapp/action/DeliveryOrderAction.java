package com.faurecia.webapp.action;

import java.math.BigDecimal;
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
import com.faurecia.model.PurchaseOrder;
import com.faurecia.model.PurchaseOrderDetail;
import com.faurecia.model.User;
import com.faurecia.service.DeliveryOrderManager;
import com.faurecia.service.GenericManager;
import com.faurecia.webapp.util.PaginatedListUtil;

public class DeliveryOrderAction extends BaseAction {
	/**
	 * 
	 */
	private static final long serialVersionUID = -5177104341924305525L;
	private DeliveryOrderManager deliveryOrderManager;
	private GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager;

	private PaginatedListUtil<DeliveryOrder> paginatedList;
	private int pageSize;
	private int page;
	private String sort;
	private String dir;
	private DeliveryOrder deliveryOrder;
	private String doNo;	
	private List<PurchaseOrderDetail> purchaseOrderDetailList;

	public void setDeliveryOrderManager(DeliveryOrderManager deliveryOrderManager) {
		this.deliveryOrderManager = deliveryOrderManager;
	}
	
	public void setPurchaseOrderDetailManager(GenericManager<PurchaseOrderDetail, Integer> purchaseOrderDetailManager) {
		this.purchaseOrderDetailManager = purchaseOrderDetailManager;
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
		status.put("Open", "Open");
		status.put("Close", "Close");
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
	
	public List<PurchaseOrderDetail> getPurchaseOrderDetailList() {
		return purchaseOrderDetailList;
	}

	public void setPurchaseOrderDetailList(List<PurchaseOrderDetail> purchaseOrderDetailList) {
		this.purchaseOrderDetailList = purchaseOrderDetailList;
	}

	public String list() {
		if (deliveryOrder == null) {
			deliveryOrder = new DeliveryOrder();
		}
			
		pageSize = pageSize == 0 ? 25 : pageSize;
		page  = page == 0 ? 1 : page;

		paginatedList = new PaginatedListUtil<DeliveryOrder>();
		paginatedList.setPageNumber(page);
		paginatedList.setObjectsPerPage(pageSize);

		DetachedCriteria selectCriteria = DetachedCriteria.forClass(DeliveryOrder.class);
		DetachedCriteria selectCountCriteria = DetachedCriteria.forClass(DeliveryOrder.class)
			.setProjection(Projections.count("doNo"));
		
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
			selectCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
			selectCountCriteria.add(Restrictions.eq("ps.plant", user.getUserPlant()));
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
			
		if (sort != null && sort.trim().length() > 0) {
			paginatedList.setSortCriterion(sort);
			if (SortOrderEnum.DESCENDING.equals(dir))
			{
				selectCriteria.addOrder(Order.desc(sort));
				paginatedList.setSortDirection(SortOrderEnum.DESCENDING);
			}
			else
			{
				selectCriteria.addOrder(Order.asc(sort));
				paginatedList.setSortDirection(SortOrderEnum.ASCENDING);
			}
		}

		paginatedList.setList(this.deliveryOrderManager.findByCriteria(selectCriteria, (page - 1) * pageSize, pageSize));
		this.deliveryOrderManager.findByCriteria(selectCountCriteria);
		paginatedList.setFullListSize(Integer.parseInt(this.deliveryOrderManager.findByCriteria(selectCountCriteria).get(0).toString()));			

		return SUCCESS;
	}

	public String cancel() {
		return CANCEL;
	}

	public String edit() throws Exception {
		if (this.doNo != null) {
			deliveryOrder = this.deliveryOrderManager.get(doNo);
		}  else if (purchaseOrderDetailList != null) {
			
			DeliveryOrder deliveryOrder = null;
			
			for (int i = 1; i < purchaseOrderDetailList.size(); i++) {
				PurchaseOrderDetail purchaseOrderDetail = this.purchaseOrderDetailManager.get(purchaseOrderDetailList.get(i).getId());
				BigDecimal currentShipQty = purchaseOrderDetailList.get(i).getCurrentShipQty();
				
				if (BigDecimal.ZERO.compareTo(currentShipQty) < 0) {
					if (deliveryOrder == null) {
						PurchaseOrder purchaseOrder = purchaseOrderDetail.getPurchaseOrder();
						deliveryOrder = new DeliveryOrder();
						deliveryOrder.setDoNo("1111111111");
						deliveryOrder.setCreateDate(new Date());
						deliveryOrder.setIsExport(false);
						
						BeanUtils.copyProperties(deliveryOrder, purchaseOrder);
					}
					
					DeliveryOrderDetail deliveryOrderDetail = new DeliveryOrderDetail();
					deliveryOrderDetail.setDeliveryOrder(deliveryOrder);
					
					BeanUtils.copyProperties(deliveryOrderDetail, purchaseOrderDetail);
					
					deliveryOrderDetail.setQty(currentShipQty);
					deliveryOrderDetail.setOrderQty(purchaseOrderDetail.getQty());
					deliveryOrderDetail.setReferenceOrderNo(purchaseOrderDetail.getPurchaseOrder().getPoNo());
					deliveryOrderDetail.setReferenceSequence(purchaseOrderDetail.getSequence());
					deliveryOrder.addDeliveryOrderDetail(deliveryOrderDetail);
				}
			}
			
			if (deliveryOrder != null) {
				this.deliveryOrderManager.save(deliveryOrder);
			}
		}
		
		return SUCCESS;
	}
}