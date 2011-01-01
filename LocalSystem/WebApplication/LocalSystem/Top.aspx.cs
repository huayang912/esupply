using System;
using com.LocalSystem.Service.MasterData;
using com.LocalSystem.Utility;
using System.Threading;
using System.Globalization;
using com.LocalSystem.Entity;
using System.Drawing;
using System.Web;
using com.LocalSystem.Web;
using System.Configuration;

public partial class Top : PageBase
{

    protected override void InitializeCulture()
    {
        if (!(this.CurrentUser.Language == null || this.CurrentUser.Language.Trim() == string.Empty))
        {
            Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.CurrentUser.Language);
        }
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        HttpCookie cookie = Request.Cookies["OEM"];
        if (cookie != null)
        {
            string imagePath = (cookie.Value == null || cookie.Value.Trim() == string.Empty) ? "Images/" : cookie.Value.Trim();
            this.LeftImage.ImageUrl = imagePath + "Logo.png";
        }
        this.Info.ForeColor = Color.Black;

        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        this.Info.Text = ConfigurationManager.AppSettings["CompanyName"].ToString();

        this.logoffHL.NavigateUrl = "~/Logoff.aspx";
        this.logoffHL.Target = "_parent";

        if (this.Info.Text.ToLower().Contains("test"))
        {
            this.imgTest.Visible = true;
        }
    }
}
