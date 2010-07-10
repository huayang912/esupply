<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewUserParameter.ascx.cs" Inherits="Modules_OffLineReport_ReportUserMaintenance_NewUserParameter" %>
<asp:HiddenField ID="txtReportUserId" runat="server"/>
<asp:HiddenField ID="txtParameterId" runat="server"/>
<!-- Modified By Vincent On 2006-9-4 -->

<h2>User Parameter Maintenance</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="BGform">
<table border="0">
	<tr>
        <td class="txtForm" height="25">Parameter Name:</td>
        <td><asp:DropDownList ID="ddlParameter" runat="server"/>
        <asp:Label ID="lblParameter" runat="server"/></td>
        <td class="txtForm">Parameter Value:</td>
        <td><asp:TextBox ID="txtParameterContent" runat="server" Width="300"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="False"/>
        </td>
    </tr>
</table></div>