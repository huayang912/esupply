<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="itemList.title" /></title>
<meta name="heading" content="<fmt:message key='itemList.heading'/>" />
</head>
<c:set var="buttons">
	<input type="button" 
		onclick="location.href='<c:url value="/editItem.html"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="itemForm" action="items" method="post" validate="true">
	<div class="left"><s:textfield key="item.code"
		cssClass="text medium" /></div>
	<div><s:textfield key="item.description" cssClass="text medium" /></div>
	<div><s:submit method="list" key="button.search" theme="simple" />
	<c:out value="${buttons}" escapeXml="false" /></div>
</s:form>

<display:table name="items" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="items" pagesize="25" class="table"
	export="true">
	<display:column property="code" sortable="true" url="/editItem.html"
		paramId="id" paramProperty="id" titleKey="item.code" />
	<display:column property="description" sortable="true"
		titleKey="item.description" />
	<display:column property="uom" sortable="true" titleKey="item.uom" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="item.item" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="item.items" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename" value="item List.xls" />
	<display:setProperty name="export.csv.filename" value="item List.csv" />
	<display:setProperty name="export.pdf.filename" value="item List.pdf" />
</display:table>
<c:out value="${buttons}" escapeXml="false" />
<script type="text/javascript">
    highlightTableRows("items");
</script>