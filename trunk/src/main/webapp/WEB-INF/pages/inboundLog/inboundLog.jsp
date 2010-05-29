<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="inboundLogList.title" /></title>
<meta name="heading"
	content="<fmt:message key='inboundLogList.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>
<meta name="menu" content="PlantAdminMenu" />
<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="inboundLogForm" action="inboundLogs" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<s:textfield key="inboundLog.createDateFrom" cssClass="text medium" />
	<A HREF="#"
		onClick="cal.select(document.forms['inboundLogForm'].inboundLogs_inboundLog_createDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
		NAME="anchDateFrom" ID="anchDateFrom"><img
		src="<c:url value="/images/calendar.png"/>" border="0" /></A>
	<s:textfield key="inboundLog.createDateTo" cssClass="text medium" />
	<A HREF="#"
		onClick="cal.select(document.forms['inboundLogForm'].inboundLogs_inboundLog_createDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
		NAME="anchDateTo" ID="anchDateTo"><img
		src="<c:url value="/images/calendar.png"/>" border="0" /></A>
	<div><s:submit method="list" key="button.search" theme="simple" /></div>
</s:form>

<display:table name="paginatedList" cellspacing="0" cellpadding="0"
	requestURI="" defaultsort="1" id="inboundLogs" class="table"
	export="true">
	<display:column property="plantSupplier.plant.name" sortable="true" sortProperty="p.name"
		titleKey="inboundLog.plantName" />
		
	<display:column property="plantSupplier.supplierName" sortable="true" sortProperty="ps.supplierName"
		titleKey="inboundLog.supplierName" />

	<display:column property="dataType" sortable="true" sortProperty="dataType"
		titleKey="inboundLog.dataType" />
		
	<display:column property="fileName" sortable="true" sortProperty="fileName"
		titleKey="inboundLog.fileName" />
		
	<display:column property="inboundResult" sortable="true" sortProperty="inboundResult"
		titleKey="inboundLog.inboundResult" />
		
	<display:column property="memo" sortable="true" sortProperty="memo"
		titleKey="inboundLog.memo" />
		
	<display:column property="createDate" sortable="true" sortProperty="createDate"
		titleKey="inboundLog.createDate" format="{0,date,MM/dd/yyyy}"/>
		
	<display:column property="createUser" sortable="true" sortProperty="createUser"
		titleKey="inboundLog.createUser" />
		
	<display:column property="lastModifyDate" sortable="true" sortProperty="lastModifyDate"
		titleKey="inboundLog.lastModifyDate" format="{0,date,MM/dd/yyyy}"/>
		
	<display:column property="lastModifyUser" sortable="true" sortProperty="lastModifyUser"
		titleKey="inboundLog.lastModifyUser" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="inboundLog.inboundLog" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="inboundLog.inboundLogs" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="inboundLog List.xls" />
	<display:setProperty name="export.csv.filename"
		value="inboundLog List.csv" />
	<display:setProperty name="export.pdf.filename"
		value="inboundLog List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("inboundLogs");    
</script>