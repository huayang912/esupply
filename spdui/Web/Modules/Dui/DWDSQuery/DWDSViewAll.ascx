<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DWDSViewAll.ascx.cs" Inherits="Modules_Dui_DWDSUpdate_DWDSViewAll" %>
<h2>
    Validated Data List for View:</h2>
<div class="BGform">
    <table border="0">
        <tr>
            <td class="txtForm">
                Condition:&nbsp;
                <asp:TextBox ID="txtCondition" runat="server" Width="500" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                    CssClass="btn1" />
                <asp:Button ID="btnDownload" runat="server" Text="Donwload" OnClick="btnDonwload_Click"
                    CssClass="btn1" />
                <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn1" />
            </td>
        </tr>
    </table>
</div>
<table border="0">
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
<asp:GridView ID="gvDWDSViewAll" meta:resourcekey="gvDWDSViewAll" runat="server"
    AllowPaging="True" PageSize="20" OnPageIndexChanging="gvDWDSViewAll_PageIndexChanging"
    CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
