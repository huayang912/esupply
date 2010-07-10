<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_DWDSMaintenance_Main" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
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
<h2>DW Data Source Maintenance</h2>
<div class="BGform">
<table border="0">
    <tr>
        <td class="txtForm" style="height:25">
           Data Source Type:&nbsp;
           <asp:DropDownList ID="ddlDSType" runat="server" /> &nbsp;&nbsp;&nbsp;           
           Name:&nbsp;
           <asp:TextBox ID="txtDSName" runat="server"/>&nbsp;&nbsp;&nbsp;
           <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" CssClass="btn1" />
        </td>
    </tr>
</table>
</div>
<p class="formBtnBoard">
    <asp:Button ID="btnNew" runat="server" Text="New DW Data Source" OnClick="btnNew_Click" CssClass="btn" /> 
    <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"  CssClass="btn" OnClientClick="return ButtonWarning('Delete')"/>
</p>
<asp:GridView runat="server" ID="gvList" AllowPaging="True" AutoGenerateColumns="False" PageSize="20" DataKeyNames="Id" OnSelectedIndexChanged="gvList_SelectedIndexChanged" OnPageIndexChanging="gvList_PageIndexChanging" CellPadding="4" CssClass="list" GridLines="Horizontal" >
    <PagerSettings Mode="NumericFirstLast" />
    <HeaderStyle CssClass="listheader" />
    <Columns>
        <asp:TemplateField>
            <ItemTemplate>
                <asp:CheckBox ID="cbSelect" runat="server" CssClass=radio />
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="DSType" HeaderText="Type" />
        <asp:TemplateField HeaderText="Data Source Name">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnDataSourceName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandName="Select" ></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="Description" HeaderText="Description" />
        <asp:BoundField DataField="CreateDate" DataFormatString="{0:d}" HeaderText="Create Date" />
        <asp:TemplateField HeaderText="Create By">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "CreateBy.UserName") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:BoundField DataField="LastUpdateDate" DataFormatString="{0:d}" HeaderText="Last Update Date" />
        <asp:TemplateField HeaderText="Last Update By">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "LastUpdateBy.UserName") %>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
</asp:GridView>
<asp:Label ID="lblRecordCount" runat="server" /></asp:Panel>
<uc2:New ID="New1" runat="server" Visible="False" />