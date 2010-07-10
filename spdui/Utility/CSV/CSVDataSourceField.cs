using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Utility.CSV
{
    public class CSVDataSourceField
    {
        private string _name;
        public string Name
        {
            get
            {
                return _name;
            }
            set
            {
                _name = value;
            }
        }        

        private string _fieldType;
        public string FieldType
        {
            get
            {
                return _fieldType;
            }
            set
            {
                _fieldType = value;
            }
        }
   
        private string _fieldLength;
        public string FieldLength
        {
            get
            {
                return _fieldLength;
            }
            set
            {
                _fieldLength = value;
            }
        }

        private bool _isDataKey;
        public bool IsDataKey
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

        private bool _isNullable;
        public bool IsNullable
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
    }
}
