using System;
using System.Collections.Generic;
using System.Text;

namespace Dndp.Persistence.Criteria.Expression
{
    public sealed class Expression
    {
        private Expression()
        {
        }

        public static SimpleExpression Eq(string propertyName, object value)
        {
            return new EqExpression(propertyName, value);
        }

        public static SimpleExpression Like(string propertyName, object value)
        {
            return new LikeExpression(propertyName, value);
        }

        public static SimpleExpression Like(string propertyName, string value, MatchMode matchMode)
        {
            return new LikeExpression(propertyName, value, matchMode);
        }

        public static SimpleExpression Gt(string propertyName, object value)
        {
            return new GtExpression(propertyName, value);
        }

        public static SimpleExpression Lt(string propertyName, object value)
        {
            return new LtExpression(propertyName, value);
        }

        public static SimpleExpression Le(string propertyName, object value)
        {
            return new LeExpression(propertyName, value);
        }

        public static SimpleExpression Ge(string propertyName, object value)
        {
            return new GeExpression(propertyName, value);
        }

        public static IExpression Between(string propertyName, object lo, object hi)
        {
            return new BetweenExpression(propertyName, lo, hi);
        }

        public static IExpression In(string propertyName, object[] values)
        {
            return new InExpression(propertyName, values);
        }

        public static IExpression IsNull(string propertyName)
        {
            return new NullExpression(propertyName);
        }

        public static IExpression EqProperty(string propertyName, string otherPropertyName)
        {
            return new EqPropertyExpression(propertyName, otherPropertyName);
        }

        public static IExpression LtProperty(string propertyName, string otherPropertyName)
        {
            return new LtPropertyExpression(propertyName, otherPropertyName);
        }

        public static IExpression LeProperty(string propertyName, string otherPropertyName)
        {
            return new LePropertyExpression(propertyName, otherPropertyName);
        }

        public static IExpression IsNotNull(string propertyName)
        {
            return new NotNullExpression(propertyName);
        }

        public static IExpression And(IExpression lhs, IExpression rhs)
        {
            return new AndExpression(lhs, rhs);
        }

        public static IExpression Or(IExpression lhs, IExpression rhs)
        {
            return new OrExpression(lhs, rhs);
        }

        public static IExpression Not(IExpression expression)
        {
            return new NotExpression(expression);
        }

        
        
        public static Conjunction Conjunction()
        {
            return new Conjunction();
        }

        public static Disjunction Disjunction()
        {
            return new Disjunction();
        }
    }
}
