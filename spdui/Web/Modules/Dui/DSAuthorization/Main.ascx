<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Dui_DSAuthorization_Main" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>
<asp:Panel ID="pnlMain" runat="server">
    <h2>
        Data Source Authorization</h2>
    <div class="BGform">
        <table border="0">
            <tr>
                <td class="txtForm" style="height: 25">
                    Data Source Type:&nbsp;
                    <asp:DropDownList ID="ddlDSType" runat="server" />
                    &nbsp;&nbsp;&nbsp; Name:&nbsp;
                    <asp:TextBox ID="txtDSName" runat="server" />&nbsp;&nbsp;&nbsp;
                    <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click"
                        CssClass="btn1" />
                </td>
            </tr>
        </table>
    </div>
    <p class="formBtnBoard">
        <asp:Button ID="btnLock" runat="server" Text="Lock Etl Confirm" OnClick="btnLock_Click"
            CssClass="btn" />
        <asp:Button ID="btnUnlock" runat="server" Text="Unlock Etl Confirm" OnClick="btnUnlock_Click"
            CssClass="btn" />
    </p>
    <asp:GridView runat="server" ID="gvList" AllowPaging="True" AutoGenerateColumns="False"
        PageSize="20" DataKeyNames="Id" OnSelectedIndexChanged="gvList_SelectedIndexChanged"
        OnPageIndexChanging="gvList_PageIndexChanging" CellPadding="4" CssClass="list"
        GridLines="Horizontal">
        <HeaderStyle CssClass="listheader" />
        <Columns>
            <asp:TemplateField HeaderText="#">
                <ItemTemplate>
                    <asp:CheckBox ID="cbSelect" runat="server" />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:BoundField DataField="DSType" HeaderText="Type" />
            <asp:TemplateField HeaderText="Data Source Name">
                <ItemTemplate>
                    <asp:LinkButton ID="lbtnDataSourceName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>'
                        CommandName="Select"></asp:LinkButton>
                    <asp:HiddenField ID="hfDSId" runat="server" Value='<%# DataBinder.Eval(Container.DataItem, "Id") %>' />
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
            <asp:CheckBoxField DataField="IsLockEtlConfirm" HeaderText="Is Locked" />
        </Columns>
        <PagerStyle HorizontalAlign="Right" />
        <AlternatingRowStyle CssClass="listA" />
    </asp:GridView>
    <asp:Label ID="lblRecordCount" runat="server" /></asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />
