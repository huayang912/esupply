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
using Dndp.Persistence.Entity.Distribution;
using Dndp.Service.Cube;
using Dndp.Persistence.Entity.Cube;

//TODO: Add other using statements here.

public partial class Modules_Cube_CubeUser_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
    private static ILog log = LogManager.GetLogger("CubeUser");    

	//The entity service
    protected ICubeAuthorizationMgr TheService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		//TODO: Add code for Page_Load here.
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdate.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
    }

    public CubeUser TheCubeUser
    {
        get
        {
            return (CubeUser)ViewState["TheCubeUser"];
        }
        set
        {
            ViewState["TheCubeUser"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        //TODO: Add code here.
        txtDistributionUserName.Text = TheCubeUser.TheDistributionUser.Name;
        txtName.Text = TheCubeUser.Name;
        //txtEmail.Text = TheReportUser.EMAIL;
        txtDescription.Text = TheCubeUser.Description;
        txtCubeSite.Text = TheCubeUser.CubeSite;
        txtCubeLibrary.Text = TheCubeUser.CubeDocumentLibrary;
        txtCubeReadUsers.Text = TheCubeUser.CubeReadUserList;
        txtCubeFullControlUsers.Text = TheCubeUser.CubeFullControlUserList;
        //txtCubeSite.Text = TheReportUser.CubeSite;
        //txtCubeLibrary.Text = TheReportUser.CubeDocumentLibrary;
        //txtCubeReadUsers.Text = TheReportUser.CubeReadUserList;
        //txtCubeFullControlUsers.Text = TheReportUser.CubeFullControlUserList;

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
        lblMessage.Visible = true;

        string name = txtName.Text.Trim();
        if (name.Length == 0)
        {
            lblMessage.Text = "User name cannot be empty.";
            return;
        }

        TheCubeUser.Name = name;
        //TheReportUser.EMAIL = txtEmail.Text.Trim();
        TheCubeUser.Description = txtDescription.Text.Trim();
        TheCubeUser.CubeSite = txtCubeSite.Text.Trim();
        TheCubeUser.CubeDocumentLibrary = txtCubeLibrary.Text.Trim();
        TheCubeUser.CubeReadUserList = txtCubeReadUsers.Text.Trim();
        TheCubeUser.CubeFullControlUserList = txtCubeFullControlUsers.Text.Trim();
        
        try
        {
            TheService.UpdateCubeUser(TheCubeUser);
            lblMessage.Text = "Update successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteCubeUser(TheCubeUser.Id);
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