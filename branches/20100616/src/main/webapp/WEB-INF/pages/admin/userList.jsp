<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="userList.title" /></title>
<meta name="heading" content="<fmt:message key='userList.heading'/>" />
<c:choose>
	<c:when
		test="${roleType == 'ROLE_ADMIN' or roleType == 'ROLE_PLANT_ADMIN' or roleType == 'ROLE_VENDOR'}">
		<meta name="menu" content="AdminMenu" />
	</c:when>
	<c:when
		test="${roleType == 'ROLE_PLANT_USER'}">
		<meta name="menu" content="PlantAdminMenu" />
	</c:when>
</c:choose>
</head>

<c:set var="buttons">
	<c:choose>
		<c:when test="${roleType == 'ROLE_ADMIN' or roleType == 'ROLE_PLANT_ADMIN' or roleType == 'ROLE_VENDOR'}">
			<input type="button" style="margin-right: 5px"
				onclick="location.href='<c:url value="/editAdmin.html?method=Add&from=list&roleType=${roleType}"/>'"
				value="<fmt:message key="button.add"/>" />
		</c:when>
		<c:when test="${roleType == 'ROLE_PLANT_USER'}">
			<input type="button" style="margin-right: 5px"
				onclick="location.href='<c:url value="/editUser.html?method=Add&from=list&roleType=${roleType}"/>'"
				value="<fmt:message key="button.add"/>" />
		</c:when>
	</c:choose>
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<c:out value="${buttons}" escapeXml="false" />

<display:table name="users" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="users" pagesize="25" class="table"
	export="true">
	<c:choose>
		<c:when test="${roleType == 'ROLE_ADMIN' or roleType == 'ROLE_PLANT_ADMIN' or roleType == 'ROLE_VENDOR'}">
			<display:column property="username" escapeXml="true" sortable="true"
				titleKey="user.username" style="width: 25%"
				url="/editAdmin.html?from=list&roleType=${roleType}" paramId="id"
				paramProperty="id" />
		</c:when>
		<c:when test="${roleType == 'ROLE_PLANT_USER'}">
			<display:column property="username" escapeXml="true" sortable="true"
				titleKey="user.username" style="width: 25%"
				url="/editUser.html?from=list&roleType=${roleType}" paramId="id"
				paramProperty="id" />
		</c:when>
	</c:choose>
	<display:column property="fullName" escapeXml="true" sortable="true"
		titleKey="activeUsers.fullName" style="width: 34%" />
	<display:column property="email" sortable="true" titleKey="user.email"
		style="width: 25%" autolink="true" media="html" />
	<display:column property="email" titleKey="user.email"
		media="csv xml excel pdf" />
	<display:column sortProperty="enabled" sortable="true"
		titleKey="user.enabled" style="width: 16%; padding-left: 15px"
		media="html">
		<input type="checkbox" disabled="disabled"
			<c:if test="${users.enabled}">checked="checked"</c:if> />
	</display:column>
	<display:column property="enabled" titleKey="user.enabled"
		media="csv xml excel pdf" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="user.user" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="user.users" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename" value="User List.xls" />
	<display:setProperty name="export.csv.filename" value="User List.csv" />
	<display:setProperty name="export.pdf.filename" value="User List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("users");
</script>
