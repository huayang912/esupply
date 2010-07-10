using System;
using System.Data;
using System.Data.SqlClient;
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
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;

//TODO: Add other using statements here.

public partial class Modules_Cube_Cube_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("Cube");

    //The entity service
    #region service define
    protected ICubeMgr TheService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    protected ICubeValidationRuleMgr TheRuleService
    {
        get
        {
            return GetService("CubeValidationRuleMgr.service") as ICubeValidationRuleMgr;
        }
    }

    protected ICubeParameterMgr TheParameterService
    {
        get
        {
            return GetService("CubeParameterMgr.service") as ICubeParameterMgr;
        }
    }

    protected ICubeOperatorMgr TheOperatorService
    {
        get
        {
            return GetService("CubeOperatorMgr.service") as ICubeOperatorMgr;
        }
    }

    protected ICubeMeasureMgr TheMeasureService
    {
        get
        {
            return GetService("CubeMeasureMgr.service") as ICubeMeasureMgr;
        }
    }

    protected ICubeDimensionMgr TheDimensionService
    {
        get
        {
            return GetService("CubeDimensionMgr.service") as ICubeDimensionMgr;
        }
    }

    protected ICubeWarmMDXMgr TheMDXService
    {
        get
        {
            return GetService("CubeWarmMDXMgr.service") as ICubeWarmMDXMgr;
        }
    }
    #endregion

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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewRule1.Back += new System.EventHandler(this.NewRule1_Back);
        NewOperator1.Back += new System.EventHandler(this.NewOperator1_Back);
        NewDimension1.Back += new System.EventHandler(this.NewDimension1_Back);
        NewMDX1.Back += new System.EventHandler(this.NewMDX1_Back);
        NewMeasure1.Back += new System.EventHandler(this.NewMeasure1_Back);
    }

    //The event handler when user click button "Back" button on NewField page.
    void NewRule1_Back(object sender, EventArgs e)
    {
        NewRule1.Visible = false;
        TheCube.CubeValidationRuleList = TheRuleService.FindCubeValidationRuleWithCubeId(TheCube.Id);        
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewOperator1_Back(object sender, EventArgs e)
    {
        NewOperator1.Visible = false;
        TheCube.CubeOperatorList = TheOperatorService.FindOperatorByCubeId(TheCube.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewMeasure1_Back(object sender, EventArgs e)
    {
        NewMeasure1.Visible = false;
        TheCube.CubeMeasureList = TheMeasureService.FindMeasureByCubeId(TheCube.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewDimension1_Back(object sender, EventArgs e)
    {
        NewDimension1.Visible = false;
        TheCube.CubeDimensionList = TheDimensionService.FindDimensionByCubeId(TheCube.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewMDX1_Back(object sender, EventArgs e)
    {
        NewMDX1.Visible = false;
        TheCube.CubeWarmMDXList = TheMDXService.FindCubeWarmMDXByCubeId(TheCube.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdate.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
    }

    public CubeDefinition TheCube
    {
        get
        {
            return (CubeDefinition)ViewState["TheCube"];
        }
        set
        {
            ViewState["TheCube"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        lblMessage.Text = string.Empty;
        lblMessage.Visible = false;
        txtProcessCubeName.Text = TheCube.ProcessCubeName;
        txtReleaseCubeName.Text = TheCube.ReleaseCubeName;
        txtDescription.Text = TheCube.Description;
        txtPostProcessSQL.Text = TheCube.PostProcessSQL;
        txtPreProcessSQL.Text = TheCube.PreProcessSQL;
        txtPostReleaseSQL.Text = TheCube.PostReleaseSQL;
        txtPreReleaseSQL.Text = TheCube.PreReleaseSQL;

        // Modified by vincent at 2007-11-13 begin
        txtPostUpdateDescriptionSQL.Text = TheCube.PostUpdateDescriptionSQL;
        txtPreUpdateDescriptionSQL.Text = TheCube.PreUpdateDescriptionSQL;
        txtPostUpdateRoleSQL.Text = TheCube.PostUpdateRoleSQL;
        txtPreUpdateRoleSQL.Text = TheCube.PreUpdateRoleSQL;

        // Modified by vincent at 2007-11-13 end

        txtProcessDatabaseName.Text = TheCube.ProcessDatabaseName;
        txtProcessServerAddr.Text = TheCube.ProcessServerAddr;
        txtProcessBackupFolder.Text = TheCube.ProcessCubeBackupFolder;
        txtReleaseDatabaseName.Text = TheCube.ReleaseDatabaseName;
        txtReleaseServerAddr.Text = TheCube.ReleaseServerAddr;

        gvRuleList.DataSource = TheCube.CubeValidationRuleList;
        gvRuleList.DataBind();

        gvOperatorList.DataSource = TheCube.CubeOperatorList;
        gvOperatorList.DataBind();

        gvMeasureList.DataSource = TheCube.CubeMeasureList;
        gvMeasureList.DataBind();

        gvDimensionList.DataSource = TheCube.CubeDimensionList;
        gvDimensionList.DataBind();

        gvMDXList.DataSource = TheCube.CubeWarmMDXList;
        gvMDXList.DataBind();

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
            //Perform "Edit" action.
            lblMessage.Visible = false;

            TheCube.ProcessCubeName = txtProcessCubeName.Text.Trim();
            TheCube.ReleaseCubeName = txtReleaseCubeName.Text.Trim();
            TheCube.Description = txtDescription.Text.Trim();
            TheCube.ProcessDatabaseName = txtProcessDatabaseName.Text.Trim();
            TheCube.ProcessServerAddr = txtProcessServerAddr.Text.Trim();
            TheCube.ProcessCubeBackupFolder = txtProcessBackupFolder.Text.Trim();
            TheCube.ReleaseDatabaseName = txtReleaseDatabaseName.Text.Trim();
            TheCube.ReleaseServerAddr = txtReleaseServerAddr.Text.Trim();
            TheCube.PostProcessSQL = txtPostProcessSQL.Text.Trim();
            TheCube.PreProcessSQL = txtPreProcessSQL.Text.Trim();
            TheCube.PostReleaseSQL = txtPostReleaseSQL.Text.Trim();
            TheCube.PreReleaseSQL = txtPreReleaseSQL.Text.Trim();
            // Modified by vincent at 2007-11-13 begin
            TheCube.PostUpdateDescriptionSQL = txtPostUpdateDescriptionSQL.Text.Trim();
            TheCube.PreUpdateDescriptionSQL = txtPreUpdateDescriptionSQL.Text.Trim();
            TheCube.PostUpdateRoleSQL = txtPostUpdateRoleSQL.Text.Trim();
            TheCube.PreUpdateRoleSQL = txtPreUpdateRoleSQL.Text.Trim();
            // Modified by vincent at 2007-11-13 end
            TheCube.CreateUser = CurrentUser;
            TheCube.CreateDate = System.DateTime.Now;
            TheCube.UpdateUser = CurrentUser;
            TheCube.UpdateDate = System.DateTime.Now;
            TheCube.ActiveFlag = 1;
            try
            {
                TheService.UpdateCube(TheCube);
                lblMessage.Text = "Data Update Successful";
                lblMessage.Visible = true;

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

    //The event handler when user click button "Delete"
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TheService.DeleteCube(TheCube);
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The event handler when user click button "Delete Rule"
    protected void btnDeleteRule_Click(object sender, EventArgs e)
    {
        try
        {
            TheRuleService.DeleteCubeValidationRule(GetSelectIdList(gvRuleList));
        }
        catch (Exception ex)
        {
            lblMessage.Text = "The Rule you selected has been used by some Cube Processes, that can't be delete.";
            lblMessage.Visible = true;
            return;
        }

        //re-load the data source
        TheCube.CubeValidationRuleList = TheRuleService.FindCubeValidationRuleWithCubeId(TheCube.Id);

        UpdateView();
    }

    //The event handler when user click button "Delete Operator"
    protected void btnDeleteOperator_Click(object sender, EventArgs e)
    {

        TheOperatorService.DeleteCubeOperator(GetSelectIdList(gvRuleList));
        

        //re-load the data source
        TheCube.CubeOperatorList = TheOperatorService.FindOperatorByCubeId(TheCube.Id);        

        UpdateView();
    }

    //The event handler when user click button "Delete Measure"
    protected void btnDeleteMeasure_Click(object sender, EventArgs e)
    {

        TheMeasureService.DeleteCubeMeasure(GetSelectIdList(gvMeasureList));

        //re-load the data source
        TheCube.CubeMeasureList = TheMeasureService.FindMeasureByCubeId(TheCube.Id);

        UpdateView();
    }

    //The event handler when user click button "Delete Dimension"
    protected void btnDeleteDimension_Click(object sender, EventArgs e)
    {

        TheDimensionService.DeleteCubeDimension(GetSelectIdList(gvDimensionList));

        //re-load the data source
        TheCube.CubeDimensionList = TheDimensionService.FindDimensionByCubeId(TheCube.Id);

        UpdateView();
    }

    //The event handler when user click button "Delete Dimension"
    protected void btnDeleteMDX_Click(object sender, EventArgs e)
    {

        TheMDXService.DeleteCubeWarmMDX(GetSelectIdList(gvMDXList));

        //re-load the data source
        TheCube.CubeWarmMDXList = TheMDXService.FindCubeWarmMDXByCubeId(TheCube.Id);

        UpdateView();
    }

    protected void btnAddRule_Click(object sender, EventArgs e)
    {
        NewRule1.Visible = true;
        NewRule1.TheCubeValidationRule = null;
        NewRule1.SetCubeId(TheCube.Id);
        NewRule1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void btnAddOperator_Click(object sender, EventArgs e)
    {
        NewOperator1.Visible = true;
        NewOperator1.TheCubeOperators = TheOperatorService.FindOperatorByCubeIdAndAllowType(TheCube.Id, "Process");        
        NewOperator1.SetCubeId(TheCube.Id);
        NewOperator1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void btnAddMeasure_Click(object sender, EventArgs e)
    {
        NewMeasure1.Visible = true;
        NewMeasure1.SetCubeId(TheCube.Id);
        NewMeasure1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void btnAddDimension_Click(object sender, EventArgs e)
    {
        NewDimension1.Visible = true;     
        NewDimension1.SetCubeId(TheCube.Id);
        NewDimension1.TheCubeDimension = null;
        NewDimension1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void btnAddMDX_Click(object sender, EventArgs e)
    {
        NewMDX1.Visible = true;
        NewMDX1.SetCubeId(TheCube.Id);
        NewMDX1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void lbtnRuleName_Click(object sender, EventArgs e)
    {
        int dsRuleId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewRule1.TheCubeValidationRule = TheRuleService.LoadCubeValidationRule(dsRuleId);
        NewRule1.UpdateView();
        NewRule1.Visible = true;
        NewRule1.SetCubeId(TheCube.Id);
        pnlMain.Visible = false;
    }

    protected void lbtnMeasureName_Click(object sender, EventArgs e)
    {
        int measureId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewMeasure1.TheCubeMeasure = TheMeasureService.LoadCubeMeasure(measureId);
        NewMeasure1.UpdateView();
        NewMeasure1.Visible = true;
        NewMeasure1.SetCubeId(TheCube.Id);
        pnlMain.Visible = false;
    }

    protected void lbtnDimensionName_Click(object sender, EventArgs e)
    {
        int dimId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewDimension1.TheCubeDimension = TheDimensionService.LoadCubeDimension(dimId);
        NewDimension1.UpdateView();
        NewDimension1.Visible = true;
        NewDimension1.SetCubeId(TheCube.Id);
        pnlMain.Visible = false;
    }

    protected void gvRule_OnRowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            e.Row.Cells[1].Text = Convert.ToString(e.Row.DataItemIndex + 1);
        }
    }

    protected void lbtnMDX_Click(object sender, EventArgs e)
    {
        int MDXId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewMDX1.TheCubeWarmMDX = TheMDXService.LoadCubeWarmMDX(MDXId);
        NewMDX1.UpdateView();
        NewMDX1.Visible = true;
        NewMDX1.SetCubeId(TheCube.Id);
        pnlMain.Visible = false;
    }
}