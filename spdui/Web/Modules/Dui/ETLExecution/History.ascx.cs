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

public partial class Modules_Dui_ETLExecution_History : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("DSExecution");

    //The entity service
    protected IDataSourceUploadMgr TheService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
        }
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
        IList<DataSourceUpload> dsUploadHistoryList = TheService.FindDataSourceUploadByLogBatchNo(LogBatchNo);
        gvDSUploadHistory.DataSource = dsUploadHistoryList;
        gvDSUploadHistory.DataBind();
    }

    public string LogBatchNo
    {
        get
        {
            return (string)ViewState["LogBatchNo"];
        }
        set
        {
            ViewState["LogBatchNo"] = value;
        }
    }

    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheService.LoadDataSourceUpload(dsUploadId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(dsUpload.UploadFileOriginName));
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter);
        TheService.DownloadUploadData(dsUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }
    protected void lbtnETLLog_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheService.LoadDataSourceUpload(dsUploadId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + HttpUtility.UrlEncode(dsUpload.Name) + "_ETLLog.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter);
        TheService.DownloadETLLog(dsUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }
}
