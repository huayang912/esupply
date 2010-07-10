using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class BetweenExpression : IExpression
    {
        private string _propertyName;
        private object _lo;
        private object _hi;

        public BetweenExpression(string propertyName, object lo, object hi)
        {
            _propertyName = propertyName;
            _lo = lo;
            _hi = hi;
        }

        public NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.BetweenExpression(_propertyName, _lo, _hi);
        }
    }
}
