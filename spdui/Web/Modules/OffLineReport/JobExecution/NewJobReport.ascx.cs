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

public partial class Modules_OffLineReport_JobExecution_NewJobReport : ModuleBase
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

    //The entity service
    protected IReportTemplateMgr TheReportService
    {
        get
        {
            return GetService("ReportTemplateMgr.service") as IReportTemplateMgr;
        }
    }

    public IList<ReportJobReport> TheReportJobReport
    {
        get
        {
            return (IList<ReportJobReport>)ViewState["TheReportJobReport"];
        }
        set
        {
            ViewState["TheReportJobReport"] = value;
        }
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        IList<int> IdList = new List<int>();
        foreach (GridViewRow row in gvReportList.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
            if (cbSelect.Checked)
            {
                IdList.Add((int)(gvReportList.DataKeys[row.RowIndex].Value));
            }
        }
        TheService.UpdateReportJobReport(IdList, int.Parse(txtReportJobId.Value));
       
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    public void UpdateView()
    {
        gvReportList.DataSource = TheReportService.LoadAllActiveReportTemplate();
        gvReportList.DataBind();
        gvReportList.Visible = true;           
    }

    public void SetReportJobId(int Id)
    {
        txtReportJobId.Value = Id.ToString();
    }

    protected bool hasData(int reportId)
    {
        if (TheReportJobReport != null)
        {
            for (int i = 0; i < TheReportJobReport.Count; i++)
            {
                if (TheReportJobReport[i].TheReport.Id == reportId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
