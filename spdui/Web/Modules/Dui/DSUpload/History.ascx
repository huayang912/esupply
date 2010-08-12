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
    <h2>
        Data Preparation - Data Upload History for "<asp:Label ID="txtDataSourceName" runat="server"
            Font-Size="Medium"></asp:Label>"</h2>
    <div class="bgForm">
        <table border="0">
            <tr>
                <td class="txtForm" style="height: 25">
                    Category:&nbsp;
                    <asp:DropDownList ID="ddlDSCategory" runat="server" />&nbsp;&nbsp;&nbsp;
                    Subject:&nbsp;
                    <asp:TextBox ID="txtName" runat="server" />&nbsp;&nbsp;&nbsp;
                    File Name:&nbsp;
                    <asp:TextBox ID="txtFileName" runat="server" />&nbsp;&nbsp;&nbsp;
                    Create By:&nbsp;
                    <asp:TextBox ID="txtCreateBy" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                        meta:resourcekey="btnSearchResource1" CssClass="btn1" />
                    <asp:Button ID="Button1" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn1" />
                </td>
            </tr>
        </table>
    </div>
    <asp:GridView Width="100%" runat="server" ID="gvDSUploadHistory" AutoGenerateColumns="False"
        DataKeyNames="Id" meta:resourcekey="gvDSUploadHistoryResource" AllowPaging="True" PageSize="100"
        OnPageIndexChanging="gvDSUploadHistory_PageIndexChanging" CellPadding="4"
        CssClass="list" GridLines="Horizontal" OnRowDataBound="gvDSUploadHistory_RowDataBound">
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
            <asp:TemplateField ShowHeader="true" HeaderText="Action" meta:resourcekey="TemplateFieldResource8"
                ItemStyle-Wrap="false">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDownload" runat="server" Text="[Download]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        Visible='<%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 0 %>'
                        CommandName="Select" OnClick="lbtnDownload_Click"></asp:LinkButton>
                    <%# ((int)DataBinder.Eval(Container.DataItem, "IsHitoryDelete")) == 1 ? "RawDeleted" : "" %>
                    <asp:LinkButton ID="lbtnUpdate" runat="server" Text="[Update Subject]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        CommandName="Select" OnClick="lbtnUpdate_Click" />
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left" />
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Panel>
