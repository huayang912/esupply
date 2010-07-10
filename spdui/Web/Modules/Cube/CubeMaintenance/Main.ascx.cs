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

public partial class Modules_Cube_Cube_Main : ModuleBase
{
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

    protected void Page_Load(object sender, EventArgs e)
    {
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
        //UpdateView();
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
        pnlMain.Visible = true;
        UpdateView();
    }

    void New1_Submit(object sender, EventArgs e)
    {
        New1.Visible = false;
        pnlMain.Visible = false;
        Edit1.Visible = true;
        Edit1.TheCube = New1.NewCube;
        Edit1.UpdateView();
    }
	
	//The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
		
		//TODO: Add other event handler code here.
    }

	//Do data query and binding.
    private void UpdateView()
    {
        IList<CubeDefinition> result = TheService.LoadAllActiveCube();
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
        pnlMain.Visible = true;
        UpdateView();
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {

        New1.Visible = true;
        pnlMain.Visible = false;
        New1.UpdateView();
	
    }

	//The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        IList<int> idList = new List<int>();

        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
            if (cbSelect.Checked)
            {
                idList.Add((int)(gvList.DataKeys[row.RowIndex].Value));
            }
        }
        TheService.DeleteCube(idList);
        UpdateView();
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)(gvList.SelectedDataKey.Value);
        Edit1.TheCube = TheService.FindCubeWtihFullInformationById(id);
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