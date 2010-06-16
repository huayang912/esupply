<%@ include file="/common/taglibs.jsp"%>

<head>
    <title><fmt:message key="plantScheduleGroupList.title"/></title>
    <meta name="heading" content="<fmt:message key='plantScheduleGroupList.heading'/>"/>
    <meta name="menu" content="PlantUserMenu" />
</head>

<c:set var="buttons">
    <input type="button" 
        onclick="location.href='<c:url value="/editPlantScheduleGroup.html"/>'"
        value="<fmt:message key="button.add"/>"/>    
    <input type="button" onclick="location.href='<c:url value="/mainMenu.html"/>'"
        value="<fmt:message key="button.done"/>"/>
</c:set>

<s:set name="plantScheduleGroups" value="plantScheduleGroups" scope="request"/>
<display:table name="plantScheduleGroups" class="table" requestURI="" id="plantScheduleGroupList" export="true" pagesize="25">
    <display:column property="name" sortable="true" url="/editPlantScheduleGroup.html" 
        paramId="id" paramProperty="id" titleKey="plantScheduleGroup.name"/>
    <display:column property="allowOverDateDeliver" sortable="true" titleKey="plantScheduleGroup.allowOverDateDeliver"/>
    <display:column property="allowOverQtyDeliver" sortable="true" titleKey="plantScheduleGroup.allowOverQtyDeliver"/>
    <display:column property="allowForecastDeliver" sortable="true" titleKey="plantScheduleGroup.allowForecastDeliver"/>
    <display:column property="isDefault" sortable="true" titleKey="plantScheduleGroup.isDefault"/>
    
    <display:setProperty name="paging.banner.item_name">
		<fmt:message key="plantScheduleGroup.plantScheduleGroup" />
	</display:setProperty>
	<display:setProperty name="paging.banner.items_name">
		<fmt:message key="plantScheduleGroup.plantScheduleGroups" />
	</display:setProperty>
    
    <display:setProperty name="export.excel.filename" value="PlantScheduleGroup List.xls"/>
    <display:setProperty name="export.csv.filename" value="PlantScheduleGroup List.csv"/>
    <display:setProperty name="export.pdf.filename" value="PlantScheduleGroup List.pdf"/>
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("plantScheduleGroups");
</script>