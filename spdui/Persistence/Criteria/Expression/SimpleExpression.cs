using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public abstract class SimpleExpression : IExpression
    {
        protected readonly string _propertyName;
        protected readonly object _value;
        protected bool _ignoreCase;

        public SimpleExpression(string propertyName, object value)
        {
            _propertyName = propertyName;
            _value = value;
            _ignoreCase = true;
        }

        public SimpleExpression(string propertyName, object value, bool ignoreCase)
        {
            _propertyName = propertyName;
            _value = value;
            _ignoreCase = ignoreCase;
        }

        public bool IgnoreCase
        {
            get
            {
                return _ignoreCase;
            }
        }

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        public object Value
        {
            get
            {
                return _value;
            }
        }

        public virtual NHibernate.Expression.ICriterion ToNHExpression()
        {
            return null;
        }
    }
}
