<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="scheduleControl.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleControl.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>

<s:form name="scheduleControlForm" action="listScheduleControl" method="post"
	validate="true">

	<div><s:select key="schedulePlantSupplier.id" list="%{suppliers}"
		listKey="id" listValue="supplierName" theme="xhtml" required="true"></s:select>
	</div>
	<div><s:submit key="button.search" theme="simple" /><s:submit key="button.cancel" method="cancel" theme="simple"/></div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleControlForm"]);
		highlightFormElements();
</script>
