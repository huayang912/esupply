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
using Dndp.Persistence.Entity.Dui;
using Dndp.Web;
using Dndp.Service;

public partial class Popup_DWMergeValidationRule : System.Web.UI.Page
{
    public ISession GetService(string serviceName)
    {
        return ServiceLocator.GetService(serviceName) as ISession;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string validationIds = Request["validationIds"];
            this.MergeFromId = Request["MergeFromId"];
            this.MergeToId = Request["MergeToId"];
            this.RuleList = TheService.FindDWDataSourceMergeRuleByIds(validationIds);

            txtValidationIds.Value = validationIds;
            foreach (DWDataSourceMergeRule rule in this.RuleList)
            {
                string ruleId = rule.Id.ToString();
                if (validationIds.IndexOf("," + ruleId) != -1)
                {
                    rule.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
                }
                else if (validationIds.IndexOf(ruleId) != -1)
                {
                    rule.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                }
            }
            gvValidationRule.DataSource = this.RuleList;
            gvValidationRule.DataBind();
        }
    }

    private IList RuleList
    {
        get
        {
            return (IList)ViewState["RuleList"];
        }
        set
        {
            ViewState["RuleList"] = value;
        }
    }

    private string MergeFromId
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

    private string MergeToId
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

    //The entity service
    protected IDWDataSourceMgr TheService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    protected bool IsChecked(string ValidationResultId)
    {
        string[] validationIds = txtValidationIds.Value.Split(',');
        for (int i = 0; i < validationIds.Length; i++)
        {
            if (validationIds[i].Equals(ValidationResultId))
            {
                return true;
            }
        }

        return false;
    }

    protected void btnInValidation_Click(object sender, EventArgs e)
    {
        //validate current rule;
        for (int i = 0; i < gvValidationRule.Rows.Count; i++)
        {
            GridViewRow row = gvValidationRule.Rows[i];
            DWDataSourceMergeRule rule = (DWDataSourceMergeRule)RuleList[i];
            if (DWDataSourceMergeRule.VALIDATION_STATUS_IN_PROGRESS.Equals(rule.ValidationStatus))
            {
                //validate rule
                string result = TheService.ValidateMergeRule(rule, this.MergeFromId, this.MergeToId, (new SessionHelper(Page)).CurrentUser);
                if (txtValidationResults.Value == string.Empty)
                {
                    txtValidationResults.Value = result;
                }
                else
                {
                    txtValidationResults.Value += ";" + result;
                }
                break;
            }
        }

        string validationIds = null;
        for (int i = 0; i < gvValidationRule.Rows.Count; i++)
        {
            GridViewRow row = gvValidationRule.Rows[i];
            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
            if (cbSelect.Visible && cbSelect.Checked)
            {
                DWDataSourceMergeRule rule = (DWDataSourceMergeRule)RuleList[i];
                if (validationIds == null)
                {
                    rule.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                    validationIds = rule.Id.ToString();
                }
                else
                {
                    validationIds += "," + rule.Id;
                }
            }
        }
        txtValidationIds.Value = validationIds;
        gvValidationRule.DataSource = RuleList;
        gvValidationRule.DataBind();
    }
}
