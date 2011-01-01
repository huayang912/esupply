using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Castle.Facilities.NHibernateIntegration;
using NHibernate;
using NHibernate.Type;
using com.LocalSystem.Entity.Operation;

//TODO: Add other using statmens here.

namespace com.LocalSystem.Persistence.Operation.NH
{
    public class NHPoBaseDao : NHDaoBase, IPoBaseDao
    {
        public NHPoBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePo(Po entity)
        {
            Create(entity);
        }

        public virtual IList<Po> GetAllPo()
        {
            return FindAll<Po>();
        }

        public virtual Po LoadPo(String code)
        {
            return FindById<Po>(code);
        }

        public virtual void UpdatePo(Po entity)
        {
            Update(entity);
        }

        public virtual void DeletePo(String code)
        {
            string hql = @"from Po entity where entity.Code = ?";
            Delete(hql, new object[] { code }, new IType[] { NHibernateUtil.String });
        }

        public virtual void DeletePo(Po entity)
        {
            Delete(entity);
        }

        public virtual void DeletePo(IList<String> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from Po entity where entity.Code in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePo(IList<Po> entityList)
        {
            IList<String> pkList = new List<String>();
            foreach (Po entity in entityList)
            {
                pkList.Add(entity.Code);
            }

            DeletePo(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
