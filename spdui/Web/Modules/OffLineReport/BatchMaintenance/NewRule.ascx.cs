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
using Dndp.Web;
using Dndp.Service.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using log4net;

public partial class Modules_OffLineReport_BatchMaintenance_NewRule : ModuleBase
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

    protected IReportValidationRuleMgr TheRuleService
    {
        get
        {
            return GetService("ReportValidationRuleMgr.service") as IReportValidationRuleMgr;
        }
    }

    public ReportValidationRule TheReportValidationRule
    {
        get
        {
            return (ReportValidationRule)ViewState["TheReportValidationRule"];
        }
        set
        {
            ViewState["TheReportValidationRule"] = value;
        }
    }

    public int BatchId
    {
        get
        {
            return (int)ViewState["BatchId"];
        }
        set
        {
            ViewState["BatchId"] = value;
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
        SaveRule();
        UpdateView();
    }

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveRule();
        TheReportValidationRule = null;
        UpdateView();
    }

    protected void SaveRule()
    {
        if (TheReportValidationRule == null)
        {
            TheReportValidationRule = new ReportValidationRule();
            TheReportValidationRule.CreateUser = CurrentUser;
            TheReportValidationRule.CreateDate = System.DateTime.Now;
            TheReportValidationRule.TheReportBatch = TheService.LoadReportBatch(BatchId);
        }
        TheReportValidationRule.Description = txtRuleDescription.Text;
        TheReportValidationRule.UpdateUser = CurrentUser;
        TheReportValidationRule.UpdateDate = System.DateTime.Now;
        TheReportValidationRule.Name = txtRuleName.Text;
        TheReportValidationRule.Content = txtRuleContent.Text;
        TheReportValidationRule.ResultContent = txtRuleResultContent.Text;
        TheReportValidationRule.UpdateContent = txtUpdateSQLContent.Text;
        TheReportValidationRule.Type = ddlRuleType.SelectedItem.Value;
        TheReportValidationRule.ActiveFlag = 1;

        if (TheReportValidationRule == null || TheReportValidationRule.Id == 0)
        {
            TheRuleService.CreateReportValidationRule(TheReportValidationRule);
        }
        else
        {
            TheRuleService.UpdateReportValidationRule(TheReportValidationRule);
        }
    }

    public void UpdateView()
    {
        txtRuleName.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.Name : "";
        txtRuleDescription.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.Description : "";
        txtRuleContent.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.Content : "";
        txtRuleResultContent.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.ResultContent : "";
        if (TheReportValidationRule != null && TheReportValidationRule.Id != 0)
        {
            ddlRuleType.Text = TheReportValidationRule.Type;
        }
        txtUpdateSQLContent.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.UpdateContent : "";
        lCreateBy.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.CreateUser.UserName : "";
        lCreateDate.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.CreateDate.ToString() : "";
        lLastUpdateBy.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.UpdateUser.UserName : "";
        lLastUpdateDate.Text = (TheReportValidationRule != null && TheReportValidationRule.Id != 0) ? TheReportValidationRule.UpdateDate.ToString() : "";
    }

    //protected void btnInsertParameter_Click(object sender, EventArgs e)
    //{
    //    txtRuleContent.Text = txtRuleContent.Text + Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlParameter.SelectedValue);
    //}
}
