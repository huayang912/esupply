<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="userProfile.title" /></title>
<meta name="heading" content="<fmt:message key='userProfile.heading'/>" />
<c:choose>
	<c:when test="${editProfile}">
		<meta name="menu" content="UserMenu" />
	</c:when>
	<c:otherwise>
		<c:choose>
			<c:when
				test="${roleType == 'ROLE_ADMIN' or roleType == 'ROLE_PLANT_ADMIN' or roleType == 'ROLE_VENDOR'}">
				<meta name="menu" content="AdminMenu" />
			</c:when>
			<c:when
				test="${roleType == 'ROLE_PLANT_USER'}">
				<meta name="menu" content="PlantAdminMenu" />
			</c:when>
		</c:choose>
	</c:otherwise>
</c:choose>
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form name="userForm" action="saveUser" method="post" validate="true">
	<li style="display: none"><s:hidden key="user.id" /> <s:hidden
		key="user.version" /> <input type="hidden" name="from"
		value="${param.from}" /><input type="hidden" name="roleType"
		value="${roleType}" /> <c:if test="${cookieLogin == 'true'}">
		<s:hidden key="user.password" />
		<s:hidden key="user.confirmPassword" />
	</c:if> <s:if test="user.version == null">
		<input type="hidden" name="encryptPass" value="true" />
	</s:if></li>
	<c:set var="buttons">
		<s:submit key="button.save" method="save"
			onclick="onFormSubmit(this.form)" />

		<c:if test="${param.from == 'list' and not empty user.id}">
			<s:submit key="button.delete" method="delete"
				onclick="return confirmDelete('user')" />
		</c:if>
		<s:submit key="button.cancel" method="cancel" />
	</c:set>
	<li class="info"><c:choose>
		<c:when test="${param.from == 'list'}">
			<p><fmt:message key="userProfile.admin.message" /></p>
		</c:when>
		<c:otherwise>
			<p><fmt:message key="userProfile.message" /></p>
		</c:otherwise>
	</c:choose></li>

	<s:textfield key="user.username" cssClass="text medium" required="true" />

	<c:if test="${cookieLogin != 'true'}">
		<li>
		<div>
		<div class="left"><s:password key="user.password"
			showPassword="true" theme="xhtml" required="true"
			cssClass="text medium" onchange="passwordChanged(this)" /></div>
		<div><s:password key="user.confirmPassword" theme="xhtml"
			required="true" showPassword="true" cssClass="text medium"
			onchange="passwordChanged(this)" /></div>
		</div>
		</li>
	</c:if>

	<s:textfield key="user.passwordHint" required="true"
		cssClass="text large" />

	<li>
	<div>
	<div class="left"><s:textfield key="user.firstName" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	<div><s:textfield key="user.lastName" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:textfield key="user.email" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	<div><s:textfield key="user.phoneNumber" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>
	<c:choose>
		<c:when test="${roleType == 'ROLE_PLANT_ADMIN'}">
			<li>
			<div class="left"><s:select key="user.userPlant.code"
				list="%{plants}" listKey="code" listValue="name" theme="xhtml"
				required="true"></s:select></div>
			</li>
		</c:when>
		<c:when test="${roleType == 'ROLE_VENDOR'}">
			<li>
			<div class="left"><s:select key="user.userSupplier.code"
				list="%{suppliers}" listKey="code" listValue="name" theme="xhtml"
				required="true"></s:select></div>
			</li>
		</c:when>
	</c:choose>
	<c:choose>
		<c:when test="${param.from == 'list'}">
			<li>
			<fieldset><legend><fmt:message
				key="userProfile.accountSettings" /></legend> <s:checkbox key="user.enabled"
				id="user.enabled" fieldValue="true" theme="simple" /> <label
				for="user.enabled" class="choice"><fmt:message
				key="user.enabled" /></label> <s:checkbox key="user.accountExpired"
				id="user.accountExpired" fieldValue="true" theme="simple" /> <label
				for="user.accountExpired" class="choice"><fmt:message
				key="user.accountExpired" /></label> <s:checkbox key="user.accountLocked"
				id="user.accountLocked" fieldValue="true" theme="simple" /> <label
				for="user.accountLocked" class="choice"><fmt:message
				key="user.accountLocked" /></label> <s:checkbox
				key="user.credentialsExpired" id="user.credentialsExpired"
				fieldValue="true" theme="simple" /> <label
				for="user.credentialsExpired" class="choice"><fmt:message
				key="user.credentialsExpired" /></label></fieldset>
			</li>
		</c:when>
		<c:otherwise>
			<li><strong><fmt:message key="user.roles" />:</strong> <s:iterator
				value="user.roleList" status="status">
				<s:property value="label" />
				<s:if test="!#status.last">,</s:if>
				<input type="hidden" name="userRoles"
					value="<s:property value="value"/>" />
			</s:iterator> <s:hidden name="user.enabled" value="%{user.enabled}" /> <s:hidden
				name="user.accountExpired" value="%{user.accountExpired}" /> <s:hidden
				name="user.accountLocked" value="%{user.accountLocked}" /> <s:hidden
				name="user.credentialsExpired" value="%{user.credentialsExpired}" />
			</li>
		</c:otherwise>
	</c:choose>
	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement(document.forms["userForm"]);
    highlightFormElements();

    function passwordChanged(passwordField) {
        if (passwordField.name == "user.password") {
            var origPassword = "<s:property value="user.password"/>";
        } else if (passwordField.name == "user.confirmPassword") {
            var origPassword = "<s:property value="user.confirmPassword"/>";
        }
        
        if (passwordField.value != origPassword) {
            createFormElement("input", "hidden",  "encryptPass", "encryptPass",
                              "true", passwordField.form);
        }
    }

<!-- This is here so we can exclude the selectAll call when roles is hidden -->
function onFormSubmit(theForm) {
<c:if test="${param.from == 'list'}">
    selectAll('userRoles');
</c:if>
}
</script>
