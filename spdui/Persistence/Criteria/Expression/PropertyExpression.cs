using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public abstract class PropertyExpression : IExpression
    {
        protected string _lhsPropertyName;
        protected string _rhsPropertyName;

        public PropertyExpression(string lhsPropertyName, string rhsPropertyName)
        {
            _lhsPropertyName = lhsPropertyName;
            _rhsPropertyName = rhsPropertyName;
        }

        public string LhsPropertyName
        {
            get
            {
                return _lhsPropertyName;
            }
        }

        public string RhsPropertyName
        {
            get
            {
                return _rhsPropertyName;
            }
        }

        public virtual NHibernate.Expression.ICriterion ToNHExpression()
        {
            return null;
        }
    }
}
