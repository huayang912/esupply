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

public partial class Modules_Cube_CubeUserRole_New : ModuleBase
{
	public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeUserRole");
	
	//The entity service
    protected ICubeAuthorizationMgr TheService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    public CubeRole TheCubeRole
    {
        get
        {
            return (CubeRole)ViewState["TheCubeRole"];
        }
        set
        {
            ViewState["TheCubeRole"] = value;
        }
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Add code for Page_Load here.
	}

    protected void btnSearch_Click(object sender, EventArgs e)
    {
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

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        IList<int> IdList = GetSelectIdList(gvUserList);
        TheService.UpdateCubeUserRole(TheCubeRole, IdList);
        //TheService.UploadRoleToCube(TheCubeRole);
        UpdateView();
    }

    //The public method to clear the view
	public void UpdateView()
    {
        gvUserList.DataSource = TheService.FindUserExcludeSpecifiedRoleByNameAndDescription(TheCubeRole.Id, txtUserName.Text.Trim(), txtUserDescription.Text.Trim());
        gvUserList.DataBind();
        gvUserList.Visible = true;     
    }
}