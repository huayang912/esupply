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

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;


public partial class Modules_OffLineReport_ReportUserMaintenance_NewUserParameter : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("ReportMaintenance");

    //The entity service
    protected IReportUserMgr TheService
    {
        get
        {
            return GetService("ReportUserMgr.service") as IReportUserMgr;
        }
    }

    //The entity service
    protected IReportParameterMgr TheParameterService
    {
        get
        {
            return GetService("ReportParameterMgr.service") as IReportParameterMgr;
        }
    }

    public ReportUserSheetParameter TheReportUserSheetParameter
    {
        get
        {
            return (ReportUserSheetParameter)ViewState["TheReportUserSheetParameter"];
        }
        set
        {
            ViewState["TheReportUserSheetParameter"] = value;
        }
    }

    public void SetReportUserId(int Id)
    {
        txtReportUserId.Value = Id.ToString();
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            //UpdateView();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveData();
        UpdateView();
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveData();
        TheReportUserSheetParameter = null;
        UpdateView();
    }

    protected void SaveData()
    {
        SessionHelper sessionHelper = new SessionHelper(Page);
        if (TheReportUserSheetParameter == null)
        {
            TheReportUserSheetParameter = new ReportUserSheetParameter();
            TheReportUserSheetParameter.TheUser = TheService.LoadReportUser(int.Parse(txtReportUserId.Value));
            TheReportUserSheetParameter.TheParamter = TheParameterService.LoadReportParameter(int.Parse(ddlParameter.SelectedValue));
        }
        TheReportUserSheetParameter.ParameterContent = txtParameterContent.Text.Trim();
        
        if (TheReportUserSheetParameter == null || TheReportUserSheetParameter.Id == 0)
        {
            TheService.CreateReportUserSheetParameter(TheReportUserSheetParameter);
        }
        else
        {
            TheService.UpdateReportUserSheetParameter(TheReportUserSheetParameter);
        }
    }

    public void UpdateView()
    {
        txtParameterContent.Text = (TheReportUserSheetParameter != null && TheReportUserSheetParameter.Id != 0 ) ? TheReportUserSheetParameter.ParameterContent : "";

        if (TheReportUserSheetParameter != null && TheReportUserSheetParameter.Id != 0) {
            txtParameterId.Value = TheReportUserSheetParameter.TheParamter.Id.ToString();
            lblParameter.Text = TheReportUserSheetParameter.TheParamter.Name;
            ddlParameter.Visible = false;
            lblParameter.Visible = true;
        } else {
            ddlParameter.DataSource = TheService.FindParameterForReportUser(int.Parse(txtReportUserId.Value));
            ddlParameter.DataValueField = "Id";
            ddlParameter.DataTextField = "Name";
            ddlParameter.DataBind();
            //ddlParameter.SelectedIndex = 0;

            ddlParameter.Visible = true;
            txtParameterId.Visible = false;
        }
    }
}
