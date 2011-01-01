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
    public class NHPoDetailBaseDao : NHDaoBase, IPoDetailBaseDao
    {
        public NHPoDetailBaseDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public virtual void CreatePoDetail(PoDetail entity)
        {
            Create(entity);
        }

        public virtual IList<PoDetail> GetAllPoDetail()
        {
            return FindAll<PoDetail>();
        }

        public virtual PoDetail LoadPoDetail(Int32 id)
        {
            return FindById<PoDetail>(id);
        }

        public virtual void UpdatePoDetail(PoDetail entity)
        {
            Update(entity);
        }

        public virtual void DeletePoDetail(Int32 id)
        {
            string hql = @"from PoDetail entity where entity.Id = ?";
            Delete(hql, new object[] { id }, new IType[] { NHibernateUtil.Int32 });
        }

        public virtual void DeletePoDetail(PoDetail entity)
        {
            Delete(entity);
        }

        public virtual void DeletePoDetail(IList<Int32> pkList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from PoDetail entity where entity.Id in (");
            hql.Append(pkList[0]);
            for (int i = 1; i < pkList.Count; i++)
            {
                hql.Append(",");
                hql.Append(pkList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public virtual void DeletePoDetail(IList<PoDetail> entityList)
        {
            IList<Int32> pkList = new List<Int32>();
            foreach (PoDetail entity in entityList)
            {
                pkList.Add(entity.Id);
            }

            DeletePoDetail(pkList);
        }


        public virtual PoDetail LoadPoDetail(String seq, String poCode)
        {
            string hql = @"from PoDetail entity where entity.Seq = ? and entity.PoCode = ?";
            IList<PoDetail> result = FindAllWithCustomQuery<PoDetail>(hql, new object[] { seq, poCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
            if (result != null && result.Count > 0)
            {
                return result[0];
            }
            else
            {
                return null;
            }
        }

        public virtual void DeletePoDetail(String seq, String poCode)
        {
            string hql = @"from PoDetail entity where entity.Seq = ? and entity.PoCode = ?";
            Delete(hql, new object[] { seq, poCode }, new IType[] { NHibernateUtil.String, NHibernateUtil.String });
        }

        #endregion Method Created By CodeSmith
    }
}
