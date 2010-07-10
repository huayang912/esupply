using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class EqExpression : SimpleExpression
    {
        public EqExpression(string propertyName, object value, bool ignoreCase)
            : base(propertyName, value, ignoreCase)
        {
        }

        public EqExpression(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.EqExpression(this.PropertyName, this.Value);
        }
    }
}
