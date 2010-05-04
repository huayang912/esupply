<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantDetail.title" /></title>
<meta name="heading" content="<fmt:message key='plantDetail.heading'/>" />
</head>

<s:form id="plantForm" action="savePlant" method="post" validate="true">
	<li>
	<div>
	<div class="left"><s:textfield key="plant.code" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	<div><s:textfield key="plant.name" theme="xhtml" required="true"
		cssClass="text medium" /></div>
	</div>
	</li>
	<s:textfield key="plant.address1" theme="xhtml" cssClass="text large" />
	<s:textfield key="plant.address2" theme="xhtml" cssClass="text large" />
	<li>
	<div>
	<div class="left"><s:textfield key="plant.contactPerson"
		theme="xhtml" cssClass="text medium" /></div>
	<div><s:textfield key="plant.phone" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>
	<s:textfield key="plant.fax" theme="xhtml" cssClass="text medium" />

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