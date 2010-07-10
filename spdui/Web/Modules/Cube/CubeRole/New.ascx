<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Cube_CubeRole_New" %>
<h2>Cube Role Maintanence</h2>

<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="bgForm">
    <table border="0">
        <tr>
            <td colspan="4">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="txtForm">Cube Description:</td>
            <td colspan="3">
                <asp:RadioButtonList RepeatColumns="4" ID="cbCube" runat="server" DataTextField="Description" DataValueField="Id" />
            </td>
        </tr>
        
	    <tr>
            <td class="txtForm">Role Name:</td>
            <td><asp:TextBox ID="txtName" runat="server" Width="200"/></td>
            <td class="txtForm">&nbsp;Description:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" Width="200"/></td>
        </tr>                 
    </table>
</div>