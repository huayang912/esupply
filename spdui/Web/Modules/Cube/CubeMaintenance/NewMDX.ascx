<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewMDX.ascx.cs" Inherits="Modules_Cube_CubeMaintenance_NewMDX" %>
<!-- Modified By Vincent On 2006-9-2 Begin -->
<h2>Warm MDX Maintenance</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />    
</p>
<asp:HiddenField ID="txtCubeId" runat="server" />
<asp:HiddenField ID="txtCubeMDXId" runat="server"/>

<div class="BGform">
<table border="0">
<tr>
    <td colspan="4"><asp:Label ID="lblMessage" runat="server" CssClass="error" Visible="False"/></td>
</tr>
<tr>
    <td class="txtform">Sequence No:</td>
    <td><asp:TextBox ID="txtSequenceNo" runat="server" Width="200"></asp:TextBox></td>
    <td class="txtform">Description:</td>
    <td><asp:TextBox ID="txtDescription" runat="server" Width="200"></asp:TextBox></td>
</tr>
<tr>
    <td class="txtform">MDX Statement:</td>
    <td><asp:TextBox ID="txtMDXStatement" runat="server" Width="400"></asp:TextBox></td>
</tr>
</table>
</div>

<!-- Modified By Vincent On 2006-9-2 End -->