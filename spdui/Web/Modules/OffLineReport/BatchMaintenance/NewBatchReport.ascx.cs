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

public partial class Modules_OffLineReport_BatchMaintenance_NewBatchReport : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("BatchMaintenance");

    //The entity service
    protected IReportBatchMgr TheService
    {
        get
        {
            return GetService("ReportBatchMgr.service") as IReportBatchMgr;
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

    public IList<ReportBatchReports> TheReportBatchReports
    {
        get
        {
            return (IList<ReportBatchReports>)ViewState["TheReportBatchReports"];
        }
        set
        {
            ViewState["TheReportBatchReports"] = value;
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
        TheService.UpdateReportBatchReports(IdList, int.Parse(txtReportBatchId.Value));
       
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

    public void SetReportBatchId(int Id)
    {
        txtReportBatchId.Value = Id.ToString();
    }

    protected bool hasData(int reportId)
    {
        if (TheReportBatchReports != null)
        {
            for (int i = 0; i < TheReportBatchReports.Count; i++)
            {
                if (TheReportBatchReports[i].TheReport.Id == reportId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
