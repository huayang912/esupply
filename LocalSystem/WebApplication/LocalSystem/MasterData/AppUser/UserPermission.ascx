<%@ Control Language="C#" AutoEventWireup="true" CodeFile="UserPermission.ascx.cs"
    Inherits="Security_User_UserPermission" %>

<script type="text/javascript" language="javascript">
//$("#idNotInPermission input:checkbox").click(

//    );
function idNotInPermissionClick()
{
    if($("#idNotInPermission input:checkbox").attr("checked")==true)
    {
        $("#idNotInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idNotInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}

function idInPermissionClick()
{
    if($("#idInPermission input:checkbox").attr("checked")==true)
    {
        $("#idInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=true;
        });
     }
    else
    {
        $("#idInPermissionList input:checkbox").each(function(index,domEle){ 
                  if(this.type=="checkbox")
                       this.checked=false;
        });
    }
}
</script>

<fieldset>
    <legend>
        <asp:Literal ID="lblCode" runat="server" Text="${Security.User.CurrentUser}:" />
        <asp:Literal ID="lbCode" runat="server" /></legend>
    
    <table width="100%">
        <tr>
            <td style="width: 10%">
                ${Security.User.Permission.NotInPermission}:
            </td>
            <td style="width: 30%" id="idNotInPermission" onclick="idNotInPermissionClick()">
                <asp:CheckBox ID="cb_NotInPermission" runat="server" Text="${Common.Select.All}" />
            </td>
            <td style="width: 10%">
            </td>
            <td style="width: 10%">
                ${Security.User.Permission.InPermission}:
            </td>
            <td style="width: 30%" id="idInPermission" onclick="idInPermissionClick()">
                <asp:CheckBox ID="cb_InPermission" runat="server" Text="${Common.Select.All}" />
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idNotInPermissionList">
                    <asp:CheckBoxList ID="CBL_NotInPermission" runat="server"
                        DataTextField="Description" DataValueField="Code" ToolTip="Description">
                    </asp:CheckBoxList>
                </div>
            </td>
            <td valign="middle" align="center" colspan="2">
                <asp:Button ID="ToInBT" runat="server" Text=">>>" OnClick="ToInBT_Click" CssClass="button2" />
                <br />
                <br />
                <asp:Button ID="ToOutBT" runat="server" Text="<<<" OnClick="ToOutBT_Click" CssClass="button2" />
            </td>
            <td style="width: 30%" valign="top">
                <div class="scrolly" id="idInPermissionList">
                    <asp:CheckBoxList ID="CBL_InPermission" runat="server" 
                        DataTextField="Description" DataValueField="Code" ToolTip="Description">
                    </asp:CheckBoxList>
                </div>
            </td>
        </tr>
    </table>
</fieldset>
