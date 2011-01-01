using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace com.LocalSystem.Entity.MasterData
{
    [Serializable]
    public abstract class AppUserPermissionBase : EntityBase
    {
        #region O/R Mapping Properties
		
		private Int32 _id;
		public Int32 Id
		{
			get
			{
				return _id;
			}
			set
			{
				_id = value;
			}
		}
		private string _appUser;
		public string AppUser
		{
			get
			{
				return _appUser;
			}
			set
			{
				_appUser = value;
			}
		}
		private string _appPermission;
		public string AppPermission
		{
			get
			{
				return _appPermission;
			}
			set
			{
				_appPermission = value;
			}
		}
        
        #endregion

		public override int GetHashCode()
        {
			if (Id != null)
            {
                return Id.GetHashCode();
            }
            else
            {
                return base.GetHashCode();
            }
        }

        public override bool Equals(object obj)
        {
            AppUserPermissionBase another = obj as AppUserPermissionBase;

            if (another == null)
            {
                return false;
            }
            else
            {
            	return (this.Id == another.Id);
            }
        } 
    }
	
}
