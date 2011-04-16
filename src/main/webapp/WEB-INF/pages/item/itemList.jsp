<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="itemList.title" /></title>
<meta name="heading" content="<fmt:message key='itemList.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>
<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/editItem.html"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/exportItem.html"/>'"
		value="<fmt:message key="button.export"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="itemForm" action="items" method="post" validate="true">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="item.plant.code" /></label> <s:select key="item.plant.code"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>
		</tr>
	</table>
	<div align="left"><s:textfield key="item.code"
		cssClass="text medium" /></div>
	<div><s:textfield key="item.description" cssClass="text medium" /></div>

	<div class="formbotton"><c:out value="${buttons}"
		escapeXml="false" /></div>
</s:form>

<c:if test="${item != null}">
	<display:table name="items" cellspacing="0" cellpadding="0"
		requestURI="" id="items" pagesize="25" class="table"
		export="true">
		<display:column property="code" sortable="true" url="/editItem.html"
			paramId="id" paramProperty="id" titleKey="item.code" />
		<display:column property="plant.name" sortable="true"
			titleKey="item.plant.code" />
		<display:column property="description" sortable="true"
			titleKey="item.description" />
		<display:column property="unitCount" sortable="true"
			titleKey="item.unitCount" />
		<display:column property="uom" sortable="true" titleKey="item.uom" />

		<display:setProperty name="paging.banner.item_name">
			<fmt:message key="item.item" />
		</display:setProperty>
		<display:setProperty name="paging.banner.items_name">
			<fmt:message key="item.items" />
		</display:setProperty>

		<display:setProperty name="export.excel.filename"
			value="item List.xls" />
		<display:setProperty name="export.csv.filename" value="item List.csv" />
		<display:setProperty name="export.pdf.filename" value="item List.pdf" />
	</display:table>

	<script type="text/javascript">
    highlightTableRows("items");
</script>
</c:if>