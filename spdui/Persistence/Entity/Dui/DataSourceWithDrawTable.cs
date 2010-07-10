using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class DataSourceWithDrawTable : EntityBase
    {
        #region O/R Mapping Properties
		
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
		
		private string _withDrawTableName;
		public string WithDrawTableName
		{
			get
			{
				return _withDrawTableName;
			}
			set
			{
				_withDrawTableName = value;
			}
		}
		
        
        #endregion

        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

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
            DataSourceWithDrawTable another = obj as DataSourceWithDrawTable;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.TheDataSource == another.TheDataSource) && (this.WithDrawTableName == another.WithDrawTableName);
            }
        } 
    }
	
}
