<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="scheduleAudit.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleAudit.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<s:form name="scheduleForm" action="scheduleAudit" method="post"
	validate="true">

	<div><s:select key="supplier.code" list="%{suppliers}"
		listKey="supplier.code" listValue="supplierName" theme="xhtml" required="true"></s:select>
	</div>
	<div><s:submit method="audit" key="button.search" theme="simple" /><s:submit key="button.cancel" method="cancel" theme="simple"/></div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
