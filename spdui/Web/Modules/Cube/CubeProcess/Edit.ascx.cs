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

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;
using Dndp.Utility.CSV;
using System.IO;
using System.Text;

//TODO: Add other using statements here.

public partial class Modules_Cube_CubeProcess_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeProcess");
	
	//The entity service
	protected ICubeProcessMgr TheService
    {
        get
        {
            return GetService("CubeProcessMgr.service") as ICubeProcessMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		//TODO: Add code for Page_Load here.
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        //btnUpdate.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
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

    public bool Editable
    {
        get
        {
            return (bool)ViewState["Editable"];
        }
        set
        {
            ViewState["Editable"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        string processStatus = TheCubeProcess.Status; 

        //update basic information
        txtId.Value = TheCubeProcess.Id.ToString();
        lblCubeDescription.Text = TheCubeProcess.TheCube.Description;
        lblCubeName.Text = TheCubeProcess.TheCube.ProcessCubeName;
        lblProcessServerAddr.Text = TheCubeProcess.TheCube.ProcessServerAddr;
        lblProcessDatabaseName.Text = TheCubeProcess.TheCube.ProcessDatabaseName;
        if (processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSuccess)
            || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSubmit)
            || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessStart))
        {
            lblProcessStartDate.Text = TheCubeProcess.StartTime.ToString();
            lblProcessEndDate.Text = TheCubeProcess.EndTime.ToString();
        }
        else
        {
            lblProcessStartDate.Text = string.Empty;
            lblProcessEndDate.Text = string.Empty;
        }
        lblProcessStatus.Text = TheCubeProcess.Status;
        lblProcessCreateUser.Text = TheCubeProcess.CreateUser.UserName;
        lblProcessCreateDate.Text = TheCubeProcess.CreateDate.ToString();
        txtProcessDescription.Text = TheCubeProcess.Description;

        //update ValidationRule info
        gvErrorValidationRule.DataSource = TheCubeProcess.ErrorCubeProcessValidationResultList;
        gvErrorValidationRule.DataBind();
        gvProblemValidationRule.DataSource = TheCubeProcess.ProblemCubeProcessValidationResultList;
        gvProblemValidationRule.DataBind();
        gvWarningValidationRule.DataSource = TheCubeProcess.WarningCubeProcessValidationResultList;
        gvWarningValidationRule.DataBind();

        //update process parameter info
        gvParameterList.DataSource = TheCubeProcess.CubeProcessParameterList;
        gvParameterList.DataBind();

        //button control             
        //if (processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_WaitingValidate)
        //    || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ValidatedFailed)
        //    || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ValidatedPassed)
        //    || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessCancelled)
        //    || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessFailed))
        //{
        //    btnValidationAll.Visible = true;
        //    btnValidationSelect.Visible = true;
        //}
        //else
        //{
        //    btnValidationAll.Visible = false;
        //    btnValidationSelect.Visible = false;
        //}

        if (processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ValidatedPassed))
        {
            btnProcess.Visible = Editable;
        }
        else
        {
            btnProcess.Visible = false;
        }

        if (processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessFailed)
            || processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessCancelled))
        {
            btnReProcess.Visible = Editable;
        }
        else
        {
            btnReProcess.Visible = false;
        }

        if (!processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSuccess)
            && !processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSubmit)
            && !processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessStart)
            && processStatus.Trim().Length != 0)
        {
            btnDelete.Visible = Editable;
            btnSave.Visible = Editable;
        }
        else
        {
            btnDelete.Visible = false;
            btnSave.Visible = false;
        }

        if (processStatus.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSubmit))
        {
            btnCancel.Visible = Editable;
        }
        else
        {
            btnCancel.Visible = false;
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

    //Event handler when user click button "Save"
    protected void btnSave_Click(object sender, EventArgs e)
    {
        TheCubeProcess.Description = txtProcessDescription.Text.Trim();
        TheService.UpdateCubeProcess(TheCubeProcess);
    }

    //Event handler when user click button "Process"
    protected void btnProcess_Click(object sender, EventArgs e)
    {
        TheCubeProcess.Description = txtProcessDescription.Text.Trim();
        TheCubeProcess.Status = CubeProcess.PROCESS_STATUS_ProcessSubmit;
        TheCubeProcess.ProcessUser = CurrentUser;
        try
        {
            TheService.UpdateCubeProcess(TheCubeProcess);
            UpdateView();
        }
        catch (Exception ex)
        {            
            lblMessage.Text = ex.Message;
        }
    }

    //Event handler when user click button "Re-Process"
    protected void btnReProcess_Click(object sender, EventArgs e)
    {
        TheCubeProcess.Description = txtProcessDescription.Text.Trim();
        TheCubeProcess.Status = CubeProcess.PROCESS_STATUS_ProcessSubmit;
        TheCubeProcess.ProcessUser = CurrentUser;
        try
        {
            TheService.UpdateCubeProcess(TheCubeProcess);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //Event handler when user click button "Cancel"
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TheCubeProcess.Status = CubeProcess.PROCESS_STATUS_ProcessCancelled;
        TheCubeProcess.ProcessUser = CurrentUser;
        try
        {
            TheService.UpdateCubeProcess(TheCubeProcess);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //Event handler when user click button "Delete"
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteCubeProcess(TheCubeProcess.Id);
            if (Back != null)
            {
                Back(this, e);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }

    }

    //Event handler when user click button "Download"
    protected void gvValidationRule_Click(object sender, EventArgs e)
    {
        int validationResultId = Int32.Parse(((LinkButton)sender).CommandArgument);
        downloadValidationResult(validationResultId);
    }

    //Event handler when user click button "ValidationFinish"
    protected void btnValidationFinish_Click(object sender, EventArgs e)
    {
        TheCubeProcess = TheService.FindCubeProcessWithAllInfoById(TheCubeProcess.Id);        
        UpdateView();
    }

    private void downloadValidationResult(int validationResultId)
    {
        CubeProcessValidationResult vr = TheService.LoadCubeProcessValidationResult(validationResultId);
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(TheCubeProcess.TheCube.Description + "_" + vr.TheRule.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_result.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadCubeProcessValidateResult(vr.TheRule.ResultContent, TheCubeProcess.CubeProcessParameterList,csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }
}