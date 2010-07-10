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

public partial class Modules_Cube_CubeRelease_History : ModuleBase
{
	public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("CubeRelease");
	
	//The entity service
	protected ICubeReleaseMgr TheService
    {
        get
        {
            return GetService("CubeReleaseMgr.service") as ICubeReleaseMgr;
        }
    }

    protected ICubeProcessMgr TheProcessService
    {
        get
        {
            return GetService("CubeProcessMgr.service") as ICubeProcessMgr;
        }
    }

    public string CubeId
    {
        get
        {
            return hfCubeId.Value;
        }
        set
        {
            hfCubeId.Value = value;
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ProcessEdit1.Back += new System.EventHandler(this.ProcessEdit1_Back);
    }

    //The event handler when user click button "Back" on New page.
    protected void ProcessEdit1_Back(object sender, EventArgs e)
    {
        ProcessEdit1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    protected void lbtnViewProcess_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeProcess process = TheProcessService.FindCubeProcessWithAllInfoById(Id);
        ProcessEdit1.TheCubeProcess = process;
        ProcessEdit1.Editable = false;
        ProcessEdit1.Visible = true;
        ProcessEdit1.UpdateView();
        pnlMain.Visible = false;
    }

	protected void Page_Load(object sender, EventArgs e)
	{
        
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
        gvCubeReleaseList.DataSource = TheService.FindAllCubeReleaseByCubeId(int.Parse(CubeId), CurrentUser);
        gvCubeReleaseList.DataBind();
    }
}