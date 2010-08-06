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

//TODO: Add other using statements here.

public partial class Modules_Cube_CubeRole_Main : ModuleBase
{
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

    protected ICubeReleaseMgr TheCubeReleaseMgr
    {
        get
        {
            return GetService("CubeReleaseMgr.service") as ICubeReleaseMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        lblMessage.Visible = false;
        //TODO: Add code for Page_Load here.
        if (!IsPostBack)
        {
            UpdateView();
        }
    }
	
	protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Edit1.Back += new System.EventHandler(this.Edit1_Back);
        New1.Back += new EventHandler(New1_Back);
        New1.Submit += new EventHandler(New1_Submit);
    }

	//Init the page by current user permissions
    protected override void InitByPermission()
    {
		base.InitByPermission();
        
		btnNew.Visible = PermissionAdd;
		
		//TODO: Add other permission init codes here.
    }

    //The event handler when user click button "Back" button on New page.
    void New1_Back(object sender, EventArgs e)
    {
        New1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;
		//TODO: Add other event handler code here.
    }

    void New1_Submit(object sender, EventArgs e)
    {
        New1.Visible = false;
        pnlMain.Visible = false;
        Edit1.Visible = true;
        Edit1.TheCubeRole = TheService.LoadCubeRoleWithAllInfo(New1.NewCubeRole.Id);
        Edit1.UpdateView();
    }
	
	//The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
    }

	//Do data query and binding.
    private void UpdateView()
    {
        IList<CubeRole> result = TheService.FindCubeRoleByNameAndDescription(txtRoleName.Text, txtRoleDescription.Text);        
        int recordCount = 0;
        if (result != null)
        {
            recordCount = result.Count;
        }
        lblRecordCount.Text = string.Format("Total {0} Records", recordCount);
        gvList.DataSource = result;
        gvList.DataBind();        
    }

	//The event handler when user click button "Back" on New page.
    protected void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        New1.Visible = true;
        New1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void btnUpdateCube_Click(object sender, EventArgs e)
    {
        IList<int> idList = GetSelectIdList(gvList);

        try
        {
            TheCubeReleaseMgr.UploadRoleToCube(idList);
            lblMessage.Text = "Update Cube Role successfully.";
            lblMessage.Visible = true;
        }
        catch (Exception ex)
        {
            lblMessage.Text = "Update Cube Role fail. " + ex.Message;
            lblMessage.Visible = true;
        }

        UpdateView();
    }

	//The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IList<int> idList = GetSelectIdList(gvList);

        TheService.DeleteCubeRole(idList);

        UpdateView();
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)(gvList.SelectedDataKey.Value);
        Edit1.TheCubeRole = TheService.LoadCubeRoleWithAllInfo(id);
        Edit1.UpdateView();
        Edit1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}