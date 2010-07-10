using System;
using System.Collections;
using System.Text;

using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.Security
{
    [Serializable]
    public class Role : EntityBase
    {
        #region O/R Mapping Properties

        private string _name;
        private string _description;

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
            Role r = obj as Role;

            if (r == null)
            {
                return false;
            }
            else
            {
                return (this.Name == r.Name)
                    && (this.Description == r.Description);
            }
        }


    }
}
