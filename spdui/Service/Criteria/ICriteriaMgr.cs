using System;
using System.Collections;
using System.Text;

namespace Dndp.Service.Criteria
{
    public interface ICriteriaMgr : ISession
    {
        IList List(Dndp.Persistence.Criteria.Criteria criteria);
    }
}
