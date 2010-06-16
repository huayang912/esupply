<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="supplierItemDetail.title" /></title>
<meta name="heading" content="<fmt:message key='supplierItemDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
<meta name="menu" content="SupplierMenu" />
</head>

<s:form name="supplierItemForm" action="saveSupplierItem" method="post" validate="true">
	<li style="display: none"><s:hidden key="supplierItem.id" />
	<s:hidden key="supplierItem.item.code" />
	<s:hidden key="supplier.code" /></li>
	<c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />
		<s:submit key="button.cancel" method="cancel" />
	</c:set>

	<li><s:label key="supplierItem.item.code" cssClass="text medium" /></li>
	
	<li><s:textfield key="supplierItem.supplierItemCode" cssClass="text medium" /></li>

	<li><s:label key="supplierItem.item.description" cssClass="text large" /></li>
	
	<li><s:label key="supplierItem.item.unitCount" cssClass="text medium" /></li>
	
	<li><s:label key="supplierItem.item.uom" cssClass="text medium" /></li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement(document.forms["supplierItemForm"]);
    highlightFormElements();
</script>
