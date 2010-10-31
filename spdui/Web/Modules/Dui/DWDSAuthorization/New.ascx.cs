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
public partial class Modules_Dui_DWDSAuthorization_New : ModuleBase
{
    public event EventHandler Back;

    public DWDataSource TheDWDataSource
    {
        get
        {
            return (DWDataSource)ViewState["TheDWDataSource"];
        }
        set
        {
            ViewState["TheDWDataSource"] = value;
        }
    }


    //Get the logger
    private static ILog log = LogManager.GetLogger("DWDSMaintenance");

    //The entity service
    protected IDWDataSourceMgr TheService
    {
        get
        {
            return GetService("DWDataSourceMgr.service") as IDWDataSourceMgr;
        }
    }

    protected IDWDataSourceParameterMgr TheDWParameterService
    {
        get
        {
            return GetService("DWDataSourceParameterMgr.service") as IDWDataSourceParameterMgr;
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

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewOperator1.Back += new System.EventHandler(this.NewOperator1_Back);
    }

    void NewOperator1_Back(object sender, EventArgs e)
    {
        NewOperator1.Visible = false;
        TheDWDataSource.DWDataSourceOperatorList = TheService.FindDWDataSourceOperatorByDWDataSourceId(TheDWDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
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
        txtName.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.Name.ToString() : String.Empty;
        txtType.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.DSType.ToString() : String.Empty;
        txtDescription.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.Description.ToString() : String.Empty;
        txtStartDate.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.QueryStartDate : String.Empty;
        txtEndDate.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.QueryEndDate : String.Empty;

        if (TheDWDataSource != null && TheDWDataSource.Id != 0)
        {
            gvOperatorList.DataSource = TheDWDataSource.DWDataSourceOperatorList;
            gvOperatorList.DataBind();
            gvOperatorList.Visible = true;
            btnAddOperator.Visible = true;
            btnDeleteOperator.Visible = true;
        }
        else
        {
            gvOperatorList.Visible = false;
            btnAddOperator.Visible = false;
            btnDeleteOperator.Visible = false;
        }

        lblMessage.Text = String.Empty;
        lblMessage.Visible = false;
    }

    protected void btnAddOperator_Click(object sender, EventArgs e)
    {
        NewOperator1.TheDWDataSourceOperators = TheService.FindDWOperatorByDSIdAndAllowType(TheDWDataSource.Id, "VIEWER");
        NewOperator1.SetDataSourceId(TheDWDataSource.Id);
        NewOperator1.UpdateView();
        NewOperator1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteOperator_Click(object sender, EventArgs e)
    {
        try
        {

            TheService.DeleteDWDataSourceOperator(GetSelectIdList(gvOperatorList));

            //re-load the data source
            TheDWDataSource = TheService.LoadDWDataSource(TheDWDataSource.Id);

            UpdateView();
        }
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this operator have been used, can not be deleted";
        }
    }
}