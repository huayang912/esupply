<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="uploadSupplierItem.title" /></title>
<meta name="heading"
	content="<fmt:message key='uploadSupplierItem.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
<meta name="menu" content="SupplierMenu" />
</head>

<s:form name="supplierItemForm" action="uploadSupplierItem"
	enctype="multipart/form-data" method="post" validate="true">
	<c:set var="buttons">
		<s:submit key="button.upload" method="upload" />

		<s:submit key="button.cancel" method="cancel" />
	</c:set>

	<li><s:select key="supplierItem.supplier.code" list="%{suppliers}"
		listKey="supplier.code" listValue="supplierName" /></li>
	<li><s:file name="file" label="%{getText('supplierItem.file')}"
		cssClass="text file" /></li>
	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>

</s:form>
