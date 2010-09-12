using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportJobValidationResult : EntityBase
    {
        #region O/R Mapping Properties

        private ReportJob _theJob;
        public ReportJob TheJob
        {
            get
            {
                return _theJob;
            }
            set
            {
                _theJob = value;
            }
        }

        private ReportValidationRule _theRule;
        public ReportValidationRule TheRule
        {
            get
            {
                return _theRule;
            }
            set
            {
                _theRule = value;
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
		
		private int _failedRowCount;
		public int FailedRowCount
		{
			get
			{
				return _failedRowCount;
			}
			set
			{
				_failedRowCount = value;
			}
		}
		
		private string _rowNoList;
		public string RowNoList
		{
			get
			{
				return _rowNoList;
			}
			set
			{
				_rowNoList = value;
			}
		}

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

        #region Non O/R Mapping Properties

        public const string ReportJobValidationResult_Status_Pending = "";
        public const string ReportJobValidationResult_Status_Failed = "Failed";
        public const string ReportJobValidationResult_Status_Passed = "Passed";

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
            ReportJobValidationResult another = obj as ReportJobValidationResult;
			
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
