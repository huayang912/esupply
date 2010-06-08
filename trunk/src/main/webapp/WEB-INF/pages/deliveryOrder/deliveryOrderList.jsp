<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="deliveryOrderList.title" /></title>
<meta name="heading"
	content="<fmt:message key='deliveryOrderList.heading'/>" />
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
<s:form name="deliveryOrderForm" action="deliveryOrders" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<s:textfield key="deliveryOrder.doNo" cssClass="text medium" />
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="deliveryOrder.createDateFrom"
				cssClass="text medium" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['deliveryOrderForm'].deliveryOrders_deliveryOrder_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="deliveryOrder.createDateTo"
				cssClass="text medium" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['deliveryOrderForm'].deliveryOrders_deliveryOrder_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="deliveryOrders" class="table"
	export="true">
	<display:column property="doNo" sortable="true"
		url="/editDeliveryOrder.html" paramId="doNo" paramProperty="doNo"
		titleKey="deliveryOrder.doNo" />
	<display:column property="plantCode" sortable="true"
		sortProperty="p.code" titleKey="deliveryOrder.plantCode" />
	<display:column property="plantName" sortable="true"
		sortProperty="p.name" titleKey="deliveryOrder.plantName" />
	<display:column property="supplierCode" sortable="true"
		sortProperty="s.code" titleKey="deliveryOrder.supplierCode" />
	<display:column property="supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="deliveryOrder.supplierName" />
	<display:column property="createDate" format="{0,date,MM/dd/yyyy}"
		sortable="true" titleKey="deliveryOrder.createDate" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="deliveryOrder.deliveryOrder" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="deliveryOrder.deliveryOrders" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="deliveryOrder List.xls" />
	<display:setProperty name="export.csv.filename"
		value="deliveryOrder List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="deliveryOrder List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("deliveryOrders");    
</script>
