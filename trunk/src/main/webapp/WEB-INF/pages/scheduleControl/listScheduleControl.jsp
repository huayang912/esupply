<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="scheduleControl.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleControl.heading'/>" />
<meta name="menu" content="PlantUserMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>

<c:set var="buttons">
	<s:submit key="button.confirm" method="save" theme="simple"/>
</c:set>
<s:form name="scheduleControlForm" action="listScheduleControl"
	method="post" validate="true">

	<div><s:select key="schedulePlantSupplier.id" list="%{suppliers}"
		listKey="id" listValue="supplierName" theme="xhtml" required="true"></s:select>
	</div>
	<div><s:submit key="button.search" theme="simple" /><s:submit
		key="button.cancel" method="cancel" theme="simple" /></div>

	<display:table name="scheduleControlList" cellspacing="0"
		cellpadding="0" requestURI="" id="scheduleControl" class="table">
		<display:column property="scheduleNo"
			titleKey="scheduleControl.ScheduleNo" />
		<display:column property="item.code"
			titleKey="scheduleControl.itemCode" />
		<display:column property="item.description"
			titleKey="scheduleControl.itemDescription" />
		<display:column titleKey="scheduleControl.expireDate">
			<table>
				<tr>
					<td><input type="hidden"
						name="scheduleControlList[${scheduleControl_rowNum}].id"
						value="<c:out value="${scheduleControl.id}"/>" /> <input
						type="text" id="scheduleControlList[${scheduleControl_rowNum}].expireDate"
						name="scheduleControlList[${scheduleControl_rowNum}].expireDate"
						value="<fmt:formatDate value="${scheduleControl.expireDate}" pattern="MM/dd/yyyy" />" class="text medium" /></td>
					<td><A HREF="#"
						onClick="cal.select(document.getElementById('scheduleControlList[${scheduleControl_rowNum}].expireDate'),'anchDate${scheduleControl_rowNum}','MM/dd/yyyy'); return false;"
						NAME="anchDate${scheduleControl_rowNum}" ID="anchDate${scheduleControl_rowNum}"><img
						src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
				</tr>
			</table>
		</display:column>
	</display:table>
	
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleControlForm"]);
		highlightFormElements();
</script>
