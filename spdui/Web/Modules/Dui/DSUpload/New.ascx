<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Dui_DSUpload_New" %>
<table border="0">
<asp:Repeater ID="errorMessage" runat="server">
    <ItemTemplate>
        <tr>
            <td style="color:Red">
               <%# DataBinder.GetDataItem(Container).ToString()%>
            </td>
         </tr>
    </ItemTemplate>
</asp:Repeater>
</table>
<table width=100% cellpadding=0 cellspacing=0>
<tr>
<td width=1% nowrap><h2>Data Preparation - Data Upload:</h2></td><td><h2><asp:Label ID="txtName" runat="server" Font-Size=Medium/></h2></td>
<td valign=bottom><p class="formBtnBoard"><asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn" /></p>
</td></tr>
</table>
<asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
<div class="BGform">
<table border=0>
	<tr>
        <td class="txtForm">Category:</td>
        <td>
            <asp:DropDownList ID="txtCategory" DataTextField="Name" DataValueField="Id" runat="server">              
            </asp:DropDownList>
        </td>
        <td class="txtForm">File:</td>
        <td>
            <asp:FileUpload ID="txtFile" runat="server"/>
        </td>
        <td class="txtForm">Subject:</td>
        <td>
            <asp:TextBox ID="txtSubject" runat="server" Width="200"/>
        </td>
        <td align="left">
            <table width="90%">
                <tr>
                    <td align="right">
                         <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="btnUpload_Click" CssClass="btn1"/>
                    </td>
                </tr>
            </table>           
        </td>
    </tr>
</table>
    <!-- Modified By Vincent On 2006-9-1 Begin Rows:80-->
    <!--<ItemStyle HorizontalAlign="Center" />-->
    <!-- Modified By Vincent On 2006-9-1 End --> 
</div>
<b>Data History List for The Data Source:</b>
<asp:GridView runat="server" ID="gvDSUploadHistory" AutoGenerateColumns="False" DataKeyNames="Id" meta:resourcekey="gvDSUploadHistoryResource" CellPadding="4" CssClass="list" GridLines="Horizontal" >
    <HeaderStyle CssClass="listheader" />                
    <Columns>                    
        <asp:TemplateField HeaderText="Category" meta:resourcekey="TemplateFieldResource3">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheDataSourceCategory.Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Subject" />
        <asp:BoundField DataField="UploadFileOriginName" HeaderText="File Name" />
        <asp:BoundField DataField="ProcessStatus" HeaderText="Status" />
        <asp:BoundField DataField="RecordRows" HeaderText="Rows" />
        <asp:TemplateField HeaderText="Create By" meta:resourcekey="TemplateFieldResource3">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "UploadBy.UserName")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="UploadDate" DataFormatString="{0:d}" HeaderText="Create Date" >
            <ItemStyle HorizontalAlign="Center" />
        </asp:BoundField>
        <asp:TemplateField HeaderText="Action" meta:resourcekey="TemplateFieldResource8">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnDelete" runat="server" Text="Delete" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" meta:resourcekey="lbtnDeleteResource" Visible='<%# (DataBinder.Eval(Container.DataItem, "ProcessStatus") == "")%>' OnClick="lbtnDelete_Click" ></asp:LinkButton>
                <asp:LinkButton ID="lbtnConfirm" runat="server" Text="Confirm" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" meta:resourcekey="lbtnConfirmResource" Visible='<%# DataBinder.Eval(Container.DataItem, "ProcessStatus") == "" && DataBinder.Eval(Container.DataItem, "Errors").Equals(0)%>' OnClick="lbtnConfirm_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnUnconfirm" runat="server" Text="Unconfirm" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" Visible='<%#DataBinder.Eval(Container.DataItem, "ProcessStatus").ToString().Equals(Dndp.Persistence.Entity.Dui.DataSourceUpload.DataSourceUpload_ProcessStatus_OWNER_CONFIRMED) %>' OnClick="lbtnUnconfirm_Click"></asp:LinkButton>
                <asp:LinkButton ID="LinkDownload" runat="server" Text="Download" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" meta:resourcekey="lbtnUnconfirmResource" OnClick="lbtnDownload_Click"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
