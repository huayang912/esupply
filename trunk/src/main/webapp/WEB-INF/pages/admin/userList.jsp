<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="userList.title" /></title>
<meta name="heading" content="<fmt:message key='userList.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>

<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button" style="margin-right: 5px"
		onclick="location.href='<c:url value="/editUser.html?method=Add&from=list"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="userFrom" action="users" method="post" validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message key="user.username" /></label></td>
			<td colspan="2"><s:textfield key="user.username"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message key="user.email" /></label></td>
			<td colspan="2"><s:textfield key="user.email"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="user.firstName" /></label></td>
			<td colspan="2"><s:textfield key="user.firstName"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message key="user.lastName" /></label></td>
			<td colspan="2"><s:textfield key="user.lastName"
				cssClass="text medium" theme="simple" /></td>
		</tr>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${user != null}">
	<display:table name="users" cellspacing="0" cellpadding="0"
		requestURI="" defaultsort="1" id="users" pagesize="25" class="table"
		export="true">
		<display:column property="username" escapeXml="true" sortable="true"
			titleKey="user.username" style="width: 25%"
			url="/editUser.html?from=list" paramId="id" paramProperty="id" />
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

		<display:setProperty name="export.excel.filename"
			value="User List.xls" />
		<display:setProperty name="export.csv.filename" value="User List.csv" />
		<display:setProperty name="export.pdf.filename" value="User List.pdf" />
	</display:table>

	<script type="text/javascript">
    highlightTableRows("users");
</script>
</c:if>
