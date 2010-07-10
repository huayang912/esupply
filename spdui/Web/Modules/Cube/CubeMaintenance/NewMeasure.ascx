<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewMeasure.ascx.cs" Inherits="Modules_Cube_CubeMeasure_New" %>
<h2>Dimension Maintenance</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />    
</p>
<asp:HiddenField ID="txtCubeId" runat="server" />

<div class="BGform">
<table border="0">
<tr>
    <td class="txtform">Measure Name:</td>
    <td><asp:TextBox ID="txtMeasureName" runat="server" Width="200"></asp:TextBox></td>
    <td class="txtform">Measure Display Name:</td>
    <td><asp:TextBox ID="txtDisplayName" runat="server" Width="200"></asp:TextBox></td>
</tr>
</table>
</div>