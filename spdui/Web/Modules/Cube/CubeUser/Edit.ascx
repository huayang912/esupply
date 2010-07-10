<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Cube_CubeUser_Edit" %>
<asp:Panel ID="pnlMain" runat="server">
<!-- Modified By Vincent On 2006-9-4 -->

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
end functione
    </script>

    <h2>
        Cube User Maintanence</h2>
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
                <td class="txtForm">Distribution User:</td>
                <td><asp:TextBox ID="txtDistributionUserName" runat="server" Width="200" ReadOnly="true"/>    
                </td>                
            </tr>
	        <tr>
                <td class="txtForm">Cube User Name:</td>
                <td><asp:TextBox ID="txtName" runat="server" Width="200"/></td>
                <td class="txtForm">&nbsp;Description:</td>
                <td><asp:TextBox ID="txtDescription" runat="server" Width="200"/></td>
            </tr>        
            <tr>            
                <td class="txtForm">Cube Site:</td>
                <td><asp:TextBox ID="txtCubeSite" runat="server" Width="200"/></td>
                <td class="txtForm">&nbsp;Cube Library:</td>
                <td><asp:TextBox ID="txtCubeLibrary" runat="server" Width="200"/></td>
            </tr>
            <tr>
                <td class="txtForm">Cube Read User List:</td>
                <td><asp:TextBox ID="txtCubeReadUsers" runat="server" Width="200"/></td>
                <td class="txtForm">&nbsp;Cube Full Control User List:</td>
                <td><asp:TextBox ID="txtCubeFullControlUsers" runat="server" Width="200"/></td>
            </tr>            
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
    </div>  
</asp:Panel>
