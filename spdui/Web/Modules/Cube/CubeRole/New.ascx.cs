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
using System.Data.SqlClient;

//TODO:Add other using statements here.

public partial class Modules_Cube_CubeRole_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeRole");
	
	//The entity service
	protected ICubeAuthorizationMgr TheService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    protected ICubeMgr TheCubeService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    private CubeRole _newCubeRole;
    public CubeRole NewCubeRole
    {
        get
        {
            return _newCubeRole;
        }
        set
        {
            _newCubeRole = value;
        }
    }

	protected void Page_Load(object sender, EventArgs e)
	{
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
            lblMessage.Text = "Role Name can not be empty";
            lblMessage.Visible = true;
        }
        else if (cbCube == null || cbCube.Items.Count == 0)
        {
            lblMessage.Text = "There is no cube to select";
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Visible = false;
            NewCubeRole = new CubeRole();
            NewCubeRole.TheCube = TheCubeService.LoadCube(int.Parse(cbCube.SelectedValue));
            NewCubeRole.Name = dsName;
            NewCubeRole.Description = txtDescription.Text.Trim();
            NewCubeRole.IsVisualtotal = 1;
            NewCubeRole.IsDrillthroughAndLocalCube = 1;
            try
            {
                TheService.CreateCubeRole(NewCubeRole);
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
        InitCubeList();
        txtName.Text = String.Empty;
        txtDescription.Text = String.Empty;
        //cbVisualtotal.Checked = true;
        btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
        cbCube.SelectedIndex = 0;
    }

    private void InitCubeList()
    {
       cbCube.DataSource = TheCubeService.LoadAllActiveCube();
       cbCube.DataBind();
    }
}