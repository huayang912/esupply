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

public partial class Modules_Dui_DWDSQuery_Main : ModuleBase
{
    //Get the logger
    private static ILog log = LogManager.GetLogger("DWDSQuery");
	
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
        DWDSViewAll1.Back += new EventHandler(DWDSViewAll1_Back);
        UpdateView();
		//TODO: Add other init code here.
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
        IList<string> FoundResult = TheService.FindDWDataSourceTypeList((new SessionHelper(Page)).CurrentUser.Id, "VIEWER");
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
        IList<DWDataSource> result = TheService.FindDWDataSourceByAllowType((new SessionHelper(Page)).CurrentUser.Id, "VIEWER", ddlDSType.Text, txtDSName.Text);
        gvDWDSList.DataSource = result;
        
        gvDWDSList.DataBind();
    }

    protected void gvDWDSList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDWDSList.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    private string GetQueryDate(object sender)
    {
        string queryDate = "";
        for (int i = 0; i < gvDWDSList.Rows.Count; i++)
        {
            if (((LinkButton)sender).CommandArgument == ((LinkButton)gvDWDSList.Rows[i].FindControl("lbtnDownload")).CommandArgument)
            {
                if (gvDWDSList.Rows[i].FindControl("ddlQueryDate") != null)
                {
                    DropDownList ddlQueryDate = (DropDownList)gvDWDSList.Rows[i].FindControl("ddlQueryDate");
                    queryDate = ddlQueryDate.SelectedValue;
                }
                break;
            }
        }

        return queryDate;
    }

    protected void lbtnDownload_Click(object sender, EventArgs e)
    {

        string queryDate = GetQueryDate(sender);

        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DWDataSource ds = TheService.LoadDWDataSource(dsId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(ds.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_Download.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter);
        if (queryDate.Trim().Length != 0)
        {
            TheService.DownloadQueryData(ds, csvWriter, queryDate);
        }
        else
        {
            TheService.DownloadQueryData(ds, csvWriter);
        }
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnViewAll_Click(object sender, EventArgs e)
    {
        String queryDate = GetQueryDate(sender);

        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DWDataSource ds = TheService.LoadDWDataSource(dsId);

        DWDSViewAll1.TheDWDataSource = ds;
        DWDSViewAll1.TheQueryDate = queryDate;
        DWDSViewAll1.ClearSearchCondition();
        DWDSViewAll1.UpdateView();

        DWDSViewAll1.Visible = true;
        pnlMain.Visible = false;
    }

    protected void gv_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }

        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            string QueryStartDate = e.Row.Cells[0].Text;
            string QueryEndDate = e.Row.Cells[1].Text;

            if (QueryStartDate != null
                && QueryStartDate.Trim().Length != 0
                && !QueryStartDate.Trim().Equals("&nbsp;"))
            {
                DateTime startDate = Convert.ToDateTime(QueryStartDate);
                DateTime endDate = DateTime.Now;

                if (QueryEndDate != null
                    && QueryEndDate.Trim().Length != 0
                    && !QueryEndDate.Trim().Equals("&nbsp;"))
                {
                    endDate = Convert.ToDateTime(QueryEndDate);
                }

                DropDownList ddlQuerydate = (DropDownList)e.Row.FindControl("ddlQueryDate");
                IList ddlList = new ArrayList();
                for (DateTime i = endDate; i.CompareTo(startDate) >= 0; i = i.AddMonths(-1))
                {
                    ddlList.Add(Convert.ToString(i.Year * 100 + i.Month));              
                }
                ddlQuerydate.DataSource = ddlList;
                ddlQuerydate.DataBind();
            }
            else
            {
                e.Row.FindControl("ddlQueryDate").Visible = false;
            }

            e.Row.Cells[0].Visible = false;
            e.Row.Cells[1].Visible = false;
        }
    }
}