<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_ETLExecution_Main" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc1" %>
<h2>Data Loading</h2>
<asp:Panel ID="pnlMain" runat="server">
<script language="vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
        case "Execute"
            ReturnVal = msgbox("Are you sure you want to execute the ETL Package?",33)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if
    end select
end function
</script>
<p class="formBtnBoard1">
    <asp:Button ID="btnRefresh" Text="Refresh Status" runat="server" OnClick="btnRefresh_Click" CssClass="btn"/>
    <asp:Button ID="btnRunETL" Text="Execute ETL Package" runat="server" OnClick="btnRunETL_Click" OnClientClick="return ButtonWarning('Execute')" CssClass="btn"/>
</p>
<table border="0" width="100%" cellpadding=5>
    <tr>
        <td width="33%" valign="top" class="txtForm">
            <div align=left>Data List Ready for Loading</div>
            <asp:GridView runat="server" ID="gvDataUploadForETL" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvErrorValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Data Source" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.TheDataSource.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Data Name" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ProcessStatus")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upload Date" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "UploadDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>            
        </td>
        <td width="33%" valign="top" class="txtForm">
            <div align=left>Data Loading In Progress</div>
            <asp:GridView runat="server" ID="gvDataUploadInETL" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvErrorValidationRuleResource" CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listheader" />                
                <Columns>                    
                    <asp:TemplateField HeaderText="Data Source" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.TheDataSource.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Category" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Data Name" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Status" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "ProcessStatus")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Upload Date" meta:resourcekey="TemplateFieldResource1" >
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "UploadDate")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>            
        </td>
        <td width="33%" valign="top" class="txtForm">
            <div align=left>Data Loading History</div>
            <asp:GridView runat="server" ID="gvETLAgentLog" AutoGenerateColumns="False" AllowPaging = "true" PageSize="10" CellPadding="4" CssClass="list" GridLines="Horizontal" OnPageIndexChanging="gvETLAgentLog_PageIndexChanging" >
                <HeaderStyle CssClass="listheader" />                
                <Columns>
                <asp:TemplateField HeaderText="Start Time">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LogBatchNo") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="End Time">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnEndTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LogBatchNo") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Status">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LogBatchNo") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Error Number">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnErrorNumber" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ErrorNumber") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LogBatchNo") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Error Batch No.">
                    <ItemTemplate>
                        <asp:LinkButton ID="lbtnLogBatchNo" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LogBatchNo") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LogBatchNo") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
               </Columns>
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>            
        </td>
    </tr>
</table>     
</asp:Panel>
<uc1:History ID="History1" runat="server" Visible="False"/>