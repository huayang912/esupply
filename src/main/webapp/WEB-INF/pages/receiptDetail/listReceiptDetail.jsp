<%@ include file="/common/taglibs.jsp"%>

<script type="text/javascript">
	var cal = new CalendarPopup();   
</script>

<head>
<title><fmt:message key="receiptList.title" /></title>
<meta name="heading" content="<fmt:message key='receiptList.heading'/>" />
<meta name="menu" content="OrderMenu" />
<script type="text/javascript"
	src="<c:url value='/scripts/CalendarPopup.js'/>"></script>
<script type="text/javascript">
	function cascadeUpdateSupplier(plantSelect) {
		if (plantSelect.options(plantSelect.selectedIndex).value != "-1") {	
			SupplierManager.getSuppliersByPlantAndUser(plantSelect.options(plantSelect.selectedIndex).value + "|${pageContext.request.remoteUser}", supplierSelectHandler);
		}
		else
		{
			SupplierManager.getAuthorizedSupplier("${pageContext.request.remoteUser}", supplierSelectHandler);
		}
	}

	function supplierSelectHandler(suppliers) {
		 DWRUtil.removeAllOptions("searchReceiptDetail_receipt_sCode");
		 if (suppliers != null) {		 
			 DWRUtil.addOptions("searchReceiptDetail_receipt_sCode",[{ name:'All', code:'-1' }], "code", "name");    
		 	DWRUtil.addOptions("searchReceiptDetail_receipt_sCode",suppliers, "code", "name");    
		 }
	}
</script>
</head>

<s:form name="receiptForm" action="searchReceiptDetail" method="post"
	validate="true">
	<div style="display: none;"><input type="hidden" name="page"
		value="1" /> <input type="hidden" name="pageSize" value="25" /></div>
	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.referenceReceiptNoLong" /></label></td>
			<td colspan="2"><s:textfield
				key="receipt.referenceReceiptNoLong" cssClass="text medium"
				theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="receipt.pCode"
				list="%{plants}" listKey="code" listValue="name" headerKey="-1" headerValue="All"
				theme="simple" onchange="cascadeUpdateSupplier(this);"/></td>
				
			<td><label class="desc"><fmt:message
				key="plantSupplier.supplier" /></label></td>
			<td colspan="2"><s:select key="receipt.sCode"
				list="%{suppliers}" listKey="code" listValue="name" headerKey="-1" headerValue="All"
				theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.referenceReceiptNo" /></label></td>
			<td colspan="2"><s:textfield key="receipt.referenceReceiptNo"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="receipt.receiptNo" /></label></td>
			<td colspan="2"><s:textfield key="receipt.receiptNo"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.itemCode" /></label></td>
			<td colspan="2"><s:textfield key="receipt.itemCode"
				cssClass="text medium" theme="simple" /></td>
			<td><label class="desc"><fmt:message
				key="receipt.supplierItemCode" /></label></td>
			<td colspan="2"><s:textfield key="receipt.supplierItemCode"
				cssClass="text medium" theme="simple" /></td>
		</tr>
		<tr>
			<td><label class="desc"><fmt:message
				key="receipt.postingDateFrom" /></label></td>
			<td><s:textfield key="receipt.postingDateFrom"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['receiptForm'].receipts_receipt_postingDateFrom,'anchDateFrom','MM/dd/yyyy'); return false;"
				NAME="anchDateFrom" ID="anchDateFrom"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
			<td><label class="desc"><fmt:message
				key="receipt.postingDateTo" /></label></td>
			<td><s:textfield key="receipt.postingDateTo"
				cssClass="text short" theme="simple" /></td>
			<td><A HREF="#"
				onClick="cal.select(document.forms['receiptForm'].receipts_receipt_postingDateTo,'anchDateTo','MM/dd/yyyy'); return false;"
				NAME="anchDateTo" ID="anchDateTo"><img
				src="<c:url value="/images/calendar.png"/>" border="0" /></A></td>
		</tr>
		<tr>
			<td></td>
			<td><s:radio name="receipt.detailOrSummary"
				list="%{detailOrSummary}" theme="simple"></s:radio></td>
		</tr>
	</table>
	</li>
	<div><s:submit method="list" key="button.search" theme="simple" />
	<s:submit key="button.cancel" method="cancel" theme="simple" /></div>
</s:form>