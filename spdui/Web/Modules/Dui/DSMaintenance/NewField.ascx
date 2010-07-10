<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewField.ascx.cs" Inherits="Modules_Dui_DSMaintenance_NewField" %>
<h2>Field</h2>
<!-- Modified By Vincent On 2006-9-2 Begin -->
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save & Back" OnClick="btnSubmit_Click"
        CssClass="btn" />    
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click"
        CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="BGform">
    <table border="0" cellpadding="2" cellspacing="2" >
        <tr>
            <td width="100" class="txtform">
                Field Name:</td>
            <td>
                <asp:TextBox ID="txtFieldName" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="txtform">
                Field Type:</td>
            <td>
                &nbsp;<asp:DropDownList ID="ddlFieldType" runat="server">
                    <asp:ListItem Selected="True">Text</asp:ListItem>
                    <asp:ListItem>Integer</asp:ListItem>
                    <asp:ListItem>Numeric</asp:ListItem>
                    <asp:ListItem>DateTime</asp:ListItem>
                </asp:DropDownList></td>
        </tr>
        <tr>
            <td class="txtform">
                Field Length:</td>
            <td>
                <asp:TextBox ID="txtFieldLength" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="txtform">
                &nbsp;</td>
            <td>
                <asp:CheckBox ID="cbAllowNull" runat="server" Text="Allow Null" /><asp:CheckBox ID="cbDataKey"
                    runat="server" Text="Data Key" /></td>
        </tr>
        <tr>
            <td class="txtform">
                Description:</td>
            <td>
                <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox></td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="False" /></td>
                <td></td>
        </tr>
    </table>
</div>
<!-- Modified By Vincent On 2006-9-2 End -->


