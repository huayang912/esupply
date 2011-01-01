<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Supplier_New" %>
<div id="divFV">
    <fieldset>
        <legend>${MasterData.Supplier.AddSupplier}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Supplier.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbCode" runat="server" CssClass="inputRequired" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlName" runat="server" Text="${MasterData.Supplier.Name}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbName" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlAddress" runat="server" Text="${MasterData.Address.Address}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbAddress" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlContact" runat="server" Text="${MasterData.Address.ContactPersonName}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbContact" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlTele" runat="server" Text="${MasterData.Address.TelephoneNumber}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbTele" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlFax" runat="server" Text="${MasterData.Address.Fax}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbFax" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlCarrier" runat="server" Text="${MasterData.Flow.Carrier}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbCarrier" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Supplier.IsActive}:" />
                </td>
                <td class="td02">
                    <asp:CheckBox ID="cbIsActive" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblLeadTime" runat="server" Text="${MasterData.Flow.Strategy.LeadTime}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbLeadTime" runat="server" />
                    <asp:RangeValidator ID="rvLeadTime" ControlToValidate="tbLeadTime" runat="server"
                        Display="Dynamic" ErrorMessage="${Common.Validator.Valid.Number}" MaximumValue="9999999"
                        MinimumValue="0" Type="Double" ValidationGroup="vgSave" />
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <div class="buttons">
                        <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.Save}" OnClick="btnInsert_Click"
                            CssClass="apply" ValidationGroup="vgSave" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
