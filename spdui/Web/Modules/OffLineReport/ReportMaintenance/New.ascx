<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_OffLineReport_ReportMaintenance_New" %>
<!-- Modified By Vincent On 2006-9-4 -->

<h2>Report Template Maintenance</h2>
<p class="formBtnBoard"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn" /></p>
<div class="BGform">
<table border="0">
	<tr>
        <td class="txtForm">Name:</td>
        <td><asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
        <td class="txtform">Type:</td>
        <td><asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="txtForm">Template File Path:</td>
        <td colspan="3"><asp:TextBox ID="txtTemplateFilePath" runat="server" Width="300"/></td>
    </tr>    
    <tr>
        <td class="txtForm">Connection String:</td>
        <td colspan="3"><asp:TextBox ID="txtConnectionString" runat="server" Width="300" TextMode="Password"></asp:TextBox></td>
    </tr>
	<tr>
        <td class="txtForm"valign="top">Description:</td>
        <td colspan="3">&nbsp;<asp:TextBox ID="txtDescription" runat="server" Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table></div>