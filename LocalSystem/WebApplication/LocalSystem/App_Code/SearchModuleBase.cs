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
using System.Collections.Generic;
using com.LocalSystem.Service.Ext.MasterData;
using System.Text.RegularExpressions;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Entity.Exception;

/// <summary>
/// Summary description for SearchModuleBase
/// </summary>
namespace com.LocalSystem.Web
{
    public abstract class SearchModuleBase : ModuleBase
    {
        public SearchModuleBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        protected abstract void InitPageParameter(IDictionary<string, string> parameters);

        protected abstract void DoSearch();

        public void QuickSearch(IDictionary<string, string> parameters)
        {
            InitPageParameter(parameters);
            DoSearch();
        }

    }
}
