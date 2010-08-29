<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_DSETLConfirm_Main" %>
<%@ Register Src="Validate.ascx" TagName="Validate" TagPrefix="uc1" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">

    <script type="text/javascript" src="Popup/lhgcore.min.js"></script>

    <script type="text/javascript" src="Popup/lhgdialog.js"></script>

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
    end select
end function
    </script>

    <h2>
        Data Confirmation</h2>
    <div class="bgForm">
        <table border="0">
            <tr>
                <td class="txtForm" style="height: 25">
                    Data Source Type:&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDSType" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Data Source Category:&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDSCategory" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    Data Source Status:&nbsp;&nbsp;
                    <asp:DropDownList ID="ddlDSStatus" runat="server" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                        meta:resourcekey="btnSearchResource1" CssClass="btn1" /><asp:Button ID="btnInValidation"
                            runat="server" OnClick="btnInValidation_Click" Style="display: none" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView runat="server" ID="gvDSCategory" AutoGenerateColumns="False" DataKeyNames="LastestDataSourceUpload"
        meta:resourcekey="gvDSCategoryResource" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvDSCategory_PageIndexChanging"
        CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" />
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
                    <asp:LinkButton ID="lbtnDSName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheDataSource.Description") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheDataSource.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSNameResource" OnClick="lbtnHistory_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Action" meta:resourcekey="TemplateFieldResource11">
                <ItemTemplate>
                    <%# GenerateRevalidateString((Dndp.Persistence.Entity.Dui.DataSourceCategory)Container.DataItem)%>
                    &nbsp;<asp:LinkButton ID="lbtnConfirm" runat="server" Text="[Confirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnConfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null  && DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED) && ((int)DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Errors")) == 0 %>'
                        OnClick="lbtnConfirm_Click" OnClientClick="return ButtonWarning('Confirm')"></asp:LinkButton>
                    <asp:LinkButton ID="lbtnUnconfirm" runat="server" Text="[Unconfirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") != null && (DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_CONFIRMED) || DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus").Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED)) %>'
                        OnClick="lbtnUnconfirm_Click" OnClientClick="return ButtonWarning('UnConfirm')"></asp:LinkButton>
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
            <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource4">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDSConfirmStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ProcessStatus")%>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnDSConfirmStatusResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Errors" meta:resourcekey="TemplateFieldResource6">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnErrors" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") == null || ((int)DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Errors")) == 0 ? "" : DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Errors").ToString() %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnErrorsResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Problems" meta:resourcekey="TemplateFieldResource7">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnProblems" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") == null || ((int)DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Problems")) == 0 ? "" : DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Problems").ToString() %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnProblemsResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Warnings" meta:resourcekey="TemplateFieldResource8">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnWarnings" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload") == null || ((int)DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Warnings")) == 0 ? "" : DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Warnings").ToString() %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnWarningsResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created By" meta:resourcekey="TemplateFieldResour9">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCreatedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.UploadBy.UserName") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnCreatedByResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Created Date" meta:resourcekey="TemplateFieldResource10">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCreatedDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.UploadDate", "{0:d}") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnCreatedDateResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Center" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Owner Confirmed By" meta:resourcekey="TemplateFieldResour11">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnOwnerConfirmedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.OwnerConfirmBy.UserName") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnOwnerConfirmedByResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ETL Confirmed By" meta:resourcekey="TemplateFieldResour12">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnETLConfirmedBy" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.ETLConfirmBy.UserName") %>'
                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestDataSourceUpload.Id") %>'
                        CommandName="Select" meta:resourcekey="lbtnETLConfirmedByResource" OnClick="lbtnValidate_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
</asp:Panel>
<!-- Modified By Vincent On 2006-9-1 Begin Rows:67-->
<!--<ItemStyle HorizontalAlign="Center" />-->
<!-- Modified By Vincent On 2006-9-1 End -->
<uc1:Validate ID="Validate1" runat="server" Visible="False" />
<uc2:History ID="History1" runat="server" Visible="False" />

<script language="javascript" type="text/javascript">
    function RevalidateAll(dsId, vrIds)
    {
	    var cusfn = function()
	    {
		    J.dialog.inwin['ValidationRule'].J('#xbtn').click( function(){ 
		        document.getElementById("<%= btnInValidation.ClientID %>").click(); 
		        J.dialog.inwin['ValidationRule'].cancel();
		    } );
		    
	    };
	    
	    var fn = function()
	    {
	        document.getElementById("<%= btnInValidation.ClientID %>").click(); 
		    J.dialog.inwin['ValidationRule'].cancel();
	    };
	
	    J.dialog.get( "ValidationRule", {  top: 90, cover: true, custom: fn, closeWin: cusfn, title : "Data Validation", page: "ValidationRule.aspx?dsUploadId=" + dsId + "&validationIds=" + vrIds } );   
    }
</script>

