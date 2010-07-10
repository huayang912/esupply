using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class AndExpression : LogicalExpression
    {
        public AndExpression(IExpression lhs, IExpression rhs)
            : base(lhs, rhs)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.AndExpression(this.LeftHandSide.ToNHExpression(), this.RightHandSide.ToNHExpression());
        }
    }
}
