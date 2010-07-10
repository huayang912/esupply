using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Dndp.Service
{
    public interface ISession
    {
        IList FindAll(Type type);

        IList FindAll(Type type, int firstRow, int maxRows);

        object FindById(Type type, object id);

        object Create(object instance);

        void Update(object instance);

        void Delete(object instance);

        void DeleteAll(Type type);

        void Save(object instance);
    }
}
