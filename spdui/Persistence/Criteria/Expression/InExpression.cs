using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    class InExpression : IExpression
    {
        protected readonly string _propertyName;
        protected readonly object[] _values;

        public InExpression(string propertyName, object[] values)
        {
            _propertyName = propertyName;
            _values = values;
        }

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        public object[] Values
        {
            get
            {
                return _values;
            }
        }

        public NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.InExpression(_propertyName, Values);
        }

    }
}
