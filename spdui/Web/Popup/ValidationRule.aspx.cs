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

public partial class Popup_ValidationRule : System.Web.UI.Page
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
            int dsUploadId = int.Parse(Request["dsUploadId"]);
            TheDataSourceUpload = TheService.LoadDataSourceUpload(dsUploadId);
            TheDataSourceUpload.ValidationResultList = TheService.FindValidationResultByIds(validationIds);

            txtValidationIds.Value = validationIds;
            foreach (ValidationResult vr in TheDataSourceUpload.ValidationResultList)
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
            gvValidationRule.DataSource = TheDataSourceUpload.ValidationResultList;
            gvValidationRule.DataBind();
        }        
    }

    public DataSourceUpload TheDataSourceUpload
    {
        get
        {
            return (DataSourceUpload)ViewState["TheDataSourceUpload"];
        }
        set
        {
            ViewState["TheDataSourceUpload"] = value;
        }
    }

    //The entity service
    protected IDataSourceMgr TheService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
        }
    }

    protected IDataSourceUploadMgr TheUploadService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
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
            ValidationResult vr = TheDataSourceUpload.ValidationResultList[i];
            if (ValidationResult.VALIDATION_STATUS_IN_PROGRESS.Equals(vr.ValidationStatus))
            {
                //validate rule
                ValidationResult validationResult = TheUploadService.ValidateRule(vr.Id, (new SessionHelper(Page)).CurrentUser);
                vr.ValidationStatus = null;
                vr.Status = validationResult.Status;
                vr.FaildRowCount = validationResult.FaildRowCount;
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
                ValidationResult vr = TheDataSourceUpload.ValidationResultList[i];
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
        gvValidationRule.DataSource = TheDataSourceUpload.ValidationResultList;
        gvValidationRule.DataBind();        
    }    
}
