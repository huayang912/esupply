<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Dui_DSUpload_History" %>
<asp:Panel ID="pnlMain" runat="server">

    <script language="vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
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
        case "WithDraw"
            ReturnVal = msgbox("Are you sure you want to withdraw the record?",17)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
         case "Archive"
            ReturnVal = msgbox("Are you sure you want to archive the record?",17)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if 
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
        Data History List for This data source</h2>
    <table>
        <tr>
            <td>
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
    </table>
    <!-- Modified By Vincent On 2006-9-2 Begin -->
    <div class="bgForm">
        <table border="0">
            <tr>
                <td class="txtForm" style="height: 25">
                    Category:&nbsp;
                    <asp:DropDownList ID="ddlDSCategory" runat="server" />&nbsp;&nbsp;&nbsp
                    Subject:&nbsp;
                    <asp:TextBox ID="txtSubject" runat="server"/>&nbsp;&nbsp;&nbsp;
                    File Name:&nbsp;
                    <asp:TextBox ID="txtFileName" runat="server"/>&nbsp;&nbsp;&nbsp;
                    Create By:&nbsp;
                    <asp:TextBox ID="txtCreateBy" runat="server"/>&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" meta:resourcekey="btnSearchResource1" CssClass=btn1 />
                    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn1" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView Width="100%" runat="server" ID="gvDSUploadHistory" AutoGenerateColumns="False"
        DataKeyNames="Id" meta:resourcekey="gvDSUploadHistoryResource" AllowPaging="True" PageSize="100"
        OnPageIndexChanging="gvDSUploadHistory_PageIndexChanging" CellPadding="4" CssClass="list"
        GridLines="Horizontal" OnRowDataBound="gvDSUploadHistory_RowDataBound">
        <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField ShowHeader="true" HeaderText="Category">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.Name")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Name" ShowHeader="true" HeaderText="Subject" />
            <asp:BoundField DataField="UploadFileOriginName" ShowHeader="true" HeaderText="File Name" />
            <asp:BoundField DataField="RecordRows" ShowHeader="true" HeaderText="Record Rows" />
            <asp:TemplateField ShowHeader="true" HeaderText="Status">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ProcessStatus")%>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="Create By">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "UploadBy.UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="UploadDate" DataFormatString="{0:d}" ShowHeader="true"
                HeaderText="Create Date">
                <ItemStyle HorizontalAlign="Center" />
            </asp:BoundField>
            <asp:TemplateField ShowHeader="true" HeaderText="ETL Confirmed By">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "ETLConfirmBy.UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="WithDrawed By">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "WithDrawBy.UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="Row Deteted By">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "RowDeleteBy.UserName")%>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="true" HeaderText="Action" meta:resourcekey="TemplateFieldResource8"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:LinkButton ID="LinkDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0 || ((int)DataBinder.Eval(Container.DataItem, "IsArchive")) == 1 %>'
                        OnClick="lbtnDownload_Click"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnConfirm" runat="server" Text="[Confirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED) && ((int)DataBinder.Eval(Container.DataItem, "Errors")) == 0 %>'
                        OnClick="lbtnConfirm_Click" OnClientClick="return ButtonWarning('Confirm')"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnUnconfirm" runat="server" Text="[Unconfirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" Visible='<%# (DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED) || DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED)) %>'
                        OnClick="lbtnUnconfirm_Click" OnClientClick="return ButtonWarning('UnConfirm')"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnWithDrawTable" runat="server" Text="[WithDraw Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_SUCCESS) && ((int)DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.TheDataSource.WithDrawTables")) == 1 && ((int)DataBinder.Eval(Container.DataItem, "IsWithdraw")) == 0 %>'
                        OnClick="lbtnWithDrawTable_Click" OnClientClick="return ButtonWarning('WithDraw')"></asp:LinkButton>
                    <%# ((int)DataBinder.Eval(Container.DataItem, "IsWithdraw")) == 1 ? "WithDrawed" : "" %>
                    <asp:LinkButton ID="lbtnArchiveTable" runat="server" Text="[Archive Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsArchive")) == 0 && ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0%>'
                        OnClick="lbtnArchiveTable_Click" OnClientClick="return ButtonWarning('Archive')"></asp:LinkButton>
                    <%# ((int)DataBinder.Eval(Container.DataItem, "IsArchive")) == 1 ? "Archived" : "" %>
                    <asp:LinkButton ID="LinkDeleteHistory" runat="server" Text="[Delete Raw Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsWithdraw")) == 1 && ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0 %>'
                        OnClick="lbtnDeleteHistory_Click" OnClientClick="return ButtonWarning('Delete')"></asp:LinkButton>
                    <%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 1 ? "RawDeleted" : "" %>
                    <asp:LinkButton ID="lbtnDownloadDWData" runat="server" Text="[Download DW Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" OnClick="lbtnDownloadDWData_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>
