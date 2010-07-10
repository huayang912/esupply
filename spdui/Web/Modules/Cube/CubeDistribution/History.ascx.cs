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
using Dndp.Persistence.Dao.Cube;
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;

//TODO: Add other using statements here.

public partial class Modules_Cube_CubeDistributionJob_History : ModuleBase
{
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

    public CubeDistributionJob TheCubeDistributionJob
    {
        get
        {
            return (CubeDistributionJob)ViewState["TheCubeDistributionJob"];
        }
        set
        {
            ViewState["TheCubeDistributionJob"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        //TODO: Add code here.
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
        //TODO: Check input
		
		//TODO: Perform "New" action.
		
		//TODO: Show feedback message to user.
    }
}