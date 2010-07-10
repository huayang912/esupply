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


public partial class Modules_OffLineReport_ReportMaintenance_NewReportSheet : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("ReportMaintenance");

    // Modified By Vincent On 2006-09-04 Begin
    //The entity service
    protected IReportParameterMgr TheParameterService
    {
        get
        {
            return GetService("ReportParameterMgr.service") as IReportParameterMgr;
        }
    }
    // Modified By Vincent On 2006-09-04 End

    public const string SHEET_TYPE_DATA_QUERY = "Data Query";
    public const string SHEET_TYPE_PIVOT_TABLE = "Pivot Table";

    //The entity service
    protected IReportTemplateMgr TheService
    {
        get
        {
            return GetService("ReportTemplateMgr.service") as IReportTemplateMgr;
        }
    }

    public ReportSheet TheReportSheet
    {
        get
        {
            return (ReportSheet)ViewState["TheReportSheet"];
        }
        set
        {
            ViewState["TheReportSheet"] = value;
        }
    }

    public void SetReportTemplateId(int Id)
    {
        txtReportId.Value = Id.ToString();
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
            UpdateView();
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        SaveSheet();
        UpdateView();
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveSheet();
        TheReportSheet = null;
        UpdateView();
    }

    protected void SaveSheet()
    {
        SessionHelper sessionHelper = new SessionHelper(Page);
        if (TheReportSheet == null)
        {
            TheReportSheet = new ReportSheet();
            TheReportSheet.CreateBy = sessionHelper.CurrentUser;
            TheReportSheet.CreateDate = System.DateTime.Now;
            TheReportSheet.TheReport = TheService.LoadReportTemplate(int.Parse(txtReportId.Value));
        }
        TheReportSheet.Description = txtDescription.Text;
        TheReportSheet.LastUpdateBy = sessionHelper.CurrentUser;
        TheReportSheet.LastUpdateDate = System.DateTime.Now;
        TheReportSheet.Name = txtName.Text;
        TheReportSheet.RuleContent = txtRuleContent.Text;
        TheReportSheet.SheetType = rdoSheetType_DataQuery.Checked ? SHEET_TYPE_DATA_QUERY : SHEET_TYPE_PIVOT_TABLE;
        TheReportSheet.SequenceNo = Convert.ToInt32(txtSequenceNo.Text);
        if (TheReportSheet == null || TheReportSheet.Id == 0)
        {
            TheService.CreateReportSheet(TheReportSheet);
        }
        else
        {
            TheService.UpdateReportSheet(TheReportSheet);
        }
    }

    public void UpdateView()
    {
        txtReportSheetId.Value = (TheReportSheet != null && TheReportSheet.Id != 0) ? TheReportSheet.Id.ToString() : "";
        txtName.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.Name : "";
        txtDescription.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.Description : "";
        txtRuleContent.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.RuleContent : "";
        lCreateBy.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.CreateBy.UserName : "";
        lCreateDate.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.CreateDate.ToString() : "";
        lLastUpdateBy.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.LastUpdateBy.UserName : "";
        lLastUpdateDate.Text = (TheReportSheet != null && TheReportSheet.Id != 0 ) ? TheReportSheet.LastUpdateDate.ToString() : "";
        // Modified By Vincent ON 2006-09-04 Begin
        if (TheReportSheet != null && TheReportSheet.Id != 0)
        {
            txtSequenceNo.Text = TheReportSheet.SequenceNo.ToString();
            rdoSheetType_DataQuery.Checked = (TheReportSheet.SheetType == SHEET_TYPE_DATA_QUERY);
            rdoSheetType_PivotTable.Checked = (TheReportSheet.SheetType == SHEET_TYPE_PIVOT_TABLE);

        }
        else
        {
            txtSequenceNo.Text = "";
            rdoSheetType_DataQuery.Checked = false;
            rdoSheetType_PivotTable.Checked = false;
        }
        //Get all parameters
        IList parameters = TheParameterService.LoadAllActiveReportParameter();
        ddlParameter.DataSource = parameters;
        ddlParameter.DataTextField = "Name";
        ddlParameter.DataValueField = "Name";
        ddlParameter.DataBind();
        // Modified By Vincent ON 2006-09-04 End
    }

    /// <summary>
    /// Insert Parameter To Sql
    /// Modified By Vincent Duan On 2006-09-04
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    protected void btnInsertParameter_Click(object sender, EventArgs e)
    {
        txtRuleContent.Text += Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlParameter.SelectedValue);

    }
}
