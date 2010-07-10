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

using Dndp.Web;

public partial class UserControls_Top : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        tdAfterLogin.Visible = Request.IsAuthenticated;
        if (Request.IsAuthenticated)
        {
            lblCurrentUser.Text = "Current User: " + (new SessionHelper(Page)).CurrentUser.UserName;
        }
    }
}
