using System;
using System.Collections;
using System.Collections.Generic;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeRole : EntityBase
    {
        #region O/R Mapping Properties

        private CubeDefinition _theCube;
        public CubeDefinition TheCube
        {
            get
            {
                return _theCube;
            }
            set
            {
                _theCube = value;
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
		
		private int _isVisualtotal;
		public int IsVisualtotal
		{
			get
			{
				return _isVisualtotal;
			}
			set
			{
				_isVisualtotal = value;
			}
		}
		
		private int _isDrillthroughAndLocalCube;
		public int IsDrillthroughAndLocalCube
		{
			get
			{
				return _isDrillthroughAndLocalCube;
			}
			set
			{
				_isDrillthroughAndLocalCube = value;
			}
		}
		
        
        #endregion

        #region Non O/R Mapping Properties

        private IList<CubeUser> _cubeUserList;
        public IList<CubeUser> CubeUserList
        {
            get
            {
                return _cubeUserList;
            }
            set
            {
                _cubeUserList = value;
            }
        }

        private IList<CubeDimension> _cubeDimensionList;
        public IList<CubeDimension> CubeDimensionList
        {
            get
            {
                return _cubeDimensionList;
            }
            set
            {
                _cubeDimensionList = value;
            }
        }

        private IList<CubeRoleDimensionMember> _cubeRoleDimensionMemberList;
        public IList<CubeRoleDimensionMember> CubeRoleDimensionMemberList
        {
            get
            {
                return _cubeRoleDimensionMemberList;
            }
            set
            {
                _cubeRoleDimensionMemberList = value;
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
            CubeRole another = obj as CubeRole;
			
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
