using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeDefinedParameter : EntityBase
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

        private CubeParameter _theParameter;
        public CubeParameter TheParameter
        {
            get
            {
                return _theParameter;
            }
            set
            {
                _theParameter = value;
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
            CubeDefinedParameter another = obj as CubeDefinedParameter;
			
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
