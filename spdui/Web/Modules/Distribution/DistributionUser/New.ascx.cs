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
using Dndp.Persistence.Dao.Distribution;
using Dndp.Persistence.Entity.Distribution;
using Dndp.Service.Distribution;
using System.Data.SqlClient;

//TODO:Add other using statements here.

public partial class Modules_Distribution_DistributionUser_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;
	
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

    private DistributionUser _newDistributionUser;
    public DistributionUser NewDistributionUser
    {
        get
        {
            return _newDistributionUser;
        }
        set
        {
            _newDistributionUser = value;
        }
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
        else
        {
            lblMessage.Visible = false;
            NewDistributionUser = new DistributionUser();
            NewDistributionUser.Name = dsName;            
            NewDistributionUser.Description = txtDescription.Text.Trim();
            NewDistributionUser.Email = txtEmail.Text.Trim();
            NewDistributionUser.DomainAccount = txtDomainAccount.Text.Trim();
            NewDistributionUser.IsReportUser = cbReportUser.Checked ? 1 : 0;
            NewDistributionUser.IsOnlineCubeUser = cbOnlineCubeUser.Checked ? 1 : 0;
            NewDistributionUser.IsOfflineCubeUser = cbOfflineCubeUser.Checked ? 1 : 0;
            NewDistributionUser.ActiveFlag = 1;
            try
            {
                TheService.CreateDistributionUser(NewDistributionUser);
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

    //The public method to clear the view
	public void UpdateView()
    {
        txtName.Text  = string.Empty;
        txtDescription.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtDomainAccount.Text = string.Empty;
        cbReportUser.Checked = true;
        cbOnlineCubeUser.Checked = true;
        cbOfflineCubeUser.Checked = true;
        btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }
}