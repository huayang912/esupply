using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.Collections.Generic;

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;

//TODO: Add other using statements here.

public partial class Modules_OffLineReport_JobExecution_Main : ModuleBase
{
    //Get the logger
	private static ILog log = LogManager.GetLogger("JobExecution");
	
	//The entity service
    protected IReportJobMgr TheService
    {
        get
        {
            return GetService("ReportJobMgr.service") as IReportJobMgr;
        }
    }

    //The entity service
    protected IReportBatchMgr TheBatchService
    {
        get
        {
            return GetService("ReportBatchMgr.service") as IReportBatchMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		if (!IsPostBack)
        {
            UpdateView();
        }
    }
	
	protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Edit1.Back += new EventHandler(Edit1_Back);
        History1.Back += new EventHandler(History1_Back);
        //UpdateView();
		//TODO: Add other init code here.
    }

    //The event handler when user click button "Back" button on New page.
    void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    void History1_Back(object sender, EventArgs e)
    {
        History1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    //Do data query and binding.
    private void UpdateView()
    {
        IList<ReportBatch> result = TheService.FindReportBatchWithJob(this.CurrentUser);
        gvReportBatch.DataSource = result;
        gvReportBatch.DataBind();
    }

	protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        ReportBatch entity = TheBatchService.LoadReportBatch(Id);
        History1.TheReportBatch = entity;
        History1.UpdateView();
        History1.Visible = true;

        pnlMain.Visible = false; 
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.CancelReportJob(Id, this.CurrentUser);
        UpdateView();
    }

    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.SubmitReportJob(Id, this.CurrentUser);
        UpdateView();
    }

    protected void lbtnRestart_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.RestartReportJob(Id, this.CurrentUser);
        UpdateView();
    }

    protected void lbtnNewJob_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        ReportBatch rb = TheBatchService.LoadReportBatch(Id);
        Edit1.Visible = true;
        Edit1.TheReportJob = TheService.CreateNewReportJobByBatchId(rb.Id, CurrentUser);
        Edit1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void lbtnEditJob_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        ReportJob rj = TheService.LoadReportJob(Id);
        Edit1.Visible = true;
        Edit1.TheReportJob = rj;
        Edit1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void gvReportBatch_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvReportBatch.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    //protected void gvReportBatch_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lblBatchType = (Label)e.Row.FindControl("lblBatchType");
    //        LinkButton lbtnBatchName = (LinkButton)e.Row.FindControl("lbtnBatchName");
    //        LinkButton lbtnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");
    //        LinkButton lbtnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");
    //        LinkButton lbtnNewJob = (LinkButton)e.Row.FindControl("lbtnNewJob");

    //        LinkButton lbtnStartTime = (LinkButton)e.Row.FindControl("lbtnStartTime");
    //        LinkButton lbtnEndTime = (LinkButton)e.Row.FindControl("lbtnEndTime");
    //        LinkButton lbtnReportDate = (LinkButton)e.Row.FindControl("lbtnReportDate");
    //        LinkButton lbtnLastUpdateDate = (LinkButton)e.Row.FindControl("lbtnLastUpdateDate");
    //        LinkButton lbtnLastUpdateUser = (LinkButton)e.Row.FindControl("lbtnLastUpdateUser");
    //        LinkButton lbtnStatus = (LinkButton)e.Row.FindControl("lbtnStatus");

    //        ReportJob reportJob = (ReportJob)e.Row.DataItem;

    //        if (reportJob.Id == 0) {
    //            lblBatchType.Text = reportJob.TheBatch.BatchType;
    //            lbtnBatchName.Text = reportJob.TheBatch.Name;
    //            lbtnBatchName.Visible = true;
    //            //lbtnBatchName.CommandArgument = reportJob.TheBatch.Id.ToString();
    //            lbtnNewJob.Visible = true;
    //           // lbtnNewJob.CommandArgument = reportJob.TheBatch.Id.ToString();

    //            lbtnStartTime.Text = string.Empty;
    //            lbtnEndTime.Text = string.Empty;
    //            lbtnReportDate.Text = string.Empty;
    //            lbtnLastUpdateDate.Text = string.Empty;
    //            lbtnLastUpdateUser.Text = string.Empty;
    //            lbtnStatus.Text = string.Empty;
    //        }
    //        else 
    //        {
    //            lblBatchType.Text = string.Empty;
    //            lbtnBatchName.Text = string.Empty;
    //            lbtnBatchName.Visible = false;
    //            //lbtnBatchName.CommandArgument = string.Empty;
    //            lbtnNewJob.Visible = false;
    //            //lbtnNewJob.CommandArgument = string.Empty;
               
    //            lbtnStartTime.Text = reportJob.StartTime.ToString();
    //            lbtnEndTime.Text = reportJob.EndTime.ToString();
    //            lbtnReportDate.Text = reportJob.ReportDate.ToString();
    //            lbtnLastUpdateDate.Text = reportJob.UpdateDate.ToString();
    //            lbtnLastUpdateUser.Text = reportJob.UpdateUser.UserName;
    //            lbtnStatus.Text = reportJob.Status;
    //        }
            
    //        if (reportJob.Id != 0 && 
    //            (reportJob.Status == ReportJob.REPORT_JOB_STATUS_RUNNING
    //            || reportJob.Status == ReportJob.REPORT_JOB_STATUS_SUBMIT
    //            || reportJob.Status == ReportJob.REPORT_JOB_STATUS_PENDING))
    //        {
    //            //lbtnBatchName.CommandArgument = reportJob.Id.ToString();
    //            lbtnCancel.Visible = true;
    //        }
    //        else 
    //        {
    //           // lbtnBatchName.CommandArgument = string.Empty;
    //            lbtnCancel.Visible = false;
    //        }

    //        if (reportJob.Id != 0 && reportJob.Status == ReportJob.REPORT_JOB_STATUS_PENDING)
    //        {
    //            //lbtnBatchName.CommandArgument = reportJob.Id.ToString();
    //            lbtnSubmit.Visible = true;               
    //        }
    //        else 
    //        {
    //            //lbtnBatchName.CommandArgument = string.Empty;
    //            lbtnSubmit.Visible = false;
    //        }
    //    }
    //}
}