<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantSupplierList.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantSupplierList.heading'/>" />
<meta name="menu" content="AdminMenu" />
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
		 DWRUtil.removeAllOptions("suppliers_plantSupplier_supplier_code");
		 if (suppliers != null) {		 
			 DWRUtil.addOptions("suppliers_plantSupplier_supplier_code",[{ name:'All', code:'-1' }], "code", "name");    
		 	DWRUtil.addOptions("suppliers_plantSupplier_supplier_code",suppliers, "code", "name");    
		 }
	}
</script>
</head>
<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/editSupplier.html?from=list"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/exportSupplier.html"/>'"
		value="<fmt:message key="button.export"/>" />
</c:set>
<s:form name="supplierForm" action="suppliers" method="post"
	validate="true">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.plant.code"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple"
				onchange="cascadeUpdateSupplier(this);" /></td>

			<td><label class="desc"><fmt:message
				key="plantSupplier.supplier" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.supplier.code"
				list="%{suppliers}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>
		</tr>
	</table>
	</li>
	<div><c:out value="${buttons}" escapeXml="false" /></div>
</s:form>

<c:if test="${plantSupplier != null}">
	<display:table name="plantSuppliers" cellspacing="0" cellpadding="0"
		requestURI="" defaultsort="1" id="plantSuppliers" pagesize="25"
		class="table" export="true">
		<display:column property="supplier.code" sortable="true"
			url="/editSupplier.html?from=list" paramId="id" paramProperty="id"
			titleKey="plantSupplier.supplierCode" />
		<display:column property="supplierName" sortable="true"
			titleKey="plantSupplier.supplier" />
		<display:column property="plant.name" sortable="true"
			titleKey="plantSupplier.plant" />
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

	<script type="text/javascript">
    highlightTableRows("plantSuppliers");
</script>
</c:if>
