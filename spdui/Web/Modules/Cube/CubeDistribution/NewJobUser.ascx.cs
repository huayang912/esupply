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
using Dndp.Persistence.Entity.Cube;
using Dndp.Service.Cube;

public partial class Modules_Cube_CubeDistribution_NewUserReport : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
    private static ILog log = LogManager.GetLogger("CubeDistribution");

    //The entity service
    protected ICubeDistributionJobMgr TheService
    {
        get
        {
            return GetService("CubeDistributionJobMgr.service") as ICubeDistributionJobMgr;
        }
    }

    //The entity service
    protected ICubeAuthorizationMgr TheUserService
    {
        get
        {
            return GetService("CubeAuthorizationMgr.service") as ICubeAuthorizationMgr;
        }
    }

    public CubeDistributionJob TheJob
    {
        get
        {
            return (CubeDistributionJob)ViewState["TheJob"];
        }
        set
        {
            ViewState["TheJob"] = value;
        }
    }

    public IList<CubeDistributionJobItem> TheJobItem
    {
        get
        {
            return (IList<CubeDistributionJobItem>)ViewState["TheJobItem"];
        }
        set
        {
            ViewState["TheJobItem"] = value;
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

        IList<CubeDistributionJobItem> list = TheService.AddCubeDistributionJobItems(TheJob, IdList);       

        //if (list != null)
        //{
        //    if (TheJobItem == null)
        //    {
        //        TheJobItem = new List<CubeDistributionJobItem>();
        //    }

        //    foreach(CubeDistributionJobItem jobItem in list)
        //    {
        //        TheJobItem.(jobItem);
        //    }
        //}
        TheJob.CubeDistributionJobItemList = TheService.FindJobItemByJobId(TheJob.Id);
        TheJobItem = TheJob.CubeDistributionJobItemList;
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
        IList<CubeUser> userList = TheService.FindCubeUserByCubeIdAndUserNameAndUserDescription(TheJob.TheCube.Id, txtUserName.Text.Trim(), txtUserDescription.Text.Trim());
        IList<CubeUser> resultUserList = new List<CubeUser>();
        if (userList != null)
        {
            foreach (CubeUser user in userList)
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
        if (TheJobItem != null)
        {
            for (int i = 0; i < TheJobItem.Count; i++)
            {
                if (TheJobItem[i].TheCubeUser.Id == userId)
                {
                    return true;
                }
            }
        }
        return false;
    }
}
