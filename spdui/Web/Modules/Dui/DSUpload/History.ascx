<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Dui_DSUpload_History" %>
<asp:Panel ID="pnlMain" runat="server">
<script language="vbscript">
function SubjectInput(DefaultValue)
    Dim ReturnVal
    ReturnVal = inputbox("Please Enter Subject","Subject",DefaultValue)
    If ReturnVal = "" then
        SubjectInput = false
    else
        document.form1.txtSubject.value = ReturnVal
        SubjectInput = true
    end if 
end function
</script>
<input type="hidden" name="txtSubject" id="txtSubject" value="aaa">
<table border="0" width="100%">
    <tr>
        <td>
        <h2>Data Preparation - Data Upload History for "<asp:Label ID="txtDataSourceName" runat="server" Font-Size=Medium></asp:Label>"</h2>
        <p class="formBtnBoard">
            <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn"/></p>
            <asp:GridView width="100%" runat="server" ID="gvDSUploadHistory" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvDSUploadHistoryResource" 
            CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvDSUploadHistory_RowDataBound" >
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
                            <asp:LinkButton ID="lbtnDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0 %>' CommandName="Select" OnClick="lbtnDownload_Click"></asp:LinkButton>
                            <%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 1 ? "RawDeleted" : "" %>
                            <asp:LinkButton ID="lbtnUpdate" runat="server" Text="[Update Subject]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnUpdate_Click"/>
                        </ItemTemplate>
                        <ItemStyle HorizontalAlign="Left" />
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </td>
    </tr>
</table>
</asp:Panel>