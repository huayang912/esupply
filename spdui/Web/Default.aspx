<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default.aspx.cs" Inherits="_Default"
    Culture="auto" UICulture="auto" %>

<%@ Register Src="UserControls/MenuBar.ascx" TagName="MenuBar" TagPrefix="uc3" %>
<%@ Register Src="UserControls/Top.ascx" TagName="Top" TagPrefix="uc1" %>
<%@ Register Src="UserControls/Bottom.ascx" TagName="Bottom" TagPrefix="uc2" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head>
    <title>Schering-Plough(China) Data Process Platform</title>
    <link href="style.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="JS/jquery.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <table width="980" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td>
                    <uc1:Top ID="Top1" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top">
                    <uc3:MenuBar ID="MenuBar1" runat="server" />
                </td>
            </tr>
            <tr>
                <td valign="top" height="400">
                    <p>
                        <asp:Label ID="lblErrorMessage" runat="server" CssClass="error" meta:resourcekey="lblErrorMessageResource1"></asp:Label></p>
                    <asp:PlaceHolder ID="phModule" runat="server"></asp:PlaceHolder>
                </td>
            </tr>
            <tr>
                <td>
                    <uc2:Bottom ID="Bottom1" runat="server" />
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
