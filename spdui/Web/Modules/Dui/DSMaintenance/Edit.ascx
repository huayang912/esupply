<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="Modules_Dui_DSMaintenance_Edit" %>
<%@ Register Src="NewField.ascx" TagName="NewField" TagPrefix="uc1" %>
<%@ Register Src="NewOperator.ascx" TagName="NewOperator" TagPrefix="uc2" %>
<%@ Register Src="NewRule.ascx" TagName="NewRule" TagPrefix="uc3" %>
<%@ Register Src="NewCategory.ascx" TagName="NewCategory" TagPrefix="uc4" %>
<%@ Register Src="NewWithDrawTable.ascx" TagName="NewWithDrawTable" TagPrefix="uc5" %>
<%@ Import Namespace="Dndp.Persistence.Entity.Dui" %>
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

    <h2>
        Data Source Maintanence</h2>
    <!-- Modified By Vincent On 2006-9-2 Begin -->
    <b>Basic Information:</b>
    <p class="formBtnBoard">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
            CssClass="btn" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" /></p>
    <div class="bgForm">
        <table border="0">
            <tr>
                <td class="txtForm">
                    Data Source Name:</td>
                <td>
                    <asp:Label ID="txtName" runat="server" /></td>
                <td class="txtForm">
                    Type:</td>
                <td>
                    <asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Description:</td>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtDescription" runat="server" Columns="80"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Data Structure:</td>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtDataStructure" runat="server" Columns="80" Rows="10" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td valign="top" class="txtform">
                    DW Query SQL:</td>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtRuleContent" runat="server" Columns="80" Rows="15" TextMode="MultiLine">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" class="txtform">
                    After Withdraw SQL:</td>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtAfterWithdrawSQL" runat="server" Columns="80" Rows="15"
                        TextMode="MultiLine">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td valign="top" class="txtform">
                    After Row Delete SQL:</td>
                <td colspan="3">
                    &nbsp;<asp:TextBox ID="txtAfterRowDelSQL" runat="server" Columns="80" Rows="15" TextMode="MultiLine">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <b>Field Definitions:</b>
    <br />
    <asp:Label ID="lblFieldDef" runat="server" Visible="false" ForeColor="red" />
    <p class="formBtnBoard">
        <asp:Button ID="btnAddField" runat="server" Text="Add Field" Width="100px" OnClick="btnAddField_Click"
            CssClass="btn" />
        <asp:Button ID="btnDeleteField" runat="server" Text="Delete" OnClick="btnDeleteField_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvFieldList" AutoGenerateColumns="False" PageSize="20"
        DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" HorizontalAlign="Left" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:BoundField DataField="SequenceNo" HeaderText="#" />
            <asp:BoundField DataField="Name" HeaderText="Field" />
            <asp:BoundField DataField="FieldType" HeaderText="Type" />
            <asp:BoundField DataField="FieldLength" HeaderText="Length" />
            <asp:TemplateField HeaderText="Allow Null">
                <ItemTemplate>
                    <asp:CheckBox ID="cbAllowNull" runat="server" Enabled="false" Checked='<%# Eval("IsNullable") %>'
                        CssClass="radio" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Data Key">
                <ItemTemplate>
                    <asp:CheckBox ID="cbDataKey" runat="server" Enabled="false" Checked='<%# Eval("IsDataKey") %>'
                        CssClass="radio" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" />
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
    <b>Operators List:</b>
    <br />
    <asp:Label runat="server" ID="lblOperators" ForeColor="red" Visible="false" />
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
    <b>Validation Rules:</b>
    <br />
    <asp:Label runat="server" ID="lblValidation" ForeColor="red" Visible="false" />
    <p class="formBtnBoard">
        <asp:Button ID="btnAddRule" runat="server" Text="Add Rule" Width="100px" OnClick="btnAddRule_Click"
            CssClass="btn" />
        <asp:Button ID="btnDeleteRule" runat="server" Text="Delete" OnClick="btnDeleteRule_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvRuleList" AutoGenerateColumns="False" PageSize="20"
        DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:BoundField DataField="SequenceNo" HeaderText="#" />
            <asp:TemplateField HeaderText="Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnRuleName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                        CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        OnClick="lbtnRuleName_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="RuleType" HeaderText="Type" />
            <asp:TemplateField HeaderText="Is Dependence">
                <ItemTemplate>
                    <asp:CheckBox ID="lbtnHasDependenceRule" runat="server" Checked='<%# DataBinder.Eval(Container.DataItem, "DependenceRule") != null %>'
                        CssClass="radio" Enabled="false" />
                </ItemTemplate>
            </asp:TemplateField>
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
    <b>Category Definitions:</b>
    <br />
    <asp:Label ID="lblCategory" runat="server" ForeColor="red" Visible="false" />
    <p class="formBtnBoard">
        <asp:Button ID="btnAddCategory" runat="server" Text="Add Category" Width="100px"
            OnClick="btnAddCategory_Click" CssClass="btn" />
        <asp:Button ID="btnDeleteCategory" runat="server" Text="Delete" OnClick="btnDeleteCategory_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvCategoryList" AutoGenerateColumns="False" PageSize="20"
        DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" CssClass="radio" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnCategoryName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                        CommandName="Select" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'
                        OnClick="lbtnCategoryName_Click"></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="Description" HeaderText="Description" />
            <asp:BoundField DataField="ActiveFlag" HeaderText="Is Active" />
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
    <b>WithDrawed Table Definitions:</b>
    <br />
    <asp:Label runat="server" ID="lblWithDrawed" ForeColor="red" Visible="false" />
    <p class="formBtnBoard">
        <asp:Button ID="btnAddWithDrawTable" runat="server" Text="Add" Width="100px" OnClick="btnAddWithDrawTable_Click"
            CssClass="btn" />
        <asp:Button ID="btnDeleteWithDrawTable" runat="server" Text="Delete" OnClick="btnDeleteWithDrawTable_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
    </p>
    <asp:GridView runat="server" ID="gvWithDrawTableList" AutoGenerateColumns="False"
        PageSize="20" DataKeyNames="Id" CellPadding="4" CssClass="list" GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" />
        <Columns>
            <asp:TemplateField>
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </ItemTemplate>
                <HeaderStyle Width="3%" />
            </asp:TemplateField>
            <asp:BoundField DataField="WithDrawTableName" HeaderText="Table Name" />
        </Columns>
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
</asp:Panel>
<!-- Modified By Vincent On 2006-9-2 End -->
<uc1:NewField ID="NewField1" runat="server" Visible="False" />
<uc2:NewOperator ID="NewOperator1" runat="server" Visible="False" />
<uc3:NewRule ID="NewRule1" runat="server" Visible="False" />
<uc4:NewCategory ID="NewCategory1" runat="server" Visible="False" />
<uc5:NewWithDrawTable ID="NewWithDrawTable1" runat="server" Visible="False" />
