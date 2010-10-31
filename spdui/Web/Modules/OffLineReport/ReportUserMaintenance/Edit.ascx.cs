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
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;
using Dndp.Persistence.Entity.Distribution;

//TODO: Add other using statements here.

public partial class Modules_OffLineReport_ReportUserMaintenance_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("ReportUserMaintenance");    

	//The entity service
    protected IReportUserMgr TheService
    {
        get
        {
            return GetService("ReportUserMgr.service") as IReportUserMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		//TODO: Add code for Page_Load here.
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewUserReport1.Back += new System.EventHandler(this.NewUserReport1_Back);
        NewUserParameter1.Back += new System.EventHandler(this.NewUserParameter1_Back);
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdate.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
    }

    public ReportUser TheReportUser
    {
        get
        {
            return (ReportUser)ViewState["TheReportUser"];
        }
        set
        {
            ViewState["TheReportUser"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        //TODO: Add code here.
        txtDistributionUserName.Text = TheReportUser.TheUser.Name;
        txtName.Text = TheReportUser.Name;
        //txtEmail.Text = TheReportUser.EMAIL;
        txtDescription.Text = TheReportUser.Description;
        txtReportSite.Text = TheReportUser.PortalSite;
        txtReportLibrary.Text = TheReportUser.PortalDocumentLibrary;
        txtReportReadUsers.Text = TheReportUser.PortalReadUserList;
        txtReportFullControlUsers.Text = TheReportUser.PortalFullControlUserList;
        //txtCubeSite.Text = TheReportUser.CubeSite;
        //txtCubeLibrary.Text = TheReportUser.CubeDocumentLibrary;
        //txtCubeReadUsers.Text = TheReportUser.CubeReadUserList;
        //txtCubeFullControlUsers.Text = TheReportUser.CubeFullControlUserList;

        gvReportList.DataSource = TheReportUser.ReportList;
        gvReportList.DataBind();

        gvParameterList.DataSource = TheReportUser.ReportParameterList;
        gvParameterList.DataBind();

        lblMessage.Visible = false;
    }

	//Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The event handler when user click button "Back" button on NewReportSheet page.
    void NewUserParameter1_Back(object sender, EventArgs e)
    {
        NewUserParameter1.Visible = false;
        TheReportUser.ReportParameterList = TheService.FindParameterByReportUserId(TheReportUser.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    //The event handler when user click button "Back" button on NewReportSheet page.
    void NewUserReport1_Back(object sender, EventArgs e)
    {
        NewUserReport1.Visible = false;
        TheReportUser.ReportList = TheService.FindReportByReportUserId(TheReportUser.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    //Event handler when user click button "Update"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = true;

        string name = txtName.Text.Trim();
        if (name.Length == 0)
        {
            lblMessage.Text = "User name cannot be empty.";
            return;
        }

        TheReportUser.Name = name;
        //TheReportUser.EMAIL = txtEmail.Text.Trim();
        TheReportUser.Description = txtDescription.Text.Trim();
        TheReportUser.PortalSite = txtReportSite.Text.Trim();
        TheReportUser.PortalDocumentLibrary = txtReportLibrary.Text.Trim();
        TheReportUser.PortalReadUserList = txtReportReadUsers.Text.Trim();
        TheReportUser.PortalFullControlUserList = txtReportFullControlUsers.Text.Trim();
        //TheReportUser.CubeSite = txtCubeSite.Text.Trim();
        //TheReportUser.CubeDocumentLibrary = txtCubeLibrary.Text.Trim();
        //TheReportUser.CubeReadUserList = txtCubeReadUsers.Text.Trim();
        //TheReportUser.CubeFullControlUserList = txtCubeFullControlUsers.Text.Trim();
        
        try
        {
            TheService.UpdateReportUser(TheReportUser);
            lblMessage.Text = "Update successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteReportUser(TheReportUser.Id);
            if (Back != null)
            {
                Back(this, e);
            }
        }
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this report user have been used, can not be deleted";
        }
    }

    protected void btnAddReportSheet_Click(object sender, EventArgs e)
    {
        NewUserReport1.TheReportUserSheet = (TheService.FindReportByReportUserId(TheReportUser.Id) as IList<ReportUserSheet>);
        NewUserReport1.SetReportUserId(TheReportUser.Id);
        NewUserReport1.UpdateView();
        NewUserReport1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteReportSheet_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteReportUserSheet(GetSelectIdList(gvReportList));

            //re-load the data source
            TheReportUser = TheService.LoadReportUser(TheReportUser.Id);

            UpdateView();
        }
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this report sheet have been used, can not be deleted";
        }
    }

    protected void lbtnParameterName_Click(object sender, EventArgs e)
    {
        int paraId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewUserParameter1.TheReportUserSheetParameter = TheService.LoadReportUserSheetParameter(paraId);
        NewUserParameter1.UpdateView();
        NewUserParameter1.Visible = true;
        NewUserParameter1.SetReportUserId(TheReportUser.Id);
        pnlMain.Visible = false;
    }

    protected void btnDeleteParameter_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteReportUserSheetParameter(GetSelectIdList(gvParameterList));

            //re-load the data source
            TheReportUser = TheService.LoadReportUser(TheReportUser.Id);

            UpdateView();
        }
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this parameter have been used, can not be deleted";
        }
    }

    protected void btnAddParameter_Click(object sender, EventArgs e)
    {
        NewUserParameter1.Visible = true;
        NewUserParameter1.TheReportUserSheetParameter = null;
        NewUserParameter1.SetReportUserId(TheReportUser.Id);
        NewUserParameter1.UpdateView();
        pnlMain.Visible = false;
    }

    //private IList<int> GetSelectIdList(GridView gv)
    //{
    //    IList<int> idList = new List<int>();

    //    foreach (GridViewRow row in gv.Rows)
    //    {
    //        CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
    //        if (cbSelect.Checked)
    //        {
    //            idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
    //        }
    //    }

    //    return idList;
    //}
}