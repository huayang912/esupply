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
    public event EventHandler Update;
	
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
        //Reload The TheDataSourceUpload Entity
        TheDataSourceUpload = TheService.LoadDataSourceUpload(TheDataSourceUpload.Id);
        TheDataSourceUpload.ValidationResultList = TheService.FindValidationResultByDataSourceUploadId(TheDataSourceUpload.Id);

        txtName.Text = TheDataSourceUpload.TheDataSourceCategory.TheDataSource.Name;
        txtCategory.Text = TheDataSourceUpload.TheDataSourceCategory.Name;
        txtSubject.Text = TheDataSourceUpload.Name;
        txtStatus.Text = TheDataSourceUpload.ProcessStatus;
        txtFileNm.Text = TheDataSourceUpload.UploadFileOriginName;
        txtSize.Text = TheDataSourceUpload.UploadFileLength.ToString();
        txtId.Value = TheDataSourceUpload.Id.ToString();
        txtRecordRows.Text = TheDataSourceUpload.RecordRows.ToString();
        txtUploadBy.Text = TheDataSourceUpload.UploadBy.UserName;
        txtUploadDate.Text = TheDataSourceUpload.UploadDate.ToString();

        if (TheDataSourceUpload.IsInValidation)
        {
            btnConfirm.Visible = false;
            btnDelete.Visible = false;
            btnBack.Visible = false;
            lblMessage.Visible = true;                        
        }
        else
        {
            if (TheDataSourceUpload.ProcessStatus.Equals(""))
            {
                if (TheDataSourceUpload.Errors == 0)
                {
                    btnConfirm.Visible = true;
                }
                else
                {
                    btnConfirm.Visible = false;
                }
                btnUnConfirm.Visible = false;
                btnDelete.Visible = true;
            } else if (TheDataSourceUpload.ProcessStatus.Equals("OWNER_CONFIRMED"))
            {
                btnDelete.Visible = false;
                btnConfirm.Visible = false;
                btnUnConfirm.Visible = true;
            }
            else
            {
                btnUnConfirm.Visible = false;
                btnDelete.Visible = false;
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

	//Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {        
        if (Back != null)
        {
            Back(this, e);
        }          
    }

    protected void btnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(txtId.Value);
        TheService.ConfirmDataSourceUpload(dsUploadId);
        Back(this, e);
    }

    protected void btnUnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(txtId.Value);
        TheService.UnconfirmDataSourceUpload(dsUploadId);
        Back(this, e);
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        TheDataSourceUpload.Name = txtSubject.Text;
        TheService.UpdateDataSourceUpload(TheDataSourceUpload);

        UpdateView();
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(txtId.Value);
        TheService.DeleteDataSourceUploadAndUploadedData(dsUploadId);
        Back(this, e);
    }

    protected void btnInValidation_Click(object sender, EventArgs e)
    {
        TheDataSourceUpload.ValidationResultList = TheService.FindValidationResultByDataSourceUploadId(TheDataSourceUpload.Id);
        UpdateView();
    }

    protected void gvValidationRule_Click(object sender, EventArgs e)
    {
        int validationResultId = Int32.Parse(((LinkButton)sender).CommandArgument);
        downloadValidationResult(validationResultId);
    }
    
    protected void gvValidationRuleUpdate_Click(object sender, EventArgs e)
    {
        int validationResultId = Int32.Parse(((LinkButton)sender).CommandArgument);
        ValidationResult vr = TheService.LoadValidationResult(validationResultId);
        if (Update != null)
        {
            TheValidationResult = vr;
            Update(this, null);
        }
    }

    public ValidationResult TheValidationResult
    {
        get
        {
            return (ValidationResult)ViewState["TheValidationResult"];
        }
        set
        {
            ViewState["TheValidationResult"] = value;
        }
    }

    private void downloadValidationResult(int validationResultId)
    {
        ValidationResult vr = TheService.LoadValidationResult(validationResultId);
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(TheDataSourceUpload.Name + "_" + vr.TheDataSourceRule.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_result.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
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
        string fileName = HttpUtility.UrlEncode(TheDataSourceUpload.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_Download.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadUploadData(TheDataSourceUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }
}