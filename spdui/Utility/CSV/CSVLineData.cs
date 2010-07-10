using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Utility.CSV
{
    public class CSVLineData
    {
        private int batchNo;
        private int rowNo;
        private int dataSourceCategoryid;
        private string category;
        private IList<string> errorMessages;
        private StringBuilder insertSqlValue = new StringBuilder();
        #region Constructor
        public CSVLineData(int batchNo, int rowNo, int dataSourceCategoryid, string category)
        {
            this.batchNo = batchNo;
            this.rowNo = rowNo;
            this.dataSourceCategoryid = dataSourceCategoryid;
            this.category = category;
        }
        #endregion Constructor       

        public bool HasError()
        {
            if (errorMessages != null && errorMessages.Count > 0)
            {
                return true;
            } 
            else 
            {
                return false;
            }
        }

        public IList<string> GetErrorMessages()
        {
            return errorMessages;
        }

        public string GetInsertSqlValue()
        {
            return insertSqlValue.ToString();
        }

        public void AddData(string[] s, CSVDataDefinitionBase[] dataDefinitionArray)
        {
            insertSqlValue.Append("(" + batchNo + ", " + rowNo + ", " + dataSourceCategoryid + ", '" + category + "'");
            for (int i = 0; i < s.Length && i < dataDefinitionArray.Length; i++)
            {
                try
                {
                    if (dataDefinitionArray[i] != null)
                    {
                        string data = dataDefinitionArray[i].ValidateAndParse(s[i].ToString().Trim(), i + 1);
                        insertSqlValue.Append(", " + data);
                    }
                }
                catch (ArgumentException e)
                {
                    if (errorMessages == null)
                    {
                        errorMessages = new List<string>();
                    }                       
                    errorMessages.Add(e.Message);
                }
            }
            insertSqlValue.Append(")");
        }
    }
}
