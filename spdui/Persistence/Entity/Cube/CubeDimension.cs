using System;
using System.Collections;
using Dndp.Utility;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.Cube
{
    [Serializable]
    public class CubeDimension : EntityBase
    {
        #region O/R Mapping Properties

        public const string Dimension_Data_Place_Holder = "Dimension Data Place Holder";
        public const string Related_Dimension_Data_Place_Holder = "Related Dimension Data Place Holder";

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

		private string _dimensionName;
		public string DimensionName
		{
			get
			{
                return _dimensionName;
			}
			set
			{
                _dimensionName = value;
			}
		}

        private string _attributeName;
		public string AttributeName
		{
			get
			{
                return _attributeName;
			}
			set
			{
                _attributeName = value;
			}
		}

        private string _setDimensionName;
        public string SetDimensionName
        {
            get
            {
                return _setDimensionName;
            }
            set
            {
                _setDimensionName = value;
            }
        }

        private string _setAttributeName;
        public string SetAttributeName
        {
            get
            {
                return _setAttributeName;
            }
            set
            {
                _setAttributeName = value;
            }
        }

        private string _relatedDimensionName;
        public string RelatedDimensionName
        {
            get
            {
                return _relatedDimensionName;
            }
            set
            {
                _relatedDimensionName = value;
            }
        }

        private string _relatedAttributeName;
        public string RelatedAttributeName
        {
            get
            {
                return _relatedAttributeName;
            }
            set
            {
                _relatedAttributeName = value;
            }
        }

        private string _MDXFormula;
        public string MDXFormula
        {
            get
            {
                return _MDXFormula;
            }
            set
            {
                _MDXFormula = value;
            }
        }

        private string _relatedMDXFormula;
        public string RelatedMDXFormula
        {
            get
            {
                return _relatedMDXFormula;
            }
            set
            {
                _relatedMDXFormula = value;
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
            CubeDimension another = obj as CubeDimension;
			
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
