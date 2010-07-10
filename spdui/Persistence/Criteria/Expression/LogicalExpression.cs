using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public abstract class LogicalExpression : IExpression
    {
        protected readonly IExpression _lhs;
        protected readonly IExpression _rhs;

        public LogicalExpression(IExpression lhs, IExpression rhs)
        {
            _lhs = lhs;
            _rhs = rhs;
        }

        public IExpression LeftHandSide
        {
            get
            {
                return _lhs;
            }
        }

        public IExpression RightHandSide
        {
            get
            {
                return _rhs;
            }
        }

        public virtual NHibernate.Expression.ICriterion ToNHExpression()
        {
            return null;
        }
    }
}
