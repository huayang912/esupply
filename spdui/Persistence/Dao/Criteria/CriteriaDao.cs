using System;
using System.Collections;
using System.Text;

using Castle.Facilities.NHibernateIntegration;

namespace Dndp.Persistence.Dao.Criteria
{
    public class CriteriaDao : NHDaoBase, ICriteriaDao
    {
        public CriteriaDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        public IList List(Persistence.Criteria.Criteria criteria)
        {
            return criteria.ToNHCriteria(GetSession()).List();
        }
    }
}
