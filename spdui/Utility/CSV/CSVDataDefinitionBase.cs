using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Utility.CSV
{
    public abstract class CSVDataDefinitionBase
    {
        protected Hashtable dataKey;

        private bool _isNullable;
        private bool _isDataKey;
        private string _columnNm;

        protected bool IsNullable
        {
            get
            {
                return _isNullable;
            }
            set
            {
                _isNullable = value;
            }
        }

        protected bool IsDataKey
        {
            get
            {
                return _isDataKey;
            }
            set
            {
                _isDataKey = value;
            }
        }

        public string ColumnNm
        {
            get
            {
                return _columnNm;
            }
            set
            {
                _columnNm = value;
            }
        }

        public abstract string ValidateAndParse(string s, int rowNo);
    }
}
