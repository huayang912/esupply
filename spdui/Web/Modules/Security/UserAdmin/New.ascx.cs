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

public partial class Modules_Security_UserAdmin_New : ModuleBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    public event EventHandler Back;

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        AddUser();
        string userName = txtUserName.Text.Trim();
        lblMessage.Text = "Add new user "+userName+" successfully.";
        lblMessage.Visible = true;
    }
    protected void btnSubmitContinue_Click(object sender, EventArgs e)
    {
        AddUser();
        string userName = txtUserName.Text.Trim();
        lblMessage.Text = "Add new user " + userName + " successfully.";
        lblMessage.Visible = true;
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtWindowsDomain.Text = string.Empty;
        txtWindowsUserName.Text = string.Empty;
        foreach (GridViewRow row in gvRole.Rows)
        {
            CheckBox cbInRole = (CheckBox)(row.FindControl("cbInRole"));
            cbInRole.Checked = false;
        }
    }

    protected void AddUser()
    {
        string userName = txtUserName.Text.Trim();
        if (userName.Length == 0)
        {
            lblMessage.Text = "Please input user name.";
            return;
        }

        string password = txtPassword.Text.Trim();
        string confirmPassword = txtConfirmPassword.Text.Trim();
        if (password != confirmPassword)
        {
            lblMessage.Text = "Please confirm the password.";
            return;
        }

        User u = new User();
        u.UserName = userName;
        u.Password = password;
        u.Email = txtEmail.Text.Trim();
        u.WindowsDomain = txtWindowsDomain.Text.Trim();
        u.WindowsUserName = txtWindowsUserName.Text.Trim();

        try
        {
            IUserMgr userMgr = GetService("UserMgr.service") as IUserMgr;
            userMgr.CreateUser(u, GetSelectedRoleIdList());
        }
        catch (ApplicationException ex)
        {
            lblMessage.Text = ex.Message;
            lblMessage.Visible = true;
            return;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (Back != null)
        {
            Back(this, e);
        }
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

    private void UpdateRoleListView()
    {
        IRoleMgr roleMgr = GetService("RoleMgr.service") as IRoleMgr;
        gvRole.DataSource = roleMgr.LoadAllRoles();
        gvRole.DataBind();
    }

    public void UpdateView()
    {
        txtUserName.Text = string.Empty;
        txtPassword.Text = string.Empty;
        txtConfirmPassword.Text = string.Empty;
        txtEmail.Text = string.Empty;
        txtWindowsDomain.Text = string.Empty;
        txtWindowsUserName.Text = string.Empty;
        lblMessage.Visible = false;
        
        UpdateRoleListView();
        
    }
}
