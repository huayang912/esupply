using com.LocalSystem.Service.Ext.MasterData;

using com.LocalSystem.Entity.MasterData;
namespace com.LocalSystem.Service.MasterData
{
    public interface ILanguageMgr
    {
        string ProcessLanguage(string content, string language);

        void ReLoadLanguage();

        string TranslateMessage(string content, string userCode);

        string TranslateMessage(string content, AppUser user);

        string TranslateMessage(string content, string userCode, params string[] parameters);

        string TranslateMessage(string content, AppUser user, params string[] parameters);

        string TranslateContent(string content, string language, params string[] parameters);
    }
}



#region Extend Interface
namespace com.LocalSystem.Service.Ext.MasterData
{
    public partial interface ILanguageMgrE : com.LocalSystem.Service.MasterData.ILanguageMgr
    {
        
    }
}
#endregion
