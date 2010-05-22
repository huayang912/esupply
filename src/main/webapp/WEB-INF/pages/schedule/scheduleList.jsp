<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="scheduleList.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleList.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/DateTimeCalendar.js'/>"></script>
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
	<li><s:textfield key="schedule.createDateTo"
		cssClass="text medium" /></li>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="schedules" class="table"
	export="true">
	<display:column property="scheduleNo" sortable="true"
		url="/editSchedule.html" paramId="scheduleNo" paramProperty="scheduleNo"
		titleKey="schedule.scheduleNo" />
	<display:column property="plantSupplier.plant.code" sortable="true"
		sortProperty="p.code" titleKey="schedule.plantCode" />
	<display:column property="plantSupplier.plant.name" sortable="true"
		sortProperty="p.name" titleKey="schedule.plantName" />
	<display:column property="plantSupplier.supplier.code" sortable="true"
		sortProperty="s.code" titleKey="schedule.supplierCode" />
	<display:column property="plantSupplier.supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="schedule.supplierName" />
	<display:column property="createDate" format="{0,date,MM/dd/yyyy}"
		sortable="true" titleKey="schedule.createDate" />


	<display:setProperty name="paging.banner.item_name"
		value="Schedule" />
	<display:setProperty name="paging.banner.items_name"
		value="Schedule" />

	<display:setProperty name="export.excel.filename"
		value="schedule List.xls" />
	<display:setProperty name="export.csv.filename"
		value="schedule List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="schedule List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("schedules");    
</script>
