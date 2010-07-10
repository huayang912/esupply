<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Cube_CubeDistributionJob_New" %>

<%@ Register Src="NewJobUser.ascx" TagName="NewJobUser" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">

<script language="vbscript" type="text/vbscript">
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
<script type="text/javascript" language="javascript">
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
<h2>Cube Distribution</h2>
<b>Basic Information:</b>
<p class="formBtnBoard">    
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnRestart" runat="server" Text="Restart" OnClick="btnRestart_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click" CssClass="btn"></asp:Button>
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />    
</p>

<div class="bgForm">
    <table border="0">
        <tr>
            <td colspan="2" align="left">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="txtForm">Cube Description:</td>
            <td><asp:Label ID="lblCubeDescription" runat="server"/></td>
            <td class="txtForm">Status:</td>
            <td><asp:Label ID="lblStatus" runat="server"/></td>
        </tr>
        <tr>
            <td class="txtForm">Start Time:</td>
            <td><asp:TextBox ID="txtStartTime" runat="server" Width="200"/></td>
            <td class="txtForm">End Time:</td>
            <td><asp:Label ID="lblEndTime" runat="server" Width="200"/></td>
        </tr>  
        <tr>
            <td class="txtForm">Offline Data From:</td>
            <td><asp:TextBox ID="txtDataBeginDate" runat="server" Width="200"/></td>
            <td class="txtForm">Offline Data To:</td>
            <td><asp:TextBox ID="txtDataEndDate" runat="server" Width="200"/></td>
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
                Need Uploading to Portal:</td>
            <td>
                <asp:RadioButton ID="rdoNeedUploadYes" runat="server" Checked="true" GroupName="rdoNeedUpload"
                    Text="YES" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoNeedUploadNo" runat="server" GroupName="rdoNeedUpload" Text="NO" />
           </td>                
        </tr>
        <tr>
            <td class="txtForm">
                Append Date To File Name:</td>
            <td>
                <asp:RadioButton ID="rdoAppendDateYes" runat="server" Checked="true" GroupName="rdoAppendDate"
                    Text="YES" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoAppendDateNo" runat="server" GroupName="rdoAppendDate" Text="NO" />
            </td>
            <td class="txtForm">
                Append User Name To File Name:</td>
            <td>
                <asp:RadioButton ID="rdoAppendUserNameYes" runat="server" Checked="true" GroupName="rdoAppendUserName"
                    Text="YES" />
                &nbsp;&nbsp;
                <asp:RadioButton ID="rdoAppendUserNameNo" runat="server" GroupName="rdoAppendUserName" Text="NO" /></td>
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
                Uploading Folder:</td>
            <td><asp:TextBox ID="txtUploadFolder" runat="server" Width="200"/></td>  
           
        </tr>
        <tr>
            <td class="txtForm">Notification Subject:</td>
            <td colspan="3"><asp:TextBox ID="txtSubject" runat="server" Width="400"/></td>
        </tr>
        <tr>
            <td class="txtForm">Notification Content:</td>
            <td colspan="3"><asp:TextBox ID="txtBody" runat="server" Columns="100" Rows="15" TextMode="MultiLine" Wrap="true"/></td>
        </tr>
    </table>
</div>

<br/>
<b>Distribute Measures:</b>
<div class="bgForm">
<asp:CheckBoxList ID="cbMeasuresSelect" runat="server" DataTextField="DisplayName" DataValueField="Name" RepeatColumns="8" RepeatDirection="Horizontal" />
</div>

<br/>
<b>User Definitions:</b>
<p class="formBtnBoard">
<asp:Button ID="btnAddUser" runat="server" Text="Add User" Width="100px" OnClick="btnAddUser_Click" CssClass="btn" />
<asp:Button ID="btnDeleteUser" runat="server" Text="Delete" OnClick="btnDeleteUser_Click" CssClass="btn"  OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvUserList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField>
            <headertemplate>
                <input id="ckbUserAll" type="checkbox" onclick="ChooseAll('User');">
            </headertemplate>
            <ItemTemplate>
                <asp:CheckBox ID="ckbUserItem" runat="server" />
            </ItemTemplate>
            <HeaderStyle Width="3%" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheCubeUser.Name")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheCubeUser.Description")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Email">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Email")%>
            </ItemTemplate>
        </asp:TemplateField>        
        <asp:TemplateField HeaderText="Domain Account">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "PortalUserName")%>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>

</asp:Panel>
<uc2:NewJobUser id="NewJobUser1" runat="server" Visible="False"/>