<%@ Control Language="C#" AutoEventWireup="true" CodeFile="NewRoleDimensionMember.ascx.cs" Inherits="Modules_Cube_CubeRoleDimensionMember_New" %>
<h2>Dimension Data</h2>
<div class=bgForm> 
    <table border=0>
        <tr>
            <td class="txtForm" style="height:25">
                Dimension Name:&nbsp;&nbsp;
                <asp:DropDownList ID="ddlDimensionName" runat="server" DataTextField="DimensionName" DataValueField="DimensionName" AutoPostBack="true" OnSelectedIndexChanged="ddlDimensionName_Change"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Attribute Name:&nbsp;&nbsp;
                <asp:DropDownList ID="ddlAttributeName" runat="server" DataTextField="AttributeName" DataValueField="AttributeName" AutoPostBack="true" OnSelectedIndexChanged="ddlAttributeName_Change"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                Columns Per Row:&nbsp;&nbsp;
                <asp:DropDownList ID="ddlColumns" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlColumns_Change">
                    <asp:ListItem Value="1" Text="1" />
                    <asp:ListItem Value="2" Text="2" />
                    <asp:ListItem Value="5" Text="5" />
                    <asp:ListItem Value="10" Text="10" />
                </asp:DropDownList>           
            </td>
            <td class="txtForm">&nbsp;&nbsp;&nbsp;&nbsp;Visualtotal:</td>
            <td><asp:CheckBox ID="cbVisualtotal" runat="server" /></td>  
        </tr>
    </table>
 </div>
 <asp:HiddenField ID="hfSelectedDimensionName" runat="server"/>
 <asp:HiddenField ID="hfSelectedAttributeName" runat="server"/>
    <p class="formBtnBoard">
        <asp:Button ID="btnUpdate" runat="server" Text="Update" OnClick="btnUpdate_Click"
            CssClass="btn" />
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
    <asp:GridView ID="gvDimensionList" runat="server" Width="100%" DataKeyNames="Id" AutoGenerateColumns="false" CellPadding="4" CssClass="list" GridLines="Horizontal" OnRowDataBound="gvDimensionList_RowDataBound" >
        <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />
        <Columns>
            <asp:BoundField DataField="Id" HeaderText="Id"></asp:BoundField>
            <asp:BoundField DataField="DimensionName" HeaderText="Dimension"></asp:BoundField>
            <asp:BoundField DataField="AttributeName" HeaderText="Attribute"></asp:BoundField>        
            <asp:TemplateField HeaderText="Members">
                <ItemTemplate>
                    <asp:CheckBoxList ID="cbMemberSelect" runat="server" DataTextField="MemberName" DataValueField="MemberValue" RepeatColumns="5" RepeatDirection="Horizontal"/>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
