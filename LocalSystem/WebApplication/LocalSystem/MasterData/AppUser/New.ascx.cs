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
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;
using com.LocalSystem.Web;
using com.LocalSystem.Entity;

public partial class MasterData_User_New : NewModuleBase
{
    public event EventHandler CreateEvent;
    public event EventHandler BackEvent;

    public void PageCleanup()
    {
        this.tbAddress.Text = string.Empty;
        this.tbCode.Text = string.Empty;
        this.tbConfirmPassword.Text = string.Empty;
        this.tbEmail.Text = string.Empty;
        this.tbFax.Text = string.Empty;
        this.tbFirstName.Text = string.Empty;
        this.tbLastName.Text = string.Empty;
        this.tbPassword.Text = string.Empty;
        this.tbPhone.Text = string.Empty;
        this.cbIsActive.Checked = true;
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {
        if (TheAppUserMgr.LoadAppUser(this.tbCode.Text.Trim()) != null)
        {
            ShowErrorMessage("Security.User.Code.Exists", this.tbCode.Text.Trim());
            return;
        }
        if (this.tbPassword.Text.Trim() != this.tbConfirmPassword.Text.Trim())
        {
            ShowErrorMessage("Security.User.Password.Different");
            return;
        }
        string password = this.tbPassword.Text.Trim();
        password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        AppUser user = new AppUser();
        user.Address = this.tbAddress.Text.Trim();
        user.Code = this.tbCode.Text.Trim();
        user.Email = this.tbEmail.Text.Trim();
        user.Fax = this.tbFax.Text.Trim();
        user.FirstName = this.tbFirstName.Text.Trim();
        user.IsActive = this.cbIsActive.Checked;
        user.Language = this.rblLanguage.SelectedValue;
        user.LastName = this.tbLastName.Text.Trim();
        user.Password = password;
        user.Phone = this.tbPhone.Text.Trim();
        TheAppUserMgr.CreateAppUser(user);
        ShowSuccessMessage("Security.User.AddUser.Successfully", user.Code);
    }

    protected void checkUserExists(object source, ServerValidateEventArgs args)
    {
        string code = this.tbCode.Text.ToLower();
        AppUser user = TheAppUserMgr.LoadAppUser(code);

        if (user != null)
        {
            args.IsValid = false;
        }
    }

    protected void btnBack_Click(object sender, EventArgs e)
    {
        if (BackEvent != null)
        {
            BackEvent(this, e);
        }
    }
}
