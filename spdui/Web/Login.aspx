<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" Culture="auto" UICulture="auto" %>

<%@ Register Src="UserControls/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc3" %>
<%@ Register Src="UserControls/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UserControls/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head>
    <title><%= GetLocalResourceObject("PageTitle").ToString()%></title>
    <link href="style.css" type="text/css" rel="stylesheet">

</head>
<body>
    <form id="form1" runat="server"><uc1:Top ID="Top1" runat="server" />
        <table width="100%" height="500" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td valign="center" align="center">
               <div  style="width:25%;text-align:right;"> 
                  <%= GetLocalResourceObject("UserLoginCaption").ToString()%>
                  <div class="BGform"   style="margin:0;padding:20px">
                       <table border="0" >
                        <tr>
                            <td class=txtForm><%= GetLocalResourceObject("txtUserNamePromt").ToString()%></td>
                            <td><asp:TextBox ID="txtUserName" runat="server" meta:resourcekey="txtUserNameResource1" CssClass=inputtxt></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td class=txtForm><%= GetLocalResourceObject("txtPasswordPromt").ToString()%></td>
                            <td><asp:TextBox ID="txtPassword" runat="server" TextMode="Password" meta:resourcekey="txtPasswordResource1"  CssClass=inputtxt></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2"><asp:Label ID="lblErrorMessage" runat="server" Visible="False" Text="Invalid user name or password." CssClass="error" meta:resourcekey="lblErrorMessage"></asp:Label></td>
                        </tr>
                       
                    </table>
               </div>
                    <asp:Button ID="btnLogin" runat="server" Text="Login" OnClick="btnLogin_Click" CssClass=btn2 />
                           </div>
                </td>
            </tr>
          
        </table> <uc2:Bottom ID="Bottom1" runat="server" />
    </form>
</body>
</html>

