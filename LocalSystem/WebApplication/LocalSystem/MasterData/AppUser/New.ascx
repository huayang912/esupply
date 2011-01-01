<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="MasterData_User_New" %>


<div id="divFV" runat="server">
    <fieldset>
        <legend>${Security.User.AddUser}</legend>
        <table class="mtable">
            <tr>
                <td class="td01">
                    <asp:Literal ID="lblCode" runat="server" Text="${Security.User.Code}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbCode" runat="server" CssClass="inputRequired"></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvCode" runat="server" ErrorMessage="${Security.User.Code.Empty}"
                        Display="Dynamic" ControlToValidate="tbCode" ValidationGroup="vgSave" />
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
                    <asp:TextBox ID="tbPassword" TextMode="Password" runat="server" CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="${Security.User.Password.Empty}"
                        Display="Dynamic" ControlToValidate="tbPassword" ValidationGroup="vgSave" />
                </td>
                <td class="td01">
                    <asp:Literal ID="lblConfirmPassword" runat="server" Text="${Security.User.ConfirmPassword}:" />
                </td>
                <td class="td02">
                    <asp:TextBox ID="tbConfirmPassword" TextMode="Password" runat="server" CssClass="inputRequired" />
                    <asp:RequiredFieldValidator ID="rfvConfirmPassword" runat="server" ErrorMessage="${Security.User.ConfirmPassword.Empty}"
                        Display="Dynamic" ControlToValidate="tbConfirmPassword" ValidationGroup="vgSave" />
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
        </table>
        <div class="tablefooter">
            <asp:Button ID="btnInsert" runat="server" Text="${Common.Button.Save}"
                OnClick="btnInsert_Click" CssClass="button2" ValidationGroup="vgSave" />
            <asp:Button ID="btnBack" runat="server" Text="${Common.Button.Back}" OnClick="btnBack_Click"
                CssClass="button2" />
        </div>
    </fieldset>
</div>
