using System;
using System.Collections;
using System.Text;

using NHibernate;

using Dndp.Persistence.Dao.Security;
using Dndp.Persistence.Entity.Security;
using Dndp.Utility;


namespace Dndp.Service.Security.Impl
{
    public class SecurityMgr : SessionBase, ISecurityMgr
    {
        private IUserDao userDao;

        private IMenuDao menuDao;

        public SecurityMgr(IUserDao userDao, IMenuDao menuDao)
        {
            this.userDao = userDao;
            this.menuDao = menuDao;
        }

        /// <summary>
        /// User login in by UserName and Password.
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public User Login(string userName, string password)
        {
            User u = FindUserByUserNamePassword(userName, password);
            if (u == null)
            {
                return null;
            }

            LoadNonMappingRelations(u);

            return u;
        }

        /// <summary>
        /// User login by windows domain account.
        /// </summary>
        /// <param name="windowsDomain"></param>
        /// <param name="windowsUserName"></param>
        /// <returns></returns>
        public User DomainLogin(string windowsDomain, string windowsUserName)
        {
            User u = FindUserByWindowsDomain(windowsDomain, windowsUserName);
            if (u == null)
            {
                return null;
            }

            LoadNonMappingRelations(u);

            return u;
        }

        /// <summary>
        /// Load the user's non-mapping relations, include Authorizations and Menus.
        /// </summary>
        /// <param name="u"></param>
        private void LoadNonMappingRelations(User u)
        {
            IList allMenus = null;

            LoadAuthorizations(u);  //get the user's related authorization list
            allMenus = LoadAllMenus();  //get all the menus

            u.Menus = new ArrayList();
            LoadMenus(u, allMenus); //load user's related menus from all menu list
        }

        private void LoadMenus(User u, IList allMenus)
        {
            if (u.Authorizations != null && u.Authorizations.Count > 0)
            {
                foreach (Authorization auth in u.Authorizations)
                {
                    foreach (Menu menu in allMenus)
                    {
                        if ((menu.TheModule != null) && (auth.TheModule.Id == menu.TheModule.Id))
                        {
                            AddMenuToUser(u, menu.Id, allMenus);
                        }
                    }
                }

                //sort the menus by PathCode
                ((ArrayList)(u.Menus)).Sort();
            }
        }

        private void AddMenuToUser(User u, int menuId, IList allMenus)
        {
            Menu menu = GetMenuById(menuId, allMenus);
            if (menu == null)
            {
                return;
            }

            if (u.Menus.Contains(menu))
            {
                return;
            }
            else
            {
                u.Menus.Add(menu);
            }

            if (menu.ParentMenuId > 0)
            {
                AddMenuToUser(u, menu.ParentMenuId, allMenus);
            }
        }

        private Menu GetMenuById(int menuId, IList allMenus)
        {
            foreach (Menu menu in allMenus)
            {
                if (menu.Id == menuId)
                {
                    return menu;
                }
            }

            return null;
        }

        private void LoadAuthorizations(User u)
        {
            u.Authorizations = userDao.FindUserAuthorizations(u.Id);
        }

        private IList LoadAllMenus()
        {
            return menuDao.LoadAllMenus();
        }

        private User FindUserByUserNamePassword(string userName, string password)
        {
            return userDao.SearchUserByUserNmAndPwd(userName, password);
        }

        private User FindUserByWindowsDomain(string windowsDomain, string windowsUserName)
        {
            return userDao.SearchUserByWinDomainAndWinUserNm(windowsDomain, windowsUserName);
        }
    }
}
