<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="noticeList.title" /></title>
<meta name="heading" content="<fmt:message key='noticeList.heading'/>" />
</head>

<c:set var="buttons">
	<input type="button"
		onclick="location.href='<c:url value="/editNotice.html?from=list"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:set name="notices" value="notices" scope="request" />
<display:table name="notices" class="table" requestURI=""
	id="noticeList" export="true" pagesize="25">
	<display:column property="title" sortable="true"
		url="/editNotice.html?from=list" paramId="id" paramProperty="id"
		titleKey="notice.title" />
	<display:column property="content" sortable="true"
		titleKey="notice.content" />
	<display:column property="displayDateFrom" sortable="true"
		titleKey="notice.displayDateFrom" />
	<display:column property="displayDateTo" sortable="true"
		titleKey="notice.displayDateTo" />
	<display:column property="fileName" sortable="true"
		titleKey="notice.fileName" />
	<display:column property="fileFullPath" sortable="true"
		titleKey="notice.fileFullPath" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="notice.notice" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="notice.notices" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="Plant List.xls" />
	<display:setProperty name="export.csv.filename" value="Plant List.csv" />
	<display:setProperty name="export.pdf.filename" value="Plant List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("plantList");
</script>