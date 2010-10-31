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

using System.Collections.Generic;

using Dndp.Web;
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;

using System.Data.SqlClient;
using System.IO;
using System.Text;
using Dndp.Utility.CSV;

//TODO: Add other using statements here.

public partial class Modules_OffLineReport_JobExecution_Edit : ModuleBase
{
    public event EventHandler Back;

    private const string invalidCharForFolder = "~ \" # % & * : < > ? { | }.¡£";
    private string[] invalidStringForFolder = new string[] {"\\\\", "//"};

    //Get the logger
    private static ILog log = LogManager.GetLogger("JobExecution");

    //The entity service
    protected IReportJobMgr TheService
    {
        get
        {
            return GetService("ReportJobMgr.service") as IReportJobMgr;
        }
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

    public IList<ReportUserSheet> TheReportUserSheet
    {
        get
        {
            return (IList<ReportUserSheet>)ViewState["TheReportUserSheet"];
        }
        set
        {
            ViewState["TheReportUserSheet"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            //UpdateView();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewJobReport1.Back += new EventHandler(NewJobReport1_Back);
        NewJobUser1.Back += new EventHandler(NewJobUser1_Back);
    }

    void NewJobReport1_Back(object sender, EventArgs e)
    {
        NewJobReport1.Visible = false;
        TheReportJob.ReportList = TheService.FindReportByJobId(TheReportJob.Id);
        pnlMain.Visible = true;
        UpdateView();
    }

    void NewJobUser1_Back(object sender, EventArgs e)
    {
        NewJobUser1.Visible = false;
        TheReportJob.UserList = TheService.FindUserByJobId(TheReportJob.Id);
        pnlMain.Visible = true;
        UpdateView();
    }

    protected void btnSave_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            SaveData();
            UpdateView();
        }        
    }
    

    //The event handler when user click button "Submit"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            SubmitData();
            UpdateView();
        }                
    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {
        TheService.CancelReportJob(TheReportJob, CurrentUser);
        UpdateView();
    }

