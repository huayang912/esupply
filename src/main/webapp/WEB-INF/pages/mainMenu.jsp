<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="mainMenu.title" /></title>
<meta name="heading" content="<fmt:message key='mainMenu.heading'/>" />
<meta name="menu" content="MainMenu" />
</head>

<p><fmt:message key="mainMenu.message" /></p>

<div class="separator"></div>

<ul class="glassList">
	<li><a href="<c:url value='/editProfile.html'/>"><fmt:message
		key="menu.user" /></a></li>
</ul>

<c:if test="${selectPlant}">
<div id="floatdiv" style="border-style: solid; border-width: 1px; background-color: rgb(249, 249, 249);">
    <fieldset>
        <legend>Base Info</legend>
        <s:form name="supplierPlantForm" action="setSupplierPlant" method="post">
        <li><s:select key="supplierPlant" list="%{plantSupplierList}" listKey="plant.code" listValue="plant.name"
		theme="xhtml" /></li>
		<s:submit key="button.save"/>
		</s:form>
   	</fieldset>
</div>
</c:if>
