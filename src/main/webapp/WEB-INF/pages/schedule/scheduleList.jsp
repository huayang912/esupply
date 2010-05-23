<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="scheduleList.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleList.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>
<meta name="menu" content="OrderMenu" />
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>
<s:form name="scheduleForm" action="schedules" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<s:textfield key="schedule.createDateFrom"
		cssClass="text medium" />
	<A HREF="#"
    	onClick="cal.select(document.forms['scheduleForm'].schedules_schedule_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
    	NAME="anchDateFrom" ID="anchDateFrom">
    	<img src="<c:url value="/images/calendar.png"/>" border="0" />
    </A>
    	
	<s:textfield key="schedule.createDateTo"
		cssClass="text medium" />
	<A HREF="#"
    	onClick="cal.select(document.forms['scheduleForm'].schedules_schedule_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
    	NAME="anchDateTo" ID="anchDateTo"><img src="<c:url value="/images/calendar.png"/>" border="0"/></A>
    	
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="schedules" class="table"
	export="true">
	<display:column property="scheduleNo" sortable="true"
		url="/editSchedule.html?from=list" paramId="scheduleNo" paramProperty="scheduleNo"
		titleKey="schedule.scheduleNo" />
	<display:column property="plantCode" sortable="true"
		sortProperty="p.code" titleKey="schedule.plantCode" />
	<display:column property="plantName" sortable="true"
		sortProperty="p.name" titleKey="schedule.plantName" />
	<display:column property="supplierCode" sortable="true"
		sortProperty="s.code" titleKey="schedule.supplierCode" />
	<display:column property="supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="schedule.supplierName" />
	<display:column property="createDate" format="{0,date,MM/dd/yyyy}"
		sortable="true" titleKey="schedule.createDate" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="schedule.schedule" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="schedule.schedules" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="schedule List.xls" />
	<display:setProperty name="export.csv.filename"
		value="schedule List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="schedule List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    //highlightTableRows("schedules");    
</script>
