<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewMergeRule.ascx.cs" Inherits="Modules_Dui_DWDSMaintenance_NewMergeRule" %>
<h2>
    Merge Rule Maintenance</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click"
        CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="BGform">
    <table border="0" cellpadding="1" cellspacing="2">
        <tr>
            <td class="txtform">
                Rule Name:</td>
            <td>
                <asp:TextBox ID="txtRuleName" runat="server" Width="300"></asp:TextBox></td>
            <td class="txtform">
                Rule Type:</td>
            <td valign="middle">
                &nbsp;<asp:DropDownList ID="ddlRuleType" runat="server">
                    <asp:ListItem Text="Error" Value="ERROR"></asp:ListItem>
                    <asp:ListItem Text="Problem" Value="PROBLEM"></asp:ListItem>
                    <asp:ListItem Text="Warning" Value="WARNING"></asp:ListItem>
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="txtform">
                Dependence Rule:</td>
            <td colspan="3">
                &nbsp;<asp:DropDownList ID="ddlDependenceRule" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="top" class="txtform">
                Rule Description:</td>
            <td colspan="3">
                <asp:TextBox ID="txtRuleDescription" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
        </tr>
        <tr>
            <td valign="top" class="txtform">
                Validation Rule SQL:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtRuleContent" runat="server" Columns="80" Rows="15" TextMode="MultiLine">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="top" class="txtform">
                Rule Result SQL:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtRuleResultContent" runat="server" Columns="80" Rows="15"
                    TextMode="MultiLine">
                </asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="txtform">
                Create By:</td>
            <td>
                <asp:Label ID="lCreateBy" runat="server"></asp:Label>
            </td>
            <td class="txtform">
                Create Date:</td>
            <td>
                <asp:Label ID="lCreateDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="txtform">
                Last Update By:</td>
            <td>
                <asp:Label ID="lLastUpdateBy" runat="server"></asp:Label>
            </td>
            <td class="txtform">
                Last Update Date:</td>
            <td>
                <asp:Label ID="lLastUpdateDate" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="False" /></td>
        </tr>
    </table>
</div>