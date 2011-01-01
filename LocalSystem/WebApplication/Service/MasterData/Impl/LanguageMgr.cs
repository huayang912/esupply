using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text.RegularExpressions;
using Castle.Services.Transaction;
using com.LocalSystem.Entity;
using com.LocalSystem.Entity.Exception;
using com.LocalSystem.Entity.MasterData;
using com.LocalSystem.Service.Ext.MasterData;
using com.LocalSystem.Utility;

namespace com.LocalSystem.Service.MasterData.Impl
{
    [Transactional]
    public class LanguageMgr : SessionBase, ILanguageMgr
    {
        public string languageFileFolder { get; set; }
        public IDictionary<string, IDictionary<string, string>> languageDic;
        public ICodeMasterMgrE codeMasterMgrE { get; set; }
        public IAppUserMgrE userMgrE { get; set; }

        protected Regex regex = new Regex("\\${[\\w \\. , -]+?}", RegexOptions.Singleline);
        protected char[] prefix = new char[] { '$', '{' };
        protected char[] surfix = new char[] { '}' };

        #region ILanguageMgrE Members
        public string ProcessLanguage(string content, string language)
        {
            if (languageDic == null)
            {
                this.LoadLanguage();
            }
            if (languageDic.ContainsKey(language))
            {
                IDictionary<string, string> targetLanguageDic = languageDic[language];
                MatchCollection mc = regex.Matches(content);

                for (int i = 0; i < mc.Count; i++)
                {
                    string[] splitKey = mc[i].Value.TrimStart(prefix).TrimEnd(surfix).Split(',');
                    string actualKey = splitKey[0];
                    if (targetLanguageDic.ContainsKey(actualKey))
                    {
                        //处理Message中的参数
                        string value = targetLanguageDic[actualKey];
                        if (splitKey.Length > 1)
                        {
                            string[] para = new string[splitKey.Length - 1];
                            for (int j = 1; j < splitKey.Length; j++)
                            {
                                para[j - 1] = splitKey[j].Trim();
                            }
                            try
                            {
                                value = string.Format(value, para);
                            }
                            catch (Exception ex)
                            {
                                //throw;
                            }
                        }

                        content = content.Replace(mc[i].Value, value);
                    }
                }
            }
            else
            {
                throw new TechnicalException("没有指定类型的语言:" + language);
            }

            return content;
        }

        public void ReLoadLanguage()
        {
            this.LoadLanguage();
        }
        public string TranslateMessage(string content, string userCode)
        {
            return this.TranslateMessage(content, userCode, null);
        }
        public string TranslateMessage(string content, AppUser user)
        {
            return this.TranslateMessage(content, user, null);
        }
        public string TranslateMessage(string content, string userCode, params string[] parameters)
        {
            AppUser user = userMgrE.LoadAppUser(userCode);
            return this.TranslateMessage(content, user, parameters);
        }
        public string TranslateMessage(string content, AppUser user, params string[] parameters)
        {
            string language = null;
            if (user != null && user.Language != null && user.Language != string.Empty)
            {
                language = user.Language;
            }
            else
            {
                language = "zh-CN";
            }
            return TranslateContent(content, language, parameters);
        }

        public string TranslateContent(string content, string language, params string[] parameters)
        {
            try
            {
                content = ProcessMessage(content, parameters);
                if (language == null || language.Trim() == string.Empty)
                {
                    language = "zh-CN";
                }
                content = this.ProcessLanguage(content, language);
            }
            catch (Exception ex)
            {
                throw new TechnicalException("TranslateContent Exception:" + ex.Message);
            }
            return content;
        }
        #endregion

        protected void LoadLanguage()
        {
            languageDic = new Dictionary<string, IDictionary<string, string>>();
            IList<CodeMaster> languages = codeMasterMgrE.GetCachedCodeMaster(BusinessConstants.CODE_MASTER_LANGUAGE);
            foreach (CodeMaster language in languages)
            {
                string languageKey = language.Value;
                string resourceFile = languageFileFolder + "/Language_" + languageKey + ".properties";
                IDictionary<string, string> targetLanguageDic = new Dictionary<string, string>();

                PropertyFileReader propertyFileReader = new PropertyFileReader(resourceFile);
                while (!propertyFileReader.EndOfStream)
                {
                    string[] property = propertyFileReader.GetPropertyLine();
                    if (property != null)
                    {
                        try
                        {
                            targetLanguageDic.Add(property[0].Trim(), property[1].Trim());
                        }
                        catch (Exception)
                        {
                        }
                    }
                }

                try
                {
                    string resourceExtFile = languageFileFolder + "/Language-ext_" + languageKey + ".properties";
                    if (File.Exists(resourceExtFile))
                    {
                        PropertyFileReader propertyExtFileReader = new PropertyFileReader(resourceExtFile);
                        while (!propertyExtFileReader.EndOfStream)
                        {
                            string[] property = propertyExtFileReader.GetPropertyLine();
                            if (property != null)
                            {
                                try
                                {
                                    if (targetLanguageDic.ContainsKey(property[0].Trim()))
                                    {
                                        targetLanguageDic.Remove(property[0].Trim());
                                    }
                                    targetLanguageDic.Add(property[0].Trim(), property[1].Trim());
                                }
                                catch (Exception)
                                {
                                }
                            }
                        }

                    }
                }
                catch (Exception e)
                {
                    log.Error(e.Message, e);
                }

                languageDic.Add(languageKey, targetLanguageDic);
            }
        }

        protected string ProcessMessage(string message, string[] paramters)
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
}

#region Extend Class
namespace com.LocalSystem.Service.Ext.MasterData.Impl
{
    [Transactional]
    public partial class LanguageMgrE : com.LocalSystem.Service.MasterData.Impl.LanguageMgr, ILanguageMgrE
    {
    }
}

#endregion
