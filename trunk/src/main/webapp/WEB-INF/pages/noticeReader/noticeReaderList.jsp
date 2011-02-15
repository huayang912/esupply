<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="noticeList.title" /></title>
<meta name="heading" content="<fmt:message key='noticeList.heading'/>" />
<meta name="menu" content="SupplierMenu" />
</head>

<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="noticeReaderForm" action="noticeReaders" method="post">
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.plant.code"
				list="%{plants}" listKey="code" listValue="name" theme="simple" 
				headerKey="-1" headerValue="All"/></td>

			<td><label class="desc"><fmt:message
				key="plantSupplier.supplier" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.supplier.code"
				list="%{suppliers}" listKey="code" listValue="name" theme="simple" 
				headerKey="-1" headerValue="All"/></td>
		</tr>
	</table>
	</li>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${plantSupplier != null}">
	<s:set name="noticeReaders" value="noticeReaders" scope="request" />
	<display:table name="noticeReaders" class="table" requestURI=""
		id="noticeReaderList" export="true" pagesize="25">
		<display:column property="notice.title" sortable="true"
			url="/editNoticeReader.html?from=list&supplierCode=${plantSupplier.supplier.code}"
			paramId="id" paramProperty="id" titleKey="notice.title" />
			<display:column property="plantSupplier.plant.name" sortable="true"
			titleKey="notice.plant" />
			<display:column property="plantSupplier.supplier.name" sortable="true"
			titleKey="notice.supplier" />
			
		<display:column property="notice.displayDateFrom" sortable="true"
			titleKey="notice.displayDateFrom" format="{0,date,MM/dd/yyyy}" />
		<display:column property="notice.fileName" sortable="true"
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
		<display:setProperty name="export.csv.filename"
			value="Notice List.csv" />
		<display:setProperty name="export.pdf.filename"
			value="Notice List.pdf" />
	</display:table>

	<script type="text/javascript">
    highlightTableRows("noticeReaderList");

	</script>
</c:if>