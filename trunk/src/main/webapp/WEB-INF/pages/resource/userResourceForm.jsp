<%@ include file="/common/taglibs.jsp"%>
<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>
<head>
<title><fmt:message key="userResource.title" /></title>
<meta name="heading" content="<fmt:message key='userResource.heading'/>" />
<meta name="menu" content="AdminMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form id="userResourceForm" action="editUserResource"
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
				key="userResource.User" /></label></td>
			<td colspan="2"><s:textfield key="user.Username"
				readonly="true" theme="simple" /></td>

			<td><label class="desc"><fmt:message key="resource.Type" /></label></td>
			<td colspan="2"><s:select key="type"
				list="%{types}" theme="simple" /></td>
				<td><s:submit method="list" key="button.search" theme="simple" /></td>
		</tr>
	</table>
	
	
	
	<table class="pickList" style="width: 100%">
		<tr>
			<th class="pickLabel"><label class="required"><fmt:message
				key="userResource.availableResources" /></label></th>
			<td></td>
			<th class="pickLabel"><label class="required"><fmt:message
				key="userResource.resources" /></label></th>
		</tr>
		<c:set var="leftList" value="${availableResources}" scope="request" />
		<s:set name="rightList" value="user.resourceList" scope="request" />
		<c:import url="/WEB-INF/pages/pickList.jsp">
			<c:param name="listCount" value="1" />
			<c:param name="leftId" value="availableResources" />
			<c:param name="rightId" value="resources" />
		</c:import>
	</table>
	
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("userResourceForm"));
    highlightFormElements();
    
    function onFormSubmit() {
    	selectAll("resources");
    }
</script>