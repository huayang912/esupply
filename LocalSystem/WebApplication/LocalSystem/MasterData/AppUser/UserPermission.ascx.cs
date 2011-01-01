using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;
using com.LocalSystem.Entity;
using com.LocalSystem.Web;
using com.LocalSystem.Entity.MasterData;
using System.Linq;

public partial class Security_User_UserPermission : ModuleBase
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

        IList<AppPermission> NotInAppPermissions = allAppPermissions.Where(p => !(inString.Contains(p.Code))).ToList();
        IList<AppPermission> InAppPermissions = allAppPermissions.Where(p => inString.Contains(p.Code)).ToList();

        this.CBL_NotInPermission.DataSource = NotInAppPermissions;
        this.CBL_InPermission.DataSource = InAppPermissions;

        this.CBL_NotInPermission.DataBind();
        this.CBL_InPermission.DataBind();
        this.cb_InPermission.Checked = false;
        this.cb_NotInPermission.Checked = false;
    }

    protected void ToInBT_Click(object sender, EventArgs e)
    {
        foreach (ListItem item in this.CBL_NotInPermission.Items)
        {
            if (item.Selected)
            {
                AppUserPermission appUserPermission = new AppUserPermission();
                appUserPermission.AppPermission = item.Value;
                appUserPermission.AppUser = this.lbCode.Text;
                TheAppUserPermissionMgr.CreateAppUserPermission(appUserPermission);
            }
        }
        this.InitPageParameter(this.lbCode.Text);
    }

    protected void ToOutBT_Click(object sender, EventArgs e)
    {
        IList<AppUserPermission> appUserPermissions = TheAppUserPermissionMgr.GetAppUserPermission(this.lbCode.Text);
        IList<int> ids = new List<int>();

        foreach (ListItem item in this.CBL_InPermission.Items)
        {
            if (item.Selected)
            {
                int q = appUserPermissions.Where(a => a.AppPermission == item.Value).Select(a => a.Id).FirstOrDefault();
                ids.Add(q);
            }
        }
        if (ids.Count > 0)
        {
            TheAppUserPermissionMgr.DeleteAppUserPermission(ids);
        }
        this.InitPageParameter(this.lbCode.Text);
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
