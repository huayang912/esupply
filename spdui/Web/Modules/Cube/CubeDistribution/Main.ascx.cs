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

public partial class Modules_Cube_CubeDistributionJob_Main : ModuleBase
{
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

    protected ICubeMgr TheCubeService
    {
        get
        {
            return GetService("CubeMgr.service") as ICubeMgr;
        }
    }

    // Modified by vincent at 2007-11-12 begin
    protected ICubeReleaseMgr TheReleaseService
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
    // Modified by vincent at 2007-11-12 end
    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateView();
    }
	
	protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        History1.Back += new System.EventHandler(this.History1_Back);
        New1.Back += new EventHandler(New1_Back);
		
		//TODO: Add other init code here.
    }

	//Init the page by current user permissions
    protected override void InitByPermission()
    {
		base.InitByPermission();
        
		//btnNew.Visible = PermissionAdd;
		
		//TODO: Add other permission init codes here.
    }

    //The event handler when user click button "Back" button on New page.
    void New1_Back(object sender, EventArgs e)
    {
        New1.Visible = false;        
        pnlMain.Visible = true;
        UpdateView();
    }
	

	//Do data query and binding.
    private void UpdateView()
    {
        lblMessage.Text = string.Empty;
        gvCubeDistributionList.DataSource = TheCubeService.FindAllCubeForCubeDistribution();
        gvCubeDistributionList.DataBind();
    }

	//The event handler when user click button "Back" on New page.
    protected void History1_Back(object sender, EventArgs e)
    {
        History1.Visible = false;
        
		//TODO: Add other code here.
    }

	//The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        //TODO: Add code here to perform the delete action.

        UpdateView();
    }

    protected void lbtnNew_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        New1.TheJob = TheService.CreateNewCubeDistributionJobByCubeId(Id, CurrentUser);
        New1.ClearMessage();
        New1.Editable = true;
        New1.Visible = true;
        New1.UpdateView();
        pnlMain.Visible = false;
    }


    protected void lbtnSubmit_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeDistributionJob job = TheService.LoadCubeDistributionJob(Id);
        job.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Submit;
        job.UpdateDate = DateTime.Now;

        try
        {
            TheService.UpdateCubeDistributionJob(job);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }        
    }

    protected void lbtnRestart_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeDistributionJob job = TheService.LoadCubeDistributionJob(Id);
        job.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Submit;
        job.UpdateDate = DateTime.Now;

        try
        {
            TheService.UpdateCubeDistributionJob(job);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }        
    }

    protected void lbtnCancel_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeDistributionJob job = TheService.LoadCubeDistributionJob(Id);
        job.Status = CubeDistributionJob.DISTRIBUTION_STATUS_Cancelled;
        job.UpdateDate = DateTime.Now;

        try
        {
            TheService.UpdateCubeDistributionJob(job);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }        
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);

        try
        {
            TheService.DeleteCubeDistributionJob(Id);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }        
    }

    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        //int cubeId = Int32.Parse(((LinkButton)sender).CommandArgument);
        //CubeDefinition Cube = TheCubeService.LoadCube(cubeId);
        //Cube.TheLastestCubeDistributionJob = TheService.FindLastestCubeDistributionJobByCubeId(cubeId, CurrentUser.Id);
        //History1.TheCube = Cube;
        //History1.UpdateView();
        //History1.Visible = true;

        //pnlMain.Visible = false;
    }

    protected void lbtnEditJob_Click(object sender, EventArgs e)
    {
        int jobId = Int32.Parse(((LinkButton)sender).CommandArgument);
        pnlMain.Visible = false;
        New1.Editable = true;
        New1.ClearMessage();
        New1.Visible = true;
        New1.TheJob = TheService.FindCubeDistributionJobWithAllInfo(jobId);
        New1.UpdateView();

    }

    protected void gvCube_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvReportBatch.PageIndex = e.NewPageIndex;
        //UpdateView();
    }

    protected void gvCubeDistributionList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnStatus = (LinkButton)e.Row.FindControl("lbtnStatus");
            LinkButton btnNew = (LinkButton)e.Row.FindControl("lbtnNew");
            LinkButton btnSubmit = (LinkButton)e.Row.FindControl("lbtnSubmit");
            LinkButton btnRestart = (LinkButton)e.Row.FindControl("lbtnRestart");
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");

            if (!btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Submit)
                && !btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Running))
            {
                btnNew.Visible = true;
            }
            else
            {
                btnNew.Visible = false;
            }

            if (btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Pending))
            {
                btnSubmit.Visible = true;
            }
            else
            {
                btnSubmit.Visible = false;
            }

            if (btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Cancelled)
                || btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Failed))
            {
                btnRestart.Visible = true;
            }
            else
            {
                btnRestart.Visible = false;
            }

            if (btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Pending)
                || btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Failed)
                || btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Cancelled))
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }

            if (btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Submit)
                || btnStatus.Text.Trim().Equals(CubeDistributionJob.DISTRIBUTION_STATUS_Running))
            {

                btnCancel.Visible = true;
            }
            else
            {
                btnCancel.Visible = false;
            }
        }
    }
}