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
using log4net;
using Dndp.Service.OffLineReport;
using Dndp.Persistence.Entity.OffLineReport;
using System.Collections.Generic;
using Dndp.Service.Security;
using Dndp.Persistence.Entity.Security;

public partial class Modules_OffLineReport_BatchMaintenance_NewBatchUser : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("BatchMaintenance");

    //The entity service
    protected IReportBatchMgr TheService
    {
        get
        {
            return GetService("ReportBatchMgr.service") as IReportBatchMgr;
        }
    }

    protected IUserMgr TheUserService
    {
        get
        {
            return GetService("UserMgr.service") as IUserMgr;
        }
    }

    public int TheReportBatchId
    {
        get
        {
            return (int)ViewState["TheReportBatchId"];
        }
        set
        {
            ViewState["TheReportBatchId"] = value;
        }
    }

    //Event handler when user click button "Back"
    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            txtUserName.Text = "";
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

        TheService.AddReportBatchUser(TheReportBatchId, IdList);
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
        IList<User> userList = TheUserService.Search(txtUserName.Text.Trim()) as IList<User>;
        IList<User> resultUserList = new List<User>();
        if (userList != null)
        {
            foreach (User user in userList)
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

    protected bool hasData(int userId)
    {
        IList reportUserList = this.TheService.FindUserByBatchId(TheReportBatchId);
        if (reportUserList != null)
        {
            for (int i = 0; i < reportUserList.Count; i++)
            {
                if (((ReportBatchUser)reportUserList[i]).TheUser.Id == userId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
