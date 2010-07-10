using System;
using System.Collections.Generic;
using System.Text;

using Dndp.Persistence.Criteria.Expression;

namespace Dndp.Persistence.Criteria
{
    [Serializable]
    public class Criteria
    {
        private Type _classType;
        private IList<IExpression> _expressions = new List<IExpression>();
        private IList<Order> _orders = new List<Order>();

        private int _maxResult = -1;
        private int _firstResult = -1;
        private int _timeout = -1;

        public Criteria(Type classType)
        {
            _classType = classType;
        }

        public Type ClassType
        {
            get
            {
                return _classType;
            }
        }

        public void Add(IExpression expression)
        {
            _expressions.Add(expression);
        }

        public void AddOrder(Order order)
        {
            _orders.Add(order);
        }

        public void RemoveAll()
        {
            _expressions.Clear();
        }

        public void RemoveAllOrders()
        {
            _orders.Clear();
        }

        public void RemoveOrder(string propertyName)
        {
            foreach (Order order in _orders)
            {
                if (order.PropertyName == propertyName)
                {
                    _orders.Remove(order);
                    break;
                }
            }
        }

        public bool IsOrderExist(string propertyName, bool ascending)
        {
            foreach (Order order in _orders)
            {
                if ((order.PropertyName == propertyName) && (order.Ascending == ascending))
                {
                    return true;
                }
            }

            return false;
        }

        public int MaxResult
        {
            get
            {
                return _maxResult;
            }
            set
            {
                _maxResult = value;
            }
        }

        public int FirstResult
        {
            get
            {
                return _firstResult;
            }
            set
            {
                _firstResult = value;
            }
        }

        public int TimeOut
        {
            get
            {
                return _timeout;
            }
            set
            {
                _timeout = value;
            }
        }

        public NHibernate.ICriteria ToNHCriteria(NHibernate.ISession session)
        {
            NHibernate.ICriteria nhCriteria = session.CreateCriteria(this.ClassType);

            if (this.MaxResult != -1)
            {
                nhCriteria.SetMaxResults(this.MaxResult);
            }

            if (this.FirstResult != -1)
            {
                nhCriteria.SetFirstResult(this.FirstResult);
            }

            if (this.TimeOut != -1)
            {
                nhCriteria.SetTimeout(this.TimeOut);
            }

            foreach (IExpression expression in _expressions)
            {
                nhCriteria.Add(expression.ToNHExpression());
            }

            foreach (Order order in _orders)
            {
                nhCriteria.AddOrder(order.ToNHOrder());
            }

            return nhCriteria;
        }

    }
}
