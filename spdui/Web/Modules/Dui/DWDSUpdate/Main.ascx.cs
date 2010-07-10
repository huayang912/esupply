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
using System.Text;
using System.IO;
using Dndp.Utility.CSV;

//TODO: Add other using statements here.

public partial class Modules_Dui_DWDSUpdate_Main : ModuleBase
{
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
        if (!IsPostBack)
        {
            UpdateSelection();
            UpdateView();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        DWDSUpdate1.Back += new EventHandler(DWDSUpdate1_Back);
        DWDSViewAll1.Back += new EventHandler(DWDSViewAll1_Back);
        UpdateView();
        //TODO: Add other init code here.
    }

    //The event handler when user click button "Back" button on New page.
    void DWDSUpdate1_Back(object sender, EventArgs e)
    {
        DWDSUpdate1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    //The event handler when user click button "Back" button on New page.
    void DWDSViewAll1_Back(object sender, EventArgs e)
    {
        DWDSViewAll1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    //The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
        //TODO: Add other event handler code here.
    }

    //Do data query and binding.
    private void UpdateSelection()
    {
        IList<string> FoundResult = TheService.FindDWDataSourceTypeList((new SessionHelper(Page)).CurrentUser.Id, "ADMIN");
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

    private void UpdateView()
    {
        IList<DWDataSource> result = TheService.FindDWDataSourceByAllowType((new SessionHelper(Page)).CurrentUser.Id, "ADMIN", ddlDSType.Text, txtDSName.Text);
        gvDWDSList.DataSource = result;
        gvDWDSList.DataBind();
    }

    protected void gvDWDSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDWDSList.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DWDataSource ds = TheService.LoadDWDataSource(dsId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + ds.Name + "_Download.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadQueryData(ds, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnDownloadUpdate_Click(object sender, EventArgs e)
    {
        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DWDataSource ds = TheService.LoadDWDataSource(dsId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + ds.Name + "_Download.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadUpdateQueryData(ds, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnViewAll_Click(object sender, EventArgs e)
    {
        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DWDataSource ds = TheService.LoadDWDataSource(dsId);

        DWDSViewAll1.TheDWDataSource = ds;
        DWDSViewAll1.UpdateView();

        DWDSViewAll1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DWDataSource ds = TheService.LoadDWDataSource(dsId);

        DWDSUpdate1.TheDWDataSource = ds;
        DWDSUpdate1.ClearSearchCondition();
        DWDSUpdate1.UpdateView();

        DWDSUpdate1.Visible = true;
        pnlMain.Visible = false;
    }
}