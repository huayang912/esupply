<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantDetail.title" /></title>
<meta name="heading" content="<fmt:message key='plantDetail.heading'/>" />
<meta name="menu" content="AdminMenu" />
</head>

<s:form id="plantForm" action="savePlant" method="post" validate="true">
	<li style="display: none"><input type="hidden" name="from"
		value="${param.from}" /></li>
	<c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			theme="simple" />
		<c:if test="${param.from == 'list' and not empty plant.code}">
			<s:submit cssClass="button" method="delete" key="button.delete"
				onclick="return confirmDelete('plant')" theme="simple" />
		</c:if>
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set>
	<li>
	<div>
	<div class="left"><s:textfield key="plant.code" theme="xhtml"
		required="true" cssClass="text medium" /> <s:hidden
		key="plant.version" /></div>
	<div><s:textfield key="plant.name" theme="xhtml" required="true"
		cssClass="text medium" /></div>
	</div>
	</li>
	<li><s:textfield key="plant.address1" theme="xhtml"
		cssClass="text large" /></li>
	<li><s:textfield key="plant.address2" theme="xhtml"
		cssClass="text large" /></li>
	<li>
	<div>
	<div class="left"><s:textfield key="plant.contactPerson"
		theme="xhtml" cssClass="text medium" /></div>
	<div><s:textfield key="plant.phone" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>
	<li><s:textfield key="plant.fax" theme="xhtml"
		cssClass="text medium" /></li>

	<li>
	<div>
	<div class="left"><s:textfield key="plant.ftpServer"
		theme="xhtml" required="true" cssClass="text medium" /></div>
	<div><s:textfield key="plant.ftpPort" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.ftpPath" required="true"
		theme="xhtml" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.ftpUser" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:password key="plant.ftpPassword"
		showPassword="true" theme="xhtml" required="true"
		cssClass="text medium" onchange="passwordChanged(this)" /></div>
	<div><s:password key="plant.confirmFtpPassword" theme="xhtml"
		showPassword="true" required="true" cssClass="text medium"
		onchange="passwordChanged(this)" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.tempFileDirectory" required="true"
		theme="xhtml" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.archiveFileDirectory" theme="xhtml"
		required="true" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.errorFileDirectory" required="true"
		theme="xhtml" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:select key="plant.inboundIntervalType"
		list="%{dateType}" theme="xhtml"></s:select></div>
	<div><s:textfield key="plant.inboundInterval" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:select key="plant.outboundIntervalType"
		list="%{dateType}" theme="xhtml"></s:select></div>
	<div><s:textfield key="plant.outboundInterval" theme="xhtml"
		required="true" cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.errorLogEmail1" theme="xhtml"
		required="true" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.errorLogEmail2" theme="xhtml"
		required="true" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:textfield key="plant.supplierNotifyEmail" theme="xhtml"
		required="true" cssClass="text large" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:select key="plant.doTemplateName" list="%{mFTemplate}"
		theme="xhtml" /></div>
	</div>
	</li>

	<li>
	<div>
	<div><s:select key="plant.boxTemplateName" list="%{boxTemplate}"
		theme="xhtml" /></div>
	</div>
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("plantForm"));
    highlightFormElements();
    
    function passwordChanged(passwordField) {
        if (passwordField.name == "plant.ftpPassword") {
            var origPassword = "<s:property value="plant.ftpPassword"/>";
        } else if (passwordField.name == "plant.confirmFtpPassword") {
            var origPassword = "<s:property value="plant.confirmFtpPassword"/>";
        }
        
        if (passwordField.value != origPassword) {
            createFormElement("input", "hidden",  "encryptPass", "encryptPass",
                              "true", passwordField.form);
        }
    }
</script>