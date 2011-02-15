<%@ include file="/common/taglibs.jsp"%>
<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>
<head>
<title><fmt:message key="notice.title" /></title>
<meta name="heading" content="<fmt:message key='notice.heading'/>" />
<meta name="menu" content="InfoMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form id="noticeForm" action="editNoticeReader" method="post">
	<c:set var="buttons">
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set>

	<li style="padding: 0px">
	<table style="margin: 0px; width: 100%;">
		<tr>
			<td colspan="2"><s:label key="noticeReader.notice.title" theme="xhtml"
				cssClass="text long" /></td>
		</tr>
		<tr>
			<td colspan="2"><s:textarea key="noticeReader.notice.content" theme="xhtml"
				cols="100" rows="8" readonly="true" /></td>
		</tr>
		<tr>
			<td><s:label key="noticeReader.notice.displayDateFrom" theme="xhtml"
				cssClass="text medium" /></td>

			<td>
			<label class="desc"><fmt:message key="noticeReader.notice.fileName"/></label>
			
			<s:url id="url" action="downloadAttachement">
				<s:param name="id">${noticeReader.notice.id}</s:param>
			</s:url> 
			
			<s:a href='%{url}'>${noticeReader.notice.fileName}</s:a>
			</td>
	</table>
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("noticeForm"));
    highlightFormElements();
</script>