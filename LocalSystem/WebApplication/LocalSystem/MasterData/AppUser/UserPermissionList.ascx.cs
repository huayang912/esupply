using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Web;
using System.Collections.Generic;
using NHibernate;
using NHibernate.Expression;


public partial class Security_User_UserPermissionList : ModuleBase
{
    public event EventHandler BackEvent; 

    protected void Page_Load(object sender, EventArgs e)
    {
       
    }

    public void InitPageParameter(string code)
    {
        this.lbCode.Text = code;

        IList<AppPermission> allAppPermissions = TheAppPermissionMgr.GetAllAppPermission();
        List<string> inString = TheAppUserMgr.CheckAndLoadAppUser(code).AppUserPermissions.Select(p => p.AppPermission).ToList();
        inString = inString == null ? new List<string>() : inString;

        IList<AppPermission> InAppPermissions = allAppPermissions.Where(p => inString.Contains(p.Code)).ToList();

        this.GV_List.DataSource = InAppPermissions;
        this.GV_List.DataBind();
    }
    protected void GV_List_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        this.GV_List.DataBind();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
