<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Security_UserAdmin_Edit" %>
<!-- Modified By Vincent On 2006-9-4 -->

<h2>
    User Maint.</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" /></p>
<div class="BGform">
    <table border="0">
        <tr>
            <td class="txtForm">
                User Name:</td>
            <td colspan="5">
                <asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
        </tr>
        <tr runat="server" id="trPassword">
            <td class="txtForm">
                Passowrd:</td>
            <td colspan="5">
                <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
                <asp:CheckBox ID="cbPassword" runat="server" Checked="false" Text="Update Password" />
            </td>
        </tr>
        <tr runat="server" id="trConfirmPassword">
            <td class="txtForm">
                Confirm Password:</td>
            <td colspan="5">
                <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="txtForm">
                Email:</td>
            <td>
                <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            <td class="txtForm">
                Windows Domain:</td>
            <td>
                <asp:TextBox ID="txtWindowsDomain" runat="server"></asp:TextBox></td>
            <td class="txtForm">
                Windows User Name:</td>
            <td>
                <asp:TextBox ID="txtWindowsUserName" runat="server"></asp:TextBox></td>
        </tr>
    </table>
</div>

    <span class="txtForm">Roles:</span>
    <p class="formBtnBoard">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
            CssClass="btn" />
    
    <asp:GridView runat="server" ID="gvRole" AutoGenerateColumns="False" DataKeyNames="Id"
        OnDataBound="gvRole_DataBound" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <Columns>
            <asp:BoundField DataField="Name" HeaderText="Role Name" />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:TemplateField HeaderText="Selected?">
                <ItemTemplate>
                    <asp:CheckBox ID="cbInRole" runat="server" Checked='<%# Eval("InRole") %>' CssClass="radio" />
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <HeaderStyle CssClass="listhead" />
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
    </p>
