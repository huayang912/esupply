<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewCategory.ascx.cs" Inherits="Modules_Dui_DSAuthorization_NewCategory" %>
<h2>
    Category</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click"
        CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<asp:HiddenField ID="txtDsId" runat="server" />
<asp:HiddenField ID="txtDscId" runat="server" />
<div class="BGform">
    <table border="0">
        <tr>
            <td class="txtform" width="100">
                Category Name:</td>
            <td>
                <asp:TextBox ID="txtCategoryName" runat="server" Width="100" ReadOnly="true"></asp:TextBox></td>
        </tr>
        <tr>
            <td class="txtform">
                Category Description:</td>
            <td>
                <asp:TextBox ID="txtCategoryDescription" runat="server" Width="200" ReadOnly="true"></asp:TextBox></td>
        </tr>
    </table>
</div>
<asp:GridView ID="gvUser" runat="server" Width="100%" DataKeyNames="Id" AutoGenerateColumns="false"
    CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="Category User">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="Operator Id"></asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="Operator Name"></asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
    </Columns>
</asp:GridView>