using System;
using System.Collections;

//TODO: Add other using statements here
using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportSheet : EntityBase
    {
        #region O/R Mapping Properties
		
		private ReportTemplate _theReport;
		public ReportTemplate TheReport
		{
			get
			{
				return _theReport;
			}
			set
			{
				_theReport = value;
			}
		}
		
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

        // Modified By Vincent On 15:37 New Field 0002
        private string _sheetType;
        public string SheetType
        {
            get
            {
                return _sheetType;
            }
            set
            {
                _sheetType = value;
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
            ReportSheet another = obj as ReportSheet;
			
            if (another == null)
            {
                return false;
            }
            else
            {
                // Modified By Vincent On 15:37 New Field 0003
            	return (this.Id == another.Id) && (this.TheReport == another.TheReport) && (this.Name == another.Name) && (this.Description == another.Description) && (this.RuleContent == another.RuleContent) && (this.SequenceNo == another.SequenceNo) && (this.CreateBy == another.CreateBy) && (this.LastUpdateBy == another.LastUpdateBy) && (this.CreateDate == another.CreateDate) && (this.LastUpdateDate == another.LastUpdateDate) && (this.SheetType == another.SheetType);
            }
        } 
    }
	
}
