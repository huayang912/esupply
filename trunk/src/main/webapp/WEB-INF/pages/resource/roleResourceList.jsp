<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="roleList.title" /></title>
<meta name="heading" content="<fmt:message key='roleList.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>

<c:set var="buttons">
	<input type="button" style="margin-right: 5px"
		onclick="location.href='<c:url value="/editRoleResource.html?method=Add&from=list"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<display:table name="roles" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="roles" pagesize="25" class="table"
	export="true">
	<display:column property="name" escapeXml="true" sortable="true"
		titleKey="role.name" style="width: 25%"
		url="/editRoleResource.html?from=list" paramId="name" paramProperty="name" />
	<display:column property="description" escapeXml="true" sortable="true"
		titleKey="role.description"/>

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="role.role" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="role.roles" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename" value="Role List.xls" />
	<display:setProperty name="export.csv.filename" value="Role List.csv" />
	<display:setProperty name="export.pdf.filename" value="Role List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("roles");
</script>

