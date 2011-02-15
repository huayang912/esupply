<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="scheduleList.title" /></title>
<meta name="heading" content="<fmt:message key='scheduleList.heading'/>" />
<meta name="menu" content="OrderMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>

<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="scheduleForm" action="schedules" method="post"
	validate="true">
	<s:hidden name="from" value="list" />
	<s:hidden key="isHistory"/>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.plant.Code"
				list="%{plants}" listKey="code" listValue="name" theme="simple" /></td>

			<td><label class="desc"><fmt:message
				key="plantSupplier.supplier" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.supplier.Code"
				list="%{suppliers}" listKey="code" listValue="name" theme="simple" /></td>
		</tr>
		<c:if test="${isHistory}">
			<tr>
				<td><label class="desc"><fmt:message
				key="schedule.createDate" /></label>
				</td>
				<td><s:textfield key="effectiveDate"
					theme="simple" required="true" /></td>
				<td><A HREF="#"
					onClick="cal.select(document.forms['scheduleForm'].schedules_effectiveDate,'anchDateFrom','MM/dd/yyyy'); return false;"
					NAME="anchDateFrom" ID="anchDateFrom"> <img
					src="<c:url value="/images/calendar.png"/>" border="0" /> </A></td>
			</tr>
		</c:if>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${plantSupplier != null}">
<p><fmt:message key="schedule.notFound" /></p>
</c:if>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
