<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_Cube_CubeProcess_Main" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>
<%@ Register Src="New.ascx" TagName="New" TagPrefix="uc2" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc3" %>
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
<h2>Cube Process</h2>
<table border="0">
    <tr>
        <td colspan="2" align="left">
            <asp:Label ID="lblMessage" runat="server" Visible="false" CssClass="error"></asp:Label>
        </td>
    </tr>
</table>
<span style="display:none">
    <asp:Button ID="btnValidationFinish" runat="server" OnClick="btnValidationFinish_Click" Text="ValidationFinish"/>&nbsp;        
</span>
<asp:GridView runat="server" ID="gvCubeProcessList" AutoGenerateColumns="False" DataKeyNames="TheLastestCubeProcess" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvCube_PageIndexChanging" CellPadding="4"  CssClass="list" GridLines="Horizontal" OnRowDataBound="gvCubeProcessList_RowDataBound">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>
        <asp:TemplateField HeaderText="Cube Description">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCubeDescription" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Description") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnNewProcess" runat="server" Text="[New Process]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnNewProcess_Click" ></asp:LinkButton>                
                <%# GenerateRevalidateString((Dndp.Persistence.Entity.Cube.CubeDefinition)Container.DataItem)%>                
                <asp:LinkButton ID="lbtnProcess" runat="server" Text="[Process]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnProcess_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnReProcess" runat="server" Text="[Re-Process]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnReProcess_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnCancel" runat="server" Text="[Cancel]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnDelete" runat="server" Text="[Delete]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnDelete_Click"  OnClientClick="return ButtonWarning('Delete')" ></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Errors">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnErrors" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Errors") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Problems">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnProblems" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Problems") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Warnings">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnWarnings" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Warnings") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.CreateDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.CreateUser.UserName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TheLastestCubeProcess.Id") %>' CommandName="Select" OnClick="lbtnEditProcess_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>   
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />
<uc2:New ID="New1" runat="server" Visible="False" />
<uc3:History ID="History1" runat="server" Visible="False" />

<script language="javascript" type="text/javascript">
    function RevalidateAll(processId, vrIds)
    {        
	    window.showModalDialog(
	        "Popup/ProcessValidationRule.aspx?processId=" + processId + "&validationIds=" + vrIds
	        , null
	        , "dialogWidth:400px;dialogHeight:300px;status:no;help:no;scroll:yes");
	    
	    document.getElementById("<%= btnValidationFinish.ClientID %>").click(); 
    }
</script>