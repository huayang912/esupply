<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_OffLineReport_BatchMaintenance_New" %>
<!-- Modified By Vincent On 2006-9-4 -->
<h2>Report Batch Maintenance:</h2>
<p class="formBtnBoard"><asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
            <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn" /></p>
<div class="BGform">
<table border="0">
	<tr>
        <td class="txtForm">Report Batch Name:</td>
        <td><asp:TextBox ID="txtName" runat="server"/></td>
        <td class="txtForm">Type:</td>
        <td><asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td class="txtForm">Description:</td>
        <td colspan="3"><asp:TextBox ID="txtDescription" runat="server" Width="400"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top" class="txtForm">SQL Before Job Run:</td>
        <td colspan="3">
            &nbsp;<asp:TextBox ID="txtPreRunSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine">
            </asp:TextBox>
        </td>
    </tr>    
    <tr>
    <td valign="top" class="txtForm">SQL After Job Run:</td>
    <td colspan="3">
        &nbsp;<asp:TextBox ID="txtPostRunSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine">
        </asp:TextBox>
    </td>
    </tr>
    <tr>
        <td class="txtForm">Notification Subject:</td>
        <td colspan="3"><asp:TextBox ID="txtSubject" runat="server" Width="300"/></td>
    </tr>
    <tr>
        <td class="txtForm">Notification Content:</td>
        <td colspan="3"><asp:TextBox ID="txtBody" runat="server" Columns="100" Rows="15" TextMode="MultiLine"/></td>
    </tr>  
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table></div>