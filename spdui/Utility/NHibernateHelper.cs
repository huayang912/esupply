using System;
using System.Collections;

using NHibernate;

namespace Utility
{
	/// <summary>
	/// The helper class for NHibernate. Use Singleton Design Patten, and multi-thread <b>unsafe<b>.
	/// </summary>
	public class NHibernateHelper
	{
		private static NHibernateHelper _instance;
		private NHibernate.Cfg.Configuration _cfg;
		private NHibernate.ISessionFactory _factory;

		
		private NHibernateHelper()
		{
			_cfg = new NHibernate.Cfg.Configuration();
            _cfg.AddAssembly(System.Configuration.ConfigurationManager.AppSettings["NHibernateMappingAssembly"]);
			_factory = _cfg.BuildSessionFactory();	
		}

        /// <summary>
		/// Get the class instance.
		/// </summary>
		public static NHibernateHelper Instance
		{
			get
			{
                lock (typeof(NHibernateHelper))
                {
                    if (_instance == null)
                    {
                        _instance = new NHibernateHelper();
                    }

                    return _instance;
                }
			}
		}

		/// <summary>
		/// Get the nihibernate session factory.
		/// </summary>
		public NHibernate.ISessionFactory SessionFactory
		{
			get
			{
				return _factory;
			}
		}



        /// <summary>
        /// 
        /// </summary>
        /// <param name="theType"></param>
        /// <param name="id"></param>
        /// <returns></returns>
        public object Load(Type theType, int id)
        {
            ISession session = Instance.SessionFactory.OpenSession();

            try
            {
                return session.Load(theType, id);
            }
            finally
            {
                session.Close();
            }
        }

        public IList LoadAll(Type theType, int id)
        {
            ISession session = Instance.SessionFactory.OpenSession();

            try
            {
                string hql = "from " + theType.FullName + " obj";
                IQuery q = session.CreateQuery(hql);
                return q.List();
            }
            finally
            {
                session.Close();
            }
        }

        public IList LoadAll(Type theType, int id, int pageSize, int pageIndex)
        {
            ISession session = Instance.SessionFactory.OpenSession();

            try
            {
                string hql = "from " + theType.FullName + " obj";
                IQuery q = session.CreateQuery(hql);
                q.SetFirstResult(pageSize * pageIndex);
                q.SetMaxResults(pageSize);
                return q.List();
            }
            finally
            {
                session.Close();
            }
        }

        public void Save(Object obj)
        {
            ISession session = Instance.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();
            
            try
            {
                session.Save(obj);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }

        }

        public void Update(Object obj)
        {
            ISession session = Instance.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();

            try
            {
                session.Update(obj);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public void Delete(Object obj)
        {
            ISession session = Instance.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();

            try
            {
                session.Delete(obj);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public void Delete(Type theType, int id)
        {
            ISession session = Instance.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();

            try
            {
                string hql = "from " + theType.FullName + " obj where obj.Id=" + id;
                session.Delete(hql);
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public void Delete(Type theType, IList idList)
        {
            if ((idList == null) || (idList.Count == 0))
            {
                return;
            }

            System.Text.StringBuilder hql = new System.Text.StringBuilder();
            hql.Append("from " + theType.FullName + " obj where obj.Id in (" + idList[0] + ",");

            for (int i = 1; i < idList.Count; i++)
            {
                hql.Append(idList[i] + ", ");
            }

            hql.Append(")");

            ISession session = Instance.SessionFactory.OpenSession();
            ITransaction trans = session.BeginTransaction();

            try
            {
                session.Delete(hql.ToString());
                trans.Commit();
            }
            catch
            {
                trans.Rollback();
                throw;
            }
            finally
            {
                session.Close();
            }
        }

        public IList ExecuteQuery(string hql)
        {
            ISession session = Instance.SessionFactory.OpenSession();
            
            try
            {
                IQuery q = session.CreateQuery(hql);
                return q.List();
            }
            finally
            {
                session.Close();
            }
        }

        public IList ExecutePagedQuery(string hql, int pageSize, int pageIndex)
        {
            ISession session = Instance.SessionFactory.OpenSession();

            try
            {
                IQuery q = session.CreateQuery(hql);
                q.SetFirstResult(pageSize * pageIndex);
                q.SetMaxResults(pageSize);
                return q.List();
            }
            finally
            {
                session.Close();
            }
        }

        public IList ExecuteQueryWithParameter(string hql, object[] parameters)
        {
            ISession session = Instance.SessionFactory.OpenSession();

            try
            {
                IQuery q = session.CreateQuery(hql);
                for (int i = 0; i < parameters.Length; i++)
                {
                    q.SetParameter(i, parameters[i]);
                }
                return q.List();
            }
            finally
            {
                session.Close();
            }
        }

        public IList ExecutePagedQueryWithParameter(string hql, int pageSize, int pageIndex, object[] parameters)
        {
            ISession session = Instance.SessionFactory.OpenSession();

            try
            {
                IQuery q = session.CreateQuery(hql);
                q.SetFirstResult(pageSize * pageIndex);
                q.SetMaxResults(pageSize);
                for (int i = 0; i < parameters.Length; i++)
                {
                    q.SetParameter(i, parameters[i]);
                }
                return q.List();
            }
            finally
            {
                session.Close();
            }
        }

    }
}
