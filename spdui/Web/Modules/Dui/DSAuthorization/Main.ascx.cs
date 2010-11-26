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

public partial class Modules_Dui_DSAuthorization_Main : ModuleBase
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
    }

    private void UpdateSelection()
    {
        IList<string> FoundResult = TheService.FindAllDataSourceType(this.CurrentUser);
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
        IList<DataSource> result = TheService.FindActiveDataSourceByTypeAndName(ddlDSType.Text.Trim(), txtDSName.Text.Trim(), this.CurrentUser);
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

    protected void btnLock_Click(object sender, EventArgs e)
    {
        IList<int> dsIdList = CollectSelectedDS();

        this.TheService.LockDataSourceEtlConfirm(dsIdList);

        UpdateView();
    }

    protected void btnUnlock_Click(object sender, EventArgs e)
    {
        IList<int> dsIdList = CollectSelectedDS();

        this.TheService.UnLockDataSourceEtlConfirm(dsIdList);

        UpdateView();
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)(gvList.SelectedDataKey.Value);
        Edit1.TheDataSource = TheService.LoadDataSource(id);
        Edit1.TheDataSource.DataSourceCategoryList = TheService.FindDataSourceCategoryByDataSourceId(id, false, this.CurrentUser);
        Edit1.UpdateView();
        Edit1.Visible = true;
        pnlMain.Visible = false;
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    private IList<int> CollectSelectedDS()
    {
        IList<int> dsIdList = new List<int>();
        foreach (GridViewRow row in gvList.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
            if (cbSelect.Checked)
            {
                HiddenField hfDSId = (HiddenField)row.FindControl("hfDSId");
                dsIdList.Add(int.Parse(hfDSId.Value));
            }
        }

        return dsIdList;
    }
}
