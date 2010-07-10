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

public partial class Modules_OffLineReport_JobExecution_NewUserReport : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("JobExecution");

    //The entity service
    protected IReportJobMgr TheService
    {
        get
        {
            return GetService("ReportJobMgr.service") as IReportJobMgr;
        }
    }

    //The entity service
    protected IReportUserMgr TheUserService
    {
        get
        {
            return GetService("ReportUserMgr.service") as IReportUserMgr;
        }
    }

    public ReportJob TheReportJob
    {
        get
        {
            return (ReportJob)ViewState["TheReportJob"];
        }
        set
        {
            ViewState["TheReportJob"] = value;
        }
    }

    public IList<ReportJobUser> TheReportJobUser
    {
        get
        {
            return (IList<ReportJobUser>)ViewState["TheReportJobUser"];
        }
        set
        {
            ViewState["TheReportJobUser"] = value;
        }
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            txtUserName.Text = "";
            txtUserDescription.Text = "";
            Back(this, e);
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        IList<int> IdList = new List<int>();
        foreach (GridViewRow row in gvUserList.Rows)
        {
            CheckBox cbSelect = (CheckBox)row.FindControl("cbSelect");
            if (cbSelect.Checked)
            {
                IdList.Add((int)(gvUserList.DataKeys[row.RowIndex].Value));
            }
        }

        if (TheReportJobUser != null)
        {
            foreach (ReportJobUser jobUser in TheReportJobUser)
            {
                IdList.Add(jobUser.TheUser.Id);
            }
        }
        TheService.UpdateReportJobUser(IdList, int.Parse(txtReportJobId.Value));
        TheReportJobUser = (TheService.FindUserByJobId(TheReportJob.Id) as IList<ReportJobUser>);
        UpdateView();
    }

    protected void Page_Load(object sender, EventArgs e)
    {        
    }

    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
    }

    public void UpdateView()
    {
        IList<ReportUser> userList = TheService.FindReportUserByReportBatchIdAndUserNameAndUserDescription(TheReportJob.TheBatch.Id, txtUserName.Text.Trim(), txtUserDescription.Text.Trim());
        IList<ReportUser> resultUserList = new List<ReportUser>();
        if (userList != null)
        {
            foreach (ReportUser user in userList)
            {
                if (!hasData(user.Id))
                {
                    resultUserList.Add(user);
                }
            }
        }
        gvUserList.DataSource = resultUserList;
        gvUserList.DataBind();
        gvUserList.Visible = true;           
    }

    public void SetReportJobId(int Id)
    {
        txtReportJobId.Value = Id.ToString();
    }

    protected bool hasData(int userId)
    {
        if (TheReportJobUser != null)
        {
            for (int i = 0; i < TheReportJobUser.Count; i++)
            {
                if (TheReportJobUser[i].TheUser.Id == userId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
