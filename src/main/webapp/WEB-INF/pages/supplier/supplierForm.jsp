<%@ include file="/common/taglibs.jsp"%>


<%@page import="com.faurecia.Constants"%><head>
<title><fmt:message key="plantSupplierDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantSupplierDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
<meta name="menu" content="AdminMenu" />
</head>

<s:form name="plantSupplierForm" action="saveSupplier" method="post"
	validate="true">
	<li style="display: none"><s:hidden key="plantSupplier.id" /> <s:if
		test="plantSupplier.plant != null">
		<s:hidden key="plantSupplier.plant.code" />
	</s:if> <s:hidden key="plantSupplier.doNoPrefix" /> <input type="hidden"
		name="from" value="${param.from}" /></li>
	<c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />
		<s:submit key="button.cancel" method="cancel" />
	</c:set>

	<s:if test="plantSupplier.plant == null">
		<li><s:select key="plantSupplier.plant.code" list="%{plants}"
			listKey="code" listValue="name" required="true" /></li>
	</s:if>

	<li><s:textfield key="plantSupplier.supplier.code"
		title="plantSupplier.supplierCode" cssClass="text large"
		required="true" /></li>

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

	<li><s:select key="plantSupplier.plantScheduleGroup.id"
		list="%{plantScheduleGroupList}" listKey="id" listValue="name"
		theme="xhtml" /></li>

	<li><s:select key="plantSupplier.doTemplateName"
		list="%{mFTemplate}" theme="xhtml" /></li>

	<li><s:select key="plantSupplier.boxTemplateName"
		list="%{boxTemplate}" theme="xhtml" /></li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["plantSupplierForm"]);
		highlightFormElements();
</script>
