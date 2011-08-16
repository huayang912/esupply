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
					<td><s:label key="schedule.plantCode" cssClass="text medium" /></td>
					<td><s:label key="schedule.supplierCode"
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
			</ul>

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
							<c:choose>
								<c:when test="${head.createDo}">
									<th><a
										href="editDeliveryOrder.html?plantSupplierId=${schedule.plantSupplier.id}&dateFrom=<fmt:formatDate value="${head.dateFrom}" pattern="MM/dd/yyyy" />&scheduleType=${head.scheduleType}">${head.scheduleType}</a>
									</th>
								</c:when>
								<c:otherwise>
									<th>${head.scheduleType}</th>
								</c:otherwise>
							</c:choose>
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
							<td><fmt:formatDate value="${dateFrom}" pattern="MM/dd/yyyy" />
							</td>
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
							<td><fmt:formatDate value="${dateTo}" pattern="MM/dd/yyyy" /></td>
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
						<td><fmt:formatDate value="${scheduleBody.createDate}"
							pattern="MM/dd/yyyy" /></td>
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
		</c:otherwise>
	</c:choose>
	<div class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></div>
	<ul style="border: 0px; padding: 0px; margin: 0px">
</s:form>

<script type="text/javascript">
		Form.focusFirstElement(document.forms["scheduleForm"]);
		highlightFormElements();
</script>
