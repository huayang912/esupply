using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class LtExpression : SimpleExpression
    {
        public LtExpression(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.LtExpression(this.PropertyName, this.Value);
        }
    }
}
