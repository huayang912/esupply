<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Nav.aspx.cs" Inherits="Nav" Culture="auto" UICulture="auto"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxControlToolkit" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Nav Page</title>

    <script language="javascript" type="text/javascript">
        window.onerror = function() { return true; } //屏蔽错误
    </script>

    <link href="App_Themes/Base.css" type="text/css" rel="stylesheet" />
    <link href="App_Themes/BaseFrame.css" type="text/css" rel="stylesheet" />
</head>
<body class="leftbody" >
    <form id="form1" runat="server">
    <input type="hidden" name="id_hideUser" id="id_hideUser" runat="server" />
    <div style="position: absolute; top: 3px; left: 3px" id="DivTreeView">
        <asp:TreeView ID="TreeView1" runat="server" DataSourceID="SiteMapDataSource1" ShowLines="true"
            ExpandDepth="1" MaxDataBindDepth="2" Target="right" ShowExpandCollapse="true"
            OnSelectedNodeChanged="TreeView1_SelectedNodeChanged">
        </asp:TreeView>
        <asp:SiteMapDataSource ID="SiteMapDataSource1" runat="server" ShowStartingNode="false" />
    </div>
    <div style="display: none; position: absolute; top: 3px; left: 0px;" id="DivFavorites"
        class="listfav" runat="server">
    </div>
    <div style="display: none; position: absolute; top: 3px; left: 0px;" id="DivHistory"
        class="listfav" runat="server">
    </div>
    </form>
</body>
</html>
