using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    [Serializable]
    public class Order
    {
        private bool _ascending;
        private string _propertyName;

        public Order(string propertyName, bool ascending)
        {
            _propertyName = propertyName;
            _ascending = ascending;
        }

        public bool Ascending
        {
            get
            {
                return _ascending;
            }
            set
            {
                _ascending = value;
            }
        }

        public string PropertyName
        {
            get
            {
                return _propertyName;
            }
            set
            {
                _propertyName = value;
            }
        }

        public static Order Asc(string propertyName)
        {
            return new Order(propertyName, true);
        }

        public static Order Desc(string propertyName)
        {
            return new Order(propertyName, false);
        }

        public NHibernate.Expression.Order ToNHOrder()
        {
            return new NHibernate.Expression.Order(_propertyName, _ascending);
        }
    }

}
