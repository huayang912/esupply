using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class GeExpression : SimpleExpression
    {
        public GeExpression( string propertyName, object value ) : base( propertyName, value )
		{
		}

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.GeExpression(this.PropertyName, this.Value);
        }
    }
}
