using System;
using System.Collections;

//TODO: Add other using statements here
using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportBatch : EntityBase
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
		
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}

        private string _preRunSQL;
        public string PreRunSQL
        {
            get
            {
                return _preRunSQL;
            }
            set
            {
                _preRunSQL = value;
            }
        }

        private string _postRunSQL;
        public string PostRunSQL
        {
            get
            {
                return _postRunSQL;
            }
            set
            {
                _postRunSQL = value;
            }
        }

		private string _batchType;
		public string BatchType
		{
			get
			{
				return _batchType;
			}
			set
			{
				_batchType = value;
			}
		}
		
		private User _createBy;
		public User CreateBy
		{
			get
			{
				return _createBy;
			}
			set
			{
				_createBy = value;
			}
		}
		
		private User _lastUpdateBy;
		public User LastUpdateBy
		{
			get
			{
				return _lastUpdateBy;
			}
			set
			{
				_lastUpdateBy = value;
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
		
		private DateTime _lastUpdateDate;
		public DateTime LastUpdateDate
		{
			get
			{
				return _lastUpdateDate;
			}
			set
			{
				_lastUpdateDate = value;
			}
		}
		
		private int _activeFlag;
		public int ActiveFlag
		{
			get
			{
				return _activeFlag;
			}
			set
			{
				_activeFlag = value;
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

        private string _emailBody;
        public string EmailBody
        {
            get
            {
                return _emailBody;
            }
            set
            {
                _emailBody = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 
        private IList _reportList;
        public IList ReportList
        {
            get
            {
                return _reportList;
            }
            set
            {
                _reportList = value;
            }
        }

        private IList _userList;
        public IList ReportUserList
        {
            get
            {
                return _userList;
            }
            set
            {
                _userList = value;
            }
        }
        
        private ReportJob _lastestReportJob;
        public ReportJob LastestReportJob
        {
            get
            {
                return _lastestReportJob;
            }
            set
            {
                _lastestReportJob = value;
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
            ReportBatch another = obj as ReportBatch;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.Name == another.Name) && (this.Description == another.Description) && (this.BatchType == another.BatchType) && (this.CreateBy == another.CreateBy) && (this.LastUpdateBy == another.LastUpdateBy) && (this.CreateDate == another.CreateDate) && (this.LastUpdateDate == another.LastUpdateDate) && (this.ActiveFlag == another.ActiveFlag);
            }
        } 
    }
	
}
