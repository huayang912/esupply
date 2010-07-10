<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewDimension.ascx.cs" Inherits="Modules_Cube_CubeMaintenance_NewDimension" %>
<!-- Modified By Vincent On 2006-9-2 Begin -->
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
    <td class="txtform">Dimension Name:</td>
    <td><asp:TextBox ID="txtDimensionName" runat="server" Width="200"></asp:TextBox></td>
    <td class="txtform">Attribute Name:</td>
    <td><asp:TextBox ID="txtAttributeName" runat="server" Width="200"></asp:TextBox></td>
</tr>
<tr>
    <td class="txtform">Set Dimension Name:</td>
    <td><asp:TextBox ID="txtSetDimensionName" runat="server" Width="200"></asp:TextBox></td>
    <td class="txtform">Set Attribute Name:</td>
    <td><asp:TextBox ID="txtSetAttributeName" runat="server" Width="200"></asp:TextBox></td>
</tr>
<tr>
    <td class="txtform">Related Dimension Name:</td>
    <td><asp:TextBox ID="txtRelatedDimensionName" runat="server" Width="200"></asp:TextBox></td>
    <td class="txtform">Related Attribute Name:</td>
    <td><asp:TextBox ID="txtRelatedAttributeName" runat="server" Width="200"></asp:TextBox></td>
</tr>
<tr>
    <td valign="top" class="txtForm">Parameters:</td>
    <td>
        &nbsp;<asp:DropDownList ID="ddlParameter" runat="server" DataTextField="Text" DataValueField="Value" Style="width:180;border-color:Red;"></asp:DropDownList>
    </td>
    <td><asp:Button ID="btnInsertParameter" runat="server" Text="Insert" CssClass="btn" OnClick="btnInsertParameter_Click"/></td>
    <td></td>
</tr>
<tr>
    <td class="txtform">MDX Formula:</td>
    <td colspan="3">
        &nbsp;<asp:TextBox ID="txtMDXFormula" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
    </td>
</tr>  
<tr>
    <td valign="top" class="txtForm">Parameters:</td>
    <td>
        &nbsp;<asp:DropDownList ID="ddlRelatedParameter" runat="server" DataTextField="Text" DataValueField="Value" Style="width:180;border-color:Red;"></asp:DropDownList>
    </td>
    <td><asp:Button ID="btnInsertRelatedParameter" runat="server" Text="Insert" CssClass="btn" OnClick="btnInsertRelatedParameter_Click"/></td>
    <td></td>
</tr>
<tr>
    <td class="txtform">Related MDX Formula:</td>
    <td colspan="3">
        &nbsp;<asp:TextBox ID="txtRelatedMDXFormula" runat="server"  Columns="100" Rows="15" TextMode="MultiLine"></asp:TextBox>
    </td>
</tr> 
</table>
</div>

<!-- Modified By Vincent On 2006-9-2 End -->