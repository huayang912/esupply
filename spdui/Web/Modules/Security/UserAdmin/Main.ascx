<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Security_UserAdmin_Main" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<script language="vbscript">
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
<asp:Panel ID="pnlMain" runat="server">
<!-- Modified By Vincent On 2006-9-4 -->

<h2>User Admin.</h2>
<br />
<asp:Label ID="lblText" runat="server"  Visible="false"  ForeColor="red" />
 <p class="formBtnBoard">

            <asp:Button ID="btnNew" runat="server" Text="New User" OnClick="btnNew_Click"  CssClass="btn"/>
</p> 
<div class="BGform">
<table border="0" >
    <tr>
        <td class="txtForm">
            <%= GetLocalResourceObject("txtUserNameCaption").ToString()%></td>
         <td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox>  <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"  CssClass="btn1"/>
        </td>
    </tr>
</table>
</div>
<hr size=1><asp:Label ID="lblRecordCount" runat="server" />

 <p class="formBtnBoard">
<asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btn" OnClick="btnDelete_Click" Visible="False" OnClientClick="return ButtonWarning('Delete')"/> 
</p> 

<asp:GridView runat="server" ID="gvUser" AllowSorting="True" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" OnPageIndexChanging="gvUser_PageIndexChanging" OnSelectedIndexChanged="gvUser_SelectedIndexChanged" meta:resourcekey="gvUserResource1" OnSorting="gvUser_Sorting" CellPadding="4" CssClass="list"  GridLines="Horizontal">
    <Columns>
        <asp:TemplateField meta:resourcekey="TemplateFieldResource1">
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" meta:resourcekey="cbSelectResource1" CssClass="radio" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="User Name" SortExpression="UserName">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName") %>' CommandName="Select" meta:resourcekey="lbtnUserNameResource1" ></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email" meta:resourcekey="BoundFieldResource1">
        </asp:BoundField>
        <asp:BoundField DataField="WindowsDomain" HeaderText="Windows Domain" meta:resourcekey="BoundFieldResource2">
        </asp:BoundField>
        <asp:BoundField DataField="WindowsUserName" HeaderText="Windows User Name" meta:resourcekey="BoundFieldResource3">
        </asp:BoundField>
        
    </Columns>
    <HeaderStyle CssClass="listhead" />
    <AlternatingRowStyle CssClass="listA" />
    
</asp:GridView>

</asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />
<uc2:New ID="New1" runat="server" Visible="False" />
