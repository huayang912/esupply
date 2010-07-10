using System;

using NHibernate;
using Dndp.Utility;
using Dndp.Persistence.Dao.Security;
using Dndp.Persistence.Entity.Security;
using System.Collections;
using Dndp.Persistence.Dao;

namespace Dndp.Service
{
	/// <summary>
	/// The base class of all Session Class.
	/// </summary>
	public class SessionBase : ISession
	{
        private IDao daoBase;

        public void setDaoBase(IDao daoBase) 
        {
            this.daoBase = daoBase;
        }

        public IList FindAll(Type type)
        {
            return daoBase.FindAll(type);
        }

        public IList FindAll(Type type, int firstRow, int maxRows)
        {
            return daoBase.FindAll(type, firstRow, maxRows);
        }

        public object FindById(Type type, object id)
        {
            return daoBase.FindById(type, id);
        }

        public object Create(object instance)
        {
            return daoBase.Create(instance);
        }

        public void Update(object instance)
        {
            daoBase.Update(instance);
        }

        public void Delete(object instance)
        {
            daoBase.Delete(instance);
        }


        public void DeleteAll(Type type)
        {
            daoBase.DeleteAll(type);
        }

        public void Save(object instance)
        {
            daoBase.Save(instance);
        }
	}
}
