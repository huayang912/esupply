using System;
using System.Collections;

using Dndp.Persistence.Entity.Security;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DataSourceUpload : EntityBase
    {
        #region O/R Mapping Properties
		
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

        private int _batchNo;
        public int BatchNo
        {
            get
            {
                return _batchNo;
            }
            set
            {
                _batchNo = value;
            }
        }
		
		private string _processStatus;
		public string ProcessStatus
		{
			get
			{
				return _processStatus;
			}
			set
			{
				_processStatus = value;
			}
		}

        private DateTime _processStatusDate;
        public DateTime ProcessStatusDate
        {
            get
            {
                return _processStatusDate;
            }
            set
            {
                _processStatusDate = value;
            }
        }

        private DateTime _uploadDate;
        public DateTime UploadDate
		{
			get
			{
				return _uploadDate;
			}
			set
			{
				_uploadDate = value;
			}
		}
		
		private string _uploadFileOriginName;
		public string UploadFileOriginName
		{
			get
			{
				return _uploadFileOriginName;
			}
			set
			{
				_uploadFileOriginName = value;
			}
		}
		
		private string _uploadFileStoreName;
		public string UploadFileStoreName
		{
			get
			{
				return _uploadFileStoreName;
			}
			set
			{
				_uploadFileStoreName = value;
			}
		}

        private DataSourceCategory _theDataSourceCategory;
        public DataSourceCategory TheDataSourceCategory
        {
            get
            {
                return _theDataSourceCategory;
            }
            set
            {
                _theDataSourceCategory = value;
            }
        }

        private User _uploadBy;
        public User UploadBy
        {
            get
            {
                return _uploadBy;
            }
            set
            {
                _uploadBy = value;
            }
        }

        private long _uploadFileLength;
        public long UploadFileLength
        {
            get
            {
                return _uploadFileLength;
            }
            set
            {
                _uploadFileLength = value;
            }
        }

        private long _recordRows;
        public long RecordRows
        {
            get
            {
                return _recordRows;
            }
            set
            {
                _recordRows = value;
            }
        }

        private int _errors;
        public int Errors
        {
            get
            {
                return _errors;
            }
            set
            {
                _errors = value;
            }
        }

        private int _problems;
        public int Problems
        {
            get
            {
                return _problems;
            }
            set
            {
                _problems = value;
            }
        }

        private int _warnings;
        public int Warnings
        {
            get
            {
                return _warnings;
            }
            set
            {
                _warnings = value;
            }
        }

        private int _isWithdraw;
        public int IsWithdraw
        {
            get
            {
                return _isWithdraw;
            }
            set
            {
                _isWithdraw = value;
            }
        }

        private int _isHitoryDelete;
        public int IsHitoryDelete
        {
            get
            {
                return _isHitoryDelete;
            }
            set
            {
                _isHitoryDelete = value;
            }
        }

        private DateTime? _ownerConfirmDate;
        public DateTime? OwnerConfirmDate
        {
            get
            {
                return _ownerConfirmDate;
            }
            set
            {
                _ownerConfirmDate = value;
            }
        }


        private User _ownerConfirmBy;
        public User OwnerConfirmBy
        {
            get
            {
                return _ownerConfirmBy;
            }
            set
            {
                _ownerConfirmBy = value;
            }
        }

        private DateTime? _etlConfirmDate;
        public DateTime? ETLConfirmDate
        {
            get
            {
                return _etlConfirmDate;
            }
            set
            {
                _etlConfirmDate = value;
            }
        }

        private User _etlConfirmBy;
        public User ETLConfirmBy
        {
            get
            {
                return _etlConfirmBy;
            }
            set
            {
                _etlConfirmBy = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties
        public static string DataSourceUpload_ProcessStatus_OWNER_CONFIRMED = "OWNER_CONFIRMED";
        public static string DataSourceUpload_ProcessStatus_ETL_CONFIRMED = "ETL_CONFIRMED";
        public static string DataSourceUpload_ProcessStatus_ETL_SUCCESS = "ETL_SUCCESS";
        public static string DataSourceUpload_ProcessStatus_ETL_FAILED = "ETL_FAILED";
        public static string DataSourceUpload_ProcessStatus_ETL_LOCKED = "ETL_LOCKED";

        private IList<ValidationResult> _validationResultList;
        public IList<ValidationResult> ValidationResultList
        {
            get
            {
                return _validationResultList;
            }
            set
            {
                _validationResultList = value;
            }
        }

        public IList<ValidationResult> ErrorValidationResultList
        {
            get
            {
                if (ValidationResultList != null) {
                    IList<ValidationResult> list = new List<ValidationResult>(); 
                    foreach (ValidationResult vr in ValidationResultList) 
                    {
                        if (vr.TheDataSourceRule.RuleType == "ERROR")
                        {
                            list.Add(vr);
                        }
                    }

                    return list;
                } 
                else
                {
                    return null;
                }
            }
        }

        public IList<ValidationResult> ProblemValidationResultList
        {
            get
            {
                if (ValidationResultList != null)
                {
                    IList<ValidationResult> list = new List<ValidationResult>();
                    foreach (ValidationResult vr in ValidationResultList)
                    {
                        if (vr.TheDataSourceRule.RuleType == "PROBLEM")
                        {
                            list.Add(vr);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        public IList<ValidationResult> WarningValidationResultList
        {
            get
            {
                if (ValidationResultList != null)
                {
                    IList<ValidationResult> list = new List<ValidationResult>();
                    foreach (ValidationResult vr in ValidationResultList)
                    {
                        if (vr.TheDataSourceRule.RuleType == "WARNING")
                        {
                            list.Add(vr);
                        }
                    }

                    return list;
                }
                else
                {
                    return null;
                }
            }
        }

        private bool _isInValidation;
        public bool IsInValidation
        {
            get
            {
                return _isInValidation;
            }
            set
            {
                _isInValidation = value;
            }
        }


        #endregion

        public override int GetHashCode()
        {
            if (Id > 0)
            {
                return Id;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            DataSourceUpload another = obj as DataSourceUpload;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.Name == another.Name) && (this.ProcessStatus == another.ProcessStatus) && (this.UploadDate == another.UploadDate) && (this.UploadFileOriginName == another.UploadFileOriginName) && (this.UploadFileStoreName == another.UploadFileStoreName);
            }
        } 
    }
	
}
