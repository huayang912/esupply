using System;
using System.Collections;
using System.Text;

using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.Security
{
    [Serializable]
    public class User : EntityBase
    {
        #region O/R Mapping Properties

        private string _userName;
        private string _password;
        private string _email;
        private string _windowsDomain;
        private string _windowsUserName;
        private IList _roles;

        public string UserName
        {
            get
            {
                return _userName;
            }
            set
            {
                _userName = value;
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                _password = value;
            }
        }

        public string Email
        {
            get
            {
                return _email;
            }
            set
            {
                _email = value;
            }
        }

        public IList Roles
        {
            get
            {
                return _roles;
            }
            set
            {
                _roles = value;
            }
        }

        public string WindowsDomain
        {
            get
            {
                return _windowsDomain;
            }
            set
            {
                _windowsDomain = value;
            }
        }

        public string WindowsUserName
        {
            get
            {
                return _windowsUserName;
            }
            set
            {
                _windowsUserName = value;
            }
        }

        #endregion

        #region Non O/R Mapping Properties

        private IList _authorizations;
        private IList _menus;

        public IList Authorizations
        {
            get
            {
                return _authorizations;
            }
            set
            {
                _authorizations = value;
            }
        }

        public IList Menus
        {
            get
            {
                return _menus;
            }
            set
            {
                _menus = value;
            }
        }

        #endregion

        public bool HasPermission(string moduleName)
        {
            foreach (Authorization auth in Authorizations)
            {
                if (auth.TheModule.Name == moduleName)
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPermissionView(string moduleName)
        {
            foreach (Authorization auth in Authorizations)
            {
                if (auth.PermissionView && (auth.TheModule.Name == moduleName)) 
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPermissionUpdate(string moduleName)
        {
            foreach (Authorization auth in Authorizations)
            {
                if (auth.PermissionUpdate && (auth.TheModule.Name == moduleName))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPermissionAdd(string moduleName)
        {
            foreach (Authorization auth in Authorizations)
            {
                if (auth.PermissionAdd && (auth.TheModule.Name == moduleName))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPermissionDelete(string moduleName)
        {
            foreach (Authorization auth in Authorizations)
            {
                if (auth.PermissionDelete && (auth.TheModule.Name == moduleName))
                {
                    return true;
                }
            }

            return false;
        }

        public bool HasPermissionFull(string moduleName)
        {
            foreach (Authorization auth in Authorizations)
            {
                if (auth.PermissionFull && (auth.TheModule.Name == moduleName))
                {
                    return true;
                }
            }

            return false;
        }

        public override int GetHashCode()
        {
            if (Id > 0)
            {
                return Id;
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            User u = obj as User;

            if (u == null)
            {
                return false;
            }
            else
            {
                return (this.UserName == u.UserName)
                    && (this.Password == u.Password)
                    && (this.Email == u.Email);
            }
        } 


    }
}
