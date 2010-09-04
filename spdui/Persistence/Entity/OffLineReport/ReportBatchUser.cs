using System;
using System.Collections;
using Dndp.Persistence.Entity.Security;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportBatchUser : EntityBase
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
            ReportBatchUser another = obj as ReportBatchUser;

            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id) && (this.TheReportBatch == another.TheReportBatch) && (this.TheUser == another.TheUser);
            }
        }
    }

}
