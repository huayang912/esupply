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
using Dndp.Web;
using Dndp.Persistence.Entity.Dui;
using log4net;
using Dndp.Service.Dui;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Dndp.Utility.CSV;

public partial class Modules_Dui_DSUpload_History : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DSUpload");

    //The entity service
    protected IDataSourceUploadMgr TheService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
        }
    }

    //The event handler when user click button "Search"
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
    }

    //The event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The public method to clear the view
    public void UpdateView()
    {
        IList<DataSourceUpload> dsUploadHistoryList = TheService.FindDataSourceUpload(TheDataSource.Id, ddlDSCategory.SelectedValue, txtName.Text, txtFileName.Text, txtCreateBy.Text, this.CurrentUser);

        txtDataSourceName.Text = TheDataSource.Name;

        gvDSUploadHistory.DataSource = dsUploadHistoryList;
        gvDSUploadHistory.DataBind();
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

    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheService.LoadDataSourceUpload(dsUploadId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(dsUpload.UploadFileOriginName);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName);
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter);
        TheService.DownloadUploadData(dsUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnUpdate_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheService.LoadDataSourceUpload(dsUploadId);

        dsUpload.Name = Request["txtSubject"].ToString();
        TheService.UpdateDataSourceUpload(dsUpload);

        UpdateView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            UpdateSelection();
        }
    }

    protected void gvDSUploadHistory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnUpdate = (LinkButton)e.Row.FindControl("lbtnUpdate");
            lbtnUpdate.Attributes.Add("onclick", "javascript:return SubjectInput('"+DataBinder.Eval(e.Row.DataItem, "Name")+"')");
        }
    }

    protected void gvDSUploadHistory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDSUploadHistory.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    //Do data query and binding.
    private void UpdateSelection()
    {
        IList<string> FoundResult = TheService.FindDataSourceCategoryListForOwner((new SessionHelper(Page)).CurrentUser.Id, true);
        List<string> DSCategoryList = new List<string>();
        DSCategoryList.Add("");
        if (FoundResult != null && FoundResult.Count > 0)
        {
            foreach (string strValue in FoundResult)
            {
                DSCategoryList.Add(strValue);
            }
            ddlDSCategory.DataSource = DSCategoryList;
            ddlDSCategory.DataBind();
        }
    }
}
