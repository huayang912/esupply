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

using Dndp.Persistence.Dao.Security;
using Dndp.Persistence.Entity.Security;
using Dndp.Web;
using Dndp.Service.Security;

public partial class Modules_Security_UserAdmin_Edit : ModuleBase
{
    public event EventHandler Back;

    protected void Page_Load(object sender, EventArgs e)
    {
    }

    protected override void InitByPermission()
    {
        base.InitByPermission();

        txtUserName.ReadOnly = !PermissionUpdate;
        trPassword.Visible = PermissionUpdate;
        trConfirmPassword.Visible = PermissionUpdate;
        txtEmail.ReadOnly = !PermissionUpdate;
        txtWindowsDomain.ReadOnly = !PermissionUpdate;
        txtWindowsUserName.ReadOnly = !PermissionUpdate;

        btnUpdate.Visible = PermissionUpdate;   
    }

    public User TheUser
    {
        get
        {
            return (User)ViewState["TheUser"];
        }
        set
        {
            ViewState["TheUser"] = value;
        }
    }

    public void UpdateView()
    {
        txtUserName.Text = TheUser.UserName;
        txtPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        cbPassword.Checked = false;
        txtEmail.Text = TheUser.Email;
        txtWindowsDomain.Text = TheUser.WindowsDomain;
        txtWindowsUserName.Text = TheUser.WindowsUserName;
        lblMessage.Visible = false;

        UpdateRolesView();
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
    }

    private void UpdateRolesView()
    {
        IRoleMgr roleMgr = GetService("RoleMgr.service") as IRoleMgr;
        IList allRoles = roleMgr.LoadAllRoles();
        IList userRoles = TheUser.Roles;

        DataTable dt = new DataTable();
        dt.Columns.Add("Id", typeof(int));
        dt.Columns.Add("Name", typeof(string));
        dt.Columns.Add("Description", typeof(string));
        dt.Columns.Add("InRole", typeof(bool));

        foreach (Role r in allRoles)
        {
            if (userRoles.Contains(r))
            {
                dt.Rows.Add(r.Id, r.Name, r.Description, true);
            }
            else
            {
                dt.Rows.Add(r.Id, r.Name, r.Description, false);
            }
        }

        gvRole.DataSource = dt;
        gvRole.DataBind();

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        if (userName.Length == 0)
        {
            lblMessage.Text = "Please input user name.";
            return;
        }

        string password = null;
        if (cbPassword.Checked)
        {
            password = txtPassword.Text.Trim();
            string confirmPassword = txtConfirmPassword.Text.Trim();

            if (password != confirmPassword)
            {
                lblMessage.Text = "Please confirm the password.";
                return;
            }
        }

        User u = new User();
        u.Id = TheUser.Id;
        u.UserName = userName;

        if (cbPassword.Checked)
        {
            u.Password = password;
        }
        else
        {
            u.Password = TheUser.Password;
        }

        u.Email = txtEmail.Text.Trim();
        u.WindowsDomain = txtWindowsDomain.Text.Trim();
        u.WindowsUserName = txtWindowsUserName.Text.Trim();

        IList roleIdList = GetSelectedRoleIdList();

        try
        {
            IUserMgr userMgr = GetService("UserMgr.service") as IUserMgr;
            userMgr.UpdateUser(u, roleIdList);
        }
        catch (ApplicationException ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
            return;
        }

        lblMessage.Text = "Update sucessfully.";
        lblMessage.Visible = true;
    }

    private IList GetSelectedRoleIdList()
    {
        IList idList = new ArrayList();
        
        foreach (GridViewRow row in gvRole.Rows)
        {
            CheckBox cbInRole = (CheckBox)(row.FindControl("cbInRole"));
            if (cbInRole.Checked)
            {
                idList.Add((int)(gvRole.DataKeys[row.RowIndex].Value));
            }
        }

        return idList;
    }

    protected void gvRole_DataBound(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvRole.Rows)
        {
            CheckBox cbInRole = (CheckBox)row.FindControl("cbInRole");
            cbInRole.Enabled = PermissionUpdate;
        }
    }
}
