using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class EqPropertyExpression : PropertyExpression
    {
        public EqPropertyExpression(string lhsPropertyName, string rhsPropertyName)
            : base(lhsPropertyName, rhsPropertyName)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.EqPropertyExpression(this.LhsPropertyName, this.RhsPropertyName);
        }
    }
}
