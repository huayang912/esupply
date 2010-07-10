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
using Dndp.Persistence.Entity.Security;
using Dndp.Web;

public partial class UserControls_TreeMenu : System.Web.UI.UserControl
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
        {
            InitTreeView();
        }
    }

    private void InitTreeView()
    {
        Queue q = new Queue();

        //fix: the CurrentUser is null error. refactor this code need.
        User user = (new SessionHelper(Page)).CurrentUser;
        if (user != null)
        {

            IList allMenus = user.Menus;

            //add root menus
            foreach (Dndp.Persistence.Entity.Security.Menu menu in allMenus)
            {
                if (menu.ParentMenuId == 0)
                {
                    TreeNode node = Menu2TreeNode(menu);
                    TreeView1.Nodes.Add(node);
                    q.Enqueue(node);
                }
            }

            //add other menus, BSF algorithm
            while (q.Count > 0)
            {
                TreeNode node = (TreeNode)q.Dequeue();

                IList subMenuList = GetSubMenuList(int.Parse(node.Value));

                foreach (Dndp.Persistence.Entity.Security.Menu subMenu in subMenuList)
                {
                    TreeNode childNode = Menu2TreeNode(subMenu);
                    node.ChildNodes.Add(childNode);
                    q.Enqueue(childNode);
                }
            }
        }
    }

    private TreeNode Menu2TreeNode(Dndp.Persistence.Entity.Security.Menu menu)
    {
        TreeNode node = new TreeNode();
        node.Text = menu.Title;
        node.ToolTip = menu.Description;
        node.Value = menu.Id.ToString();
        node.NavigateUrl = GetMenuUrl(menu);

        if (node.NavigateUrl.Length == 0)
        {
            node.SelectAction = TreeNodeSelectAction.None;
        }
        return node;
    }

    private IList GetSubMenuList(int menuId)
    {
        IList result = new ArrayList();

        IList allMenus = (new SessionHelper(Page)).CurrentUser.Menus;

        foreach (Dndp.Persistence.Entity.Security.Menu menu in allMenus)
        {
            if (menu.ParentMenuId == menuId)
            {
                result.Add(menu);
            }
        }

        return result;
    }

    private string GetMenuUrl(Dndp.Persistence.Entity.Security.Menu menu)
    {
        if (menu.TheModule == null)
        {
            return string.Empty;
        }
        else
        {
            return Request.ApplicationPath + "/Default.aspx?mid=" + menu.TheModule.Id.ToString();
        }
    }


}
