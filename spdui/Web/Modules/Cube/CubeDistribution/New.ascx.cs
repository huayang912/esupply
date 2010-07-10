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
using System.Collections.Generic;

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;

//TODO:Add other using statements here.

public partial class Modules_Cube_CubeDistributionJob_New : ModuleBase
{
    private const string invalidCharForFolder = "~ \" # % & * : < > ? { | }.¡£";
    private string[] invalidStringForFolder = new string[] { "\\\\", "//" };

	public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeDistributionJob");
	
	//The entity service
	protected ICubeDistributionJobMgr TheService
    {
        get
        {
            return GetService("CubeDistributionJobMgr.service") as ICubeDistributionJobMgr;
        }
    }

    protected ICubeMgr TheCubeService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    protected ICubeMeasureMgr TheCubeMeasureService
    {
        get
        {
            return GetService("CubeMeasureMgr.service") as ICubeMeasureMgr;
        }
    }

    public CubeDistributionJob TheJob
    {
        get
        {
            return (CubeDistributionJob)ViewState["TheJob"];
        }
        set
        {
            ViewState["TheJob"] = value;
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewJobUser1.Back += new EventHandler(NewJobUser1_Back);
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Add code for Page_Load here.
       
	}

    //The event handler when user click button "Save"
    protected void btnSave_Click(object sender, EventArgs e)
    {
        //Check input
        if (CheckInput())
        {
            SaveData();
        }   
    }    
	
	//The event handler when user click button "Submit"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
        if (CheckInput())
        {
            TheJob.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Submit;
            SaveData();
        }   
	}

