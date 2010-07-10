<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewWithDrawTable.ascx.cs" Inherits="Modules_Dui_DSMaintenance_NewWithDrawTable" %>
<asp:HiddenField ID="txtDsId" runat="server" />
<h2>Data Source Maintenance</h2>
<p class="formBtnBoard">
<asp:Button ID="btnSubmit" runat="server" Text="Save & Back" OnClick="btnSubmit_Click" CssClass="btn" />
<asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click" CssClass="btn" />
<asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="BGform">
<table border="0">
<tr>
    <td Class="txtform">WithDrawed Table Name:</td>
    <td><asp:TextBox ID="txtWithDrawTableName" runat="server" Width="100"></asp:TextBox></td>
</tr>
</table>
</div>