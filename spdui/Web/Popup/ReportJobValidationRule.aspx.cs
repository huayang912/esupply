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
using Dndp.Service;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;
using Dndp.Persistence.Entity.Dui;
using Dndp.Web;

public partial class Popup_ReportJobValidationRule : System.Web.UI.Page
{
    public ISession GetService(string serviceName)
    {
        return ServiceLocator.GetService(serviceName) as ISession;
    }

    public ReportJob TheReportJob
    {
        get
        {
            return (ReportJob)ViewState["TheReportJob"];
        }
        set
        {
            ViewState["TheReportJob"] = value;
        }
    }

    //The entity service
    protected IReportJobMgr TheService
    {
        get
        {
            return GetService("ReportJobMgr.service") as IReportJobMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            string validationIds = Request["validationIds"];
            int jobId = int.Parse(Request["jobId"]);
            TheReportJob = TheService.LoadReportJob(jobId);
            TheReportJob.RuleList = TheService.FindValidationResultByIds(validationIds);

            txtValidationIds.Value = validationIds;
            foreach (ReportJobValidationResult vr in TheReportJob.RuleList)
            {
                string vrID = vr.Id.ToString();
                if (validationIds.IndexOf("," + vrID) != -1)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
                }
                else if (validationIds.IndexOf(vrID) != -1)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                }
            }
            gvValidationRule.DataSource = TheReportJob.RuleList;
            gvValidationRule.DataBind();
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

    protected void gvValidationRule_SelectedIndexChanged(object sender, EventArgs e)
    {
    }

    protected void btnInValidation_Click(object sender, EventArgs e)
    {
        //validate current rule;
        for (int i = 0; i < gvValidationRule.Rows.Count; i++)
        {
            GridViewRow row = gvValidationRule.Rows[i];
            ReportJobValidationResult vr = (ReportJobValidationResult)TheReportJob.RuleList[i];
            if (ValidationResult.VALIDATION_STATUS_IN_PROGRESS.Equals(vr.ValidationStatus))
            {
                //validate rule
                ReportJobValidationResult validationResult = TheService.ValidateRule(vr.Id);
                vr.ValidationStatus = null;
                vr.Status = validationResult.Status;
                vr.FailedRowCount = validationResult.FailedRowCount;
                break;
            }
        }

        //
        string validationIds = null;
        for (int i = 0; i < gvValidationRule.Rows.Count; i++)
        {
            GridViewRow row = gvValidationRule.Rows[i];
            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
            if (cbSelect.Visible && cbSelect.Checked)
            {
                ReportJobValidationResult vr = (ReportJobValidationResult)TheReportJob.RuleList[i];
                if (validationIds == null)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                    validationIds = vr.Id.ToString();
                }
                else
                {
                    validationIds += "," + vr.Id;
                }
            }
        }
        txtValidationIds.Value = validationIds;
        gvValidationRule.DataSource = TheReportJob.RuleList;
        gvValidationRule.DataBind();
    }    
}
