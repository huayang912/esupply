<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="supplierItemList.title" /></title>
<meta name="heading" content="<fmt:message key='itemList.heading'/>" />
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
		 DWRUtil.removeAllOptions("supplierItems_supplierItem_sCode");
		 if (suppliers != null) {		 
			 DWRUtil.addOptions("supplierItems_supplierItem_sCode",[{ name:'All', code:'-1' }], "code", "name");    
		 	DWRUtil.addOptions("supplierItems_supplierItem_sCode",suppliers, "code", "name");    
		 }
	}
</script>
</head>
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="supplierItemForm" action="supplierItems" method="post"
	validate="true">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="supplierItem.plant.code" /></label></td>
			<td colspan="2"><s:select key="supplierItem.pCode"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple"
				onchange="cascadeUpdateSupplier(this);" /></td>

			<td><label class="desc"><fmt:message
				key="supplierItem.supplier.code" /></label></td>
			<td colspan="2"><s:select key="supplierItem.sCode"
				list="%{suppliers}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="supplierItem.item.code" /></label></td>
			<td colspan="2"><s:textfield key="supplierItem.item.code"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="supplierItem.supplierItemCode" /></label></td>
			<td><s:textfield key="supplierItem.supplierItemCode"
				cssClass="text medium" theme="simple" /></td>
		</tr>
	</table>
	</li>
	<div><s:submit method="list" key="button.search" theme="simple" />

	<input type="button"
		onclick="location.href='<c:url value="/editSupplierItem.html"/>'"
		value="<fmt:message key="button.add"/>" /> <input type="button"
		onclick="location.href='<c:url value="/importSupplierItem.html"/>'"
		value="<fmt:message key="button.import"/>" /></div>
</s:form>

<c:if test="${supplierItem != null}">
	<display:table name="paginatedList" cellspacing="0" cellpadding="0"
		requestURI="" id="supplierItems" pagesize="25" class="table"
		export="true">
		<display:column property="item.code" sortable="true"
			sortProperty="i.code" url="/editSupplierItem.html" paramId="id"
			paramProperty="id" titleKey="item.code" />
		<display:column property="item.plant.name" sortable="true"
			sortProperty="p.name" titleKey="supplierItem.plant.code" />
		<display:column property="supplier.name" sortable="true"
			sortProperty="s.name" titleKey="supplierItem.supplier.name" />
		<display:column property="supplierItemCode" sortable="true"
			titleKey="supplierItem.supplierItemCode" />
		<display:column property="item.description" sortable="true"
			sortProperty="i.description" titleKey="item.description" />
		<display:column property="item.unitCount" sortable="true"
			sortProperty="i.unitCount" titleKey="item.unitCount" />
		<display:column property="item.uom" sortable="true"
			sortProperty="i.uom" titleKey="item.uom" />

		<display:setProperty name="paging.banner.item_name">
			<fmt:message key="supplierItem.supplierItem" />
		</display:setProperty>
		<display:setProperty name="paging.banner.items_name">
			<fmt:message key="supplierItem.supplierItems" />
		</display:setProperty>

		<display:setProperty name="export.excel.filename"
			value="supplierItem List.xls" />
		<display:setProperty name="export.csv.filename"
			value="supplierItem List.csv" />
		<display:setProperty name="export.pdf.filename"
			value="supplierItem List.pdf" />
	</display:table>
	<c:out value="${buttons}" escapeXml="false" />
	<script type="text/javascript">
    highlightTableRows("supplierItems");
</script>
</c:if>
