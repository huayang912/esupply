using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeUserRole : EntityBase
    {
        #region O/R Mapping Properties
       
        private CubeRole _theCubeRole;
        public CubeRole TheCubeRole
        {
            get
            {
                return _theCubeRole;
            }
            set
            {
                _theCubeRole = value;
            }
        }

        private CubeUser _theCubeUser;
        public CubeUser TheCubeUser
        {
            get
            {
                return _theCubeUser;
            }
            set
            {
                _theCubeUser = value;
            }
        }

        #endregion

        #region Non O/R Mapping Properties

        //TODO: Add Non O/R Mapping Properties here. 

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
            CubeUserRole another = obj as CubeUserRole;
			
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
