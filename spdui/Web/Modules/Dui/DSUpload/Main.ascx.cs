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

public partial class Modules_Dui_DSUpload_Main : ModuleBase
{
    //Get the logger
	private static ILog log = LogManager.GetLogger("DSUpload");
	
	//The entity service
    protected IDataSourceUploadMgr TheDSUploadService
    {
        get
        {
            return GetService("DataSourceUploadMgr.service") as IDataSourceUploadMgr;
        }
    }

    protected IDataSourceMgr TheDSService
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
        New1.Back += new EventHandler(New1_Back);
        Validate1.Back += new EventHandler(Validate1_Back);
        Validate1.Update += new EventHandler(Validate1_Update);
        ValidateUpdate1.Back += new EventHandler(ValidateUpdate1_Back);
        History1.Back += new EventHandler(History1_Back);
        New1.Submit += new EventHandler(New1_Submit);
        //UpdateView();
		//TODO: Add other init code here.
    }

    void New1_Submit(object sender, EventArgs e)
    {
        New1.Visible = false;
        DataSourceUpload dsUpload = New1.TheDataSourceCategory.LastestDataSourceUpload;
        dsUpload.IsInValidation = false;
        
        Validate1.TheDataSourceUpload = dsUpload;
        Validate1.UpdateView();
        Validate1.Visible = true;

        pnlMain.Visible = false;    
    }


    //The event handler when user click button "Back" button on New page.
    void New1_Back(object sender, EventArgs e)
    {
        New1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    void Validate1_Back(object sender, EventArgs e)
    {
        Validate1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
    }

    void Validate1_Update(object sender, EventArgs e)
    {
        Validate1.Visible = false;
        ValidationResult vr = Validate1.TheValidationResult;

        ValidateUpdate1.TheValidationResult = vr;
        ValidateUpdate1.UpdateView();
        ValidateUpdate1.Visible = true;
        pnlMain.Visible = false;
    }

    void ValidateUpdate1_Back(object sender, EventArgs e)
    {
        ValidateUpdate1.Visible = false;
        DataSourceUpload dsUpload = TheDSUploadService.LoadDataSourceUpload(ValidateUpdate1.TheValidationResult.TheDataSourceUpload.Id);
        if (dsUpload != null)
        {
            dsUpload.IsInValidation = false;
            dsUpload.ValidationResultList =
                TheDSUploadService.FindValidationResultByDataSourceUploadId(dsUpload.Id);

            Validate1.TheDataSourceUpload = dsUpload;
            Validate1.UpdateView();
            Validate1.Visible = true;

            pnlMain.Visible = false;
        }
        else
        {
            Validate1.Visible = false;
            pnlMain.Visible = true;
            UpdateView();
        }
    }

    void History1_Back(object sender, EventArgs e)
    {
        History1.Visible = false;
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
        IList<string> FoundResult = TheDSUploadService.FindDataSourceTypeListForOwner((new SessionHelper(Page)).CurrentUser.Id);
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

        FoundResult = TheDSUploadService.FindDataSourceCategoryListForOwner((new SessionHelper(Page)).CurrentUser.Id, false);
        List<string> DSCategoryList = new List<string>();
        DSCategoryList.Add("");
        if (FoundResult != null)
        {
            foreach (string strValue in FoundResult)
            {
                DSCategoryList.Add(strValue);
            }
        }
        ddlDSCategory.DataSource = DSCategoryList;
        ddlDSCategory.DataBind();

        List<ListItem> DSStatusList = TheDSUploadService.FindDataSourceStatusListForOwner((new SessionHelper(Page)).CurrentUser.Id);
        ddlDSStatus.DataSource = DSStatusList;
        ddlDSStatus.DataTextField = "Text";
        ddlDSStatus.DataValueField = "Value";
        ddlDSStatus.DataBind();
    }

    //Do data query and binding.
    private void UpdateView()
    {
        IList<DataSourceCategory> result = TheDSUploadService.FindDataSourceCategoryForOwner((new SessionHelper(Page)).CurrentUser.Id, ddlDSCategory.Text, ddlDSType.Text, ddlDSStatus.SelectedValue, txtDSName.Text);
        gvDSCategory.DataSource = result;
        gvDSCategory.DataBind();
    }

	//The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        
    }

    protected void gvDSCategory_SelectedIndexChanged(object sender, EventArgs e)
    {      

    }
    protected void lbtnUpload_Click(object sender, EventArgs e)
    {        
        int dsCategoryId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceCategory dsCategory = TheDSUploadService.LoadDataSourceCategory(dsCategoryId);

        New1.TheDataSourceCategory = dsCategory;    
        New1.UpdateView();
        New1.Visible = true;

        pnlMain.Visible = false;        
    }
    protected void lbtnDelete_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheDSUploadService.DeleteDataSourceUploadAndUploadedData(dsUploadId);
        UpdateView();
    }
    protected void lbtnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheDSUploadService.ConfirmDataSourceUpload(dsUploadId);
        UpdateView();
    }
    protected void lbtnUnconfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheDSUploadService.UnconfirmDataSourceUpload(dsUploadId);
        UpdateView();
    }
    protected void lbtnValidate_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheDSUploadService.LoadDataSourceUpload(dsUploadId);
        dsUpload.IsInValidation = false;
        dsUpload.ValidationResultList = 
            TheDSUploadService.FindValidationResultByDataSourceUploadId(dsUploadId);

        Validate1.TheDataSourceUpload = dsUpload;
        Validate1.UpdateView();
        Validate1.Visible = true;

        pnlMain.Visible = false;    
    }

    protected void lbtnTemplate_Click(object sender, EventArgs e)
    {
        int dscId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSource ds = TheDSUploadService.LoadDataSourceCategory(dscId).TheDataSource;
        
        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(ds.Description);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_Template.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheDSUploadService.DownloadUploadTemplate(ds, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnDownload_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheDSUploadService.LoadDataSourceUpload(dsUploadId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(dsUpload.UploadFileOriginName);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName);
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter); ;
        TheDSUploadService.DownloadUploadData(dsUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnETLLog_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheDSUploadService.LoadDataSourceUpload(dsUploadId);

        Response.Clear();
        Response.ContentType = "application/octet-stream";
        string fileName = HttpUtility.UrlEncode(dsUpload.Name);
        fileName = fileName.Replace("+", "%20");
        Response.AddHeader("Content-Disposition", "attachment;FileName=" + fileName + "_ETLLog.csv");
        TextWriter txtWriter = new StreamWriter(Response.OutputStream, Encoding.GetEncoding("GB2312"));
        CSVWriter csvWriter = new CSVWriter(txtWriter);
        TheDSUploadService.DownloadETLLog(dsUpload, csvWriter);
        txtWriter.Flush();
        Response.End();

        UpdateView();
    }

    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSource ds = TheDSService.LoadDataSource(dsId);
        History1.TheDataSource = ds;
        History1.UpdateView();
        History1.Visible = true;

        pnlMain.Visible = false; 
    }

    protected void gvDSCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDSCategory.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}