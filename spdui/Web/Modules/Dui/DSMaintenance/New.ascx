<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Dui_DSMaintenance_New" %>
<h2>
    Data Source Maintenance:</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click"
        CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Cancel" OnClick="btnBack_Click" CssClass="btn" /></p>
<div class="BGform">
    <table border="0">
        <tr>
            <td class="txtForm">
                Name:</td>
            <td>
                <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
            <td class="txtform">
                Type:</td>
            <td>
                <asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="txtForm" valign="top">
                Description:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtDescription" runat="server" Columns="80"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="txtForm" valign="top">
                Data Structure:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtDataStructure" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top" class="txtform">
                DW Query SQL:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtRuleContent" runat="server" Columns="80" Rows="15" TextMode="MultiLine">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
    </table>
</div>
