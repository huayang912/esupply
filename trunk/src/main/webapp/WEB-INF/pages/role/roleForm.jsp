<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="roleDetail.title" /></title>
<meta name="heading" content="<fmt:message key='roleDetail.heading'/>" />
		<meta name="menu" content="AdminMenu" />
</head>

<s:form id="roleForm" action="saveRole" method="post" validate="true">
	<li style="display:none"><input type="hidden" name="from"
		value="${param.from}" /></li>
	<c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			theme="simple" />
		<c:if test="${param.from == 'list' and not empty role.id}">
			<s:submit cssClass="button" method="delete" key="button.delete"
				onclick="return confirmDelete('role')" theme="simple" />
		</c:if>
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set>
	<li>
	<div>
	<div class="left"><s:textfield key="role.name" theme="xhtml"
		required="true" cssClass="text medium" />
		<s:hidden key="role.Id" /></div>
	<div><s:textfield key="role.description" theme="xhtml" required="true"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("roleForm"));
    highlightFormElements();
</script>