    protected void btnRestart_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            SaveData();
            TheService.RestartReportJob(TheReportJob, this.CurrentUser);
            UpdateView();
        }           
    }

    private bool CheckInput()
    {
        //if (rdoNeedUploadYes.Checked && txtUploadFolder.Text.Trim().Length == 0)
        //{
        //    lblMessage.Text = "Uploading Folder must be enter when selecting upload to portal";
        //    lblMessage.Visible = true;
        //    return false;
        //}

        foreach (string invalidStr in invalidStringForFolder)
        {
            if (txtUploadFolder.Text.Contains(invalidStr))
            {
                lblMessage.Text = "Uploading Folder can't contains double \\ or double /";
                lblMessage.Visible = true;
                return false;
            }
        }

        char[] invalidChars = invalidCharForFolder.Replace(" ","").ToCharArray();
        foreach (char invalidChar in invalidChars)
        {
            if (txtUploadFolder.Text.Contains(invalidChar.ToString()))
            {
                lblMessage.Text = "Uploading Folder can't contains " + invalidCharForFolder;
                lblMessage.Visible = true;
                return false;
            }
        }

        if (txtStartTime.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Start Time can not be empty";
            lblMessage.Visible = true;
            return false;
        }
        else
        {
            if (txtReportDate.Text.Trim().Length == 0)
            {
                lblMessage.Text = "Report Date can not be empty";
                lblMessage.Visible = true;
                return false;
            }
            else
            {
                return true;
            }
        }
    }


    private void SynchronizeData()
    {
        lblMessage.Visible = true;
        SessionHelper sessionHelper = new SessionHelper(Page);
        TheReportJob.StartTime = Convert.ToDateTime(txtStartTime.Text.Trim());
        TheReportJob.ReportDate = Convert.ToDateTime(txtReportDate.Text.Trim());
        TheReportJob.NeedSendMail = rdoSendMailYes.Checked ? "YES" : "NO";

        TheReportJob.EmailBody = txtBody.Text.Trim();
        TheReportJob.EMailSubject = txtSubject.Text.Trim();

        TheReportJob.JobDescription = txtRptDesc.Text.Trim();

        TheReportJob.AppendDateToFileName = rdoAppendDateYes.Checked ? "YES" : "NO";
        TheReportJob.RunPreSQL = rdoRunPreSQLYes.Checked ? "YES" : "NO";

        TheReportJob.AppendUserNameToFileName = rdoAppendUserNameYes.Checked ? "YES" : "NO";
        TheReportJob.NeedCreateSubFolder = rdoCreateSubFolderYes.Checked ? "YES" : "NO";
        TheReportJob.NeedUploadToPortal = rdoNeedUploadYes.Checked ? "YES" : "NO";
        TheReportJob.UploadFolder = txtUploadFolder.Text.Trim();

        TheReportJob.UpdateDate = DateTime.Now;
        TheReportJob.UpdateUser = CurrentUser;
    }

    private void SaveData()
    {

        SynchronizeData();

        try
        {
            TheService.UpdateReportJob(TheReportJob);
            lblMessage.Text = "Data Update Successful";
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    private void SubmitData()
    {

        SynchronizeData();

        try
        {
            TheReportJob.Status = ReportJob.REPORT_JOB_STATUS_SUBMIT;
            TheService.UpdateReportJob(TheReportJob);
            lblMessage.Text = "Data Submit Successful";
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The public method to clear the view
    public void UpdateView()
    {
        txtId.Value = TheReportJob.Id.ToString();
        lblBatchName.Text = TheReportJob.TheBatch.Name;
        lblValidateStatus.Text = TheReportJob.ValidateStatus;
        lblStatus.Text = TheReportJob.Status;
        txtStartTime.Text = TheReportJob.StartTime.ToString();
        lblEndTime.Text = (TheReportJob.EndTime == null) ? "" : TheReportJob.EndTime.ToString();

        rdoSendMailNo.Checked = (TheReportJob.NeedSendMail.Equals("NO"));
        rdoSendMailYes.Checked = !rdoSendMailNo.Checked;

        rdoAppendDateNo.Checked = (TheReportJob.AppendDateToFileName.Equals("NO"));
        rdoAppendDateYes.Checked = !rdoAppendDateNo.Checked;

        rdoRunPreSQLNo.Checked = (TheReportJob.RunPreSQL.Equals("NO"));
        rdoRunPreSQLYes.Checked = !rdoRunPreSQLNo.Checked;

        txtBody.Text = TheReportJob.EmailBody;
        txtSubject.Text = TheReportJob.EMailSubject;

        txtRptDesc.Text = TheReportJob.JobDescription;
        
        txtReportDate.Text = TheReportJob.ReportDate.ToString();

        rdoAppendUserNameNo.Checked = (TheReportJob.AppendUserNameToFileName.Equals("NO"));
        rdoAppendUserNameYes.Checked = !rdoAppendUserNameNo.Checked;

        rdoCreateSubFolderNo.Checked = (TheReportJob.NeedCreateSubFolder.Equals("NO"));
        rdoCreateSubFolderYes.Checked = !rdoCreateSubFolderNo.Checked;

        rdoNeedUploadNo.Checked = (TheReportJob.NeedUploadToPortal.Equals("NO"));
        rdoNeedUploadYes.Checked = !rdoNeedUploadNo.Checked;

        txtUploadFolder.Text = TheReportJob.UploadFolder;

        lblCreateDate.Text = TheReportJob.CreateDate.ToString();
        lblCreateUser.Text = TheReportJob.CreateUser.UserName;

        lblUpdateDate.Text = TheReportJob.UpdateDate.ToString();
        lblUpdateUser.Text = TheReportJob.UpdateUser.UserName;

        if (TheReportJob.Status.Equals(ReportJob.REPORT_JOB_STATUS_SUBMIT))
        {
            btnCancel.Visible = true;
            btnSave.Visible = false;
        }
        else
        {
            btnCancel.Visible = false;
            btnSave.Visible = true;
        }

        if (TheReportJob.Status.Equals(ReportJob.REPORT_JOB_STATUS_PENDING))
        {
            btnSubmit.Visible = true;
        }
        else
        {
            btnSubmit.Visible = false;
        }

        if (TheReportJob.Status.Equals(ReportJob.REPORT_JOB_STATUS_FAILED)
            || TheReportJob.Status.Equals(ReportJob.REPORT_JOB_STATUS_CANCEL))
        {
            btnRestart.Visible = true;
        }
        else
        {
            btnRestart.Visible = false;
        }

        gvReportList.DataSource = TheReportJob.ReportList;
        gvReportList.DataBind();

        gvUserList.DataSource = TheReportJob.UserList;
        gvUserList.DataBind();

        //update ValidationRule info
        gvErrorValidationRule.DataSource = TheReportJob.ErrorReportJobValidationResultList;
        gvErrorValidationRule.DataBind();
        gvProblemValidationRule.DataSource = TheReportJob.ProblemReportJobValidationResultList;
        gvProblemValidationRule.DataBind();
        gvWarningValidationRule.DataSource = TheReportJob.WarningReportJobValidationResultList;
        gvWarningValidationRule.DataBind();

        //btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }

    protected void btnAddReport_Click(object sender, EventArgs e)
    {
        NewJobReport1.SetReportJobId(TheReportJob.Id);
        NewJobReport1.TheReportJobReport = (TheService.FindReportByJobId(TheReportJob.Id) as IList<ReportJobReport>);
        NewJobReport1.TheReportBatchId = TheReportJob.TheBatch.Id;
        NewJobReport1.UpdateView();
        NewJobReport1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteReport_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteReportJobReport(GetSelectReportIdList(gvReportList));

            //re-load the data source
            SynchronizeData();
            ReportJob newReportJob = TheService.LoadReportJob(TheReportJob.Id);
            TheReportJob.ReportList = newReportJob.ReportList;

            UpdateView();
        } 
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this report have been used, can not be deleted";
        }
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        NewJobUser1.TheReportJobUser = (TheService.FindUserByJobId(TheReportJob.Id) as IList<ReportJobUser>);
        NewJobUser1.SetReportJobId(TheReportJob.Id);
        NewJobUser1.TheReportJob = this.TheReportJob;
        NewJobUser1.UpdateView();
        NewJobUser1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteReportJobUser(GetSelectUserIdList(gvUserList));

            //re-load the data source    
            SynchronizeData();
            ReportJob newReportJob = TheService.LoadReportJob(TheReportJob.Id);
            TheReportJob.UserList = newReportJob.UserList;

            UpdateView();
        }
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this user have been used, can not be deleted";
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
        TheReportJob = TheService.LoadReportJob(TheReportJob.Id);
        UpdateView();
    }

    private void downloadValidationResult(int validationResultId)
    {
        ReportJobValidationResult vr = TheService.LoadReportJobValidationResult(validationResultId);
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(TheReportJob.TheBatch.Description + "_" + vr.TheRule.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_result.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadValidateResult(vr.TheRule.ResultContent, csvWriter, vr.TheJob.Id);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    private IList<int> GetSelectReportIdList(GridView gv)
    {
        IList<int> idList = new List<int>();

        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("ckbReportItem");
            if (cbSelect.Checked)
            {
                idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
            }
        }

        return idList;
    }

    private IList<int> GetSelectUserIdList(GridView gv)
    {
        IList<int> idList = new List<int>();

        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("ckbUserItem");
            if (cbSelect.Checked)
            {
                idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
            }
        }

        return idList;
    }
}