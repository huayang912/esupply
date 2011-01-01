<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_Item_New" %>
<div id="divFV">
    <fieldset>
        <legend>${MasterData.Item.NewItem}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbCode" runat="server" CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rtvCode" runat="server" ErrorMessage="${MasterData.Item.Code.Empty}"
                        Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlDescription" runat="server" Text="${MasterData.Item.Description}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbDescription" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlUom" runat="server" Text="${MasterData.Item.Uom}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbUom" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlUc" runat="server" Text="${MasterData.Item.Uc}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbUc" runat="server"></asp:TextBox>
                    <asp:RegularExpressionValidator ID="revCount" ControlToValidate="tbUc" runat="server"
                        ValidationGroup="vgSave" ErrorMessage="${MasterData.Item.UC.Format}" ValidationExpression="^[0-9]+(.[0-9]{1,8})?$"
                        Display="Dynamic" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblIsActive" runat="server" Text="${MasterData.Item.IsActive}:" />
                </td>
                <td class="td02">
                    <asp:CheckBox ID="cbIsActive" runat="server" />
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
