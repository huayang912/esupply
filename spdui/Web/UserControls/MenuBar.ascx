<%@ Control Language="C#" AutoEventWireup="true" CodeFile="MenuBar.ascx.cs" Inherits="UserControls_MenuBar" %>

<%@ Register TagPrefix="solpart" Namespace="Solpart.WebControls" Assembly="SolpartWebControls" %>
<p style="margin:0;margin-bottom:5px;">
<solpart:SolpartMenu id="ctlSolpartMenu" runat="server" Display="Horizontal" RootArrow="True" SelectedColor="White" BackColor="#D6EFF7" IconImagesPath="<% =Request.ApplicationPath %>/Images/" HighlightColor="#7B9EBD" IconWidth="30" MenuBarHeight="30" MenuItemHeight="20" SelectedBorderColor="Transparent" SelectedForeColor="#003366" ShadowColor="#7B9EBD" Font-Bold="False" Moveable="False" MenuEffects-MouseOverExpand="true"></solpart:SolpartMenu>
</p>