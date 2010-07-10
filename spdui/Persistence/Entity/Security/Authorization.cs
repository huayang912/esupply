using System;
using System.Collections;
using System.Text;

using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.Security
{
    [Serializable]
    public class Authorization : EntityBase
    {
        #region O/R Mapping Properties

        private bool _permissionView;
        private bool _permissionUpdate;
        private bool _permissionAdd;
        private bool _permissionDelete;
        private bool _permissionFull;

        private Role _theRole;
        private Module _theModule;

        public bool PermissionView
        {
            get
            {
                return _permissionView;
            }
            set
            {
                _permissionView = value;
            }
        }

        public bool PermissionUpdate
        {
            get
            {
                return _permissionUpdate;
            }
            set
            {
                _permissionUpdate = value;
            }
        }

        public bool PermissionAdd
        {
            get
            {
                return _permissionAdd;
            }
            set
            {
                _permissionAdd = value;
            }
        }

        public bool PermissionDelete
        {
            get
            {
                return _permissionDelete;
            }
            set
            {
                _permissionDelete = value;
            }
        }

        public bool PermissionFull
        {
            get
            {
                return _permissionFull;
            }
            set
            {
                _permissionFull = value;
            }
        }

        public Role TheRole
        {
            get
            {
                return _theRole;
            }
            set
            {
                _theRole = value;
            }
        }

        public Module TheModule
        {
            get
            {
                return _theModule;
            }
            set
            {
                _theModule = value;
            }
        }

        #endregion

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
            Authorization a = obj as Authorization;

            if (a == null)
            {
                return false;
            }
            else
            {
                return (this.PermissionView == a.PermissionView)
                    && (this.PermissionUpdate == a.PermissionUpdate)
                    && (this.PermissionAdd == a.PermissionAdd)
                    && (this.PermissionDelete == a.PermissionDelete)
                    && (this.PermissionFull == a.PermissionFull);
            }
        }
    }
}
