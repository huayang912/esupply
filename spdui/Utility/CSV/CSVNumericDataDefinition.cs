using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Utility.CSV
{
    public class CSVNumericDataDefinition : CSVDataDefinitionBase
    {
        private string _length;
        private int _totalLength;
        private int _fractionLength;

        private string Length
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

        private int TotalLength
        {
            get
            {
                return _totalLength;
            }
            set
            {
                _totalLength = value;
            }
        }

        private int FractionLength
        {
            get
            {
                return _fractionLength;
            }
            set
            {
                _fractionLength = value;
            }
        }

        public CSVNumericDataDefinition(bool isNullAble, bool isDataKey, string length, string columnNm)
        {
            this.IsNullable = isNullAble;
            this.IsDataKey = isDataKey;
            this.ColumnNm = columnNm;
            this.Length = length;
            string[] s = length.Split(',');
            TotalLength = Convert.ToInt16(s[0].Trim());
            if (s.Length > 1)
            {
                FractionLength = Convert.ToInt16(s[1].Trim());
            } 
            else 
            {
                FractionLength = 0;
            }
        }

        public CSVNumericDataDefinition(bool isNullAble, bool isDataKey, string columnNm)
        {
            this.IsNullable = isNullAble;
            this.IsDataKey = isDataKey;
            this.ColumnNm = columnNm;
            this.Length = "9,2";
            TotalLength = 9;
            FractionLength = 2;
        }


        public override string ValidateAndParse(string s, int rowNo)
        {
            if (!IsNullable && (s == null || s.Trim().Length == 0))
            {
                throw new ArgumentException("The value of row(" + rowNo + "), column(" + ColumnNm + ") can not be null");                  
            } 

            try
            {
                if (s == null || s.Trim().Length == 0)
                {
                    s = "NULL";
                }
                else
                {
                    //Format Decimal Data
                    s = s.Replace(",", "");

                    decimal d = Convert.ToDecimal(s);

                    //check data length
                    int totalLen = 0;
                    int fractLen = 0;
                    string[] ss = s.Split('.');
                    if (ss.Length > 1)
                    {
                        totalLen = ss[0].Trim().Length + ss[1].Trim().Length;
                        fractLen = ss[1].Trim().Length;
                    }
                    else
                    {
                        totalLen = ss[0].Trim().Length;
                    }

                    if (totalLen > TotalLength)
                    {
                        throw new ArgumentException("The total length of value(" + s + ") of row(" + rowNo + "), column(" + ColumnNm + ") is over the field specified total length(" + TotalLength + ")");
                    }
                    else if (fractLen > FractionLength)
                    {
                        throw new ArgumentException("The fraction length of value(" + s + ") of row(" + rowNo + "), column(" + ColumnNm + ") is over the field specified fraction length(" + FractionLength + ")");
                    }
                }
            }
            catch (FormatException)
            {
                throw new ArgumentException("The value(" + s + ") of row(" + rowNo + "), column(" + ColumnNm + ") is not in valid numeric format");
            }                       

            return s;
        }
    }
}
