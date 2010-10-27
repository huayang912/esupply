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

public partial class Modules_Dui_DWDSMaintenance_New : ModuleBase
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

    private void InitDDL()
    {
        //Get all parameters
        IList<DWDataSourceParameter> parameters = TheDWParameterService.LoadAllActiveDWDataSourceParameter();
        ddlParameter.DataSource = parameters;
        ddlParameter.DataTextField = "Name";
        ddlParameter.DataValueField = "Name";
        ddlParameter.DataBind();
    }

	protected void Page_Load(object sender, EventArgs e)
	{        
		//Add code for Page_Load here.
        if (!IsPostBack)
        {
            InitDDL();
            UpdateView();            
        }
	}

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewOperator1.Back += new System.EventHandler(this.NewOperator1_Back);
        NewRule1.Back += new System.EventHandler(this.NewRule1_Back);
    }

    protected void btnInsertParameter_Click(object sender, EventArgs e)
    {
        txtQuerySQL.Text = txtQuerySQL.Text + Dndp.Utility.DataParameterHelper.GetParameterPlaceholder(ddlParameter.SelectedValue);
    }

    void NewOperator1_Back(object sender, EventArgs e)
    {
        NewOperator1.Visible = false;
        TheDWDataSource.DWDataSourceOperatorList = TheService.FindDWDataSourceOperatorByDWDataSourceId(TheDWDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    void NewRule1_Back(object sender, EventArgs e)
    {
        NewRule1.Visible = false;
        TheDWDataSource.DWDataSourceMergeRuleList = TheService.FindDWDataSourceMergeRuleByDWDataSourceId(TheDWDataSource.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

	//The event handler when user click button "Save"
	protected void btnSubmit_Click(object sender, EventArgs e)
	{
        if (txtName.Text.Trim().Length == 0)
        {
            lblMessage.Text = "Data Source Name can not be empty";
            lblMessage.Visible = true;
        }
        else
        {
            SaveData();
        }
	}

    protected void SaveData()
    {
        lblMessage.Visible = false;
        SessionHelper sessionHelper = new SessionHelper(Page);
        if (TheDWDataSource == null)
        {
            TheDWDataSource = new DWDataSource();
            TheDWDataSource.CreateBy = sessionHelper.CurrentUser;
            TheDWDataSource.CreateDate = System.DateTime.Now;
        }
        TheDWDataSource.Name = txtName.Text;
        TheDWDataSource.DSType = txtType.Text;
        TheDWDataSource.Description = txtDescription.Text;
        TheDWDataSource.QuerySQL = txtQuerySQL.Text;
        TheDWDataSource.DeleteQuerySQL = txtDeleteQuerySQL.Text;
        TheDWDataSource.DeleteSQL = txtDeleteSQL.Text;
        TheDWDataSource.MergeQuerySQL = txtMergeQuerySQL.Text;
        TheDWDataSource.MergeSQL = txtMergeSQL.Text;
        TheDWDataSource.MergeResultSQL = txtMergeResultSQL.Text;
        TheDWDataSource.LastUpdateBy = sessionHelper.CurrentUser;
        TheDWDataSource.LastUpdateDate = System.DateTime.Now;
        if (txtStartDate.Text.Trim().Length != 0)
        {
            try
            {
                DateTime startDate = Convert.ToDateTime(txtStartDate.Text);
                TheDWDataSource.QueryStartDate = startDate.ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                lblMessage.Text = "DW Query Start Date is not a valid Date type";
                lblMessage.Visible = true;
                return;
            }
        }
        else
        {
            TheDWDataSource.QueryStartDate = null;
        }
        if (txtEndDate.Text.Trim().Length != 0)
        {
            try
            {
                DateTime endDate = Convert.ToDateTime(txtEndDate.Text);
                TheDWDataSource.QueryEndDate = endDate.ToString("yyyy-MM-dd");
            }
            catch (FormatException)
            {
                lblMessage.Text = "DW Query End Date is not a valid Date type";
                lblMessage.Visible = true;
                return;
            }
        }
        else
        {
            TheDWDataSource.QueryEndDate = null;
        }

        try
        {
            if (TheDWDataSource == null || TheDWDataSource.Id == 0)
            {
                TheService.CreateDWDataSource(TheDWDataSource);
                lblMessage.Text = "Data Source Insert Successful";
            }
            else
            {
                TheService.UpdateDWDataSource(TheDWDataSource);
                lblMessage.Text = "Data Source Update Successful";
            }
        }
        catch (SqlException ex)
        {
            lblMessage.Text = ex.Message;
        }
        lblMessage.Visible = true;
    }

	//The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The event handler when user click button "Delete"
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        if (TheDWDataSource != null && TheDWDataSource.Id != 0)
        {
            TheService.DeleteDWDataSource(TheDWDataSource.Id);
        }
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
        txtQuerySQL.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.QuerySQL : String.Empty;
        txtDeleteQuerySQL.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.DeleteQuerySQL : String.Empty;
        txtDeleteSQL.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.DeleteSQL : String.Empty;
        txtMergeQuerySQL.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.MergeQuerySQL : String.Empty;
        txtMergeSQL.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.MergeSQL : String.Empty;
        txtMergeResultSQL.Text = (TheDWDataSource != null && TheDWDataSource.Id != 0) ? TheDWDataSource.MergeResultSQL : String.Empty;

        if (TheDWDataSource != null && TheDWDataSource.Id != 0)
        {
            gvOperatorList.DataSource = TheDWDataSource.DWDataSourceOperatorList;
            gvOperatorList.DataBind();
            gvOperatorList.Visible = true;
            gvRuleList.DataSource = TheDWDataSource.DWDataSourceMergeRuleList;
            gvRuleList.DataBind();
            gvRuleList.Visible = true;
            btnAddOperator.Visible = true;
            btnDeleteOperator.Visible = true;
            btnDelete.Visible = true;
            btnAddRule.Visible = true;
            btnDeleteRule.Visible = true;
        }
        else
        {
            gvOperatorList.Visible = false;
            gvRuleList.Visible = false;
            btnAddOperator.Visible = false;
            btnDeleteOperator.Visible = false;
            btnDelete.Visible = false;
            btnAddRule.Visible = false;
            btnDeleteRule.Visible = false;
        }

        btnSubmit.Visible = true;
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
        TheService.DeleteDWDataSourceOperator(GetSelectIdList(gvOperatorList));

        //re-load the data source
        TheDWDataSource = TheService.LoadDWDataSource(TheDWDataSource.Id);

        UpdateView();
    }

    protected void btnAddRule_Click(object sender, EventArgs e)
    {
        NewRule1.Visible = true;
        NewRule1.TheDWDataSourceMergeRule = null;
        NewRule1.TheDWDataSourceId = TheDWDataSource.Id;
        NewRule1.UpdateView();
        pnlMain.Visible = false;
    }

    protected void lbtnRuleName_Click(object sender, EventArgs e)
    {
        int dsRuleId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewRule1.TheDWDataSourceMergeRule = TheService.LoadDWDataSourceMergeRule(dsRuleId);
        NewRule1.TheDWDataSourceId  = TheDWDataSource.Id;
        NewRule1.UpdateView();
        NewRule1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void btnDeleteRule_Click(object sender, EventArgs e)
    {
        TheService.DeleteDWDataSourceMergeRule(GetSelectIdList(gvRuleList));

        //re-load the data source
        TheDWDataSource = TheService.LoadDWDataSource(TheDWDataSource.Id);

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
}