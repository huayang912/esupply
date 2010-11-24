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

//TODO: Add other using statements here.

public partial class Modules_Dui_DSMaintenance_Main : ModuleBase
{
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
		if (!IsPostBack)
        {
            UpdateSelection();
            UpdateView();
        }
    }
	
	protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        Edit1.Back += new System.EventHandler(this.Edit1_Back);
        New1.Back += new EventHandler(this.New1_Back);
        New1.Submit += new EventHandler(New1_Submit);
       
		//TODO: Add other init code here.
    }

    void New1_Submit(object sender, EventArgs e)
    {
        New1.Visible = false;
        pnlMain.Visible = false;
        Edit1.Visible = true;
        Edit1.TheDataSource = New1.NewDataSource;
        Edit1.UpdateView();
    }


    //The event handler when user click button "Back" button on New page.
    void New1_Back(object sender, EventArgs e)
    {
        New1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;
		//TODO: Add other event handler code here.
    }

    private void UpdateSelection()
    {
        IList<string> FoundResult = TheService.FindAllDataSourceType();
        List<string> DSTypeList = new List<string>();
        DSTypeList.Add("");
        if (FoundResult != null)
        {
            foreach (string strValue in FoundResult)
            {
                DSTypeList.Add(strValue);
            }
        }
        ddlDSType.DataSource = DSTypeList;
        ddlDSType.DataBind();
    }
	
	//The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
		
		//TODO: Add other event handler code here.
    }

	//Do data query and binding.
    private void UpdateView()
    {
        IList<DataSource> result = TheService.FindActiveDataSourceByTypeAndName(ddlDSType.Text.Trim(), txtDSName.Text.Trim());
        int recordCount = 0;
        if (result != null)
        {
            recordCount = result.Count;
        }
        lblRecordCount.Text = string.Format("Total {0} Records", recordCount);
        gvList.DataSource = result;
        gvList.DataBind();
    }

	//The event handler when user click button "Back" on New page.
    protected void Edit1_Back(object sender, EventArgs e)
    {
        Edit1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;        
    }

    protected void btnNew_Click(object sender, EventArgs e)
    {
        New1.Visible = true;
        New1.UpdateView();
        pnlMain.Visible = false;
		//TODO: Add othere code here.
    }

	//The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            IList<int> idList = new List<int>();

            foreach (GridViewRow row in gvList.Rows)
            {
                CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
                if (cbSelect.Checked)
                {
                    idList.Add((int)(gvList.DataKeys[row.RowIndex].Value));
                }
            }
            TheService.DeleteDataSource(idList);

            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)(gvList.SelectedDataKey.Value);
        Edit1.TheDataSource = TheService.LoadDataSource(id);
        Edit1.UpdateView();
        Edit1.Visible = true;
        pnlMain.Visible = false;
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}