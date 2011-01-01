using System;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using com.LocalSystem.Web;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;
using System.Globalization;
using System.Threading;
using com.LocalSystem.Entity;

public partial class Nav : PageBase
{
    private bool result = false;

    private string language;
    private string defaultLanguage = ConfigurationManager.AppSettings["DefaultLanguage"].ToString();

    #region 多语言
    protected override void InitializeCulture()
    {
        this.language = this.CurrentUser.Language;
        if (this.language == null || this.language.Trim() == string.Empty)
        {
            this.language = System.Globalization.CultureInfo.CurrentCulture.Name;
            return;
        }

        Thread.CurrentThread.CurrentUICulture = new CultureInfo(this.language);
    }
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
        else
        {
            this.TreeView1.TreeNodeDataBound += new TreeNodeEventHandler(TreeView1_TreeNodeDataBound);
            this.id_hideUser.Value = this.CurrentUser.Code;

            if (!Page.IsPostBack)
            {
                result = true;
            }
        }
    }

    protected void TreeView1_TreeNodeDataBound(object sender, TreeNodeEventArgs e)
    {
        TreeNode treeNode = e.Node;
        SiteMapNode siteMapNode = (SiteMapNode)treeNode.DataItem;

        if (siteMapNode.Url == string.Empty)
        {
            e.Node.SelectAction = TreeNodeSelectAction.Expand;
        }

        #region 生成Icon
        string imageUrl = "\\Nav\\" + siteMapNode.Description + ".png";
        string imageMapPath = Server.MapPath("Images") + imageUrl;
        if (File.Exists(imageMapPath))
        {
            treeNode.ImageUrl = "~\\Images\\" + imageUrl;
        }
        else
        {
            treeNode.ImageUrl = "~/Images/Nav/Default.png";
        }
        #endregion

        #region 判断权限
        if (this.CurrentUser.Code.ToLower() == "admin" || HasPermission(siteMapNode))
        {
            //string text = TheLanguageMgr.TranslateContent(treeNode.Text, this.language);
            //string toolTip = TheLanguageMgr.TranslateContent(siteMapNode.ResourceKey, this.language);
            treeNode.ToolTip = siteMapNode.ResourceKey;
            //treeNode.Text = "&nbsp;" + text;
        }
        else
        {
            try
            {
                treeNode.Parent.ChildNodes.Remove(treeNode);
            }
            catch (Exception)
            {
                this.TreeView1.Nodes.Remove(treeNode);
            }
        }
        #endregion

        if (result)
        {
            try
            {
                this.TreeView1.Nodes[0].Expand();
            }
            catch (Exception)
            {
                //throw;
            }
        }
    }

    protected void TreeView1_SelectedNodeChanged(object sender, EventArgs e)
    {
        if (this.Session["Current_User"] == null)
        {
            this.Response.Redirect("~/Logoff.aspx");
        }
    }

    private bool HasPermission(SiteMapNode siteMapNode)
    {
        string url = siteMapNode.Url.Trim();
        url = url.Contains("Main.aspx") ? ("~/" + url.Remove(0, siteMapNode.Url.IndexOf("Main.aspx"))) : string.Empty;

        if (this.CurrentUser.HasPermission(url))
        {
            return true;
        }
        else
        {
            if (siteMapNode.ChildNodes != null && siteMapNode.ChildNodes.Count > 0)
            {
                foreach (SiteMapNode childSiteMapNode in siteMapNode.ChildNodes)
                {
                    if (HasPermission(childSiteMapNode))
                    {
                        return true;
                    }
                }
            }
            return false;
        }
    }
}