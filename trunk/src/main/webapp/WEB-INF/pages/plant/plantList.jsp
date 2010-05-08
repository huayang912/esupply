<%@ include file="/common/taglibs.jsp"%>

<head>
    <title><fmt:message key="plantList.title"/></title>
    <meta name="heading" content="<fmt:message key='plantList.heading'/>"/>
</head>

<c:set var="buttons">
    <input type="button" style="margin-right: 5px"
        onclick="location.href='<c:url value="/editPlant.html?from=list"/>'"
        value="<fmt:message key="button.add"/>"/>
    
    <input type="button" onclick="location.href='<c:url value="/mainMenu.html"/>'"
        value="<fmt:message key="button.done"/>"/>
</c:set>

<c:out value="${buttons}" escapeXml="false" />

<s:set name="plants" value="plants" scope="request"/>
<display:table name="plants" class="table" requestURI="" id="plantList" export="true" pagesize="25">
    <display:column property="code" sortable="true" url="/editPlant.html?from=list" 
        paramId="code" paramProperty="code" titleKey="plant.code"/>
    <display:column property="name" sortable="true" titleKey="plant.name"/>
    <display:column property="address1" sortable="true" titleKey="plant.address1"/>
    <display:column property="address2" sortable="true" titleKey="plant.address2"/>
    <display:column property="contactPerson" sortable="true" titleKey="plant.contactPerson"/>
    <display:column property="phone" sortable="true" titleKey="plant.phone"/>
    <display:column property="fax" sortable="true" titleKey="plant.fax"/>
    
    <display:setProperty name="paging.banner.item_name" value="plant"/>
    <display:setProperty name="paging.banner.items_name" value="plants"/>
    
    <display:setProperty name="export.excel.filename" value="Plant List.xls"/>
    <display:setProperty name="export.csv.filename" value="Plant List.csv"/>
    <display:setProperty name="export.pdf.filename" value="Plant List.pdf"/>
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("plantList");
</script>