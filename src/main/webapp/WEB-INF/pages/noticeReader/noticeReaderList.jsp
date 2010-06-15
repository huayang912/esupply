<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="noticeList.title" /></title>
<meta name="heading" content="<fmt:message key='noticeList.heading'/>" />
<meta name="menu" content="SupplierMenu" />
</head>

<c:set var="buttons">	
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:set name="notices" value="notices" scope="request" />
<display:table name="notices" class="table" requestURI=""
	id="noticeList" export="true" pagesize="25">
	<display:column property="title" sortable="true"
		url="/editNoticeReader.html?from=list" paramId="id" paramProperty="id"
		titleKey="notice.title" />
	<display:column property="displayDateFrom" sortable="true"
		titleKey="notice.displayDateFrom" format="{0,date,MM/dd/yyyy}"/>
	<display:column property="fileName" sortable="true"
		url="/downloadAttachement.html" paramId="id" paramProperty="id"
		titleKey="notice.fileName" />

	<display:setProperty name="paging.banner.item_name">
		<fmt:message key="notice.notice" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="notice.notices" />
	</display:setProperty>

	<display:setProperty name="export.excel.filename"
		value="Notice List.xls" />
	<display:setProperty name="export.csv.filename" value="Notice List.csv" />
	<display:setProperty name="export.pdf.filename" value="Notice List.pdf" />
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("plantList");
</script>