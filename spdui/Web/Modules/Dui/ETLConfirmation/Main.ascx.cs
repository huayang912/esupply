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
using System.IO;
using System.Text;
using Dndp.Utility.CSV;
using Dndp.Persistence.Dao;

//TODO: Add other using statements here.

public partial class Modules_Dui_DSETLConfirm_Main : ModuleBase
{
    //Get the logger
	private static ILog log = LogManager.GetLogger("DSETLConfirm");
	
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

    protected SqlHelperDao TheSqlHelperDao
    {
        get
        {
            return GetSqlHelper();
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
        Validate1.Back += new EventHandler(Validate1_Back);
        History1.Back += new EventHandler(History1_Back);
        //UpdateView();
		//TODO: Add other init code here.
    }


    //The event handler when user click button "Back" button on New page.
    void Validate1_Back(object sender, EventArgs e)
    {
        Validate1.Visible = false;
        pnlMain.Visible = true;
        UpdateView();
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
        IList<string> FoundResult = TheDSUploadService.FindDataSourceTypeListForETLConfirmer((new SessionHelper(Page)).CurrentUser.Id);
        List<string> DSTypeList = new List<string>();
        DSTypeList.Add("");
        if (FoundResult != null && FoundResult.Count > 0) 
        {
            foreach (string strValue in FoundResult)
            {
                DSTypeList.Add(strValue);
            }
            ddlDSType.DataSource = DSTypeList;
            ddlDSType.DataBind();
        }

        FoundResult = TheDSUploadService.FindDataSourceCategoryListForETLConfirmer((new SessionHelper(Page)).CurrentUser.Id, false);
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

        List<ListItem> DSStatusList = TheDSUploadService.FindDataSourceStatusListForETLConfirmer((new SessionHelper(Page)).CurrentUser.Id);
        ddlDSStatus.DataSource = DSStatusList;
        ddlDSStatus.DataTextField = "Text";
        ddlDSStatus.DataValueField = "Value";
        ddlDSStatus.DataBind();
    }

	//Do data query and binding.
    private void UpdateView()
    {
        IList<DataSourceCategory> result = TheDSUploadService.FindDataSourceCategoryForETLConfirmer((new SessionHelper(Page)).CurrentUser.Id, ddlDSCategory.Text, ddlDSType.Text, ddlDSStatus.SelectedValue);
        gvDSCategory.DataSource = result;
        gvDSCategory.DataBind();
    }

	//The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        
    }
    
    protected void lbtnConfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheDSUploadService.ETLConfirmDataSourceUpload(dsUploadId, this.CurrentUser);
        UpdateView();
    }
    protected void lbtnUnconfirm_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        TheDSUploadService.ETLUnconfirmDataSourceUpload(dsUploadId);
        UpdateView();
    }
    /*
    protected void lbtnRevalidate_Click(object sender, EventArgs e)
    {
        int dsUploadId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSourceUpload dsUpload = TheDSUploadService.LoadDataSourceUpload(dsUploadId);
        dsUpload.IsInValidation = false;
        dsUpload.ValidationResultList =
            TheDSUploadService.FindValidationResultByDataSourceUploadId(dsUploadId);

        Validate1.TheDataSourceUpload = dsUpload;
        Validate1.ValidateAll();        
        Validate1.Visible = true;

        pnlMain.Visible = false;
    }
    */
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
    
    protected void lbtnHistory_Click(object sender, EventArgs e)
    {
        int dsId = Int32.Parse(((LinkButton)sender).CommandArgument);
        DataSource ds = TheDSService.LoadDataSource(dsId);
        History1.TheDataSource = ds;
        History1.UpdateView();
        History1.Visible = true;

        pnlMain.Visible = false; 
    
    }

    protected void gvDSCategory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            LinkButton lbtnDSName = (LinkButton)e.Row.FindControl("lbtnDSName");
            DataSourceCategory dsc = (DataSourceCategory)e.Row.DataItem;
            if (dsc.TheDataSource.DataStructure != null && dsc.TheDataSource.DataStructure.Trim() != string.Empty)
            {
                e.Row.Cells[2].Attributes.Add("onmouseover", "e=this.style.backgroundColor; this.style.backgroundColor=this.style.borderColor");
                e.Row.Cells[2].Attributes.Add("onmouseout", "this.style.backgroundColor=e");
                e.Row.Cells[2].Attributes.Add("title", GetDataStructure(dsc.TheDataSource.DataStructure));
            }
            // ((GridView)(sender)).Columns[2]
        }
    }

    protected string GenerateRevalidateString(DataSourceCategory dsc)
    {
        
        if (dsc.LastestDataSourceUpload != null && dsc.LastestDataSourceUpload.ProcessStatus != null)
        {
            IList<ValidationResult> list =
                TheDSUploadService.FindValidationResultByDataSourceUploadId(dsc.LastestDataSourceUpload.Id);
            string vrIds = "";
            if (list != null) {
                for (int i = 0; i < list.Count; i++ )
                {
                    if (vrIds.Trim().Length == 0) 
                    {
                        vrIds = list[i].Id.ToString();
                    }
                    else
                    {
                        vrIds += "," + list[i].Id.ToString();
                    }
                    
                }                
            }
            return "<a href=\"#\" onclick=\"javascript:RevalidateAll('" + dsc.LastestDataSourceUpload.Id + "','" + vrIds + "');\">[Revalidate All]</a>";
        }
        else
        {
            return "";
        }
    }
    protected void btnInValidation_Click(object sender, EventArgs e)
    {
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

    protected void gvDSCategory_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvDSCategory.PageIndex = e.NewPageIndex;
        UpdateView();
    }

    private string GetDataStructure(string dataStructure)
    {
        string structure = string.Empty;
        structure += "cssbody=[obbd] cssheader=[obhd] header=[Data Source Structure] body=[";
        try
        {
            DataSet dataSet = this.TheSqlHelperDao.ExecuteDataset(dataStructure);

            structure += "<table class='list' cellspacing='0' cellpadding='1' border='1' style='border-collapse:collapse;'>";
            structure += "<tr class='listhead' align='left'>";

            DataTableReader dataReader = dataSet.CreateDataReader();
            //write csv header
            for (int i = 0; i < dataReader.FieldCount; i++)
            {
                structure += "<th scope='col' nowrap='true'>" + dataReader.GetName(i) + "</th>";
            }
            structure += "</tr>";

            //write csv content
            while (dataReader.Read())
            {
                structure += "<tr>";
                object[] fields = new object[dataReader.FieldCount];
                dataReader.GetValues(fields);
                for (int i = 0; i < fields.Length; i++)
                {
                    if (i == 0)
                    {
                        structure += "<td align='left'>";
                    }
                    else
                    {
                        structure += "<td align='center'>";
                    }
                    if (fields[i] != null)
                    {
                        structure += fields[i].ToString();
                    }
                    else
                    {
                        structure += "";
                    }
                    structure += "</td>";
                }
                structure += "</tr>";
            }
            structure += "</table>]";
        }
        catch
        {
            structure = "Error Data Structure Sql.";
        }

        return structure;
    }
}