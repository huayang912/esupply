using System;
using System.Collections;

using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DWDataSourceMergeRule : EntityBase
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
		
		private string _ruleType;
		public string RuleType
		{
			get
			{
				return _ruleType;
			}
			set
			{
				_ruleType = value;
			}
		}
		
		private string _ruleContent;
		public string RuleContent
		{
			get
			{
				return _ruleContent;
			}
			set
			{
				_ruleContent = value;
			}
		}

        private string _resultContent;
        public string ResultContent
        {
            get
            {
                return _resultContent;
            }
            set
            {
                _resultContent = value;
            }
        }

        private string _updateContent;
        public string UpdateContent
        {
            get
            {
                return _updateContent;
            }
            set
            {
                _updateContent = value;
            }
        }

		private int _sequenceNo;
		public int SequenceNo
		{
			get
			{
				return _sequenceNo;
			}
			set
			{
				_sequenceNo = value;
			}
		}

        private DWDataSource _theDWDataSource;
        public DWDataSource TheDWDataSource
        {
            get
            {
                return _theDWDataSource;
            }
            set
            {
                _theDWDataSource = value;
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

        private DWDataSourceMergeRule _dependenceRule;
        public DWDataSourceMergeRule DependenceRule
        {
            get
            {
                return _dependenceRule;
            }
            set
            {
                _dependenceRule = value;
            }
        }
        
        #endregion

        #region Non O/R Mapping Properties

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

        public static string VALIDATION_STATUS_IN_PROGRESS = "In Progress";
        public static string VALIDATION_STATUS_WAITING = "Waiting";
        private string _validationStatus;
        public string ValidationStatus
        {
            get
            {
                return _validationStatus;
            }
            set
            {
                _validationStatus = value;
            }
        }

        private int _faildRowCount;
        public int FaildRowCount
        {
            get
            {
                return _faildRowCount;
            }
            set
            {
                _faildRowCount = value;
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
            DataSourceRule another = obj as DataSourceRule;
			
            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id) && (this.Name == another.Name) && (this.Description == another.Description) && (this.CreateDate == another.CreateDate) && (this.LastUpdateDate == another.LastUpdateDate) && (this.RuleType == another.RuleType) && (this.RuleContent == another.RuleContent) && (this.UpdateContent == another.UpdateContent) && (this.SequenceNo == another.SequenceNo);
            }
        } 
    }
	
}
