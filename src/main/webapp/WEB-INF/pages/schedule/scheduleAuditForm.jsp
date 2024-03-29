<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="scheduleAudit.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleAudit.heading'/>" />
<meta name="menu" content="OrderMenu" />
<script type="text/javascript">
function checkedAll (obj) {	
	var aa= document.getElementsByName("scheduleItem");
	for (var i =0; i < aa.length; i++) 
	{
	 	aa[i].checked = obj.checked;
	}
}
</script>
<script type="text/javascript">
	function cascadeUpdateSupplier(plantSelect) {		
		SupplierManager.getSuppliersByPlantAndUser(plantSelect.options(plantSelect.selectedIndex).value + "|${pageContext.request.remoteUser}", supplierSelectHandler);
	}

	function supplierSelectHandler(suppliers) {
		 DWRUtil.removeAllOptions("listScheduleAudit_plantSupplier_supplier_code");
		 if (suppliers != null) {
		 	DWRUtil.addOptions("listScheduleAudit_plantSupplier_supplier_code",suppliers,"code","name");    
		 }
	}
</script>
</head>

<c:set var="buttons">
	<s:submit key="button.confirm" method="confirm" theme="simple" />
</c:set>
<s:form name="scheduleForm" action="listScheduleAudit" method="post"
	validate="true">

	<li style="padding: 0px">
	<table style="margin: 0px">
		<tr>
			<td><label class="desc"><fmt:message
				key="plantSupplier.plant" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.plant.code"
				list="%{plants}" listKey="code" listValue="name" theme="simple" 
				onchange="cascadeUpdateSupplier(this);" /></td>

			<td><label class="desc"><fmt:message
				key="plantSupplier.supplier" /></label></td>
			<td colspan="2"><s:select key="plantSupplier.supplier.code"
				list="%{suppliers}" listKey="code" listValue="name" theme="simple" /></td>
		</tr>
	</table>
	</li>

	<div><s:submit method="listAudit" key="button.search"
		theme="simple" /><s:submit key="button.cancel" method="cancel"
		theme="simple" /></div>
	</ul>

	<c:if test="${plantSupplier != null}">
		<c:choose>
			<c:when test="${schedule == null}">
				<p><fmt:message key="scheduleAudit.notFound" /></p>
			</c:when>
			<c:otherwise>
				<table id="purchaseOrderDetailList" cellpadding="0" class="table"
					cellspacing="0">
					<thead>
						<tr>
							<th nowrap="nowrap"><fmt:message
								key="scheduleDetail.createDate" /></th>
							<th nowrap="nowrap"><fmt:message
								key="scheduleDetail.releaseNo" /></th>
							<th nowrap="nowrap"><fmt:message
								key="scheduleDetail.itemCode" /></th>
							<th nowrap="nowrap"><fmt:message
								key="scheduleDetail.itemDescription" /></th>
							<th nowrap="nowrap"><fmt:message
								key="scheduleDetail.supplierItemCode" /></th>
							<s:iterator id="head"
								value="%{scheduleView.scheduleHead.headList}">
								<th>${head.scheduleType}</th>
							</s:iterator>
						</tr>
					</thead>
					<tbody>
						<tr class="odd">
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td><fmt:message key="scheduleDetail.eta" /></td>
							<s:iterator id="dateFrom"
								value="%{scheduleView.scheduleHead.headList}">
								<td><fmt:formatDate value="${dateFrom}"
									pattern="MM/dd/yyyy" /></td>
							</s:iterator>
						</tr>
						<tr class="even">
							<td></td>
							<td></td>
							<td></td>
							<td></td>
							<td><fmt:message key="scheduleDetail.etd" /></td>
							<s:iterator id="dateTo"
								value="%{scheduleView.scheduleHead.headList}">
								<td><fmt:formatDate value="${dateTo}"
									pattern="MM/dd/yyyy" /></td>
							</s:iterator>
						</tr>
						<s:iterator id="scheduleBody"
							value="%{scheduleView.scheduleBodyList}" status="rowstatus">
							<s:if test="#rowstatus.odd == true">
								<tr class="odd">
							</s:if>
							<s:else>
								<tr class="even">
							</s:else>
							<td><input type="hidden" name="scheduleItem"
								value="${scheduleBody.scheduleItemId}" /> <fmt:formatDate
								value="${scheduleBody.createDate}" pattern="MM/dd/yyyy" /></td>
							<td>${scheduleBody.releaseNo}</td>
							<td nowrap="nowrap">${scheduleBody.itemCode}</td>
							<td nowrap="nowrap">${scheduleBody.itemDescription}</td>
							<td nowrap="nowrap">${scheduleBody.supplierItemCode}</td>
							<s:iterator id="qty" value="#scheduleBody.qtyList">
								<td><fmt:formatNumber value="${qty}" pattern="#,###.##" /></td>
							</s:iterator>
							</tr>
						</s:iterator>
					</tbody>
				</table>

				<c:out value="${buttons}" escapeXml="false" />
			</c:otherwise>
		</c:choose>

		<ul style="border: 0px; padding: 0px; margin: 0px">
	</c:if>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
