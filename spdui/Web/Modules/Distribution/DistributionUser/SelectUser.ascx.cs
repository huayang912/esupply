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
using Dndp.Service.Distribution;
using Dndp.Persistence.Entity.Distribution;

//TODO: Add other using statements here.

public partial class Modules_Distribution_DistributionUser_SelectUser : ModuleBase
{
    public event EventHandler Back;

    //Get the logger
	private static ILog log = LogManager.GetLogger("DistributionUser");
	
	//The entity service
    protected IDistributionUserMgr TheService
    {
        get
        {
            return GetService("DistributionUserMgr.service") as IDistributionUserMgr;
        }
    }

    private DistributionUser _selectedDistributionUser;
    public DistributionUser SelectedDistributionUser
    {
        get
        {
            return _selectedDistributionUser;
        }
        set
        {
            _selectedDistributionUser = value;
        }
    }

    public bool IsOfflineReportUser
    {
        get
        {
            return ViewState["IsOfflineReportUser"] != null ? (bool)ViewState["IsOfflineReportUser"] : false;
        }
        set
        {
            ViewState["IsOfflineReportUser"] = value;
        }
    }

    public bool IsOfflineCubeUser
    {
        get
        {
            return ViewState["IsOfflineCubeUser"] != null ? (bool)ViewState["IsOfflineCubeUser"] : false;
        }
        set
        {
            ViewState["IsOfflineCubeUser"] = value;
        }
    }

    public bool IsOnlineCubeUser
    {
        get
        {
            return ViewState["IsOnlineCubeUser"] != null ? (bool)ViewState["IsOnlineCubeUser"] : false;
        }
        set
        {
            ViewState["IsOnlineCubeUser"] = value;
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
		//TODO: Add code for Page_Load here.
        //if (!IsPostBack)
        //{
        //    UpdateView();
        //}
    }
   
	
	//The event handler when user button "Search".
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        UpdateView();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    //Do data query and binding.
    public void UpdateView()
    {
        //TODO: Add your code to do data query and binding here.
        IList<DistributionUser> result = null;
        if (txtUserName.Text.Trim().Length != 0) 
        {
            result = TheService.FindDistributionUserByName(txtUserName.Text, IsOfflineReportUser, IsOfflineCubeUser, IsOnlineCubeUser);
        } 
        else
        {
            result = TheService.LoadAllActiveDistributionUser(IsOfflineReportUser, IsOfflineCubeUser, IsOnlineCubeUser) as IList<DistributionUser>;
        }
        int recordCount = 0;
        if (result != null)
        {
            recordCount = result.Count;
        }
        lblRecordCount.Text = string.Format("Total {0} Records", recordCount);
        gvList.DataSource = result;
        gvList.DataBind();
    }

    protected void gvList_SelectedIndexChanged(object sender, EventArgs e)
    {
        int id = (int)(gvList.SelectedDataKey.Value);
        SelectedDistributionUser = TheService.LoadDistributionUser(id);

        if (Back != null)
        {
            Back(this, e);
        }
    }

    protected void gvList_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvList.PageIndex = e.NewPageIndex;
        UpdateView();
    }
}