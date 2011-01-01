using System;
using System.Web.UI.HtmlControls;
using com.LocalSystem.Web;

public partial class Lefttop : PageBase
{

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] != null)
        {
            this.SUser.Text = this.CurrentUser.Code;
            this.SUser.Text += " [" + this.CurrentUser.Name + "]";
        }
        else
            this.Response.Redirect("~/Logoff.aspx");
    }
}
