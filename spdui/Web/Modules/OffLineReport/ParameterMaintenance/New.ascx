<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_OffLineReport_ParameterMaintenance_New" %>
<!-- Modified By Vincent On 2006-9-4 -->

<h2>Report Parameter Maintenance:</h2>
<p class="formBtnBoard"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn" /></p>
<div class="BGform">
<table border="0">
	<tr>
        <td class="txtForm">Parameter Name:</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="300"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table></div>