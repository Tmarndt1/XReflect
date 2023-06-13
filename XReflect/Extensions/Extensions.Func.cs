using System;
using System.Linq.Expressions;

namespace XReflect
{
    public static partial class Extensions
    {
        internal static Expression<Func<T1, T2, bool>> ToExpression<T1, T2>(this Func<T1, T2, bool> f)
        {
            return (a, b) => f(a, b);
        }
    }
}
