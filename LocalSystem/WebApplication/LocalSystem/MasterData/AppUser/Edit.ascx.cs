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
using com.LocalSystem.Web;
using com.LocalSystem.Entity;
using System.Collections.Generic;

public partial class MasterData_User_Edit : EditModuleBase
{
    public event EventHandler BackEvent;

    protected string UserCode
    {
        get
        {
            return (string)ViewState["UserCode"];
        }
        set
        {
            ViewState["UserCode"] = value;
        }
    }

    protected string Password
    {
        get
        {
            return (string)ViewState["Password"];
        }
        set
        {
            ViewState["Password"] = value;
        }
    }

    public bool IsUserPreference
    {
        get
        {
            return ViewState["IsUserPreference"] == null ? false : (bool)ViewState["IsUserPreference"];
        }
        set
        {
            ViewState["IsUserPreference"] = value;
        }
    }


    public void InitPageParameter(string code, bool isUserPreference)
    {
        this.UserCode = code;
        this.IsUserPreference = IsUserPreference;

        AppUser user = TheAppUserMgr.LoadAppUser(this.UserCode);
        this.rblLanguage.Items[1].Selected = this.rblLanguage.Items[1].Value == user.Language ? true : false;
        this.rblLanguage.Items[0].Selected = this.rblLanguage.Items[0].Value == user.Language ? true : false;

        this.tbAddress.Text = user.Address;
        this.tbCode.Text = user.Code;
        this.tbEmail.Text = user.Email;
        this.tbFax.Text = user.Fax;
        this.tbFirstName.Text = user.FirstName;
        this.cbIsActive.Checked = user.IsActive;
        this.tbLastName.Text = user.LastName;
        this.tbPassword.Text = string.Empty;
        this.tbConfirmPassword.Text = string.Empty;
        this.tbPhone.Text = user.Phone;
        this.Password = user.Password;

        if (IsUserPreference)
        {
            this.btnBack.Visible = false;
            this.btnDelete.Visible = false;
        }
    }
    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        if (this.tbPassword.Text.Trim() != this.tbConfirmPassword.Text.Trim())
        {
            ShowErrorMessage("Security.User.Password.Different");
            return;
        }
        AppUser user = new AppUser();
        user.Address = this.tbAddress.Text.Trim();
        user.Code = this.tbCode.Text.Trim();
        user.Email = this.tbEmail.Text.Trim();
        user.Fax = this.tbFax.Text.Trim();
        user.FirstName = this.tbFirstName.Text.Trim();
        user.IsActive = this.cbIsActive.Checked;
        user.Language = this.rblLanguage.SelectedValue;
        user.LastName = this.tbLastName.Text.Trim();
        if (this.tbPassword.Text != string.Empty)
        {
            string password = this.tbPassword.Text.Trim();
            user.Password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");
        }
        else
        {
            user.Password = this.Password;
        }
        user.Phone = this.tbPhone.Text.Trim();
        TheAppUserMgr.UpdateAppUser(user);
        ShowSuccessMessage("Security.User.UpdateUser.Successfully", UserCode);
    }

    protected void btnDelete_Click(object sender, EventArgs e)
    {
        try
        {
            TheAppUserMgr.DeleteAppUser(this.UserCode);
            btnBack_Click(this, e);
            ShowSuccessMessage("Security.User.DeleteUser.Successfully", UserCode);
        }
        catch (Exception)
        {
            ShowErrorMessage("Security.User.DeleteUser.Fail", UserCode);
            //throw;
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
