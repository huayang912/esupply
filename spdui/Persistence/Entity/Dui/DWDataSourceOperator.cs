using System;
using System.Collections;

using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DWDataSourceOperator : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _allowType;
		public string AllowType
		{
			get
			{
				return _allowType;
			}
			set
			{
				_allowType = value;
			}
		}

        private User _theUser;
        public User TheUser
        {
            get
            {
                return _theUser;
            }
            set
            {
                _theUser = value;
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
        
        #endregion

        #region Non O/R Mapping Properties
        
        public const string OPERATOR_ADMIN_Value = "ADMIN";
        public const string OPERATOR_VIEWER_Value = "VIEWER";

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
            DataSourceOperator another = obj as DataSourceOperator;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.AllowType == another.AllowType);
            }
        } 
    }
	
}
