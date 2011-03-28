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
<s:form name="supplierItemForm" action="supplierItems" method="post"
	validate="true">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<c:if
			test="<%=request.isUserInRole(com.faurecia.Constants.PLANT_USER_ROLE)%>">
			<tr>
				<td><label class="desc"><fmt:message
					key="supplierItem.supplier.code" /></label></td>

				<td colspan="2"><s:select key="supplierItem.supplier.code"
					list="%{suppliers}" listKey="supplier.code"
					listValue="supplierName" theme="simple" /></td>
			</tr>
		</c:if>
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
	
	<input
		type="button"
		onclick="location.href='<c:url value="/editSupplierItem.html"/>'"
		value="<fmt:message key="button.add"/>" />
		
		<input type="button"
		onclick="location.href='<c:url value="/importSupplierItem.html"/>'"
		value="<fmt:message key="button.import"/>" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="supplierItems" pagesize="25"
	class="table" export="true">
	<display:column property="item.code" sortable="true"
		url="/editSupplierItem.html" paramId="id" paramProperty="id"
		titleKey="item.code" />
	<display:column property="supplierItemCode" sortable="true"
		titleKey="supplierItem.supplierItemCode" />
	<display:column property="item.description" sortable="true"
		titleKey="item.description" />
	<display:column property="item.unitCount" sortable="true"
		titleKey="item.unitCount" />
	<display:column property="item.uom" sortable="true" titleKey="item.uom" />

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