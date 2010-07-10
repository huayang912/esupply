<%@ Control Language="C#" AutoEventWireup="true" CodeFile="SelectUser.ascx.cs" Inherits="Modules_Distribution_DistributionUser_SelectUser" %>
<asp:Panel ID="pnlMain" runat="server">
<h2>Select Distribution User</h2>
<div class="BGform">
<table border="0">
    <tr>
        <td class="txtForm" style="height:25">        
           Name:&nbsp;
           <asp:TextBox ID="txtUserName" runat="server"/>&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn1" />
           <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
        </td>
    </tr>
</table>
</div>
    <asp:GridView runat="server" ID="gvList" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnPageIndexChanging="gvList_PageIndexChanging" CellPadding="4" CssClass="list" GridLines="Horizontal" >
    <PagerSettings Mode="NumericFirstLast" />
    <HeaderStyle CssClass="listheader" />
    <Columns>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandName="Select" ></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="Email" HeaderText="EMAIL" />      
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
<asp:Label ID="lblRecordCount" runat="server" />
</asp:Panel>