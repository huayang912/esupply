<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_Cube_CubeRelease_History" %>
<%@ Register Src="../CubeProcess/Edit.ascx" TagName="ProcessEdit" TagPrefix="uc1" %>
<asp:Panel ID="pnlMain" runat="server">
<asp:HiddenField ID="hfCubeId" runat="server"/>
<h2>Cube Release History</h2>
    <p class="formBtnBoard">        
        <asp:Button ID="btnBack" runat="server" Text="Back" OnClick="btnBack_Click" CssClass="btn" />
    </p>
<asp:GridView runat="server" ID="gvCubeReleaseList" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" PageSize="15" CellPadding="4"  CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>
        <asp:TemplateField HeaderText="Cube Description">
            <ItemTemplate>
                <asp:Label ID="lbtnCubeDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheProcess.TheCube.Description") %>' ></asp:Label>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Process Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProcessDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheProcess.EndTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheProcess.Id") %>' CommandName="Select" OnClick="lbtnViewProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Process User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProcessUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheProcess.ProcessUser.UserName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheProcess.Id") %>' CommandName="Select" OnClick="lbtnViewProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Release Date">
            <ItemTemplate>
                <asp:Label ID="lbLastReleaseDate" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "ReleaseDate") %> '/>     
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Release User">
            <ItemTemplate>
                <asp:Label ID="lbLastReleaseUser" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "ReleaseUser.UserName") %> '/>     
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="lbStatus" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "Status")%> '/>                               
            </ItemTemplate>
        </asp:TemplateField>  
        
        <asp:TemplateField HeaderText="Description">
            <ItemTemplate>
                <asp:Label ID="lbDescription" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "Description")%> '/>                               
            </ItemTemplate>
        </asp:TemplateField>                      
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:ProcessEdit ID="ProcessEdit1" runat="server" Visible="False" />