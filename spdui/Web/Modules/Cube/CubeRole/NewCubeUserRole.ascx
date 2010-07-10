<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewCubeUserRole.ascx.cs" Inherits="Modules_Cube_CubeUserRole_New" %>
<h2>New Cube User</h2>

<script type="text/javascript">
function ChooseAll() 
{
    var flagCheck = document.all("ckbUserAll").checked;
    var inputs = document.all.tags("INPUT");
    
    for (var i=0; i < inputs.length; i++) 
    { 
        if (inputs[i].type == "checkbox" && inputs[i].id.lastIndexOf("cbSelect") > 0 ) 
        { 
            inputs[i].checked = flagCheck; 
        } 
    }
}
</script>

<div class="BGform">
<table border="0">
    <tr>
        <td class="txtForm" style="height:25">        
           Name:&nbsp;
           <asp:TextBox ID="txtUserName" runat="server"/>&nbsp;&nbsp;&nbsp;
           Description:&nbsp;
           <asp:TextBox ID="txtUserDescription" runat="server"/>&nbsp;&nbsp;&nbsp;           
           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn1" />
        </td>
    </tr>
</table>
</div>
<p class="formBtnBoard">
    <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" CssClass="btn" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>

<asp:gridview id="gvUserList" runat="server" DataKeyNames="Id" AutoGenerateColumns="false" CellPadding="4" CssClass="list" GridLines="Horizontal" >
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />            
    <Columns>
        <asp:TemplateField HeaderText="User">
            <headertemplate>
                <input ID="ckbUserAll" type="checkbox" onclick="ChooseAll();">
            </headertemplate>
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Id" HeaderText="Id"></asp:BoundField>
        <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
        <asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
        <asp:TemplateField HeaderText="EMAIL">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheDistributionUser.Email")%>
            </ItemTemplate>
        </asp:TemplateField>  
        <asp:TemplateField HeaderText="Domain Account">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheDistributionUser.DomainAccount")%>
            </ItemTemplate>
        </asp:TemplateField>     
    </Columns>
</asp:gridview>
<asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>