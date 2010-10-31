<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_OffLineReport_ReportUserMaintenance_Main" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
<!-- Modified By Vincent On 2006-9-4 -->

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
<h2>Off Line Report User</h2>
<br />
<asp:Label ID="lblText" runat="server"  Visible="false"  ForeColor="red" />
<div class="BGform">
<table border="0">
    <tr>
        <td class="txtForm" style="height:25">        
           Name:&nbsp;
           <asp:TextBox ID="txtUserName" runat="server"/>&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn1" />
        </td>
    </tr>
</table>
</div>
<p class="formBtnBoard">
    <asp:Button ID="btnNew" runat="server" Text="New User" OnClick="btnNew_Click" CssClass="btn" /> 
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  CssClass="btn" OnClientClick="return ButtonWarning('Delete')"/>
</p>
    <asp:GridView runat="server" ID="gvList" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnPageIndexChanging="gvList_PageIndexChanging" CellPadding="4" CssClass="list" GridLines="Horizontal" >
    <PagerSettings Mode="NumericFirstLast" />
    <HeaderStyle CssClass="listheader" />
    <Columns>
        <asp:TemplateField>
            <ItemStyle Width="50" />
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Name">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandName="Select" ></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:TemplateField HeaderText="EMAIL">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheUser.Email")%>
            </ItemTemplate>
        </asp:TemplateField>        
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
<asp:Label ID="lblRecordCount" runat="server" />
</asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />
<uc2:New ID="New1" runat="server" Visible="False" />