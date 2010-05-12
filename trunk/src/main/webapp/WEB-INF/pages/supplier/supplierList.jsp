<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="supplierList.title" /></title>
<meta name="heading" content="<fmt:message key='supplierList.heading'/>" />
</head>

<s:form name="supplierForm" action="suppliers" method="post"
	validate="true">
	<li>
	<div>
	<div class="left"><s:textfield key="plantSupplier.supplier.code"
		cssClass="text medium" /></div>
	<div><s:textfield key="plantSupplier.supplierName"
		cssClass="text medium" /></div>
	</div>
	</li>
	<s:submit cssClass="button" method="list" key="button.search"
		theme="simple" />
</s:form>

<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<c:out value="${buttons}" escapeXml="false" />

<display:table name="plantSuppliers" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="plantSuppliers" pagesize="25"
	class="table" export="true">
	<display:column property="supplier.code" sortable="true"
		url="/editSupplier.html" paramId="id" paramProperty="id"
		titleKey="plantSupplier.supplier.code" />
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

	<display:setProperty name="paging.banner.plantSupplier_name"
		value="plantSupplier" />
	<display:setProperty name="paging.banner.plantSuppliers_name"
		value="plantSuppliers" />

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
