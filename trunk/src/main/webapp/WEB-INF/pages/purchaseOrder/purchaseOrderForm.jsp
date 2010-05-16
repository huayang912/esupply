<%@ include file="/common/taglibs.jsp"%>

<head>
<title><fmt:message key="purchaseOrderDetail.title" /></title>
<meta name="heading"
	content="<fmt:message key='purchaseOrderDetail.heading'/>" />
<script type="text/javascript"
	src="<c:url value='/scripts/selectbox.js'/>"></script>
</head>

<s:form name="purchaseOrderForm" action="savePurchaseOrder"
	method="post" validate="true">
	<li style="display: none"><s:hidden key="purchaseOrder.poNo" /></li>
	<li class="buttonBar right"><c:set var="buttons">
		<s:submit key="button.cancel" method="cancel" />
	</c:set> <c:out value="${buttons}" escapeXml="false" /></li>

	<li>
	<div>
	<div class="left"><s:label
		key="purchaseOrder.plantSupplier.plant.code" cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.plantSupplier.supplier.code"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:label key="purchaseOrder.plantName"
		cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.supplierName"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div class="left"><s:label key="purchaseOrder.plantAddress1"
		cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.supplierAddress1"
		cssClass="text medium" /></div>
	</li>

	<li>
	<div class="left"><s:label key="purchaseOrder.plantContactPerson"
		cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.supplierContactPerson"
		cssClass="text medium" /></div>
	</li>

	<li>
	<div class="left"><s:label key="purchaseOrder.plantPhone"
		cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.supplierPhone"
		cssClass="text medium" /></div>
	</li>

	<li>
	<div>
	<div class="left"><s:label key="purchaseOrder.plantFax"
		cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.supplierFax"
		cssClass="text medium" /></div>
	</div>
	</li>

	<li>
	<div>
	<div class="left"><s:label key="purchaseOrder.createDate"
		cssClass="text medium" /></div>
	<div><s:label key="purchaseOrder.status" cssClass="text medium" /></div>
	</div>
	</li>

	<display:table name="purchaseOrder.purchaseOrderDetailList"
		cellspacing="0" cellpadding="0" requestURI=""
		id="purchaseOrderDetailList" class="table">
		<display:column property="sequence"
			titleKey="purchaseOrderDetail.sequence" />
		<display:column property="item.code"
			titleKey="purchaseOrderDetail.itemCode" />
		<display:column property="item.description"
			titleKey="purchaseOrderDetail.itemDescription" />
		<display:column property="supplierItemCode"
			titleKey="purchaseOrderDetail.supplierItemCode" />
		<display:column property="uom" titleKey="purchaseOrderDetail.uom" />
		<display:column property="deliveryDate" format="{0,date,yyyy-MM-dd}"
			titleKey="purchaseOrderDetail.deliveryDate" />
		<display:column property="qty" titleKey="purchaseOrderDetail.qty" />
		<display:column property="shipQty"
			titleKey="purchaseOrderDetail.shipQty" />
	</display:table>

	<li class="buttonBar bottom"><c:out value="${buttons}"
		escapeXml="false" /></li>
</s:form>



<script type="text/javascript">
		Form.focusFirstElement(document.forms["purchaseOrderForm"]);
		highlightFormElements();
</script>
