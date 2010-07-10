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

public partial class Modules_Cube_CubeRelease_Main : ModuleBase
{
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

    protected ICubeAuthorizationMgr TheAuthService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateView();
        if (IsPostBack)
        {
            lblMessage.Text = "";
        }
    }
	
	protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        ProcessEdit1.Back += new System.EventHandler(this.ProcessEdit1_Back);
        History1.Back += new EventHandler(this.History1_Back);
		
		//TODO: Add other init code here.
    }

	//Init the page by current user permissions
    protected override void InitByPermission()
    {
		base.InitByPermission();       
		
		//TODO: Add other permission init codes here.
    }

    //The event handler when user click button "Back" button on New page.
    void History1_Back(object sender, EventArgs e)
    {
        History1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

	//Do data query and binding.
    private void UpdateView()
    {
        gvCubeReleaseList.DataSource = TheProcessService.FindAllCubeForCubeReleaseByUserId(CurrentUser.Id);
        gvCubeReleaseList.DataBind();
    }

	//The event handler when user click button "Back" on New page.
    protected void ProcessEdit1_Back(object sender, EventArgs e)
    {
        ProcessEdit1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        string Id = ((LinkButton)sender).CommandArgument;
        History1.CubeId = Id;
        History1.Visible = true;
        pnlMain.Visible = false;
        History1.UpdateView();
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

    protected void lbtnRelease_Click(object sender, EventArgs e)
    {
        // Modified by vincent 2007-11-12 begin
        try
        {
            int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
            CubeProcess process = TheProcessService.LoadCubeProcess(Id);
            TheAuthService.SynchronizeVisualtotal(process.TheCube.Id);
            TheService.ReleaseCube(process, CurrentUser);
            UpdateView();
            lblMessage.Text = "Release Cube Successfully.";
        }
        catch (Exception ee)
        {
            throw ee;
        }
        // Modified by vincent 2007-11-12 end
    }

    protected void lbtnUpdateCubeRole_Click(object sender, EventArgs e)
    {
        // Modified by vincent 2007-11-09 begin
        //int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        //TheService.UploadRoleToCube(Id);
        string[] ids= ((LinkButton)sender).CommandArgument.Split(',');
        int cubeid = Convert.ToInt32(ids[0]);
        int processid = Convert.ToInt32(ids[1]);
        CubeProcess cp = TheProcessService.FindCubeProcessWithAllInfoById(processid);
        cp.Status = CubeProcess.PROCESS_STATUS_UpdateRole;
        TheProcessService.UpdateCubeProcess(cp);
        
        try
        {
            TheService.UploadRoleToCube(cubeid);
            
            lblMessage.Text = "Update Cube Role Successfully.";
        }
        catch (Exception ee)
        {
            throw ee;
        }
        if (TheService.IsProcessCancelled(cubeid))
        {
            cp.Status = CubeProcess.PROCESS_STATUS_UpdateRoleCancelled;
        }
        else
        {
            cp.Status = CubeProcess.PROCESS_STATUS_UpdateRoleCompleted;
        }

        TheProcessService.UpdateCubeProcess(cp);
        UpdateView();
        // Modified by vincent 2007-11-09 end
    }

    protected void lbtnUpdateDescription_Click(object sender, EventArgs e)
    {
        // Modified by vincent 2007-11-12 begin 
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        try
        {
            foreach (GridViewRow gv in gvCubeReleaseList.Rows)
            {
                LinkButton lb = (LinkButton)gv.FindControl("lbtnUpdateDescription");
                if (lb.CommandArgument == Id.ToString())
                {
                    TextBox tb = (TextBox)gv.FindControl("txtLastProcessDescription");

                    TheService.UpdateProcessDescription(Id, tb.Text);
                }
            }
            lblMessage.Text = "Update Cube Description Successfully.";
        }
        catch (Exception ee)
        {
            throw ee;
        }
        // Modified by vincent 2007-11-12 end
    }

    //protected void lbtnRollback_Click(object sender, EventArgs e)
    //{

    //}

    //protected void gvCubeReleaseList_RowDataBound(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        Label lbStatus = (Label)e.Row.FindControl("lbStatus");

    //        LinkButton lbtnRollback = (LinkButton)e.Row.FindControl("lbtnRollback");

    //        if (lbStatus.Text == CubeRelease.RELEASE_STATUS_Failed)
    //        {
    //            lbtnRollback.Visible = true;
    //        }
    //        else
    //        {
    //            lbtnRollback.Visible = false;
    //        }
    //    }
    //}
    // Modified By Vincent at 2007-11-08 begin
    protected void gvCubeReleaseList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        LinkButton lbrelease = (LinkButton)e.Row.FindControl("lbtnRelease");
        LinkButton lbudpaterole = (LinkButton)e.Row.FindControl("lbtnUpdateCubeRole");
        LinkButton lbwarm = (LinkButton)e.Row.FindControl("lbtnWarmCache");
        LinkButton lbupdateDescription = (LinkButton)e.Row.FindControl("lbtnUpdateDescription");
        
        if (lbrelease != null)
        {
            string processid = lbrelease.CommandArgument.ToString();
            
            CubeProcess process = TheProcessService.FindCubeProcessWithAllInfoById(Convert.ToInt32(processid));
            int cubeId = process.TheCube.Id;
            
            if (process.Status == CubeProcess.PROCESS_STATUS_ProcessStart ||
                process.Status == CubeProcess.PROCESS_STATUS_UpdateRole || 
                TheService.IsDistributing(cubeId) ||
                TheService.IsReleasing(cubeId) )
            {
                lbrelease.Visible = false;
                lbwarm.Visible = false;
            }
            else
            {
                lbrelease.Visible = true;
                lbwarm.Visible = true;
            }
        }

        if (lbudpaterole != null)
        {
            string processid = lbrelease.CommandArgument.ToString();
            CubeProcess process = TheProcessService.FindCubeProcessWithAllInfoById(Convert.ToInt32(processid));
            if (process.Status == CubeProcess.PROCESS_STATUS_UpdateRole || 
                process.Status == CubeProcess.PROCESS_STATUS_ProcessStart)
            {
                lbudpaterole.Visible = false;
                lbupdateDescription.Visible = false;
            }
            else
            {
                lbudpaterole.Visible = true;
                lbupdateDescription.Visible = true;
            }
            lbudpaterole.CommandArgument += "," + lbrelease.CommandArgument;
        }

    }
    // Modified By Vincent at 2007-11-08 end
    // Modified By Vincent at 2007-11-13 begin
    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        TheService.WarmCache();
        lblMessage.Text = "SP Cube Warm Cacher has been started.";
    }
    // Modified By Vincent at 2007-11-13 end
}