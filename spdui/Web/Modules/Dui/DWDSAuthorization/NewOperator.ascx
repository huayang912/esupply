<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewOperator.ascx.cs" Inherits="Modules_Dui_DWDSAuthorization_NewOperator" %>
<h2>Add Operator</h2>

<p class="formBtnBoard">
 <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn" />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="BGform">
<table border="0">
<tr><td>
<asp:RadioButtonList AutoPostBack="true" id="rblType" CssClass=radio runat="server" OnSelectedIndexChanged="rblType_SelectedIndexChanged">
    <asp:ListItem Selected="True" Value="VIEWER">Data Viewer</asp:ListItem>
    <asp:ListItem Value="ADMIN">Data Administrator</asp:ListItem>
</asp:RadioButtonList> 
<asp:HiddenField ID="txtDsId" runat="server" />
</td></tr>
</table>
</div>
<asp:gridview id="gvOWNER" runat="server" DataKeyNames="Id" AutoGenerateColumns="False" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <Columns>
        <asp:TemplateField HeaderText="Data Viewer">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" Checked='<%# hasPermission((int)DataBinder.Eval(Container.DataItem, "Id"), Dndp.Persistence.Entity.Dui.DWDataSourceOperator.OPERATOR_VIEWER_Value) %>'  CssClass=radio />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="Operator Id"></asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="Operator Name"></asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>        
    </Columns>
    <HeaderStyle CssClass="listhead" />
    <AlternatingRowStyle CssClass="listA" />
</asp:gridview> 
<asp:gridview id="gvETL" runat="server" DataKeyNames="Id" Visible="False" AutoGenerateColumns="False" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <Columns>
        <asp:TemplateField HeaderText="Data Administrator">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" Checked='<%# hasPermission((int)DataBinder.Eval(Container.DataItem, "Id"), Dndp.Persistence.Entity.Dui.DWDataSourceOperator.OPERATOR_ADMIN_Value) %>'  CssClass=radio />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="Operator Id"></asp:BoundField>
        <asp:BoundField DataField="UserName" HeaderText="Operator Name"></asp:BoundField>
        <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>        
    </Columns>
    <HeaderStyle CssClass="listhead" />
    <AlternatingRowStyle CssClass="listA" />
</asp:gridview>

<asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>