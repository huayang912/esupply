using System;
using System.Collections;

//TODO: Add other using statements here
using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportTemplate : EntityBase
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

        private string _templateFilePath;
        public string TemplateFilePath
        {
            get
            {
                return _templateFilePath;
            }
            set
            {
                _templateFilePath = value;
            }
        }

        private string _connectionString;
        public string ConnectionString
        {
            get
            {
                return _connectionString;
            }
            set
            {
                _connectionString = value;
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
		
		private string _reportType;
		public string ReportType
		{
			get
			{
				return _reportType;
			}
			set
			{
				_reportType = value;
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
		// Modified By Vincent 2006-09-04 Begin
        
        //private string _needSendMail;
        //public string NeedSendMail
        //{
        //    get
        //    {
        //        return _needSendMail;
        //    }
        //    set
        //    {
        //        _needSendMail = value;
        //    }
        //}
        // Modified By Vincent 2006-09-04 End
        #endregion

        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 
        private IList _reportSheetList;
        public IList ReportSheetList
        {
            get
            {
                return _reportSheetList;
            }
            set
            {
                _reportSheetList = value;
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
            ReportTemplate another = obj as ReportTemplate;
			
            if (another == null)
            {
                return false;
            }
            else
            {
                // Modified By Vincent 2006-09-04 Begin
                return (this.Id == another.Id) && (this.Name == another.Name) && (this.Description == another.Description) && (this.CreateBy == another.CreateBy) && (this.LastUpdateBy == another.LastUpdateBy) && (this.CreateDate == another.CreateDate) && (this.LastUpdateDate == another.LastUpdateDate) && (this.ReportType == another.ReportType) && (this.ActiveFlag == another.ActiveFlag) ;
                // Modified By Vincent 2006-09-04 End
                // && (this.NeedSendMail == another.NeedSendMail)
            }
        } 
    }
	
}
