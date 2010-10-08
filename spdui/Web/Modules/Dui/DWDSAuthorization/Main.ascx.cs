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


public partial class Modules_Dui_DWDSAuthorization_Main : ModuleBase
{
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
        New1.Back += new EventHandler(this.New1_Back);
        //TODO: Add other init code here.
    }

    //The event handler when user click button "Back" button on New page.
    void New1_Back(object sender, EventArgs e)
    {
        New1.Visible = false;
        UpdateView();
        pnlMain.Visible = true;
        //TODO: Add other event handler code here.
    }

    //The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {

        UpdateView();

        //TODO: Add other event handler code here.
    }

    private void UpdateSelection()
    {
        IList<string> FoundResult = TheService.FindDWDataSourceTypeList(this.CurrentUser.Id);
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

    //Do data query and binding.
    private void UpdateView()
    {
        IList<DWDataSource> result = TheService.FindDWDataSourceByTypeAndName(ddlDSType.Text.Trim(), txtDSName.Text.Trim());

        //IList result = TheService.LoadAllDWDataSource();
        int recordCount = 0;
        if (result != null)
        {
            recordCount = result.Count;
        }
        lblRecordCount.Text = string.Format("Total {0} Records", recordCount);
        gvList.DataSource = result;
        gvList.DataBind();
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)(gvList.SelectedDataKey.Value);
        New1.TheDWDataSource = TheService.LoadDWDataSource(id);
        New1.UpdateView();
        New1.Visible = true;
        pnlMain.Visible = false;
    }
    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}