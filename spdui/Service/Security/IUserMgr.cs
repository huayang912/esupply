using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;
using Dndp.Persistence.Entity.Security;

namespace Dndp.Service.Security
{
    public interface IUserMgr
    {
        #region Method Created By CodeSmith

        User LoadUser(int userId);

        void DeleteUser(int userId);

        void DeleteUser(User user);

        void DeleteUser(IList<int> userIdList);

        void DeleteUser(IList<User> userList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        void CreateUser(User u, IList roleIdList);

        void UpdateUser(User u, IList roleIdList);

        IList Search(string userName);

        IList Search(string userName, string[] orderByColumns); 
        
        User LoadUserWithRoles(int userId);

        #endregion Customized Methods

    }
}
