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

using Solpart.WebControls;

using Dndp.Web;


public partial class UserControls_MenuBar : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        InitMenus();
    }

    private void InitMenus()
    {
        foreach (Dndp.Persistence.Entity.Security.Menu menu in (new SessionHelper(Page)).CurrentUser.Menus)
        {
            if (menu.ParentMenuId == 0)
            {
                ctlSolpartMenu.AddMenuItem(menu.Id.ToString(), menu.Title, GetMenuUrl(menu));
            }
            else
            {
                ctlSolpartMenu.AddMenuItem(menu.ParentMenuId.ToString(), menu.Id.ToString(), menu.Title, GetMenuUrl(menu));
            }
        }
    }

    private string GetMenuUrl(Dndp.Persistence.Entity.Security.Menu menu)
    {
        if (menu.TheModule == null)
        {
            return string.Empty;
        }
        else
        {
            return ApplicationConstant.DEFAULT_PAGE + "?mid=" + menu.TheModule.Id.ToString();
        }
    }

}
