using System.Collections.Generic;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;

//TODO: Add other using statements here.

namespace com.LocalSystem.Service.MasterData
{
    public interface ICodeMasterMgr : ICodeMasterBaseMgr
    {
        #region Customized Methods

        IList<CodeMaster> GetCachedCodeMaster(string code);

        CodeMaster GetCachedCodeMaster(string code, string value);

        IList<CodeMaster> GetCodeMasterList(string code, object[] valueArray);

        CodeMaster GetDefaultCodeMaster(string code);

        string GetRandomTheme(string themeType);

        IList<CodeMaster> GetAllCodeMaster();
        #endregion Customized Methods
    }
}





#region Extend Interface





namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface ICodeMasterMgrE : com.LocalSystem.Service.MasterData.ICodeMasterMgr
    {
        
    }
}

#endregion
