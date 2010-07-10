<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Distribution_DistributionUser_New" %>
<h2>Distribution User Maintanence</h2>

<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="bgForm">
<table border="0">
	<tr>
        <td class="txtForm">User Name:</td>
        <td><asp:TextBox ID="txtName" runat="server" Width="200"/></td>
        <td class="txtForm">&nbsp;Description:</td>
        <td><asp:TextBox ID="txtDescription" runat="server" Width="200"/></td>
    </tr>
    
    <tr>
        <td class="txtForm">Email:</td>
        <td><asp:TextBox ID="txtEmail" runat="server" Width="200"/></td>
        <td class="txtForm">Domain Account:</td>
        <td><asp:TextBox ID="txtDomainAccount" runat="server" Width="200"/></td>
    </tr>
    
    <tr>
        <td class="txtForm">Is Offline Report User:</td>
        <td><asp:CheckBox ID="cbReportUser" runat="server" Checked="true" /></td>
        <td class="txtForm">Is Online Cube User:</td>
        <td><asp:CheckBox ID="cbOnlineCubeUser" runat="server" Checked="true" /></td>
    </tr>
    
    <tr>
        <td class="txtForm">Is Offline Cube User:</td>
        <td><asp:CheckBox ID="cbOfflineCubeUser" runat="server" Checked="true" /></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table></div>