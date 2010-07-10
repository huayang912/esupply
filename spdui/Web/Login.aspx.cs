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

using log4net;

using Dndp.Persistence.Entity.Security;
using Dndp.Web;
using Dndp.Service.Security;

public partial class Login : System.Web.UI.Page
{
    private static ILog log = LogManager.GetLogger("User Login");

    protected void Page_Load(object sender, EventArgs e)
    {
        lblErrorMessage.Visible = false;
    }
    protected void btnLogin_Click(object sender, EventArgs e)
    {
        string userName = txtUserName.Text.Trim();
        string password = txtPassword.Text.Trim();

        log.Info(string.Format("User \"{0}\" try to login.", userName));

        if ((userName.Length == 0) || (password.Length == 0))
        {
            lblErrorMessage.Visible = true;
            return;
        }

        ISecurityMgr securityMgr = ServiceLocator.GetService("SecurityMgr.service") as ISecurityMgr;
        User u = securityMgr.Login(userName, password);

        if (u == null)
        {
            lblErrorMessage.Visible = true;
            return;
        }

        FormsAuthentication.SetAuthCookie(u.UserName, false);
        Session["CurrentUser"] = u;

        // this method no good. We can refactor the code for the request redirect.
        // We don't know how to using the FormsAuthentication for redirect.
        // FormsAuthentication.RedirectFromLoginPage(userName, true);
        String gotourl = null;
        if (Session["RequestUrl"] != null)
        {
            gotourl = Session["RequestUrl"] as string;
            //reset the reuesturl attribute
            Session["RequestUrl"] = null;
        }
        else
        {
            gotourl = FormsAuthentication.GetRedirectUrl(userName, false);
        }
        Response.Redirect(gotourl);
    }
}
