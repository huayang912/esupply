<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="purchaseOrderList.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderList.heading'/>" />
<c:choose>
	<c:when test="<%=request.isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)%>">
		<meta name="menu" content="PlantOrderMenu" />
	</c:when>
	<c:when test="<%=request.isUserInRole(com.faurecia.Constants.VENDOR_ROLE)%>">
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
<s:form name="purchaseOrderForm" action="purchaseOrders" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<s:textfield key="purchaseOrder.poNo" cssClass="text medium" />
	<li><s:select key="purchaseOrder.status" list="%{status}"
		theme="xhtml" /></li>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="purchaseOrder.createDateFrom"
				cssClass="text medium" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['purchaseOrderForm'].purchaseOrders_purchaseOrder_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>

	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="purchaseOrder.createDateTo"
				cssClass="text medium" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['purchaseOrderForm'].purchaseOrders_purchaseOrder_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="purchaseOrders" class="table"
	export="true">
	<display:column property="poNo" sortable="true"
		url="/editPurchaseOrder.html" paramId="poNo" paramProperty="poNo"
		titleKey="purchaseOrder.poNo" />
	<display:column property="plantCode" sortable="true"
		sortProperty="p.code" titleKey="purchaseOrder.plantCode" />
	<display:column property="plantName" sortable="true"
		sortProperty="p.name" titleKey="purchaseOrder.plantName" />
	<display:column property="supplierCode" sortable="true"
		sortProperty="s.code" titleKey="purchaseOrder.supplierCode" />
	<display:column property="supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="purchaseOrder.supplierName" />
	<display:column property="createDate" format="{0,date,MM/dd/yyyy}"
		sortable="true" titleKey="purchaseOrder.createDate" />
	<display:column property="status" sortable="true"
		titleKey="purchaseOrder.status" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="purchaseOrder.purchaseOrder" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="purchaseOrder.purchaseOrders" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="purchaseOrder List.xls" />
	<display:setProperty name="export.csv.filename"
		value="purchaseOrder List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="purchaseOrder List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("purchaseOrders");    
</script>
