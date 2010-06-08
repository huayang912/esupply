<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="outboundLogList.title" /></title>
<meta name="heading"
	content="<fmt:message key='outboundLogList.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>
<meta name="menu" content="PlantAdminMenu" />
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="outboundLogForm" action="outboundLogs" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<s:textfield key="outboundLog.createDateFrom" cssClass="text medium" />
	<A HREF="#"
		onClick="cal.select(document.forms['outboundLogForm'].outboundLogs_outboundLog_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
		NAME="anchDateFrom" ID="anchDateFrom"><img
		src="<c:url value="/images/calendar.png"/>" border="0" /></A>
	<s:textfield key="outboundLog.createDateTo" cssClass="text medium" />
	<A HREF="#"
		onClick="cal.select(document.forms['outboundLogForm'].outboundLogs_outboundLog_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
		NAME="anchDateTo" ID="anchDateTo"><img
		src="<c:url value="/images/calendar.png"/>" border="0" /></A>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="outboundLogs" class="table"
	export="true">
	<display:column property="plantSupplier.plant.name" sortable="true"
		sortProperty="p.name" titleKey="outboundLog.plantName" />

	<display:column property="plantSupplier.supplierName" sortable="true"
		sortProperty="ps.supplierName" titleKey="outboundLog.supplierName" />

	<display:column property="doNo" sortable="true"
		sortProperty="doNo" titleKey="outboundLog.doNo" />

	<display:column property="outboundResult" sortable="true"
		sortProperty="outboundResult" titleKey="outboundLog.outboundResult" />

	<display:column property="memo" sortable="true" sortProperty="memo"
		titleKey="outboundLog.memo" />

	<display:column property="createDate" sortable="true"
		sortProperty="createDate" titleKey="outboundLog.createDate"
		format="{0,date,MM/dd/yyyy}" />

	<display:column property="createUser" sortable="true"
		sortProperty="createUser" titleKey="outboundLog.createUser" />

	<display:column property="lastModifyDate" sortable="true"
		sortProperty="lastModifyDate" titleKey="outboundLog.lastModifyDate"
		format="{0,date,MM/dd/yyyy}" />

	<display:column property="lastModifyUser" sortable="true"
		sortProperty="lastModifyUser" titleKey="outboundLog.lastModifyUser" />

	<display:column sortable="false" titleKey="outboundLog.export"
		url="/exportOutboundLog.html" paramId="doNo"
		paramProperty="doNo">
		<fmt:message key="outboundLog.export" />
	</display:column>

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="outboundLog.outboundLog" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="outboundLog.outboundLogs" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="outboundLog List.xls" />
	<display:setProperty name="export.csv.filename"
		value="outboundLog List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="outboundLog List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("outboundLogs");    
</script>
