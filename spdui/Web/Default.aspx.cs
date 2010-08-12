using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Dndp.Web;
using log4net;

public partial class _Default : System.Web.UI.Page 
{
    protected void Page_Load(object sender, EventArgs e)
    {
        
    }

    protected override void OnInit(EventArgs e)
    {
        base.OnInit(e);
        DomainLogin();  //new
        LoadModule();
    }

    private void DomainLogin()  //new function
    {
        if (Session["CurrentUser"] != null)
        {
            return;
        }

        Dndp.Service.Security.ISecurityMgr securityMgr = ServiceLocator.GetService("SecurityMgr.service") as Dndp.Service.Security.ISecurityMgr;
        if (this.User.Identity.Name != null && this.User.Identity.Name.Trim() != string.Empty)
        {
            string domainFullName = this.User.Identity.Name;
            string[] ary = domainFullName.Split('\\');
            Dndp.Persistence.Entity.Security.User u = securityMgr.DomainLogin(ary[0], ary[1]);

            if (u == null)
            {
                //**********登陆失败处理，可以redirect到一个页面，提示无权限访问本系统***********
                Response.Redirect("NoPermission.htm", true);
                return;
            }

            FormsAuthentication.SetAuthCookie(u.UserName, false);
            Session["CurrentUser"] = u;
        }
        else
        {
            Response.Redirect("LoginPage.aspx", true);
            return;
        }
    }		

    private void LoadModule()
    {
        if (Request.Params["mid"] == null)
        {
            LoadModule(5);
            return;
        }

        int mid = 0;
        try
        {
            mid = int.Parse(Request.Params["mid"] as string);
        }
        catch
        {
            return;
        }

        if (mid == 0)
        {
            return;
        }

        LoadModule(mid);
    }

    private void LoadModule(int mid)
    {
        foreach (Dndp.Persistence.Entity.Security.Authorization auth in CurrentUser.Authorizations)
        {
            if (auth.TheModule.Id == mid)
            {
                ModuleBase ctlModule = (ModuleBase)(Page.LoadControl(auth.TheModule.SourceFile));
                ctlModule.CurrentModuleName = auth.TheModule.Name;
                phModule.Controls.Add(ctlModule);
                lblErrorMessage.Visible = false;
                return;
            }
        }

        lblErrorMessage.Text = "Sorry, you have no permission to access this module.";
        lblErrorMessage.Visible = true;

        return;
    }

    private Dndp.Persistence.Entity.Security.User CurrentUser
    {
        get
        {
            return (new SessionHelper(this)).CurrentUser;
        }
    }
}
