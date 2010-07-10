<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_DWDSQuery_Main" %>
<%@ Register Src="DWDSViewAll.ascx" TagName="DWDSViewAll" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
<h2>Data Source List</h2>
<div class="BGform">
<table border="0">
    <tr>
        <td class="txtForm" style="height:25">
           Data Source Type:&nbsp;&nbsp;
           <asp:DropDownList ID="ddlDSType" runat="server" /> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           Data Source Name:&nbsp;&nbsp;
           <asp:TextBox ID="txtDSName" runat="server"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" CssClass=btn1 />
        </td>
    </tr>
</table>
</div>
<asp:GridView runat="server" ID="gvDWDSList" AutoGenerateColumns="False" meta:resourcekey="gvDSCategoryResource"
    AllowPaging="True" PageSize="15" OnPageIndexChanging="gvDWDSList_PageIndexChanging" OnRowDataBound="gv_RowDataBound"
    CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listheader" />
    <Columns>
        <asp:BoundField HeaderText="QueryStartDate" DataField="QueryStartDate"/>
        <asp:BoundField HeaderText="QueryEndDate" DataField="QueryEndDate"/>
        <asp:TemplateField HeaderText="Type">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "DSType")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Data Source">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Query Date">
            <ItemTemplate>
                <asp:DropDownList ID="ddlQueryDate" runat="server"/>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Description")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                    CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnDownload_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnViewAll" runat="server" Text="[View All Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                    CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnViewAll_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>            
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
</asp:Panel>
<uc2:DWDSViewAll ID="DWDSViewAll1" runat="server" Visible="False" />
