<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Dui_DWDSAuthorization_New" %>
<%@ Register Src="NewOperator.ascx" TagName="NewOperator" TagPrefix="uc2" %>
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

    <h2>
        Data Source Maintenance</h2>
    <p class="formBtnBoard">
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
    <div class="BGform">
        <table border="0">
            <tr>
                <td class="txtForm">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server" ReadOnly="true"></asp:TextBox></td>
                <td class="txtForm">
                    Type:</td>
                <td>
                    <asp:TextBox ID="txtType" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Description:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" Columns="60" Rows="2" TextMode="MultiLine" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    DW Query Start Date:</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server" ReadOnly="true"></asp:TextBox></td>
                <td class="txtForm" valign="top">
                    End Date:</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server" ReadOnly="true"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <b>Operators List:</b>
    <p class="formBtnBoard">
        <asp:Button ID="btnAddOperator" runat="server" Text="Add Operator" OnClick="btnAddOperator_Click"
            Width="100px" CssClass="btn" />
        <asp:Button ID="btnDeleteOperator" runat="server" Text="Delete" OnClick="btnDeleteOperator_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvOperatorList" AutoGenerateColumns="False" PageSize="20"
        DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Operator Name">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheUser.UserName") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Email">
                <ItemTemplate>
                    <%# DataBinder.Eval(Container.DataItem, "TheUser.Email") %>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="AllowType" HeaderText="Allow Type" />
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
</asp:Panel>
<uc2:NewOperator ID="NewOperator1" runat="server" Visible="False" />
