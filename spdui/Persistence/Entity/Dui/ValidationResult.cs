using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Dui
{
    [Serializable]
    public class ValidationResult : EntityBase
    {
        #region O/R Mapping Properties
		
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

        private DataSourceUpload _theDataSourceUpload;
        public DataSourceUpload TheDataSourceUpload
        {
            get
            {
                return _theDataSourceUpload;
            }
            set
            {
                _theDataSourceUpload = value;
            }
        }

        private DataSourceRule _theDataSourceRule;
        public DataSourceRule TheDataSourceRule
        {
            get
            {
                return _theDataSourceRule;
            }
            set
            {
                _theDataSourceRule = value;
            }
        }
        
        #endregion

        #region Non O/R Mapping Properties
        //this field is for the validation purpose
        //if this value is null, means it do not be select in validation list
        //if this value is "In Process", means now server is validating current rule
        //if this value is "Waiting", means now server is in the queue of validation list
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
            ValidationResult another = obj as ValidationResult;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.Status == another.Status) && (this.FaildRowCount == another.FaildRowCount);
            }
        } 
    }
	
}
