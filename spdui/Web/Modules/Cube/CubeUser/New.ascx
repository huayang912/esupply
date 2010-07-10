<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Cube_CubeUser_New" %>
<%@ Register Src="../../Distribution/DistributionUser/SelectUser.ascx" TagName="SelectUser" TagPrefix="uc1" %>
<asp:Panel ID="pnlMain" runat="server">
<h2>Cube User Maintanence</h2>
<!-- Modified By Vincent On 2006-9-4 -->

<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="bgForm">
<table border="0">
    <tr>
        <td class="txtForm">Distribution User:</td>
        <td><asp:TextBox ID="txtDistributionUserName" runat="server" Width="200" ReadOnly="true"/>    
        </td>
        <td>
            <asp:Button ID="btnSelect" runat="server" Text="Select User" CssClass="btn1" OnClick="btnSelectUser_Click" />
        </td>
    </tr>
	<tr>
        <td class="txtForm">Report User Name:</td>
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
</table></div>
</asp:Panel>
<uc1:SelectUser ID="SelectUser1" runat="server" Visible="False" />