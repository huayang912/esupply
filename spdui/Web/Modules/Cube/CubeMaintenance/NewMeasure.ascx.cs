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

public partial class Modules_Cube_CubeMeasure_New : ModuleBase
{
	public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeMeasure");
	
	//The entity service
	protected ICubeMeasureMgr TheService
    {
        get
        {
            return GetService("CubeMeasureMgr.service") as ICubeMeasureMgr;
        }
    }

    protected ICubeMgr TheCubeService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    public CubeMeasure TheCubeMeasure
    {
        get
        {
            return (CubeMeasure)ViewState["TheCubeMeasure"];
        }
        set
        {
            ViewState["TheCubeMeasure"] = value;
        }
    }

    public void SetCubeId(int cubeId)
    {
        txtCubeId.Value = cubeId.ToString();
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Add code for Page_Load here.
	}
	
	//The event handler when user click button "Submit"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
        SaveMeasure();
	}

    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        SaveMeasure();
        TheCubeMeasure = null;
        UpdateView();
    }

	//The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void SaveMeasure()
    {
        if (TheCubeMeasure == null)
        {
            TheCubeMeasure = new CubeMeasure();
            CubeDefinition cube = TheCubeService.LoadCube(int.Parse(txtCubeId.Value));
            TheCubeMeasure.TheCube = cube;
        }

        TheCubeMeasure.Name = txtMeasureName.Text;
        TheCubeMeasure.DisplayName = txtDisplayName.Text;

        if (TheCubeMeasure.Id == 0)
        {
            TheService.CreateCubeMeasure(TheCubeMeasure);
        }
        else
        {
            TheService.UpdateCubeMeasure(TheCubeMeasure);
        }

    }

    //The public method to clear the view
	public void UpdateView()
    {
        if (TheCubeMeasure == null)
        {
            txtMeasureName.Text = String.Empty;
            txtDisplayName.Text = String.Empty;
        }
        else
        {
            txtMeasureName.Text = TheCubeMeasure.Name;
            txtDisplayName.Text = TheCubeMeasure.DisplayName;            
        }
    }
}