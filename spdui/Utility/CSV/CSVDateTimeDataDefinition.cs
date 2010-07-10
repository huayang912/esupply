using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Utility.CSV
{
    public class CSVDateTimeDataDefinition : CSVDataDefinitionBase
    {
        public CSVDateTimeDataDefinition(bool isNullAble, bool isDataKey, string columnNm)
        {
            this.IsNullable = isNullAble;
            this.IsDataKey = isDataKey;
            this.ColumnNm = columnNm;
        }

        public override string ValidateAndParse(string s, int rowNo)
        {
            if (!IsNullable && (s == null || s.Trim().Length == 0))
            {
                throw new ArgumentException("The value of row(" + rowNo + "), column(" + ColumnNm + ") can not be null");                
            }
            //
            try
            {
                if (s == null || s.Trim().Length == 0)
                {
                    s = "NULL";
                }
                else
                {
                    DateTime dt = Convert.ToDateTime(s);

                    return "'" + dt.ToString("yyyy-MM-dd HH:mm:ss") + "'";
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("The value(" + s + ") of row(" + rowNo + "), column(" + ColumnNm + ") is not in valid datetime format");
            }
            return s;
        }
    }
}
