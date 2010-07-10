using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class Disjunction : Junction
    {
        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.Disjunction();
        }
    }
}
