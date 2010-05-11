<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="plantDetail.title" /></title>
<meta name="heading" content="<fmt:message key='plantDetail.heading'/>" />
</head>

<s:form id="plantForm" action="savePlant" method="post" validate="true">
	<li style="display: none"><input type="hidden" name="from"
		value="${param.from}" /></li>
	<li class="buttonBar right"><c:set var="buttons">
		<s:submit cssClass="button" method="save" key="button.save"
			theme="simple" />
		<c:if test="${param.from == 'list' and not empty plant.code}">
			<s:submit cssClass="button" method="delete" key="button.delete"
				onclick="return confirmDelete('plant')" theme="simple" />
		</c:if>
		<s:submit cssClass="button" method="cancel" key="button.cancel"
			theme="simple" />
	</c:set><c:out value="${buttons}" escapeXml="false" /></li>
	<li>
	<div>
	<div class="left"><s:textfield key="plant.code" theme="xhtml"
		required="true" cssClass="text medium" /></div>
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
		theme="xhtml" cssClass="text medium" /></div>
	<div><s:textfield key="plant.ftpPort" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:textfield key="plant.ftpUser" theme="xhtml"
		cssClass="text medium" /></div>
	<div><s:textfield key="plant.ftpPath" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:password key="plant.ftpPassword"
		showPassword="true" theme="xhtml" cssClass="text medium"
		onchange="passwordChanged(this)" /></div>
	<div><s:password key="plant.confirmFtpPassword" theme="xhtml"
		showPassword="true" cssClass="text medium"
		onchange="passwordChanged(this)" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:textfield key="plant.tempFileDirectory"
		theme="xhtml" cssClass="text medium" /></div>
	<div><s:textfield key="plant.archiveFileDirectory" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:textfield key="plant.errorFileDirectory"
		theme="xhtml" cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:select key="plant.inboundIntervalType"
		list="{'Year', 'Month', 'Week', 'Day', 'Hour', 'Minitus'}"
		theme="xhtml"></s:select></div>
	<div><s:textfield key="plant.inboundInterval" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:select key="plant.outboundIntervalType"
		list="{'Year', 'Month', 'Week', 'Day', 'Hour', 'Minitus'}"
		theme="xhtml"></s:select></div>
	<div><s:textfield key="plant.outboundInterval" theme="xhtml"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("plantForm"));

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