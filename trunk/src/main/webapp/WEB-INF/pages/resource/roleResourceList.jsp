<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="roleList.title" /></title>
<meta name="heading" content="<fmt:message key='roleList.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>


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

</display:table>

<script type="text/javascript">
    highlightTableRows("roles");
</script>

