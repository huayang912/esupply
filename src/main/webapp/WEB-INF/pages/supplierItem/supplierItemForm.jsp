<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="supplierItemDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='supplierItemDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
<meta name="menu" content="AdminMenu" />
<script type="text/javascript">
	function cascadeUpdateSupplier(plantSelect) {
		SupplierManager.getSuppliersByPlantAndUser(plantSelect.options(plantSelect.selectedIndex).value + "|${pageContext.request.remoteUser}", supplierSelectHandler);
	}

	function supplierSelectHandler(suppliers) {
		 DWRUtil.removeAllOptions("saveSupplierItem_supplierItem_supplier_code");
		 if (suppliers != null) {		 
		 	DWRUtil.addOptions("saveSupplierItem_supplierItem_supplier_code",suppliers, "code", "name");    
		 }
	}
</script>
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
		<li><s:select key="supplierItem.pCode" list="%{plants}"
			listKey="code" listValue="name" onchange="cascadeUpdateSupplier(this);"/></li>
	
		<li><s:select key="supplierItem.supplier.code" list="%{suppliers}"
			listKey="code" listValue="name" /></li>

		<li><s:textfield key="supplierItem.item.code"
			cssClass="text medium" /></li>
	</c:if>

	<c:if test="${not empty supplierItem.id}">
		<li style="display: none"><s:hidden key="supplierItem.id" /><s:hidden
			key="supplierItem.supplier.code" /><s:hidden
			key="supplierItem.item.code" cssClass="text medium" /></li>

		<li><s:label key="supplierItem.item.plant.name"
			cssClass="text medium" /></li>

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
    Form.focusFirstElement(document.forms["saveSupplierItem"]);
    highlightFormElements();
</script>
