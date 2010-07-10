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
        IList<ReportBatch> result = TheService.FindReportBatchWithJob();
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
        TheService.CancelReportJob(Id);
        UpdateView();
    }

    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.SubmitReportJob(Id);
        UpdateView();
    }

    protected void lbtnRestart_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.RestartReportJob(Id);
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
}