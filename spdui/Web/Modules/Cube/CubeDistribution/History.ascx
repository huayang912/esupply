<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Cube_CubeDistributionJob_History" %>
<table border="0">
	<tr>
        <td>JobDescription:</td>
        <td><asp:TextBox ID="txtJobDescription" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>CreateDate:</td>
        <td><asp:TextBox ID="txtCreateDate" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>Creator:</td>
        <td><asp:TextBox ID="txtCreator" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>UpdateDate:</td>
        <td><asp:TextBox ID="txtUpdateDate" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>BeginDate:</td>
        <td><asp:TextBox ID="txtBeginDate" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>EndDate:</td>
        <td><asp:TextBox ID="txtEndDate" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>Status:</td>
        <td><asp:TextBox ID="txtStatus" runat="server"></asp:TextBox></td>
    </tr>
	<tr>
        <td>StatusDescription:</td>
        <td><asp:TextBox ID="txtStatusDescription" runat="server"></asp:TextBox></td>
    </tr>
    <tr>
        <td>
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
    <tr>
        <td colspan="2">
            <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click" />
            <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" />
        </td>
    </tr>
</table>