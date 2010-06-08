<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantScheduleGroupDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantScheduleGroupDetail.heading'/>" />
<meta name="menu" content="PlantUserMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form id="plantScheduleGroupForm" action="savePlantScheduleGroup"
	method="post" validate="true">
	<li style="display: none"><input type="hidden"
		name="plantScheduleGroup.id" value="${plantScheduleGroup.id}" /> <input
		type="hidden" name="plantScheduleGroup.plant.code"
		value="${plantScheduleGroup.plant.code}" /></li>
	<c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			onclick="onFormSubmit()" theme="simple" />
		<c:if test="${not empty plantScheduleGroup.id}">
			<s:submit cssClass="button" method="delete" key="button.delete"
				onclick="return confirmDelete('plantScheduleGroup')" theme="simple" />
		</c:if>
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set>

	<li style="padding: 0px">
	<table style="margin: 0px; width: 100%;">
		<tr>
			<td colspan="2"><s:textfield key="plantScheduleGroup.name"
				theme="xhtml" required="true" cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:checkbox key="plantScheduleGroup.isDefault" /></td>
			<td><s:checkbox key="plantScheduleGroup.allowOverDateDeliver" />
			</td>
		</tr>
		<tr>
			<td><s:checkbox key="plantScheduleGroup.allowOverQtyDeliver" /></td>
			<td><s:checkbox key="plantScheduleGroup.allowForecastDeliver" />
			</td>
		</tr>
	</table>
	</li>


	<li>
	<fieldset><legend><fmt:message
		key="plantScheduleGroup.assignSuppliers" /></legend>
	<table class="pickList" style="width:100%">
		<tr>
			<th class="pickLabel"><label class="required"><fmt:message
				key="plantScheduleGroup.availableSuppliers" /></label></th>
			<td></td>
			<th class="pickLabel"><label class="required"><fmt:message
				key="plantScheduleGroup.suppliers" /></label></th>
		</tr>
		<c:set var="leftList" value="${availableSuppliers}" scope="request" />
		<s:set name="rightList" value="plantScheduleGroup.supplierList"
			scope="request" />
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
    Form.focusFirstElement($("plantScheduleGroupForm"));
    highlightFormElements();

    function onFormSubmit() {
    	selectAll("suppliers");
    }
</script>