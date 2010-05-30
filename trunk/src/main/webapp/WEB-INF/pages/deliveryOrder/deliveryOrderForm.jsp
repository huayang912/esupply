<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="deliveryOrderDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='deliveryOrderDetail.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<s:form name="deliveryOrderForm" action="editDeliveryOrder"
	method="post" validate="true">
	<c:set var="buttons">
		<c:if test="${empty deliveryOrder.doNo}">
			<s:submit key="button.save" />
		</c:if>
		<input type="button" value="<fmt:message key="button.cancel"/>"
			onclick="window.location.href='cancelDeliveryOrder.html'" />
	</c:set>

	<table width="100%">
		<tr>
			<td><s:label key="deliveryOrder.doNo" cssClass="text medium" /></td>
			<td></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.plantCode"
				cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.supplierCode"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.plantName"
				cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.supplierName"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.plantAddress1"
				cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.supplierAddress1"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.plantContactPerson"
				cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.supplierContactPerson"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.plantPhone"
				cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.supplierPhone"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.plantFax" cssClass="text medium" />
			</td>
			<td><s:label key="deliveryOrder.supplierFax"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.createDate"
				cssClass="text medium" /></td>
			<td></td>
		</tr>
	</table>

	<display:table name="deliveryOrder.deliveryOrderDetailList"
		cellspacing="0" cellpadding="0" requestURI="" id="deliveryOrderDetail"
		class="table">
		<display:column property="sequence"
			titleKey="deliveryOrderDetail.sequence" />
		<display:column property="item.code"
			titleKey="deliveryOrderDetail.itemCode" />
		<display:column property="itemDescription"
			titleKey="deliveryOrderDetail.itemDescription" />
		<display:column property="supplierItemCode"
			titleKey="deliveryOrderDetail.supplierItemCode" />
		<display:column property="uom" titleKey="deliveryOrderDetail.uom" />
		<display:column property="orderQty"
			titleKey="deliveryOrderDetail.orderQty" />
		<c:if test="${not empty deliveryOrder.doNo}">
			<display:column property="qty" titleKey="deliveryOrderDetail.qty" />
		</c:if>
		<c:if test="${empty deliveryOrder.doNo}">
			<display:column titleKey="deliveryOrderDetail.Qty">
				<input type="text"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].currentQty"
					value="${purchaseOrderDetail.qty}" class="text medium" />
			</display:column>
		</c:if>
	</display:table>

	<div class="buttonBar bottom">
	<div align="right"><c:out value="${buttons}" escapeXml="false" /></div>
	</div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["deliveryOrderForm"]);
		highlightFormElements();
</script>
