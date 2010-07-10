using System;
using System.Collections;
using System.Text;

namespace Dndp.Persistence.Dao.Criteria
{
    public interface ICriteriaDao
    {
        IList List(Persistence.Criteria.Criteria criteria);
    }
}
