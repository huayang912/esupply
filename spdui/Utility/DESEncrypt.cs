using System;
using System.Text;
using System.Security;
using System.Security.Cryptography;
using System.IO;
namespace SPEncryptUtility
{
    /// <summary>
    /// 此处定义的是DES加密,为了便于今后的管理和维护
    /// 请不要随便改动密码,或者改变了密码后请一定要
    /// 牢记先前的密码,否则将会照成不可预料的损失
    /// </summary>
    public class DESEncrypt
    {
        private static string m_strEncryptKey = "abcdefgh";
        private static byte[] IV =  new byte[] { 0x12, 0x34, 0x56, 0x78, 0x90, 0xAB, 0xCD, 0xEF };
        public static string Encrypt(string p_strInput)
        {
            byte[] byKey = null;            
            byKey = System.Text.Encoding.UTF8.GetBytes(m_strEncryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            byte[] inputByteArray = Encoding.UTF8.GetBytes(p_strInput);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateEncryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            return Convert.ToBase64String(ms.ToArray());
        }

        public static string Decrypt(string p_strInput)
        {
            byte[] byKey = null;
            byte[] inputByteArray = new Byte[p_strInput.Length];
            byKey = System.Text.Encoding.UTF8.GetBytes(m_strEncryptKey.Substring(0, 8));
            DESCryptoServiceProvider des = new DESCryptoServiceProvider();
            inputByteArray = Convert.FromBase64String(p_strInput);
            MemoryStream ms = new MemoryStream();
            CryptoStream cs = new CryptoStream(ms, des.CreateDecryptor(byKey, IV), CryptoStreamMode.Write);
            cs.Write(inputByteArray, 0, inputByteArray.Length);
            cs.FlushFinalBlock();
            System.Text.Encoding encoding = new System.Text.UTF8Encoding();
            return encoding.GetString(ms.ToArray());
        }
    }
}


