using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.Collections.Specialized;
using System.Collections;
using System.IO;

namespace SPCubeUtility
{
    public class PortalService
    {
        private const string SUCCEED_RESULT = "<Result/>";        
        private const string DWS_SERVICE_URL_FORMAT = "http://{0}/_vti_bin/dws.asmx";
        private const string UPLOAD_SERVICE_URL_FORMAT = "http://{0}/_vti_bin/wsupload.asmx";
        private const string ALREADY_EXISTS = "<Error ID=\"13\">AlreadyExists</Error>";
        //private string siteDWSServiceUrl = "http://#SERVER#/{0}/_vti_bin/dws.asmx";        
        private string siteDWSServiceUrl = "http://{0}/_vti_bin/dws.asmx";        
        private DwsService.Dws dws;
        private UploadService.WSUpload upload;
        private string _server = "";
        public PortalService(bool encryptPassword, string userName, string password, string domain, string serverName,
           int timeOutSeconds)
        {
            if (encryptPassword)
            {
                //DecryptPassword
                password = SPEncryptUtility.DESEncrypt.Decrypt(password);
            }
            dws = new DwsService.Dws();
            upload = new UploadService.WSUpload();
            NetworkCredential credential = new NetworkCredential(userName, password, domain);
            dws.Credentials = credential;
            upload.Credentials = credential;
            dws.Url = string.Format(DWS_SERVICE_URL_FORMAT, serverName);
            upload.Timeout = timeOutSeconds * 1000;
            upload.Url = string.Format(UPLOAD_SERVICE_URL_FORMAT, serverName);
            //siteDWSServiceUrl = siteDWSServiceUrl.Replace("#SERVER#", serverName);
            _server = serverName;
        }

        public string CreateFolder(string siteName, string folder)
        {
            dws.Url = string.Format(siteDWSServiceUrl, siteName.ToLower().Replace("http://", ""));
            string result = "";
            string[] folders = folder.Split('/');
            string currentFolder = folders[0];
            
            for (int i = 1; i < folders.Length; i++)
            {
                currentFolder = currentFolder + "/" + folders[i];
                result = dws.CreateFolder(currentFolder);
                if (result != SUCCEED_RESULT && result != ALREADY_EXISTS)
                {
                    throw new Exception(result);
                }
            }
            
            return GetFolder(siteName, folder);
        }

        public void CreateFullPathFolder(string siteName, string documentLibrary, string folder)
        {
            string[] targetFolders = folder.Trim('/').Split('/');
            string targetFolder = "";
            foreach (string eachFolder in targetFolders)
            {
                targetFolder = targetFolder + '/' + eachFolder;                
            }            
        }

        public void DeleteFolder(string siteName, string folder)
        {
            if (!siteName.EndsWith("/"))
            {
                siteName += "/";
            }
            dws.Url = string.Format(siteDWSServiceUrl, siteName);
            string result = dws.DeleteFolder(folder);
            if (result != SUCCEED_RESULT)
            {
                throw new Exception(result);
            }
        }

        public string GetFolder(string site, string folder)
        {
            return "http://" + site.ToLower().Replace("http://", "") + "/" + folder.TrimEnd('/');
        }

        public void UploadFile(string targetFolder, string sourceFile, System.Collections.Hashtable security)
        {
            string fileName = sourceFile.Substring(sourceFile.LastIndexOf("\\") + 1);
            string targetFile = targetFolder.TrimEnd('/') + "/" + fileName;
            
            FileStream fs = new FileStream(sourceFile, System.IO.FileMode.Open);
            byte[] binFile = new byte[(int)fs.Length];
            fs.Read(binFile, 0, (int)fs.Length);
            fs.Close();
            
            string str = upload.UploadDocument(fileName, binFile, targetFolder);            
            string result2 = SetDocumentSecurity(targetFolder, security);
            string result1 = SetDocumentSecurity(targetFile, security);

            if (result1 != "Sucessful")
            {
                throw new Exception(result1);
            }
            if (result2 != "Sucessful")
            {
                throw new Exception(result2);
            }
        }

        public string SetDocumentSecurity(string targetFile, System.Collections.Hashtable security)
        {
            string[] users = (string[])(new ArrayList(security.Keys).ToArray(typeof(string)));
            string[] permissions = new string[security.Count];

            for (int i = 0; i < users.Length; i++)
            {
                permissions[i] = security[users[i]].ToString();
            }
            string result = upload.SetDocumentSecurity(targetFile, users, permissions, true);
            return result;
        }
    }
}
