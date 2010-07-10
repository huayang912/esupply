<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Cube_CubeDistributionJob_Main" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc1" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">
<script language="vbscript" type="text/vbscript">
function ButtonWarning(Action)
    Dim ReturnVal
    Select Case Action
        case "Delete"
            ReturnVal = msgbox("Are you sure you want to delete the record?",17)
            If ReturnVal = 1 then
                ButtonWarning = true
            else
                ButtonWarning = false
            end if
    end select
end function
</script>
<h2>Offline Cube Distribution</h2>
<table border="0">
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
<asp:GridView runat="server" ID="gvCubeDistributionList" AutoGenerateColumns="False" DataKeyNames="TheLastestCubeDistributionJob" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvCube_PageIndexChanging" CellPadding="4"  CssClass="list" GridLines="Horizontal" OnRowDataBound="gvCubeDistributionList_RowDataBound">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>
        <asp:TemplateField HeaderText="Cube Description">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCubeDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnNew" runat="server" Text="[New Job]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnNew_Click" ></asp:LinkButton>                                
                <asp:LinkButton ID="lbtnSubmit" runat="server" Text="[Submit]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnSubmit_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnRestart" runat="server" Text="[Restart]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnRestart_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnCancel" runat="server" Text="[Cancel]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnDelete" runat="server" Text="[Delete]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnDelete_Click"  OnClientClick="return ButtonWarning('Delete')" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Start Time">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.JobStartDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Time">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnEndTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.JobEndDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.CreateDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Creator") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeDistributionJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>   
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:History ID="History1" runat="server" Visible="False" />
<uc2:New ID="New1" runat="server" Visible="False" />