    //The event handler when user click button "Restart"
    protected void btnRestart_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            TheJob.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Submit;
            SaveData();
        }  
    }

    //The event handler when user click button "Cancel"
    protected void btnCancel_Click(object sender, EventArgs e)
    {
        if (CheckInput())
        {
            TheJob.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Cancelled;
            SaveData();            
        }  
    }

    //The event handler when user click button "Delete"
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TheService.DeleteCubeDistributionJob(TheJob);
        if (Back != null)
        {
            Back(this, e);
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

    public void ClearMessage()
    {
        lblMessage.Text = string.Empty;
    }

    //The public method to clear the view
	public void UpdateView()
    {
        string jobStatus = TheJob.Status;

        if (jobStatus != CubeDistributionJob.DISTRIBUTION_STATUS_Submit
            && jobStatus != CubeDistributionJob.DISTRIBUTION_STATUS_Success
            && jobStatus != CubeDistributionJob.DISTRIBUTION_STATUS_Running)
        {
            btnSave.Visible = Editable;
        }
        else
        {
            btnSave.Visible = false;
        }

        if (jobStatus == CubeDistributionJob.DISTRIBUTION_STATUS_Pending)
        {
            btnSubmit.Visible = Editable;
        }
        else
        {
            btnSubmit.Visible = false;
        }

        if (jobStatus == CubeDistributionJob.DISTRIBUTION_STATUS_Cancelled
            || jobStatus == CubeDistributionJob.DISTRIBUTION_STATUS_Failed)
        {
            btnRestart.Visible = Editable;
        }
        else
        {
            btnRestart.Visible = false;
        }

        if (jobStatus == CubeDistributionJob.DISTRIBUTION_STATUS_Submit
            || jobStatus == CubeDistributionJob.DISTRIBUTION_STATUS_Running)
        {
            btnCancel.Visible = Editable;
        }
        else
        {
            btnCancel.Visible = false;
        }

        if (jobStatus != CubeDistributionJob.DISTRIBUTION_STATUS_Submit
            && jobStatus != CubeDistributionJob.DISTRIBUTION_STATUS_Success
            && jobStatus != CubeDistributionJob.DISTRIBUTION_STATUS_Running)
        {
            btnDelete.Visible = Editable;
        }
        else
        {
            btnDelete.Visible = false;
        }

        //update basic information
        lblCubeDescription.Text = TheJob.TheCube.Description;
        lblStatus.Text = TheJob.Status;
        txtStartTime.Text = TheJob.JobStartDate.ToString();
        lblEndTime.Text = TheJob.JobEndDate.ToString();
        // Modified by vincent at 2007-11-08 begin
        //txtDataBeginDate.Text = TheJob.BeginDate.ToString("yyyy-MM-dd");
        //txtDataEndDate.Text = TheJob.EndDate.ToString("yyyy-MM-dd");
        int currentYear = DateTime.Now.Year;
        txtDataBeginDate.Text = (currentYear - 2).ToString() + "-1-1";
        txtDataEndDate.Text = currentYear.ToString() + "-12-31";
        // Modified by vincent at 2007-11-08 end

        if (TheJob.NeedSendMail == "YES")
        {
            rdoSendMailYes.Checked = true;
        }
        else
        {
            rdoSendMailNo.Checked = true;
        }

        if (TheJob.AppendDateToFileName == "YES")
        {
            rdoAppendDateYes.Checked = true;
        }
        else
        {
            rdoAppendDateNo.Checked = true;
        }

        // Modified by vincent at 2007-11-21 begin
        if (TheJob.AppendUserNameToFileName == "YES")
        {
            rdoAppendUserNameYes.Checked = true;
        }
        else
        {
            rdoAppendUserNameNo.Checked = true;
        }
        // Modified by vincent at 2007-11-21 end

        if (TheJob.NeedPublishToPortal == "YES")
        {
            rdoNeedUploadYes.Checked = true;
        }
        else
        {
            rdoNeedUploadNo.Checked = true;
        }
        txtUploadFolder.Text = TheJob.PublishFolder;
        if (TheJob.NeedCreateSubFolder == "YES")
        {
            rdoCreateSubFolderYes.Checked = true;
        }
        else
        {
            rdoCreateSubFolderNo.Checked = true;
        }
        

        txtSubject.Text = TheJob.EMailSubject;
        txtBody.Text = TheJob.EMailBody;
        
        gvUserList.DataSource = TheJob.CubeDistributionJobItemList;
        gvUserList.DataBind();

        cbMeasuresSelect.DataSource = TheCubeMeasureService.FindMeasureByCubeId(TheJob.TheCube.Id);
        cbMeasuresSelect.DataBind();

        //check Distribute Measures
        string[] measureAry = TheJob.MeasureList.Split(',');
        for (int i = 0; i < cbMeasuresSelect.Items.Count; i++)
        {
            ListItem li = cbMeasuresSelect.Items[i];
            for (int j = 0; j < measureAry.Length; j++)
            {
                if (li.Value == measureAry[j])
                {
                    li.Selected = true;
                    break;
                }
            }
        }
    }

    //The event handler when user click button "AddUser"
    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        NewJobUser1.TheJobItem = TheService.FindJobItemByJobId(TheJob.Id);
        NewJobUser1.TheJob = this.TheJob;
        NewJobUser1.UpdateView();
        NewJobUser1.Visible = true;
        pnlMain.Visible = false;
    }

    //The event handler when user click button "DeleteUser"
    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        IList<int> IdList = new List<int>();
        foreach (GridViewRow row in gvUserList.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("ckbUserItem");
            if (cbSelect.Checked)
            {
                IdList.Add((int)(gvUserList.DataKeys[row.RowIndex].Value));
            }
        }

        TheService.DeleteCubeDistributionJobItem(IdList);
        TheJob.CubeDistributionJobItemList = TheService.FindJobItemByJobId(TheJob.Id);
        UpdateView();
    }

    protected void NewJobUser1_Back(object sender, EventArgs e)
    {
        NewJobUser1.Visible = false;
        TheJob.CubeDistributionJobItemList = TheService.FindJobItemByJobId(TheJob.Id);
        pnlMain.Visible = true;
        UpdateView();
    }

    private bool CheckInput()
    {
       
        foreach (string invalidStr in invalidStringForFolder)
        {
            if (txtUploadFolder.Text.Contains(invalidStr))
            {
                lblMessage.Text = "Uploading Folder can't contains double \\ or double /";
                return false;
            }
        }

        char[] invalidChars = invalidCharForFolder.Replace(" ", "").ToCharArray();
        foreach (char invalidChar in invalidChars)
        {
            if (txtUploadFolder.Text.Contains(invalidChar.ToString()))
            {
                lblMessage.Text = "Uploading Folder can't contains " + invalidCharForFolder;
                return false;
            }
        }

        if (txtStartTime.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Start Time can not be empty";
            return false;
        }

        if (txtDataBeginDate.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Offline Data From can not be empty";
            return false;
        }

        if (txtDataEndDate.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Offline Data To can not be empty";
            return false;
        }

        if (rdoSendMailYes.Checked && txtSubject.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Email Subject can not be empty when choose send mail";
            return false;
        }

        if (rdoSendMailYes.Checked && txtBody.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Email Body can not be empty when choose send mail";
            return false;
        }

        return true;
    }


    private void SynchronizeData()
    {
        lblMessage.Visible = true;

        TheJob.JobStartDate = Convert.ToDateTime(txtStartTime.Text.Trim());
        TheJob.BeginDate = Convert.ToDateTime(txtDataBeginDate.Text.Trim());
        TheJob.EndDate = Convert.ToDateTime(txtDataEndDate.Text.Trim());
        TheJob.NeedSendMail = rdoSendMailYes.Checked ? "YES" : "NO";

        TheJob.EMailBody = txtBody.Text.Trim();
        TheJob.EMailSubject = txtSubject.Text.Trim();
        TheJob.JobDescription = txtSubject.Text.Trim();

        TheJob.AppendDateToFileName = rdoAppendDateYes.Checked ? "YES" : "NO";
        // Modified by vincent at 2007-11-21 begin
        TheJob.AppendUserNameToFileName = rdoAppendUserNameYes.Checked ? "YES" : "NO";
        // Modified by vincent at 2007-11-21 end 

        TheJob.NeedCreateSubFolder = rdoCreateSubFolderYes.Checked ? "YES" : "NO";
        TheJob.NeedPublishToPortal = rdoNeedUploadYes.Checked ? "YES" : "NO";
        TheJob.PublishFolder = txtUploadFolder.Text.Trim();

        TheJob.UpdateDate = DateTime.Now;

        string selectedMeasures = string.Empty;
        for (int i = 0; i < cbMeasuresSelect.Items.Count; i++)
        {
            ListItem li = cbMeasuresSelect.Items[i];
            if (li.Selected)
            {
                selectedMeasures += li.Value + ",";
            }
        }

        TheJob.MeasureList = selectedMeasures.Trim(',');
    }

    private void SaveData()
    {

        SynchronizeData();

        //try
        //{
        TheService.UpdateCubeDistributionJob(TheJob);
        TheService.SynchronizeCubeDistributionJobItem(TheJob);
        UpdateView();
        lblMessage.Text = "Data Update Successful";                        
        //}
        //catch (Exception ex)
        //{
        //    lblMessage.Text = ex.Message;
        //}
    }
}