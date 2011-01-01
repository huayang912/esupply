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
    public class NHOutboundLogBaseDao : NHDaoBase, IOutboundLogBaseDao
    {
        public NHOutboundLogBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateOutboundLog(OutboundLog entity)
        {
            Create(entity);
        }

        public virtual IList<OutboundLog> GetAllOutboundLog()
        {
            return FindAll<OutboundLog>();
        }

        public virtual OutboundLog LoadOutboundLog(Int32 id)
        {
            return FindById<OutboundLog>(id);
        }

        public virtual void UpdateOutboundLog(OutboundLog entity)
        {
            Update(entity);
        }

        public virtual void DeleteOutboundLog(Int32 id)
        {
            string hql = @"from OutboundLog entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteOutboundLog(OutboundLog entity)
        {
            Delete(entity);
        }

        public virtual void DeleteOutboundLog(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from OutboundLog entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteOutboundLog(IList<OutboundLog> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (OutboundLog entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteOutboundLog(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
