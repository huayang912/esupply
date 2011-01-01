<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantSupplierList.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantSupplierList.heading'/>" />
	<meta name="menu" content="PlantUserMenu" />
</head>
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="supplierForm" action="suppliers" method="post"
	validate="true">
	<div class="left"><s:textfield key="plantSupplier.supplier.code"
		cssClass="text medium" /></div>
	<div><s:textfield key="plantSupplier.supplierName"
		cssClass="text medium" /></div>
	<div><s:submit method="list" key="button.search" theme="simple" /><c:out
		value="${buttons}" escapeXml="false" /></div>
</s:form>

<display:table name="plantSuppliers" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="plantSuppliers" pagesize="25"
	class="table" export="true">
	<display:column property="supplier.code" sortable="true"
		url="/editSupplier.html?from=list" paramId="id" paramProperty="id"
		titleKey="plantSupplier.supplierCode" />
	<display:column property="supplierName" sortable="true"
		titleKey="plantSupplier.supplierName" />
	<display:column property="supplierAddress1" sortable="true"
		titleKey="plantSupplier.supplierAddress1" />
	<display:column property="supplierAddress2" sortable="true"
		titleKey="plantSupplier.supplierAddress2" />
	<display:column property="supplierContactPerson" sortable="true"
		titleKey="plantSupplier.supplierContactPerson" />
	<display:column property="supplierPhone" sortable="true"
		titleKey="plantSupplier.supplierPhone" />
	<display:column property="supplierFax" sortable="true"
		titleKey="plantSupplier.supplierFax" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="plantSupplier.supplier" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="plantSupplier.suppliers" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="plantSupplier List.xls" />
	<display:setProperty name="export.csv.filename"
		value="plantSupplier List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="plantSupplier List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("plantSuppliers");
</script>
