<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="receiptList.title" /></title>
<meta name="heading" content="<fmt:message key='receiptList.heading'/>" />
<c:choose>
	<c:when
		test="<%=request.isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)%>">
		<meta name="menu" content="PlantOrderMenu" />
	</c:when>
	<c:when
		test="<%=request.isUserInRole(com.faurecia.Constants.VENDOR_ROLE)%>">
		<meta name="menu" content="SupplierOrderMenu" />
	</c:when>
</c:choose>
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>

<s:form name="receiptForm" action="searchReceiptDetail" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.receiptNo" /></label></td>
			<td colspan="2"><s:textfield key="receipt.receiptNo"
				cssClass="text medium" theme="simple" /></td>
			<c:if
				test="<%=request.isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)%>">
				<td><label class="desc"><fmt:message
					key="receipt.supplierCode" /></label></td>
				<td colspan="2"><s:select key="receipt.plantSupplier.id"
					list="%{suppliers}" listKey="id" listValue="supplierName"
					theme="simple" /></td>
			</c:if>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.itemCode" /></label></td>
			<td colspan="2"><s:textfield key="receipt.itemCode"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="receipt.supplierItemCode" /></label></td>
			<td colspan="2"><s:textfield key="receipt.supplierItemCode"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.postingDateFrom" /></label></td>
			<td><s:textfield key="receipt.postingDateFrom"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['receiptForm'].receipts_receipt_postingDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
			<td><label class="desc"><fmt:message
				key="receipt.postingDateTo" /></label></td>
			<td><s:textfield key="receipt.postingDateTo"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['receiptForm'].receipts_receipt_postingDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
		<tr>
			<td></td>
			<td><s:radio name="receipt.detailOrSummary"
				list="%{detailOrSummary}" theme="simple"></s:radio></td>
		</tr>
	</table>
	</li>
	<div><s:submit method="list" key="button.search" theme="simple" />
	<s:submit key="button.cancel" method="cancel" theme="simple" /></div>
	</ul>

	<display:table name="paginatedList" cellspacing="0" cellpadding="0"
		requestURI="" id="receiptDetail" class="table" export="true">
		<c:if test="${receipt.detailOrSummary == 'Detail'}">
			<display:column property="receipt.receiptNo" sortable="true"
				sortProperty="r.receiptNo" titleKey="receipt.receiptNo" />
		</c:if>
		<c:if test="${receipt.detailOrSummary == 'Detail'}">
			<display:column property="item.code" sortable="true"
				sortProperty="i.code" titleKey="receiptDetail.itemCode" />
		</c:if>
		<c:if test="${receipt.detailOrSummary == 'Summary'}">
			<display:column property="itemCode" sortable="true"
				sortProperty="i.code" titleKey="receiptDetail.itemCode" />
		</c:if>
		<display:column property="itemDescription" sortable="true"
			titleKey="receiptDetail.itemDescription" />
		<display:column property="supplierItemCode" sortable="true"
			titleKey="receiptDetail.supplierItemCode" />
		<display:column property="uom" sortable="true"
			titleKey="receiptDetail.uom" />
		<display:column property="qty" sortable="true"
			titleKey="receiptDetail.qty" />
		<c:if test="${receipt.detailOrSummary == 'Detail'}">
			<display:column property="receipt.postingDate"
				format="{0,date,MM/dd/yyyy}" sortable="true"
				sortProperty="r.postingDate" titleKey="receipt.postingDate" />
		</c:if>

		<display:setProperty name="paging.banner.item_name">
			<fmt:message key="receipt.receipt" />
		</display:setProperty>
		<display:setProperty name="paging.banner.items_name">
			<fmt:message key="receipt.receipts" />
		</display:setProperty>

		<display:setProperty name="export.excel.filename"
			value="receipt List.xls" />
		<display:setProperty name="export.csv.filename"
			value="receipt List.csv" />
		<display:setProperty name="export.pdf.filename"
			value="receipt List.pdf" />
	</display:table>

	<c:if test="${paginatedList.fullListSize > 0}">
		<div class="buttonBar bottom"><s:submit key="button.export" action="exportReceiptDetail"
			theme="simple" /></div>
	</c:if>
	<ul style="border: 0px; padding: 0px; margin: 0px">
</s:form>

<script type="text/javascript">
    highlightTableRows("receiptDetail");    
</script>