using System;
using System.Collections;
using System.Text;

using Dndp.Persistence.Dao.Criteria;

namespace Dndp.Service.Criteria.Impl
{
    class CriteriaMgr : SessionBase, ICriteriaMgr
    {
        private ICriteriaDao _criteriaDao;

        public CriteriaMgr(ICriteriaDao criteriaDao)
        {
            _criteriaDao = criteriaDao;
        }

        public IList List(Dndp.Persistence.Criteria.Criteria criteria)
        {
            return _criteriaDao.List(criteria);
        }
    }
}
