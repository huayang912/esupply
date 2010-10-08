<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewOperator.ascx.cs" Inherits="Modules_Dui_DSAuthorization_NewOperator" %>
<h2>
    Operator</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
        CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<asp:HiddenField ID="txtDsId" runat="server" />
<div class="BGform">
    <table border="0">
        <tr>
            <td>
                <asp:RadioButtonList AutoPostBack="true" ID="rblType" runat="server" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
                    <asp:ListItem Selected="True" Value="OWNER">Data Source Owner</asp:ListItem>
                    <asp:ListItem Value="ETL">ETL Confirmer</asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
    </table>
</div>
<asp:GridView ID="gvOWNER" runat="server" Width="100%" DataKeyNames="Id" AutoGenerateColumns="false"
    CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="Data Source Owner">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" Checked='<%# hasPermission((int)DataBinder.Eval(Container.DataItem, "Id"), "OWNER") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="Operator Id"></asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="Operator Name"></asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
    </Columns>
</asp:GridView>
<asp:GridView ID="gvETL" runat="server" Width="100%" DataKeyNames="Id" Visible="false"
    AutoGenerateColumns="false" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="ETL Confirmer">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" Checked='<%# hasPermission((int)DataBinder.Eval(Container.DataItem, "Id"), "ETL") %>' />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="Operator Id"></asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="Operator Name"></asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
    </Columns>
</asp:GridView>