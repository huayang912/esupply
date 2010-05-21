<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="purchaseOrderList.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderList.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/DateTimeCalendar.js'/>"></script>
</head>
<meta name="menu" content="OrderMenu" />
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
	<s:textfield key="purchaseOrder.createDateFrom"
		cssClass="text medium" />
	<li><s:textfield key="purchaseOrder.createDateTo"
		cssClass="text medium" /></li>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="purchaseOrders" class="table"
	export="true">
	<display:column property="poNo" sortable="true"
		url="/editPurchaseOrder.html" paramId="poNo" paramProperty="poNo"
		titleKey="purchaseOrder.poNo" />
	<display:column property="plantSupplier.plant.code" sortable="true"
		sortProperty="p.code" titleKey="purchaseOrder.plantCode" />
	<display:column property="plantSupplier.plant.name" sortable="true"
		sortProperty="p.name" titleKey="purchaseOrder.plantName" />
	<display:column property="plantSupplier.supplier.code" sortable="true"
		sortProperty="s.code" titleKey="purchaseOrder.supplierCode" />
	<display:column property="plantSupplier.supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="purchaseOrder.supplierName" />
	<display:column property="createDate" format="{0,date,MM/dd/yyyy}"
		sortable="true" titleKey="purchaseOrder.createDate" />
	<display:column property="status" sortable="true"
		titleKey="purchaseOrder.status" />

	<display:setProperty name="paging.banner.item_name"
		value="Purchase Order" />
	<display:setProperty name="paging.banner.items_name"
		value="Purchase Orders" />

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
