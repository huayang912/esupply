using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class NotExpression : IExpression
    {
        private IExpression _expression;

        public NotExpression(IExpression expression)
        {
            _expression = expression;
        }

        public IExpression Expression
        {
            get
            {
                return _expression;
            }
        }

        public NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.NotExpression(_expression.ToNHExpression());
        }
    }
}
