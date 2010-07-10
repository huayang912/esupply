using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public abstract class Junction : IExpression
    {
        protected IList<IExpression> _expressions = new List<IExpression>();

        public Junction Add(IExpression expression)
        {
            _expressions.Add(expression);
            return this;
        }

        public IList<IExpression> Expressions
        {
            get
            {
                return _expressions;
            }
        }

        public virtual NHibernate.Expression.ICriterion ToNHExpression()
        {
            return null;
        }
    }
}
