<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_Item_Edit" %>
<%@ Register Assembly="com.LocalSystem.Control" Namespace="com.LocalSystem.Control"
    TagPrefix="cc2" %>
<div id="divFV" runat="server">
    <fieldset>
        <legend>${MasterData.Item.UpdateItem}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCode" runat="server" Text="${MasterData.Item.Code}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbCode" runat="server" />
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
                    <asp:Literal ID="ltlCreateUser" runat="server" Text="${MasterData.Common.CreateUser}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbCreateUser" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlCreateDate" runat="server" Text="${MasterData.Common.CreateDate}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbCreateDate" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="ltlLastModifyUser" runat="server" Text="${MasterData.Common.LastModifyUser}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbLastModifyUser" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="ltlLastModifyDate" runat="server" Text="${MasterData.Common.LastModifyDate}:" />
                </td>
                <td class="td02">
                    <asp:Label ID="lbLastModifyDate" runat="server" />
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
                        <asp:Button ID="btnSave" runat="server" Text="${Common.Button.Save}" OnClick="btnSave_Click"
                            CssClass="apply" ValidationGroup="vgSave" />
                        <asp:Button ID="btnDelete" runat="server" Text="${Common.Button.Delete}" OnClick="btnDelete_Click"
                            CssClass="delete" OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
