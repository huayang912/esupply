<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantDetail.title" /></title>
<meta name="heading" content="<fmt:message key='plantDetail.heading'/>" />
</head>

<s:form id="plantForm" action="savePlant" method="post"
	validate="true">
	<s:textfield key="plant.code" required="true" cssClass="text medium" />

	<s:textfield key="plant.name" required="true" cssClass="text medium" />

	<li class="buttonBar bottom"><s:submit cssClass="button"
		method="save" key="button.save" theme="simple" /> <c:if
		test="${not empty plant.code}">
		<s:submit cssClass="button" method="delete" key="button.delete"
			onclick="return confirmDelete('plant')" theme="simple" />
	</c:if> <s:submit cssClass="button" method="cancel" key="button.cancel"
		theme="simple" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("plantForm"));
</script>