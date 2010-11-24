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

//TODO:Add other using statements here.

public partial class Modules_Dui_DSUpload_New : ModuleBase
{
    public event EventHandler Back;

    public event EventHandler Submit;

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

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
    }

    //The event handler when user click button "Submit"
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        //Check input

        //Perform "New" action.

        //Show feedback message to user.
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
        txtName.Text = TheDataSourceCategory.TheDataSource.Name;
        txtSubject.Text = "";

        IList<DataSourceCategory> dsCategoryList = TheService.FindDataSourceCategory(TheDataSourceCategory.TheDataSource.Id);
        IList<DataSourceUpload> dsUploadHistoryList = TheService.FindDataSourceUpload(TheDataSourceCategory.TheDataSource.Id);
        txtCategory.DataSource = dsCategoryList;
        txtCategory.DataBind();
        //
        if (txtCategory.Items != null)
        {
            foreach (ListItem item in txtCategory.Items)
            {
                if (item.Value == TheDataSourceCategory.Id.ToString())
                {
                    item.Selected = true;
                    break;
                }
            }
        }

        gvDSUploadHistory.DataSource = dsUploadHistoryList;
        gvDSUploadHistory.DataBind();
        errorMessage.DataSource = null;
        errorMessage.DataBind();
    }

    public DataSourceCategory TheDataSourceCategory
    {
        get
        {
            return (DataSourceCategory)ViewState["TheDataSourceCategory"];
        }
        set
        {
            ViewState["TheDataSourceCategory"] = value;
        }
    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        DataSourceUpload dsUpload = new DataSourceUpload();
        dsUpload.Name = txtSubject.Text;

        DataSourceCategory dsCategory = new DataSourceCategory();
        dsCategory.Id = int.Parse(txtCategory.SelectedValue);
        dsUpload.TheDataSourceCategory = dsCategory;

        SessionHelper sessionHelper = new SessionHelper(Page);
        dsUpload.UploadBy = sessionHelper.CurrentUser;

        dsUpload.UploadDate = System.DateTime.Now;

        dsUpload.UploadFileOriginName = txtFile.FileName;

        dsUpload.IsHitoryDelete = 0;
        dsUpload.IsWithdraw = 0;

        IList<string> errorMessages = TheService.UploadCSV(dsUpload, txtFile.FileContent);
        if (errorMessages != null && errorMessages.Count > 0)
        {
            errorMessage.DataSource = errorMessages;
            errorMessage.DataBind();
        }
        else
        {
            if (Submit != null)
            {
                dsUpload.TheDataSourceCategory = TheDataSourceCategory;
                TheDataSourceCategory.LastestDataSourceUpload = dsUpload;
                Submit(this, null);
            }
        }
    }

    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
            TheService.DeleteDataSourceUploadAndUploadedData(dsUploadId);
            UpdateView();
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
        }
    }
    protected void lbtnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.ConfirmDataSourceUpload(dsUploadId, this.CurrentUser);
        UpdateView();
    }
    protected void lbtnUnconfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheService.UnconfirmDataSourceUpload(dsUploadId);
        UpdateView();
    }

    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheService.LoadDataSourceUpload(dsUploadId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(dsUpload.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_Download.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheService.DownloadUploadData(dsUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }
}