using System;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public class AppUser : AppUserBase
    {
        #region Non O/R Mapping Properties

        public bool HasPermission(string permissionCode)
        {
            foreach (AppUserPermission p in this.AppUserPermissions)
            {
                if (permissionCode == p.AppPermission)
                {
                    return true;
                }
            }
            return false;
        }
        public string Name
        {
            get
            {
                return ((FirstName != null ? FirstName : string.Empty) + " " + (LastName != null ? LastName : string.Empty));
            }

        }
        #endregion
    }
}