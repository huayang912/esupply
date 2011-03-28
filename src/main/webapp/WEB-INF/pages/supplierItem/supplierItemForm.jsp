<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="supplierItemDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='supplierItemDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
<meta name="menu" content="SupplierMenu" />
</head>

<s:form name="supplierItemForm" action="saveSupplierItem" method="post"
	validate="true">
	<c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />
		<c:if test="${not empty supplierItem.id}">
			<s:submit cssClass="button" method="delete" key="button.delete"
				onclick="return confirmDelete('supplier item')" theme="simple" />
		</c:if>
		<s:submit key="button.cancel" method="cancel" />
	</c:set>

	<c:if test="${empty supplierItem.id}">
		<li><s:select key="supplierItem.supplier.code"
			list="%{suppliers}" listKey="supplier.code" listValue="supplierName" /></li>

		<li><s:textfield key="supplierItem.item.code"
			cssClass="text medium" /></li>
	</c:if>

	<c:if test="${not empty supplierItem.id}">
		<li style="display: none"><s:hidden key="supplierItem.id" /><s:hidden
			key="supplierItem.supplier.code" /><s:hidden
			key="supplierItem.item.code" cssClass="text medium" /></li>

		<li><s:label key="supplierItem.supplier.name"
			cssClass="text medium" /></li>

		<li><s:label key="supplierItem.item.code" cssClass="text medium" /></li>
	</c:if>

	<li><s:textfield key="supplierItem.supplierItemCode"
		cssClass="text medium" /></li>

	<li><s:label key="supplierItem.item.description"
		cssClass="text large" /></li>

	<li><s:label key="supplierItem.item.unitCount"
		cssClass="text medium" /></li>

	<li><s:label key="supplierItem.item.uom" cssClass="text medium" /></li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement(document.forms["supplierItemForm"]);
    highlightFormElements();
</script>
