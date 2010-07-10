<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewReportSheet.ascx.cs" Inherits="Modules_OffLineReport_ReportMaintenance_NewReportSheet" %>
<asp:HiddenField ID="txtReportId" runat="server" />
<asp:HiddenField ID="txtReportSheetId" runat="server"/>
<!-- Modified By Vincent On 2006-9-4 -->
<!-- VINCENT TODO: Add Sequence No. Type Code -->

<h2>Sheet Maintenance</h2>
<p class="formBtnBoard">
<asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>

<div class="BGform">
<table border="0">
<tr>
    <td class="txtForm">Sheet Name:</td>
    <td><asp:TextBox ID="txtName" runat="server" Width="150"></asp:TextBox></td>
    <td class="txtForm">Sheet Description:</td>
    <td><asp:TextBox ID="txtDescription" runat="server" Width="300"></asp:TextBox></td>
</tr>
<tr>
    <td class="txtForm">Sequence No.:</td>
    <td><asp:TextBox ID="txtSequenceNo" runat="server" Width="150"></asp:TextBox><asp:RegularExpressionValidator
            ID="RegularExpressionValidator1" runat="server" ControlToValidate="txtSequenceNo"
            ErrorMessage="Must be digital" Font-Size="9pt" ValidationExpression="[0-9].*"></asp:RegularExpressionValidator></td>
    <td class="txtForm">Type:</td>
    <td>&nbsp;<asp:RadioButton ID="rdoSheetType_DataQuery" Checked="true" GroupName="rdoSheetType" runat="server" Text="Data Query" /> &nbsp;&nbsp;
        <asp:RadioButton ID="rdoSheetType_PivotTable" GroupName="rdoSheetType" runat="server" Text="Pivot Table"/></td>
</tr>
<tr>
    <td valign="top" class="txtForm">Parameters:</td>
    <td>
        &nbsp;<asp:DropDownList ID="ddlParameter" runat="server" Style="width:120;border-color:Red;">
        <asp:ListItem Text="P1" Value="P1"></asp:ListItem>
        <asp:ListItem Text="P2" Value="P2"></asp:ListItem>
        <asp:ListItem Text="P3" Value="P3"></asp:ListItem>
        </asp:DropDownList>
    </td>
    <td><asp:Button ID="btnInsertParameter" runat="server" Text="Insert" OnClick="btnInsertParameter_Click" CssClass="btn" /></td>
    <td></td>
</tr>
<tr>
    <td valign="top" class="txtForm">Query SQL:</td>
    <td colspan="3">
        &nbsp;<asp:TextBox ID="txtRuleContent" runat="server"  Columns="80" Rows="15" TextMode="MultiLine">
        </asp:TextBox>
    </td>
</tr>
<tr>
    <td class="txtForm">Create By:</td>
    <td>
        <asp:Label ID="lCreateBy" runat="server"></asp:Label>
    </td>
    <td class="txtForm">Create Date:</td>
    <td>
        <asp:Label ID="lCreateDate" runat="server"></asp:Label>
    </td>
</tr>
<tr>
    <td class="txtForm">Last Update By:</td>
    <td>
        <asp:Label ID="lLastUpdateBy" runat="server"></asp:Label>
    </td>
    <td class="txtForm">Last Update Date:</td>
    <td>
        <asp:Label ID="lLastUpdateDate" runat="server"></asp:Label>
    </td>
</tr>
<tr>
    <td colspan="4"><asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="False"/></td>
</tr>

</table>
</div>