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

//TODO: Add other using statements here.

public partial class Modules_Dui_DSMaintenance_Edit : ModuleBase
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
        NewField1.Back += new System.EventHandler(this.NewField1_Back);
        NewCategory1.Back += new System.EventHandler(this.NewCategory1_Back);
        NewWithDrawTable1.Back += new System.EventHandler(this.NewWithDrawTable1_Back);
        NewRule1.Back += new System.EventHandler(this.NewRule1_Back);
        NewOperator1.Back += new System.EventHandler(this.NewOperator1_Back);
    }

    //The event handler when user click button "Back" button on NewField page.
    void NewField1_Back(object sender, EventArgs e)
    {
        NewField1.Visible = false;
        TheDataSource.DataSourceFieldList = TheService.FindDataSourceFieldByDataSourceId(TheDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewCategory1_Back(object sender, EventArgs e)
    {
        NewCategory1.Visible = false;
        TheDataSource.DataSourceCategoryList = TheService.FindDataSourceCategoryByDataSourceId(TheDataSource.Id, true);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewWithDrawTable1_Back(object sender, EventArgs e)
    {
        NewWithDrawTable1.Visible = false;
        TheDataSource.DataSourceWithDrawTableList = TheService.FindDataSourceWithDrawTableByDataSourceId(TheDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
    }
    void NewRule1_Back(object sender, EventArgs e)
    {
        NewRule1.Visible = false;
        TheDataSource.DataSourceRuleList = TheService.FindDataSourceRuleByDataSourceId(TheDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewOperator1_Back(object sender, EventArgs e)
    {
        NewOperator1.Visible = false;
        TheDataSource.DataSourceOperatorList = TheService.FindDataSourceOperatorByDataSourceId(TheDataSource.Id);
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
        txtDataStructure.Text = TheDataSource.DataStructure;
        txtRuleContent.Text = TheDataSource.DWQuerySQL;
        txtType.Text = TheDataSource.DSType;

        gvFieldList.DataSource = TheDataSource.DataSourceFieldList;
        gvFieldList.DataBind();

        gvOperatorList.DataSource = TheDataSource.DataSourceOperatorList;
        gvOperatorList.DataBind();

        gvRuleList.DataSource = TheDataSource.DataSourceRuleList;
        gvRuleList.DataBind();

        gvCategoryList.DataSource = TheDataSource.DataSourceCategoryList;
        gvCategoryList.DataBind();

        gvWithDrawTableList.DataSource = TheDataSource.DataSourceWithDrawTableList;
        gvWithDrawTableList.DataBind();

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

    //Event handler when user click button "Update"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = true;

        string name = txtName.Text.Trim();
        if (name.Length == 0)
        {
            lblMessage.Text = "Data source name cannot be empty.";
            return;
        }

        string description = txtDescription.Text.Trim();
        string dataStructure = txtDataStructure.Text.Trim();

        TheDataSource.Name = name;
        TheDataSource.Description = description;
        TheDataSource.DataStructure = dataStructure;
        TheDataSource.DWQuerySQL = txtRuleContent.Text.Trim();
        TheDataSource.DSType = txtType.Text.Trim();
        TheDataSource.LastUpdateBy = CurrentUser;
        TheDataSource.LastUpdateDate = DateTime.Now;

        try
        {
            TheService.UpdateDataSource(TheDataSource);
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
        TheService.DeleteDataSource(TheDataSource.Id);
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void btnDeleteField_Click(object sender, EventArgs e)
    {
        TheService.DeleteDataSourceField(GetSelectIdList(gvFieldList));
        
        //re-load the data source
        TheDataSource = TheService.LoadDataSource(TheDataSource.Id);
        
        UpdateView();
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
        TheService.DeleteDataSourceOperator(GetSelectIdList(gvOperatorList));

        //re-load the data source
        TheDataSource = TheService.LoadDataSource(TheDataSource.Id);

        UpdateView();
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

    protected void btnDeleteRule_Click(object sender, EventArgs e)
    {
        TheService.DeleteDataSourceRule(GetSelectIdList(gvRuleList));

        //re-load the data source
        TheDataSource = TheService.LoadDataSource(TheDataSource.Id);

        UpdateView();
    }

    protected void btnDeleteCategory_Click(object sender, EventArgs e)
    {
        TheService.DeleteDataSourceCategory(GetSelectIdList(gvCategoryList));

        //re-load the data source
        TheDataSource = TheService.LoadDataSource(TheDataSource.Id);

        UpdateView();
    }

    protected void btnDeleteWithDrawTable_Click(object sender, EventArgs e)
    {
        TheService.DeleteDataSourceWithDrawTable(GetSelectIdList(gvWithDrawTableList));

        //re-load the data source
        TheDataSource = TheService.LoadDataSource(TheDataSource.Id);

        UpdateView();
    }

    protected void btnAddField_Click(object sender, EventArgs e)
    {        
        NewField1.Visible = true;
        NewField1.DataSourceId = TheDataSource.Id;
        NewField1.UpdateView();
        pnlMain.Visible = false;
    }
    protected void btnAddRule_Click(object sender, EventArgs e)
    {        
        NewRule1.Visible = true;
        NewRule1.TheDataSourceRule = null;
        NewRule1.SetDataSourceId(TheDataSource.Id);
        NewRule1.UpdateView();
        pnlMain.Visible = false;
    }
    protected void lbtnRuleName_Click(object sender, EventArgs e)
    {
        int dsRuleId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewRule1.TheDataSourceRule = TheService.LoadDataSourceRule(dsRuleId);
        NewRule1.SetDataSourceId(TheDataSource.Id);
        NewRule1.UpdateView();
        NewRule1.Visible = true;
        pnlMain.Visible = false;
    }
    protected void lbtnCategoryName_Click(object sender, EventArgs e)
    {
        int dsCategoryId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewCategory1.SetDataSourceId(TheDataSource.Id);
        NewCategory1.SetDataSourceCategoryId(dsCategoryId);
        NewCategory1.UpdateView();
        NewCategory1.Visible = true;
        pnlMain.Visible = false;
    }
    protected void btnAddCategory_Click(object sender, EventArgs e)
    {
        NewCategory1.Visible = true;
        NewCategory1.SetDataSourceId(TheDataSource.Id);
        pnlMain.Visible = false;
    }
    protected void btnAddWithDrawTable_Click(object sender, EventArgs e)
    {
        NewWithDrawTable1.Visible = true;
        NewWithDrawTable1.SetDataSourceId(TheDataSource.Id);
        pnlMain.Visible = false;
    }
}