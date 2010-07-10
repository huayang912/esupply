using System;
using System.Collections;
using Dndp.Persistence.Entity.Distribution;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeUser : EntityBase
    {
        #region O/R Mapping Properties

        private DistributionUser _theDistributionUser;
        public DistributionUser TheDistributionUser
        {
            get
            {
                return _theDistributionUser;
            }
            set
            {
                _theDistributionUser = value;
            }
        }
		
		private string _name;
		public string Name
		{
			get
			{
				return _name;
			}
			set
			{
				_name = value;
			}
		}
		
		private string _description;
		public string Description
		{
			get
			{
				return _description;
			}
			set
			{
				_description = value;
			}
		}
		
		private string _cubeSite;
		public string CubeSite
		{
			get
			{
				return _cubeSite;
			}
			set
			{
				_cubeSite = value;
			}
		}
		
		private string _cubeDocumentLibrary;
		public string CubeDocumentLibrary
		{
			get
			{
				return _cubeDocumentLibrary;
			}
			set
			{
				_cubeDocumentLibrary = value;
			}
		}
		
		private string _cubeReadUserList;
		public string CubeReadUserList
		{
			get
			{
				return _cubeReadUserList;
			}
			set
			{
				_cubeReadUserList = value;
			}
		}
		
		private string _cubeFullControlUserList;
		public string CubeFullControlUserList
		{
			get
			{
				return _cubeFullControlUserList;
			}
			set
			{
				_cubeFullControlUserList = value;
			}
		}
		
		private int _activeFlag;
		public int ActiveFlag
		{
			get
			{
				return _activeFlag;
			}
			set
			{
				_activeFlag = value;
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
            CubeUser another = obj as CubeUser;
			
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
