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
using Dndp.Persistence.Dao.Distribution;
using Dndp.Persistence.Entity.Distribution;
using Dndp.Service.Distribution;

using System.Data.SqlClient;

public partial class Modules_Distribution_DistributionUser_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("DistributionUser");
	
	//The entity service
	protected IDistributionUserMgr TheService
    {
        get
        {
            return GetService("DistributionUserMgr.service") as IDistributionUserMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		//TODO: Add code for Page_Load here.
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdate.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
    }

    public DistributionUser TheDistributionUser
    {
        get
        {
            return (DistributionUser)ViewState["TheDistributionUser"];
        }
        set
        {
            ViewState["TheDistributionUser"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        txtName.Text = TheDistributionUser.Name;
        txtEmail.Text = TheDistributionUser.Email;
        txtDescription.Text = TheDistributionUser.Description;
        txtDomainAccount.Text = TheDistributionUser.DomainAccount;
        cbReportUser.Checked = TheDistributionUser.IsReportUser == 1;
        cbOnlineCubeUser.Checked = TheDistributionUser.IsOnlineCubeUser == 1;
        cbOfflineCubeUser.Checked = TheDistributionUser.IsOfflineCubeUser == 1;

        lblMessage.Visible = false;
    }

	//Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //Event handler when user click button "Update"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string dsName = txtName.Text.Trim();
        if (dsName.Length == 0)
        {
            lblMessage.Text = "User Name can not be empty";
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Visible = false;
            TheDistributionUser.Name = dsName;
            TheDistributionUser.Description = txtDescription.Text.Trim();
            TheDistributionUser.Email = txtEmail.Text.Trim();
            TheDistributionUser.DomainAccount = txtDomainAccount.Text.Trim();
            TheDistributionUser.IsReportUser = cbReportUser.Checked ? 1 : 0;
            TheDistributionUser.IsOnlineCubeUser = cbOnlineCubeUser.Checked ? 1 : 0;
            TheDistributionUser.IsOfflineCubeUser = cbOfflineCubeUser.Checked ? 1 : 0;
            try
            {
                TheService.UpdateDistributionUser(TheDistributionUser);
                lblMessage.Text = "Update successfully.";
                lblMessage.Visible = true;                
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }
    }

    //Event handler when user click button "Delete"
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteDistributionUser(TheDistributionUser);
            if (Back != null)
            {
                Back(this, e);
            }
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }
}