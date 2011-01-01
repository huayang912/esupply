using System;
using System.Linq;
using System.Web.Services;
using System.Web.Services.Protocols;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.Exception;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.Operation;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;
using com.LocalSystem.Service.Ext.Criteria;

/// <summary>
/// Summary description for BaseWS
/// </summary>
public class BaseWS : System.Web.Services.WebService
{
    public BaseWS()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    protected T GetService<T>(string serviceName)
    {
        return ServiceLocator.GetService<T>(serviceName);
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
    protected IEntityPreferenceMgrE TheEntityPreferenceMgr { get { return GetService<IEntityPreferenceMgrE>("EntityPreferenceMgr.service"); } }
    protected IBarCodeMgrE TheBarCodeMgr { get { return GetService<IBarCodeMgrE>("BarCodeMgr.service"); } }
    //protected ILotNoMgrE TheLotNoMgr { get { return GetService<ILotNoMgrE>("LotNoMgr.service"); } }
    #endregion

    protected string RenderingLanguage(string content, string userCode, params string[] parameters)
    {
        try
        {
            content = ProcessMessage(content, parameters);
            string language = "zh-CN";
            EntityPreference entityPreference = TheEntityPreferenceMgr.LoadEntityPreference(BusinessConstants.CODE_MASTER_LANGUAGE);
            if (entityPreference != null)
            {
                language = entityPreference.Value;
            }
            if (userCode != null && userCode.Trim() != string.Empty)
            {
                AppUser user = TheAppUserMgr.LoadAppUser(userCode);
                language = user.Language;
            }
            content = TheLanguageMgr.ProcessLanguage(content, language);
        }
        catch (Exception ex)
        {
            return content;
        }
        return content;
    }

    private string ProcessMessage(string message, string[] paramters)
    {
        string messageParams = string.Empty;
        if (paramters != null && paramters.Length > 0)
        {
            //处理Message参数
            foreach (string para in paramters)
            {
                messageParams += "," + para;
            }
        }
        message = "${" + message + messageParams + "}";

        return message;
    }

}
