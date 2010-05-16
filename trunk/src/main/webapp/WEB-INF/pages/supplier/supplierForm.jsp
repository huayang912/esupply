<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantSupplierDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantSupplierDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>

<c:choose>
	<c:when
		test="${editProfile}">
		<meta name="menu" content="SupplierProfileMenu" />
	</c:when>
	<c:otherwise>
		<meta name="menu" content="PlantUserMenu" />
	</c:otherwise>
</c:choose>
</head>

<s:form name="plantSupplierForm" action="saveSupplier"
	method="post" validate="true">
	<li style="display: none"><s:hidden key="plantSupplier.id" /> <s:hidden
		key="plantSupplier.plant.code" /></li>
	<c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />
		<s:submit key="button.cancel" method="cancel" />
	</c:set> 
	
	<li><s:textfield key="plantSupplier.supplier.code"
	    title="plantSupplier.supplierCode"
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
