<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Cube_CubeRole_Edit" %>
<%@ Register Src="NewCubeUserRole.ascx" TagName="NewCubeUser" TagPrefix="uc1" %>
<%@ Register Src="NewRoleDimensionMember.ascx" TagName="NewRoleDimensionMember" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
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
end function
</script>

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
<asp:Label ID="lblMessage" runat="server" CssClass="error"></asp:Label>
<h2>Cube Role Maintanence</h2>
<b>Basic Information:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click"
        CssClass="btn" />
    <asp:Button ID="btnUpdateCube" runat="server" Text="Update Cube" OnClick="btnUpdateCube_Click"
        CssClass="btn" />
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
        CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
</p>
<div class="bgForm">
    <table border="0">
        <tr>
            <td class="txtForm">Cube Description:</td>
            <td><asp:TextBox ID="txtCubeName" runat="server" Width="200" ReadOnly="true"/></td>
        </tr>
        <tr>
            <td class="txtForm">Role Name:</td>
            <td><asp:TextBox ID="txtName" runat="server" Width="200"/></td>
            <td class="txtForm">&nbsp;Description:</td>
            <td><asp:TextBox ID="txtDescription" runat="server" Width="200"/></td>
        </tr>                             
    </table>
</div>  
<b>Set Dimension Visual Total:</b>
<p class="formBtnBoard">&nbsp;&nbsp;</p>
<asp:GridView runat="server" ID="gvSetDimensionVisualTotal" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvSetDimensionVisualTotal_RowDataBound">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="#" HeaderStyle-Width="30"/>        
        <asp:BoundField DataField="SetDimensionName" HeaderText="Set Dimension Name"  HeaderStyle-Width="300"/>        
        <asp:TemplateField HeaderText="Visual Total" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center" HeaderStyle-Width="80">
            <ItemTemplate>                
                <asp:CheckBox ID="chkVisualTotal" Text="" Checked="true" runat="server"/>
            </ItemTemplate>
        </asp:TemplateField>                   
        <asp:TemplateField HeaderText="" HeaderStyle-HorizontalAlign="center" ItemStyle-HorizontalAlign="center">
            <ItemTemplate>                
                &nbsp;
            </ItemTemplate>
        </asp:TemplateField>                   
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
<br/>
<b>Dimension Data:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnAssignMember" runat="server" Text="Assign Member" Width="100px" OnClick="btnAssignMember_Click" CssClass="btn" />   
</p>
<asp:GridView runat="server" ID="gvDimensionList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal"  OnRowDataBound="gvDimensionList_RowDataBound">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:BoundField DataField="Id" HeaderText="#" />
        <asp:BoundField DataField="DimensionName" HeaderText="Dimension Name" />
        <asp:BoundField DataField="AttributeName" HeaderText="Attribute Name" /> 
        <asp:BoundField HeaderText="Assigned Members" />                    
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>

<b>Cube Users:</b>
<p class="formBtnBoard">
    <asp:Button ID="btnAddUser" runat="server" Text="Add User" Width="100px" OnClick="btnAddUser_Click" CssClass="btn" />
    <asp:Button ID="btnDeleteUser" runat="server" Text="Delete" OnClick="btnDeleteUser_Click" CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
</p>

<asp:GridView runat="server" id="gvUserList" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField>
            <headertemplate>
                <input id="ckbUserAll" type="checkbox" onclick="ChooseAll();">
            </headertemplate>
            <ItemTemplate>
                <asp:CheckBox id="cbSelect" runat="server" />
            </ItemTemplate>
            <HeaderStyle Width="3%" />
        </asp:TemplateField>
        <asp:BoundField DataField="Name" HeaderText="Name" />
        <asp:BoundField DataField="Description" HeaderText="Description" />
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
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
</asp:Panel>
<uc1:NewCubeUser ID="NewCubeUser1" runat="server" Visible="False" />
<uc2:NewRoleDimensionMember ID="NewRoleDimensionMember1" runat="server" Visible="False" />