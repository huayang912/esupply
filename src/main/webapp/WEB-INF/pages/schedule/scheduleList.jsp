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
			<s:select key="schedule.supplierCode" list="%{suppliers}"
				listKey="code" listValue="name" theme="xhtml" required="true"></s:select>
		</c:when>
	</c:choose>

	<s:hidden name="from" value="list"/>
	<s:textfield key="schedule.createDate" cssClass="text medium"
		required="true" />
	<A HREF="#"
		onClick="cal.select(document.forms['scheduleForm'].editSchedule_schedule_createDate,'anchDateFrom','MM/dd/yyyy'); return false;"
		NAME="anchDateFrom" ID="anchDateFrom"> <img
		src="<c:url value="/images/calendar.png"/>" border="0" /> </A>

	<div><s:submit method="edit" key="button.search" theme="simple" /></div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
