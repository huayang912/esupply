<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Distribution_DistributionUser_Edit" %>

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
    
      <h2>Distribution User Maintanence</h2>
    <b>Basic Information:</b>
    <p class="formBtnBoard">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
            CssClass="btn" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
    
<div class="bgForm">
    <table border="0">
        <tr>
            <td class="txtForm">User Name:</td>
            <td><asp:TextBox ID="txtName" runat="server" Width="200"/></td>
            <td class="txtForm">&nbsp;Description:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" Width="200"/></td>
        </tr>        
        <tr>
            <td class="txtForm">Email:</td>
            <td><asp:TextBox ID="txtEmail" runat="server" Width="200"/></td>
            <td class="txtForm">Domain Account:</td>
            <td><asp:TextBox ID="txtDomainAccount" runat="server" Width="200"/></td>
        </tr>
        
        <tr>
            <td class="txtForm">Is Offline Report User:</td>
            <td><asp:CheckBox ID="cbReportUser" runat="server" /></td>
            <td class="txtForm">Is Online Cube User:</td>
            <td><asp:CheckBox ID="cbOnlineCubeUser" runat="server" /></td>
        </tr>
        
        <tr>
            <td class="txtForm">Is Offline Cube User:</td>
            <td><asp:CheckBox ID="cbOfflineCubeUser" runat="server" /></td>
        </tr>
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
    </table>
</div>