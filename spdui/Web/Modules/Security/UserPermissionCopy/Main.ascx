<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Security_UserPermissionCopy_Main" %>
<script language="javascript" type="text/javascript">
function preCopy()
{

    var fr = document.getElementsByName("rdSelect");
   
    for(var i = 0; fr.length; i++)
    {
        if (fr[i].checked) 
        {
            document.getElementById("<%= hfFromId.ClientID %>").value = fr[i].value;
            break;
        }
    }
     
     
    var to = document.getElementsByName("cbSelect");
    var hfTo = document.getElementById("<%= hfToId.ClientID %>");
    for(var i = 0; to.length; i++)
    {
        if (to[i].checked) 
        {
        
            if (hfTo.value == "")
            {
                hfTo.value = to[i].value;
            }
            else
            {
                hfTo.value += "," + to[i].value;
            }
        }
    }
}
</script>
<asp:Panel ID="pnlMain" runat="server">
    <h2>
        User Permission Copy</h2>
    <table border="0">
        <tr>
            <td class="TXTform2">
                <asp:RadioButtonList ID="rbDataSource" runat="server">
                    <asp:ListItem Text="All" Value="All" Selected="true"></asp:ListItem>
                    <asp:ListItem Text="DataSource" Value="DS"></asp:ListItem>
                    <asp:ListItem Text="DW DataSource" Value="DW"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
            <td class="TXTform2">
                <asp:Button ID="btnCopy" Text="Copy" runat="server" OnClick="btnCopy_Click" OnClientClick="javascript:preCopy()"
                    CssClass="btn1" />
            </td>
        </tr>
    </table>
    <table border="0">
        <tr>
            <td class="TXTform2">
                <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
            </td>
        </tr>
    </table>
    <hr />
    <table border="0" width="100%">
        <tr>
            <td class="TXTform2" width="50%" valign="top">
                User Name:&nbsp;<asp:TextBox ID="txtFromUserName" runat="server" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearchFrom" runat="server" Text="Search" OnClick="btnSearchFrom_Click"
                    CssClass="btn1" />
            </td>
            <td class="TXTform2" width="50%" valign="top">
                User Name:&nbsp;<asp:TextBox ID="txtToUserName" runat="server" />&nbsp;&nbsp;&nbsp;
                <asp:Button ID="btnSearchTo" runat="server" Text="Search" OnClick="btnSearchTo_Click"
                    CssClass="btn1" />
            </td>
        </tr>
        <tr>
            <td width="50%" valign="top">
                <asp:HiddenField ID="hfFromId" runat="server" />
                <div style="height: 400px; overflow: scroll">
                    <asp:GridView runat="server" ID="gvCopyFrom" AutoGenerateColumns="False" DataKeyNames="Id"
                        CellPadding="4" CssClass="list" GridLines="Horizontal">
                        <HeaderStyle CssClass="listheader" />
                        <Columns>
                            <asp:TemplateField HeaderText="From">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td valign="top">
                                                <input type="radio" class="radio" name="rdSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="lbUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="listA" />
                    </asp:GridView>
                </div>
            </td>
            <td width="50%" valign="top">
                <asp:HiddenField ID="hfToId" runat="server" />
                <div style="height: 400px; overflow: scroll">
                    <asp:GridView runat="server" ID="gvCopyTo" AutoGenerateColumns="False" DataKeyNames="Id"
                        CellPadding="4" CssClass="list" GridLines="Horizontal">
                        <HeaderStyle CssClass="listheader" />
                        <Columns>
                            <asp:TemplateField HeaderText="To ">
                                <ItemTemplate>
                                    <table cellpadding="0" cellspacing="0" border="0">
                                        <tr>
                                            <td valign="top">
                                                <input type="checkbox" class="radio" name="cbSelect" value='<%# DataBinder.Eval(Container.DataItem, "Id")%>' />
                                            </td>
                                            <td valign="top">
                                                <asp:Label ID="lbUserName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UserName")%>' />
                                            </td>
                                        </tr>
                                    </table>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <AlternatingRowStyle CssClass="listA" />
                    </asp:GridView>
                </div>
            </td>
        </tr>
    </table>
</asp:Panel>



