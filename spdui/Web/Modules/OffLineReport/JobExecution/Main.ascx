<%@ Control Language="C#" AutoEventWireup="true" CodeFile="Main.ascx.cs" Inherits="Modules_OffLineReport_JobExecution_Main" %>
<%@ Register Src="Edit.ascx" TagName="Edit" TagPrefix="uc1" %>
<%@ Register Src="History.ascx" TagName="History" TagPrefix="uc2" %>
<asp:Panel ID="pnlMain" runat="server">

<!-- Modified By Vincent On 2006-9-3 Begin -->


<script language="vbscript">
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
<h2>Report Distribution</h2>
<asp:GridView runat="server" ID="gvReportBatch" AutoGenerateColumns="False" DataKeyNames="LastestReportJob" AllowPaging="True" PageSize="15" OnPageIndexChanging="gvReportBatch_PageIndexChanging" CellPadding="4"  CssClass="list" GridLines="Horizontal">
    <HeaderStyle CssClass="listhead" HorizontalAlign="Left" />                
    <Columns>                    
        <asp:TemplateField HeaderText="Type">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "BatchType") %>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Batch Name">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnBatchName" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "Name") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" OnClick="lbtnHistory_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Action">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCancel" runat="server" Text="[Cancel]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob") != null && ( DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_RUNNING) ||(DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_SUBMIT)||(DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_PENDING))) )%>' OnClick="lbtnCancel_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnSubmit" runat="server" Text="[Submit]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob") != null && (DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_PENDING))%>' OnClick="lbtnSubmit_Click"></asp:LinkButton>
                <asp:LinkButton ID="lbtnNewJob" runat="server" Text="[New Job]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob") == null || DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").ToString().Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_SUCCESS) || DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_FAILED) || DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").ToString().Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_CANCEL)%>' OnClick="lbtnNewJob_Click" ></asp:LinkButton>
                <asp:LinkButton ID="lbtnRestart" runat="server" Text="[Restart]" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" Visible='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob") != null && (DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_FAILED) || DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").ToString().Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_CANCEL)|| DataBinder.Eval(Container.DataItem, "LastestReportJob.Status").ToString().Equals(Dndp.Persistence.Entity.OffLineReport.ReportJob.REPORT_JOB_STATUS_SUCCESS))%>' OnClick="lbtnRestart_Click"></asp:LinkButton>
            </ItemTemplate>
            <ItemStyle HorizontalAlign="Left" Wrap="False" />
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Start Time">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStartTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.StartTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="End Time">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnEndTime" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.EndTime") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Report Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnReportDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.ReportDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create Date">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateDate" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.CreateDate") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Create User">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnCreateUser" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.CreateUser.UserName") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Status") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="Validate Status">
            <ItemTemplate>
                <asp:LinkButton ID="lbtnValidateStatus" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.ValidateStatus") %>' CommandArgument='<%# DataBinder.Eval(Container.DataItem, "LastestReportJob.Id") %>' CommandName="Select" OnClick="lbtnEditJob_Click"></asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
    <AlternatingRowStyle CssClass="listA" />
    <PagerStyle HorizontalAlign="Right" />
</asp:GridView>
</asp:Panel>
<uc1:Edit ID="Edit1" runat="server" Visible="False" />
<uc2:History ID="History1" runat="server" Visible="False"/>

<!-- Modified By Vincent On 2006-9-3 End -->