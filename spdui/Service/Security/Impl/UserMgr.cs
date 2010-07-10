using System;
using System.Collections;
using System.Text;

using NHibernate;

using Dndp.Persistence.Entity.Security;
using Utility;
using Dndp.Persistence.Dao.Security;
using Castle.Services.Transaction;
using System.Collections.Generic;

using Dndp.Persistence.Criteria;
using Dndp.Persistence.Criteria.Expression;

namespace Dndp.Service.Security.Impl
{
    [Transactional]
    public class UserMgr : SessionBase, IUserMgr
    {
        private IUserDao userDao;
        private IRoleDao roleDao;

        public UserMgr(IUserDao userDao, IRoleDao roleDao)
        {
            this.userDao = userDao;
            this.roleDao = roleDao;
        }

        #region Method Created By CodeSmith

        [Transaction(TransactionMode.Requires)]
        public void CreateUser(User u, IList roleIdList)
        {
            if (userDao.IsUserExist(u.UserName, u.Id))
            {
                throw new ApplicationException("Add user failed, the user name already exists.");
            }

            if (roleIdList != null)
            {
                u.Roles = new ArrayList();

                foreach (int roleId in roleIdList)
                {
                    u.Roles.Add(roleDao.SearchRoleByPK(roleId));
                }
            }
            userDao.CreateUser(u);
        }

        [Transaction(TransactionMode.Unspecified)]
        public User LoadUser(int userId)
        {
            if (userId <= 0)
            {
                throw new ArgumentException("Invliad parameter: userId");
            }

            return userDao.LoadUser(userId);
        }

        [Transaction(TransactionMode.Requires)]
        public virtual void UpdateUser(User u, IList roleIdList)
        {
            if (userDao.IsUserExist(u.UserName, u.Id))
            {
                throw new ApplicationException("Update user failed, the user name already exists.");
            }

            if (roleIdList != null)
            {
                u.Roles = new ArrayList();
                foreach (int id in roleIdList)
                {
                    u.Roles.Add(roleDao.SearchRoleByPK(id));
                }
            }
            userDao.UpdateUser(u);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteUser(int userId)
        {
            userDao.DeleteUser(userId);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteUser(User user)
        {
            userDao.DeleteUser(user);
        }

       
        [Transaction(TransactionMode.Requires)]
        public void DeleteUser(IList<int> userIdList)
        {
            if ((userIdList == null) || (userIdList.Count == 0))
            {
                return;
            }

            userDao.DeleteUser(userIdList);
        }

        [Transaction(TransactionMode.Requires)]
        public void DeleteUser(IList<User> userList)
        {
            if ((userList == null) || (userList.Count == 0))
            {
                return;
            }

            userDao.DeleteUser(userList);
        }

        #endregion Method Created By CodeSmith

        #region Customized Methods

        [Transaction(TransactionMode.Unspecified)]
        public IList Search(string userName)
        {
            return userDao.SearchUserByUserName(userName);
        }

        [Transaction(TransactionMode.Unspecified)]
        public IList Search(string userName, string[] orderByColumns)
        {
            return userDao.SearchUserByUserName(userName, orderByColumns);
        }

        [Transaction(TransactionMode.Requires)]
        public User LoadUserWithRoles(int userId)
        {
            User user = LoadUser(userId);
            int count = user.Roles.Count;
            return user;
        }

        #endregion Customized Methods
    }
}
