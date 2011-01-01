using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Globalization;
using System.IO;
using System.Linq;
using System.ServiceModel.Channels;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.Operation;
using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Service.Ext.Operation;
using com.LocalSystem.Utility;
using com.LocalSystem.Entity.MasterData;

/// <summary>
/// Summary description for ControlBase
/// </summary>
namespace com.LocalSystem.Web
{
    public abstract class ControlBase : System.Web.UI.UserControl
    {

        #region 变量
        protected AppUser CurrentUser
        {
            get
            {
                return (new SessionHelper(Page)).CurrentUser;
            }
        }

        private IDictionary<string, System.Web.UI.Control> _findControlHelperCache = new Dictionary<string, System.Web.UI.Control>();
        #endregion

        #region 构造函数
        public ControlBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        #endregion

        #region 页面事件
        protected override void Render(HtmlTextWriter writer)
        {
            StringBuilder sb = new StringBuilder();
            StringWriter sw = new StringWriter(sb);
            HtmlTextWriter mywriter = new HtmlTextWriter(sw);
            base.Render(mywriter);
            string content = sb.ToString();
            if (!(CurrentUser != null && CurrentUser.Language != null && CurrentUser.Language != string.Empty))
            {
                //CurrentUser.Language = System.Globalization.CultureInfo.CurrentCulture.Name;
                string language = "zh-CN";
                EntityPreference entityPreference = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.CODE_MASTER_LANGUAGE);
                if (entityPreference != null)
                {
                    language = entityPreference.Value;
                }
                CurrentUser.Language = language;
            }
            content = TheLanguageMgr.ProcessLanguage(content, CurrentUser.Language);
            writer.Write(content);
        }

        /*
         * 用于反射调用,参见GridView
         * 
         */
        public string Render(String content)
        {
            if (CurrentUser != null && CurrentUser.Language != null && CurrentUser.Language != string.Empty)
            {
                //
            }
            else
            {
                CurrentUser.Language = System.Globalization.CultureInfo.CurrentCulture.Name;
            }
            content = TheLanguageMgr.ProcessLanguage(content, CurrentUser.Language);
            return content;
        }


        public HttpResponse GetResponse()
        {
            return Response;
        }
        #endregion

        #region 方法
        protected T GetService<T>(string serviceName)
        {
            return ServiceLocator.GetService<T>(serviceName);
        }

        /// <summary>
        /// This helper automates locating a control by ID.
        /// 
        /// It calls FindControl on the NamingContainer, then the Page.  If that fails,
        /// it fires the resolve event.
        /// </summary>
        /// <param name="id">The ID of the control to find</param>
        /// <param name="props">The TargetProperties class associated with that control</param>
        /// <returns></returns>
        protected System.Web.UI.Control FindControlHelper(string id)
        {
            System.Web.UI.Control c = null;
            if (_findControlHelperCache.ContainsKey(id))
            {
                c = _findControlHelperCache[id];
            }
            else
            {
                c = base.FindControl(id);  // Use "base." to avoid calling self in an infinite loop
                System.Web.UI.Control nc = NamingContainer;
                while ((null == c) && (null != nc))
                {
                    c = nc.FindControl(id);
                    nc = nc.NamingContainer;
                }

                if (null != c)
                {
                    _findControlHelperCache[id] = c;
                }
            }
            return c;
        }

        public override System.Web.UI.Control FindControl(string id)
        {
            // Use FindControlHelper so that more complete searching and OnResolveControlID will be used
            return FindControlHelper(id);
        }
        #endregion

        #region Services
        protected ICriteriaMgrE TheCriteriaMgr { get { return GetService<ICriteriaMgrE>("CriteriaMgr.service"); } }
        protected ILanguageMgrE TheLanguageMgr { get { return GetService<ILanguageMgrE>("LanguageMgr.service"); } }
        protected IAppUserMgrE TheAppUserMgr { get { return GetService<IAppUserMgrE>("AppUserMgr.service"); } }
        protected IAppPermissionMgrE TheAppPermissionMgr { get { return GetService<IAppPermissionMgrE>("AppPermissionMgr.service"); } }
        protected IAppUserPermissionMgrE TheAppUserPermissionMgr { get { return GetService<IAppUserPermissionMgrE>("AppUserPermissionMgr.service"); } }
        protected ICodeMasterMgrE TheCodeMasterMgr { get { return GetService<ICodeMasterMgrE>("CodeMasterMgr.service"); } }
        protected IItemMgrE TheItemMgr { get { return GetService<IItemMgrE>("ItemMgr.service"); } }
        protected ISupplierMgrE TheSupplierMgr { get { return GetService<ISupplierMgrE>("SupplierMgr.service"); } }
        protected IOutboundLogMgrE TheOutboundLogMgr { get { return GetService<IOutboundLogMgrE>("OutboundLogMgr.service"); } }
        protected IPoDetailMgrE ThePoDetailMgr { get { return GetService<IPoDetailMgrE>("PoDetailMgr.service"); } }
        protected IPoMgrE ThePoMgr { get { return GetService<IPoMgrE>("PoMgr.service"); } }
        protected IEntityPreferenceMgrE TheEntityPreferenceMgr { get { return GetService<IEntityPreferenceMgrE>("EntityPreferenceMgr.service"); } }
        protected IBarCodeMgrE TheBarCodeMgr { get { return GetService<IBarCodeMgrE>("BarCodeMgr.service"); } }
        protected IBusinessMgrE TheBusinessMgr { get { return GetService<IBusinessMgrE>("BusinessMgr.service"); } }
        #endregion
    }
}
