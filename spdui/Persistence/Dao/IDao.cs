using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Dao
{
    /// <summary>
    /// Summary description for IDaoBase.
    /// </summary>
    /// <remarks>
    /// Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>

    public interface IDao
    {
        Array FindAll(Type type);

        Array FindAll(Type type, int firstRow, int maxRows);

        object FindById(Type type, object id);

        object Create(object instance);

        void Update(object instance);       

        void Delete(object instance);
       
        void DeleteAll(Type type);

        void Save(object instance);
    }
}
