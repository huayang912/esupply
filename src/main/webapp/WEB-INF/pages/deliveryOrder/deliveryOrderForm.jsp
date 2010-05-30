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
			<td>
				<s:hidden name="deliveryOrder.plantCode" key="deliveryOrder.plantCode"/>
				<s:hidden name="deliveryOrder.supplierCode" key="deliveryOrder.supplierCode" /> 
				<s:hidden name="deliveryOrder.plantName" key="deliveryOrder.plantName" />
				<s:hidden name="deliveryOrder.supplierName" key="deliveryOrder.supplierName" /> 
				<s:hidden name="deliveryOrder.plantAddress1" key="deliveryOrder.plantAddress1" />
				<s:hidden name="deliveryOrder.supplierAddress1" key="deliveryOrder.supplierAddress1" />
				<s:hidden name="deliveryOrder.plantContactPerson" key="deliveryOrder.plantContactPerson" />
				<s:hidden name="deliveryOrder.supplierContactPerson" key="deliveryOrder.supplierContactPerson" />
				<s:hidden name="deliveryOrder.supplierPhone" key="deliveryOrder.supplierPhone" />
				<s:hidden name="deliveryOrder.plantPhone" key="deliveryOrder.plantPhone" />
				<s:hidden name="deliveryOrder.plantFax" key="deliveryOrder.plantFax" />
				<s:hidden name="deliveryOrder.supplierFax" key="deliveryOrder.supplierFax" />
				<s:hidden name="deliveryOrder.createDate" key="deliveryOrder.createDate" />
				<s:hidden name="deliveryOrder.plantSupplier.id" key="deliveryOrder.plantSupplier.id" />
			</td>
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
			<td><s:label key="deliveryOrder.plantFax" cssClass="text medium" /></td>
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
			<display:column titleKey="deliveryOrderDetail.qty">
				<input type="text" name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].currentQty" value="${deliveryOrderDetail.currentQty}" class="text medium" />
				<input type="hidden" name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].orderQty" value="<c:out value="${deliveryOrderDetail.orderQty}"/>"/>
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
