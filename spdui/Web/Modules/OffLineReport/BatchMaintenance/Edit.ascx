<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_OffLineReport_BatchMaintenance_Edit" %>
<%@ Register Src="NewBatchReport.ascx" TagName="NewBatchReport" TagPrefix="uc1" %>
<%@ Register Src="NewBatchUser.ascx" TagName="NewBatchUser" TagPrefix="uc2" %>
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
    end select
end function
</script>
<script type="text/javascript">
function ChooseAll(varControlName) 
{
    var flagCheck = document.all("ckb"+varControlName+"All").checked;
    var inputs = document.all.tags("INPUT");
    
    for (var i=0; i < inputs.length; i++) 
    { 
        if (inputs[i].type == "checkbox" && inputs[i].id.lastIndexOf("ckb"+varControlName+"Item") > 0 ) 
        { 
            inputs[i].checked = flagCheck; 
        } 
    }
}
</script>
<h2>Report Batch Maintanence</h2>
<b>Basic Information:</b>
<p class="formBtnBoard"><asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn" />
            <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" /></p>
<div class=bgForm>
<table border="0">
	<tr>
        <td class="txtForm">Report Batch Name:</td>
        <td><asp:TextBox ID="txtName" runat="server"/></td>
        <td class="txtForm">Type:</td>
        <td><asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td class="txtForm">Description:</td>
        <td colspan="3"><asp:TextBox ID="txtDescription" runat="server" Width="400"></asp:TextBox></td>
    </tr>
    <tr>
        <td valign="top" class="txtForm" valign="top">SQL Before Job Run:</td>
        <td colspan="3">
            &nbsp;<asp:TextBox ID="txtPreRunSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
    <td valign="top" class="txtForm" valign="top">SQL After Job Run:</td>
    <td colspan="3">
        &nbsp;<asp:TextBox ID="txtPostRunSQL" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"/>
    </td>
    </tr>
    <tr>
        <td class="txtForm">Notification Subject:</td>
        <td colspan="3"><asp:TextBox ID="txtSubject" runat="server" Width="300"/></td>
    </tr>
    <tr>
        <td class="txtForm">Notification Content:</td>
        <td colspan="3"><asp:TextBox ID="txtBody" runat="server" Columns="100" Rows="15" TextMode="MultiLine"/></td>
    </tr>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
</div>
<b>Report Definitions:</b>
<p class="formBtnBoard"><asp:Button ID="btnAddReport" runat="server" Text="Add Report" Width="100px" OnClick="btnAddReport_Click" CssClass="btn" />
<asp:Button ID="btnDeleteReport" runat="server" Text="Delete" OnClick="btnDeleteReport_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>

    <asp:GridView runat="server" ID="gvReportList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheReport.Name") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Description">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheReport.Description") %>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
<br>
<b>User Authorization:</b>
<p class="formBtnBoard">
<asp:Button ID="btnAddUser" runat="server" Text="Add User" Width="100px" OnClick="btnAddUser_Click" CssClass="btn" />
<asp:Button ID="btnDeleteUser" runat="server" Text="Delete" OnClick="btnDeleteUser_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvUserList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField>
            <headertemplate>
                <input ID="ckbUserAll" type="checkbox" onclick="ChooseAll('User');">
            </headertemplate>
            <ItemTemplate>
                <asp:CheckBox ID="ckbUserItem" runat="server" />
            </ItemTemplate>
            <HeaderStyle Width="3%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheUser.UserName") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheUser.Email")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
</asp:Panel>
<uc1:NewBatchReport id="NewBatchReport1" runat="server" Visible="False"/>
<uc2:NewBatchUser id="NewBatchUser1" runat="server" Visible="False"/>
