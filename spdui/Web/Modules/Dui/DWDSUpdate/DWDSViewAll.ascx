<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DWDSViewAll.ascx.cs" Inherits="Modules_Dui_DWDSUpdate_DWDSViewAll" %>
<!-- Modified By Vincent On 2006-9-4 -->
<h2>Validated Data List for Update:</h2>
<p class="formBtnBoard1">
    <asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn"/>
</p>
<table border="0" width="100%">
    <tr>
        <td>
            <asp:GridView ID="gvDWDSViewAll"  meta:resourcekey="gvDWDSViewAll" runat="server" 
                AutoGenerateColumns="True" allowpaging="true" PageSize="20" OnPageIndexChanging="gvDWDSViewAll_PageIndexChanging"
                CellPadding="4" CssClass="list" GridLines="Horizontal">
                <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
                <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
        </td>
    </tr>  
</table>
