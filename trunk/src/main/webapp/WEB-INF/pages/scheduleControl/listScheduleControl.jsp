<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="scheduleControl.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleControl.heading'/>" />
<meta name="menu" content="AdminMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>

<script type="text/javascript">
	function cascadeUpdateSupplier(plantSelect) {
		SupplierManager.getSuppliersByPlantAndUser(plantSelect.options(plantSelect.selectedIndex).value + "|${pageContext.request.remoteUser}", supplierSelectHandler);
	}

	function supplierSelectHandler(suppliers) {
		 DWRUtil.removeAllOptions("listScheduleControl_schedulePlantSupplier_supplier_code");
		 if (suppliers != null) {		 
		 	DWRUtil.addOptions("listScheduleControl_schedulePlantSupplier_supplier_code",suppliers, "code", "name");    
		 }
	}
</script>
</head>

<c:set var="buttons">
	<s:submit key="button.confirm" method="save" theme="simple" />
</c:set>
<s:form name="scheduleControlForm" action="listScheduleControl"
	method="post" validate="true">

	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="schedulePlantSupplier.plant.code"
				list="%{plants}" listKey="code" listValue="name" theme="simple" onchange="cascadeUpdateSupplier(this);"/></td>

			<td><label class="desc"><fmt:message
				key="plantSupplier.supplier" /></label></td>
			<td colspan="2"><s:select
				key="schedulePlantSupplier.supplier.code" list="%{suppliers}"
				listKey="code" listValue="name" theme="simple" /></td>
		</tr>
	</table>
	</li>

	<div><s:submit key="button.search" theme="simple" /><s:submit
		key="button.cancel" method="cancel" theme="simple" /></div>

	<c:if test="${schedulePlantSupplier != null}">
		<display:table name="scheduleControlList" cellspacing="0"
			cellpadding="0" requestURI="" id="scheduleControl" class="table">
			<display:column property="scheduleNo"
				titleKey="scheduleControl.ScheduleNo" />
			<display:column property="item.code"
				titleKey="scheduleControl.itemCode" />
			<display:column property="item.description"
				titleKey="scheduleControl.itemDescription" />
			<display:column titleKey="scheduleControl.expireDate">
				<table>
					<tr>
						<td><input type="hidden"
							name="scheduleControlList[${scheduleControl_rowNum}].id"
							value="<c:out value="${scheduleControl.id}"/>" /> <input
							type="text"
							id="scheduleControlList[${scheduleControl_rowNum}].expireDate"
							name="scheduleControlList[${scheduleControl_rowNum}].expireDate"
							value="<fmt:formatDate value="${scheduleControl.expireDate}" pattern="MM/dd/yyyy" />"
							class="text medium" /></td>
						<td><A HREF="#"
							onClick="cal.select(document.getElementById('scheduleControlList[${scheduleControl_rowNum}].expireDate'),'anchDate${scheduleControl_rowNum}','MM/dd/yyyy'); return false;"
							NAME="anchDate${scheduleControl_rowNum}"
							ID="anchDate${scheduleControl_rowNum}"><img
							src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
					</tr>
				</table>
			</display:column>
		</display:table>

		<c:out value="${buttons}" escapeXml="false" />
	</c:if>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["listScheduleControl"]);
		highlightFormElements();
</script>
