using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace com.LocalSystem.Service
{
    public interface ISession
    {
        IList<T> FindAll<T>();

        IList<T> FindAll<T>(int firstRow, int maxRows);

        T FindById<T>(object id);

        object Create(object instance);

        void Update(object instance);

        void Delete(object instance);

        void DeleteAll(Type type);

        void Save(object instance);

        void FlushSession();

        void CleanSession();
    }
}

#region

namespace com.LocalSystem.Service.Ext
{
    public partial interface ISessionE : ISession
    {

    }
}

#endregion