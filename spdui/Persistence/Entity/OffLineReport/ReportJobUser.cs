using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportJobUser : EntityBase
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
            ReportJobUser another = obj as ReportJobUser;
			
            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id) && (this.TheJob == another.TheJob) && (this.TheUser == another.TheUser);
            }
        } 
    }
	
}
