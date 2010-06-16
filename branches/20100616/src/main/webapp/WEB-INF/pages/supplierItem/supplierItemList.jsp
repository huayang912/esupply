<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="supplierItemList.title" /></title>
<meta name="heading" content="<fmt:message key='itemList.heading'/>" />
<meta name="menu" content="SupplierMenu" />
</head>
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="supplierItemForm" action="supplierItems" method="post" validate="true">
	<div class="left"><s:textfield key="supplierItem.item.code"
		cssClass="text medium" /></div>
	<div><s:textfield key="supplierItem.supplierItemCode" cssClass="text medium" /></div>
	<div class="formbotton"><s:submit method="list" key="button.search" theme="simple" />
	</div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="supplierItems" pagesize="25" class="table"
	export="true">
	<display:column property="item.code" sortable="true" url="/editSupplierItem.html"
		paramId="id" paramProperty="id" titleKey="item.code" />
	<display:column property="supplierItemCode" sortable="true"
		titleKey="supplierItem.supplierItemCode" />
	<display:column property="item.description" sortable="true"
		titleKey="item.description" />
	<display:column property="item.unitCount" sortable="true" titleKey="item.unitCount" />
	<display:column property="item.uom" sortable="true" titleKey="item.uom" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="supplierItem.supplierItem" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="supplierItem.supplierItems" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename" value="supplierItem List.xls" />
	<display:setProperty name="export.csv.filename" value="supplierItem List.csv" />
	<display:setProperty name="export.pdf.filename" value="supplierItem List.pdf" />
</display:table>
<c:out value="${buttons}" escapeXml="false" />
<script type="text/javascript">
    highlightTableRows("supplierItems");
</script>