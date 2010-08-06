<%@ Control Language="C#" AutoEventWireup="true" CodeFile="History.ascx.cs" Inherits="Modules_OffLineReport_JobExecution_History" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>

<!-- Modified By Vincent On 2006-9-3 Begin -->
<asp:Panel ID="pnlMain" runat="server">
<h2>Job History for"<asp:Label ID="txtBatchName" runat="server" Font-Size=Medium></asp:Label>"</h2>
<p class="formBtnBoard">

<asp:Button ID="btnBack" runat="server" OnClick="btnBack_Click" Text="Back" CssClass="btn" />
</p>
<asp:GridView runat="server" ID="gvHistory" AutoGenerateColumns="False" DataKeyNames="Id" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvHistory_PageIndexChanging" CellPadding="4"  CssClass="list" GridLines="Horizontal" OnRowDataBound="gvHistory_RowDataBound">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>                    
        <asp:TemplateField HeaderText="Type">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheBatch.BatchType")%>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Batch Name">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TheBatch.Name") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCancel" runat="server" Text="[Cancel]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# (DataBinder.Eval(Container.DataItem, "Status").Equals("Pending") || DataBinder.Eval(Container.DataItem, "Status").Equals("Submit") || DataBinder.Eval(Container.DataItem, "Status").Equals("Running"))%>' OnClick="lbtnCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnSubmit" runat="server" Text="[Submit]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "Status").Equals("Pending") %>' OnClick="lbtnSubmit_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnRestart" runat="server" Text="[Restart]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# !DataBinder.Eval(Container.DataItem, "Status").Equals("Submit") && !DataBinder.Eval(Container.DataItem, "Status").Equals("Pending") %>' OnClick="lbtnRestart_Click"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Start Time">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "StartTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Time">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnEndTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "EndTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Report Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnReportDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "ReportDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Update Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnLastUpdateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UpdateDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Last Update User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnLastUpdateUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "UpdateUser.UserName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />
    <!-- Modified By Vincent On 2006-9-1 Begin Rows:30-->
    <!--<ItemStyle HorizontalAlign="Center" />-->
    <!-- Modified By Vincent On 2006-9-1 End --> 
    
<!-- Modified By Vincent On 2006-9-3 End -->