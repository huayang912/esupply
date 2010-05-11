<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="itemDetail.title" /></title>
<meta name="heading" content="<fmt:message key='itemDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form name="itemForm" action="saveItem" method="post" validate="true">
	<li style="display: none"><s:hidden key="item.id" /> <input
		type="hidden" name="from" value="${param.from}" /> <input
		type="hidden" name="roleType" value="${roleType}" /></li>
	<li class="buttonBar right"><c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />
		<s:submit key="button.cancel" method="cancel" />
	</c:set> <c:out value="${buttons}" escapeXml="false" /></li>
	<li class="info">
	<p><fmt:message key="itemDetail.info.message" /></p>
	</li>

	<li><s:textfield key="item.code" cssClass="text large"
		required="true" /></li>

	<li><s:textfield key="item.description" cssClass="text large"
		required="true" /></li>

	<li><s:textfield key="item.Uom" required="true"
		cssClass="text medium" /></li>
	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement(document.forms["itemForm"]);
    highlightFormElements();

<!-- This is here so we can exclude the selectAll call when roles is hidden -->
function onFormSubmit(theForm) {
<c:if test="${param.from == 'list'}">
    selectAll('itemRoles');
</c:if>
}
</script>
