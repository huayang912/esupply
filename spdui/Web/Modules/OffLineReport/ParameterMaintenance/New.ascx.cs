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

public partial class Modules_OffLineReport_ParameterMaintenance_New : ModuleBase
{
	public event EventHandler Back;
    public event EventHandler Submit;
	//Get the logger
	private static ILog log = LogManager.GetLogger("ParameterMaintenance");
	
	//The entity service
	protected IReportParameterMgr TheService
    {
        get
        {
            return GetService("ReportParameterMgr.service") as IReportParameterMgr;
        }
    }

    private ReportParameter _newReportParameter;
    public ReportParameter NewReportParameter
    {
        get
        {
            return _newReportParameter;
        }
        set
        {
            _newReportParameter = value;
        }
    }

	protected void Page_Load(object sender, EventArgs e)
	{
		//Add code for Page_Load here.
	}
	
	//The event handler when user click button "Submit"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
		string dsName = txtName.Text.Trim();
        if (dsName.Length == 0)
        {
            lblMessage.Text = "Parameter Name can not be empty";
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Visible = false;
            NewReportParameter = new ReportParameter();
            SessionHelper sessionHelper = new SessionHelper(Page);
            NewReportParameter.Name = dsName;
            try
            {
                TheService.CreateReportParameter(NewReportParameter);
                lblMessage.Text = "Data Insert Successful";
                lblMessage.Visible = true;
                //btnSubmit.Visible = false;

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
    }
}