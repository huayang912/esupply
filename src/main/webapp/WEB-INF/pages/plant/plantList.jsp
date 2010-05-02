<%@ include file="/common/taglibs.jsp"%>

<head>
    <title><fmt:message key="plantList.title"/></title>
    <meta name="heading" content="<fmt:message key='plantList.heading'/>"/>
</head>

<c:set var="buttons">
    <input type="button" style="margin-right: 5px"
        onclick="location.href='<c:url value="/editPlant.html"/>'"
        value="<fmt:message key="button.add"/>"/>
    
    <input type="button" onclick="location.href='<c:url value="/mainMenu.html"/>'"
        value="<fmt:message key="button.done"/>"/>
</c:set>

<c:out value="${buttons}" escapeXml="false" />

<s:set name="plants" value="plants" scope="request"/>
<display:table name="plants" class="table" requestURI="" id="plantList" export="true" pagesize="25">
    <display:column property="code" sortable="true" href="editPlant.html" 
        paramId="code" paramProperty="code" titleKey="plant.code"/>
    <display:column property="name" sortable="true" titleKey="plant.name"/>
</display:table>

<c:out value="${buttons}" escapeXml="false" />

<script type="text/javascript">
    highlightTableRows("plantList");
</script>