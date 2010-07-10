using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class NullExpression : IExpression
    {
        private readonly string _propertyName;

        public NullExpression(string propertyName)
        {
            _propertyName = propertyName;
        }

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
        }

        public NHibernate.Expression.ICriterion ToNHExpression()
        {
            return new NHibernate.Expression.NullExpression(_propertyName);
        }
    }
}
