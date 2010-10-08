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
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Dui;
using Dndp.Web;
using Dndp.Service;
using Dndp.Service.Cube;
using Dndp.Persistence.Entity.Cube;

public partial class Popup_ProcessValidationRule : System.Web.UI.Page
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
            int processId = int.Parse(Request["processId"]);
            TheCubeProcess = TheService.FindCubeProcessWithAllInfoById(processId);
            TheCubeProcess.CubeProcessValidationResultList = TheService.FindCubeProcessValidationResultByIds(validationIds);

            txtValidationIds.Value = validationIds;
            foreach (CubeProcessValidationResult vr in TheCubeProcess.CubeProcessValidationResultList)
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
            gvValidationRule.DataSource = TheCubeProcess.CubeProcessValidationResultList;
            gvValidationRule.DataBind();
        }        
    }

    public CubeProcess TheCubeProcess
    {
        get
        {
            return (CubeProcess)ViewState["TheCubeProcess"];
        }
        set
        {
            ViewState["TheCubeProcess"] = value;
        }
    }

    //The entity service
    protected ICubeProcessMgr TheService
    {
        get
        {
            return GetService("CubeProcessMgr.service") as ICubeProcessMgr;
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
            CubeProcessValidationResult vr = TheCubeProcess.CubeProcessValidationResultList[i];
            if (ValidationResult.VALIDATION_STATUS_IN_PROGRESS.Equals(vr.ValidationStatus))
            {
                //validate rule
                CubeProcessValidationResult validationResult = TheService.ValidateCubeProcessRule(vr.Id, TheCubeProcess.CubeProcessParameterList, (new SessionHelper(Page)).CurrentUser);
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
                CubeProcessValidationResult vr = TheCubeProcess.CubeProcessValidationResultList[i];
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
        gvValidationRule.DataSource = TheCubeProcess.CubeProcessValidationResultList;
        gvValidationRule.DataBind();        
    }    
}
