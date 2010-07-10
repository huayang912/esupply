<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Cube_CubeProcess_History" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>

<asp:Panel ID="pnlMain" runat="server">
<h2>Process History for "<asp:Label ID="txtCubeName" runat="server" Font-Size=Medium></asp:Label>"</h2>

<p class="formBtnBoard">
<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn" />
</p>

<asp:GridView runat="server" ID="gvHistory" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvHistory_PageIndexChanging" CellPadding="4"  CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>                    
        <asp:TemplateField HeaderText="Cube Description">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheCube.Description") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Errors">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnErrors" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Errors") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Problems">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProblems" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Problems") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Warnings">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnWarnings" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Warnings") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreateDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "CreateUser.UserName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />