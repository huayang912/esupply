using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DataSourceCategory : EntityBase
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

        private DataSource _theDataSource;
        public DataSource TheDataSource
        {
            get
            {
                return _theDataSource;
            }
            set
            {
                _theDataSource = value;
            }
        }

        private bool _activeFlag;
        public bool ActiveFlag
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

        private IList _users;
        public IList Users
        {
            get
            {
                return _users;
            }
            set
            {
                _users = value;
            }
        }
        #endregion

        #region Non O/R Mapping Properties

        private DataSourceUpload _lastestDataSourceUpload;
        public DataSourceUpload LastestDataSourceUpload
        {
            get
            {
                return _lastestDataSourceUpload;
            }
            set
            {
                _lastestDataSourceUpload = value;
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
            DataSourceCategory another = obj as DataSourceCategory;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.Name == another.Name) && (this.Description == another.Description);
            }
        } 
    }
	
}
