using System;
using System.Collections.Generic;
using System.Text;
using NHibernate.Expression;
using NHibernate.Type;

namespace Dndp.Persistence.Dao
{
    /// <summary>
    /// Summary description for INHDaoBase.
    /// </summary>
    /// <remarks>
    /// Contributed by Jackey <Jackey.ding@atosorigin.com>
    /// </remarks>
    
    interface INHDao : IDao
    {
        void Delete(string hqlString);

        void Delete(string hqlString, object value, IType type);

        void Delete(string hqlString, object[] values, IType[] type);

        Array FindAll(Type type, ICriterion[] criterias);

        Array FindAll(Type type, ICriterion[] criterias, int firstRow, int maxRows);

        Array FindAll(Type type, ICriterion[] criterias, Order[] sortItems);

        Array FindAll(Type type, ICriterion[] criterias, Order[] sortItems, int firstRow, int maxRows);

        Array FindAllWithCustomQuery(string queryString);        

        Array FindAllWithCustomQuery(string queryString, object value);

        Array FindAllWithCustomQuery(string queryString, object value, IType type);       

        Array FindAllWithCustomQuery(string queryString, object[] values);

        Array FindAllWithCustomQuery(string queryString, object[] values, IType[] types);

        Array FindAllWithCustomQuery(string queryString, int firstRow, int maxRows);

        Array FindAllWithCustomQuery(string queryString, object value, int firstRow, int maxRows);

        Array FindAllWithCustomQuery(string queryString, object value, IType type, int firstRow, int maxRows);

        Array FindAllWithCustomQuery(string queryString, object[] values, int firstRow, int maxRows);

        Array FindAllWithCustomQuery(string queryString, object[] values, IType[] type, int firstRow, int maxRows);

        Array FindAllWithNamedQuery(string namedQuery);

        Array FindAllWithNamedQuery(string namedQuery, object value);

        Array FindAllWithNamedQuery(string namedQuery, object value, IType type);

        Array FindAllWithNamedQuery(string namedQuery, object[] values);

        Array FindAllWithNamedQuery(string namedQuery, object[] values, IType[] types);

        Array FindAllWithNamedQuery(string namedQuery, int firstRow, int maxRows);

        Array FindAllWithNamedQuery(string namedQuery, object value, int firstRow, int maxRows);

        Array FindAllWithNamedQuery(string namedQuery, object value, IType type, int firstRow, int maxRows);

        Array FindAllWithNamedQuery(string namedQuery, object[] values, int firstRow, int maxRows);

        Array FindAllWithNamedQuery(string namedQuery, object[] values, IType[] type, int firstRow, int maxRows);

        void InitializeLazyProperties(object instance);

        void InitializeLazyProperty(object instance, string propertyName);
    }
}
