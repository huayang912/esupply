<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="noticeList.title" /></title>
<meta name="heading" content="<fmt:message key='noticeList.heading'/>" />
<meta name="menu" content="InfoMenu" />
</head>

<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/editNotice.html?from=list"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="noticeForm" action="notices" method="post">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.plant.code"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1"
				headerValue="All" theme="simple" /></td>
		</tr>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${plantSupplier != null}">
	<s:set name="notices" value="notices" scope="request" />
	<display:table name="notices" class="table" requestURI=""
		id="noticeList" export="true" pagesize="25">
		<display:column property="title" sortable="true"
			url="/editNotice.html?from=list" paramId="id" paramProperty="id"
			titleKey="notice.title" />
		<display:column property="plant.name" sortable="true"
			titleKey="notice.plant"/>
		<display:column property="fileName" sortable="true"
			url="/downloadAttachement.html" paramId="id" paramProperty="id"
			titleKey="notice.fileName" />
		<display:column property="displayDateFrom" sortable="true"
			titleKey="notice.displayDateFrom" format="{0,date,MM/dd/yyyy}" />
		<display:column property="displayDateTo" sortable="true"
			titleKey="notice.displayDateTo" format="{0,date,MM/dd/yyyy}" />

		<display:setProperty name="paging.banner.item_name">
			<fmt:message key="notice.notice" />
		</display:setProperty>
		<display:setProperty name="paging.banner.items_name">
			<fmt:message key="notice.notices" />
		</display:setProperty>

		<display:setProperty name="export.excel.filename"
			value="Notice List.xls" />
		<display:setProperty name="export.csv.filename"
			value="Notice List.csv" />
		<display:setProperty name="export.pdf.filename"
			value="Notice List.pdf" />
	</display:table>

	<script type="text/javascript">
    highlightTableRows("plantList");
</script>
</c:if>