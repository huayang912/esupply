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

using System.Data.SqlClient;

//TODO:Add other using statements here.

public partial class Modules_OffLineReport_BatchMaintenance_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;

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

    private ReportBatch _newReportBatch;
    public ReportBatch NewReportBatch
    {
        get
        {
            return _newReportBatch;
        }
        set
        {
            _newReportBatch = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            UpdateView();
        }
    }
	
	//The event handler when user click button "Submit"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
        string dsName = txtName.Text.Trim();
        if (dsName.Length == 0)
        {
            lblMessage.Text = "Report Batch Name can not be empty";
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Visible = false;
            NewReportBatch = new ReportBatch();
            SessionHelper sessionHelper = new SessionHelper(Page);
            NewReportBatch.Name = dsName;
            NewReportBatch.Description = txtDescription.Text.Trim();
            NewReportBatch.BatchType = txtType.Text.Trim();

            NewReportBatch.EmailBody = txtBody.Text.Trim();
            NewReportBatch.EMailSubject = txtSubject.Text.Trim();

            NewReportBatch.ActiveFlag = 1;
            NewReportBatch.CreateBy = sessionHelper.CurrentUser;
            NewReportBatch.CreateDate = System.DateTime.Now;
            NewReportBatch.LastUpdateBy = sessionHelper.CurrentUser;
            NewReportBatch.LastUpdateDate = System.DateTime.Now;
            try
            {
                TheService.CreateReportBatch(NewReportBatch);
                lblMessage.Text = "Data Insert Successful";
                lblMessage.Visible = true;
                btnSubmit.Visible = false;

                if (Submit != null)
                {
                    Submit(this, null);
                }
            }
            catch (SqlException ex)
            {
                lblMessage.Text = ex.Message;
                lblMessage.Visible = true;
            }
        }
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
        //Add code to clear the view.
        txtName.Text = String.Empty;
        txtDescription.Text = String.Empty;
        txtType.Text = String.Empty;
        btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }
}