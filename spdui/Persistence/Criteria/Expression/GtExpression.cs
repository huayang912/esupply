using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class GtExpression : SimpleExpression
    {
        public GtExpression(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.GtExpression(this.PropertyName, this.Value);
        }
    }
}
