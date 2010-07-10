using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeDistributionJob : EntityBase
    {
        public const string DISTRIBUTION_STATUS_Pending = "Pending";
        public const string DISTRIBUTION_STATUS_Submit = "Submit";
        public const string DISTRIBUTION_STATUS_Running = "Running";
        public const string DISTRIBUTION_STATUS_Cancelled = "Cancelled";
        public const string DISTRIBUTION_STATUS_Failed = "Failed";
        public const string DISTRIBUTION_STATUS_Success = "Success";

        #region O/R Mapping Properties

        private CubeDefinition _theCube;
        public CubeDefinition TheCube
        {
            get
            {
                return _theCube;
            }
            set
            {
                _theCube = value;
            }
        }

		private string _jobDescription;
		public string JobDescription
		{
			get
			{
				return _jobDescription;
			}
			set
			{
				_jobDescription = value;
			}
		}
		
		private DateTime _createDate;
		public DateTime CreateDate
		{
			get
			{
				return _createDate;
			}
			set
			{
				_createDate = value;
			}
		}
		
		private string _creator;
		public string Creator
		{
			get
			{
				return _creator;
			}
			set
			{
				_creator = value;
			}
		}
		
		private DateTime _updateDate;
		public DateTime UpdateDate
		{
			get
			{
				return _updateDate;
			}
			set
			{
				_updateDate = value;
			}
		}

        private DateTime _jobStartDate;
        public DateTime JobStartDate
        {
            get
            {
                return _jobStartDate;
            }
            set
            {
                _jobStartDate = value;
            }
        }

        private DateTime _jobEndDate;
        public DateTime JobEndDate
        {
            get
            {
                return _jobEndDate;
            }
            set
            {
                _jobEndDate = value;
            }
        }
		
		private DateTime _beginDate;
		public DateTime BeginDate
		{
			get
			{
				return _beginDate;
			}
			set
			{
				_beginDate = value;
			}
		}
		
		private DateTime _endDate;
		public DateTime EndDate
		{
			get
			{
				return _endDate;
			}
			set
			{
				_endDate = value;
			}
		}
		
		private string _status;
		public string Status
		{
			get
			{
				return _status;
			}
			set
			{
				_status = value;
			}
		}
		
		private string _statusDescription;
		public string StatusDescription
		{
			get
			{
				return _statusDescription;
			}
			set
			{
				_statusDescription = value;
			}
		}

        private string _needSendMail;
        public string NeedSendMail
        {
            get
            {
                return _needSendMail;
            }
            set
            {
                _needSendMail = value;
            }
        }

        private string _needPublishToPortal;
        public string NeedPublishToPortal
        {
            get
            {
                return _needPublishToPortal;
            }
            set
            {
                _needPublishToPortal = value;
            }
        }

        private string _publishFolder;
        public string PublishFolder
        {
            get
            {
                return _publishFolder;
            }
            set
            {
                _publishFolder = value;
            }
        }

        private string _needCreateSubFolder;
        public string NeedCreateSubFolder
        {
            get
            {
                return _needCreateSubFolder;
            }
            set
            {
                _needCreateSubFolder = value;
            }
        }

        private string _appendDateToFileName;
        public string AppendDateToFileName
        {
            get
            {
                return _appendDateToFileName;
            }
            set
            {
                _appendDateToFileName = value;
            }
        }
        // Modified by vincent at 2007-11-21 begin
        private string _appendUserNameToFileName;
        public string AppendUserNameToFileName
        {
            get
            {
                return _appendUserNameToFileName;
            }
            set
            {
                _appendUserNameToFileName = value;
            }
        }
        // Modified by vincent at 2007-11-21 end

        private string _eMailBody;
        public string EMailBody
        {
            get
            {
                return _eMailBody;
            }
            set
            {
                _eMailBody = value;
            }
        }

        private string _eMailSubject;
        public string EMailSubject
        {
            get
            {
                return _eMailSubject;
            }
            set
            {
                _eMailSubject = value;
            }
        }

        private string _measureList;
        public string MeasureList
        {
            get
            {
                return _measureList;
            }
            set
            {
                _measureList = value;
            }
        }

        private string _dimensionList;
        public string DimensionList
        {
            get
            {
                return _dimensionList;
            }
            set
            {
                _dimensionList = value;
            }
        }

        private string _cubeRoleList;
        public string CubeRoleList
        {
            get
            {
                return _cubeRoleList;
            }
            set
            {
                _cubeRoleList = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties

        private IList<CubeDistributionJobItem> _cubeDistributionJobItemList;
        public IList<CubeDistributionJobItem> CubeDistributionJobItemList
        {
            get
            {
                return _cubeDistributionJobItemList;
            }
            set
            {
                _cubeDistributionJobItemList = value;
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
            CubeDistributionJob another = obj as CubeDistributionJob;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id);
            }
        } 
    }
	
}
