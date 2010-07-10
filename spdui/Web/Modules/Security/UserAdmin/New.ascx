<%@ Control Language="C#" AutoEventWireup="true" CodeFile="New.ascx.cs" Inherits="Modules_Security_UserAdmin_New" %>
 <!-- Modified By Vincent On 2006-9-4 -->

 <h2>New User</h2>
 <p class="formBtnBoard">
 <asp:Button ID="btnSubmit" runat="server" Text="Save" OnClick="btnSubmit_Click"  CssClass="btn" />
 <asp:Button ID="btnSubmitContinue" runat="server" Text="Save & Continue Add" OnClick="btnSubmitContinue_Click"  CssClass="btn"  />
 <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click"  CssClass="btn"  />
</p>
<div class="BGform">
<table border="0">
    <tr>
        <td class="txtform">User Name:</td>
        <td><asp:TextBox ID="txtUserName" runat="server"></asp:TextBox></td>
         <td class="txtform">Passowrd:</td>
        <td>
            <asp:TextBox ID="txtPassword" runat="server" TextMode="Password"></asp:TextBox>
        </td>

        <td class="txtform">Comfirm Password:</td>
        <td>
            <asp:TextBox ID="txtConfirmPassword" runat="server" TextMode="Password"></asp:TextBox>
            
        </td>
    </tr>
    <tr>
        <td class="txtform">Email:</td>
        <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>

        <td class="txtform">Windows Domain:</td>
        <td><asp:TextBox ID="txtWindowsDomain" runat="server"></asp:TextBox></td>

       <td class="txtform">Windows User Name:</td>
        <td><asp:TextBox ID="txtWindowsUserName" runat="server"></asp:TextBox></td>
    </tr>
    </table>
</div>
<span class="txtform">Roles:</span>
 <asp:GridView runat="server" ID="gvRole" AutoGenerateColumns="False" DataKeyNames="Id" CssClass="list" CellPadding="4" GridLines="Horizontal">
                <Columns>
                    <asp:BoundField DataField="Name" HeaderText="Role Name" />
                    <asp:BoundField DataField="Description" HeaderText="Description" />
                    <asp:TemplateField HeaderText="In role?">
                        <ItemTemplate>
                            <asp:CheckBox ID="cbInRole" runat="server" Checked="false" CssClass="radio"/>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <HeaderStyle CssClass="listHead" HorizontalAlign="Left" />
        <AlternatingRowStyle CssClass="listA" />
            </asp:GridView>
<asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>

           