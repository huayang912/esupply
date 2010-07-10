using System;

using Utility;

namespace Dndp.Persistence.Entity
{
	/// <summary>
	/// The base class of all Business Entities
	/// </summary>
    [Serializable]
	public abstract class EntityBase
	{
        private int _id;
        public int Id
        {
            get
            {
                return _id;
            }
            set
            {
                _id = value;
            }
        }

        //protected static NHibernate.ISessionFactory SessionFactory
        //{
        //    get
        //    {
        //        return NHibernateHelper.Instance.SessionFactory;
        //    }
        //}

		

	}
}
