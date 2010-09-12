using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.Mobile;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.MobileControls;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using Dndp.Web;
using log4net;
using Dndp.Service.Dui;
using Dndp.Persistence.Entity.Dui;

public partial class Modules_Dui_DSMaintenance_NewRule : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DSMaintenance");

    //The entity service
    protected IDataSourceMgr TheService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
        }
    }

    public DataSourceRule TheDataSourceRule
    {
        get
        {
            return (DataSourceRule)ViewState["TheDataSourceRule"];
        }
        set
        {
            ViewState["TheDataSourceRule"] = value;
        }
    }

    public void SetDataSourceId(int dsId)
    {
        txtDsId.Value = dsId.ToString();
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
        TheDataSourceRule = null;
        UpdateView();
    }

    protected void SaveRule()
    {
        SessionHelper sessionHelper = new SessionHelper(Page);
        if (TheDataSourceRule == null)
        {
            TheDataSourceRule = new DataSourceRule();
            TheDataSourceRule.CreateBy = sessionHelper.CurrentUser;
            TheDataSourceRule.CreateDate = System.DateTime.Now;
            TheDataSourceRule.TheDataSource = TheService.LoadDataSource(int.Parse(txtDsId.Value));
        }              
        TheDataSourceRule.Description = txtRuleDescription.Text;
        TheDataSourceRule.LastUpdateBy = sessionHelper.CurrentUser;
        TheDataSourceRule.LastUpdateDate = System.DateTime.Now;
        TheDataSourceRule.Name = txtRuleName.Text;
        TheDataSourceRule.RuleContent = txtRuleContent.Text;
        TheDataSourceRule.ResultContent = txtRuleResultContent.Text;
        TheDataSourceRule.UpdateContent = txtUpdateSQLContent.Text;
        TheDataSourceRule.RuleType = ddlRuleType.SelectedItem.Value;
        if (TheDataSourceRule == null || TheDataSourceRule.Id == 0)
        {
            TheService.CreateDataSourceRule(TheDataSourceRule);
        }
        else
        {
            TheService.UpdateDateSourceRule(TheDataSourceRule);
        }
    }

    public void UpdateView()
    {
        txtDsRuleId.Value = (TheDataSourceRule != null && TheDataSourceRule.Id != 0) ? TheDataSourceRule.Id.ToString() : "";
        txtRuleName.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.Name : "";
        txtRuleDescription.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.Description : "";
        txtRuleContent.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.RuleContent : "";
        txtRuleResultContent.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0) ? TheDataSourceRule.ResultContent : "";
        if (TheDataSourceRule != null && TheDataSourceRule.Id != 0)
        {
            ddlRuleType.Text = TheDataSourceRule.RuleType;
        }
        txtUpdateSQLContent.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0) ? TheDataSourceRule.UpdateContent : "";
        lCreateBy.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.CreateBy.UserName : "";
        lCreateDate.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.CreateDate.ToString() : "";
        lLastUpdateBy.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.LastUpdateBy.UserName : "";
        lLastUpdateDate.Text = (TheDataSourceRule != null && TheDataSourceRule.Id != 0 ) ? TheDataSourceRule.LastUpdateDate.ToString() : "";
    }
}
