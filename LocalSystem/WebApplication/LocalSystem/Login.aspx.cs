using System;
using System.Web;
using System.Linq;
using System.Web.Security;
using System.Collections.Generic;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.MasterData;
using com.LocalSystem.Utility;
using com.LocalSystem.Web;
using System.Net;
using com.LocalSystem.Service.Ext.MasterData;
using System.Configuration;
using com.LocalSystem.Entity.Operation;

public partial class Login : PageBase
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!Page.IsPostBack)
        {
            
        }
        this.Title = ConfigurationManager.AppSettings["CompanyName"].ToString();
    }

    protected void Login_Click(object sender, EventArgs e)
    {
        string userCode = this.txtUsername.Value.Trim().ToLower();
        string password = this.txtPassword.Value.Trim();
        password = FormsAuthentication.HashPasswordForStoringInConfigFile(password, "MD5");

        if (userCode.Equals(string.Empty))
        {
            errorLabel.Text = "Please enter your Account!";
            return;
        }

        AppUser user = TheAppUserMgr.LoadAppUser(userCode,true);

        if (user == null)
        {
            errorLabel.Text = "Identification failure. Try again!";
        }
        else
        {
            if (password == user.Password && user.IsActive)
            {
                this.Session["Current_User"] = user;
                Response.Redirect("~/Default.aspx");
            }
            else
            {
                errorLabel.Text = "Identification failure. Try again!";
            }
        }
    }

}