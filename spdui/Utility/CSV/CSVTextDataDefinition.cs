using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Utility.CSV
{
    public class CSVTextDataDefinition : CSVDataDefinitionBase
    {
        private int _length;

        private int Length
        {
            get
            {
                return _length;
            }
            set
            {
                _length = value;
            }
        }

        public CSVTextDataDefinition(bool isNullAble, bool isDataKey, int length, string columnNm)
        {
            this.IsNullable = isNullAble;
            this.IsDataKey = isDataKey;
            this.ColumnNm = columnNm;
            this.Length = length;
        }

        public CSVTextDataDefinition(bool isNullAble, bool isDataKey, string columnNm)
        {
            this.IsNullable = isNullAble;
            this.IsDataKey = isDataKey;
            this.ColumnNm = columnNm;
            this.Length = 255;
        }

        public override string ValidateAndParse(string s, int rowNo)
        {
            if (!IsNullable && (s == null || s.Trim().Length == 0))
            {
                throw new ArgumentException("The value of row(" + rowNo + "), column(" + ColumnNm + ") can not be null");                
            }
            if (IsDataKey)
            {
                s = s.ToUpper();
            }
            if (s != null && s.Length > this.Length)
            {
                throw new ArgumentException("The length of value(" + s + ") of row(" + rowNo + "), column(" + ColumnNm + ") is " + s.Length + ", which is over the field specified length(" + this.Length + ")");
            }
            return "'" + FormatTextData(s) + "'";
        }

        private string FormatTextData(string SQLStatement)
        {
            SQLStatement = SQLStatement.Replace("'", "''");
            return SQLStatement;
        }
    }
}
