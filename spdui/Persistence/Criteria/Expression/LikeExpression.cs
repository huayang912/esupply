using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    class LikeExpression : SimpleExpression
    {
        private MatchMode _matchMode = MatchMode.None;
        private string _stringValue;

        public LikeExpression(string propertyName, object value, bool ignoreCase)
            : base(propertyName, value, ignoreCase)
        {
        }

        public LikeExpression(string propertyName, object value)
            : base(propertyName, value)
        {
        }

        public LikeExpression(string propertyName, string value, MatchMode matchMode)
            : base(propertyName, value)
        {
            _stringValue = value;
            _matchMode = matchMode;
        }

        public MatchMode MatchMode
        {
            get
            {
                return _matchMode;
            }
        }

        public override NHibernate.Expression.ICriterion ToNHExpression()
        {
            if (this.MatchMode == MatchMode.None)
            {
                return new NHibernate.Expression.LikeExpression(this.PropertyName, this.Value, this.IgnoreCase);
            }
            else
            {
                NHibernate.Expression.MatchMode nhMatchMode = NHMatchModeConvert.ToNHMatchMode(this.MatchMode);
                return new NHibernate.Expression.LikeExpression(this.PropertyName,
                    nhMatchMode.ToMatchString(_stringValue));
            }
        }

    }
}
