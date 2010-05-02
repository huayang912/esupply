<%@ include file="/common/taglibs.jsp"%>

<head>
    <title><fmt:message key="plantDetail.title"/></title>
    <meta name="heading" content="<fmt:message key='plantDetail.heading'/>"/>
</head>

<s:form id="plantForm" action="savePlant" method="post" validate="true">
<s:hidden name="plant.Code" value="%{plant.Code}"/>

    <s:textfield key="plant.Name" required="true" cssClass="text medium"/>

    <li class="buttonBar bottom">         
        <s:submit cssClass="button" method="save" key="button.save" theme="simple"/>
        <c:if test="${not empty plant.Code}"> 
            <s:submit cssClass="button" method="delete" key="button.delete" onclick="return confirmDelete('plant')" theme="simple"/>
        </c:if>
        <s:submit cssClass="button" method="cancel" key="button.cancel" theme="simple"/>
    </li>
</s:form>

<script type="text/javascript">
    Form.focusFirstElement($("plantForm"));
</script>