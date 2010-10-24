<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Dui_DWDSMaintenance_New" %>
<%@ Register Src="NewOperator.ascx" TagName="NewOperator" TagPrefix="uc2" %>
<%@ Register Src="NewMergeRule.ascx" TagName="NewRule" TagPrefix="uc2" %>
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
        <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click" CssClass="btn" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" OnClick="btnDelete_Click"
            CssClass="btn" OnClientClick="return ButtonWarning('Delete')" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
    <div class="BGform">
        <table border="0">
            <!-- Modified By Vincent On 2006-9-1 Begin -->
            <tr>
                <td class="txtForm">
                    Name:</td>
                <td>
                    <asp:TextBox ID="txtName" runat="server"></asp:TextBox></td>
                <td class="txtForm">
                    Type:</td>
                <td>
                    <asp:TextBox ID="txtType" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Description:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtDescription" runat="server" Columns="60" Rows="2" TextMode="MultiLine"></asp:TextBox></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    DW Query Start Date:</td>
                <td>
                    <asp:TextBox ID="txtStartDate" runat="server"></asp:TextBox></td>
                <td class="txtForm" valign="top">
                    End Date:</td>
                <td>
                    <asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td valign="top" class="txtForm">
                    Parameters:</td>
                <td>
                    &nbsp;<asp:DropDownList ID="ddlParameter" runat="server" Style="width: 120; border-color: Red;">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Button ID="btnInsertParameter" runat="server" Text="Insert" CssClass="btn" OnClick="btnInsertParameter_Click" /></td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    All Data Query SQL:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtQuerySQL" runat="server" Columns="80" Rows="10" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Data to Delete Query SQL:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtDeleteQuerySQL" runat="server" Columns="80" Rows="10" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Delete Data SQL:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtDeleteSQL" runat="server" Columns="80" Rows="10" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Data to Merge Query SQL:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtMergeQuerySQL" runat="server" Columns="80" Rows="10" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    Merge Data SQL:</td>
                <td colspan="3">
                    <asp:TextBox ID="txtMergeSQL" runat="server" Columns="80" Rows="10" TextMode="MultiLine" /></td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="txtForm" valign="top">
                    DW Query Start Date:</td>
                <td>
                    <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox></td>
                <td class="txtForm" valign="top">
                    End Date:</td>
                <td>
                    <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox></td>
            </tr>
            <!-- Modified By Vincent On 2006-9-1 End -->
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
    <b>Merge Validation Rules:</b>
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
</asp:Panel>
<uc2:NewOperator ID="NewOperator1" runat="server" Visible="False" />
<uc2:NewRule ID="NewRule1" runat="server" Visible="False" />
