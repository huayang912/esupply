<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Cube_CubeProcess_New" %>
<h2>Cube Process</h2>
<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" CssClass="btn" />    
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
            <td class="txtForm">Cube Name:</td>
            <td><asp:Label ID="lblCubeName" runat="server"/></td>
        </tr>        
        <tr>
            <td class="txtForm">Process Server Address:</td>
            <td><asp:Label ID="lblProcessServerAddr" runat="server"/></td>
            <td class="txtForm">Process Database Name:</td>
            <td><asp:Label ID="lblProcessDatabaseName" runat="server"/></td>
        </tr>
        <tr>
            <td valign="top" class="txtForm">Cube Process Description:</td>
            <td colspan="3">
                &nbsp;<asp:TextBox ID="txtProcessDescription" runat="server"  Columns="100" Rows="5" TextMode="MultiLine"></asp:TextBox>
            </td>
        </tr>
    </table>
</div>

<b>Process Parameters:</b>

<asp:GridView runat="server" ID="gvParameterList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="TheParameter" CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvParameterList_RowDataBound">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="#">
            <ItemTemplate>
                <asp:Label ID="lblParameterId" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheParameter.Id")%>'/>                
            </ItemTemplate>
        </asp:TemplateField>      
        
        <asp:TemplateField HeaderText="Parameter Name">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheParameter.Name")%>
            </ItemTemplate>
        </asp:TemplateField>            
        <asp:TemplateField HeaderText="Parameter Value">
            <ItemTemplate>
                <asp:TextBox ID="txtParameterValue" runat="server"  Width="200" />
            </ItemTemplate>
        </asp:TemplateField>              
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>