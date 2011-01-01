<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Operation_Po_Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="ttd01">
                ${MasterData.Po.Supplier}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCode" runat="server" Visible="true" />
            </td>
            <td class="td01">
                ${MasterData.Po.Status}:
            </td>
            <td class="td02">
                <asp:DropDownList ID="ddlStatus" runat="server">
                    <asp:ListItem Selected="True" />
                    <asp:ListItem Text="Create" Value="Create" />
                    <asp:ListItem Text="Submit" Value="Submit" />
                    <asp:ListItem Text="Cancel" Value="Cancel" />
                    <asp:ListItem Text="Close" Value="Close" />
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="ttd01">
                ${Common.Business.StartTime}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" Visible="true" />
                <ajax:CalendarExtender ID="ceStartDate" TargetControlID="tbStartDate" Format="yyyy-MM-dd"
                    runat="server" />
            </td>
            <td class="td01">
                ${Common.Business.StartDate}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" Visible="true" />
                <ajax:CalendarExtender ID="ceEndDate" TargetControlID="tbEndDate" Format="yyyy-MM-dd"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td colspan="3" />
            <td class="td02">
                <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                    CssClass="button2" />
            </td>
        </tr>
    </table>
</fieldset>
