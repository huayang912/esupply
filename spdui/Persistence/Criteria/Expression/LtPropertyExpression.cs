using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class LtPropertyExpression : PropertyExpression
    {
        public LtPropertyExpression(string lhsPropertyName, string rhsPropertyName)
            : base(lhsPropertyName, rhsPropertyName)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.LtPropertyExpression(this.LhsPropertyName, this.RhsPropertyName);
        }
    }
}
