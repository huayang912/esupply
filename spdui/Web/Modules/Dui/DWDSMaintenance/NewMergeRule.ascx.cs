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
using Dndp.Service.Dui;
using log4net;
using Dndp.Persistence.Entity.Dui;
using Dndp.Web;

public partial class Modules_Dui_DWDSMaintenance_NewMergeRule : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DWDataSourceMaintenance");

    //The entity service
    protected IDWDataSourceMgr TheService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    public DWDataSourceMergeRule TheDWDataSourceMergeRule
    {
        get
        {
            return (DWDataSourceMergeRule)ViewState["TheDWDataSourceMergeRule"];
        }
        set
        {
            ViewState["TheDWDataSourceMergeRule"] = value;
        }
    }

    public int TheDWDataSourceId
    {
        get
        {
            return ViewState["TheDWDataSourceId"] != null ? (int)ViewState["TheDWDataSourceId"] : -1;
        }
        set
        {
            ViewState["TheDWDataSourceId"] = value;
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
        TheDWDataSourceMergeRule = null;
        UpdateView();
    }

    protected void SaveRule()
    {
        SessionHelper sessionHelper = new SessionHelper(Page);
        if (TheDWDataSourceMergeRule == null)
        {
            TheDWDataSourceMergeRule = new DWDataSourceMergeRule();
            TheDWDataSourceMergeRule.CreateBy = sessionHelper.CurrentUser;
            TheDWDataSourceMergeRule.CreateDate = System.DateTime.Now;
            TheDWDataSourceMergeRule.TheDWDataSource = TheService.LoadDWDataSource(TheDWDataSourceId);
        }
        TheDWDataSourceMergeRule.Description = txtRuleDescription.Text;
        TheDWDataSourceMergeRule.LastUpdateBy = sessionHelper.CurrentUser;
        TheDWDataSourceMergeRule.LastUpdateDate = System.DateTime.Now;
        TheDWDataSourceMergeRule.Name = txtRuleName.Text;
        TheDWDataSourceMergeRule.RuleContent = txtRuleContent.Text;
        TheDWDataSourceMergeRule.ResultContent = txtRuleResultContent.Text;
        //TheDWDataSourceMergeRule.UpdateContent = txtUpdateSQLContent.Text;
        TheDWDataSourceMergeRule.RuleType = ddlRuleType.SelectedItem.Value;
        if (ddlDependenceRule.SelectedValue != "0")
        {
            TheDWDataSourceMergeRule.DependenceRule = TheService.LoadDWDataSourceMergeRule(int.Parse(ddlDependenceRule.SelectedValue));
        }
        else
        {
            TheDWDataSourceMergeRule.DependenceRule = null;
        }
        if (TheDWDataSourceMergeRule == null || TheDWDataSourceMergeRule.Id == 0)
        {
            TheService.CreateDWDataSourceMergeRule(TheDWDataSourceMergeRule);
        }
        else
        {
            TheService.UpdateDWDataSourceMergeRule(TheDWDataSourceMergeRule);
        }
    }

    public void UpdateView()
    {
        txtRuleName.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.Name : "";
        txtRuleDescription.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.Description : "";
        txtRuleContent.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.RuleContent : "";
        txtRuleResultContent.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.ResultContent : "";
        if (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0)
        {
            ddlRuleType.Text = TheDWDataSourceMergeRule.RuleType;
        }

        IList newList = new ArrayList();
        newList.Add(new DataSourceRule());
        if (TheDWDataSourceId != -1)
        {
            IList list = TheService.FindDWDataSourceMergeRuleByDWDataSourceId(this.TheDWDataSourceId);
            if (list != null && list.Count > 0)
            {
                foreach (DWDataSourceMergeRule rule in list)
                {
                    if (TheDWDataSourceMergeRule == null || TheDWDataSourceMergeRule.Id != rule.Id)
                    {
                        newList.Add(rule);
                    }
                }
            }
        }
        ddlDependenceRule.DataSource = newList;
        ddlDependenceRule.DataValueField = "Id";
        ddlDependenceRule.DataTextField = "Name";
        ddlDependenceRule.DataBind();

        if (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.DependenceRule != null)
        {
            ddlDependenceRule.SelectedValue = TheDWDataSourceMergeRule.DependenceRule.Id.ToString();
        }

        //txtUpdateSQLContent.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.UpdateContent : "";
        lCreateBy.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.CreateBy.UserName : "";
        lCreateDate.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.CreateDate.ToString() : "";
        lLastUpdateBy.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.LastUpdateBy.UserName : "";
        lLastUpdateDate.Text = (TheDWDataSourceMergeRule != null && TheDWDataSourceMergeRule.Id != 0) ? TheDWDataSourceMergeRule.LastUpdateDate.ToString() : "";
    }
}