<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="receiptDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='receiptDetail.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<s:form name="receiptForm" action="saveReceipt" method="post"
	validate="true">

	<c:set var="buttons">
		<s:submit key="button.cancel" method="cancel" />
	</c:set>

	<table width="100%">
		<tr>
			<td><s:label key="receipt.referenceReceiptNoLong"
				cssClass="text medium" /></td>
			<td><s:label key="receipt.postingDate" cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="receipt.referenceReceiptNo"
				cssClass="text medium" /></td>
			<td><s:label key="receipt.receiptNo" cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="receipt.plantCode" cssClass="text medium" /></td>
			<td><s:label key="receipt.supplierCode" cssClass="text medium" /></td>
		</tr>
		<tr>
			<td><s:label key="receipt.plantName" cssClass="text medium" /></td>
			<td><s:label key="receipt.supplierName" cssClass="text medium" /></td>
		</tr>
	</table>

	<display:table name="receipt.receiptDetailList" cellspacing="0"
		cellpadding="0" requestURI="" id="receiptDetail" class="table">
		<display:column property="referenceOrderNo"
			titleKey="receiptDetail.referenceOrderNo" />
		<display:column property="referenceSequence"
			titleKey="receiptDetail.referenceSequence" />
		<display:column property="item.code" titleKey="receiptDetail.itemCode" />
		<display:column property="itemDescription"
			titleKey="receiptDetail.itemDescription" />
		<display:column property="supplierItemCode"
			titleKey="receiptDetail.supplierItemCode" />
		<display:column property="uom" titleKey="receiptDetail.uom" />
		<display:column property="qty" titleKey="receiptDetail.qty" />
		<display:column property="plusMinus"
			titleKey="receiptDetail.plusMinus" />
	</display:table>

	<div class="buttonBar bottom">
	<div align="right"><c:out value="${buttons}" escapeXml="false" /></div>
	</div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["receiptForm"]);
		highlightFormElements();
</script>
