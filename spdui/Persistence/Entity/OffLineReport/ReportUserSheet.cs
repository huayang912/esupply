using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportUserSheet : EntityBase
    {
        #region O/R Mapping Properties
		
		private ReportUser _theUser;
		public ReportUser TheUser
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
            ReportUserSheet another = obj as ReportUserSheet;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.TheUser == another.TheUser) && (this.TheReport == another.TheReport);
            }
        } 
    }
	
}
