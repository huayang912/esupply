<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Search.ascx.cs" Inherits="MasterData_Supplier_Search" %>
<fieldset>
    <table class="mtable">
        <tr>
            <td class="td01">
                <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Supplier.Code}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbCode" runat="server" />
            </td>
            <td class="td01">
                <asp:Literal ID="ltlDescription" runat="server" Text="${MasterData.Supplier.Name}:" />
            </td>
            <td class="td02">
                <asp:TextBox ID="tbDesc" runat="server"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="td01">
                <%--<asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Item.IsActive}:" />--%>
                <asp:Literal ID="ltlSelect" runat="server" Text="${Common.FileUpload.PleaseSelect}:"></asp:Literal>
            </td>
            <td class="td02">
                <%--<asp:CheckBox ID="cbIsActive" runat="server" Checked="true" />--%>
                <asp:FileUpload ID="fileUpload" ContentEditable="false" runat="server" />
                <asp:Button ID="btnImport" runat="server" Text="${Common.Button.Import}" OnClick="btnImport_Click"
                    CssClass="add" />
            </td>
            <td class="td01" />
            <td class="td02">
                <div class="buttons">
                    <asp:Button ID="btnSearch" runat="server" Text="${Common.Button.Search}" OnClick="btnSearch_Click"
                        CssClass="query" />
                    <asp:Button ID="btnNew" runat="server" Text="${Common.Button.New}" OnClick="btnNew_Click"
                        CssClass="add" />
                </div>
            </td>
        </tr>
    </table>
</fieldset>
<div id="GridView" runat="server" visible="false">
    <div id="floatdiv">
        <fieldset>
            <asp:GridView ID="GV_List" runat="server" AutoGenerateColumns="False" DataKeyNames="Code">
                <Columns>
                    <asp:BoundField DataField="Code" HeaderText="${MasterData.Supplier.Code}" />
                    <asp:BoundField DataField="Name" HeaderText="${MasterData.Supplier.Name}" />
                    <asp:BoundField DataField="Address" HeaderText="${Security.User.Address}" />
                    <asp:BoundField DataField="Contact" HeaderText="${MasterData.Address.ContactPersonName}" />
                    <asp:BoundField DataField="Phone" HeaderText="${MasterData.Address.TelephoneNumber}" />
                    <asp:BoundField DataField="Fax" HeaderText="${MasterData.Address.Fax}" />
                </Columns>
            </asp:GridView>
            <div class="tablefooter">
                <asp:Button ID="btnCreate" runat="server" Text="${Common.Button.Create}" OnClick="btnCreate_Click"
                    CssClass="button2" />
                <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" CssClass="button2"
                    OnClick="btnBack_Click" />
            </div>
        </fieldset>
    </div>
</div>
