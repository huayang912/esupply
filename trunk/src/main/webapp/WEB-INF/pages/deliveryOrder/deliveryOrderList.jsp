<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="deliveryOrderList.title" /></title>
<meta name="heading"
	content="<fmt:message key='deliveryOrderList.heading'/>" />
<meta name="menu" content="OrderMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>

<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="deliveryOrderForm" action="deliveryOrders" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.doNo" /></label></td>
			<td colspan="2"><s:textfield key="deliveryOrder.externalDoNo"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.plantCode" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.pCode"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>

			<td><label class="desc"><fmt:message
				key="deliveryOrder.supplierCode" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.sCode"
				list="%{suppliers}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.status" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.status"
				list="%{status}" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.isPrint" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.printFlag"
				list="%{isPrint}" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.createDateFrom" /></label></td>
			<td><s:textfield key="deliveryOrder.createDateFrom"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['deliveryOrderForm'].deliveryOrders_deliveryOrder_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.createDateTo" /></label></td>
			<td><s:textfield key="deliveryOrder.createDateTo"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['deliveryOrderForm'].deliveryOrders_deliveryOrder_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${deliveryOrder != null}">
	<display:table name="paginatedList" cellspacing="0" cellpadding="0"
		requestURI="" defaultsort="1" id="deliveryOrders" class="table"
		export="true">
		<display:column property="externalDoNo" sortable="true"
			sortProperty="externalDoNo" url="/editDeliveryOrder.html"
			paramId="doNo" paramProperty="doNo" titleKey="deliveryOrder.doNo" />
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

	<script type="text/javascript">
    highlightTableRows("deliveryOrders");    
</script>
</c:if>
