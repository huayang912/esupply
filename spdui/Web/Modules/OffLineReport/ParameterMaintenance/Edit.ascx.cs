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

using log4net;

using Dndp.Web;
using Dndp.Persistence.Dao.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using Dndp.Service.OffLineReport;

//TODO: Add other using statements here.

public partial class Modules_OffLineReport_ParameterMaintenance_Edit : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("ParameterMaintenance");

    //The entity service
    protected IReportParameterMgr TheService
    {
        get
        {
            return GetService("ReportParameterMgr.service") as IReportParameterMgr;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        //TODO: Add code for Page_Load here.
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        btnUpdate.Visible = PermissionUpdate;

        //TODO: Add other permission init codes here.
    }

    public ReportParameter TheReportParameter
    {
        get
        {
            return (ReportParameter)ViewState["TheReportParameter"];
        }
        set
        {
            ViewState["TheReportParameter"] = value;
        }
    }

    //Update the view by the entity class stored in ViewState
    public void UpdateView()
    {
        //TODO: Add code here.
        txtName.Text = TheReportParameter.Name;
        
        lblText.Visible = false;
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

    //Event handler when user click button "Update"
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        lblMessage.Visible = true;

        string name = txtName.Text.Trim();
        if (name.Length == 0)
        {
            lblMessage.Text = "Parameter name cannot be empty.";
            return;
        }

        TheReportParameter.Name = name;

        try
        {
            TheService.UpdateReportParameter(TheReportParameter);
            lblMessage.Text = "Update successfully.";
        }
        catch (Exception ex)
        {
            lblMessage.Text = ex.Message;
        }
    }

    //The event handler when user click button "Delete".
    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheService.DeleteReportParameter(TheReportParameter.Id);
            if (Back != null)
            {
                Back(this, e);
            }
          }
        catch (Exception ex)
        {
            this.lblMessage.Visible = true;
            this.lblMessage.Text = "this report patameter have been used, can not be deleted";
        }
    }
}