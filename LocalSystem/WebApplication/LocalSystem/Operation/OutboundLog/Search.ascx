<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_OutboundLog_Search" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajax" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                  ${Common.Business.StartTime}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbStartDate" runat="server" />
                <ajax:CalendarExtender ID="ceStartDate" TargetControlID="tbStartDate" Format="yyyy-MM-dd"
                    runat="server" />
            </td>
            <td class="td01">
                ${Common.Business.StartDate}:
            </td>
            <td class="td02">
                <asp:TextBox ID="tbEndDate" runat="server" />
                <ajax:CalendarExtender ID="ceEndDate" TargetControlID="tbEndDate" Format="yyyy-MM-dd"
                    runat="server" />
            </td>
        </tr>
        <tr>
            <td class="td01">
            </td>
            <td class="td02">
            </td>
            <td class="td01" />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
