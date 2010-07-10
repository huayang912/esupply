<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Dui_DSUpload_History" %>
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
<h2>Data History List for This data source</h2>
<table>
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
<!-- Modified By Vincent On 2006-9-2 Begin -->

<table border="0" width="100%">
    <tr>
        <td>
        <p class="formBtnBoard">
        <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn"  />
            <asp:GridView width="100%" runat="server" ID="gvDSUploadHistory" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvDSUploadHistoryResource" 
            CellPadding="4" CssClass="list" GridLines="Horizontal" >
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
                    <asp:BoundField DataField="UploadDate" DataFormatString="{0:d}" ShowHeader="true" HeaderText="Create Date" >
                        <ItemStyle HorizontalAlign="Center" />
                    </asp:BoundField>
                    <asp:TemplateField ShowHeader="true" HeaderText="Action" meta:resourcekey="TemplateFieldResource8" ItemStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0 %>' OnClick="lbtnDownload_Click"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnConfirm" runat="server" Text="[Confirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals("OWNER_CONFIRMED") && ((int)DataBinder.Eval(Container.DataItem, "Errors")) == 0 %>' OnClick="lbtnConfirm_Click" OnClientClick="return ButtonWarning('Confirm')"></asp:LinkButton>
                            <asp:LinkButton ID="lbtnUnconfirm" runat="server" Text="[Unconfirm]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# (DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals("ETL_CONFIRMED") || DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals("ETL_FAILED")) %>' OnClick="lbtnUnconfirm_Click" OnClientClick="return ButtonWarning('UnConfirm')"></asp:LinkButton>                            
                            <asp:LinkButton ID="lbtnWithDrawTable" runat="server" Text="[WithDraw Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "ProcessStatus").Equals("ETL_SUCCESS") && ((int)DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.TheDataSource.WithDrawTables")) == 1 && ((int)DataBinder.Eval(Container.DataItem, "IsWithdraw")) == 0 %>' OnClick="lbtnWithDrawTable_Click" OnClientClick="return ButtonWarning('WithDraw')"></asp:LinkButton>
                            <%# ((int)DataBinder.Eval(Container.DataItem, "IsWithdraw")) == 1 ? "WithDrawed" : "" %>
                            <asp:LinkButton ID="LinkDeleteHistory" runat="server" Text="[Delete Raw Data]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsWithdraw")) == 1 && ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0 %>' OnClick="lbtnDeleteHistory_Click" OnClientClick="return ButtonWarning('Delete')"></asp:LinkButton>
                            <%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 1 ? "RawDeleted" : "" %>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
            </p>
        </td>
    </tr>  
</table>