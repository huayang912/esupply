using System;
using System.Collections;
using System.Text;
using System.Collections.Generic;

using NHibernate;
using Castle.Services.Transaction;

using Utility;
using Dndp.Persistence.Entity.Cube;
using Dndp.Persistence.Dao.Cube;
using Dndp.Utility;
using SPEncryptUtility;
//TODO: Add other using statements here.

namespace Dndp.Service.Property.Impl
{
    [Transactional]
    public class PropertyMgr : IPropertyMgr
    {
        private string DEFAULT_ProcessCubeBackupFolder = "C:\\";
        private string DEFAULT_CSV_ENCODING = "GB2312";
        private string DEFAULT_DW_DBString = "SPDW.dbo.";
        private int DEFAULT_CSV_RECORD_PER_PARSE = 20;
        private int DEFAULT_CommitRecordCount = 20;

        private int _commitRecordCount;
        private string _csvEncoding;
        private string _DWDBString;
        private int _csvRecordPerParse;
        private string _processCubeBackupFolder;
        private string _productCubeUserName;
        private string _productCubePassword;

        public PropertyMgr(int commitRecordCount,
                           string csvEncoding,
                           string DWDBString,
                           int csvRecordPerParse,
                           string processCubeBackupFolder,
                           string productCubeUserName,
                           bool encryptProductCubeUserName,
                           string productCubePassword,
                           bool encryptProductCubePassword)
        {
            _commitRecordCount = commitRecordCount;
            _csvEncoding = csvEncoding;
            _DWDBString = DWDBString;
            _csvRecordPerParse = csvRecordPerParse;
            _processCubeBackupFolder = processCubeBackupFolder;
            if (encryptProductCubeUserName)
            {
                _productCubeUserName = DESEncrypt.Decrypt(productCubeUserName);
            }
            else
            {
                _productCubeUserName = productCubeUserName;
            }

            if (encryptProductCubePassword)
            {
                _productCubePassword = DESEncrypt.Decrypt(productCubePassword);
            }
            else
            {
                _productCubePassword = productCubePassword;
            }
        }

        public int CommitRecordCount
        {
            get
            {
                if (_commitRecordCount == 0)
                {
                    return DEFAULT_CommitRecordCount;
                }
                else
                {
                    return _commitRecordCount;
                }
            }
        }

        public string CSVEncoding
        {
            get
            {
                if (_csvEncoding == null)
                {
                    return DEFAULT_CSV_ENCODING;
                }
                else
                {
                    return _csvEncoding;
                }
            }
        }

        public string DWDBString
        {
            get
            {
                if (_DWDBString == null)
                {
                    return DEFAULT_DW_DBString;
                }
                else
                {
                    return _DWDBString;
                }
            }
        }

        public int CSVRecordPerParse
        {
            get
            {
                if (_csvRecordPerParse == 0)
                {
                    return DEFAULT_CSV_RECORD_PER_PARSE;
                }
                else
                {
                    return _csvRecordPerParse;
                }
            }
        }

        public string ProcessCubeBackupFolder
        {
            get
            {
                if (_processCubeBackupFolder == null)
                {
                    return DEFAULT_ProcessCubeBackupFolder;
                }
                else
                {
                    return _processCubeBackupFolder;
                }
            }
        }

        public string ProductCubeUserName
        {
            get { return _productCubeUserName; }
        }

        public string ProductCubePassword
        {
            get { return _productCubePassword; }
        } 
    }
}
