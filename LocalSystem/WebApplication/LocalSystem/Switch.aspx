<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Switch.aspx.cs" Inherits="Switch" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="MSHTML 6.00.5730.13" name="GENERATOR" />
    <title>Switch Page</title>

    <script type="text/javascript" src="Js/jquery.js"></script>

    <script language="javascript" type="text/javascript">
        var flag = false;
        function shift_status() {
            var mainFrame = top.document.getElementsByName("mainframeset")[0];
            if (flag) {
                if (screen.width > 1024) {
                    mainFrame.cols = "220,9,*";
                } else if (screen.width > 800) {
                    mainFrame.cols = "220,9,*";
                } else {
                    mainFrame.cols = "200,9,*";
                }
                //document.getElementById("menuSwitch1").src='Images/Nav/Spacer.gif';
                //document.getElementById("menuSwitch1").title='隐藏';
            }
            else {
                //parent.main.cols = "0,9,*";
                mainFrame.cols = "0,9,*";
                //document.getElementById("menuSwitch1").src='Images/Nav/Spacer.gif';
                //document.getElementById("menuSwitch1").title='显示';
            }
            flag = !flag;
        }
      
    </script>

    <link rel="stylesheet" type="text/css" href="App_Themes/BaseFrame.css" />
</head>
<body onclick="shift_status()" style="margin: 0px; background: url('Images/Switch_bg.jpg') right"
    class="switch">
    <table cellspacing="0" cellpadding="0" border="0" style="height: 100%;" id="tableSwitch"
        runat="server">
        <tr>
            <td width="100">
            </td>
        </tr>
    </table>
</body>
</html>
