using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    public enum MatchMode
    {
        Exact,
        Start,
        End,
        Anywhere,
        None
    }

    public class NHMatchModeConvert
    {
        private NHMatchModeConvert()
        { 
        }

        public static NHibernate.Expression.MatchMode ToNHMatchMode(MatchMode matchMode)
        {
            switch (matchMode)
            {
                case MatchMode.Start:
                    return NHibernate.Expression.MatchMode.Start;

                case MatchMode.End:
                    return NHibernate.Expression.MatchMode.Start;

                case MatchMode.Exact:
                    return NHibernate.Expression.MatchMode.Exact;

                case MatchMode.Anywhere:
                    return NHibernate.Expression.MatchMode.Anywhere;
                
                case MatchMode.None:
                    return null;

                default:
                    return null;
            }
        }
    }
}
