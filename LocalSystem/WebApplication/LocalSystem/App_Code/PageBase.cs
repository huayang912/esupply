using com.LocalSystem.Service.Ext.Criteria;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;
using com.LocalSystem.Service.Ext.Operation;
using com.LocalSystem.Entity.MasterData;


/// <summary>
/// Summary description for PageBase
/// </summary>

namespace com.LocalSystem.Web
{
    public class PageBase : System.Web.UI.Page
    {
        public PageBase()
        {
            //
            // TODO: Add constructor logic here
            //
        }
        protected T GetService<T>(string serviceName)
        {
            return ServiceLocator.GetService<T>(serviceName);
        }

        protected AppUser CurrentUser
        {
            get
            {
                return (new SessionHelper(this.Page)).CurrentUser;
            }
        }
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
        #endregion
    }
}
