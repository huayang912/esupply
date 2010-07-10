<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewCategory.ascx.cs" Inherits="Modules_Dui_DSMaintenance_NewCategory" %>
<!-- Modified By Vincent On 2006-9-2 Begin -->
<h2>Category</h2>
<p class="formBtnBoard">
    <asp:Button ID="btnSubmit" runat="server" Text="Save & Back" OnClick="btnSubmit_Click" CssClass="btn" />
    <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />    
</p>
<asp:HiddenField ID="txtDsId" runat="server" />

<div class="BGform">
<table border="0">
<tr>
    <td class="txtform" width="100">Category Name:</td>
    <td><asp:TextBox ID="txtCategoryName" runat="server" Width="100"></asp:TextBox></td>
</tr>
<tr>
    <td class="txtform">Category Description:</td>
    <td><asp:TextBox ID="txtCategoryDescription" runat="server" Width="200"></asp:TextBox></td>
</tr>
</table>
</div>

<!-- Modified By Vincent On 2006-9-2 End -->