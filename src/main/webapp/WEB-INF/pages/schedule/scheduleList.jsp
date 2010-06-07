<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="scheduleList.title" /></title>
<meta name="heading" content="<fmt:message key='scheduleList.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>
<meta name="menu" content="OrderMenu" />
<s:form name="scheduleForm" action="editSchedule" method="post"
	validate="true">
	<c:choose>
		<c:when test="${isPlantUser}">
			<li style="padding: 0px">
			<s:select key="schedule.supplierCode" list="%{suppliers}"
				listKey="code" listValue="name" theme="xhtml" required="true"></s:select>
				</li>
		</c:when>
	</c:choose>

	<s:hidden name="from" value="list"/>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="schedule.createDate"
				cssClass="text medium" required="true" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['scheduleForm'].editSchedule_schedule_createDate,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"> <img
				src="<c:url value="/images/calendar.png"/>" border="0" /> </A></td>
		</tr>
	</table>
	</li>
	<div><s:submit method="edit" key="button.search" theme="simple" /></div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
