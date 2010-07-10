<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_DWDSUpdate_Main" %>
<%@ Register Src="DWDSUpdate.ascx" TagName="DWDSUpdate" TagPrefix="uc1" %>
<%@ Register Src="DWDSViewAll.ascx" TagName="DWDSViewAll" TagPrefix="uc2" %>
<!-- Modified By Vincent On 2006-9-4 -->
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
<asp:GridView Width="100%" runat="server" ID="gvDWDSList" AutoGenerateColumns="False"
    meta:resourcekey="gvDSCategoryResource" AllowPaging="true" PageSize="15" OnPageIndexChanging="gvDWDSList_PageIndexChanging"
    CellPadding="4" CssClass="list" GridLines="Horizontal" >
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
    <Columns>
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
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Description")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ShowHeader="true" HeaderText="Action" meta:resourcekey="TemplateFieldResource8"
            ItemStyle-Wrap="false">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnDownload" runat="server" Text="[Download All]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                    CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnDownload_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbtnViewAll" runat="server" Text="[View All Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                    CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnViewAll_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbtnDownloadUpdate" runat="server" Text="[Download Updatable Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                    CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnDownloadUpdate_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lbtnUpdate" runat="server" Text="[Update Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id")%>'
                    Visible='<%# DataBinder.Eval(Container.DataItem, "DeleteQuerySQL") != null && (DataBinder.Eval(Container.DataItem, "DeleteQuerySQL").ToString().Length > 0)%>'
                    CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnUpdate_Click"></asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" />
        </asp:TemplateField>
    </Columns>
</asp:GridView>
</asp:Panel>
<uc1:DWDSUpdate ID="DWDSUpdate1" runat="server" Visible="False" />
<uc2:DWDSViewAll ID="DWDSViewAll1" runat="server" Visible="False" />
