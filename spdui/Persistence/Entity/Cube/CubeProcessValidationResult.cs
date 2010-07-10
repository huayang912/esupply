using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeProcessValidationResult : EntityBase
    {
        #region O/R Mapping Properties

        private CubeProcess _theProcess;
        public CubeProcess TheProcess
        {
            get
            {
                return _theProcess;
            }
            set
            {
                _theProcess = value;
            }
        }

        private CubeValidationRule _theRule;
        public CubeValidationRule TheRule
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

        public const string CubeProcessValidationResult_Status_Pending = "";
        public const string CubeProcessValidationResult_Status_Failed = "Failed";
        public const string CubeProcessValidationResult_Status_Passed = "Passed";

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
            CubeProcessValidationResult another = obj as CubeProcessValidationResult;
			
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
