using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Security;
using NHibernate;
using Castle.Facilities.NHibernateIntegration;
using System.Collections;
using NHibernate.Type;
using Dndp.Persistence.Dao;

namespace Dndp.Persistence.Dao.Security.NH
{
    public class NHUserDao : NHDaoBase, IUserDao
    {
        public NHUserDao(ISessionManager sessionManager)
            : base(sessionManager)
        {
        }

        #region Method Created By CodeSmith

        public void CreateUser(User user)
        {
            Create(user);
        }

        public User LoadUser(int userId)
        {
            return FindById(typeof(User), userId) as User;
        }

        public void UpdateUser(User user)
        {
            Update(user);
        }

        public void DeleteUser(int userId)
        {
            string hql = @"from User u where u.Id = ?";

            Delete(hql, userId, NHibernate.NHibernateUtil.Int32);
        }

        public void DeleteUser(User user)
        {
            Delete(user);
        }

        public void DeleteUser(IList<int> userIdList)
        {
            StringBuilder hql = new StringBuilder();
            hql.Append("from User u where u.Id in (");
            hql.Append(userIdList[0]);
            for (int i = 1; i < userIdList.Count; i++)
            {
                hql.Append(",");
                hql.Append(userIdList[i]);
            }
            hql.Append(")");

            Delete(hql.ToString());
        }

        public void DeleteUser(IList<User> userList)
        {
            IList<int> userIdList = new List<int>();
            foreach (User user in userList)
            {
                userIdList.Add(user.Id);
            }

            DeleteUser(userIdList);
        }

        public IList<User> FindUserByRole(int roleId)
        {
            string hql = "select u from User as u, Role as r where r.Id = ? and r in elements(u.Roles)";
            return FindAllWithCustomQuery(hql, roleId) as IList<User>;
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        public IList SearchUserByUserName(string userName)
        {
            string queryUserName = userName.Replace("'", "''");

            string hql = @"from User u where u.UserName like ?";

            return FindAllWithCustomQuery(hql, "%" + userName + "%", NHibernate.NHibernateUtil.String);
        }

        public IList SearchUserByUserName(string userName, string[] orderByColumns)
        {
            if ((orderByColumns == null) || (orderByColumns.Length == 0))
            {
                return SearchUserByUserName(userName);
            }

            StringBuilder hql = new StringBuilder();
            hql.Append("from User u where u.UserName like ? order by ");
            hql.Append(orderByColumns[0]);
            for (int i = 1; i < orderByColumns.Length; i++)
            {
                hql.Append(", ");
                hql.Append(orderByColumns[i]);
            }

            return FindAllWithCustomQuery(hql.ToString(), "%" + userName + "%", NHibernate.NHibernateUtil.String);
        }

        public User SearchUserByUserNmAndPwd(string userNm, string userPwd)
        {
            string hql = @"from User u where u.UserName = ? and u.Password = ?";

            IList list = FindAllWithCustomQuery(hql, new object[] { userNm, userPwd },
                new IType[] { NHibernate.NHibernateUtil.String, NHibernate.NHibernateUtil.String });

            if (list != null && list.Count > 0)
            {
                return list[0] as User;
            }
            else
            {
                return null;
            }
        }

        public User SearchUserByWinDomainAndWinUserNm(string windowsDomain, string windowsUserName)
        {
            string hql = @"from User u where u.WindowsDomain = ? and u.WindowsUserName = ?";

            IList list = FindAllWithCustomQuery(hql, new object[] { windowsDomain, windowsUserName },
                new IType[] { NHibernate.NHibernateUtil.String, NHibernate.NHibernateUtil.String });

            if (list != null && list.Count > 0)
            {
                return list[0] as User;
            }
            else
            {
                return null;
            }
        }

        public Boolean IsUserExist(string userName, int excludedUserId)
        {
            string hql = "select count(*) from User u where u.UserName = ? and u.Id != ?";

            IList list = FindAllWithCustomQuery(hql, new Object[] { userName, excludedUserId },
                new IType[] { NHibernate.NHibernateUtil.String, NHibernate.NHibernateUtil.Int32 });

            int count = (int)list[0];

            return (count > 0);  
        }

        public IList FindUserAuthorizations(int userId)
        {
           string hql = @"
                    select auth
                        from Authorization auth, Role r, User u
                        where auth.TheRole=r
                            and r in elements(u.Roles)
                            and u.Id = ?
                    ";

            return FindAllWithCustomQuery(hql, new object[] { userId }, new IType[] { NHibernate.NHibernateUtil.Int32 });
        }

        public IList<User> GetAllUser()
        {
            string hql = "select u from User as u";
            return FindAllWithCustomQuery(hql) as IList<User>;
        }

        #endregion Customized Methods
    }
}
