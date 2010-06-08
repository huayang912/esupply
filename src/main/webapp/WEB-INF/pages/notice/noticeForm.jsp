<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="notice.title" /></title>
<meta name="heading" content="<fmt:message key='notice.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
</head>

<s:form id="noticeForm" action="saveNotice" method="post" validate="true">
	<li style="display: none"><input type="hidden" name="from"
		value="${param.from}" /></li>
	<c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			theme="simple" />
		<c:if test="${param.from == 'list' and not empty notice.id}">
			<s:submit cssClass="button" method="delete" key="button.delete"
				onclick="return confirmDelete('notice')" theme="simple" />
		</c:if>
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set>
	<li>
	<div>
	<div><s:textfield key="notice.title" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	</div>
	</li>
	<li>
	<div>
	<div><s:textfield key="notice.content" theme="xhtml" required="true"
		cssClass="text medium" /></div>
	</div>
	</li>
	<li>
	<div>
	<div><s:textfield key="notice.fileFullPath" 
		theme="xhtml" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="notice.fileName" theme="xhtml"
		 cssClass="text medium" /></div>
	</div>
	</li>
	
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="notice.displayDateFrom"
				cssClass="text medium" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['noticeForm'].noticeForm_notice_displayDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><s:textfield key="notice.displayDateTo"
				cssClass="text medium" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['noticeForm'].noticeForm_notice_displayDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
	</table>
	</li>
	
	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("noticeForm"));
    highlightFormElements();
    
</script>