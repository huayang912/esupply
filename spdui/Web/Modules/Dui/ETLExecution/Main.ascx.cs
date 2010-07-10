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

public partial class Modules_Dui_ETLExecution_Main : ModuleBase
{
    //Get the logger
    private static ILog log = LogManager.GetLogger("ETLExecution");
	
	//The entity service
    protected IDataSourceUploadMgr TheService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        UpdateView();
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        History1.Back += new EventHandler(History1_Back);
        //UpdateView();
        //TODO: Add other init code here.
    }

    void History1_Back(object sender, EventArgs e)
    {
        History1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    //Do data query and binding.
    private void UpdateView()
    {
        gvDataUploadForETL.DataSource = TheService.FindDataSourceUploadForETL();
        gvDataUploadForETL.DataBind();

        gvDataUploadInETL.DataSource = TheService.FindDataSourceUploadInETL();
        gvDataUploadInETL.DataBind();

        gvETLAgentLog.DataSource = TheService.FindETLAgentLog();
        gvETLAgentLog.DataBind();

        if ((TheService.FindDataSourceUploadForETL() == null || TheService.FindDataSourceUploadForETL().Count.Equals(0))
         || (TheService.FindDataSourceUploadInETL() != null && !TheService.FindDataSourceUploadInETL().Count.Equals(0))
         || (!TheService.FindETLRunStatus()))
        {
            btnRunETL.Visible = false;
            btnRefresh.Visible = true;
        }
        else
        {
            btnRunETL.Visible = true;
            btnRefresh.Visible = false;
        }
    }

    protected void btnRunETL_Click(object sender, EventArgs e)
    {
        TheService.RunETLPackage("Run ETL");
        btnRunETL.Visible = false;
        btnRefresh.Visible = true;
        UpdateView();
    }

    protected void btnRefresh_Click(object sender, EventArgs e)
    {
        UpdateView();
    }

    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        string LogBatchNo = ((LinkButton)sender).CommandArgument.ToString();
        History1.LogBatchNo = LogBatchNo;
        History1.UpdateView();
        History1.Visible = true;

        pnlMain.Visible = false;
    }
    protected void gvETLAgentLog_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvETLAgentLog.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}