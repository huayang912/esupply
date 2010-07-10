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

public partial class Modules_OffLineReport_JobExecution_History : ModuleBase
{
    public event EventHandler Back;

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


    //The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The public method to clear the view
    public void UpdateView()
    {
        IList<ReportJob> result = TheService.FindReportJobByBatchId(TheReportBatch.Id) as IList<ReportJob>;

        txtBatchName.Text = TheReportBatch.Name;

        gvHistory.DataSource = result;
        gvHistory.DataBind();
    }

    public ReportBatch TheReportBatch
    {
        get
        {
            return (ReportBatch)ViewState["TheReportBatch"];
        }
        set
        {
            ViewState["TheReportBatch"] = value;
        }
    }


    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.CancelReportJob(Id);
        UpdateView();
    }

    protected void lbtnRestart_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.RestartReportJob(Id);
        UpdateView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Edit1.Back += new EventHandler(Edit1_Back);
    }

    void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
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

    protected void gvHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvHistory.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    // Modified by vincent at 2007-12-05 begin
    protected void gvHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {

    }
    // Modified by vincent at 2007-12-05 end
}
