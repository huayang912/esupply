using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class LeExpression : SimpleExpression
    {
        public LeExpression(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.LeExpression(this.PropertyName, this.Value);
        }
    }
}
