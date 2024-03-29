<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="purchaseOrderList.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderList.heading'/>" />
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
		 DWRUtil.removeAllOptions("purchaseOrders_purchaseOrder_sCode");
		 if (suppliers != null) {		 
			 DWRUtil.addOptions("purchaseOrders_purchaseOrder_sCode",[{ name:'All', code:'-1' }], "code", "name");    
		 	DWRUtil.addOptions("purchaseOrders_purchaseOrder_sCode",suppliers, "code", "name");    
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
<s:form name="purchaseOrderForm" action="purchaseOrders" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="purchaseOrder.plantCode" /></label></td>
			<td colspan="2"><s:select key="purchaseOrder.pCode"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" onchange="cascadeUpdateSupplier(this);"/></td>

			<td><label class="desc"><fmt:message
				key="purchaseOrder.supplierCode" /></label></td>
			<td colspan="2"><s:select key="purchaseOrder.sCode"
				list="%{suppliers}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="purchaseOrder.poNo" /></label></td>
			<td colspan="2"><s:textfield key="purchaseOrder.poNo"
				cssClass="text medium" theme="simple" /></td>

			<td><label class="desc"><fmt:message
				key="purchaseOrder.status" /></label></td>
			<td colspan="2"><s:select key="purchaseOrder.status"
				list="%{status}" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="purchaseOrder.createDateFrom" /></label></td>
			<td><s:textfield key="purchaseOrder.createDateFrom"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['purchaseOrderForm'].purchaseOrders_purchaseOrder_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
			<td><label class="desc"><fmt:message
				key="purchaseOrder.createDateTo" /></label></td>
			<td><s:textfield key="purchaseOrder.createDateTo"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['purchaseOrderForm'].purchaseOrders_purchaseOrder_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${purchaseOrder != null}">
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

	<script type="text/javascript">
    highlightTableRows("purchaseOrders");    
</script>
</c:if>