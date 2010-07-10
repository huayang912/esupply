using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.Dui;
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Dui;
using System.Text;
using System.IO;
using Dndp.Utility.CSV;

//TODO: Add other using statements here.

public partial class Modules_Dui_DSUpload_Validate : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
    private static ILog log = LogManager.GetLogger("DSUpload");
	
	//The entity service
	protected IDataSourceUploadMgr TheService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		//TODO: Add code for Page_Load here.
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

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {        
        txtName.Text = TheDataSourceUpload.TheDataSourceCategory.TheDataSource.Name;
        txtCategory.Text = TheDataSourceUpload.TheDataSourceCategory.Name;
        txtSubject.Text = TheDataSourceUpload.Name;
        txtStatus.Text = TheDataSourceUpload.ProcessStatus;
        txtFileNm.Text = TheDataSourceUpload.UploadFileOriginName;
        txtSize.Text = TheDataSourceUpload.UploadFileLength.ToString();
        txtId.Value = TheDataSourceUpload.Id.ToString();
        if (TheDataSourceUpload.IsInValidation)
        {
            //btnValidateAll.Visible = false;
            //btnValidateSelected.Visible = false;
            btnConfirm.Visible = false;
            btnBack.Visible = false;
            lblMessage.Visible = true;                        
        }
        else
        {
            if (TheDataSourceUpload.ProcessStatus.Equals("OWNER_CONFIRMED"))
            {
                btnConfirm.Visible = true;
                btnUnConfirm.Visible = false;
            }
            else
            {
                btnConfirm.Visible = false;
                btnUnConfirm.Visible = true;
            }
            btnBack.Visible = true;
            lblMessage.Visible = false;            
        }

        gvErrorValidationRule.DataSource = TheDataSourceUpload.ErrorValidationResultList;
        gvErrorValidationRule.DataBind();
        gvProblemValidationRule.DataSource = TheDataSourceUpload.ProblemValidationResultList;
        gvProblemValidationRule.DataBind();
        gvWarningValidationRule.DataSource = TheDataSourceUpload.WarningValidationResultList;
        gvWarningValidationRule.DataBind();
    }
    /*
    public void ValidateAll()
    {
        StringBuilder validationIds = null;
        foreach (ValidationResult vr in TheDataSourceUpload.ErrorValidationResultList)
        {
            if (validationIds == null)
            {
                validationIds = new StringBuilder();
                validationIds.Append(vr.Id);
                vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
            }
            else
            {
                validationIds.Append("," + vr.Id);
                vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
            }
        }

        foreach (ValidationResult vr in TheDataSourceUpload.ProblemValidationResultList)
        {
            if (validationIds == null)
            {
                validationIds = new StringBuilder();
                validationIds.Append(vr.Id);
                vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
            }
            else
            {
                validationIds.Append("," + vr.Id);
                vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
            }
        }

        foreach (ValidationResult vr in TheDataSourceUpload.WarningValidationResultList)
        {
            if (validationIds == null)
            {
                validationIds = new StringBuilder();
                validationIds.Append(vr.Id);
                vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
            }
            else
            {
                validationIds.Append("," + vr.Id);
                vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
            }
        }

        //change dataupload        
        if (validationIds != null)
        {
            string strValidationIds = validationIds.ToString();
            txtValidationIds.Value = strValidationIds;
            TheDataSourceUpload.IsInValidation = true;            
        }

        UpdateView();
    }
    */
	//Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {        
        if (Back != null)
        {
            Back(this, e);
        }          
    }
    /*
    protected void btnValidateAll_Click(object sender, EventArgs e)
    {
        //loop rule for validation
        StringBuilder validationIds = null;
        foreach (GridViewRow row in gvErrorValidationRule.Rows)
        {
            if (validationIds == null)
            {
                validationIds = new StringBuilder();
                validationIds.Append(gvErrorValidationRule.DataKeys[row.RowIndex].Value);
            }
            else
            {
                validationIds.Append("," + gvErrorValidationRule.DataKeys[row.RowIndex].Value);
            }
        }

        foreach (GridViewRow row in gvProblemValidationRule.Rows)
        {
            if (validationIds == null)
            {
                validationIds = new StringBuilder();
                validationIds.Append(gvProblemValidationRule.DataKeys[row.RowIndex].Value);
            }
            else
            {
                validationIds.Append("," + gvProblemValidationRule.DataKeys[row.RowIndex].Value);
            }
        }

        foreach (GridViewRow row in gvWarningValidationRule.Rows)
        {
            if (validationIds == null)
            {
                validationIds = new StringBuilder();
                validationIds.Append(gvWarningValidationRule.DataKeys[row.RowIndex].Value);
            }
            else
            {
                validationIds.Append("," + gvWarningValidationRule.DataKeys[row.RowIndex].Value);
            }
        }

        //change dataupload        
        if (validationIds != null) 
        {
            string strValidationIds = validationIds.ToString();
            txtValidationIds.Value = strValidationIds;
            TheDataSourceUpload.IsInValidation = true;            
            foreach (ValidationResult vr in TheDataSourceUpload.ValidationResultList)
            {
                string vrID = vr.Id.ToString();
                if (strValidationIds.IndexOf(","+ vrID) != -1)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
                }
                else if (strValidationIds.IndexOf(vrID) != -1)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                }                 
            }
        }

        UpdateView();        
    }

    protected void btnValidateSelected_Click(object sender, EventArgs e)
    {
        //loop rule for validation
        StringBuilder validationIds = null;
        foreach (GridViewRow row in gvErrorValidationRule.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbErrorSelect");
            if (cbSelect.Checked)
            {
                if (validationIds == null)
                {
                    validationIds = new StringBuilder();
                    validationIds.Append(gvErrorValidationRule.DataKeys[row.RowIndex].Value);
                }
                else
                {
                    validationIds.Append("," + gvErrorValidationRule.DataKeys[row.RowIndex].Value);
                }
            }
        }

        foreach (GridViewRow row in gvProblemValidationRule.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbProblemSelect");
            if (cbSelect.Checked)
            {
                if (validationIds == null)
                {
                    validationIds = new StringBuilder();
                    validationIds.Append(gvProblemValidationRule.DataKeys[row.RowIndex].Value);
                }
                else
                {
                    validationIds.Append("," + gvProblemValidationRule.DataKeys[row.RowIndex].Value);
                }
            }
        }

        foreach (GridViewRow row in gvWarningValidationRule.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbWarningSelect");
            if (cbSelect.Checked)
            {
                if (validationIds == null)
                {
                    validationIds = new StringBuilder();
                    validationIds.Append(gvWarningValidationRule.DataKeys[row.RowIndex].Value);
                }
                else
                {
                    validationIds.Append("," + gvWarningValidationRule.DataKeys[row.RowIndex].Value);
                }
            }
        }
       
        //change dataupload        
        if (validationIds != null)
        {
            string strValidationIds = validationIds.ToString();
            txtValidationIds.Value = strValidationIds;
            TheDataSourceUpload.IsInValidation = true;
            foreach (ValidationResult vr in TheDataSourceUpload.ValidationResultList)
            {
                string vrID = vr.Id.ToString();
                if (strValidationIds.IndexOf("," + vrID) != -1)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_WAITING;
                }
                else if (strValidationIds.IndexOf(vrID) != -1)
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                }
            }
        }

        UpdateView();        
    }
    */
    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(txtId.Value);
        TheService.ETLConfirmDataSourceUpload(dsUploadId);
        Back(this, e);
    }

    protected void btnUnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(txtId.Value);
        TheService.ETLUnconfirmDataSourceUpload(dsUploadId);
        Back(this, e);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(txtId.Value);
        TheService.DeleteDataSourceUploadAndUploadedData(dsUploadId);
        Back(this, e);
    }

    protected void btnInValidation_Click(object sender, EventArgs e)
    {
        string strValidationIds = txtValidationIds.Value;
        if (strValidationIds != null && strValidationIds.Trim().Length > 0)
        {
            //seperate the validation result id for "in progress" and "waiting"
            int nowValidationId = 0;
            int nextValidationId = 0;
            string pendingValidationIds = "";
            string[] ids = strValidationIds.Split(',');
            nowValidationId = int.Parse(ids[0]);
            if (ids.Length > 1)
            {
                nextValidationId = int.Parse(ids[1]);
                pendingValidationIds = strValidationIds.Substring(strValidationIds.IndexOf(",") + 1);
            }
            else
            {
                TheDataSourceUpload.IsInValidation = false;
            }

            //validate rule
            ValidationResult validationResult = TheService.ValidateRule(nowValidationId);

            //update status            
            foreach (ValidationResult vr in TheDataSourceUpload.ValidationResultList)
            {
                if (nowValidationId == vr.Id)
                {
                    vr.ValidationStatus = null;
                    vr.Status = validationResult.Status;
                    vr.FaildRowCount = validationResult.FaildRowCount;
                }
                else if (nextValidationId == vr.Id) 
                {
                    vr.ValidationStatus = ValidationResult.VALIDATION_STATUS_IN_PROGRESS;
                }
            }

            txtValidationIds.Value = pendingValidationIds;
        }
        else
        {
            TheDataSourceUpload.IsInValidation = false;
        }
        
        UpdateView();
    }
    protected void gvErrorValidationRule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int validationResultId = (int)(gvErrorValidationRule.SelectedDataKey.Value);
        downloadValidationResult(validationResultId);
    }
    protected void gvProblemValidationRule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int validationResultId = (int)(gvProblemValidationRule.SelectedDataKey.Value);
        downloadValidationResult(validationResultId);
    }
    protected void gvWarningValidationRule_SelectedIndexChanged(object sender, EventArgs e)
    {
        int validationResultId = (int)(gvWarningValidationRule.SelectedDataKey.Value);
        downloadValidationResult(validationResultId);
    }

    private void downloadValidationResult(int validationResultId)
    {
        ValidationResult vr = TheService.LoadValidationResult(validationResultId);
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + vr.TheDataSourceRule.Name + "_result.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream);
        CSVWriter csvWriter = new CSVWriter(txtWriter);;
        TheService.DownloadValidateResult(vr, csvWriter);
        txtWriter.Flush();   
        Response.End();

        UpdateView();
    }

    protected void btnDownload_Click(object sender, EventArgs e)
    {
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + TheDataSourceUpload.Name + "_Download.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadUploadData(TheDataSourceUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }
}