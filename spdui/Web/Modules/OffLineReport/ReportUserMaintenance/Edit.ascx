<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_OffLineReport_ReportUserMaintenance_Edit" %>
<%@ Register Src="NewUserReport.ascx" TagName="NewUserReport" TagPrefix="uc1" %>
<%@ Register Src="NewUserParameter.ascx" TagName="NewUserParameter" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
<!-- Modified By Vincent On 2006-9-4 -->

    <script language="vbscript" type="text/vbscript">
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

    <h2>
        Report User Maintanence</h2>
    <b>Basic Information:</b>
    <p class="formBtnBoard">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
            CssClass="btn" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
    <div class="bgForm">
        <table border="0">
            <tr>
                <td class="txtForm">Distribution User:</td>
                <td><asp:TextBox ID="txtDistributionUserName" runat="server" Width="200" ReadOnly="true"/>    
                </td>                
            </tr>
	        <tr>
                <td class="txtForm">Report User Name:</td>
                <td><asp:TextBox ID="txtName" runat="server" Width="200"/></td>
                <td class="txtForm">&nbsp;Description:</td>
                <td><asp:TextBox ID="txtDescription" runat="server" Width="200"/></td>
            </tr>        
            <tr>            
                <td class="txtForm">Report Site:</td>
                <td><asp:TextBox ID="txtReportSite" runat="server" Width="200"/></td>
                <td class="txtForm">&nbsp;Report Library:</td>
                <td><asp:TextBox ID="txtReportLibrary" runat="server" Width="200"/></td>
            </tr>
            <tr>
                <td class="txtForm">Report Read User List:</td>
                <td><asp:TextBox ID="txtReportReadUsers" runat="server" Width="200"/></td>
                <td class="txtForm">&nbsp;Report Full Control User List:</td>
                <td><asp:TextBox ID="txtReportFullControlUsers" runat="server" Width="200"/></td>
            </tr>            
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <b>Report Definitions:</b>
    <p class="formBtnBoard">
        <asp:Button ID="btnAddReportSheet" runat="server" Text="Add Report" Width="100px"
            OnClick="btnAddReportSheet_Click" CssClass="btn" />
        <asp:Button ID="btnDeleteReportSheet" runat="server" Text="Delete" OnClick="btnDeleteReportSheet_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvReportList" AutoGenerateColumns="False" PageSize="20"
        DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Report Name">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheReport.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheReport.Description")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheReport.ReportType")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Template File Path">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheReport.TemplateFilePath")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
    <b>Parameter Definitions:</b>
    <p class="formBtnBoard">
        <asp:Button ID="btnAddParameter" runat="server" Text="Add Parameter" Width="100px"
            OnClick="btnAddParameter_Click" CssClass="btn" />
        <asp:Button ID="btnDeleteParameter" runat="server" Text="Delete" OnClick="btnDeleteParameter_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvParameterList" AutoGenerateColumns="False" PageSize="20"
        DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Parameter Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnParameterName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheParamter.Name") %>'
                        CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        OnClick="lbtnParameterName_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Value">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ParameterContent")%>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
</asp:Panel>
<uc1:NewUserReport ID="NewUserReport1" runat="server" Visible="False" />
<uc2:NewUserParameter ID="NewUserParameter1" runat="server" Visible="False" />
