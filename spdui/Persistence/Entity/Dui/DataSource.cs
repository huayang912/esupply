using System;
using System.Collections;

using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DataSource : EntityBase
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

        private string _dSType;
        public string DSType
        {
            get
            {
                return _dSType;
            }
            set
            {
                _dSType = value;
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

        private string _dataStructure;
        public string DataStructure
        {
            get
            {
                return _dataStructure;
            }
            set
            {
                _dataStructure = value;
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

        private int _withDrawTables;
        public int WithDrawTables
        {
            get
            {
                return _withDrawTables;
            }
            set
            {
                _withDrawTables = value;
            }
        }

        private string _dWQuerySQL;
        public string DWQuerySQL
        {
            get
            {
                return _dWQuerySQL;
            }
            set
            {
                _dWQuerySQL = value;
            }
        }

        private string _afterWithdrawSQL;
        public string AfterWithdrawSQL
        {
            get
            {
                return _afterWithdrawSQL;
            }
            set
            {
                _afterWithdrawSQL = value;
            }
        }

        private string _afterRowDeleteSQL;
        public string AfterRowDeleteSQL
        {
            get
            {
                return _afterRowDeleteSQL;
            }
            set
            {
                _afterRowDeleteSQL = value;
            }
        }

        #endregion

        #region Non O/R Mapping Properties

        private IList _dataSourceCategoryList;
        public IList DataSourceCategoryList
        {
            get
            {
                return _dataSourceCategoryList;
            }
            set
            {
                _dataSourceCategoryList = value;
            }
        }

        private IList _dataSourceWithDrawTableList;
        public IList DataSourceWithDrawTableList
        {
            get
            {
                return _dataSourceWithDrawTableList;
            }
            set
            {
                _dataSourceWithDrawTableList = value;
            }
        }

        private IList _dataSourceFieldList;
        public IList DataSourceFieldList
        {
            get
            {
                return _dataSourceFieldList;
            }
            set
            {
                _dataSourceFieldList = value;
            }
        }

        private IList _dataSourceOperatorList;
        public IList DataSourceOperatorList
        {
            get
            {
                return _dataSourceOperatorList;
            }
            set
            {
                _dataSourceOperatorList = value;
            }
        }

        private IList _dataSourceRuleList;
        public IList DataSourceRuleList
        {
            get
            {
                return _dataSourceRuleList;
            }
            set
            {
                _dataSourceRuleList = value;
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
            DataSource another = obj as DataSource;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.Name == another.Name) && (this.Description == another.Description) && (this.CreateDate == another.CreateDate) && (this.LastUpdateDate == another.LastUpdateDate);
            }
        } 
    }
	
}
