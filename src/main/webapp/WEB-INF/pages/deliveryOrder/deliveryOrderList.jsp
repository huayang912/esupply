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
<script type="text/javascript">
	function cascadeUpdateSupplier(plantSelect) {
		if (plantSelect.options(plantSelect.selectedIndex).value != "-1") {	
			SupplierManager.getSuppliersByPlantAndUser(plantSelect.options(plantSelect.selectedIndex).value + "|${pageContext.request.remoteUser}", supplierSelectHandler);
		}
		else
		{
			SupplierManager.getAuthorizedSupplier("${pageContext.request.remoteUser}", supplierSelectHandler);
		}
	}

	function supplierSelectHandler(suppliers) {
		 DWRUtil.removeAllOptions("deliveryOrders_deliveryOrder_sCode");
		 if (suppliers != null) {		 
			 DWRUtil.addOptions("deliveryOrders_deliveryOrder_sCode",[{ name:'All', code:'-1' }], "code", "name");    
		 	DWRUtil.addOptions("deliveryOrders_deliveryOrder_sCode",suppliers, "code", "name");    
		 }
	}
</script>
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
				headerValue="All" theme="simple" onchange="cascadeUpdateSupplier(this);"/></td>

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
				key="deliveryOrder.isRead" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.readFlag"
				list="%{isRead}" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.isPrint" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.printFlag"
				list="%{isPrint}" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="deliveryOrder.isExport" /></label></td>
			<td colspan="2"><s:select key="deliveryOrder.ExportFlag"
				list="%{isExport}" theme="simple" /></td>
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
		requestURI="" id="deliveryOrders" class="table"
		export="true">
		<display:column property="createDate" sortable="true"
			sortProperty="createDate" url="/editDeliveryOrder.html?from=list"
			paramId="doNo" paramProperty="doNo"
			titleKey="deliveryOrder.createDate"
			format="{0, date, MM/dd/yyyy HH:mm:ss}" />
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
			sortProperty="firstReadDate" titleKey="deliveryOrder.firstReadDate"
			format="{0, date, MM/dd/yyyy HH:mm:ss}" />
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
</c:if>

<script type="text/javascript">
    highlightTableRows("deliveryOrders");    
</script>
