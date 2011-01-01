using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.LocalSystem.Service.Ext.Criteria;
using NHibernate.Expression;
using NHibernate;
using com.LocalSystem.Utility;
using System.Collections.Generic;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.MasterData;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;

/// <summary>
/// Summary description for ListModuleBase
/// </summary>
namespace com.LocalSystem.Web
{
    public abstract class ListModuleBase : ModuleBase
    {
        public ListModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        public void SetSearchCriteria(DetachedCriteria SelectCriteria, DetachedCriteria SelectCountCriteria)
        {
            new SessionHelper(this.Page).AddUserSelectCriteria(this.TemplateControl.AppRelativeVirtualPath, SelectCriteria, SelectCountCriteria);
        }

        public void SetSearchCriteria(DetachedCriteria SelectCriteria, DetachedCriteria SelectCountCriteria, IDictionary<string, string> alias)
        {
            new SessionHelper(this.Page).AddUserSelectCriteria(this.TemplateControl.AppRelativeVirtualPath, SelectCriteria, SelectCountCriteria, alias);
        }

        public abstract void UpdateView();
        
    }
}
