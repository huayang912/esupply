<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="MasterData_Operation_Po_Main" %>
<%@ Register Src="~/Operation/Po/Edit.ascx" TagName="Edit" TagPrefix="uc2" %>
<%@ Register Src="~/Operation/Po/List.ascx" TagName="List" TagPrefix="uc2" %>
<%@ Register Src="~/Operation/Po/Search.ascx" TagName="Search" TagPrefix="uc2" %>

<uc2:Search ID="ucSearch" runat="server" Visible="True" />
<uc2:Edit ID="ucEdit" runat="server" Visible="false" />
<uc2:List ID="ucList" runat="server" Visible="false" />
