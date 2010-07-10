using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;

using Dndp.Service.Security;
using Dndp.Persistence.Entity.Security;
using System.Collections.Specialized;

namespace Dndp.Web
{

    /// <summary>
    /// Summary description for SessionHelper
    /// </summary>
    public class SessionHelper
    {
        private System.Web.UI.Page _page;

        public SessionHelper(Page page)
        {
            _page = page;
        }

        public User CurrentUser
        {
            get
            {
                if (_page.Session["CurrentUser"] == null)
                {
                    FormsAuthentication.SignOut();
                    _page.Session.RemoveAll();

                    //this code for the request redirect. need to refactor.
                    if (GetQueryString(_page).Length > 0)
                    {
                        _page.Session["RequestUrl"] = ApplicationConstant.DEFAULT_PAGE + "?" + GetQueryString(_page);
                    }
                    _page.Response.Redirect("Login.aspx");
                }

                return _page.Session["CurrentUser"] as User;
            }
            set
            {
                _page.Session["CurrentUser"] = value;
            }
        }

        public string CurrentModuleName
        {
            get
            {
                if (_page.Session["CurrentModuleName"] == null)
                {
                    FormsAuthentication.SignOut();
                    _page.Session.RemoveAll();
                    _page.Response.Redirect("Login.aspx");
                }

                return _page.Session["CurrentModuleName"] as string;
            }
            set
            {
                _page.Session["CurrentModuleName"] = value;
            }
        }
        private string GetQueryString(Page page)
        {
            string queryString = "";

            NameValueCollection qs = page.Request.QueryString;

            foreach (string key in qs.AllKeys)
                foreach (string value in qs.GetValues(key))
                    queryString += page.Server.UrlEncode(key) + "=" + page.Server.UrlEncode(value) + "&";

            return queryString.TrimEnd('&');
        }
    }

}