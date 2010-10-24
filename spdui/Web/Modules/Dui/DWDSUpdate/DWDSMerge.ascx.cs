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
using Dndp.Service.Dui;
using Dndp.Persistence.Entity.Dui;
using System.Collections.Generic;

public partial class Modules_Dui_DWDSUpdate_DWDSMerge : ModuleBase
{
    public event EventHandler Back;

    protected int DWDataSourceId
    {
        get
        {
            return ViewState["DWDataSourceId"] != null ? (int)ViewState["DWDataSourceId"] : 0;
        }
        set
        {
            ViewState["DWDataSourceId"] = value;
        }
    }

    protected string MergeFromId
    {
        get
        {
            return (string)ViewState["MergeFromId"];
        }
        set
        {
            ViewState["MergeFromId"] = value;
        }
    }

    protected string MergeToId
    {
        get
        {
            return (string)ViewState["MergeToId"];
        }
        set
        {
            ViewState["MergeToId"] = value;
        }
    }

    private bool ValidationPass
    {
        get
        {
            return ViewState["ValidationPass"] != null ? (bool)ViewState["ValidationPass"] : false;
        }
        set
        {
            ViewState["ValidationPass"] = value;
        }
    }

    private IDictionary<int, string> ValidationResult
    {
        get
        {
            return (IDictionary<int, string>)ViewState["ValidationResult"];
        }
        set
        {
            ViewState["ValidationResult"] = value;
        }
    }

    //The entity service
    protected IDWDataSourceMgr TheService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    public void init(int dwDSId)
    {
        CleanAll();
        this.DWDataSourceId = dwDSId;
        DWDataSource dwDataSource = this.TheService.LoadDWDataSource(this.DWDataSourceId);
        this.title.InnerText = "Merge master data for '" + dwDataSource.Name + "'";
    }

    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        CleanAll();
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void btnQuery_Click(object sender, EventArgs e)
    {
        this.ValidationPass = false;
        this.btnMerge.Visible = false;
        this.ValidationResult = null;
        this.lblMessage.Text = string.Empty;
        this.lblMessage.Visible = false;

        if (this.txtMergeFromId.Text.Trim() == string.Empty)
        {
            this.lblMessage.Text = "To-be merged Record ID can't be empty";
            this.lblMessage.Visible = true;
            return;
        }
        else
        {
            this.MergeFromId = this.txtMergeFromId.Text.Trim();
        }

        if (this.txtMergeToId.Text.Trim() == string.Empty)
        {
            this.lblMessage.Text = "Merged to Record ID";
            this.lblMessage.Visible = true;
            return;
        }
        else
        {
            this.MergeToId = this.txtMergeToId.Text.Trim();
        }

        if (this.MergeFromId == this.MergeToId)
        {
            this.lblMessage.Text = "To-be merged Record ID can't equal Merged to Record ID";
            this.lblMessage.Visible = true;
            return;
        }

        DataSet ds = this.TheService.FindMergeRecords(this.DWDataSourceId, this.txtMergeFromId.Text.Trim(), this.txtMergeToId.Text.Trim());
        if (ds.Tables[0].Rows.Count != 2)
        {
            this.lblMessage.Text = "The Record ID you input is not correct.";
            this.lblMessage.Visible = true;
            return;
        }

        this.title2.Visible = true;
        this.title3.Visible = true;
        this.hr1.Visible = true;
        this.hr2.Visible = true;
        this.gvDWDSMerge.DataSource = ds;
        this.gvDWDSMerge.DataBind();

        showRules();
    }

    protected void btnMerge_Click(object sender, EventArgs e)
    {

        try
        {
            this.TheService.MergeDWData(this.DWDataSourceId, this.MergeFromId, this.MergeToId, this.CurrentUser);
            this.lblMessage.Text = "Data successfully merged.";
            this.lblMessage.Visible = true;
            CleanAll();
        }
        catch (Exception ex)
        {
            this.lblMessage.Text = ex.Message;
            this.lblMessage.Visible = true;
        }

    }

