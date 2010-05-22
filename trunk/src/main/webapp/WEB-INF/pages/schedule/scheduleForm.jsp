<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="scheduleDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='scheduleDetail.heading'/>" />
<meta name="menu" content="OrderMenu" />
</head>

<s:form name="scheduleForm" action="saveSchedule" method="post"
	validate="true">
	
	<input type="hidden" name="from" value="${param.from}" />

	<c:set var="buttons">
		<s:submit key="button.cancel" method="cancel" />
	</c:set>

	<c:choose>
		<c:when test="${schedule == null}">
			<p><fmt:message key="scheduleDetail.notFound" /></p>
		</c:when>
		<c:otherwise>

			<table width="100%">
				<tr>
					<td><s:label key="schedule.scheduleNo" cssClass="text medium" /></td>
					<td></td>
				</tr>
				<tr>
					<td><s:label key="schedule.plantSupplier.plant.code"
						cssClass="text medium" /></td>
					<td><s:label key="schedule.plantSupplier.supplier.code"
						cssClass="text medium" /></td>
				</tr>
				<tr>
					<td><s:label key="schedule.plantName" cssClass="text medium" /></td>
					<td><s:label key="schedule.supplierName"
						cssClass="text medium" /></td>
				</tr>
				<tr>
					<td><s:label key="schedule.plantAddress1"
						cssClass="text medium" /></td>
					<td><s:label key="schedule.supplierAddress1"
						cssClass="text medium" /></td>
				</tr>
				<tr>
					<td><s:label key="schedule.plantContactPerson"
						cssClass="text medium" /></td>
					<td><s:label key="schedule.supplierContactPerson"
						cssClass="text medium" /></td>
				</tr>
				<tr>
					<td><s:label key="schedule.plantPhone" cssClass="text medium" /></td>
					<td><s:label key="schedule.supplierPhone"
						cssClass="text medium" /></td>
				</tr>
				<tr>
					<td><s:label key="schedule.plantFax" cssClass="text medium" />
					</td>
					<td><s:label key="schedule.supplierFax" cssClass="text medium" /></td>
				</tr>
			</table>

			<table id="purchaseOrderDetailList" cellpadding="0" class="table"
				cellspacing="0">
				<thead>
					<tr>
						<th><fmt:message key="scheduleItem.Item.Code" /></th>
						<th><fmt:message key="scheduleItem.Item.Description" /></th>
						<th><fmt:message key="scheduleItem.SupplierItemCode" /></th>
						<s:iterator id="scheduleType"
							value="%{scheduleView.scheduleHead.scheduleTypeList}">
							<th>${scheduleType}</th>
						</s:iterator>
					</tr>
				</thead>
				<tbody>
					<tr class="odd">
						<td></td>
						<td></td>
						<td></td>
						<s:iterator id="dateFrom"
							value="%{scheduleView.scheduleHead.dateFromList}">
							<td>${dateFrom}</td>
						</s:iterator>
					</tr>
					<tr class="even">
						<td></td>
						<td></td>
						<td></td>
						<s:iterator id="dateTo"
							value="%{scheduleView.scheduleHead.dateToList}">
							<td>${dateTo}</td>
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
						<td>${scheduleBody.itemCode}</td>
						<td>${scheduleBody.itemDescription}</td>
						<td>${scheduleBody.supplierItemCode}</td>
						<s:iterator id="qty" value="#scheduleBody.qtyList">
							<td>${qty}</td>
						</s:iterator>
						</tr>
					</s:iterator>
				</tbody>
			</table>
		</c:otherwise>
	</c:choose>
	<div class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></div>
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
