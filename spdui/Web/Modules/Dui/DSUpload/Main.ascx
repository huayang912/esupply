<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_DSUpload_Main" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc1" %>
<%@ Register Src="Validate.ascx" TagName="Validate" TagPrefix="uc2" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc3" %>
<%@ Register Src="ValidateUpdate.ascx" TagName="ValidateUpdate" TagPrefix="uc4" %>
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
        case "Confirm"
            ReturnVal = msgbox("Are you sure you want to confirm the record?",33)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
        case "UnConfirm"
            ReturnVal = msgbox("Are you sure you want to unconfirm the record?",33)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
    end select
end function
    </script>

    <h2>
        Data Preparation</h2>
    <div class="BGform">
        <table border="0">
            <tr>
                <td class="txtForm" style="height: 25">
                    Type:&nbsp;
                    <asp:DropDownList ID="ddlDSType" runat="server" />
                    &nbsp;&nbsp;&nbsp; Category:&nbsp;
                    <asp:DropDownList ID="ddlDSCategory" runat="server" />&nbsp;&nbsp;&nbsp; Status:&nbsp;
                    <asp:DropDownList ID="ddlDSStatus" runat="server" />&nbsp;&nbsp;&nbsp; Name:&nbsp;
                    <asp:TextBox ID="txtDSName" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                        CssClass="btn1" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView runat="server" ID="gvDSCategory" AutoGenerateColumns="False" DataKeyNames="LastestDataSourceUpload"
        meta:resourcekey="gvDSCategoryResource" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvDSCategory_PageIndexChanging"
        CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvDSCategory_RowDataBound">
        <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField HeaderText="Category" meta:resourcekey="TemplateFieldResource3">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDSCategoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheDataSource.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSCategoryNameResource" OnClick="lbtnHistory_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Type" meta:resourcekey="TemplateFieldResource1">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheDataSource.DSType") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data Source" meta:resourcekey="TemplateFieldResource1">
                <ItemTemplate>
                    <asp:Label ID="lbDataSource" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheDataSource.Description") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action" meta:resourcekey="TemplateFieldResource8">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDelete" runat="server" Text="[Delete]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDeleteResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null && (DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").Equals(""))%>'
                        OnClick="lbtnDelete_Click" OnClientClick="return ButtonWarning('Delete')"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnUpload" runat="server" Text="[Upload]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnUploadResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") == null || DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").ToString().Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_SUCCESS)%>'
                        OnClick="lbtnUpload_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnConfirm" runat="server" Text="[Confirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnConfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null && DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").Equals("")  && DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Errors").Equals(0)%>'
                        OnClick="lbtnConfirm_Click" OnClientClick="return ButtonWarning('Confirm')"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnUnconfirm" runat="server" Text="[Unconfirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null && DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").ToString().Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED) %>'
                        OnClick="lbtnUnconfirm_Click" OnClientClick="return ButtonWarning('UnConfirm')"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnTemplate" runat="server" Text="[Download Template]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnTemplate_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null%>'
                        OnClick="lbtnDownload_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnETLLog" runat="server" Text="[Log]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null && DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").ToString().Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED) %>'
                        OnClick="lbtnETLLog_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" Wrap="False" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Uploaded Data" meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDSUploadName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Name") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSUploadNameResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="File Name" meta:resourcekey="TemplateFieldResource2">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDSFileName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.UploadFileOriginName") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSUploadNameResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource4">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDSConfirmStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus")%>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSConfirmStatusResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Rows" meta:resourcekey="TemplateFieldResource4">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDSRecordRows" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.RecordRows")%>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSConfirmStatusResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created By" meta:resourcekey="TemplateFieldResource6">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.UploadBy.UserName") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnCreatedByResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created Date" meta:resourcekey="TemplateFieldResource7">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCreatedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.UploadDate", "{0:d}") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnCreatedDateResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner Confirmed By" meta:resourcekey="TemplateFieldResource8">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnOwnerConfirmedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.OwnerConfirmBy.UserName") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnOwnerConfirmedByResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner Confirmed Date" meta:resourcekey="TemplateFieldResource7">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnOwnerConfirmedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.OwnerConfirmDate", "{0:d}") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnOwnerConfirmedDateResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
        <PagerStyle HorizontalAlign="Right" />
    </asp:GridView>
</asp:Panel>
<uc1:New ID="New1" runat="server" Visible="False" />
<uc2:Validate ID="Validate1" runat="server" Visible="False" />
<uc3:History ID="History1" runat="server" Visible="False" />
<uc4:ValidateUpdate ID="ValidateUpdate1" runat="server" Visible="False" />
<!-- Modified By Vincent On 2006-9-1 Begin Rows:76 -->
<!--<ItemStyle HorizontalAlign="Center" />-->
<!-- Rows :72  -->
<!-- Modified By Vincent On 2006-9-1 End -->
