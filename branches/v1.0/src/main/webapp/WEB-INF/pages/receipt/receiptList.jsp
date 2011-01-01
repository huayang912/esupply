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
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="receiptForm" action="receipts" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.referenceReceiptNoLong" /></label></td>
			<td colspan="2"><s:textfield key="receipt.referenceReceiptNoLong"
				cssClass="text medium" theme="simple" /></td>
			<c:if test="<%=request.isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)%>">
				<td><label class="desc"><fmt:message
					key="receipt.supplierCode" /></label></td>
				<td colspan="2"><s:select key="receipt.plantSupplier.id"
					list="%{suppliers}" listKey="id"
					listValue="supplierName" theme="simple" /></td>
			</c:if>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.referenceReceiptNo" /></label></td>
			<td colspan="2"><s:textfield key="receipt.referenceReceiptNo"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="receipt.receiptNo" /></label></td>
			<td colspan="2"><s:textfield key="receipt.receiptNo"
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
	</table>
	</li>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" id="receipts" class="table" export="true">
	<display:column property="referenceReceiptNoLong" sortable="true" sortProperty="referenceReceiptNoLong"
		url="/editReceipt.html" paramId="receiptNo" paramProperty="receiptNo"
		titleKey="receipt.referenceReceiptNoLong" />
	<display:column property="referenceReceiptNo" sortable="true"
		sortProperty="referenceReceiptNo" titleKey="receipt.referenceReceiptNo" />
	<display:column property="receiptNo" sortable="true"
		sortProperty="receiptNo" titleKey="receipt.receiptNo" />	
	<display:column property="plantCode" sortable="true"
		sortProperty="p.code" titleKey="receipt.plantCode" />
	<display:column property="plantName" sortable="true"
		sortProperty="p.name" titleKey="receipt.plantName" />
	<display:column property="supplierCode" sortable="true"
		sortProperty="s.code" titleKey="receipt.supplierCode" />
	<display:column property="supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="receipt.supplierName" />
	<display:column property="postingDate" format="{0,date,MM/dd/yyyy}"
		sortable="true" titleKey="receipt.postingDate" />

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

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("receipts");    
</script>
