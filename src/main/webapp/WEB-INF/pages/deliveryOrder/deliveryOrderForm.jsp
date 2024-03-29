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
		<table>
			<tr>
				<c:if test="${empty deliveryOrder.doNo}">
					<td><s:submit key="button.save" /></td>
				</c:if>
				<c:if
					test="${not empty deliveryOrder.doNo and deliveryOrder.status == 'Create'}">
					<td><s:submit key="button.save" action="saveDeliveryOrder" /></td>
					<td><s:submit key="button.confirm"
						action="confirmDeliveryOrder" /></td>
					<td><s:submit key="button.delete" action="deleteDeliveryOrder" /></td>
				</c:if>
				<c:if
					test="${not empty deliveryOrder.doNo and empty deliveryOrder.fileIdentitfier and deliveryOrder.status == 'Confirm'}">
					<td><s:submit key="button.print"
						action="printDeliveryOrder" /></td>
				</c:if>
				<c:if
					test="${not empty deliveryOrder.doNo and not empty deliveryOrder.fileIdentitfier and deliveryOrder.status == 'Confirm'}">
					<td><s:submit key="button.printSupplier"
						action="printDeliveryOrder" /></td>
					<td><s:submit key="button.printLogistic"
						action="printLogisticDeliveryOrder" /></td>
					<td><s:submit key="button.printPalletLabel"
						action="printPalletLabel" /></td>
					<td><s:submit key="button.printBoxLabel"
						action="printBoxLabel" /></td>
				</c:if>
				<td><c:if test="${param.from == 'list'}">
					<input type="button" value="<fmt:message key="button.cancel"/>"
						onclick="window.location.href='cancelDeliveryOrder.html'" />
				</c:if> <c:if test="${param.from == 'list2'}">
					<input type="button" value="<fmt:message key="button.cancel"/>"
						onclick="window.location.href='deliveryOrders3.html?fileIdentitfier=${deliveryOrder.fileIdentitfier}'" />
				</c:if></td>
			</tr>
		</table>
	</c:set>

	<table width="100%">
		<tr>
			<td><s:label key="deliveryOrder.externalDoNo"
				cssClass="text medium" /></td>
			<td><input type="hidden" name="from" value="${param.from}" /> <s:hidden
				name="deliveryOrder.doNo" key="deliveryOrder.doNo" /> <s:hidden
				name="deliveryOrder.plantCode" key="deliveryOrder.plantCode" /> <s:hidden
				name="deliveryOrder.supplierCode" key="deliveryOrder.supplierCode" />
			<s:hidden name="deliveryOrder.plantName"
				key="deliveryOrder.plantName" /> <s:hidden
				name="deliveryOrder.supplierName" key="deliveryOrder.supplierName" />
			<s:hidden name="deliveryOrder.plantAddress1"
				key="deliveryOrder.plantAddress1" /> <s:hidden
				name="deliveryOrder.supplierAddress1"
				key="deliveryOrder.supplierAddress1" /> <s:hidden
				name="deliveryOrder.plantContactPerson"
				key="deliveryOrder.plantContactPerson" /> <s:hidden
				name="deliveryOrder.supplierContactPerson"
				key="deliveryOrder.supplierContactPerson" /> <s:hidden
				name="deliveryOrder.supplierPhone" key="deliveryOrder.supplierPhone" />
			<s:hidden name="deliveryOrder.plantPhone"
				key="deliveryOrder.plantPhone" /> <s:hidden
				name="deliveryOrder.plantFax" key="deliveryOrder.plantFax" /> <s:hidden
				name="deliveryOrder.supplierFax" key="deliveryOrder.supplierFax" />
			<s:hidden name="deliveryOrder.createDate"
				key="deliveryOrder.createDate" /> <s:hidden
				name="deliveryOrder.plantSupplier.id"
				key="deliveryOrder.plantSupplier.id" /> <s:hidden
				name="deliveryOrder.isExport" key="deliveryOrder.isExport" /> <s:hidden
				name="deliveryOrder.isPrint" key="deliveryOrder.isPrint" /> <s:hidden
				name="deliveryOrder.isRead" key="deliveryOrder.isRead" /><s:hidden
				name="deliveryOrder.firstReadDate" key="deliveryOrder.firstReadDate" /><s:hidden
				name="deliveryOrder.fileIdentitfier"
				key="deliveryOrder.fileIdentitfier" /> <s:hidden
				name="deliveryOrder.allowOverQty" key="deliveryOrder.allowOverQty" />
			<s:hidden name="deliveryOrder.externalDoNo"
				key="deliveryOrder.externalDoNo" /></td>
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
			<td><s:label key="deliveryOrder.status" cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.createDate"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.isRead" cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.firstReadDate"
				cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="deliveryOrder.isPrint" cssClass="text medium" /></td>
			<td><s:label key="deliveryOrder.isExport" cssClass="text medium" /></td>
		</tr>
	</table>

	<display:table name="deliveryOrder.deliveryOrderDetailList"
		cellspacing="0" cellpadding="0" requestURI="" id="deliveryOrderDetail"
		class="table">
		<c:if
			test="${not empty deliveryOrder.doNo and deliveryOrder.status == 'Confirm'}">
			<display:column
				title="<input type='checkbox' name='allbox' value='all' onclick='checkAll(this.form);' />">
				<input type="checkbox"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].isChoosen" />
			</display:column>
		</c:if>
		<display:column property="sequence"
			titleKey="deliveryOrderDetail.sequence" />
		<display:column property="item.code"
			titleKey="deliveryOrderDetail.itemCode" />
		<display:column property="sebango"
			titleKey="deliveryOrderDetail.sebango" />
		<display:column property="itemDescription"
			titleKey="deliveryOrderDetail.itemDescription" />
		<display:column property="supplierItemCode"
			titleKey="deliveryOrderDetail.supplierItemCode" />
		<display:column property="uom" titleKey="deliveryOrderDetail.uom" />
		<display:column property="orderQty"
			titleKey="deliveryOrderDetail.orderQty" format="{0,number,#,###.##}" />
		<display:column property="deliverQty"
			titleKey="deliveryOrderDetail.deliverQty"
			format="{0,number,#,###.##}" />
		<c:if
			test="${not empty deliveryOrder.doNo and deliveryOrder.status == 'Confirm'}">
			<display:column property="qty" titleKey="deliveryOrderDetail.qty"
				format="{0,number,#,###.##}" />
		</c:if>
		<c:if
			test="${empty deliveryOrder.doNo or deliveryOrder.status == 'Create'}">
			<display:column titleKey="deliveryOrderDetail.qty">
				<input type="text"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].qty"
					value="<fmt:formatNumber value="${deliveryOrderDetail.qty}" pattern="###.##" />"
					class="text medium" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].id"
					value="<c:out value="${deliveryOrderDetail.id}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].scheduleItemDetail.id"
					value="<c:out value="${deliveryOrderDetail.scheduleItemDetail.id}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].purchaseOrderDetail.id"
					value="<c:out value="${deliveryOrderDetail.purchaseOrderDetail.id}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].orderQty"
					value="<c:out value="${deliveryOrderDetail.orderQty}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].item.code"
					value="<c:out value="${deliveryOrderDetail.item.code}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].sequence"
					value="<c:out value="${deliveryOrderDetail.sequence}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].itemDescription"
					value="<c:out value="${deliveryOrderDetail.itemDescription}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].supplierItemCode"
					value="<c:out value="${deliveryOrderDetail.supplierItemCode}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].uom"
					value="<c:out value="${deliveryOrderDetail.uom}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].unitCount"
					value="<c:out value="${deliveryOrderDetail.unitCount}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].referenceOrderNo"
					value="<c:out value="${deliveryOrderDetail.referenceOrderNo}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].referenceSequence"
					value="<c:out value="${deliveryOrderDetail.referenceSequence}"/>" />
				<input type="hidden"
					name="deliveryOrderDetailList[${deliveryOrderDetail_rowNum}].item.id"
					value="<c:out value="${deliveryOrderDetail.item.id}"/>" />
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
