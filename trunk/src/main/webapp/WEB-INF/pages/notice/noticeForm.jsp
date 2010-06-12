<%@ include file="/common/taglibs.jsp"%>
<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>
<head>
<title><fmt:message key="notice.title" /></title>
<meta name="heading" content="<fmt:message key='notice.heading'/>" />
<meta name="menu" content="PlantUserMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form id="noticeForm" action="saveNotice" enctype="multipart/form-data" method="post"
	validate="true">
	<li style="display: none"><input type="hidden" name="from"
		value="${param.from}" /></li>
	<c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			onclick="onFormSubmit()" theme="simple" />
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
		required="true" cssClass="text long" /></div>
	</div>
	</li>
	<li>
	<div>
	<div><s:textarea key="notice.content" theme="xhtml"
		 cols="100" rows="8" /></div>
	</div>
	</li>
	
	<li>
	<div>
	<div><s:file name="file" label="%{getText('notice.file')}" cssClass="text file" required="true"/></div>
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

	<li>
	<fieldset><legend><fmt:message
		key="notice.assignSuppliers" /></legend>
	<table class="pickList" style="width: 100%">
		<tr>
			<th class="pickLabel"><label class="required"><fmt:message
				key="notice.availableSuppliers" /></label></th>
			<td></td>
			<th class="pickLabel"><label class="required"><fmt:message
				key="notice.suppliers" /></label></th>
		</tr>
		<c:set var="leftList" value="${availableSuppliers}" scope="request" />
		<s:set name="rightList" value="notice.supplierList" scope="request" />
		<c:import url="/WEB-INF/pages/pickList.jsp">
			<c:param name="listCount" value="1" />
			<c:param name="leftId" value="availableSuppliers" />
			<c:param name="rightId" value="suppliers" />
		</c:import>
	</table>
	</fieldset>
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("noticeForm"));
    highlightFormElements();
    
    function onFormSubmit() {
    	selectAll("suppliers");
    }
</script>