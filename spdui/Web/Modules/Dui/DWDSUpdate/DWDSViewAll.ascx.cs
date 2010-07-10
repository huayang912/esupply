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
using System.Text;
using System.IO;

//TODO: Add other using statements here.

public partial class Modules_Dui_DWDSUpdate_DWDSViewAll : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DWDSUpdate");

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
        //TODO: Add code for Page_Load here.
    }

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

    //Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        gvDWDSViewAll.DataSource = TheService.FindViewAllResult(TheDWDataSource);
        gvDWDSViewAll.DataBind();
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void gvDWDSViewAll_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDWDSViewAll.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}