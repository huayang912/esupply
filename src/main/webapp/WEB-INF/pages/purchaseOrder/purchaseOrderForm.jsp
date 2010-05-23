<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="purchaseOrderDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderDetail.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<s:form name="purchaseOrderForm" action="savePurchaseOrder"
	method="post" validate="true">
	<c:set var="buttons">
		<s:submit key="button.cancel" method="cancel" />
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
		id="purchaseOrderDetailList" class="table">
		<display:column property="sequence"
			titleKey="purchaseOrderDetail.sequence" />
		<display:column property="item.code"
			titleKey="purchaseOrderDetail.itemCode" />
		<display:column property="itemDescription"
			titleKey="purchaseOrderDetail.itemDescription" />
		<display:column property="supplierItemCode"
			titleKey="purchaseOrderDetail.supplierItemCode" />
		<display:column property="uom" titleKey="purchaseOrderDetail.uom" />
		<display:column property="deliveryDate" format="{0,date,yyyy-MM-dd}"
			titleKey="purchaseOrderDetail.deliveryDate" />
		<display:column property="qty" titleKey="purchaseOrderDetail.qty" />
		<display:column property="shipQty"
			titleKey="purchaseOrderDetail.shipQty" />
	</display:table>

	<div class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["purchaseOrderForm"]);
		highlightFormElements();
</script>
