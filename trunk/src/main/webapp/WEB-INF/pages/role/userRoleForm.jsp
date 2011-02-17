<%@ include file="/common/taglibs.jsp"%>
<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>
<head>
<title><fmt:message key="userRole.title" /></title>
<meta name="heading" content="<fmt:message key='userRole.heading'/>" />
<meta name="menu" content="InfoMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form id="userRoleForm" action="editUserRole"
	enctype="multipart/form-data" method="post" validate="true">
	
	<c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			onclick="onFormSubmit()" theme="simple" />
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="userRole.Role" /></label></td>
			<td colspan="2"><s:textfield key="role.Name"
				readonly="true" theme="simple" /></td>
		</tr>
	</table>
	
	
	
	<table class="pickList" style="width: 100%">
		<tr>
			<th class="pickLabel"><label class="required"><fmt:message
				key="userRole.availableUsers" /></label></th>
			<td></td>
			<th class="pickLabel"><label class="required"><fmt:message
				key="userRole.users" /></label></th>
		</tr>
		<c:set var="leftList" value="${availableUsers}" scope="request" />
		<s:set name="rightList" value="role.userList" scope="request" />
		<c:import url="/WEB-INF/pages/pickList.jsp">
			<c:param name="listCount" value="1" />
			<c:param name="leftId" value="availableUsers" />
			<c:param name="rightId" value="users" />
		</c:import>
	</table>
	
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("userRoleForm"));
    highlightFormElements();
    
    function onFormSubmit() {
    	selectAll("users");
    }
</script>