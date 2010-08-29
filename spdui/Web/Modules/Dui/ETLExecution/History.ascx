<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Dui_ETLExecution_History" %>
<!-- Modified By Vincent On 2006-9-2 Begin -->

<table border="0" width="100%">
    <tr>
        <td>
            <p class="formBtnBoard">
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn" />
            </p>
            <asp:GridView width="100%" runat="server" ID="gvDSUploadHistory" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvDSUploadHistoryResource" 
                CellPadding="4" CssClass="list" GridLines="Horizontal" >
                <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
                <Columns>                    
                    <asp:TemplateField ShowHeader="true" HeaderText="Category">
                        <ItemTemplate>
                            <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.Name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="Name" ShowHeader="true" HeaderText="Uploaded Data" />
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
                    <asp:BoundField DataField="UploadDate" DataFormatString="{0:d}" ShowHeader="true" HeaderText="Create Date" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="true" HeaderText="Action" meta:resourcekey="TemplateFieldResource8" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnDownload_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnETLLog" runat="server" Text="[Log]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "ProcessStatus").ToString().Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_ETL_FAILED) %>' OnClick="lbtnETLLog_Click"></asp:LinkButton>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>  
</table>
<!-- Modified By Vincent On 2006-9-1 Begin Rows:43-->
<!--<ItemStyle HorizontalAlign="Center" />-->
<!-- Modified By Vincent On 2006-9-1 End --> 
<!-- Modified By Vincent On 2006-9-2 End -->