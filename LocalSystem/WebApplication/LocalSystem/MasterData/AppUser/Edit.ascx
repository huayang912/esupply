<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Edit.ascx.cs" Inherits="MasterData_User_Edit" %>
<%@ Register Assembly="com.LocalSystem.Control" Namespace="com.LocalSystem.Control"
    TagPrefix="cc1" %>


<div id="divFV" runat="server">
    <fieldset>
        <legend>${Security.User.UpdateUser}</legend>
        <table class="mtable" width="100%">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCode" runat="server" Text="${Security.User.Code}:" />
                </td>
                <td class="td02">
                    <asp:Literal ID="tbCode" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblFirstName" runat="server" Text="${Security.User.FirstName}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbFirstName" runat="server"></asp:TextBox>
                </td>
                <td class="td01">
                    <asp:Literal ID="lblLastName" runat="server" Text="${Security.User.LastName}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbLastName" runat="server"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblPassword" runat="server" Text="${Security.User.Password}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblConfirmPassword" runat="server" Text="${Security.User.ConfirmPassword}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbConfirmPassword" TextMode="Password" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblEmail" runat="server" Text="${Security.User.Email}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbEmail" runat="server" />
                    <asp:RegularExpressionValidator ID="revEmail" runat="server" ControlToValidate="tbEmail"
                        Display="Dynamic" ErrorMessage="${Security.User.Email.Format.Error}" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                        ValidationGroup="vgSave" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblAddress" runat="server" Text="${Security.User.Address}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbAddress" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblPhone" runat="server" Text="${Security.User.Phone}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbPhone" runat="server" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblFax" runat="server" Text="${MasterData.Address.Fax}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbFax" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                    ${Security.UserPreference.Language}:
                </td>
                <td class="td02">
                    <asp:RadioButtonList ID="rblLanguage" runat="server" RepeatLayout="Flow" RepeatDirection="Horizontal">
                        <asp:ListItem Selected="True" Text="中文" Value="zh-CN" />
                        <asp:ListItem Text="English" Value="en-US" />
                    </asp:RadioButtonList>
                </td>
                <td class="td01">
                    <asp:Literal ID="lblIsActive" runat="server" Text="${Security.User.IsActive}:" />
                </td>
                <td class="td02">
                    <asp:CheckBox ID="cbIsActive" runat="server" />
                </td>
            </tr>
            <tr>
                <td class="td01">
                </td>
                <td class="td02">
                </td>
                <td class="td01">
                </td>
                <td class="td02">
                    <div class="buttons">
                        <asp:Button ID="btnUpdate" runat="server" Text="${Common.Button.Save}" ValidationGroup="vgSave"
                            CssClass="add"   OnClick="btnUpdate_Click"/>
                        <asp:Button ID="btnDelete" runat="server" CommandName="Delete" Text="${Common.Button.Delete}"
                            OnClientClick="return confirm('${Common.Button.Delete.Confirm}')" CssClass="delete"  OnClick="btnDelete_Click" />
                        <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                            CssClass="back" />
                    </div>
                </td>
            </tr>
        </table>
    </fieldset>
</div>
