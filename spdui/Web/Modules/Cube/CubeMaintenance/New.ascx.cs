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

public partial class Modules_Cube_Cube_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;

	
	//Get the logger
	private static ILog log = LogManager.GetLogger("Cube");
	
	//The entity service
	protected ICubeMgr TheService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    protected ICubeParameterMgr TheParameterService
    {
        get
        {
            return GetService("CubeParameterMgr.service") as ICubeParameterMgr;
        }
    }

    private CubeDefinition _newCube;
    public CubeDefinition NewCube
    {
        get
        {
            return _newCube;
        }
        set
        {
            _newCube = value;
        }
    }

	protected void Page_Load(object sender, EventArgs e)
	{
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            InitDDL();
            //UpdateView();
        }
	}

    private void InitDDL()
    {
        //Get all parameters
        IList<CubeParameter> parameters = TheParameterService.LoadAllActiveCubeParameter();
        ddlParameter.DataSource = parameters;
        ddlParameter.DataTextField = "Name";
        ddlParameter.DataValueField = "Name";
        ddlParameter.DataBind();
    }

    protected void btnInsertParameter_Click(object sender, EventArgs e)
    {
        txtPreProcessSQL.Text = txtPreProcessSQL.Text + Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlParameter.SelectedValue);
    }

	
	//The event handler when user click button "Submit"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		//Check input
        string errorMessage = "";
        if (txtProcessCubeName.Text.Trim().Length == 0)
        {
            errorMessage = "Process Cube Name can not be empty";
        }

        if (txtReleaseCubeName.Text.Trim().Length == 0)
        {
            errorMessage = "Release Cube Name can not be empty";
        }

        if (txtProcessServerAddr.Text.Trim().Length == 0)
        {
            errorMessage = "Process Server Address can not be empty";
        }

        if (txtProcessDatabaseName.Text.Trim().Length == 0)
        {
            errorMessage = "Process Database Name can not be empty";
        }

        if (txtReleaseServerAddr.Text.Trim().Length == 0)
        {
            errorMessage = "Release Server Address can not be empty";
        }

        if (txtReleaseDatabaseName.Text.Trim().Length == 0)
        {
            errorMessage = "Release Database Name can not be empty";
        }

        if (errorMessage.Trim().Length == 0)
        {
            //Perform "New" action.
            lblMessage.Visible = false;
            CubeDefinition cube = new CubeDefinition();

            cube.ProcessCubeName = txtProcessCubeName.Text.Trim();
            cube.ReleaseCubeName = txtReleaseCubeName.Text.Trim();
            cube.Description = txtDescription.Text.Trim();
            cube.ProcessDatabaseName = txtReleaseDatabaseName.Text.Trim();
            cube.ProcessServerAddr = txtReleaseServerAddr.Text.Trim();
            cube.ProcessCubeBackupFolder = txtProcessBackupFolder.Text.Trim();
            cube.ReleaseDatabaseName = txtReleaseDatabaseName.Text.Trim();
            cube.ReleaseServerAddr = txtReleaseServerAddr.Text.Trim();
            cube.PostProcessSQL = txtPostProcessSQL.Text.Trim();
            cube.PreProcessSQL = txtPreProcessSQL.Text.Trim();
            cube.PostReleaseSQL = txtPostReleaseSQL.Text.Trim();
            cube.PreReleaseSQL = txtPreReleaseSQL.Text.Trim();
            cube.CreateUser = CurrentUser;
            cube.CreateDate = System.DateTime.Now;
            cube.UpdateUser = CurrentUser;
            cube.UpdateDate = System.DateTime.Now;
            cube.ActiveFlag = 1;
            // Modified by vincent at 2007-11-13 begin
            cube.PreUpdateRoleSQL = txtPostUpdateRoleSQL.Text.Trim();
            cube.PostUpdateRoleSQL = txtPostUpdateRoleSQL.Text.Trim();
            cube.PreUpdateDescriptionSQL = txtPreUpdateDescriptionSQL.Text.Trim();
            cube.PostUpdateDescriptionSQL = txtPostUpdateDescriptionSQL.Text.Trim();

            // Modified by vincent at 2007-11-13 end
            try
            {
                TheService.CreateCube(cube);
                lblMessage.Text = "Data Insert Successful";
                lblMessage.Visible = true;
                btnSubmit.Visible = false;

                if (Submit != null)
                {
                    this.NewCube = cube;
                    Submit(this, null);
                    this.NewCube = null;
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }
        else
        {
            //Show feedback message to user.
            lblMessage.Text = errorMessage;
            lblMessage.Visible = true;
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
        //Add code to clear the view.
        txtProcessCubeName.Text = String.Empty;
        txtReleaseCubeName.Text = String.Empty;
        txtDescription.Text = String.Empty;
        txtProcessServerAddr.Text = String.Empty;
        txtProcessDatabaseName.Text = String.Empty;
        txtReleaseServerAddr.Text = String.Empty;
        txtReleaseDatabaseName.Text = String.Empty;
        txtPreProcessSQL.Text = String.Empty;
        txtPostProcessSQL.Text = String.Empty;
        txtPreReleaseSQL.Text = String.Empty;
        txtPostReleaseSQL.Text = String.Empty;

        // Modified by vincent at 2007-11-13 begin                
        txtPreUpdateRoleSQL.Text = String.Empty;
        txtPostUpdateRoleSQL.Text = String.Empty;
        txtPreUpdateDescriptionSQL.Text = String.Empty;
        txtPostUpdateDescriptionSQL.Text = String.Empty;
        // Modified by vincent at 2007-11-13 end
        btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }
}