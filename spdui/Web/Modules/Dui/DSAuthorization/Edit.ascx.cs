using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.Dui;
using Dndp.Persistence.Entity.Dui;
using Dndp.Service.Dui;
public partial class Modules_Dui_DSAuthorization_Edit : ModuleBase
{
    public event EventHandler Back;

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
        //TODO: Add code for Page_Load here.
    }


    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewOperator1.Back += new System.EventHandler(this.NewOperator1_Back);
        NewCategory1.Back += new System.EventHandler(this.NewCategory1_Back);
    }

    void NewOperator1_Back(object sender, EventArgs e)
    {
        NewOperator1.Visible = false;
        TheDataSource.DataSourceOperatorList = TheService.FindDataSourceOperatorByDataSourceId(TheDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewCategory1_Back(object sender, EventArgs e)
    {
        NewCategory1.Visible = false;
        TheDataSource.DataSourceCategoryList = TheService.FindDataSourceCategoryByDataSourceId(TheDataSource.Id, false, this.CurrentUser);
        UpdateView();
        pnlMain.Visible = true;
    }

    public DataSource TheDataSource
    {
        get
        {
            return (DataSource)ViewState["TheDataSource"];
        }
        set
        {
            ViewState["TheDataSource"] = value;
        }
    }

    //Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        txtName.Text = TheDataSource.Name;
        txtDescription.Text = TheDataSource.Description;
        txtType.Text = TheDataSource.DSType;

        gvOperatorList.DataSource = TheDataSource.DataSourceOperatorList;
        gvOperatorList.DataBind();

        gvCategoryList.DataSource = TheDataSource.DataSourceCategoryList;
        gvCategoryList.DataBind();

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

    protected void btnAddOperator_Click(object sender, EventArgs e)
    {
        NewOperator1.TheDataSourceOperators = TheService.FindOperatorByDSIdAndAllowType(TheDataSource.Id, "OWNER");
        NewOperator1.SetDataSourceId(TheDataSource.Id);
        NewOperator1.UpdateView();
        NewOperator1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteOperator_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteDataSourceOperator(GetSelectIdList(gvOperatorList));

            //re-load the data source
            TheDataSource = TheService.LoadDataSource(TheDataSource.Id);

            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }

    protected void lbtnCategoryName_Click(object sender, EventArgs e)
    {
        int dsCategoryId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewCategory1.SetDataSourceCategoryId(dsCategoryId);
        NewCategory1.UpdateView();
        NewCategory1.Visible = true;
        NewCategory1.SetDataSourceId(TheDataSource.Id);
        pnlMain.Visible = false;
    }
}