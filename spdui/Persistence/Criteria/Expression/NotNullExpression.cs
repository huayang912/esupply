using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class NotNullExpression : IExpression
    {
        private readonly string _propertyName;

        public NotNullExpression(string propertyName)
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
            return new NHibernate.Expression.NotNullExpression(_propertyName);
        }
    }
}
