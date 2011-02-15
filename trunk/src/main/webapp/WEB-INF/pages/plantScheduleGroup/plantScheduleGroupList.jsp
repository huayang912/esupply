<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantScheduleGroupList.title" /></title>
<meta name="heading"
	content="<fmt:message key='plantScheduleGroupList.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>

<c:set var="buttons">
	<s:submit method="list" key="button.search" theme="simple" />
	<input type="button"
		onclick="location.href='<c:url value="/editPlantScheduleGroup.html"/>'"
		value="<fmt:message key="button.add"/>" />
	<input type="button"
		onclick="location.href='<c:url value="/mainMenu.html"/>'"
		value="<fmt:message key="button.done"/>" />
</c:set>

<s:form name="plantScheduleGroupFrom" action="plantScheduleGroups"
	method="post" validate="true">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantScheduleGroup.plant.code" /></label> <s:select
				key="plantScheduleGroup.plant.code" list="%{plants}" listKey="code"
				listValue="name" headerKey="-1" headerValue="All" theme="simple" />
			</td>
		</tr>
	</table>
	<c:out value="${buttons}" escapeXml="false" />
</s:form>

<c:if test="${plantScheduleGroup != null}">
	<s:set name="plantScheduleGroups" value="plantScheduleGroups"
		scope="request" />
	<display:table name="plantScheduleGroups" class="table" requestURI=""
		id="plantScheduleGroupList" export="true" pagesize="25">
		<display:column property="name" url="/editPlantScheduleGroup.html"
			paramId="id" paramProperty="id" titleKey="plantScheduleGroup.name" />
			
		<display:column property="plant.name" titleKey="plantScheduleGroup.plant.code" />
		<display:column titleKey="plantScheduleGroup.allowOverDateDeliver">
			<input type="checkbox" disabled="disabled"
				<c:if test="${plantScheduleGroupList.allowOverDateDeliver}">checked="checked"</c:if> />
		</display:column>
		<display:column titleKey="plantScheduleGroup.allowOverQtyDeliver">
			<input type="checkbox" disabled="disabled"
				<c:if test="${plantScheduleGroupList.allowOverQtyDeliver}">checked="checked"</c:if> />
		</display:column>
		<display:column titleKey="plantScheduleGroup.allowForecastDeliver">
			<input type="checkbox" disabled="disabled"
				<c:if test="${plantScheduleGroupList.allowForecastDeliver}">checked="checked"</c:if> />
		</display:column>
		<display:column titleKey="plantScheduleGroup.isDefault">
			<input type="checkbox" disabled="disabled"
				<c:if test="${plantScheduleGroupList.isDefault}">checked="checked"</c:if> />
		</display:column>

		<display:setProperty name="paging.banner.item_name">
			<fmt:message key="plantScheduleGroup.plantScheduleGroup" />
		</display:setProperty>
		<display:setProperty name="paging.banner.items_name">
			<fmt:message key="plantScheduleGroup.plantScheduleGroups" />
		</display:setProperty>

		<display:setProperty name="export.excel.filename"
			value="PlantScheduleGroup List.xls" />
		<display:setProperty name="export.csv.filename"
			value="PlantScheduleGroup List.csv" />
		<display:setProperty name="export.pdf.filename"
			value="PlantScheduleGroup List.pdf" />
	</display:table>

	<script type="text/javascript">
    highlightTableRows("plantScheduleGroups");
</script>
</c:if>