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

public partial class Modules_Cube_CubeProcess_Main : ModuleBase
{
    //Get the logger
	private static ILog log = LogManager.GetLogger("CubeProcess");
	
	//The entity service
	protected ICubeProcessMgr TheService
    {
        get
        {
            return GetService("CubeProcessMgr.service") as ICubeProcessMgr;
        }
    }

    protected ICubeMgr TheCubeService
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
        History1.Back += new EventHandler(History1_Back);
        New1.Submit += new EventHandler(New1_Submit);
		//TODO: Add other init code here.
    }

    protected void New1_Submit(object sender, EventArgs e)
    {
        New1.Visible = false;
        pnlMain.Visible = false;
        Edit1.Editable = true;
        Edit1.Visible = true;
        Edit1.TheCubeProcess = New1.NewCubeProcess;
        Edit1.UpdateView();
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
        UpdateView();
        pnlMain.Visible = true;
        //TODO: Add other event handler code here.
    }

    //The event handler when user click button "Back" button on History page.
    void History1_Back(object sender, EventArgs e)
    {
        History1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;
		//TODO: Add other event handler code here.
    }	

	//Do data query and binding.
    private void UpdateView()
    {
        lblMessage.Text = string.Empty;
       gvCubeProcessList.DataSource = TheCubeService.FindAllCubeForCubeProcessByUserId(CurrentUser.Id);
       gvCubeProcessList.DataBind();
    }

	//The event handler when user click button "Back" on New page.
    protected void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;
		//TODO: Add other code here.
    }

    protected void btnHistory_Click(object sender, EventArgs e)
    {
        History1.Visible = true;
        History1.UpdateView();

        //TODO: Add othere code here.
    }

    protected void lbtnNewProcess_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeDefinition cube = TheCubeService.FindCubeWtihFullInformationById(Id);
        New1.TheCube = cube;
        New1.Visible = true;        
        New1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void lbtnProcess_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeProcess process = TheService.LoadCubeProcess(Id);
        process.Status = CubeProcess.PROCESS_STATUS_ProcessSubmit;
        process.ProcessUser = CurrentUser;
        try
        {
            TheService.UpdateCubeProcess(process);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lbtnReProcess_Click(object sender, EventArgs e)
    {
        int Id = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeProcess process = TheService.LoadCubeProcess(Id);
        process.Status = CubeProcess.PROCESS_STATUS_ProcessSubmit;
        process.ProcessUser = CurrentUser;
        try
        {
            TheService.UpdateCubeProcess(process);
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
        CubeProcess process = TheService.LoadCubeProcess(Id);
        process.Status = CubeProcess.PROCESS_STATUS_ProcessCancelled;
        process.ProcessUser = CurrentUser;
        try
        {
            TheService.UpdateCubeProcess(process);
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
            TheService.DeleteCubeProcess(Id);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        int cubeId = Int32.Parse(((LinkButton)sender).CommandArgument);
        CubeDefinition Cube = TheCubeService.LoadCube(cubeId);
        Cube.TheLastestCubeProcess = TheService.FindLastestCubeProcessByCubeId(cubeId, CurrentUser.Id);
        History1.TheCube = Cube;
        History1.UpdateView();
        History1.Visible = true;

        pnlMain.Visible = false; 
    }

    protected void lbtnEditProcess_Click(object sender, EventArgs e)
    {
        int processId = Int32.Parse(((LinkButton)sender).CommandArgument);        
        pnlMain.Visible = false;
        Edit1.Editable = true;
        Edit1.Visible = true;
        Edit1.TheCubeProcess = TheService.FindCubeProcessWithAllInfoById(processId);
        Edit1.UpdateView();

    }

    protected void btnValidationFinish_Click(object sender, EventArgs e)
    {
        UpdateView();
    }

    protected void gvCube_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        //gvReportBatch.PageIndex = e.NewPageIndex;
        //UpdateView();
    }

    protected string GenerateRevalidateString(CubeDefinition cube)
    {

        if (cube.TheLastestCubeProcess != null)
        {
            string status = cube.TheLastestCubeProcess.Status;
            if (status.Trim().Equals(CubeProcess.PROCESS_STATUS_WaitingValidate)
                || status.Trim().Equals(CubeProcess.PROCESS_STATUS_ValidatedFailed)
                || status.Trim().Equals(CubeProcess.PROCESS_STATUS_ValidatedPassed)
                || status.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessCancelled)
                || status.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessFailed))
            {
                cube.TheLastestCubeProcess.CubeProcessValidationResultList = TheService.FindCubeProcessValidationResultByProcessId(cube.TheLastestCubeProcess.Id, "SPDW");
                if (cube.TheLastestCubeProcess.CubeProcessValidationResultList != null
                    && cube.TheLastestCubeProcess.CubeProcessValidationResultList.Count > 0)
                {
                    return "<a href=\"#\" onclick=\"javascript:RevalidateAll('" + cube.TheLastestCubeProcess.Id + "','" + cube.TheLastestCubeProcess.CubeProcessValidationResultIds + "');\">[Validate All]</a>";
                }
                else
                {
                    return string.Empty;
                }

            }
            else
            {
                return string.Empty;
            }
            
        }
        else
        {
            return string.Empty;
        }
    }

    protected void gvCubeProcessList_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton btnStatus = (LinkButton)e.Row.FindControl("lbtnStatus");
            LinkButton btnNewProcess = (LinkButton)e.Row.FindControl("lbtnNewProcess");
            LinkButton btnValidation = (LinkButton)e.Row.FindControl("lbtnValidation");
            LinkButton btnProcess = (LinkButton)e.Row.FindControl("lbtnProcess");
            LinkButton btnReProcess = (LinkButton)e.Row.FindControl("lbtnReProcess");
            LinkButton btnDelete = (LinkButton)e.Row.FindControl("lbtnDelete");
            LinkButton btnCancel = (LinkButton)e.Row.FindControl("lbtnCancel");            
            // Modified by vincent at 2007-11-12 begin
            if (!btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSubmit)
                && !btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessStart)
                && !btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_UpdateRole))
            {
                btnNewProcess.Visible = true;
            }
            else
            {
                btnNewProcess.Visible = false;
            }
            // Modified by vincent at 2007-11-12 end

            if (btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ValidatedPassed))
            {
                btnProcess.Visible = true;
            }
            else
            {
                btnProcess.Visible = false;
            }

            if (btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessFailed)
                || btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessCancelled))
            {
                btnReProcess.Visible = true;
            }
            else
            {
                btnReProcess.Visible = false;
            }

            if (!btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSuccess)
                && !btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSubmit)
                && !btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessStart)
                && !btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_UpdateRole)
                && btnStatus.Text.Trim().Length != 0) 
            {
                btnDelete.Visible = true;
            }
            else
            {
                btnDelete.Visible = false;
            }
            // Modified by vincent at 2007-11-08 begin
            if (btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessSubmit)
                || btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_ProcessStart)
                || btnStatus.Text.Trim().Equals(CubeProcess.PROCESS_STATUS_UpdateRole))
            {

                btnCancel.Visible = true;
            }
            else
            {
                btnCancel.Visible = false;
            }
            // Modified by vincent at 2007-11-08 end
        }
    }
}