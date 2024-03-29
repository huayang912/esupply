<%@ include file="/common/taglibs.jsp"%>


<%@page import="com.faurecia.Constants"%><head>
<title><fmt:message key="purchaseOrderDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderDetail.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<s:form name="purchaseOrderForm" action="editDeliveryOrder"
	method="post" validate="true">
	<c:set var="buttons">
		<table>
			<tr>
				<td><c:if test="${purchaseOrder.status != 'Close'}">
					<s:submit key="button.save" method="edit" />
				</c:if></td>
				<td><input type="button"
					value="<fmt:message key="button.cancel"/>"
					onclick="window.location.href='cancelPurchaseOrder.html'" /></td>
			</tr>
		</table>
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
		cellspacing="0" cellpadding="0" requestURI="" id="purchaseOrderDetail"
		class="table">
		<display:column property="sequence"
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
		<display:column property="shipQty"
			titleKey="purchaseOrderDetail.shipQty" />
		<c:if test="${purchaseOrder.status != 'Close'}">
			<display:column titleKey="purchaseOrderDetail.currentShipQty">
				<input type="hidden"
					name="purchaseOrderDetailList[${purchaseOrderDetail_rowNum}].id"
					value="<c:out value="${purchaseOrderDetail.id}"/>" />
				<input type="text"
					name="purchaseOrderDetailList[${purchaseOrderDetail_rowNum}].currentShipQty"
					value="${purchaseOrderDetail.remainQty}" class="text medium" />
			</display:column>
		</c:if>
	</display:table>

	<div class="buttonBar bottom">
	<div align="right"><c:out value="${buttons}" escapeXml="false" /></div>
	</div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["purchaseOrderForm"]);
		highlightFormElements();
</script>
