using System;
using System.Collections;

using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DWDataSource : EntityBase
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

        private string _querySQL;
        public string QuerySQL
        {
            get
            {
                return _querySQL;
            }
            set
            {
                _querySQL = value;
            }
        }

        private string _deleteQuerySQL;
        public string DeleteQuerySQL
        {
            get
            {
                return _deleteQuerySQL;
            }
            set
            {
                _deleteQuerySQL = value;
            }
        }

        private string _deleteSQL;
        public string DeleteSQL
        {
            get
            {
                return _deleteSQL;
            }
            set
            {
                _deleteSQL = value;
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

        private String _queryStartDate;
        public String QueryStartDate
        {
            get
            {
                return _queryStartDate;
            }
            set
            {
                _queryStartDate = value;
            }
        }

        private String _queryEndDate;
        public String QueryEndDate
        {
            get
            {
                return _queryEndDate;
            }
            set
            {
                _queryEndDate = value;
            }
        }

        private string _mergeQuerySQL;
        public string MergeQuerySQL
        {
            get
            {
                return _mergeQuerySQL;
            }
            set
            {
                _mergeQuerySQL = value;
            }
        }

        private string _mergeSQL;
        public string MergeSQL
        {
            get
            {
                return _mergeSQL;
            }
            set
            {
                _mergeSQL = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties

        private IList _dWDataSourceOperatorList;
        public IList DWDataSourceOperatorList
        {
            get
            {
                return _dWDataSourceOperatorList;
            }
            set
            {
                _dWDataSourceOperatorList = value;
            }
        }

        private IList _dWDataSourceMergeRuleList;
        public IList DWDataSourceMergeRuleList
        {
            get
            {
                return _dWDataSourceMergeRuleList;
            }
            set
            {
                _dWDataSourceMergeRuleList = value;
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
