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

public partial class Modules_OffLineReport_ReportUserMaintenance_NewUserReport : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("BatchMaintenance");

    //The entity service
    protected IReportUserMgr TheService
    {
        get
        {
            return GetService("ReportUserMgr.service") as IReportUserMgr;
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

    public IList<ReportUserSheet> TheReportUserSheet
    {
        get
        {
            return (IList<ReportUserSheet>)ViewState["TheReportUserSheet"];
        }
        set
        {
            ViewState["TheReportUserSheet"] = value;
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
        TheService.UpdateReportUserSheet(IdList, int.Parse(txtReportUserId.Value));
       
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

    public void SetReportUserId(int Id)
    {
        txtReportUserId.Value = Id.ToString();
    }

    protected bool hasData(int reportId)
    {
        if (TheReportUserSheet != null)
        {
            for (int i = 0; i < TheReportUserSheet.Count; i++)
            {
                if (TheReportUserSheet[i].TheReport.Id == reportId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
