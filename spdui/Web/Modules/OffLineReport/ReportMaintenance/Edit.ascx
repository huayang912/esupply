<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_OffLineReport_ReportMaintenance_Edit" %>
<%@ Register Src="NewReportSheet.ascx" TagName="NewReportSheet" TagPrefix="uc1" %>
<asp:Panel ID="pnlMain" runat="server">
<script language="vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
        case "Delete"
            ReturnVal = msgbox("Are you sure you want to delete the record?",17)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if
    end select
end function
</script>
<h2>Report Template Maintanence</h2>
<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn" />
    <asp:Button ID="btnUpdateConn" runat="server" Text="Save Connection" OnClick="btnUpdateConn_Click" CssClass="btn" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  CssClass="btn" OnClientClick="return ButtonWarning('Delete')"/>
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class=bgForm>
<table border="0">
	<tr>
        <td class="txtForm">Report Name:</td>
        <td><asp:TextBox ID="txtName" runat="server"/></td>
        <td class="txtForm">Type:</td>
        <td><asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td class="txtForm">Template File Path:</td>
        <td colspan="3"><asp:TextBox ID="txtTemplateFilePath" runat="server" Width="300"/></td>
    </tr>    
    <tr>
        <td class="txtForm">Connection String:</td>
        <td colspan="3"><asp:TextBox ID="txtConnectionString" runat="server" Width="300"></asp:TextBox></td>
    </tr>
	<tr>
        <td class="txtForm" valign="top">Description:</td>
        <td colspan="3">&nbsp;<asp:TextBox ID="txtDescription" runat="server" Columns="60" Rows="3" TextMode="MultiLine"></asp:TextBox></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
</div>
<b>Sheet Definitions:</b>
<p class="formBtnBoard">
<asp:Button ID="btnAddReportSheet" runat="server" Text="Add Sheet" Width="100px" OnClick="btnAddReportSheet_Click" CssClass="btn" />
<asp:Button ID="btnDeleteReportSheet" runat="server" Text="Delete" OnClick="btnDeleteReportSheet_Click" CssClass="btn" OnClientClick="return ButtonWarning('Delete')"/>
</p>
    <asp:GridView runat="server" ID="gvSheetList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Sheet Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnSheetName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' OnClick="lbtnSheetName_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="SequenceNo" HeaderText="Sequence No." />
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Create Date" />
            <asp:TemplateField HeaderText="Create By">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "CreateBy.UserName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="LastUpdateDate" DataFormatString="{0:d}" HeaderText="Last Update Date" />
            <asp:TemplateField HeaderText="Last Update By">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "LastUpdateBy.UserName") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>

</asp:Panel>
<uc1:NewReportSheet id="NewReportSheet1" runat="server" Visible="False"/>
