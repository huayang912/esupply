using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

using com.LocalSystem.Persistence.Criteria;
using NHibernate.Expression;

namespace com.LocalSystem.Service.Criteria.Impl
{
    public class CriteriaMgr : SessionBase, ICriteriaMgr
    {
        public ICriteriaDao _criteriaDao {get;set; }

        public IList FindAll(DetachedCriteria criteria)
        {
            return _criteriaDao.FindAll(criteria);
        }

        public IList FindAll(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            return _criteriaDao.FindAll(criteria, firstRow, maxRows);
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria)
        {
            return _criteriaDao.FindAll<T>(criteria);
        }

        public IList<T> FindAll<T>(DetachedCriteria criteria, int firstRow, int maxRows)
        {
            return _criteriaDao.FindAll<T>(criteria, firstRow, maxRows);
        }
    }
}

#region Extend Class

namespace com.LocalSystem.Service.Ext.Criteria.Impl
{
    public partial class CriteriaMgrE : com.LocalSystem.Service.Criteria.Impl.CriteriaMgr, ICriteriaMgrE
    {
    }
}

#endregion
