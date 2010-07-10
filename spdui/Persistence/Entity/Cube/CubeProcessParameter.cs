using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeProcessParameter : EntityBase
    {
        #region O/R Mapping Properties
		
		private string _value;
		public string Value
		{
			get
			{
				return _value;
			}
			set
			{
				_value = value;
			}
		}

        private CubeProcess _theProcess;
        public CubeProcess TheProcess
        {
            get
            {
                return _theProcess;
            }
            set
            {
                _theProcess = value;
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
            CubeProcessParameter another = obj as CubeProcessParameter;
			
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
