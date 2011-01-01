<%@ include file="/common/taglibs.jsp"%>

<div id="divider">
<div></div>
</div>

<c:if test="${pageContext.request.remoteUser != null}">
	<span class="left"><fmt:message key="user.status" />
	${pageContext.request.remoteUser}</span>
</c:if>
<span class="right"> &copy; <fmt:message key="copyright.year" />
<a href="<fmt:message key="company.url"/>"><fmt:message
	key="company.name" /></a> </span>
