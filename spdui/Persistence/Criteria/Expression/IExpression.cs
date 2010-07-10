using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    public interface IExpression
    {
        NHibernate.Expression.ICriterion ToNHExpression();
    }
}
