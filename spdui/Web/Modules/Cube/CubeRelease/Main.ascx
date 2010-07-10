<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Cube_CubeRelease_Main" %>
<%@ Register Src="../CubeProcess/Edit.ascx" TagName="ProcessEdit" TagPrefix="uc1" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
<script language="javascript" type="text/javascript">

function ButtonWarning(Action)
{
    if (Action == "Release")
    {
        return confirm("Are you sure you want to Release the cube?");
    }
    else if (Action == "Update")
    {
        return confirm("Are you sure you want to Update Cube Role?");
    }
}

</script>

<h2>Cube Release</h2>
<asp:Label ID="lblMessage" runat="server" Visible="true" CssClass="error"></asp:Label>
<asp:GridView runat="server" ID="gvCubeReleaseList" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" PageSize="15" CellPadding="4"  CssClass="list" GridLines="Horizontal" OnRowDataBound="gvCubeReleaseList_RowDataBound">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>
        <asp:TemplateField HeaderText="Cube Description">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCubeDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheCube.Description") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheCube.Id") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Process Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProcessDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnViewProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Process User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProcessUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ProcessUser.UserName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnViewProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Process Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProcessStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnViewProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Process Description">
            <ItemTemplate>
                <asp:TextBox ID="txtLastProcessDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' Columns="50" Rows="5" TextMode="MultiLine" Width="100"></asp:TextBox>
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Release Date">
            <ItemTemplate>
                <asp:Label ID="lbLastReleaseDate" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "TheLastestCubeRelease.ReleaseDate") %> '/>     
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Last Release User">
            <ItemTemplate>
                <asp:Label ID="lbLastReleaseUser" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "TheLastestCubeRelease.ReleaseUser.UserName") %> '/>     
            </ItemTemplate>
        </asp:TemplateField>
        
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:Label ID="lbStatus" runat="server" Text=' <%# DataBinder.Eval(Container.DataItem, "TheLastestCubeRelease.Status")%> '/>                               
            </ItemTemplate>
        </asp:TemplateField>          
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnUpdateDescription" runat="server" Text="[Update Description]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheCube.Id") %>' CommandName="Select" OnClick="lbtnUpdateDescription_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnUpdateCubeRole" Visible="true" runat="server" Text="[Update Cube Role]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheCube.Id")%>' CommandName="Select" OnClick="lbtnUpdateCubeRole_Click" OnClientClick="return ButtonWarning('Update')"></asp:LinkButton>
                <asp:LinkButton ID="lbtnRelease" runat="server" Text="[Release]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnRelease_Click" OnClientClick="return ButtonWarning('Release')"></asp:LinkButton>
                <asp:LinkButton ID="lbtnWarmCache" runat="server" Text="[Warm Cache]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="LinkButton1_Click" OnClientClick="return ButtonWarning('Warm Cache')"></asp:LinkButton>                
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" /> 
        </asp:TemplateField>
                      
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:ProcessEdit ID="ProcessEdit1" runat="server" Visible="False" />
<uc2:History ID="History1" runat="server" Visible="False" />