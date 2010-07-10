<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Top.ascx.cs" Inherits="UserControls_Top" %>

<table border="0" width="100%" cellspacing="0" cellpadding="0">
	<tr >
	    <td valign="top" width="200" height="50"><img src="<%= Request.ApplicationPath %>/Images/logo-e.gif"></td>
	    <td  align="right" runat="server" id="tdAfterLogin">
	        <asp:Label ID="lblCurrentUser" runat="server" CssClass=TxtTop></asp:Label> | <a href="Logoff.aspx" class=TxtTop>Logoff</a>
	    </td>
    </tr>
</table>
<table border="0" width="100%" cellspacing="1" cellpadding="0" bgcolor=7b9ebd>
	<tr >
	    <td ></td>
    </tr>
</table>
