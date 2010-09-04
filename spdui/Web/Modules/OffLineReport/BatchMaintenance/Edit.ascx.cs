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

//TODO: Add other using statements here.

public partial class Modules_OffLineReport_BatchMaintenance_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("BatchMaintenance");
	
	//The entity service
	protected IReportBatchMgr TheService
    {
        get
        {
            return GetService("ReportBatchMgr.service") as IReportBatchMgr;
        }
    }

    public ReportBatch TheReportBatch
    {
        get
        {
            return (ReportBatch)ViewState["TheReportBatch"];
        }
        set
        {
            ViewState["TheReportBatch"] = value;
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewBatchReport1.Back += new System.EventHandler(this.NewBatchReport1_Back);
        NewBatchUser1.Back += new System.EventHandler(this.NewBatchUser1_Back);
    }


	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        //TODO: Add code here.
        txtName.Text = TheReportBatch.Name;
        txtDescription.Text = TheReportBatch.Description;
        txtType.Text = TheReportBatch.BatchType;
        txtPreRunSQL.Text = TheReportBatch.PreRunSQL;
        txtPostRunSQL.Text = TheReportBatch.PostRunSQL;

        txtSubject.Text = TheReportBatch.EMailSubject;
        txtBody.Text = TheReportBatch.EmailBody;

        gvReportList.DataSource = TheReportBatch.ReportList;
        gvReportList.DataBind();

        gvUserList.DataSource = TheReportBatch.ReportUserList;
        gvUserList.DataBind();

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

    //The event handler when user click button "Back" button on NewBatchReport1 page.
    void NewBatchReport1_Back(object sender, EventArgs e)
    {
        NewBatchReport1.Visible = false;
        TheReportBatch.ReportList = TheService.FindReportByBatchId(TheReportBatch.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    //The event handler when user click button "Back" button on NewBatchUser1 page.
    void NewBatchUser1_Back(object sender, EventArgs e)
    {
        NewBatchUser1.Visible = false;
        TheReportBatch.ReportUserList = TheService.FindUserByBatchId(TheReportBatch.Id);
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
            lblMessage.Text = "Batch name cannot be empty.";
            return;
        }

        string description = txtDescription.Text.Trim();

        TheReportBatch.Name = name;
        TheReportBatch.Description = description;
        TheReportBatch.BatchType = txtType.Text.Trim();
        TheReportBatch.PreRunSQL = txtPreRunSQL.Text.Trim();
        TheReportBatch.PostRunSQL = txtPostRunSQL.Text.Trim();

        TheReportBatch.EmailBody = txtBody.Text.Trim();
        TheReportBatch.EMailSubject = txtSubject.Text.Trim();

        TheReportBatch.LastUpdateBy = CurrentUser;
        TheReportBatch.LastUpdateDate = DateTime.Now;

        try
        {
            TheService.UpdateReportBatch(TheReportBatch);
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
        TheService.DeleteReportBatch(TheReportBatch.Id);
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void btnAddReport_Click(object sender, EventArgs e)
    {
        NewBatchReport1.TheReportBatchReports = (TheService.FindReportByBatchId(TheReportBatch.Id) as IList<ReportBatchReports>);
        NewBatchReport1.SetReportBatchId(TheReportBatch.Id);
        NewBatchReport1.UpdateView();
        NewBatchReport1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteReport_Click(object sender, EventArgs e)
    {
        TheService.DeleteReportBatchReports(GetSelectIdList(gvReportList));

        //re-load the data source
        TheReportBatch = TheService.LoadReportBatch(TheReportBatch.Id);

        UpdateView();
    }

    protected void btnAddUser_Click(object sender, EventArgs e)
    {
        NewBatchUser1.TheReportBatchId = this.TheReportBatch.Id;
        NewBatchUser1.UpdateView();
        NewBatchUser1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteUser_Click(object sender, EventArgs e)
    {
        TheService.DeleteReportBatchUser(GetSelectUserIdList(gvUserList));

        //re-load the data source    
        TheReportBatch = TheService.LoadReportBatch(TheReportBatch.Id);

        UpdateView();
    }

    private IList<int> GetSelectUserIdList(GridView gv)
    {
        IList<int> idList = new List<int>();

        foreach (GridViewRow row in gv.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("ckbUserItem");
            if (cbSelect.Checked)
            {
                idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
            }
        }

        return idList;
    }
}