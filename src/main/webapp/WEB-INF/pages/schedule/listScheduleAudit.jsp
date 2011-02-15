<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="scheduleAudit.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleAudit.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<c:set var="buttons">
	<s:submit method="listAudit" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="scheduleForm" action="listScheduleAudit" method="post"
	validate="true">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plantCode" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.plantCode"
				list="%{plants}" listKey="code" listValue="name" theme="simple" /></td>

			<td><label class="desc"><fmt:message
				key="plantSupplier.supplierCode" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.supplierCode"
				list="%{suppliers}" listKey="code" listValue="name" theme="simple" /></td>
		</tr>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
