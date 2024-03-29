<%@ include file="/common/taglibs.jsp"%>

<c:if test="${pageContext.request.locale.language != 'en' and false}">
	<div id="switchLocale"><a href="<c:url value='/?locale=en'/>">
	<fmt:message key="webapp.name" /> in English</a></div>
</c:if>

<div id="branding">
<ul>
	<li><a href="<fmt:message key="company.url"/>"> <img
		style="border-width: 0px;"
		src="<c:url value="/images/logofaurecia.gif"/>"
		alt="Equipement supplier for the automotive industry" /> </a></li>
	<li>
	<h1><a href="<c:url value='/'/>"> <fmt:message
		key="webapp.name" /> </a></h1>
	<p><fmt:message key="webapp.tagline" /></p>
	</li>
</ul>
</div>
<hr />

<%-- Put constants into request scope --%>
<appfuse:constants scope="request" />