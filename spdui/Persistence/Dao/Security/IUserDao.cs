using System;
using System.Collections.Generic;
using System.Text;
using Dndp.Persistence.Entity.Security;
using System.Collections;

namespace Dndp.Persistence.Dao.Security
{
    public interface IUserDao
    {
        #region Method Created By CodeSmith

        void CreateUser(User user);

        User LoadUser(int userId);

        void UpdateUser(User user);
        
        void DeleteUser(int userId);

        void DeleteUser(User user);

        void DeleteUser(IList<int> userIdList);

        void DeleteUser(IList<User> userList);

        #endregion Method Created By CodeSmith

        #region Customized Methods

        IList SearchUserByUserName(string userName);

        IList SearchUserByUserName(string userName, string[] orderByColumns);

        User SearchUserByUserNmAndPwd(string userNm, string userPwd);

        User SearchUserByWinDomainAndWinUserNm(string windowsDomain, string windowsUserName);

        Boolean IsUserExist(string userName, int excludedUserId);

        IList FindUserAuthorizations(int userId);

        IList<User> FindUserByRole(int roleId);

        IList<User> GetAllUser();

        #endregion Customized Methods
    }
}
