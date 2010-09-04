<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_OffLineReport_JobExecution_Edit" %>
<%@ Register Src="NewJobReport.ascx" TagName="NewJobReport" TagPrefix="uc1" %>
<%@ Register Src="NewJobUser.ascx" TagName="NewJobUser" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
<!-- Modified By Vincent On 2006-9-4 -->

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
<h2>Report Distribution Job Definition</h2>
<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn" />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn" />
    <asp:Button ID="btnRestart" runat="server" Text="Restart" OnClick="btnRestart_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class=bgForm>
<table border="0">
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
	<tr>
        <td class="txtForm">Batch Name:</td>
        <td><asp:Label ID="lblBatchName" runat="server"/></td>
        <td class="txtForm">Status:</td>
        <td><asp:Label ID="lblStatus" runat="server"/></td>
    </tr>
    <tr>
        <td class="txtForm">Start Time:</td>
        <td><asp:TextBox ID="txtStartTime" runat="server" Width="200"/></td>
        <td class="txtForm">End Time:</td>
        <td><asp:Label ID="lblEndTime" runat="server"/></td>
    </tr>
    <tr>
        <td class="txtForm">
            Send Mail:</td>
        <td>
            <asp:RadioButton ID="rdoSendMailYes" runat="server" Checked="true" GroupName="rdoSendMail"
                Text="YES" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rdoSendMailNo" runat="server" GroupName="rdoSendMail" Text="NO" />
        </td>
        <td class="txtForm">
            Append Date To File Name:</td>
        <td>
            <asp:RadioButton ID="rdoAppendDateYes" runat="server" Checked="true" GroupName="rdoAppendDate"
                Text="YES" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rdoAppendDateNo" runat="server" GroupName="rdoAppendDate" Text="NO" />
       </td>                
    </tr>
    <tr>
        <td class="txtForm">Report Date:</td>
        <td><asp:TextBox ID="txtReportDate" runat="server" Width="200"/></td>
        <td class="txtForm">
            Append User Name To File Name:</td>
        <td>
            <asp:RadioButton ID="rdoAppendUserNameYes" runat="server" Checked="true" GroupName="rdoAppendUserName"
                Text="YES" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rdoAppendUserNameNo" runat="server" GroupName="rdoAppendUserName" Text="NO" />
       </td>
       
        
    </tr>
    <tr>
        <td class="txtForm">
            Need Uploading to Portal:</td>
        <td>
            <asp:RadioButton ID="rdoNeedUploadYes" runat="server" Checked="true" GroupName="rdoNeedUpload"
                Text="YES" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rdoNeedUploadNo" runat="server" GroupName="rdoNeedUpload" Text="NO" />
       </td>            
        <td class="txtForm">
            Uploading Folder:</td>
        <td><asp:TextBox ID="txtUploadFolder" runat="server" Width="200"/></td>  
       
    </tr>
    <tr>
        <td class="txtForm">
            Create Sub Folder For User:</td>
        <td>
            <asp:RadioButton ID="rdoCreateSubFolderYes" runat="server" Checked="true" GroupName="rdoCreateSubFolder"
                Text="YES" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rdoCreateSubFolderNo" runat="server" GroupName="rdoCreateSubFolder" Text="NO" />
       </td>
        <td class="txtForm">
            Prepare Report Data:</td>
        <td>
            <asp:RadioButton ID="rdoRunPreSQLYes" runat="server" Checked="true" GroupName="rdoRunPreSQL"
                Text="YES" />
            &nbsp;&nbsp;
            <asp:RadioButton ID="rdoRunPreSQLNo" runat="server" GroupName="rdoRunPreSQL" Text="NO"/></td> 
               
    </tr>
    <tr>
        <td class="txtForm">Notification Subject:</td>
        <td colspan="3"><asp:TextBox ID="txtSubject" runat="server" Width="400"/></td>
    </tr>
    <tr>
        <td class="txtForm">Notification Content:</td>
        <td colspan="3"><asp:TextBox ID="txtBody" runat="server" Columns="100" Rows="15" TextMode="MultiLine" Wrap="true"/></td>
    </tr>
    <tr>
        <td class="txtForm">Report Description:</td>
        <td colspan="3"><asp:TextBox ID="txtRptDesc" runat="server" Columns="100" Rows="15" TextMode="MultiLine" Wrap="true"/></td>
    </tr>
    <tr>
        <td class="txtForm">Create Date:</td>
        <td><asp:Label ID="lblCreateDate" runat="server"/></td>
        <td class="txtForm">Create User:</td>
        <td><asp:Label ID="lblCreateUser" runat="server"/></td>
    </tr>
    <tr>
        <td class="txtForm">Last Update Date:</td>
        <td><asp:Label ID="lblUpdateDate" runat="server"/></td>
        <td class="txtForm">Last Update User:</td>
        <td><asp:Label ID="lblUpdateUser" runat="server"/></td>
    </tr>
</table>

</div>

<b>Report Definitions:</b>
<p class="formBtnBoard">
<asp:Button ID="btnAddReport" runat="server" Text="Update Report" Width="100px" OnClick="btnAddReport_Click" CssClass="btn" />
<asp:Button ID="btnDeleteReport" runat="server" Text="Delete" OnClick="btnDeleteReport_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvReportList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField>
            <headertemplate>
                <input ID="ckbReportAll" type="checkbox" onclick="ChooseAll('Report');">
            </headertemplate>
            <ItemTemplate>
                <asp:CheckBox ID="ckbReportItem" runat="server"/>
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
<b>User Definitions:</b>
<p class="formBtnBoard">
<asp:Button ID="btnAddUser" runat="server" Text="Update User" Width="100px" OnClick="btnAddUser_Click" CssClass="btn" />
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
                <%# DataBinder.Eval(Container.DataItem, "TheUser.Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheUser.Description")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheUser.TheUser.Email")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Domain Account">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheUser.TheUser.DomainAccount")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create Report">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "ReportCreateStatus")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Send Email">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "ReportEmailStatus")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Upload Portal">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "ReportPortalStatus")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>

</asp:Panel>
<uc1:NewJobReport id="NewJobReport1" runat="server" Visible="False"/>
<uc2:NewJobUser id="NewJobUser1" runat="server" Visible="False"/>