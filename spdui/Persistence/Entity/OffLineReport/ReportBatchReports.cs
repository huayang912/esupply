using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportBatchReports : EntityBase
    {
        #region O/R Mapping Properties
		
		private ReportBatch _theReportBatch;
		public ReportBatch TheReportBatch
		{
			get
			{
				return _theReportBatch;
			}
			set
			{
				_theReportBatch = value;
			}
		}
		
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
            ReportBatchReports another = obj as ReportBatchReports;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.TheReportBatch == another.TheReportBatch) && (this.TheReport == another.TheReport);
            }
        } 
    }
	
}
