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
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;

using System.Data.SqlClient;
using Dndp.Persistence.Entity.Distribution;

//TODO:Add other using statements here.

public partial class Modules_OffLineReport_ReportUserMaintenance_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;

	//Get the logger
	private static ILog log = LogManager.GetLogger("ReportUserMaintenance");
	
	//The entity service
    protected IReportUserMgr TheService
    {
        get
        {
            return GetService("ReportUserMgr.service") as IReportUserMgr;
        }
    }

    public DistributionUser DistributionUser
    {
        get
        {
            return ViewState["DistributionUser"] != null ? (DistributionUser)ViewState["DistributionUser"] : null;
        }
        set
        {
            ViewState["DistributionUser"] = value;
        }
    }

    private ReportUser _newReportUser;
    public ReportUser NewReportUser
    {
        get
        {
            return _newReportUser;
        }
        set
        {
            _newReportUser = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        SelectUser1.Back += new System.EventHandler(this.SelectUser1_Back);
        //TODO: Add other init code here.
    }

    //The event handler when user click button "Back" button on New page.
    void SelectUser1_Back(object sender, EventArgs e)
    {
        DistributionUser = SelectUser1.SelectedDistributionUser;
        SelectUser1.Visible = false;
        if (DistributionUser != null)
        {
            txtDistributionUserName.Text = DistributionUser.Name;
        }
        pnlMain.Visible = true;        
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            UpdateView();
        }
    }

    //The event handler when user click button "Submit"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string dsName = txtName.Text.Trim();
        if (dsName.Length == 0)
        {
            lblMessage.Text = "User Name can not be empty";
            lblMessage.Visible = true;
        }
        else if (DistributionUser == null)
        {
            lblMessage.Text = "Please select Distribution User";
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Visible = false;
            NewReportUser = new ReportUser();
            NewReportUser.TheUser = DistributionUser;
            NewReportUser.Name = dsName;
            NewReportUser.Description = txtDescription.Text.Trim();
            //NewReportUser.EMAIL = txtEmail.Text.Trim();
            NewReportUser.PortalSite = txtReportSite.Text.Trim();
            NewReportUser.PortalDocumentLibrary = txtReportLibrary.Text.Trim();
            NewReportUser.PortalReadUserList = txtReportReadUsers.Text.Trim();
            NewReportUser.PortalFullControlUserList = txtReportFullControlUsers.Text.Trim();            
            //NewReportUser.CubeSite = txtCubeSite.Text.Trim();
            //NewReportUser.CubeDocumentLibrary = txtCubeLibrary.Text.Trim();
            //NewReportUser.CubeReadUserList = txtCubeReadUsers.Text.Trim();
            //NewReportUser.CubeFullControlUserList = txtReportFullControlUsers.Text.Trim();
            NewReportUser.ActiveFlag = 1;
            try
            {
                TheService.CreateReportUser(NewReportUser);
                lblMessage.Text = "Data Insert Successful";
                lblMessage.Visible = true;
                btnSubmit.Visible = false;

                if (Submit != null)
                {
                    Submit(this, null);
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
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

    //The event handler when user click button "Select User"
    protected void btnSelectUser_Click(object sender, EventArgs e)
    {
        SelectUser1.SelectedDistributionUser = null;
        SelectUser1.IsOfflineCubeUser = false;
        SelectUser1.IsOnlineCubeUser = false;
        SelectUser1.IsOfflineReportUser = true;
        SelectUser1.Visible = true;
        SelectUser1.UpdateView();
        pnlMain.Visible = false;  
    }

    //The public method to clear the view
    public void UpdateView()
    {
        //Add code to clear the view.
        txtDistributionUserName.Text = DistributionUser != null ? DistributionUser.Name : "";
        txtName.Text = String.Empty;
        //txtEmail.Text = String.Empty;
        txtDescription.Text = String.Empty;
        txtDistributionUserName.Text = String.Empty;
        txtReportFullControlUsers.Text = String.Empty;
        txtReportLibrary.Text = String.Empty;
        txtReportReadUsers.Text = String.Empty;
        txtReportSite.Text = String.Empty;
        btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }
}