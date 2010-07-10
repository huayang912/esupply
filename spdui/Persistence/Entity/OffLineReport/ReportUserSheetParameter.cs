using System;
using System.Collections;

//TODO: Add other using statements here

namespace Dndp.Persistence.Entity.OffLineReport
{
    [Serializable]
    public class ReportUserSheetParameter : EntityBase
    {
        #region O/R Mapping Properties

        private ReportUser _theUser;
        public ReportUser TheUser
		{
			get
			{
                return _theUser;
			}
			set
			{
                _theUser = value;
			}
		}

        private ReportParameter _theParamter;
        public ReportParameter TheParamter
        {
            get
            {
                return _theParamter;
            }
            set
            {
                _theParamter = value;
            }
        }
		
		private string _parameterContent;
		public string ParameterContent
		{
			get
			{
				return _parameterContent;
			}
			set
			{
				_parameterContent = value;
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
            ReportUserSheetParameter another = obj as ReportUserSheetParameter;
			
            if (another == null)
            {
                return false;
            }
            else
            {
                return (this.Id == another.Id) && (this.TheUser == another.TheUser) && (this.ParameterContent == another.ParameterContent);
            }
        } 
    }
	
}
