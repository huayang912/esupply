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
using Dndp.Persistence.Dao.Dui;
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Dui;
using System.Data.SqlClient;

//TODO:Add other using statements here.

public partial class Modules_Dui_DSMaintenance_New : ModuleBase
{
	public event EventHandler Back;

    public event EventHandler Submit;

    private DataSource _newDataSource;
    public DataSource NewDataSource
    {
        get
        {
            return _newDataSource;
        }
        set
        {
            _newDataSource = value;
        }
    }
    
	//Get the logger
	private static ILog log = LogManager.GetLogger("DSMaintenance");
	
	//The entity service
	protected IDataSourceMgr TheService
    {
        get
        {
            return GetService("DataSourceMgr.service") as IDataSourceMgr;
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
        string dsName = txtName.Text;
        if (dsName.Trim().Length == 0)
        {
            lblMessage.Text = "Data Source Name can not be empty";
            lblMessage.Visible = true;
        }
        else if (dsName.IndexOf(" ") != -1)
        {
            lblMessage.Text = "Please remove space in Data Source Name";
            lblMessage.Visible = true;
        }
        else
        {
            lblMessage.Visible = false;
            NewDataSource = new DataSource();
            SessionHelper sessionHelper = new SessionHelper(Page);
            NewDataSource.Name = dsName;
            NewDataSource.Description = txtDescription.Text.Trim();
            NewDataSource.DWQuerySQL = txtRuleContent.Text.Trim();
            NewDataSource.DSType = txtType.Text.Trim();
            NewDataSource.ActiveFlag = 1;
            NewDataSource.CreateBy = sessionHelper.CurrentUser;
            NewDataSource.CreateDate = System.DateTime.Now;
            NewDataSource.LastUpdateBy = sessionHelper.CurrentUser;
            NewDataSource.LastUpdateDate = System.DateTime.Now;
            try
            {
                TheService.CreateDataSource(NewDataSource);
                lblMessage.Text = "Data Source Insert Successful";
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
        txtRuleContent.Text = string.Empty;
        txtType.Text = String.Empty;
        btnSubmit.Visible = true;
        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }
}