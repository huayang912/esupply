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
    public class NHBarCodeBaseDao : NHDaoBase, IBarCodeBaseDao
    {
        public NHBarCodeBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreateBarCode(BarCode entity)
        {
            Create(entity);
        }

        public virtual IList<BarCode> GetAllBarCode()
        {
            return FindAll<BarCode>();
        }

        public virtual BarCode LoadBarCode(Int32 id)
        {
            return FindById<BarCode>(id);
        }

        public virtual void UpdateBarCode(BarCode entity)
        {
            Update(entity);
        }

        public virtual void DeleteBarCode(Int32 id)
        {
            string hql = @"from BarCode entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeleteBarCode(BarCode entity)
        {
            Delete(entity);
        }

        public virtual void DeleteBarCode(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from BarCode entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeleteBarCode(IList<BarCode> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (BarCode entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeleteBarCode(pkList);
        }


        #endregion Method Created By CodeSmith
    }
}
