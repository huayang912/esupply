using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class LePropertyExpression : PropertyExpression
    {
        public LePropertyExpression(string lhsPropertyName, string rhsPropertyName)
            : base(lhsPropertyName, rhsPropertyName)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.LePropertyExpression(this.LhsPropertyName, this.RhsPropertyName);
        }
    }
}
