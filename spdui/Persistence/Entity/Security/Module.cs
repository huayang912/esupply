using System;
using System.Collections;
using System.Text;

using Dndp.Persistence.Entity.Security;

namespace Dndp.Persistence.Entity.Security
{
    [Serializable]
    public class Module : EntityBase
    {
        #region O/R Mapping Properties

        private string _name;
        private string _description;
        private string _sourceFile;

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

        public string SourceFile
        {
            get
            {
                return _sourceFile;
            }
            set
            {
                _sourceFile = value;
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
            Module m = obj as Module;

            if (m == null)
            {
                return false;
            }
            else
            {
                return (this.Name == m.Name)
                    && (this.Description == m.Description)
                    && (this.SourceFile == m.SourceFile);
            }

        }
    }
}
