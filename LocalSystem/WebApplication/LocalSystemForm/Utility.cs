using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.Windows.Forms;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace LocalSystemForm
{
    public class Utility
    {
        public static string GetBarcodeCode128BByStr(string str)
        {
            int total = 104;
            int a = 0;
            int endAsc = 0;
            char endChar = new char();
            for (int i = 0; i < str.Length; i++)
            {
                //转换ASCII数值
                a = Convert.ToInt32(Convert.ToChar(str.Substring(i, 1)));

                //Code 128 SET B 字符集
                if (a >= 32)
                {
                    total += (a - 32) * (i + 1);
                }
                else
                {
                    total += (a + 64) * (i + 1);
                }
            }
            endAsc = total % 103;
            //字符集大于95直接赋值，其它转换后获得
            if (endAsc >= 95)
            {
                switch (endAsc)
                {
                    case 95:
                        endChar = Convert.ToChar("Ã");
                        break;
                    case 96:
                        endChar = Convert.ToChar("Ä");
                        break;
                    case 97:
                        endChar = Convert.ToChar("Å");
                        break;
                    case 98:
                        endChar = Convert.ToChar("Æ");
                        break;
                    case 99:
                        endChar = Convert.ToChar("Ç");
                        break;
                    case 100:
                        endChar = Convert.ToChar("È");
                        break;
                    case 101:
                        endChar = Convert.ToChar("É");
                        break;
                    case 102:
                        endChar = Convert.ToChar("Ê");
                        break;
                    default:
                        endChar = Convert.ToChar("");
                        break;
                }
            }
            else
            {
                endAsc += 32;
                endChar = Convert.ToChar(endAsc);
            }
            //生成Code 128B条码字符串
            string result = "Ì" + str + endChar.ToString() + "Î";
            return result;
        }

        public static string Md5(string originalPassword)
        {
            MD5CryptoServiceProvider x = new MD5CryptoServiceProvider();
            byte[] bs = System.Text.Encoding.UTF8.GetBytes(originalPassword);
            bs = x.ComputeHash(bs);
            System.Text.StringBuilder s = new System.Text.StringBuilder();
            foreach (byte b in bs)
            {
                s.Append(b.ToString("x2").ToUpper());
            }
            return s.ToString();
        }

        public static bool IsDecimal(string str)
        {
            try
            {
                if (str.Contains("+"))
                {
                    string[] chars = str.Split('+');
                    for (int i = 0; i < chars.Length; i++)
                    {
                        if (i == chars.Length - 1 && chars[i].ToString() == string.Empty)
                            return true;
                        Convert.ToDecimal(chars[i]);
                    }
                }
                else
                {
                    Convert.ToDecimal(str);

                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static string FormatExMessage(string message)
        {
            try
            {
                if (message.StartsWith("System.Web.Services.Protocols.SoapException"))
                {
                    message = message.Remove(0, 44);
                    message = message.Remove(message.IndexOf("\n"), message.Length - message.IndexOf("\n"));
                }
                message = message.Replace("\\n", "\n\n");
            }
            catch (Exception ex)
            {
                return message;
            }
            return message;
        }


        public static DataGridView RenderDataGridViewBackColor(DataGridView dataGrid)
        {
            foreach (DataGridViewRow row in dataGrid.Rows)
            {
                decimal CurrentQty = Convert.ToDecimal(row.Cells["CurrentQty"].Value.ToString());
                decimal CurrentRejectQty = 0;
                try { CurrentRejectQty = Convert.ToDecimal(row.Cells["CurrentRejectQty"].Value.ToString()); }
                catch (Exception) { }
                decimal Qty = Convert.ToDecimal(row.Cells["Qty"].Value.ToString());
                if (CurrentQty + CurrentRejectQty == Qty)
                {
                    row.DefaultCellStyle.ForeColor = Color.Green;
                }
                else if (CurrentQty + CurrentRejectQty > Qty)
                {
                    row.DefaultCellStyle.ForeColor = Color.OrangeRed;
                }
                else if (CurrentQty + CurrentRejectQty < Qty)
                {
                    //row.DefaultCellStyle.BackColor = Color.Yellow;
                }
            }
            return dataGrid;
        }


        public static void DataGridViewDecimalFilter(object sender, KeyPressEventArgs e)
        {
            string str;
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
                return;
            }
            else
            {
                str = ((DataGridViewTextBoxEditingControl)sender).Text + e.KeyChar.ToString();
            }

            if (Utility.IsDecimal(str) || str == "-")
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void DataGridViewIntFilter(object sender, KeyPressEventArgs e)
        {
            string str;
            if (e.KeyChar.ToString() == "\b" || e.KeyChar.ToString() == ".")
            {
                e.Handled = false;
                return;
            }
            else
            {
                str = ((DataGridViewTextBoxEditingControl)sender).Text + e.KeyChar.ToString();
            }

            if (Utility.IsDecimal(str) || str == "-")
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void TextBoxDecimalFilter(object sender, KeyPressEventArgs e)
        {
            string str;
            if (e.KeyChar.ToString() == "\b")
            {
                e.Handled = false;
                return;
            }
            else
            {
                str = ((TextBox)sender).Text + e.KeyChar.ToString();
            }

            if (Utility.IsDecimal(str) || str == "-")
            {
                e.Handled = false;
            }
            else
            {
                e.Handled = true;
            }
        }

        public static void Log(string logstr)
        {
            FileStream fs = new FileStream("C:\\SconitTemp\\Sconit_CSLog.txt", FileMode.OpenOrCreate, FileAccess.Write);
            StreamWriter m_streamWriter = new StreamWriter(fs);
            m_streamWriter.BaseStream.Seek(0, SeekOrigin.End);
            m_streamWriter.WriteLine(logstr + " " + DateTime.Now.ToString() + "\n");
            m_streamWriter.Flush();
            m_streamWriter.Close();
            fs.Close();
        }





        #region 私有方法
      
        private static void CopyProperty(object sourceObj, object targetObj)
        {
            PropertyInfo[] sourcePropertyInfoAry = sourceObj.GetType().GetProperties();
            PropertyInfo[] targetPropertyInfoAry = targetObj.GetType().GetProperties();

            foreach (PropertyInfo sourcePropertyInfo in sourcePropertyInfoAry)
            {
                foreach (PropertyInfo targetPropertyInfo in targetPropertyInfoAry)
                {
                    if (sourcePropertyInfo.Name == targetPropertyInfo.Name)
                    {
                        if (targetPropertyInfo.CanWrite && sourcePropertyInfo.CanRead)
                        {
                            targetPropertyInfo.SetValue(targetObj, sourcePropertyInfo.GetValue(sourceObj, null), null);
                        }
                    }
                }
            }
        }
        #endregion
    }

    public static class BusinessConstants
    {
        public static readonly string TEMP_FILE_PATH = "Reports/Templates/TempFiles/";
        public static readonly string CODE_MASTER_LANGUAGE = "Language";
        public static readonly string CODE_MASTER_LANGUAGE_VALUE_ZH_CN = "zh-CN";
        public static readonly string CODE_MASTER_LANGUAGE_VALUE_EN = "en";
        public static readonly string CODE_MASTER_PLANTADDRESS = "PlantAddress";
        public static readonly string CODE_MASTER_PLANTCODE = "PlantCode";
        public static readonly string CODE_MASTER_PLANTCONTACT = "PlantContact";
        public static readonly string CODE_MASTER_PLANTFAX = "PlantFax";
        public static readonly string CODE_MASTER_PLANTNAME = "PlantName";
        public static readonly string CODE_MASTER_PLANTPHONE = "PlantPhone";

        public static readonly string SEARCH_MODE_CRITERIA = "Criteria";
        public static readonly string SEARCH_MODE_CUSTOMIZE = "Customize";

        public static readonly string PO_STATUS = "Status";
        public static readonly string PO_STATUS_VALUE_CREATE = "Create";
        public static readonly string PO_STATUS_VALUE_SUBMIT = "Submit";
        public static readonly string PO_STATUS_VALUE_CANCEL = "Cancel";
        public static readonly string PO_STATUS_VALUE_CLOSE = "Close";

        public static readonly string BARCODE_STATUS = "Status";
        public static readonly string BARCODE_STATUS_VALUE_CREATE = "Create";
        public static readonly string BARCODE_STATUS_VALUE_ERROR = "Error";
        public static readonly string BARCODE_STATUS_VALUE_WARNING = "Warning";
        public static readonly string BARCODE_STATUS_VALUE_CLOSE = "Close";
    }
}
