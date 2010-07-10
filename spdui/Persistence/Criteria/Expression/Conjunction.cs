using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class Conjunction : Junction
    {
        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.Conjunction();
        }
    }
}
