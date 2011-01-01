using System;
using System.Web;
using com.LocalSystem.Entity;
using com.LocalSystem.Service.MasterData;
using com.LocalSystem.Utility;
using com.LocalSystem.Web;
using System.Configuration;

public partial class _Default : PageBase
{
    protected int leftdownHeight = 0;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        else
        {
            this.Title = ConfigurationManager.AppSettings["CompanyName"].ToString();

            if (!Page.IsPostBack)
            {

            }
        }
    }

}