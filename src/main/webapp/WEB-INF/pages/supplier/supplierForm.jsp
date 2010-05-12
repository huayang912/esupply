<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantSupplierDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantSupplierDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form name="plantSupplierForm" action="saveSupplier"
	method="post" validate="true">
	<li style="display: none"><s:hidden key="plantSupplier.id" /> <s:hidden
		key="plantSupplier.plant.code" /></li>
	<li class="buttonBar right"><c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />
		<s:submit key="button.cancel" method="cancel" />
	</c:set> <c:out value="${buttons}" escapeXml="false" /></li>
	<li class="info">
	<p><fmt:message key="plantSupplierDetail.info.message" /></p>
	</li>

	<li><s:textfield key="plantSupplier.supplier.code"
		cssClass="text large" required="true" /></li>

	<li><s:textfield key="plantSupplier.supplierName"
		cssClass="text large" required="true" /></li>

	<li><s:textfield key="plantSupplier.supplierAddress1"
		cssClass="text large" required="true" /></li>

	<li><s:textfield key="plantSupplier.supplierAddress2"
		cssClass="text large" required="true" /></li>

	<li><s:textfield key="plantSupplier.supplierContactPerson"
		cssClass="text large" required="true" /></li>
		
	<li><s:textfield key="plantSupplier.supplierPhone"
		cssClass="text large" required="true" /></li>
		
	<li><s:textfield key="plantSupplier.supplierFax"
		cssClass="text large" required="true" /></li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["plantSupplierForm"]);
		highlightFormElements();
</script>
