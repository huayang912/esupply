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
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;

//TODO: Add other using statements here.

public partial class Modules_OffLineReport_ReportMaintenance_Edit : ModuleBase
{
    public event EventHandler Back;
	
	//Get the logger
	private static ILog log = LogManager.GetLogger("ReportMaintenance");
	
	//The entity service
	protected IReportTemplateMgr TheService
    {
        get
        {
            return GetService("ReportTemplateMgr.service") as IReportTemplateMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //Add code for Page_Load here.
        if (!IsPostBack)
        {
            //UpdateView();
        }
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        NewReportSheet1.Back += new System.EventHandler(this.NewReportSheet1_Back);
    }
    
    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdate.Visible = PermissionUpdate;   
		
		//TODO: Add other permission init codes here.
    }

    public ReportTemplate TheReportTemplate
    {
        get
        {
            return (ReportTemplate)ViewState["TheReportTemplate"];
        }
        set
        {
            ViewState["TheReportTemplate"] = value;
        }
    }

	//Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        //TODO: Add code here.
        txtName.Text = TheReportTemplate.Name;
        txtDescription.Text = TheReportTemplate.Description;
        txtType.Text = TheReportTemplate.ReportType;
        txtConnectionString.Text = String.Empty;
        txtTemplateFilePath.Text = TheReportTemplate.TemplateFilePath;
        // Modified By Vincent On 2009-09-04 Begin
        //rdoSendMailYes.Checked =  (TheReportTemplate.NeedSendMail == "YES");
        //rdoSendMailNo.Checked = (TheReportTemplate.NeedSendMail == "NO");
        // Modified By Vincent On 2009-09-04 Begin
        
        gvSheetList.DataSource = TheReportTemplate.ReportSheetList;
        gvSheetList.DataBind();

        lblMessage.Visible = false;
    }

	//Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //The event handler when user click button "Back" button on NewReportSheet page.
    void NewReportSheet1_Back(object sender, EventArgs e)
    {
        NewReportSheet1.Visible = false;
        TheReportTemplate.ReportSheetList = TheService.FindReportSheetByReportId(TheReportTemplate.Id);
        UpdateView();
        pnlMain.Visible = true;
    }

    //Event handler when user click button "Update"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = true;

        string name = txtName.Text.Trim();
        if (name.Length == 0)
        {
            lblMessage.Text = "Report name cannot be empty.";
            return;
        }

        string description = txtDescription.Text.Trim();

        TheReportTemplate.Name = name;
        TheReportTemplate.Description = description;
        TheReportTemplate.ReportType = txtType.Text.Trim();
        //TheReportTemplate.ConnectionString = txtConnectionString.Text.Trim();
        TheReportTemplate.TemplateFilePath = txtTemplateFilePath.Text.Trim();
        TheReportTemplate.LastUpdateBy = CurrentUser;        
        TheReportTemplate.LastUpdateDate = DateTime.Now;
        // Modified By Vincent On 2009-09-04 Begin
        // TheReportTemplate.NeedSendMail = rdoSendMailYes.Checked ? "YES" : "NO";
        // Modified By Vincent On 2009-09-04 Begin
        try
        {
            TheService.UpdateReportTemplate(TheReportTemplate);
            lblMessage.Text = "Update successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        UpdateView();
    }

    protected void btnUpdateConn_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = true;

        TheReportTemplate.ConnectionString = txtConnectionString.Text.Trim();
        TheReportTemplate.LastUpdateBy = CurrentUser;
        TheReportTemplate.LastUpdateDate = DateTime.Now;
        
        try
        {
            TheService.UpdateReportTemplate(TheReportTemplate);
            lblMessage.Text = "Update successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
        UpdateView();
    }
    
    //The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        TheService.DeleteReportTemplate(TheReportTemplate.Id);
        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void lbtnSheetName_Click(object sender, EventArgs e)
    {
        int sheetId = Int32.Parse(((LinkButton)sender).CommandArgument);
        NewReportSheet1.TheReportSheet = TheService.LoadReportSheet(sheetId);
        NewReportSheet1.UpdateView();
        NewReportSheet1.Visible = true;
        NewReportSheet1.SetReportTemplateId(TheReportTemplate.Id);
        pnlMain.Visible = false;
    }

    protected void btnDeleteReportSheet_Click(object sender, EventArgs e)
    {
        TheService.DeleteReportSheet(GetSelectIdList(gvSheetList));

        //re-load the data source
        TheReportTemplate = TheService.LoadReportTemplate(TheReportTemplate.Id);

        UpdateView();
    }

    protected void btnAddReportSheet_Click(object sender, EventArgs e)
    {
        NewReportSheet1.Visible = true;
        NewReportSheet1.TheReportSheet = null;
        NewReportSheet1.SetReportTemplateId(TheReportTemplate.Id);
        NewReportSheet1.UpdateView();
        pnlMain.Visible = false;
    }

    //private IList<int> GetSelectIdList(GridView gv)
    //{
    //    IList<int> idList = new List<int>();

    //    foreach (GridViewRow row in gv.Rows)
    //    {
    //        CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
    //        if (cbSelect.Checked)
    //        {
    //            idList.Add((int)(gv.DataKeys[row.RowIndex].Value));
    //        }
    //    }

    //    return idList;
    //}
}