    protected void btnInValidation_Click(object sender, EventArgs e)
    {
        if (this.hfValidationResult.Value != string.Empty)
        {
            string[] vrs = this.hfValidationResult.Value.Split(';');
            foreach (string vr in vrs)
            {
                string[] v = vr.Split(':');
                int ruleId = int.Parse(v[0]);
                string ruleResult = v[1];

                this.ValidationResult[ruleId] = ruleResult;
            }

            showRules();
        }

        this.hfValidationResult.Value = string.Empty;
    }

    protected void gvDWDSMerge_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            //LinkButton ltnDelete = (LinkButton)e.Row.Cells[0].Controls[0];
            //if (ltnDelete.Text.Equals("Delete"))
            //{
            //    ltnDelete.Attributes.Add("onclick", "javascript:return ButtonWarning('Delete')");
            //}
        }
    }

    private void showRules()
    {
        IList ruleList = TheService.FindDWDataSourceMergeRuleByDWDataSourceId(this.DWDataSourceId);

        if (ruleList != null && ruleList.Count > 0)
        {
            IList errorRuleList = FilterRules(ruleList, "Error");
            checkShowMerge(errorRuleList);
            this.gvErrorValidationRule.DataSource = errorRuleList;
            this.gvErrorValidationRule.DataBind();
            this.gvWarningValidationRule.DataSource = FilterRules(ruleList, "Warning");
            this.gvWarningValidationRule.DataBind();
            this.gvProblemValidationRule.DataSource = FilterRules(ruleList, "Problem");
            this.gvProblemValidationRule.DataBind();
        }
        else
        {
            this.btnMerge.Visible = true;
        }
    }

    private void checkShowMerge(IList errorRuleList)
    {

        if (errorRuleList != null && errorRuleList.Count > 0)
        {
            bool passed = true;
            foreach (DWDataSourceMergeRule rule in errorRuleList)
            {
                if (rule.Status == null || rule.Status.ToLower() != "passed")
                {
                    passed = false;
                    break;
                }
            }

            if (passed)
            {
                this.btnMerge.Visible = true;
            }
            else
            {
                this.btnMerge.Visible = false;
            }
        }
        else
        {
            this.btnMerge.Visible = true;
        }
    }

    private IList FilterRules(IList list, string ruleType)
    {
        if (list != null && list.Count > 0)
        {
            IList resultList = new ArrayList();
            foreach (DWDataSourceMergeRule rule in list)
            {
                if (rule.RuleType.ToLower() == ruleType.ToLower())
                {
                    if (this.ValidationResult == null)
                    {
                        this.ValidationResult = new Dictionary<int, string>();
                    }

                    if (this.ValidationResult.ContainsKey(rule.Id))
                    {
                        rule.Status = this.ValidationResult[rule.Id];
                    }
                    else
                    {
                        this.ValidationResult.Add(rule.Id, "");
                    }

                    resultList.Add(rule);
                }
            }

            return resultList;
        }
        else
        {
            return null;
        }
    }

    private void CleanAll()
    {
        this.title2.Visible = false;
        this.title3.Visible = false;
        this.hr1.Visible = false;
        this.hr2.Visible = false;
        this.ValidationPass = false;
        this.btnMerge.Visible = false;
        this.ValidationResult = null;
        this.txtMergeFromId.Text = string.Empty;
        this.txtMergeToId.Text = string.Empty;
        this.gvDWDSMerge.DataSource = null;
        this.gvDWDSMerge.DataBind();
        this.gvErrorValidationRule.DataSource = null;
        this.gvErrorValidationRule.DataBind();
        this.gvWarningValidationRule.DataSource = null;
        this.gvWarningValidationRule.DataBind();
        this.gvProblemValidationRule.DataSource = null;
        this.gvProblemValidationRule.DataBind();
        this.MergeFromId = null;
        this.MergeToId = null;
        this.hfValidationResult.Value = string.Empty;
    }
}
