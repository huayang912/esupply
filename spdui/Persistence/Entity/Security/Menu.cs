using System;
using System.Collections;
using System.Text;

using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.Security
{
    [Serializable]
    public class Menu : EntityBase, System.IComparable
    {
        #region O/R Mapping Properties

        private string _title;
        private string _description;
        private string _pathCode;
        private int _parentMenuId;
        private Module _theModule;

        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                _title = value;
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

        public string PathCode
        {
            get
            {
                return _pathCode;
            }
            set
            {
                _pathCode = value;
            }
        }

        public int ParentMenuId
        {
            get
            {
                return _parentMenuId;
            }
            set
            {
                _parentMenuId = value;
            }
        }

        public Module TheModule
        {
            get
            {
                return _theModule;
            }
            set
            {
                _theModule = value;
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
            Menu m = obj as Menu;

            if (m == null)
            {
                return false;
            }
            else
            {
                return (this.Title == m.Title)
                    && (this.Description == m.Description)
                    && (this.PathCode == m.PathCode)
                    && (this.ParentMenuId == m.ParentMenuId);
            }
        }

        public int CompareTo(object obj)
        {
            Menu another = obj as Menu;
            if (another == null)
            {
                throw new ArgumentException("Invalid Compare Type");
            }

            if (this.PathCode.Length < another.PathCode.Length)
            {
                return -1;
            }
            else if (this.PathCode.Length > another.PathCode.Length)
            {
                return 1;
            }
            else
            {
                return this.PathCode.CompareTo(another.PathCode);
            }
        }
    }
}
