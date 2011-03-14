<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="deliveryOrderList.title" /></title>
<meta name="heading"
	content="<fmt:message key='deliveryOrderList.heading'/>" />
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
		onclick="location.href='<c:url value="/deliveryOrders2.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<display:table name="deliveryOrderDetailList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="deliveryOrders" class="table"
	export="true">
	<display:column property="createDate" sortable="true"
		sortProperty="createDate"  url="/editDeliveryOrder.html?from=list2"
		paramId="doNo" paramProperty="doNo" titleKey="deliveryOrder.createDate" format="{0, date, MM/dd/yyyy HH:mm:ss}" />
	<display:column property="plantCode" sortable="true"
		sortProperty="p.code" titleKey="deliveryOrder.plantCode" />
	<display:column property="title" sortable="true" sortProperty="title"
		titleKey="deliveryOrder.title" />
	<display:column property="supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="deliveryOrder.supplierName" />
	<display:column property="plantContactPerson" sortable="true"
		sortProperty="plantContactPerson"
		titleKey="deliveryOrder.plantContactPerson" />
	<display:column property="firstReadDate" sortable="true"
		sortProperty="firstReadDate" titleKey="deliveryOrder.firstReadDate" format="{0, date, MM/dd/yyyy HH:mm:ss}"/>
	<display:column property="externalDoNo" sortable="true"
		sortProperty="externalDoNo" titleKey="deliveryOrder.doNo" />
	<display:column property="murn" sortable="true"
		titleKey="deliveryOrder.murn" />
	<display:column property="status" sortable="true"
		titleKey="deliveryOrder.status" />
	<display:column sortable="true" titleKey="deliveryOrder.isPrint">
		<input type="checkbox" disabled="disabled"
			<c:if test="${deliveryOrders.isPrint}">checked="checked"</c:if> />
	</display:column>
	<display:column sortable="true" titleKey="deliveryOrder.isExport">
		<input type="checkbox" disabled="disabled"
			<c:if test="${deliveryOrders.isExport}">checked="checked"</c:if> />
	</display:column>

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
