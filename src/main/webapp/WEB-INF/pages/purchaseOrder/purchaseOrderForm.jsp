<%@ include file="/common/taglibs.jsp"%>


<%@page import="com.faurecia.Constants"%><head>
<title><fmt:message key="purchaseOrderDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderDetail.heading'/>" />
<c:choose>
	<c:when test="<%=request.isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)%>">
		<meta name="menu" content="PlantOrderMenu" />
	</c:when>
	<c:when test="<%=request.isUserInRole(com.faurecia.Constants.VENDOR_ROLE)%>">
		<meta name="menu" content="SupplierOrderMenu" />
	</c:when>
</c:choose>
</head>

<s:form name="purchaseOrderForm" action="editDeliveryOrder"
	method="post" validate="true">
	<c:set var="buttons">
		<s:submit key="button.save" />
		<input type="button" value="<fmt:message key="button.cancel"/>" onclick="window.location.href='cancelPurchaseOrder.html'"/> 		
	</c:set>

	<table width="100%">
		<tr>
			<td><s:label key="purchaseOrder.poNo" cssClass="text medium" /></td>
			<td></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.plantCode"
				cssClass="text medium" /></td>
			<td><s:label key="purchaseOrder.supplierCode"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.plantName"
				cssClass="text medium" /></td>
			<td><s:label key="purchaseOrder.supplierName"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.plantAddress1"
				cssClass="text medium" /></td>
			<td><s:label key="purchaseOrder.supplierAddress1"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.plantContactPerson"
				cssClass="text medium" /></td>
			<td><s:label key="purchaseOrder.supplierContactPerson"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.plantPhone"
				cssClass="text medium" /></td>
			<td><s:label key="purchaseOrder.supplierPhone"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.plantFax" cssClass="text medium" />
			</td>
			<td><s:label key="purchaseOrder.supplierFax"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="purchaseOrder.createDate"
				cssClass="text medium" /></td>
			<td><s:label key="purchaseOrder.status" cssClass="text medium" />
			</td>
		</tr>
	</table>

	<display:table name="purchaseOrder.purchaseOrderDetailList"
		cellspacing="0" cellpadding="0" requestURI=""
		id="purchaseOrderDetail" class="table" > 
		<display:column	property="sequence" 
		titleKey="purchaseOrderDetail.sequence" />
		<display:column property="item.code"
			titleKey="purchaseOrderDetail.itemCode" />
		<display:column property="itemDescription"
			titleKey="purchaseOrderDetail.itemDescription" />
		<display:column property="supplierItemCode"
			titleKey="purchaseOrderDetail.supplierItemCode" />
		<display:column property="uom" titleKey="purchaseOrderDetail.uom" />
		<display:column property="deliveryDate" format="{0,date,MM/dd/yyyy}"
			titleKey="purchaseOrderDetail.deliveryDate" />
		<display:column property="qty" titleKey="purchaseOrderDetail.qty" />
		<display:column property="shipQty" titleKey="purchaseOrderDetail.shipQty" />
		<display:column titleKey="purchaseOrderDetail.currentShipQty" >
			<input type="hidden" name="purchaseOrderDetailList[${purchaseOrderDetail_rowNum}].id" value="<c:out value="${purchaseOrderDetail.id}"/>"/>
			<input type="text" name="purchaseOrderDetailList[${purchaseOrderDetail_rowNum}].currentShipQty" value="${purchaseOrderDetail.remainQty}" class="text medium"/>
		</display:column>
	</display:table>

	<div class="buttonBar bottom">
	<div align="right"><c:out value="${buttons}" escapeXml="false" /></div>
	</div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["purchaseOrderForm"]);
		highlightFormElements();
</script>
