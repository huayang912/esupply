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
<div style="z-index: 17;position:absolute; background-color:#FFFFFF;width:600px;padding:10px">
	<fieldset>
		<legend><fmt:message key="mainMenu.selectPlant" /></legend>
		<s:form name="supplierPlantForm" action="setSupplierPlant" method="post">
		<li><s:select name="supplierPlant" key="mainMenu.plantList" list="%{plantSupplierList}" listKey="plant.code" listValue="plant.name"
		theme="xhtml" /></li>
		<s:submit key="button.save"/>
		</s:form>
	</fieldset>
</div>
<div id='divHide' style='display: block; z-index: 16; top: 0pt; left: 0pt; position: fixed; height: 100%; width: 100%; opacity: 0.5;filter:alpha(opacity=50); background-color: rgb(0, 0, 0);'/>
</c:if>
